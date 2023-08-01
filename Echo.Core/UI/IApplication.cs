namespace Echo.Core.UI
{
    public interface IApplication
    {
        string Title { get; set; }
        bool IsActive { get; }
        bool IsMinimized { get; }
    }
}