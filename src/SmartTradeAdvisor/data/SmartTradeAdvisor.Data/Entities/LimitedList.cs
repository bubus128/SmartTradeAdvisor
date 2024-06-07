namespace SmartTradeAdvisor.Data.Entities;

/// <summary>
/// Represents a list with a limited number of elements. When a new element is added and the limit is exceeded, the oldest element is automatically removed.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="LimitedList{T}"/> class with the specified limit.
/// </remarks>
/// <param name="limit">The maximum number of elements allowed in the list.</param>
public class LimitedList<T>(int limit) : List<T>
{
    private readonly int _limit = limit;

    /// <summary>
    /// Adds an object to the end of the <see cref="LimitedList{T}"/>. If the list exceeds its limit, the oldest element is removed.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="LimitedList{T}"/>.</param>
    public new void Add(T item)
    {
        base.Add(item);
        if (Count > _limit)
        {
            RemoveAt(0); // Removes the oldest element
        }
    }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the range of valid indexes for the list.</exception>
    public new T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            return base[index];
        }
        set
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            base[index] = value;
        }
    }
}
