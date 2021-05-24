public class Factory<T> : IFactorable<T> where T : new()
{
    public T Create()
    {
        return new T();
    }
}
