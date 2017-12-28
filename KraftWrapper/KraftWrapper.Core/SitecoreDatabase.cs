using KraftWrapper.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace KraftWrapper.Core
{
    public class SitecoreDatabase : ISitecoreDatabase
    {
        private Database _database;

        public SitecoreDatabase(Database database)
        {
            _database = database ?? throw new ArgumentNullException("Input sitecore database is null.");
        }

        public ISitecoreItem GetItem(string path)
        {
            return CreateSitecoreItem(_database.GetItem(path));
        }

        public ISitecoreItem GetItem(Guid id)
        {
            var sitecoreId = new ID(id);
            return CreateSitecoreItem(_database.GetItem(sitecoreId));
        }

        private static SitecoreItem CreateSitecoreItem(Item item)
        {
            if (item == null)
            {
                return null;
            }

            return new SitecoreItem(item);
        }
    }
}