namespace KraftWrapper.Interfaces
{
    public interface ISitecoreSiteContext
    {
        ISitecoreItem RootItem { get; }
        string Name { get; }
        string StartPath { get; }
    }
}