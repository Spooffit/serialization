namespace serialization.Interfaces;

public interface IFactory<T>
{
    public T Produce();
    public ICollection<T> ProduceArrange(int count);
}