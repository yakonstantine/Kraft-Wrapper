using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using Moq;
using System.Web;

namespace KraftWrapper.TestHelper
{
    public static class CustomFieldMockHelper
    {
        public static ISitecoreTextField MockTextField(
            string value,
            HtmlString htmlString = null)
        {
            var customField = new Mock<ISitecoreTextField>();

            customField
               .Setup(x => x.Value)
               .Returns(value);

            customField
               .Setup(x => x.RenderToHtml(It.IsAny<string>()))
               .Returns(htmlString);

            return customField.Object;
        }

        public static ISitecoreLinkField MockLinkField(
            string text,
            string friendlyUrl,
            string value = "",
            HtmlString htmlString = null)
        {
            var customField = new Mock<ISitecoreLinkField>();

            customField
                .Setup(x => x.Text)
                .Returns(text);

            customField
                .Setup(x => x.FriendlyUrl)
                .Returns(friendlyUrl);

            customField
               .Setup(x => x.Value)
               .Returns(value);

            customField
               .Setup(x => x.RenderToHtml(It.IsAny<string>()))
               .Returns(htmlString);

            return customField.Object;
        }

        public static ISitecoreCheckboxField MockCheckboxField(
            bool isChecked,
            string value = "",
            HtmlString htmlString = null)
        {
            var customField = new Mock<ISitecoreCheckboxField>();

            customField
                .Setup(x => x.IsChecked)
                .Returns(isChecked);

            customField
               .Setup(x => x.Value)
               .Returns(value);

            customField
               .Setup(x => x.RenderToHtml(It.IsAny<string>()))
               .Returns(htmlString);

            return customField.Object;
        }

        public static ISitecoreImageField MockImageField(
            string url,
            string src,
            string width,
            string alt,
            string value = "",
            HtmlString htmlString = null)
        {
            var customField = new Mock<ISitecoreImageField>();

            customField
                .Setup(x => x.Url)
                .Returns(url);

            customField
                .Setup(x => x.Src)
                .Returns(src);

            customField
                .Setup(x => x.Width)
                .Returns(width);

            customField
                .Setup(x => x.Alt)
                .Returns(alt);

            customField
               .Setup(x => x.Value)
               .Returns(value);

            customField
               .Setup(x => x.RenderToHtml(It.IsAny<string>()))
               .Returns(htmlString);

            return customField.Object;
        }

        public static ISitecoreInternalLinkField MockInternalLinkField(
            string path,
            string itemUrl,
            ISitecoreItem targetItem,
            string value = "",
            HtmlString htmlString = null)
        {
            var customField = new Mock<ISitecoreInternalLinkField>();

            customField
                .Setup(x => x.Path)
                .Returns(path);

            customField
                .Setup(x => x.ItemUrl)
                .Returns(itemUrl);

            customField
                .Setup(x => x.TargetItem)
                .Returns(targetItem);

            customField
               .Setup(x => x.Value)
               .Returns(value);

            customField
               .Setup(x => x.RenderToHtml(It.IsAny<string>()))
               .Returns(htmlString);

            return customField.Object;
        }
    }
}
