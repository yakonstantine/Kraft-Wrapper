namespace KraftWrapper.Interfaces
{
    public interface ISitecoreContext
    {
        ISitecoreItem CurrentItem { get; }
        ISitecoreItem HomepageItem { get; }
        bool IsPreview { get; }
        ISitecoreSiteContext Site { get; }
        ISitecoreDatabase Database { get; }
        ISitecoreItem DatasourceItem { get; }
        string Placeholder { get; }
        bool IsExperienceEditor { get; }
    }
}
