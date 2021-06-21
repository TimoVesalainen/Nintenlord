using System;
using System.Collections.Concurrent;

namespace Nintenlord.Utility
{
    public sealed partial class ResourceBufferer<T>
    {
        readonly Func<T> resourceCreator;

        readonly ConcurrentQueue<T> createdResources;

        public ResourceBufferer(Func<T> resourceCreator)
        {
            this.resourceCreator = resourceCreator ?? throw new ArgumentNullException(nameof(resourceCreator));
            this.createdResources = new ConcurrentQueue<T>();
        }

        public Usage<T> ClaimResource()
        {
            if (createdResources.TryDequeue(out var resource))
            {
                return new Usage<T>(resource, Release);
            }
            else
            {
                return new Usage<T>(resourceCreator(), Release);
            }
        }

        public void Trim(int maxRemaining = 0)
        {
            while (createdResources.Count > maxRemaining)
            {
                if (createdResources.TryDequeue(out var resourse))
                {
                    if (resourse is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        private void Release(T resourse)
        {
            createdResources.Enqueue(resourse);
        }
    }
}
