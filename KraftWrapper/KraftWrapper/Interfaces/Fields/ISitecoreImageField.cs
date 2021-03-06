﻿namespace KraftWrapper.Interfaces.Fields
{
    public interface ISitecoreImageField : ISitecoreBaseCustomField
    {
        string Url { get; }    
        string Src { get; }
        string Width { get; }    
        string Alt { get; }
    }
}
