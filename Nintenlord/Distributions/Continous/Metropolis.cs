using System;
using System.Collections.Generic;

namespace Nintenlord.Distributions.Continous
{
    public sealed class Metropolis<T> : IContinousDistribution<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly Func<T, double> target;
        public static Metropolis<T> Distribution(Func<T, double> target, IDistribution<T> initial, Func<T, IDistribution<T>> proposal)
        {
            IDistribution<T> transition(T d)
            {
                T candidate = proposal(d).Sample();
                return new CoinFlip<T>(candidate, d, target(candidate) / target(d));
            }

            var markov = new MarkovChain<T>(initial, transition);
            return new Metropolis<T>(target, markov.Sample().GetEnumerator());
        }
        private Metropolis(
          Func<T, double> target,
          IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
            this.target = target;
        }

        public double Density(T item)
        {
            return target(item);
        }

        public T Sample()
        {
            this.enumerator.MoveNext();
            return this.enumerator.Current;
        }
    }
}
