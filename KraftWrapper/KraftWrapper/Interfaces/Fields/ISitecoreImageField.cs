using System;

namespace KraftWrapper.Interfaces.Fields
{
    public interface ISitecoreImageField : ISitecoreBaseCustomField
    {
        Guid Id { get; }
        string Url { get; }    
        string Src { get; }
        string Width { get; }    
        string Alt { get; }
    }
}
