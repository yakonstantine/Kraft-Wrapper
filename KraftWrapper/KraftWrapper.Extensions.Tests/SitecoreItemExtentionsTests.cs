using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Fake.Models;
using KraftWrapper.Interfaces;
using KraftWrapper.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraftWrapper.Extensions.Tests
{
    [TestClass]
    public class SitecoreItemExtentionsTests
    {
        #region All field types values

        // Simple field
        private const string _source = "source value";
        private const string _textValue = "text value";
        private const int _integerValue = 101;
        private const bool _booleanValue = true;
        private const double _doubleValue = 1.01;

        private DateTime _dateTimeValue = DateTime.Now;
        private HtmlString _htmlStringValue = new HtmlString("html string value");

        // ISitecoreTextField
        private const string _textFieldText = "text field text";
        private HtmlString _textFieldHtmlString = new HtmlString("text Field Html String");

        // ISitecoreLinkField
        private const string _linkFieldText = "link Field Text";
        private const string _linkFieldValue = "link Field Value";
        private const string _linkFieldFriendlyUrl = @"link/Field/Friendly/Url";
        private HtmlString _linkFieldHtmlString = new HtmlString("link Field Html String");

        // ISitecoreCheckboxField
        private const bool _checkboxFieldIsChecked = true;
        private const string _checkboxFieldValue = "checkbox Field Value";
        private HtmlString _checkboxFieldHtmlString = new HtmlString("checkbox Field Html String");

        // ISitecoreImageField
        private const string _imageFieldUrl = "image Field Url";
        private const string _imageFieldSrc = @"image/Field/Src";
        private const string _imageFieldWidth = "101";
        private const string _imageFieldAlt = "10";
        private const string _imageFieldValue = "image Field Value";
        private HtmlString _imageFieldHtmlString = new HtmlString("image Field Html String");

        // ISitecoreInternalLinkField
        private const string _internalLinkPath = @"internal/Link/Path";
        private const string _internalLinkItemUrl = @"internal/Link/Item/Url";
        private const string _internalLinkValue = "internal Link Value";
        private HtmlString _internalLinkHtmlString = new HtmlString("internal Link Html String");
        private const string _internalLinkTargetItemName = "internal Link Target Item Name";

        #endregion

        [TestMethod]
        public void Mapping_ValidateModelWithAllFieldTypes()
        {
            var sitecoreItem = CreateItemWithAllFieldTypes();

            var result = sitecoreItem.As<FakeModelWithAllFieldTypes>();

            ValidateObjectWithAllFieldTypes(result);
        }

        [TestMethod]
        public void Mapping_ValidateTemplateWithChildList()
        {
            var children = new List<ISitecoreItem>
                    {
                        CreateItemWithAllFieldTypes(),
                        CreateItemWithAllFieldTypes()
                    };

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithChildList.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithChildList.TextValueId:
                            {
                                return FieldMockHelper.MockSitecoreField(_textValue, "");
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

            var result = sitecoreItem.Object.As<FakeModelWithOneChildList>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);

            Assert.IsNotNull(result.Children);
            Assert.AreEqual(children.Count, result.Children.Count());

            foreach (var child in result.Children)
            {
                ValidateObjectWithAllFieldTypes(child);
            }
        }

        [TestMethod]
        public void Mapping_ValidateTemplateWithTwoChildrenLists()
        {
            var children = new List<ISitecoreItem>
                    {
                        CreateItemWithAllFieldTypes(),
                        CreateItemWithAllFieldTypes(),
                        CreateItemWithTwoFields(),
                        CreateItemWithTwoFields(),
                        CreateItemWithTwoFields()
                    };

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithTwoChildrenLists.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithTwoChildrenLists.TextValueId:
                            {
                                return FieldMockHelper.MockSitecoreField(_textValue, "");
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

            var result = sitecoreItem.Object.As<FakeModelWithTwoChildrenLists>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);

            Assert.IsNotNull(result.ChildrenWithAllFields);
            Assert.AreEqual(2, result.ChildrenWithAllFields.Count());

            foreach (var child in result.ChildrenWithAllFields)
            {
                ValidateObjectWithAllFieldTypes(child);
            }

            Assert.IsNotNull(result.ChildrenWithTwoFields);
            Assert.AreEqual(3, result.ChildrenWithTwoFields.Count());

            foreach (var child in result.ChildrenWithTwoFields)
            {
                ValidateObjectWithTwoFields(child);
            }
        }

        [TestMethod]
        public void Mapping_ValidateTemplateWithTwoChildrenLists_SubItemsOfOnlyOneType()
        {
            var children = new List<ISitecoreItem>
                    {
                        CreateItemWithTwoFields(),
                        CreateItemWithTwoFields()
                    };

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithTwoChildrenLists.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithTwoChildrenLists.TextValueId:
                            {
                                return FieldMockHelper.MockSitecoreField(_textValue, "");
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

            var result = sitecoreItem.Object.As<FakeModelWithTwoChildrenLists>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);

            Assert.IsNotNull(result.ChildrenWithTwoFields);
            Assert.AreEqual(2, result.ChildrenWithTwoFields.Count());

            foreach (var child in result.ChildrenWithTwoFields)
            {
                ValidateObjectWithTwoFields(child);
            }

            Assert.IsNotNull(result.ChildrenWithAllFields);
            Assert.AreEqual(0, result.ChildrenWithAllFields.Count());
        }

        [TestMethod]
        public void Mapping_IfItemTemplateIdIsNotEqualTemplateIdFormModel()
        {
            var sitecoreItem = SetupItemWithAllFieldTypes();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid("{00000000-0000-0000-0000-111111111111}"));

            var result = sitecoreItem.Object.As<FakeModelWithAllFieldTypes>();

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Mapping_IfModelDoesNotHaveTemplateAttribute()
        {
            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid("{00000000-0000-0000-0000-111111111111}"));

            sitecoreItem.Object.As<FakeModelWithoutTempalteAttribute>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Mapping_IfModelDoesNotHaveFieldAttributeForAnyProperty()
        {
            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithoutFieldAttribute.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithoutFieldAttribute.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue,
                                    _source);
                            }
                        case IDsForModelWithoutFieldAttribute.IntegerValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _integerValue.ToString(),
                                    _source);
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });

            sitecoreItem.Object.As<FakeModelWithoutFieldAttribute>();
        }

        [TestMethod]
        public void Mapping_IfOneFieldDoesNotExistInItem()
        {
            var sitecoreItem = SetupItemWithTwoFields();
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithTwoFields.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue,
                                    _source);
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });

            var result = sitecoreItem.Object.As<FakeModelWithTwoFields>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);
            Assert.AreEqual(default(int), result.IntegerValue);
        }

        [TestMethod]
        public void Mapping_ValidateModelWithNamedAttributeParameters()
        {
            #region Setup ISitecoreItem

            var textField = FieldMockHelper.MockSitecoreField(_textValue, _source);
            var integerField = FieldMockHelper.MockSitecoreField(_integerValue.ToString(), _source);

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithNamedAttributeParameters.TemplateId));
            sitecoreItem
               .Setup(x => x.TemplateName)
               .Returns(IDsForModelWithNamedAttributeParameters.TemplateName);
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithNamedAttributeParameters.TextValueFieldId:
                            {
                                return textField;
                            }
                        case IDsForModelWithNamedAttributeParameters.IntegerValueFieldId:
                            {
                                return integerField;
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });
            sitecoreItem
               .Setup(x => x.GetField(It.IsAny<string>()))
               .Returns((string fieldName) =>
               {
                   switch (fieldName)
                   {
                       case IDsForModelWithNamedAttributeParameters.TextValueFieldName:
                           {
                               return textField;
                           }
                       case IDsForModelWithNamedAttributeParameters.IntegerValueFieldName:
                           {
                               return integerField;
                           }
                       default:
                           {
                               return null;
                           }
                   }
               });
            sitecoreItem
               .Setup(x => x.GetField(It.IsAny<int>()))
               .Returns((int fieldIndex) =>
               {
                   switch (fieldIndex)
                   {
                       case IDsForModelWithNamedAttributeParameters.TextValueFieldIndex:
                           {
                               return textField;
                           }
                       case IDsForModelWithNamedAttributeParameters.IntegerValueFieldIndex:
                           {
                               return integerField;
                           }
                       default:
                           {
                               return null;
                           }
                   }
               });

            #endregion

            var result = sitecoreItem.Object.As<FakeModelWithNamedAttributeParameters>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);
            Assert.AreEqual(_integerValue, result.IntegerValue);
        }

        [TestMethod]
        public void Mapping_ValidateModelChild()
        {
            #region Setup ISitecoreItem

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelChild.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithTwoFields.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue,
                                    _source);
                            }
                        case IDsForModelWithTwoFields.IntegerValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _integerValue.ToString(),
                                    _source);
                            }
                        case IDsForModelChild.BooleanValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _booleanValue.ToString(),
                                    _source);
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });

            #endregion

            var result = sitecoreItem.Object.As<FakeModelChild>();

            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);
            Assert.AreEqual(_integerValue, result.IntegerValue);
            Assert.AreEqual(_booleanValue, result.BooleanValue);
        }

        public Mock<ISitecoreItem> SetupItemWithAllFieldTypes()
        {
            var targetItem = new Mock<ISitecoreItem>();
            targetItem
                .Setup(x => x.Name)
                .Returns(_internalLinkTargetItemName);

            var internalLinkTargetItem = targetItem.Object;

            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithAllFieldTypes.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithAllFieldTypes.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue,
                                    _source);
                            }
                        case IDsForModelWithAllFieldTypes.IntegerValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _integerValue.ToString(),
                                    _source);
                            }
                        case IDsForModelWithAllFieldTypes.BooleanValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _booleanValue ? "1" : "0",
                                    _source);
                            }
                        case IDsForModelWithAllFieldTypes.DoubleValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _doubleValue.ToString(),
                                    _source);
                            }
                        case IDsForModelWithAllFieldTypes.DateTimeValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _dateTimeValue.ToString(),
                                    _source);
                            }
                        case IDsForModelWithAllFieldTypes.HtmlStringValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _htmlStringValue.ToString(),
                                    _source,
                                    _htmlStringValue);
                            }
                        case IDsForModelWithAllFieldTypes.TextFieldId:
                            {
                                var textField = CustomFieldMockHelper.MockTextField(
                                    _textFieldText,
                                    _textFieldHtmlString);

                                return FieldMockHelper.MockSitecoreField(
                                    _textFieldText,
                                    _source,
                                    textField);
                            }
                        case IDsForModelWithAllFieldTypes.LinkFieldId:
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
                        case IDsForModelWithAllFieldTypes.CheckboxFieldId:
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
                        case IDsForModelWithAllFieldTypes.ImageFieldId:
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
                        case IDsForModelWithAllFieldTypes.InternalLinkFieldId:
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

            return sitecoreItem;
        }

        public ISitecoreItem CreateItemWithAllFieldTypes()
        {
            return SetupItemWithAllFieldTypes().Object;
        }

        public Mock<ISitecoreItem> SetupItemWithTwoFields()
        {
            var sitecoreItem = new Mock<ISitecoreItem>();
            sitecoreItem
                .Setup(x => x.TemplateId)
                .Returns(new Guid(IDsForModelWithTwoFields.TemplateId));
            sitecoreItem
                .Setup(x => x.GetField(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    switch (id.ToIDString())
                    {
                        case IDsForModelWithTwoFields.TextValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _textValue,
                                    _source);
                            }
                        case IDsForModelWithTwoFields.IntegerValueFieldId:
                            {
                                return FieldMockHelper.MockSitecoreField(
                                    _integerValue.ToString(),
                                    _source);
                            }
                        default:
                            {
                                return null;
                            }
                    }
                });

            return sitecoreItem;
        }

        public ISitecoreItem CreateItemWithTwoFields()
        {
            return SetupItemWithTwoFields().Object;
        }

        public void ValidateObjectWithAllFieldTypes(FakeModelWithAllFieldTypes result)
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

        public void ValidateObjectWithTwoFields(FakeModelWithTwoFields result)
        {
            Assert.IsNotNull(result);
            Assert.AreEqual(_textValue, result.TextValue);
            Assert.AreEqual(_integerValue, result.IntegerValue);
        }

        private static void ValidateHtmlString(HtmlString expected, HtmlString actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
