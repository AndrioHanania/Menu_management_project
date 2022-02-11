namespace Menus.Delegates
{
    public interface IMenuItem
    {
        void Show();

        string Name { get; set; }
    }
}