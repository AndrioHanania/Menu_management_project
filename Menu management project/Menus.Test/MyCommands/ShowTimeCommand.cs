using System;
using Menus.Interfaces;

namespace Menus.Test
{
    public class ShowTimeCommand : ICommand
    {
        void ICommand.Execute()
        {
            MyCommands.ShowTime();
        }
    }
}