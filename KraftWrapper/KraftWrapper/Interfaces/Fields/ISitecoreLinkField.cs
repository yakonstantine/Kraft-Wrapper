namespace KraftWrapper.Interfaces.Fields
{
    public interface ISitecoreLinkField : ISitecoreBaseCustomField
    {
        string Text { get; }
        string FriendlyUrl { get; }
    }
}
