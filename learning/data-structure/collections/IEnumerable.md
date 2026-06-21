# .NET Generic Collection Hierarchy

This document summarizes common collection interfaces and concrete types in .NET, their purpose, and short examples.

## 1. `IEnumerable<T>`

- **Namespace:** `System.Collections.Generic`
- **Purpose:** Provides iteration over a sequence (supports `foreach` and LINQ).
- **Key member:** `GetEnumerator()`

Example:

```csharp
IEnumerable<int> numbers = new List<int> { 1, 2, 3 };
foreach (var n in numbers)
     Console.WriteLine(n);
```

## 2. `ICollection<T>`

- **Namespace:** `System.Collections.Generic`
- **Extends:** `IEnumerable<T>`
- **Adds:** `Count`, `Add(T)`, `Remove(T)`, `Clear()`, `Contains(T)`, `CopyTo(T[] array, int arrayIndex)`

Example:

```csharp
ICollection<int> numbers = new List<int>();
numbers.Add(1);
numbers.Add(2);
Console.WriteLine(numbers.Count); // 2
```

## 3. `IList<T>`

- **Namespace:** `System.Collections.Generic`
- **Extends:** `ICollection<T>`
- **Adds:** indexer `T this[int index] { get; set; }`, `IndexOf`, `Insert`, `RemoveAt`

Example:

```csharp
IList<string> names = new List<string> { "Alice", "Bob" };
names[0] = "Charlie";
names.Insert(1, "David");
Console.WriteLine(names[1]); // David
```

## 4. `IReadOnlyCollection<T>`

- **Namespace:** `System.Collections.Generic`
- **Extends:** `IEnumerable<T>`
- **Adds:** `Count` (read-only view)

Example:

```csharp
IReadOnlyCollection<int> readOnlyNumbers = new List<int> { 1, 2, 3 };
Console.WriteLine(readOnlyNumbers.Count); // 3
// readOnlyNumbers.Add(4); // Not allowed
```

## 5. `IReadOnlyList<T>`

- **Namespace:** `System.Collections.Generic`
- **Extends:** `IReadOnlyCollection<T>`
- **Adds:** indexer `T this[int index] { get; }` (read-only)

Example:

```csharp
IReadOnlyList<string> readOnlyNames = new List<string> { "Alice", "Bob" };
Console.WriteLine(readOnlyNames[0]); // Alice
```

## 6. `List<T>`

- **Namespace:** `System.Collections.Generic`
- **Type:** class
- **Implements:** `IList<T>`, `ICollection<T>`, `IEnumerable<T>`
- **Extra:** `Capacity`, `AddRange()`, `BinarySearch()`, `Sort()`, `Reverse()`, `TrimExcess()`

Example:

```csharp
List<int> numbers = new List<int> { 1, 2, 3 };
numbers.Add(4);
numbers.Sort();
Console.WriteLine(numbers[0]); // 1
```

## 7. `LinkedList<T>`

- **Namespace:** `System.Collections.Generic`
- **Type:** class
- **Implements:** `ICollection<T>`, `IEnumerable<T>`
- **Extra:** `AddFirst()`, `AddLast()`, `RemoveFirst()`, `RemoveLast()`, `LinkedListNode<T>`

Example:

```csharp
LinkedList<int> ll = new LinkedList<int>();
ll.AddLast(1);
ll.AddFirst(0);
ll.Remove(1);
```

## 8. `HashSet<T>`

- **Namespace:** `System.Collections.Generic`
- **Type:** class
- **Implements:** `ICollection<T>`, `IEnumerable<T>`
- **Extra:** `UnionWith()`, `IntersectWith()`, `ExceptWith()`, `IsSubsetOf()` (set operations)

Example:

```csharp
HashSet<int> set = new HashSet<int> { 1, 2, 3 };
set.Add(3); // ignored
Console.WriteLine(set.Contains(2)); // true
```

## 9. `Dictionary<TKey, TValue>`

- **Namespace:** `System.Collections.Generic`
- **Type:** class
- **Implements:** `ICollection<KeyValuePair<TKey, TValue>>`, `IEnumerable<KeyValuePair<TKey, TValue>>`
- **Extra:** `TryGetValue()`, `ContainsKey()`, indexer `dict[key]`, `Keys`, `Values`

Example:

```csharp
Dictionary<string, int> dict = new Dictionary<string, int>();
dict["Alice"] = 25;
Console.WriteLine(dict.ContainsKey("Alice")); // true
Console.WriteLine(dict["Alice"]); // 25
```

## 10. `SortedList<TKey, TValue>` & `SortedDictionary<TKey, TValue>`

- **Purpose:** Key-value collections that maintain keys in sorted order.

Example:

```csharp
var sd = new SortedDictionary<int, string>();
sd[2] = "B";
sd[1] = "A";
foreach (var kvp in sd)
     Console.WriteLine(kvp.Key); // 1, 2
```

## 11. `Queue<T>`

- **Namespace:** `System.Collections.Generic`
- **Purpose:** FIFO collection (`Enqueue`, `Dequeue`, `Peek`)

Example:

```csharp
Queue<int> q = new Queue<int>();
q.Enqueue(1);
q.Enqueue(2);
Console.WriteLine(q.Dequeue()); // 1
```

## 12. `Stack<T>`

- **Namespace:** `System.Collections.Generic`
- **Purpose:** LIFO collection (`Push`, `Pop`, `Peek`)

Example:

```csharp
Stack<int> stack = new Stack<int>();
stack.Push(1);
stack.Push(2);
Console.WriteLine(stack.Pop()); // 2
```

### Quick Hierarchy Summary (Interface → Common Concrete)

```
IEnumerable<T>
 └─ ICollection<T>
       ├─ IList<T> → List<T>
       ├─ IReadOnlyCollection<T>
       │     └─ IReadOnlyList<T>
       ├─ LinkedList<T>
       ├─ HashSet<T>
       ├─ Queue<T>
       ├─ Stack<T>
       └─ Dictionary<TKey, TValue>
             ├─ SortedDictionary<TKey, TValue>
             └─ SortedList<TKey, TValue>
```

---

Notes
- Interfaces define capabilities (iterate, count, modify, index).
- Concrete classes provide implementations optimized for different use cases (array-backed, linked, hash-based, key-value, queue, stack).
