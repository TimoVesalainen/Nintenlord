using System;
using System.Threading.Tasks;

namespace Nintenlord.Utility
{
    public sealed class AsyncUpdaetableValue<T> : IUpdatableValue<T>
    {
        private readonly Func<Task<T>> updateFunc;
        private Task settingTask;

        public T Value { get; private set; }

        public AsyncUpdaetableValue(Func<Task<T>> updateFunc, T startValue = default)
        {
            this.updateFunc = updateFunc;
            this.Value = startValue;
        }

        public void NeedsUpdate()
        {
            async Task Update()
            {
                var newValue = await updateFunc();

                Value = newValue;
                settingTask = null;
            }

            if (settingTask == null)
            {
                settingTask = Update();
            }
        }

        public static implicit operator AsyncUpdaetableValue<T>(T item)
        {
            return new AsyncUpdaetableValue<T>(() => Task.FromResult(item), item);
        }
    }
}