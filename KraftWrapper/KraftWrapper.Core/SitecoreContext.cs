using KraftWrapper.Interfaces;
using Sitecore.Mvc.Presentation;

namespace KraftWrapper.Core
{
    public class SitecoreContext : ISitecoreContext
    {
        private readonly RenderingContext _renderingContext = RenderingContext.CurrentOrNull;

        public ISitecoreItem CurrentItem
        {
            get
            {
                if (Sitecore.Context.Item == null)
                {
                    return null;
                }

                return new SitecoreItem(Sitecore.Context.Item);
            }
        }

        public ISitecoreDatabase Database
        {
            get
            {
                if (Sitecore.Context.Database == null)
                {
                    return null;
                }

                return new SitecoreDatabase(Sitecore.Context.Database);
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
                if (Sitecore.Context.Site == null)
                {
                    return null;
                }

                return this.Database.GetItem(Sitecore.Context.Site.StartPath);
            }
        }

        public ISitecoreSiteContext Site
        {
            get
            {
                if (Sitecore.Context.Site == null)
                {
                    return null;
                }

                return new SitecoreSiteContext(Sitecore.Context.Site, this.Database);
            }
        }

        public bool IsPreview
        {
            get
            {
                return Sitecore.Context.PageMode.IsPreview;
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

        public bool IsExperienceEditor
        {
            get
            {
                return Sitecore.Context.PageMode.IsExperienceEditor;
            }
        }

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
