using KraftWrapper.Options;

namespace KraftWrapper.Interfaces
{
    public interface ISavableSitecoreItem
    {
        ISitecoreItem SourceItem { get; }
        ISitecoreLanguage Language { get; }
        void Save();
        void Publish(PublishMode publishMode, bool deep, bool withRelatedItems);
    }
}
