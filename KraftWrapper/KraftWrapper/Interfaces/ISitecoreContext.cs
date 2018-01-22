using System.Collections.Generic;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreContext
    {
        ISitecoreItem CurrentItem { get; }
        ISitecoreItem HomepageItem { get; }
        ISitecoreSiteContext Site { get; }
        ISitecoreDatabase Database { get; }
        ISitecoreItem DatasourceItem { get; }
        IDictionary<string, string> RenderingParameters { get; }
        string Placeholder { get; }
        bool IsPreview { get; }
        bool IsExperienceEditor { get; }
    }
}
