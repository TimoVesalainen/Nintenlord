namespace Nintenlord.Utility
{
    public struct UpdateOnce<T>
    {
        bool updated;
        private T currentValue;

        public T CurrentValue
        {
            get { return currentValue; }
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
