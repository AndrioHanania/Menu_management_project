using Menus.Interfaces;

namespace Menus.Test
{
    public class CountSpacesCommand : ICommand
    {
        void ICommand.Execute()
        {
            MyCommands.CountSpaces();
        }
    }
}