using KraftWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace KraftWrapper.Options
{
    public class PublishOptions
    {
        private readonly ISitecoreLanguage _language;
        private readonly ISitecoreItem _rootItem;
        private readonly ISitecoreDatabase _targetDatabase;
        private readonly ISitecoreDatabase _sourceDatabase;

        public PublishOptions(
            ISitecoreDatabase sourceDatabase,
            ISitecoreDatabase targetDatabase,
            ISitecoreItem rootItem,
            ISitecoreLanguage language
            )
        {
            _sourceDatabase = sourceDatabase;
            _targetDatabase = targetDatabase;
            _rootItem = rootItem;
            _language = language;
        }

        public bool Deep { get; set; }
        public ISitecoreLanguage Language { get { return _language; } }
        public PublishMode Mode { get; set; }
        public DateTime PublishDate { get; set; }
        public IList<string> PublishingTargets { get; set; }
        public bool PublishRelatedItems { get; set; }
        public bool RepublishAll { get; set; }
        public ISitecoreItem RootItem { get { return _rootItem; } }
        public ISitecoreDatabase SourceDatabase { get { return _sourceDatabase; } }
        public ISitecoreDatabase TargetDatabase { get { return _targetDatabase; } }
        public string UserName { get; set; } = string.Empty;
    }

    public enum PublishMode
    {
        Unknown = 0,
        Full = 1,
        Incremental = 2,
        SingleItem = 3,
        Smart = 4
    }
}
