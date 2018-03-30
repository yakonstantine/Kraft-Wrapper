using KraftWrapper.Interfaces;
using System;

namespace KraftWrapper.Core.Helpers
{
    static class DefaultImplementationFactory
    {
        public static SitecoreLanguage GetDefaultImplementation(ISitecoreLanguage language)
        {
            if (language == null)
            {
                throw new ArgumentNullException("Input parameter is null.");
            }

            var defaultImplementation = language as SitecoreLanguage;

            if (defaultImplementation == null)
            {
                throw new ArgumentException($"The language input parameter is not a defaul implamantation of ISitecoreLanguage. Language name is {language.Name}.");
            }

            return defaultImplementation;
        }

        public static SitecoreDatabase GetDefaultImplementation(ISitecoreDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("Input parameter is null.");
            }

            var defaultImplementation = database as SitecoreDatabase;

            if (defaultImplementation == null)
            {
                throw new ArgumentException($"The database input parameter is not a defaul implamantation of ISitecoreLanguage. DB name is {database.Name}.");
            }

            return defaultImplementation;
        }

        public static SitecoreItem GetDefaultImplementation(ISitecoreItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Input parameter is null.");
            }

            var defaultImplementation = item as SitecoreItem;

            if (defaultImplementation == null)
            {
                throw new ArgumentException($"The item input parameter is not a defaul implamantation of ISitecoreLanguage. Item name is {item.Name}.");
            }

            return defaultImplementation;
        }
    }
}
