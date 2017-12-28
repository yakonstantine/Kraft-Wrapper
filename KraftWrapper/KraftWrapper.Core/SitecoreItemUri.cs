using KraftWrapper.Interfaces;
using Sitecore.Data;
using System;

namespace KraftWrapper.Core
{
    public class SitecoreItemUri : ISitecoreItemUri
    {
        private readonly ItemUri _itemUri;

        public SitecoreItemUri(ItemUri itemUri)
        {
            _itemUri = itemUri ?? throw new ArgumentNullException("Input ItemUri is null."); ;
        }

        public string DatabaseName
        {
            get { return _itemUri.DatabaseName; }
        }

        public bool IsEmpty
        {
            get { return _itemUri.IsEmpty; }
        }

        public Guid ItemID
        {
            get { return _itemUri.ItemID.Guid; }
        }

        public ISitecoreLanguage Language
        {
            get
            {
                return _itemUri.Language != null ? new SitecoreLanguage(_itemUri.Language) : null;
            }
        }

        public string Path
        {
            get { return _itemUri.Path; }
        }
    }
}
