namespace Menus.Interfaces
{
    public interface ISupportListeners
    {
        void AddToListeners(IListener i_Listener);

       void RemoveFromListeners(IListener i_Listener);

    }
}