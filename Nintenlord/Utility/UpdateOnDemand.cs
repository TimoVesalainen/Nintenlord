using System;

namespace Nintenlord.Utility
{
    public sealed class UpdateOnDemand<T> : IUpdatableValue<T>
    {
        private T item;
        private bool update;
        private readonly Func<T> valueFactory;
        private readonly object lockObject = new();

        public T Value
        {
            get
            {
                if (update)
                {
                    lock (lockObject)
                    {
                        if (update)
                        {
                            item = valueFactory();
                            update = false;
                        }
                    }
                }
                return item;
            }
        }

        public UpdateOnDemand(Func<T> valueFactory)
        {
            this.valueFactory = valueFactory;
            update = true;
            item = default;
        }

        public UpdateOnDemand(Func<T> valueFactory, T startValue)
        {
            this.valueFactory = valueFactory;
            update = false;
            item = startValue;
        }

        public void NeedsUpdate()
        {
            update = true;
        }

        public static implicit operator UpdateOnDemand<T>(T item)
        {
            return new UpdateOnDemand<T>(() => item, item);
        }
    }
}
