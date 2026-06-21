# Collection inheritance — Sequence Diagram

This sequence diagram shows the high-level inheritance/implementation relationships between common .NET collection interfaces and some concrete types.

```mermaid
sequenceDiagram
    participant Consumer as Consumer
    participant IEnumerable as IEnumerable
    participant IEnumerableT as IEnumerable<T>
    participant ICollectionT as ICollection<T>
    participant IListT as IList<T>
    participant IReadOnlyCollection as IReadOnlyCollection<T>
    participant IReadOnlyList as IReadOnlyList<T>
    participant List as List<T>
    participant Array as T[]
    participant Dictionary as Dictionary<TKey, TValue>
    participant Queue as Queue<T>
    participant Stack as Stack<T>

    Note over IEnumerable,ICollectionT: Conceptual inheritance / implementation
    Consumer->>IEnumerable: consumes sequence (foreach / LINQ)
    IEnumerable->>IEnumerableT: generic variant
    IEnumerableT->>ICollectionT: extends (adds Count, CopyTo)
    ICollectionT->>IListT: extends (adds indexer, Insert, RemoveAt)
    IListT->>IReadOnlyList: read-only view
    ICollectionT->>IReadOnlyCollection: read-only collection
    IListT->>List: implemented by (List<T>)
    IListT->>Array: implemented by (T[])
    ICollectionT->>Queue: implemented by (Queue<T>)
    ICollectionT->>Stack: implemented by (Stack<T>)
    ICollectionT->>Dictionary: values implement ICollection<TValue>
    Note over List,Array: Concrete, indexed, materialized collections
```

Notes:
- This is a sequence-style representation to visualize relationships; consider a class diagram for strict inheritance layout.
