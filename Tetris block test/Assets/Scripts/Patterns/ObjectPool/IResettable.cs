namespace ObjectPool
{
    public interface IResettable
    {
        void Reset();
        void PoolInit();
    }
}