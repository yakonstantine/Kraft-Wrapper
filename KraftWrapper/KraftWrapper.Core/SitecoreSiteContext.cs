using KraftWrapper.Interfaces;
using Sitecore.Sites;
using System;

namespace KraftWrapper.Core
{
    class SitecoreSiteContext : ISitecoreSiteContext
    {
        private readonly SiteContext _siteContext;
        private readonly ISitecoreDatabase _sitecoreDatabase;

        public SitecoreSiteContext(SiteContext siteContext, ISitecoreDatabase sitecoreDatabase)
        {
            if (siteContext == null || sitecoreDatabase == null)
            {
                throw new ArgumentNullException("One from the input parametrs is null.");
            }

            _siteContext = siteContext;
            _sitecoreDatabase = sitecoreDatabase;
        }

        public ISitecoreItem RootItem
        {
            get
            {
                return _sitecoreDatabase.GetItem(_siteContext.RootPath);
            }
        }

        public string Name
        {
            get
            {
                return _siteContext.Name;
            }
        }

        public string StartPath
        {
            get
            {
                return _siteContext.StartPath;
            }
        }
    }
}
