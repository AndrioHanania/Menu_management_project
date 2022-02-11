using System;
using System.Collections.Generic;

namespace Menus.Interfaces
{
    public class MainMenu : ISupportListeners
    {
        private Stack<MenuItemWithSubMenu> m_MenusStack;
        private readonly LinkedList<IListener> r_Listeners;
        private bool m_IsRunning;

        public MainMenu(MenuItemWithSubMenu i_MenuItemWithSubMenu)
        {
            r_Listeners = new LinkedList<IListener>();
            m_MenusStack = new Stack<MenuItemWithSubMenu>();
            m_IsRunning = true;
            m_MenusStack.Push(i_MenuItemWithSubMenu);
        }

        void ISupportListeners.AddToListeners(IListener i_Listener)
        {
            r_Listeners.AddLast(i_Listener);
        }

        void ISupportListeners.RemoveFromListeners(IListener i_Listener)
        {
            r_Listeners.Remove(i_Listener);
        }

        public void Show()
        {
            MenuItemWithSubMenu firstMenuItem = m_MenusStack.Peek();

            firstMenuItem.MenuItems.Insert(firstMenuItem.MenuItems.Count,
                new MenuItemWithCommand("Exit", new BackCommand(m_MenusStack)));
            insertBackItemToAllSubMenu(firstMenuItem);
            while (m_IsRunning)
            {
                Console.Clear();
                ((IMenuItem)m_MenusStack.Peek()).Show();
                Console.WriteLine("Please enter menu item: ");
                bool isValid = isValidSelection(Console.ReadLine(), out int selectionValue);

                if(isValid)
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

        private void insertBackItemToAllSubMenu(MenuItemWithSubMenu i_SubMenuItem)
        {
            foreach (IMenuItem menuItem in i_SubMenuItem.MenuItems)
            {
                if(menuItem.GetType() == typeof(MenuItemWithSubMenu))
                {
                    MenuItemWithSubMenu subMenuItem = menuItem as MenuItemWithSubMenu;

                    subMenuItem.MenuItems.Insert(subMenuItem.MenuItems.Count,
                        new MenuItemWithCommand("Back", new BackCommand(m_MenusStack)));
                    insertBackItemToAllSubMenu(subMenuItem);
                }
            }
        }

        private void showItem(IMenuItem i_Item)
        {
            if(i_Item is MenuItemWithCommand commandItem)
            {
                Console.Clear();
                if (commandItem.Command is BackCommand)
                {
                    commandItem.Command.Execute();
                }
                else
                {
                    ((IMenuItem)commandItem).Show();
                    notifyAllListeners(commandItem.Command);
                    pressAnyKeyToContinue();
                }
            }
            else if(i_Item is MenuItemWithSubMenu subMenuItem)
            {
                m_MenusStack.Push(subMenuItem);
            }
        }

        private void notifyAllListeners(ICommand i_Command)
        {
            foreach (IListener listener in r_Listeners)
            {
                listener.OnNotify(i_Command);
            }
        }

        private bool isValidSelection(string i_InputSelection, out int i_SelectionValue)
        {
            bool isParsed = int.TryParse(i_InputSelection, out i_SelectionValue);

            return isParsed && i_SelectionValue >= 0 && i_SelectionValue <=
                   m_MenusStack.Peek().MenuItems.Count - 1;
        }

        private static void pressAnyKeyToContinue()
        {
            Console.WriteLine("Press Any Key to continue");
            Console.ReadLine();
        }
    }
}