using Nintenlord.Distributions.Discrete;
using System;
using System.Collections.Generic;

namespace Nintenlord.Distributions
{
    public sealed class MarkovChain<T> : IDistribution<IEnumerable<T>>
    {
        readonly IDistribution<T> startElement;
        readonly Func<T, IDistribution<T>> transition;

        public MarkovChain(IDistribution<T> startElement, Func<T, IDistribution<T>> transition)
        {
            this.startElement = startElement ?? throw new ArgumentNullException(nameof(startElement));
            this.transition = transition ?? throw new ArgumentNullException(nameof(transition));
        }

        public IEnumerable<T> Sample()
        {
            var current = startElement;
            while (current is not EmptyDistribution<T>)
            {
                var currentItem = current.Sample();
                yield return currentItem;
                current = transition(currentItem);
            }
        }
    }
}
