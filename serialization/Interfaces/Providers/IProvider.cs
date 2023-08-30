namespace serialization.Interfaces.Providers;

public interface IProvider<T>
{
    public T Provide();
    public ICollection<T> ProvideArrange(int count);
}