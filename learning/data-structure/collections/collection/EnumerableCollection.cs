
namespace collection;

public class EnumerableCollection
{
    public IEnumerable<int> sequence = Enumerable.Range(1,10);

    public IEnumerable<int> numbers = [1,2,3,4];

    public IEnumerable<int> emptyList = Enumerable.Empty<int>();

    public void TestEnumerables()
    {
      Console.WriteLine($"Count of variable sequence : {sequence.Count()}");  
    }

}


