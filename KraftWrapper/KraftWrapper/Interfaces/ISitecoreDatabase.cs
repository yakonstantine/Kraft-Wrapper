using System;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreDatabase
    {
        ISitecoreItem GetItem(string path);
        ISitecoreItem GetItem(Guid id);
    }
}