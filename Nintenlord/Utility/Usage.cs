using System;

namespace Nintenlord.Utility
{
    public readonly struct Usage<T> : IDisposable
    {
        public readonly T Resourse;
        private readonly Action<T> release;

        public Usage(T resourse, Action<T> release)
        {
            Resourse = resourse;
            this.release = release;
        }

        public void Dispose()
        {
            release(Resourse);
        }

        public static implicit operator Usage<T>(T item)
        {
            return new Usage<T>(item, _ => { });
        }
    }
}
