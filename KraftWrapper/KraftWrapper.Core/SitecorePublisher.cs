using KraftWrapper.Core.Helpers;
using KraftWrapper.Interfaces;
using KraftWrapper.Options;
using System.Linq;

namespace KraftWrapper.Core
{
    public class SitecorePublisher : ISitecorePublisher
    {
        public void Publish(PublishOptions options)
        {
            var defSourceDb = DefaultImplementationFactory.GetDefaultImplementation(options.SourceDatabase);
            var defTargetDb = DefaultImplementationFactory.GetDefaultImplementation(options.TargetDatabase);
            var defItem = DefaultImplementationFactory.GetDefaultImplementation(options.RootItem);
            var defLanguage = DefaultImplementationFactory.GetDefaultImplementation(options.Language);
            var sitecorePublishMode = ConvertToSitecorePublishMode(options.Mode);

            Sitecore.Publishing.PublishOptions publishOptions = null;

            if (string.IsNullOrEmpty(options.UserName))
            {
                publishOptions = new Sitecore.Publishing.PublishOptions(
                defSourceDb.RawValue,
                defTargetDb.RawValue,
                sitecorePublishMode,
                defLanguage.RawValue,
                options.PublishDate);
            }
            else
            {
                publishOptions = new Sitecore.Publishing.PublishOptions(
                defSourceDb.RawValue,
                defTargetDb.RawValue,
                sitecorePublishMode,
                defLanguage.RawValue,
                options.PublishDate,
                options.PublishingTargets?.ToList(),
                options.UserName);
            }

            publishOptions.Deep = options.Deep;
            publishOptions.PublishRelatedItems = options.PublishRelatedItems;
            publishOptions.RepublishAll = options.RepublishAll;
            publishOptions.RootItem = defItem.RawValue;

            (new Sitecore.Publishing.Publisher(publishOptions)).Publish();
        }

        private static Sitecore.Publishing.PublishMode ConvertToSitecorePublishMode(PublishMode publishMode)
        {
            switch (publishMode)
            {
                case PublishMode.Unknown:
                    {
                        return Sitecore.Publishing.PublishMode.Unknown;
                    };
                case PublishMode.Full:
                    {
                        return Sitecore.Publishing.PublishMode.Full;
                    };
                case PublishMode.Incremental:
                    {
                        return Sitecore.Publishing.PublishMode.Incremental;
                    };
                case PublishMode.SingleItem:
                    {
                        return Sitecore.Publishing.PublishMode.SingleItem;
                    };
                case PublishMode.Smart:
                    {
                        return Sitecore.Publishing.PublishMode.Smart;
                    };
                default:
                    {
                        return Sitecore.Publishing.PublishMode.Unknown;
                    };
            }
        }
    }


}
