using System;

namespace Menus.Delegates
{
    public delegate void MenuItemAction();

    public class MenuItemWithDelegate : IMenuItem
    {
        public event MenuItemAction ActionExecution;

        public MenuItemWithDelegate(string i_Name, MenuItemAction i_LogicalMethod)
        {
            Name = i_Name;
            ActionExecution += i_LogicalMethod;
        }

        void IMenuItem.Show()
        {
            Console.WriteLine(Name);
        }

        public void Execution()
        {
            OnExecution();
        }

        protected virtual void OnExecution()
        {
            ActionExecution?.Invoke();
        }

        public string Name { get; set; }




    }
}