using Menus.Interfaces;

namespace Menus.Test
{
    public class MenuListener : IListener
    {
        void IListener.OnNotify(ICommand i_Command)
        {
            i_Command.Execute();
        }
    }
}