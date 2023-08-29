namespace serialization.Interfaces;

public interface IProvider<T>
{
    public T Provide();
    public ICollection<T> ProvideArrange(int count);
}