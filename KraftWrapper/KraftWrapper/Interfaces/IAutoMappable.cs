using System;

namespace KraftWrapper.Interfaces
{
    public interface IAutoMappable
    {
        T As<T>() where T : class, IModel, new();
        object As(Type type);
    }
}
