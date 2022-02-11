using System.Collections.Generic;

namespace Menus.Interfaces
{
    public class BackCommand : ICommand
    {
        private Stack<MenuItemWithSubMenu> m_CommandStack;

        public BackCommand(Stack<MenuItemWithSubMenu> i_CommandStack)
        {
            m_CommandStack = i_CommandStack;
        }

        void ICommand.Execute()
        {
            if (m_CommandStack.Count > 0)
            {
                m_CommandStack.Pop();
            }
        }
    }
}