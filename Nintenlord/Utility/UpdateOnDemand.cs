﻿using System;

namespace Nintenlord.Utility
{
    public sealed class UpdateOnDemand<T> : IUpdatableValue<T>
    {
        T item;
        bool update;
        readonly Func<T> valueFactory;
        object lockObject = new object();

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
            item = default(T);
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
