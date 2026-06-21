Console.WriteLine("Hello, World!");

IEnumerable<int> numbers = Enumerable.Range(1, 10);

var extendedNumbers = numbers.Append(11);

var originalCount = numbers.Count; // Output: 10


foreach (var number in extendedNumbers)
{
    Console.WriteLine(number);
}
