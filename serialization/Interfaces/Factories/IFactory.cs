namespace serialization.Interfaces.Factories;

public interface IFactory<T>
{
    public T Produce();
    public ICollection<T> ProduceArrange(int count);
}