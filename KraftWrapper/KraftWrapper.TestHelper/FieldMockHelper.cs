using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using Moq;
using System;
using System.Web;

namespace KraftWrapper.TestHelper
{
    public static class FieldMockHelper
    {
        public static ISitecoreField MockSitecoreField(
            string value,
            string source,
            HtmlString htmlString = null)
        {
            var field = new Mock<ISitecoreField>();
            field
                .Setup(x => x.Value)
                .Returns(value);
            field
                .Setup(x => x.Source)
                .Returns(source);
            field
                .Setup(x => x.RenderToHtml(It.IsAny<string>()))
                .Returns(htmlString);

            return field.Object;
        }

        public static ISitecoreField MockSitecoreField<T>(
            string value,
            string source,
            T castedField,
            HtmlString htmlString = null)
            where T : class, ISitecoreBaseCustomField
        {
            var field = new Mock<ISitecoreField>();
            field
                .Setup(x => x.Value)
                .Returns(value);
            field
                .Setup(x => x.Source)
                .Returns(source);
            field
                .Setup(x => x.RenderToHtml(It.IsAny<string>()))
                .Returns(htmlString);
            field
                .Setup(x => x.CastToCustomField<T>())
                .Returns(castedField);
            field
                .Setup(x => x.CastToCustomField(It.IsAny<Type>()))
                .Returns((Type type) =>
                {
                    if (type == typeof(T))
                        return castedField;

                    return null;
                });

            return field.Object;
        }
    }
}
