// -----------------------------------------------------------------------
// <copyright file="UpdateOnDemand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Utility
{
    public interface IUpdatableValue<T>
    {
        T Value { get; }

        void NeedsUpdate();
    }
}