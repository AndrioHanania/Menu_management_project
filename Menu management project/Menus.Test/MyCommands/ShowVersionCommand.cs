using Menus.Interfaces;

namespace Menus.Test
{
    public class ShowVersionCommand : ICommand
    {
        void ICommand.Execute()
        {
            MyCommands.ShowVersion();
        }
    }
}