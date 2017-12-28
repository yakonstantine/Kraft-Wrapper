using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace KraftWrapper.Core.Fields
{
    public class SitecoreImageField : SitecoreBaseCustomField<ImageField>, ISitecoreImageField
    {
        public SitecoreImageField(ImageField field) : base(field)
        {
        }

        public string Url
        {
            get
            {
                if (_field == null || _field.MediaItem == null)
                {
                    return string.Empty;
                }

                var mediaItem = new MediaItem(_field.MediaItem);

                return MediaManager.GetMediaUrl(mediaItem);
            }
        }

        public string Src
        {
            get
            {
                var imageUrl = string.Empty;

                if (_field == null || _field.MediaItem == null)
                {
                    return imageUrl;
                }

                var image = new MediaItem(_field.MediaItem);

                imageUrl = Sitecore.StringUtil
                    .EnsurePrefix('/', HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(image)));
                return imageUrl;
            }
        }

        public string Width
        {
            get
            {
                if (_field == null || _field.MediaItem == null)
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
                if (_field == null || _field.MediaItem == null)
                {
                    return "";
                }

                return _field.Alt;
            }
        }

    }
}
