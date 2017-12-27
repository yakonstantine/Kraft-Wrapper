using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Fake.Models;
using KraftWrapper.Interfaces;
using KraftWrapper.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web;

namespace KraftWrapper.Extensions.Tests
{
    [TestClass]
    public class SitecoreItemExtentionsTests
    {
        #region All field types values

        // Simple field
        private string _source = "source value";
        private string _textValue = "text value";
        private int _integerValue = 101;
        private bool _booleanValue = true;
        private double _doubleValue = 1.01;

        private DateTime _dateTimeValue = DateTime.Now;
        private HtmlString _htmlStringValue = new HtmlString("html string value");

        // ISitecoreTextField
        private string _textFieldText = "text field text";
        private HtmlString _textFieldHtmlString = new HtmlString("text Field Html String");

        // ISitecoreLinkField
        private string _linkFieldText = "link Field Text";
        private string _linkFieldValue = "link Field Value";
        private string _linkFieldFriendlyUrl = @"link/Field/Friendly/Url";
        private HtmlString _linkFieldHtmlString = new HtmlString("link Field Html String");

        // ISitecoreCheckboxField
        private bool _checkboxFieldIsChecked = true;
        private string _checkboxFieldValue = "checkbox Field Value";
        private HtmlString _checkboxFieldHtmlString = new HtmlString("checkbox Field Html String");

        // ISitecoreImageField
        private string _imageFieldUrl = "image Field Url";
        private string _imageFieldSrc = @"image/Field/Src";
        private string _imageFieldWidth = "101";
        private string _imageFieldAlt = "10";
        private string _imageFieldValue = "image Field Value";
        private HtmlString _imageFieldHtmlString = new HtmlString("image Field Html String");

        // ISitecoreInternalLinkField
        private string _internalLinkPath = @"internal/Link/Path";
        private string _internalLinkItemUrl = @"internal/Link/Item/Url";
        private string _internalLinkValue = "internal Link Value";
        private HtmlString _internalLinkHtmlString = new HtmlString("internal Link Html String");
        private string _internalLinkTargetItemName = "internal Link Target Item Name";

        #endregion

        [TestMethod]
        public void Mapping_ValidateAllFieldTipes()
        {
            var sitecoreItem = SetupItemWithAllFieldTipes();

            var result = sitecoreItem.As<AllFieldTipesFakeModel>();

            ValidateObjectWithAllFieldTipes(result);
        }

