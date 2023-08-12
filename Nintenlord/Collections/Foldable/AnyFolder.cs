using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections.Foldable
{
    public sealed class AnyFolder<T> : IFolder<T, bool, bool>
    {
        readonly Predicate<T> predicate;
        private Predicate<T> predicate1;

        public AnyFolder(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public bool Start => false;

        public bool Fold(bool state, T input)
        {
            return state || predicate(input);
        }

        public bool Transform(bool state)
        {
            return state;
        }
    }
}
