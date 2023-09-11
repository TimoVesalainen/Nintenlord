using System.Linq;

namespace Nintenlord.Distributions.Continous
{
    public sealed class IrwinHallDistribution : IDistribution<double>
    {
        public int Count { get; }

        public IrwinHallDistribution(int count)
        {

            Count = count;
        }

        public double Sample()
        {
            return StandardUniformDoubleDistribution.Distribution.Samples().Take(Count).Sum();
        }
    }
}
