namespace ObjectPool
{
    public interface IResettable
    {
        void Reset();
        void PoolInit(); // calls when you instantiate an object inside the pool
    }
}