using KraftWrapper.Interfaces;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Sites;

namespace KraftWrapper.Core
{
    public class SitecoreContext : ISitecoreContext
    {
        private readonly Item _contextItem = Sitecore.Context.Item;
        private readonly Database _contextDatabase = Sitecore.Context.Database;
        private readonly SiteContext _siteContext = Sitecore.Context.Site;

        private readonly RenderingContext _renderingContext = RenderingContext.CurrentOrNull;

        public ISitecoreItem CurrentItem
        {
            get
            {
                if (_contextItem == null)
                {
                    return null;
                }

                return new SitecoreItem(_contextItem);
            }
        }

        public ISitecoreDatabase Database
        {
            get
            {
                if (_contextDatabase == null)
                {
                    return null;
                }

                return new SitecoreDatabase(_contextDatabase);
            }
        }

        public ISitecoreItem DatasourceItem
        {
            get
            {
                if (!CanUseRenderingContext
                    || _renderingContext.Rendering.Item == null)
                {
                    return null;
                }

                return new SitecoreItem(_renderingContext.Rendering.Item); ;
            }
        }

        public ISitecoreItem HomepageItem
        {
            get
            {
                if (_siteContext == null)
                {
                    return null;
                }

                return this.Database.GetItem(_siteContext.StartPath);
            }
        }

        public ISitecoreSiteContext Site
        {
            get
            {
                if (_siteContext == null)
                {
                    return null;
                }

                return new SitecoreSiteContext(_siteContext, this.Database);
            }
        }

        public RenderingParameters Parameters
        {
            get
            {
                if (!CanUseRenderingContext)
                {
                    return null;
                }

                return _renderingContext.Rendering.Parameters;
            }
        }

        public string Placeholder
        {
            get
            {
                if (!CanUseRenderingContext)
                {
                    return null;
                }

                return _renderingContext.Rendering.Placeholder;
            }
        }

        public bool IsExperienceEditor { get; } = Sitecore.Context.PageMode.IsExperienceEditor;

        public bool IsPreview { get; } = Sitecore.Context.PageMode.IsPreview;

        private bool CanUseRenderingContext
        {
            get
            {
                return _renderingContext != null
                    && _renderingContext.Rendering != null;
            }
        }
    }
}
