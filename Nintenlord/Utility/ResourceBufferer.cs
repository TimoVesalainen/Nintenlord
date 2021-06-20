using System;
using System.Collections.Concurrent;

namespace Nintenlord.Utility
{
    public sealed class ResourceBufferer<T>
    {
        readonly Func<T> resourceCreator;

        readonly ConcurrentQueue<T> createdResources;

        public ResourceBufferer(Func<T> resourceCreator)
        {
            this.resourceCreator = resourceCreator ?? throw new ArgumentNullException(nameof(resourceCreator));
            this.createdResources = new ConcurrentQueue<T>();
        }

        public Usage ClaimResource()
        {
            if (createdResources.TryDequeue(out var resource))
            {
                return new Usage(resource, this);
            }
            else
            {
                return new Usage(resourceCreator(), this);
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

        public readonly struct Usage : IDisposable
        {
            public readonly T Resourse;
            private readonly ResourceBufferer<T> owner;

            public Usage(T resourse, ResourceBufferer<T> owner)
            {
                Resourse = resourse;
                this.owner = owner;
            }

            public void Dispose()
            {
                owner.Release(Resourse);
            }
        }

        private void Release(T resourse)
        {
            createdResources.Enqueue(resourse);
        }
    }
}
