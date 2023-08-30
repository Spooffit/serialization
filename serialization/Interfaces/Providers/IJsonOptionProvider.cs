using serialization.Interfaces.Factories;

namespace serialization.Interfaces.Providers;

public interface IJsonOptionProvider : IJsonReadOptionFactory, IJsonWriteOptionFactory
{
    
}