using System;
using Menus.Interfaces;

namespace Menus.Test
{
    public class ShowDateCommand : ICommand
    {
        void ICommand.Execute()
        {
            MyCommands.ShowDate();
        }
    }
}