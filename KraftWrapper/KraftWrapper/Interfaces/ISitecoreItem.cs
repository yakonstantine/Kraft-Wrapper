using System;
using System.Collections.Generic;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreItem : IAutoMappable
    {
        Guid Id { get; }
        Guid TemplateId { get; }
        string TemplateName { get; }
        string Name { get; }
        string DisplayName { get; }
        ISitecoreItemUri ItemUri { get; }
        ISitecoreLanguage Language { get; }
        string FullPath { get; }

        ISitecoreField GetField(string name);
        ISitecoreField GetField(int index);
        ISitecoreField GetField(Guid id);

        string GetPropertyValue(string name);
        string GetPropertyValue(int index);
        string GetPropertyValue(Guid id);

        IList<ISitecoreItem> SelectSubItems(string query = "");
        IList<ISitecoreItem> GetChildren();
        IList<ISitecoreItem> GetDescendants();
        IList<ISitecoreItem> GetVersions();

        ISitecoreItem Add(string newItemName, ISitecoreTemplate template);

        void BeginEdit();
        void EndEdit();
        void CancelEdit();
    }
}