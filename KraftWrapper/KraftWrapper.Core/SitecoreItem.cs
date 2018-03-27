using KraftWrapper.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KraftWrapper.Core
{
    partial class SitecoreItem : ISitecoreItem
    {
        private Item _item;

        public SitecoreItem()
        {

        }

        public SitecoreItem(Item item)
        {
            _item = item ?? throw new ArgumentNullException("Input item is null.");
        }

        public virtual Guid Id
        {
            get
            {
                return _item.ID.Guid;
            }
        }

        public virtual Guid TemplateId
        {
            get
            {
                return _item.TemplateID.Guid;
            }
        }

        public virtual string TemplateName
        {
            get
            {
                return _item.TemplateName;
            }
        }

        public virtual string Name
        {
            get
            {
                return _item.Name;
            }
        }

        public virtual string DisplayName
        {
            get
            {
                return _item.DisplayName;
            }
        }

        public virtual string FullPath
        {
            get { return _item.Paths.FullPath; }
        }

        public virtual ISitecoreItemUri ItemUri
        {
            get
            {
                return _item.Uri != null ? new SitecoreItemUri(_item.Uri) : null;
            }
        }

        public virtual ISitecoreLanguage Language
        {
            get
            {
                return _item.Language != null ? new SitecoreLanguage(_item.Language) : null;
            }
        }

        public virtual ISitecoreField GetField(Guid id)
        {
            var sitecoreId = new ID(id);
            return CreateSitecoreField(_item.Fields[sitecoreId]);
        }

        public virtual ISitecoreField GetField(int index)
        {
            return CreateSitecoreField(_item.Fields[index]);
        }

        public virtual ISitecoreField GetField(string name)
        {
            return CreateSitecoreField(_item.Fields[name]);
        }

        public virtual string GetPropertyValue(Guid id)
        {
            var sitecoreId = new ID(id);
            return _item[sitecoreId];
        }

        public virtual string GetPropertyValue(int index)
        {
            return _item[index];
        }

        public virtual string GetPropertyValue(string name)
        {
            return _item[name];
        }

        public virtual IList<ISitecoreItem> SelectSubItems(string query = "")
        {
            var subItems = _item.Axes
                .SelectItems(query);

            if (subItems == null)
            {
                return new List<ISitecoreItem>();
            }

            return subItems
                .Select(x => (ISitecoreItem)new SitecoreItem(x))
                .ToList();
        }

        public virtual IList<ISitecoreItem> GetChildren()
        {
            var children = _item.GetChildren();

            if (children == null)
            {
                return new List<ISitecoreItem>();
            }

            return children
                .Select(i => (ISitecoreItem)new SitecoreItem(i))
                .ToList();
        }

        public virtual IList<ISitecoreItem> GetDescendants()
        {
            var descendants = _item.Axes
                .GetDescendants();

            if (descendants == null)
            {
                return new List<ISitecoreItem>();
            }

            return descendants
                .Select(x => (ISitecoreItem)new SitecoreItem(x))
                .ToList();
        }

        public virtual IList<ISitecoreItem> GetVersions()
        {
            var versions = _item.Versions.GetVersions();

            if (versions == null)
            {
                return new List<ISitecoreItem>();
            }

            return versions
                .Select(x => (ISitecoreItem)new SitecoreItem(x))
                .ToList();
        }

        public virtual ISitecoreItem Add(string newItemName, ISitecoreTemplate template)
        {
            var newItem = _item.Add(newItemName, new TemplateID(new ID(template.Id)));

            return new SitecoreItem(newItem);
        }

        public void BeginEdit()
        {
            _item.Editing.BeginEdit();
        }

        public void EndEdit()
        {
            _item.Editing.EndEdit();
        }

        public void CancelEdit()
        {
            _item.Editing.CancelEdit();
        }

        private static ISitecoreField CreateSitecoreField(Field field)
        {
            if (field == null)
            {
                return null;
            }

            return new SitecoreField(field);
        }
    }
}
