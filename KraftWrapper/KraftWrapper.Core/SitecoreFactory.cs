using KraftWrapper.Interfaces;
using Sitecore.Configuration;

namespace KraftWrapper.Core
{
    public class SitecoreFactory : ISitecoreFactory
    {
        public ISitecoreDatabase GetDatabase(string databaseName)
        {
            var database = Factory.GetDatabase(databaseName);

            if (database == null)
            {
                return null;
            }

            return new SitecoreDatabase(database);
        }

        public ISitecoreDatabase GetDatabase(string databaseName, bool assert)
        {
            var database = Factory.GetDatabase(databaseName, assert);

            if (database == null)
            {
                return null;
            }

            return new SitecoreDatabase(database);
        }
    }
}
