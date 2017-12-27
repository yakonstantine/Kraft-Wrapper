﻿using System;
using System.Collections.Generic;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreItem
    {
        Guid Id { get; }
        Guid TemplateID { get; }
        string Name { get; }
        string DisplayName { get; }
        ISitecoreItemUri ItemUri { get; }
        ISitecoreLanguage Language { get; }
        ISitecoreField GetField(string name);
        ISitecoreField GetField(int index);
        ISitecoreField GetField(Guid id);
        string GetPropertyValue(string name);
        string GetPropertyValue(int index);
        string GetPropertyValue(Guid id);
        IList<ISitecoreItem> SelectSubItems(string query = "");
        IList<ISitecoreItem> GetChildren();
    }
}