        [TestMethod]
        public void Mapping_ValidateTemplateWithChildren()
        {
            var textValue = "test field value";
            var children = new List<ISitecoreItem>
                    {
                        SetupItemWithAllFieldTipes(),
                        SetupItemWithAllFieldTipes(),
                        SetupItemWithAllFieldTipes()
                    };

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(TemplateWithChildren.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (ConvertToIDString(id))
                    {
                        case TemplateWithChildren.TextValueId:
                            {
                                return FieldMockHelper.MockSitecoreField(textValue, "");
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });
            sitecoreItem
                .Setup(x => x.GetChildren())
                .Returns(() =>
                {
                    return children;
                });

            var result = sitecoreItem.Object.As<TemplateWithChildrenFakeModel>();

            Assert.IsNotNull(result);
            Assert.AreEqual(textValue, result.TextValue);

            Assert.IsNotNull(result.Children);
            Assert.AreEqual(children.Count, result.Children.Count);

            foreach(var child in result.Children)
            {
                ValidateObjectWithAllFieldTipes(child);
            }
        }

        public ISitecoreItem SetupItemWithAllFieldTipes()
        {
            var targetItem = new Mock<ISitecoreItem>();
            targetItem
                .Setup(x => x.Name)
                .Returns(_internalLinkTargetItemName);

            var internalLinkTargetItem = targetItem.Object;

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(AllFieldTipesTemplate.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (ConvertToIDString(id))
                    {
                        case AllFieldTipesTemplate.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue, 
                                    _source);
                            }
                        case AllFieldTipesTemplate.IntegerValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _integerValue.ToString(), 
                                    _source);
                            }
                        case AllFieldTipesTemplate.BooleanValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _booleanValue ? "1" : "0", 
                                    _source);
                            }
                        case AllFieldTipesTemplate.DoubleValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _doubleValue.ToString(), 
                                    _source);
                            }
                        case AllFieldTipesTemplate.DateTimeValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _dateTimeValue.ToString(), 
                                    _source);
                            }
                        case AllFieldTipesTemplate.HtmlStringValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _htmlStringValue.ToString(),
                                    _source,
                                    _htmlStringValue);
                            }
                        case AllFieldTipesTemplate.TextFieldId:
                            {
                                var textField = CustomFieldMockHelper.MockTextField(
                                    _textFieldText,
                                    _textFieldHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _textFieldText, 
                                    _source, 
                                    textField);
                            }
                        case AllFieldTipesTemplate.LinkFieldId:
                            {
                                var linkField = CustomFieldMockHelper.MockLinkField(
                                    _linkFieldText,
                                    _linkFieldFriendlyUrl,
                                    _linkFieldValue,
                                    _linkFieldHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _linkFieldValue, 
                                    _source, 
                                    linkField);
                            }
                        case AllFieldTipesTemplate.CheckboxFieldId:
                            {
                                var checkboxField = CustomFieldMockHelper.MockCheckboxField(
                                    _checkboxFieldIsChecked,
                                    _checkboxFieldValue,
                                    _checkboxFieldHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _checkboxFieldValue, 
                                    _source, 
                                    checkboxField);
                            }
                        case AllFieldTipesTemplate.ImageFieldId:
                            {
                                var imageField = CustomFieldMockHelper.MockImageField(
                                    _imageFieldUrl,
                                    _imageFieldSrc,
                                    _imageFieldWidth,
                                    _imageFieldAlt,
                                    _imageFieldValue,
                                    _imageFieldHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _imageFieldValue, 
                                    _source, 
                                    imageField);
                            }
                        case AllFieldTipesTemplate.InternalLinkFieldId:
                            {
                                var internalLinkField = CustomFieldMockHelper.MockInternalLinkField(
                                    _internalLinkPath,
                                    _internalLinkItemUrl,
                                    internalLinkTargetItem,
                                    _internalLinkValue,
                                    _internalLinkHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _imageFieldValue, 
                                    _source, 
                                    internalLinkField);
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });

            return sitecoreItem.Object;
        }

        public void ValidateObjectWithAllFieldTipes(AllFieldTipesFakeModel result)
        {
            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);
            Assert.AreEqual(_integerValue, result.IntegerValue);
            Assert.AreEqual(_booleanValue, result.BooleanValue);
            Assert.AreEqual(_doubleValue, result.DoubleValue);
            Assert.AreEqual(_dateTimeValue.ToString(), result.DateTimeValue.ToString());
            ValidateHtmlString(_htmlStringValue, result.HtmlStringValue);

            Assert.IsNotNull(result.TextField);
            Assert.AreEqual(_textFieldText, result.TextField.Value);
            ValidateHtmlString(_textFieldHtmlString, result.TextField.RenderToHtml());

            Assert.IsNotNull(result.LinkField);
            Assert.AreEqual(_linkFieldText, result.LinkField.Text);
            Assert.AreEqual(_linkFieldValue, result.LinkField.Value);
            Assert.AreEqual(_linkFieldFriendlyUrl, result.LinkField.FriendlyUrl);
            ValidateHtmlString(_linkFieldHtmlString, result.LinkField.RenderToHtml());

            Assert.IsNotNull(result.CheckboxField);
            Assert.AreEqual(_checkboxFieldIsChecked, result.CheckboxField.IsChecked);
            Assert.AreEqual(_checkboxFieldValue, result.CheckboxField.Value);
            ValidateHtmlString(_checkboxFieldHtmlString, result.CheckboxField.RenderToHtml());

            Assert.IsNotNull(result.ImageField);
            Assert.AreEqual(_imageFieldUrl, result.ImageField.Url);
            Assert.AreEqual(_imageFieldSrc, result.ImageField.Src);
            Assert.AreEqual(_imageFieldWidth, result.ImageField.Width);
            Assert.AreEqual(_imageFieldAlt, result.ImageField.Alt);
            Assert.AreEqual(_imageFieldValue, result.ImageField.Value);
            ValidateHtmlString(_imageFieldHtmlString, result.ImageField.RenderToHtml());

            Assert.IsNotNull(result.InternalLinkField);
            Assert.AreEqual(_internalLinkPath, result.InternalLinkField.Path);
            Assert.AreEqual(_internalLinkItemUrl, result.InternalLinkField.ItemUrl);
            Assert.AreEqual(_internalLinkValue, result.InternalLinkField.Value);
            ValidateHtmlString(_internalLinkHtmlString, result.InternalLinkField.RenderToHtml());

            Assert.IsNotNull(result.InternalLinkField.TargetItem);
            Assert.AreEqual(_internalLinkTargetItemName, result.InternalLinkField.TargetItem.Name);
        }

        private static string ConvertToIDString(Guid source)
        {
            return $"{{{source.ToString().ToUpper()}}}";
        }

        private static void ValidateHtmlString(HtmlString expected, HtmlString actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
