using System;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreItemUri
    {
        string DatabaseName { get; }
        bool IsEmpty { get; }
        Guid ItemID { get; }
        ISitecoreLanguage Language { get; }
        string Path { get; }
    }
}
