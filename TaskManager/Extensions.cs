using System.IO;

namespace TaskManager
{
    public static class Extensions
    {
        public static void Reset(this Stream stream)
        {
            stream.SetLength(0);
        }

        public static Disposable<MemoryStream> WithCapacity(this Disposable<MemoryStream> ms, int capacity)
        {
            ms.Value.Capacity = capacity;
            return ms;
        }
    }
}