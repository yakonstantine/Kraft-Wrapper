using KraftWrapper.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KraftWrapper.Core
{
    public class SitecoreItem : ISitecoreItem
    {
        private Item _item;

        public SitecoreItem(Item item)
        {
            _item = item ?? throw new ArgumentNullException("Input item is null.");
        }

        public Guid Id
        {
            get
            {
                return _item.ID.Guid;
            }
        }

        public Guid TemplateId
        {
            get
            {
                return _item.TemplateID.Guid;
            }
        }

        public string Name
        {
            get
            {
                return _item.Name;
            }
        }

        public string DisplayName
        {
            get
            {
                return _item.DisplayName;
            }
        }

        public ISitecoreItemUri ItemUri
        {
            get
            {
                return _item.Uri != null ? new SitecoreItemUri(_item.Uri) : null;
            }
        }

        public ISitecoreLanguage Language
        {
            get
            {
                return _item.Language != null ? new SitecoreLanguage(_item.Language) : null;
            }
        }

        public ISitecoreField GetField(Guid id)
        {
            var sitecoreId = new ID(id);
            return CreateSitecoreField(_item.Fields[sitecoreId]);
        }

        public ISitecoreField GetField(int index)
        {
            return CreateSitecoreField(_item.Fields[index]);
        }

        public ISitecoreField GetField(string name)
        {
            return CreateSitecoreField(_item.Fields[name]);
        }

        public string GetPropertyValue(Guid id)
        {
            var sitecoreId = new ID(id);
            return _item[sitecoreId];
        }

        public string GetPropertyValue(int index)
        {
            return _item[index];
        }

        public string GetPropertyValue(string name)
        {
            return _item[name];
        }

        public IList<ISitecoreItem> SelectSubItems(string query = "")
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

        private static ISitecoreField CreateSitecoreField(Field field)
        {
            if (field == null)
            {
                return null;
            }

            return new SitecoreField(field);
        }

        public IList<ISitecoreItem> GetChildren()
        {
            return _item.GetChildren()
                .Select(i => (ISitecoreItem)new SitecoreItem(i))
                .ToList();
        }
    }
}
