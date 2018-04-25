using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;

namespace KraftWrapper.Core.Fields
{
    class SitecoreImageField : SitecoreBaseCustomField<ImageField>, ISitecoreImageField
    {
        private readonly MediaItem _mediaItem;

        public SitecoreImageField(ImageField field) : base(field)
        {
            if (_field != null && _field.MediaItem != null)
            {
                _mediaItem = new MediaItem(_field.MediaItem);
            }
        }

        public Guid Id
        {
            get
            {
                if (_mediaItem == null)
                {
                    return default(Guid);
                }

                return _mediaItem.ID.Guid;
            }
        }

        public string Url
        {
            get
            {
                if (_mediaItem == null)
                {
                    return string.Empty;
                }

                return MediaManager.GetMediaUrl(_mediaItem);
            }
        }

        public string Src
        {
            get
            {
                if (_mediaItem == null)
                {
                    return string.Empty;
                }

                return Sitecore.StringUtil
                     .EnsurePrefix('/', HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(_mediaItem)));
            }
        }

        public string Width
        {
            get
            {
                if (_mediaItem == null)
                {
                    return "0";
                }

                return _field.Width;
            }
        }

        public string Alt
        {
            get
            {
                if (_mediaItem == null)
                {
                    return "";
                }

                return _field.Alt;
            }
        }

    }
}
