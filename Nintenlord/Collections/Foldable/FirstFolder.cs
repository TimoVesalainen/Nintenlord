﻿using Nintenlord.Utility;
using System;

namespace Nintenlord.Collections.Foldable
{
    public sealed class FirstFolder<T> : IFolder<T, Maybe<T>, Maybe<T>>
    {
        public readonly static FirstFolder<T> Instance = new(x => true);

        readonly Predicate<T> predicate;

        public FirstFolder(Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        public Maybe<T> Start => Maybe<T>.Nothing;

        public Maybe<T> Fold(Maybe<T> state, T input)
        {
            if (state.HasValue)
            {
                return state;
            }
            else if (predicate(input))
            {
                return input;
            }
            else
            {
                return state;
            }
        }

        public Maybe<T> Transform(Maybe<T> state)
        {
            return state;
        }
    }
}
