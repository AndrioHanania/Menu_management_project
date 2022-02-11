using System;
using System.Collections.Generic;
using System.Text;

namespace Menus.Interfaces
{
    public class MenuItemWithSubMenu : IMenuItem
    {
        private List<IMenuItem> m_MenuItems;

        public MenuItemWithSubMenu(string i_Name, List<IMenuItem> i_Items)
        {
            Name = i_Name;
            m_MenuItems = i_Items;
        }

        void IMenuItem.Show()
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;

            sb.AppendLine(Name);
            sb.AppendLine("--------------------");
            foreach (IMenuItem item in m_MenuItems)
            {
                if(index != m_MenuItems.Count)
                {
                    sb.AppendLine($"{index}) {item.Name}");
                    index++;
                }
            }

            sb.AppendLine($"0) {m_MenuItems[m_MenuItems.Count - 1].Name}");
            Console.WriteLine(sb);
        }

        public string Name { get; set; }

        public List<IMenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }
        }
    }
}