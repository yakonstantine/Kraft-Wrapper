namespace KraftWrapper.Interfaces.Fields
{
    public interface ISitecoreInternalLinkField : ISitecoreBaseCustomField
    {
        string Path { get; }
        string ItemUrl { get; } 
        ISitecoreItem TargetItem { get; }
    }
}
