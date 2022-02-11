using System;

namespace Menus.Interfaces
{
    public class MenuItemWithCommand : IMenuItem
    {
        private ICommand m_Command;

        public MenuItemWithCommand(string i_Name, ICommand i_Command)
        {
            Name = i_Name;
            m_Command = i_Command;
        }

        void IMenuItem.Show()
        {
            Console.WriteLine(Name);
        }

        public ICommand Command
        {
            get { return m_Command; }
        }

        public string Name { get; set; }
    }
}