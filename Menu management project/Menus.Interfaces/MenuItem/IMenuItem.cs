namespace Menus.Interfaces
{
    public interface IMenuItem
    {
        void Show();

        string Name { get; set; }
    }
}