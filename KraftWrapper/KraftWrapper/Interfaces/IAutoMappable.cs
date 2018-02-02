using System;

namespace KraftWrapper.Interfaces
{
    public interface IAutoMappable
    {
        T As<T>() where T : class, ISitecoreTemplate, new();
        object As(Type type);
    }
}
