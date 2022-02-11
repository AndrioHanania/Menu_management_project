using System;
using System.Collections.Generic;

namespace Menus.Delegates
{
    public class MainMenu : IMenu
    {
        private readonly Stack<MenuItemWithSubMenu> m_MenusStack;
        private bool m_IsRunning;

        public event Action<MenuItemWithDelegate> SelectedMenuItemWithEvent;

        public MainMenu(MenuItemWithSubMenu i_MenuItemWithSubMenu)
        {
            m_MenusStack = new Stack<MenuItemWithSubMenu>();
            m_IsRunning = true;
            m_MenusStack.Push(i_MenuItemWithSubMenu);
        }

        void IMenu.Show()
        {
            Show();
        }

        public void Show()
        {
            MenuItemWithSubMenu firstMenuItem = m_MenusStack.Peek();

            firstMenuItem.MenuItems.Insert(firstMenuItem.MenuItems.Count,
                new MenuItemWithDelegate("Exit", goBack));
            insertBackItemToAllSubMenu(firstMenuItem);
            while(m_IsRunning)
            {
                Console.Clear();
                m_MenusStack.Peek().Show();
                Console.WriteLine("Please enter menu item: ");
                bool isValid = isValidSelection(Console.ReadLine(), out int selectionValue);

                if (isValid)
                {
                    MenuItemWithSubMenu topItemInStack = m_MenusStack.Peek();

                    showItem(
                        selectionValue != 0
                            ? topItemInStack.MenuItems[selectionValue - 1]
                            : topItemInStack.MenuItems[topItemInStack.MenuItems.Count - 1]);

                    if (m_MenusStack.Count == 0)
                    {
                        m_IsRunning = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Selection");
                    pressAnyKeyToContinue();
                }
            }
        }

        private void showItem(IMenuItem i_Item)
        {
            if (i_Item is MenuItemWithDelegate delegateItem)
            {
                Console.Clear();
                if (delegateItem.Name.Equals("Exit") || delegateItem.Name.Equals("Back"))
                {
                    delegateItem.Execution();
                }
                else
                {
                    ((IMenuItem)delegateItem).Show();
                    OnSelectedMenuItemWithEvent(delegateItem);
                    pressAnyKeyToContinue();
                }
            }
            else if (i_Item is MenuItemWithSubMenu subMenuItem)
            {
                m_MenusStack.Push(subMenuItem);
            }
        }

        protected virtual void OnSelectedMenuItemWithEvent(MenuItemWithDelegate i_DelegateItem)
        {
            SelectedMenuItemWithEvent?.Invoke(i_DelegateItem);
        }

        private static void pressAnyKeyToContinue()
        {
            Console.WriteLine("Press Any Key to continue");
            Console.ReadLine();
        }

        private bool isValidSelection(string i_InputSelection, out int i_SelectionValue)
        {
            bool isParsed = int.TryParse(i_InputSelection, out i_SelectionValue);

            return isParsed && i_SelectionValue >= 0 && i_SelectionValue <=
                   m_MenusStack.Peek().MenuItems.Count - 1;
        }

        private void goBack()
        {
            if (m_MenusStack.Count > 0)
            {
                m_MenusStack.Pop();
            }
        }

        private void insertBackItemToAllSubMenu(MenuItemWithSubMenu i_SubMenuItem)
        {
            foreach (IMenuItem menuItem in i_SubMenuItem.MenuItems)
            {
                if (menuItem.GetType() == typeof(MenuItemWithSubMenu))
                {
                    MenuItemWithSubMenu subMenuItem = menuItem as MenuItemWithSubMenu;

                    subMenuItem.MenuItems.Insert(subMenuItem.MenuItems.Count,
                        new MenuItemWithDelegate("Back", goBack));
                    insertBackItemToAllSubMenu(subMenuItem);
                }
            }
        }

    }
}