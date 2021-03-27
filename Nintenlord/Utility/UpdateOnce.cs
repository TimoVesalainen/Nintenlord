namespace Nintenlord.Utility
{
    public struct UpdateOnce<T>
    {
        private bool updated;
        private T currentValue;

        public T CurrentValue
        {
            get => currentValue;
            set
            {
                if (!updated)
                {
                    updated = true;
                    currentValue = value;
                }
            }
        }

        public UpdateOnce(T startValue)
        {
            currentValue = startValue;
            updated = false;
        }

        public void SetAsCanBeUpdated()
        {
            updated = true;
        }
    }
}
