using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nintenlord.Utility;

namespace Nintenlord.Distributions.Combinatorics
{
    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<TIn> inDistribution;
        readonly Func<TIn, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<TIn> inDistribution, Func<TIn, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return selector(inDistribution.Sample());
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1)> inDistribution;
        readonly Func<TIn0, TIn1, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1)> inDistribution, Func<TIn0, TIn1, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TIn2, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1, TIn2)> inDistribution;
        readonly Func<TIn0, TIn1, TIn2, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1, TIn2)> inDistribution, Func<TIn0, TIn1, TIn2, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TIn2, TIn3, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1, TIn2, TIn3)> inDistribution;
        readonly Func<TIn0, TIn1, TIn2, TIn3, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1, TIn2, TIn3)> inDistribution, Func<TIn0, TIn1, TIn2, TIn3, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TIn2, TIn3, TIn4, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4)> inDistribution;
        readonly Func<TIn0, TIn1, TIn2, TIn3, TIn4, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4)> inDistribution, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5)> inDistribution;
        readonly Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5)> inDistribution, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }

    /// <summary>
    /// Helper for distributions with bijections between items.
    /// </summary>
    public sealed class FuncDistribution<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> : IDistribution<TOut>
    {
        readonly IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6)> inDistribution;
        readonly Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> selector;
        
        /// <param name="inDistribution">Original distributions</param>
        /// <param name="selector">Bijection between support of distributions</param>
        public FuncDistribution(IDistribution<(TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6)> inDistribution, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> selector)
        {
            this.inDistribution = inDistribution ?? throw new ArgumentNullException(nameof(inDistribution));
            this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public TOut Sample()
        {
            return inDistribution.Sample().Apply(selector);
        }
    }
}
