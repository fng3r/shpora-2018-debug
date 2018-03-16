using System.IO;

namespace TaskManager
{
    public static class MemoryStreamPool
    {
        private static readonly Pool<MemoryStream> instance
            = new Pool<MemoryStream>(
                new PoolConfig<MemoryStream>(
                    () => new MemoryStream(),
                    stream => stream.Reset()));

        public static Disposable<MemoryStream> Get(int capacity)
        {
            return instance.Acquire().WithCapacity(capacity);
        }
    }
}