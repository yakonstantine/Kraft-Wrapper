# Kraft-Wrapper
Kraft Wrapper is a lightweight wrapper and an object mapper for Sitecore 8.

## About
Kraft Wrapper is a library that make Sitecore API injectable and unit testable. 
It is interface based, model of the Sitecore API, which is included auto mapping from an item to your model class.

A key feature of the implementation is a lightweight, you don't need to do something extra to use Kraft Wrapper, 
just download package from NuGet and do code.

You can download the package from <a href="https://www.nuget.org/packages/KraftWrapper/" target="_blank">NuGet</a>

## Example of usage
### Creation of a Model class
You should create model class is inherited from ISitecoreTemplate, and add attributes to class and to all properties.
If you want automatically map a sub items, just create a collection property. 
```c#
// You should add a SitecoreTemplate attribure with teamplate ID.
[SitecoreTemplate(TemplateId = "{00000000-0000-0000-0000-000000001000}")]
public class MyModel : ISitecoreTemplate
{
  [SitecoreField(FieldId = "{00000000-0000-0000-0000-000000001001}")]
  public string TextValue { get; set; }

  [SitecoreField(FieldId = "{00000000-0000-0000-0000-000000001002}")]
  public int IntegerValue { get; set; }

  [SitecoreField(FieldId = "{00000000-0000-0000-0000-000000001003}")]
  public ISitecoreLinkField LinkField { get; set; }

  // In a child class should be added all attributes   
  public IList<ChildModel> Children { get; set; }
}
```
### Get a datasource item and map to a target model
You can get an item using ISitecoreContext, dafault implementation of the interface you can find in the KraftWrapper.Core project.
After that you can cast an ISitecoreItem instance to you model class using an extension method. 
```c#
var sitecoreContext = new SitecoreContext();
var myModel = sitecoreContext.DatasourceItem.As<MyModel>();
```
