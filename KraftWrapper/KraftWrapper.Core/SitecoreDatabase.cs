using KraftWrapper.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KraftWrapper.Core
{
    public class SitecoreDatabase : ISitecoreDatabase
    {
        private Database _database;

        public SitecoreDatabase(Database database)
        {
            _database = database ?? throw new ArgumentNullException("Input sitecore database is null.");
        }

        public string ConnectionStringName
        {
            get
            {
                return _database.ConnectionStringName;
            }
        }

        public IList<ISitecoreLanguage> Languages
        {
            get
            {
                if (_database.Languages == null)
                    return new List<ISitecoreLanguage>();

                return _database
                    .Languages
                    .Select(x => (ISitecoreLanguage)new SitecoreLanguage(x))
                    .ToList();
            }
        }

        public string Name
        {
            get
            {
                return _database.Name;
            }
        }

        public ISitecoreItem GetItem(string path)
        {
            return CreateSitecoreItem(_database.GetItem(path));
        }

        public ISitecoreItem GetItem(string path, ISitecoreLanguage language)
        {
            var item = _database.GetItem(path, ((SitecoreLanguage)language).RawValue);

            return CreateSitecoreItem(item);
        }

        public ISitecoreItem GetItem(Guid id)
        {
            var sitecoreId = new ID(id);
            return CreateSitecoreItem(_database.GetItem(sitecoreId));
        }

        public ISitecoreItem GetItem(Guid id, ISitecoreLanguage language)
        {
            var sitecoreId = new ID(id);
            var item = _database.GetItem(sitecoreId, ((SitecoreLanguage)language).RawValue);

            return CreateSitecoreItem(item);
        }

        private static ISitecoreItem CreateSitecoreItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new SitecoreItem(item);
        }
    }
}