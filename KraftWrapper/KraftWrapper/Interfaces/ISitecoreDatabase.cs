using System;
using System.Collections.Generic;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreDatabase
    {
        string ConnectionStringName { get; }
        IList<ISitecoreLanguage> Languages { get; }
        string Name { get; }

        ISitecoreItem GetItem(string path);
        ISitecoreItem GetItem(string path, ISitecoreLanguage language);
        ISitecoreItem GetItem(Guid id);
        ISitecoreItem GetItem(Guid id, ISitecoreLanguage language);
        ISitecoreTemplate GetTemplate(Guid templateId);
        ISitecoreTemplate GetTemplate(string fullName);
    }
}