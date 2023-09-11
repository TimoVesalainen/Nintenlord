namespace Nintenlord.Distributions.Continous
{
    public interface IContinousDistribution<T> : IDistribution<T>
    {
        double Density(T item);
    }

    public static class ContinousDistributionHelpers
    {
    }
}
