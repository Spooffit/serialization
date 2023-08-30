using serialization.Interfaces.Providers;

namespace serialization.Common.Providers;

public class TransportIdProvider : ITransportIdProvider
{
    private static int _ids = 1;
    public int Provide()
    {
        return ++_ids;
    }

    public ICollection<int> ProvideArrange(int count)
    {
        var ids = new List<int>();
        for (int i = 0; i < count; i++)
        {
            ids.Add(Provide());
        }
        return ids;
    }
}