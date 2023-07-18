﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Distributions
{
    public sealed class SelectDistribution<TIn, TOut> : IDistribution<TOut>
    {
        private readonly IDistribution<TIn> distribution;
        private readonly Func<TIn, TOut> selector;

        public SelectDistribution(IDistribution<TIn> distribution, Func<TIn, TOut> selector)
        {
            this.distribution = distribution ?? throw new ArgumentNullException(nameof(distribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return selector(distribution.Sample());
        }
    }
}
