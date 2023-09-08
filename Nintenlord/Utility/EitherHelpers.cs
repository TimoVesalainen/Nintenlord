using System;
using System.Reactive.Linq;

namespace Nintenlord.Utility
{
	public static partial class EitherHelpers
	{
		public static Either<TOut, T1> Select0<T0, T1, TOut>(this Either<T0, T1> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1>.From0(selector(item)), Either<TOut, T1>.From1);
		}

		public static Either<T0, TOut> Select1<T0, T1, TOut>(this Either<T0, T1> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut>.From0, item => Either<T0, TOut>.From1(selector(item)));
		}

		public static Either<TOut, T1> Select<T0, T1, TOut>(this Either<T0, T1> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1> MultiSelect<T0, T1, TOut0, TOut1>(this Either<T0, T1> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1)
		{
			return either.Apply(x => Either<TOut0, TOut1>.From0(func0(x)), x => Either<TOut0, TOut1>.From1(func1(x)));
		}

		public static Either<T0, T1> SelectMany0<T0, T1, TIn>(this Either<TIn, T1> either, Func<TIn, Either<T0, T1>> selector)
		{
			return either.Apply(selector, Either<T0, T1>.From1);
		}

		public static Either<T0, T1> SelectMany1<T0, T1, TIn>(this Either<T0, TIn> either, Func<TIn, Either<T0, T1>> selector)
		{
			return either.Apply(Either<T0, T1>.From0, selector);
		}

		public static Either<T0, T1> SelectMany<T0, T1, TIn>(this Either<TIn, T1> either, Func<TIn, Either<T0, T1>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1> MultiSelectMany<T0, T1, TOut0, TOut1>(this Either<T0, T1> either, Func<T0, Either<TOut0, TOut1>> func0, Func<T1, Either<TOut0, TOut1>> func1)
		{
			return either.Apply(func0, func1);
		}

		public static Either<T0, T1> Where0<T0, T1>(this Either<T0, T1> either, Predicate<T0> predicate, T1 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1>.From1);
		}

		public static Either<T0, T1> Where<T0, T1>(this Either<T0, T1> either, Predicate<T0> predicate, T1 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1>, TOut> Combine<T0, T1, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1)
		{
			return either => either.Apply(func0, func1);
		}

		public static IObservable<Either<T0, T1>> MergeEither<T0, T1>(this IObservable<T0> observable0, IObservable<T1> observable1)
		{
			return observable0.Select(Either<T0, T1>.From0).Merge(observable1.Select(Either<T0, T1>.From1));
		}

#region Associate for 2
#endregion Associate for 2

		//TODO: Commutative operations
		public static Either<TOut, T1, T2> Select0<T0, T1, T2, TOut>(this Either<T0, T1, T2> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2>.From0(selector(item)), Either<TOut, T1, T2>.From1, Either<TOut, T1, T2>.From2);
		}

		public static Either<T0, TOut, T2> Select1<T0, T1, T2, TOut>(this Either<T0, T1, T2> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2>.From0, item => Either<T0, TOut, T2>.From1(selector(item)), Either<T0, TOut, T2>.From2);
		}

		public static Either<T0, T1, TOut> Select2<T0, T1, T2, TOut>(this Either<T0, T1, T2> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut>.From0, Either<T0, T1, TOut>.From1, item => Either<T0, T1, TOut>.From2(selector(item)));
		}

		public static Either<TOut, T1, T2> Select<T0, T1, T2, TOut>(this Either<T0, T1, T2> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2> MultiSelect<T0, T1, T2, TOut0, TOut1, TOut2>(this Either<T0, T1, T2> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2>.From2(func2(x)));
		}

		public static Either<T0, T1, T2> SelectMany0<T0, T1, T2, TIn>(this Either<TIn, T1, T2> either, Func<TIn, Either<T0, T1, T2>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2>.From1, Either<T0, T1, T2>.From2);
		}

		public static Either<T0, T1, T2> SelectMany1<T0, T1, T2, TIn>(this Either<T0, TIn, T2> either, Func<TIn, Either<T0, T1, T2>> selector)
		{
			return either.Apply(Either<T0, T1, T2>.From0, selector, Either<T0, T1, T2>.From2);
		}

		public static Either<T0, T1, T2> SelectMany2<T0, T1, T2, TIn>(this Either<T0, T1, TIn> either, Func<TIn, Either<T0, T1, T2>> selector)
		{
			return either.Apply(Either<T0, T1, T2>.From0, Either<T0, T1, T2>.From1, selector);
		}

		public static Either<T0, T1, T2> SelectMany<T0, T1, T2, TIn>(this Either<TIn, T1, T2> either, Func<TIn, Either<T0, T1, T2>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2> MultiSelectMany<T0, T1, T2, TOut0, TOut1, TOut2>(this Either<T0, T1, T2> either, Func<T0, Either<TOut0, TOut1, TOut2>> func0, Func<T1, Either<TOut0, TOut1, TOut2>> func1, Func<T2, Either<TOut0, TOut1, TOut2>> func2)
		{
			return either.Apply(func0, func1, func2);
		}

		public static Either<T0, T1, T2> Where0<T0, T1, T2>(this Either<T0, T1, T2> either, Predicate<T0> predicate, T2 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2>.From1, Either<T0, T1, T2>.From2);
		}

		public static Either<T0, T1, T2> Where1<T0, T1, T2>(this Either<T0, T1, T2> either, Predicate<T1> predicate, T2 error = default)
		{
			return either.Apply(Either<T0, T1, T2>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2>.From2);
		}

		public static Either<T0, T1, T2> Where<T0, T1, T2>(this Either<T0, T1, T2> either, Predicate<T0> predicate, T2 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2>, TOut> Combine<T0, T1, T2, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2)
		{
			return either => either.Apply(func0, func1, func2);
		}

		public static IObservable<Either<T0, T1, T2>> MergeEither<T0, T1, T2>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1).Select(x => (Either<T0, T1, T2>)x).Merge(observable2.Select(Either<T0, T1, T2>.From2));
		}

#region Associate for 3
		public static Either<T0, T1, T2> Associate2And1<T0, T1, T2>(this Either<Either<T0, T1>, T2> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2> UnAssociate2And1<T0, T1, T2>(this Either<T0, T1, T2> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x);
		}
		public static Either<T0, T1, T2> Associate1And2<T0, T1, T2>(this Either<T0, Either<T1, T2>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>> UnAssociate1And2<T0, T1, T2>(this Either<T0, T1, T2> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x);
		}
#endregion Associate for 3

		//TODO: Commutative operations
		public static Either<TOut, T1, T2, T3> Select0<T0, T1, T2, T3, TOut>(this Either<T0, T1, T2, T3> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2, T3>.From0(selector(item)), Either<TOut, T1, T2, T3>.From1, Either<TOut, T1, T2, T3>.From2, Either<TOut, T1, T2, T3>.From3);
		}

		public static Either<T0, TOut, T2, T3> Select1<T0, T1, T2, T3, TOut>(this Either<T0, T1, T2, T3> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2, T3>.From0, item => Either<T0, TOut, T2, T3>.From1(selector(item)), Either<T0, TOut, T2, T3>.From2, Either<T0, TOut, T2, T3>.From3);
		}

		public static Either<T0, T1, TOut, T3> Select2<T0, T1, T2, T3, TOut>(this Either<T0, T1, T2, T3> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut, T3>.From0, Either<T0, T1, TOut, T3>.From1, item => Either<T0, T1, TOut, T3>.From2(selector(item)), Either<T0, T1, TOut, T3>.From3);
		}

		public static Either<T0, T1, T2, TOut> Select3<T0, T1, T2, T3, TOut>(this Either<T0, T1, T2, T3> either, Func<T3, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, TOut>.From0, Either<T0, T1, T2, TOut>.From1, Either<T0, T1, T2, TOut>.From2, item => Either<T0, T1, T2, TOut>.From3(selector(item)));
		}

		public static Either<TOut, T1, T2, T3> Select<T0, T1, T2, T3, TOut>(this Either<T0, T1, T2, T3> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3> MultiSelect<T0, T1, T2, T3, TOut0, TOut1, TOut2, TOut3>(this Either<T0, T1, T2, T3> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2, Func<T3, TOut3> func3)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2, TOut3>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2, TOut3>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2, TOut3>.From2(func2(x)), x => Either<TOut0, TOut1, TOut2, TOut3>.From3(func3(x)));
		}

		public static Either<T0, T1, T2, T3> SelectMany0<T0, T1, T2, T3, TIn>(this Either<TIn, T1, T2, T3> either, Func<TIn, Either<T0, T1, T2, T3>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2, T3>.From1, Either<T0, T1, T2, T3>.From2, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> SelectMany1<T0, T1, T2, T3, TIn>(this Either<T0, TIn, T2, T3> either, Func<TIn, Either<T0, T1, T2, T3>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3>.From0, selector, Either<T0, T1, T2, T3>.From2, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> SelectMany2<T0, T1, T2, T3, TIn>(this Either<T0, T1, TIn, T3> either, Func<TIn, Either<T0, T1, T2, T3>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3>.From0, Either<T0, T1, T2, T3>.From1, selector, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> SelectMany3<T0, T1, T2, T3, TIn>(this Either<T0, T1, T2, TIn> either, Func<TIn, Either<T0, T1, T2, T3>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3>.From0, Either<T0, T1, T2, T3>.From1, Either<T0, T1, T2, T3>.From2, selector);
		}

		public static Either<T0, T1, T2, T3> SelectMany<T0, T1, T2, T3, TIn>(this Either<TIn, T1, T2, T3> either, Func<TIn, Either<T0, T1, T2, T3>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3> MultiSelectMany<T0, T1, T2, T3, TOut0, TOut1, TOut2, TOut3>(this Either<T0, T1, T2, T3> either, Func<T0, Either<TOut0, TOut1, TOut2, TOut3>> func0, Func<T1, Either<TOut0, TOut1, TOut2, TOut3>> func1, Func<T2, Either<TOut0, TOut1, TOut2, TOut3>> func2, Func<T3, Either<TOut0, TOut1, TOut2, TOut3>> func3)
		{
			return either.Apply(func0, func1, func2, func3);
		}

		public static Either<T0, T1, T2, T3> Where0<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> either, Predicate<T0> predicate, T3 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2, T3>.From1, Either<T0, T1, T2, T3>.From2, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> Where1<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> either, Predicate<T1> predicate, T3 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2, T3>.From2, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> Where2<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> either, Predicate<T2> predicate, T3 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3>.From0, Either<T0, T1, T2, T3>.From1, x => predicate(x) ? x : error, Either<T0, T1, T2, T3>.From3);
		}

		public static Either<T0, T1, T2, T3> Where<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> either, Predicate<T0> predicate, T3 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2, T3>, TOut> Combine<T0, T1, T2, T3, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3)
		{
			return either => either.Apply(func0, func1, func2, func3);
		}

		public static IObservable<Either<T0, T1, T2, T3>> MergeEither<T0, T1, T2, T3>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1, observable2).Select(x => (Either<T0, T1, T2, T3>)x).Merge(observable3.Select(Either<T0, T1, T2, T3>.From3));
		}

#region Associate for 4
		public static Either<T0, T1, T2, T3> Associate3And1<T0, T1, T2, T3>(this Either<Either<T0, T1, T2>, T3> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3> UnAssociate3And1<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x);
		}
		public static Either<T0, T1, T2, T3> Associate2And2<T0, T1, T2, T3>(this Either<Either<T0, T1>, Either<T2, T3>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>> UnAssociate2And2<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x);
		}
		public static Either<T0, T1, T2, T3> Associate2And1And1<T0, T1, T2, T3>(this Either<Either<T0, T1>, T2, T3> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3> UnAssociate2And1And1<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x);
		}
		public static Either<T0, T1, T2, T3> Associate1And3<T0, T1, T2, T3>(this Either<T0, Either<T1, T2, T3>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>> UnAssociate1And3<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x);
		}
		public static Either<T0, T1, T2, T3> Associate1And2And1<T0, T1, T2, T3>(this Either<T0, Either<T1, T2>, T3> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3> UnAssociate1And2And1<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x);
		}
		public static Either<T0, T1, T2, T3> Associate1And1And2<T0, T1, T2, T3>(this Either<T0, T1, Either<T2, T3>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3>>(x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>> UnAssociate1And1And2<T0, T1, T2, T3>(this Either<T0, T1, T2, T3> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x);
		}
#endregion Associate for 4

		//TODO: Commutative operations
		public static Either<TOut, T1, T2, T3, T4> Select0<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2, T3, T4>.From0(selector(item)), Either<TOut, T1, T2, T3, T4>.From1, Either<TOut, T1, T2, T3, T4>.From2, Either<TOut, T1, T2, T3, T4>.From3, Either<TOut, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, TOut, T2, T3, T4> Select1<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2, T3, T4>.From0, item => Either<T0, TOut, T2, T3, T4>.From1(selector(item)), Either<T0, TOut, T2, T3, T4>.From2, Either<T0, TOut, T2, T3, T4>.From3, Either<T0, TOut, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, TOut, T3, T4> Select2<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut, T3, T4>.From0, Either<T0, T1, TOut, T3, T4>.From1, item => Either<T0, T1, TOut, T3, T4>.From2(selector(item)), Either<T0, T1, TOut, T3, T4>.From3, Either<T0, T1, TOut, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, TOut, T4> Select3<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T3, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, TOut, T4>.From0, Either<T0, T1, T2, TOut, T4>.From1, Either<T0, T1, T2, TOut, T4>.From2, item => Either<T0, T1, T2, TOut, T4>.From3(selector(item)), Either<T0, T1, T2, TOut, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, TOut> Select4<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T4, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, TOut>.From0, Either<T0, T1, T2, T3, TOut>.From1, Either<T0, T1, T2, T3, TOut>.From2, Either<T0, T1, T2, T3, TOut>.From3, item => Either<T0, T1, T2, T3, TOut>.From4(selector(item)));
		}

		public static Either<TOut, T1, T2, T3, T4> Select<T0, T1, T2, T3, T4, TOut>(this Either<T0, T1, T2, T3, T4> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4> MultiSelect<T0, T1, T2, T3, T4, TOut0, TOut1, TOut2, TOut3, TOut4>(this Either<T0, T1, T2, T3, T4> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2, Func<T3, TOut3> func3, Func<T4, TOut4> func4)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2, TOut3, TOut4>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4>.From2(func2(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4>.From3(func3(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4>.From4(func4(x)));
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany0<T0, T1, T2, T3, T4, TIn>(this Either<TIn, T1, T2, T3, T4> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2, T3, T4>.From1, Either<T0, T1, T2, T3, T4>.From2, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany1<T0, T1, T2, T3, T4, TIn>(this Either<T0, TIn, T2, T3, T4> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, selector, Either<T0, T1, T2, T3, T4>.From2, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany2<T0, T1, T2, T3, T4, TIn>(this Either<T0, T1, TIn, T3, T4> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, Either<T0, T1, T2, T3, T4>.From1, selector, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany3<T0, T1, T2, T3, T4, TIn>(this Either<T0, T1, T2, TIn, T4> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, Either<T0, T1, T2, T3, T4>.From1, Either<T0, T1, T2, T3, T4>.From2, selector, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany4<T0, T1, T2, T3, T4, TIn>(this Either<T0, T1, T2, T3, TIn> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, Either<T0, T1, T2, T3, T4>.From1, Either<T0, T1, T2, T3, T4>.From2, Either<T0, T1, T2, T3, T4>.From3, selector);
		}

		public static Either<T0, T1, T2, T3, T4> SelectMany<T0, T1, T2, T3, T4, TIn>(this Either<TIn, T1, T2, T3, T4> either, Func<TIn, Either<T0, T1, T2, T3, T4>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4> MultiSelectMany<T0, T1, T2, T3, T4, TOut0, TOut1, TOut2, TOut3, TOut4>(this Either<T0, T1, T2, T3, T4> either, Func<T0, Either<TOut0, TOut1, TOut2, TOut3, TOut4>> func0, Func<T1, Either<TOut0, TOut1, TOut2, TOut3, TOut4>> func1, Func<T2, Either<TOut0, TOut1, TOut2, TOut3, TOut4>> func2, Func<T3, Either<TOut0, TOut1, TOut2, TOut3, TOut4>> func3, Func<T4, Either<TOut0, TOut1, TOut2, TOut3, TOut4>> func4)
		{
			return either.Apply(func0, func1, func2, func3, func4);
		}

		public static Either<T0, T1, T2, T3, T4> Where0<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> either, Predicate<T0> predicate, T4 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4>.From1, Either<T0, T1, T2, T3, T4>.From2, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> Where1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> either, Predicate<T1> predicate, T4 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4>.From2, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> Where2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> either, Predicate<T2> predicate, T4 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, Either<T0, T1, T2, T3, T4>.From1, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4>.From3, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> Where3<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> either, Predicate<T3> predicate, T4 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4>.From0, Either<T0, T1, T2, T3, T4>.From1, Either<T0, T1, T2, T3, T4>.From2, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4>.From4);
		}

		public static Either<T0, T1, T2, T3, T4> Where<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> either, Predicate<T0> predicate, T4 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2, T3, T4>, TOut> Combine<T0, T1, T2, T3, T4, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4)
		{
			return either => either.Apply(func0, func1, func2, func3, func4);
		}

		public static IObservable<Either<T0, T1, T2, T3, T4>> MergeEither<T0, T1, T2, T3, T4>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1, observable2, observable3).Select(x => (Either<T0, T1, T2, T3, T4>)x).Merge(observable4.Select(Either<T0, T1, T2, T3, T4>.From4));
		}

#region Associate for 5
		public static Either<T0, T1, T2, T3, T4> Associate4And1<T0, T1, T2, T3, T4>(this Either<Either<T0, T1, T2, T3>, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4> UnAssociate4And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate3And2<T0, T1, T2, T3, T4>(this Either<Either<T0, T1, T2>, Either<T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>> UnAssociate3And2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate3And1And1<T0, T1, T2, T3, T4>(this Either<Either<T0, T1, T2>, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4> UnAssociate3And1And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate2And3<T0, T1, T2, T3, T4>(this Either<Either<T0, T1>, Either<T2, T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>> UnAssociate2And3<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate2And2And1<T0, T1, T2, T3, T4>(this Either<Either<T0, T1>, Either<T2, T3>, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4> UnAssociate2And2And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate2And1And2<T0, T1, T2, T3, T4>(this Either<Either<T0, T1>, T2, Either<T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>> UnAssociate2And1And2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate2And1And1And1<T0, T1, T2, T3, T4>(this Either<Either<T0, T1>, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4> UnAssociate2And1And1And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And4<T0, T1, T2, T3, T4>(this Either<T0, Either<T1, T2, T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>> UnAssociate1And4<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And3And1<T0, T1, T2, T3, T4>(this Either<T0, Either<T1, T2, T3>, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4> UnAssociate1And3And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And2And2<T0, T1, T2, T3, T4>(this Either<T0, Either<T1, T2>, Either<T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>> UnAssociate1And2And2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And2And1And1<T0, T1, T2, T3, T4>(this Either<T0, Either<T1, T2>, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4> UnAssociate1And2And1And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And1And3<T0, T1, T2, T3, T4>(this Either<T0, T1, Either<T2, T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>> UnAssociate1And1And3<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And1And2And1<T0, T1, T2, T3, T4>(this Either<T0, T1, Either<T2, T3>, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4> UnAssociate1And1And2And1<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x);
		}
		public static Either<T0, T1, T2, T3, T4> Associate1And1And1And2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, Either<T3, T4>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>> UnAssociate1And1And1And2<T0, T1, T2, T3, T4>(this Either<T0, T1, T2, T3, T4> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x);
		}
#endregion Associate for 5

		//TODO: Commutative operations
		public static Either<TOut, T1, T2, T3, T4, T5> Select0<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2, T3, T4, T5>.From0(selector(item)), Either<TOut, T1, T2, T3, T4, T5>.From1, Either<TOut, T1, T2, T3, T4, T5>.From2, Either<TOut, T1, T2, T3, T4, T5>.From3, Either<TOut, T1, T2, T3, T4, T5>.From4, Either<TOut, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, TOut, T2, T3, T4, T5> Select1<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2, T3, T4, T5>.From0, item => Either<T0, TOut, T2, T3, T4, T5>.From1(selector(item)), Either<T0, TOut, T2, T3, T4, T5>.From2, Either<T0, TOut, T2, T3, T4, T5>.From3, Either<T0, TOut, T2, T3, T4, T5>.From4, Either<T0, TOut, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, TOut, T3, T4, T5> Select2<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut, T3, T4, T5>.From0, Either<T0, T1, TOut, T3, T4, T5>.From1, item => Either<T0, T1, TOut, T3, T4, T5>.From2(selector(item)), Either<T0, T1, TOut, T3, T4, T5>.From3, Either<T0, T1, TOut, T3, T4, T5>.From4, Either<T0, T1, TOut, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, TOut, T4, T5> Select3<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T3, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, TOut, T4, T5>.From0, Either<T0, T1, T2, TOut, T4, T5>.From1, Either<T0, T1, T2, TOut, T4, T5>.From2, item => Either<T0, T1, T2, TOut, T4, T5>.From3(selector(item)), Either<T0, T1, T2, TOut, T4, T5>.From4, Either<T0, T1, T2, TOut, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, TOut, T5> Select4<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T4, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, TOut, T5>.From0, Either<T0, T1, T2, T3, TOut, T5>.From1, Either<T0, T1, T2, T3, TOut, T5>.From2, Either<T0, T1, T2, T3, TOut, T5>.From3, item => Either<T0, T1, T2, T3, TOut, T5>.From4(selector(item)), Either<T0, T1, T2, T3, TOut, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, TOut> Select5<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T5, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, TOut>.From0, Either<T0, T1, T2, T3, T4, TOut>.From1, Either<T0, T1, T2, T3, T4, TOut>.From2, Either<T0, T1, T2, T3, T4, TOut>.From3, Either<T0, T1, T2, T3, T4, TOut>.From4, item => Either<T0, T1, T2, T3, T4, TOut>.From5(selector(item)));
		}

		public static Either<TOut, T1, T2, T3, T4, T5> Select<T0, T1, T2, T3, T4, T5, TOut>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5> MultiSelect<T0, T1, T2, T3, T4, T5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2, Func<T3, TOut3> func3, Func<T4, TOut4> func4, Func<T5, TOut5> func5)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From2(func2(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From3(func3(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From4(func4(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>.From5(func5(x)));
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany0<T0, T1, T2, T3, T4, T5, TIn>(this Either<TIn, T1, T2, T3, T4, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany1<T0, T1, T2, T3, T4, T5, TIn>(this Either<T0, TIn, T2, T3, T4, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, selector, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany2<T0, T1, T2, T3, T4, T5, TIn>(this Either<T0, T1, TIn, T3, T4, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, selector, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany3<T0, T1, T2, T3, T4, T5, TIn>(this Either<T0, T1, T2, TIn, T4, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, selector, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany4<T0, T1, T2, T3, T4, T5, TIn>(this Either<T0, T1, T2, T3, TIn, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, selector, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany5<T0, T1, T2, T3, T4, T5, TIn>(this Either<T0, T1, T2, T3, T4, TIn> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, selector);
		}

		public static Either<T0, T1, T2, T3, T4, T5> SelectMany<T0, T1, T2, T3, T4, T5, TIn>(this Either<TIn, T1, T2, T3, T4, T5> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5> MultiSelectMany<T0, T1, T2, T3, T4, T5, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>(this Either<T0, T1, T2, T3, T4, T5> either, Func<T0, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func0, Func<T1, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func1, Func<T2, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func2, Func<T3, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func3, Func<T4, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func4, Func<T5, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5>> func5)
		{
			return either.Apply(func0, func1, func2, func3, func4, func5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where0<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T0> predicate, T5 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T1> predicate, T5 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T2> predicate, T5 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5>.From3, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T3> predicate, T5 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5>.From4, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where4<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T4> predicate, T5 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5>.From0, Either<T0, T1, T2, T3, T4, T5>.From1, Either<T0, T1, T2, T3, T4, T5>.From2, Either<T0, T1, T2, T3, T4, T5>.From3, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5>.From5);
		}

		public static Either<T0, T1, T2, T3, T4, T5> Where<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> either, Predicate<T0> predicate, T5 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2, T3, T4, T5>, TOut> Combine<T0, T1, T2, T3, T4, T5, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5);
		}

		public static IObservable<Either<T0, T1, T2, T3, T4, T5>> MergeEither<T0, T1, T2, T3, T4, T5>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1, observable2, observable3, observable4).Select(x => (Either<T0, T1, T2, T3, T4, T5>)x).Merge(observable5.Select(Either<T0, T1, T2, T3, T4, T5>.From5));
		}

#region Associate for 6
		public static Either<T0, T1, T2, T3, T4, T5> Associate5And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2, T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, T5> UnAssociate5And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, T5>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate4And2<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5>> UnAssociate4And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate4And1And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2, T3>, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, T5> UnAssociate4And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, T5>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate3And3<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5>> UnAssociate3And3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate3And2And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2>, Either<T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, T5> UnAssociate3And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, T5>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate3And1And2<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5>> UnAssociate3And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate3And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1, T2>, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, T5> UnAssociate3And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, T5>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And4<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5>> UnAssociate2And4<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And3And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, Either<T2, T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, T5> UnAssociate2And3And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, T5>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And2And2<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>> UnAssociate2And2And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And2And1And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, Either<T2, T3>, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, T5> UnAssociate2And2And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, T5>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And1And3<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5>> UnAssociate2And1And3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And1And2And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, T2, Either<T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, T5> UnAssociate2And1And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, T5>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And1And1And2<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5>> UnAssociate2And1And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate2And1And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<Either<T0, T1>, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, T5> UnAssociate2And1And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, T5>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And5<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2, T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5>> UnAssociate1And5<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5>>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And4And1<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2, T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, T5> UnAssociate1And4And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, T5>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And3And2<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5>> UnAssociate1And3And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And3And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2, T3>, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, T5> UnAssociate1And3And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, T5>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And2And3<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5>> UnAssociate1And2And3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And2And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2>, Either<T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, T5> UnAssociate1And2And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, T5>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And2And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5>> UnAssociate1And2And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And2And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, Either<T1, T2>, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, T5> UnAssociate1And2And1And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, T5>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And4<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, Either<T2, T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5>> UnAssociate1And1And4<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And3And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, Either<T2, T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, T5> UnAssociate1And1And3And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, T5>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And2And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5>> UnAssociate1And1And2And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And2And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, Either<T2, T3>, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, T5> UnAssociate1And1And2And1And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, T5>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And1And3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, Either<T3, T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5>> UnAssociate1And1And1And3<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And1And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, Either<T3, T4>, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, T5> UnAssociate1And1And1And2And1<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, T5>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5> Associate1And1And1And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, Either<T4, T5>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5>> UnAssociate1And1And1And1And2<T0, T1, T2, T3, T4, T5>(this Either<T0, T1, T2, T3, T4, T5> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x);
		}
#endregion Associate for 6

		//TODO: Commutative operations
		public static Either<TOut, T1, T2, T3, T4, T5, T6> Select0<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2, T3, T4, T5, T6>.From0(selector(item)), Either<TOut, T1, T2, T3, T4, T5, T6>.From1, Either<TOut, T1, T2, T3, T4, T5, T6>.From2, Either<TOut, T1, T2, T3, T4, T5, T6>.From3, Either<TOut, T1, T2, T3, T4, T5, T6>.From4, Either<TOut, T1, T2, T3, T4, T5, T6>.From5, Either<TOut, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, TOut, T2, T3, T4, T5, T6> Select1<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2, T3, T4, T5, T6>.From0, item => Either<T0, TOut, T2, T3, T4, T5, T6>.From1(selector(item)), Either<T0, TOut, T2, T3, T4, T5, T6>.From2, Either<T0, TOut, T2, T3, T4, T5, T6>.From3, Either<T0, TOut, T2, T3, T4, T5, T6>.From4, Either<T0, TOut, T2, T3, T4, T5, T6>.From5, Either<T0, TOut, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, TOut, T3, T4, T5, T6> Select2<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut, T3, T4, T5, T6>.From0, Either<T0, T1, TOut, T3, T4, T5, T6>.From1, item => Either<T0, T1, TOut, T3, T4, T5, T6>.From2(selector(item)), Either<T0, T1, TOut, T3, T4, T5, T6>.From3, Either<T0, T1, TOut, T3, T4, T5, T6>.From4, Either<T0, T1, TOut, T3, T4, T5, T6>.From5, Either<T0, T1, TOut, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, TOut, T4, T5, T6> Select3<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T3, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, TOut, T4, T5, T6>.From0, Either<T0, T1, T2, TOut, T4, T5, T6>.From1, Either<T0, T1, T2, TOut, T4, T5, T6>.From2, item => Either<T0, T1, T2, TOut, T4, T5, T6>.From3(selector(item)), Either<T0, T1, T2, TOut, T4, T5, T6>.From4, Either<T0, T1, T2, TOut, T4, T5, T6>.From5, Either<T0, T1, T2, TOut, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, TOut, T5, T6> Select4<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T4, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, TOut, T5, T6>.From0, Either<T0, T1, T2, T3, TOut, T5, T6>.From1, Either<T0, T1, T2, T3, TOut, T5, T6>.From2, Either<T0, T1, T2, T3, TOut, T5, T6>.From3, item => Either<T0, T1, T2, T3, TOut, T5, T6>.From4(selector(item)), Either<T0, T1, T2, T3, TOut, T5, T6>.From5, Either<T0, T1, T2, T3, TOut, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, TOut, T6> Select5<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T5, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, TOut, T6>.From0, Either<T0, T1, T2, T3, T4, TOut, T6>.From1, Either<T0, T1, T2, T3, T4, TOut, T6>.From2, Either<T0, T1, T2, T3, T4, TOut, T6>.From3, Either<T0, T1, T2, T3, T4, TOut, T6>.From4, item => Either<T0, T1, T2, T3, T4, TOut, T6>.From5(selector(item)), Either<T0, T1, T2, T3, T4, TOut, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, TOut> Select6<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T6, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, TOut>.From0, Either<T0, T1, T2, T3, T4, T5, TOut>.From1, Either<T0, T1, T2, T3, T4, T5, TOut>.From2, Either<T0, T1, T2, T3, T4, T5, TOut>.From3, Either<T0, T1, T2, T3, T4, T5, TOut>.From4, Either<T0, T1, T2, T3, T4, T5, TOut>.From5, item => Either<T0, T1, T2, T3, T4, T5, TOut>.From6(selector(item)));
		}

		public static Either<TOut, T1, T2, T3, T4, T5, T6> Select<T0, T1, T2, T3, T4, T5, T6, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6> MultiSelect<T0, T1, T2, T3, T4, T5, T6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2, Func<T3, TOut3> func3, Func<T4, TOut4> func4, Func<T5, TOut5> func5, Func<T6, TOut6> func6)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From2(func2(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From3(func3(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From4(func4(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From5(func5(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>.From6(func6(x)));
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany0<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<TIn, T1, T2, T3, T4, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany1<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, TIn, T2, T3, T4, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, selector, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany2<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, T1, TIn, T3, T4, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, selector, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany3<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, T1, T2, TIn, T4, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, selector, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany4<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, T1, T2, T3, TIn, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, selector, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany5<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, T1, T2, T3, T4, TIn, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, selector, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany6<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<T0, T1, T2, T3, T4, T5, TIn> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, selector);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> SelectMany<T0, T1, T2, T3, T4, T5, T6, TIn>(this Either<TIn, T1, T2, T3, T4, T5, T6> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6> MultiSelectMany<T0, T1, T2, T3, T4, T5, T6, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Func<T0, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func0, Func<T1, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func1, Func<T2, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func2, Func<T3, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func3, Func<T4, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func4, Func<T5, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func5, Func<T6, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6>> func6)
		{
			return either.Apply(func0, func1, func2, func3, func4, func5, func6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where0<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T0> predicate, T6 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T1> predicate, T6 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T2> predicate, T6 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T3> predicate, T6 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From4, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T4> predicate, T6 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From5, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where5<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T5> predicate, T6 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6>.From0, Either<T0, T1, T2, T3, T4, T5, T6>.From1, Either<T0, T1, T2, T3, T4, T5, T6>.From2, Either<T0, T1, T2, T3, T4, T5, T6>.From3, Either<T0, T1, T2, T3, T4, T5, T6>.From4, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6>.From6);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6> Where<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> either, Predicate<T0> predicate, T6 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2, T3, T4, T5, T6>, TOut> Combine<T0, T1, T2, T3, T4, T5, T6, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5, Func<T6, TOut> func6)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5, func6);
		}

		public static IObservable<Either<T0, T1, T2, T3, T4, T5, T6>> MergeEither<T0, T1, T2, T3, T4, T5, T6>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5, IObservable<T6> observable6)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1, observable2, observable3, observable4, observable5).Select(x => (Either<T0, T1, T2, T3, T4, T5, T6>)x).Merge(observable6.Select(Either<T0, T1, T2, T3, T4, T5, T6>.From6));
		}

#region Associate for 7
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate6And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4, T5>, T6> UnAssociate6And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4, T5>, T6>>(x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate5And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>> UnAssociate5And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate5And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, T5, T6> UnAssociate5And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, T5, T6>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate4And3<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>> UnAssociate4And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate4And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6> UnAssociate4And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate4And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>> UnAssociate4And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate4And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2, T3>, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, T5, T6> UnAssociate4And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, T5, T6>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And4<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>> UnAssociate3And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6> UnAssociate3And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>> UnAssociate3And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6> UnAssociate3And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>> UnAssociate3And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6> UnAssociate3And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>> UnAssociate3And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate3And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1, T2>, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, T5, T6> UnAssociate3And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, T5, T6>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And5<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>> UnAssociate2And5<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And4And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6> UnAssociate2And4And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And3And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>> UnAssociate2And3And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And3And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6> UnAssociate2And3And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And2And3<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>> UnAssociate2And2And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And2And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6> UnAssociate2And2And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And2And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>> UnAssociate2And2And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And2And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6> UnAssociate2And2And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And4<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>> UnAssociate2And1And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6> UnAssociate2And1And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>> UnAssociate2And1And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6> UnAssociate2And1And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>> UnAssociate2And1And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6> UnAssociate2And1And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>> UnAssociate2And1And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate2And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<Either<T0, T1>, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, T5, T6> UnAssociate2And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, T5, T6>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And6<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5, T6>> UnAssociate1And6<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5, T6>>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And5And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5>, T6> UnAssociate1And5And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5>, T6>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And4And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>> UnAssociate1And4And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And4And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, T5, T6> UnAssociate1And4And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, T5, T6>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And3And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>> UnAssociate1And3And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And3And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6> UnAssociate1And3And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And3And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>> UnAssociate1And3And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And3And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2, T3>, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, T5, T6> UnAssociate1And3And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, T5, T6>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>> UnAssociate1And2And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6> UnAssociate1And2And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>> UnAssociate1And2And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6> UnAssociate1And2And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>> UnAssociate1And2And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6> UnAssociate1And2And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>> UnAssociate1And2And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, Either<T1, T2>, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, T5, T6> UnAssociate1And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, T5, T6>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And5<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5, T6>> UnAssociate1And1And5<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5, T6>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And4And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5>, T6> UnAssociate1And1And4And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5>, T6>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And3And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>> UnAssociate1And1And3And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And3And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, T5, T6> UnAssociate1And1And3And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, T5, T6>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And2And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>> UnAssociate1And1And2And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And2And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6> UnAssociate1And1And2And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And2And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>> UnAssociate1And1And2And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, Either<T2, T3>, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, T5, T6> UnAssociate1And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, T5, T6>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, Either<T3, T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5, T6>> UnAssociate1And1And1And4<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5, T6>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, Either<T3, T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5>, T6> UnAssociate1And1And1And3And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5>, T6>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>> UnAssociate1And1And1And2And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, Either<T3, T4>, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, T5, T6> UnAssociate1And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, T5, T6>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, Either<T4, T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5, T6>> UnAssociate1And1And1And1And3<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5, T6>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, Either<T4, T5>, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5>, T6> UnAssociate1And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5>, T6>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6> Associate1And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, Either<T5, T6>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, T4, Either<T5, T6>> UnAssociate1And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6>(this Either<T0, T1, T2, T3, T4, T5, T6> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, Either<T5, T6>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x);
		}
#endregion Associate for 7

		//TODO: Commutative operations
		public static Either<TOut, T1, T2, T3, T4, T5, T6, T7> Select0<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T0, TOut> selector)
		{
			return either.Apply(item => Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From0(selector(item)), Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From1, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From2, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From3, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From4, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From5, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From6, Either<TOut, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, TOut, T2, T3, T4, T5, T6, T7> Select1<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T1, TOut> selector)
		{
			return either.Apply(Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From0, item => Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From1(selector(item)), Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From2, Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From3, Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From4, Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From5, Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From6, Either<T0, TOut, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, TOut, T3, T4, T5, T6, T7> Select2<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T2, TOut> selector)
		{
			return either.Apply(Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From0, Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From1, item => Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From2(selector(item)), Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From3, Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From4, Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From5, Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From6, Either<T0, T1, TOut, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, TOut, T4, T5, T6, T7> Select3<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T3, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From0, Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From1, Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From2, item => Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From3(selector(item)), Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From4, Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From5, Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From6, Either<T0, T1, T2, TOut, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, TOut, T5, T6, T7> Select4<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T4, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From0, Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From1, Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From2, Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From3, item => Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From4(selector(item)), Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From5, Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From6, Either<T0, T1, T2, T3, TOut, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, TOut, T6, T7> Select5<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T5, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From0, Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From1, Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From2, Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From3, Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From4, item => Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From5(selector(item)), Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From6, Either<T0, T1, T2, T3, T4, TOut, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, TOut, T7> Select6<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T6, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From0, Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From1, Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From2, Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From3, Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From4, Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From5, item => Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From6(selector(item)), Either<T0, T1, T2, T3, T4, T5, TOut, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, TOut> Select7<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T7, TOut> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From0, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From1, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From2, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From3, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From4, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From5, Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From6, item => Either<T0, T1, T2, T3, T4, T5, T6, TOut>.From7(selector(item)));
		}

		public static Either<TOut, T1, T2, T3, T4, T5, T6, T7> Select<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T0, TOut> selector)
		{
			return either.Select0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7> MultiSelect<T0, T1, T2, T3, T4, T5, T6, T7, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T0, TOut0> func0, Func<T1, TOut1> func1, Func<T2, TOut2> func2, Func<T3, TOut3> func3, Func<T4, TOut4> func4, Func<T5, TOut5> func5, Func<T6, TOut6> func6, Func<T7, TOut7> func7)
		{
			return either.Apply(x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From0(func0(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From1(func1(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From2(func2(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From3(func3(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From4(func4(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From5(func5(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From6(func6(x)), x => Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>.From7(func7(x)));
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany0<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<TIn, T1, T2, T3, T4, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany1<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, TIn, T2, T3, T4, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany2<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, TIn, T3, T4, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany3<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, T2, TIn, T4, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany4<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, T2, T3, TIn, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany5<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, T2, T3, T4, TIn, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany6<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, T2, T3, T4, T5, TIn, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, selector, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany7<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<T0, T1, T2, T3, T4, T5, T6, TIn> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, selector);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> SelectMany<T0, T1, T2, T3, T4, T5, T6, T7, TIn>(this Either<TIn, T1, T2, T3, T4, T5, T6, T7> either, Func<TIn, Either<T0, T1, T2, T3, T4, T5, T6, T7>> selector)
		{
			return either.SelectMany0(selector);
		}
		
		public static Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7> MultiSelectMany<T0, T1, T2, T3, T4, T5, T6, T7, TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Func<T0, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func0, Func<T1, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func1, Func<T2, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func2, Func<T3, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func3, Func<T4, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func4, Func<T5, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func5, Func<T6, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func6, Func<T7, Either<TOut0, TOut1, TOut2, TOut3, TOut4, TOut5, TOut6, TOut7>> func7)
		{
			return either.Apply(func0, func1, func2, func3, func4, func5, func6, func7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where0<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T0> predicate, T7 error = default)
		{
			return either.Apply(x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T1> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T2> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T3> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T4> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T5> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From6, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where6<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T6> predicate, T7 error = default)
		{
			return either.Apply(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From0, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From1, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From2, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From3, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From4, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From5, x => predicate(x) ? x : error, Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7);
		}

		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Where<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> either, Predicate<T0> predicate, T7 error = default)
		{
			return Where0(either, predicate, error);
		}

		public static Func<Either<T0, T1, T2, T3, T4, T5, T6, T7>, TOut> Combine<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5, Func<T6, TOut> func6, Func<T7, TOut> func7)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5, func6, func7);
		}

		public static IObservable<Either<T0, T1, T2, T3, T4, T5, T6, T7>> MergeEither<T0, T1, T2, T3, T4, T5, T6, T7>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5, IObservable<T6> observable6, IObservable<T7> observable7)
		{
			// TODO: Reduce calls
			return observable0.MergeEither(observable1, observable2, observable3, observable4, observable5, observable6).Select(x => (Either<T0, T1, T2, T3, T4, T5, T6, T7>)x).Merge(observable7.Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7));
		}

#region Associate for 8
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate7And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4, T5, T6>, T7> UnAssociate7And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4, T5, T6>, T7>>(x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (Either<T0, T1, T2, T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate6And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4, T5>, Either<T6, T7>> UnAssociate6And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate6And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4, T5>, T6, T7> UnAssociate6And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4, T5>, T6, T7>>(x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (Either<T0, T1, T2, T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate5And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6, T7>> UnAssociate5And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6, T7>>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate5And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>, T7> UnAssociate5And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, Either<T5, T6>, T7>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate5And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, T5, Either<T6, T7>> UnAssociate5And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, T5, Either<T6, T7>>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate5And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3, T4>, T5, T6, T7> UnAssociate5And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3, T4>, T5, T6, T7>>(x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (Either<T0, T1, T2, T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6, T7>> UnAssociate4And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6, T7>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>, T7> UnAssociate4And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5, T6>, T7>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5>, Either<T6, T7>> UnAssociate4And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6, T7> UnAssociate4And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, Either<T4, T5>, T6, T7>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6, T7>> UnAssociate4And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6, T7>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>, T7> UnAssociate4And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, Either<T5, T6>, T7>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, T5, Either<T6, T7>> UnAssociate4And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, T5, Either<T6, T7>>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate4And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2, T3>, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2, T3>, T4, T5, T6, T7> UnAssociate4And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2, T3>, T4, T5, T6, T7>>(x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (Either<T0, T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6, T7>> UnAssociate3And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>, T7> UnAssociate3And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5, T6>, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5>, Either<T6, T7>> UnAssociate3And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6, T7> UnAssociate3And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4, T5>, T6, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6, T7>> UnAssociate3And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>, T7> UnAssociate3And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, Either<T5, T6>, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, T5, Either<T6, T7>> UnAssociate3And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, T5, Either<T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6, T7> UnAssociate3And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, Either<T3, T4>, T5, T6, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6, T7>> UnAssociate3And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>, T7> UnAssociate3And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5, T6>, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5>, Either<T6, T7>> UnAssociate3And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6, T7> UnAssociate3And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, Either<T4, T5>, T6, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6, T7>> UnAssociate3And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>, T7> UnAssociate3And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, Either<T5, T6>, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, T5, Either<T6, T7>> UnAssociate3And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, T5, Either<T6, T7>>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate3And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1, T2>, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1, T2>, T3, T4, T5, T6, T7> UnAssociate3And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1, T2>, T3, T4, T5, T6, T7>>(x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (Either<T0, T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And6<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6, T7>> UnAssociate2And6<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And5And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>, T7> UnAssociate2And5And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And4And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5>, Either<T6, T7>> UnAssociate2And4And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And4And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6, T7> UnAssociate2And4And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4, T5>, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And3And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6, T7>> UnAssociate2And3And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And3And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>, T7> UnAssociate2And3And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, Either<T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And3And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, T5, Either<T6, T7>> UnAssociate2And3And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, T5, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And3And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6, T7> UnAssociate2And3And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3, T4>, T5, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6, T7>> UnAssociate2And2And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>, T7> UnAssociate2And2And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>> UnAssociate2And2And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6, T7> UnAssociate2And2And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, Either<T4, T5>, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6, T7>> UnAssociate2And2And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>, T7> UnAssociate2And2And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, Either<T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, T5, Either<T6, T7>> UnAssociate2And2And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, T5, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6, T7> UnAssociate2And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, Either<T2, T3>, T4, T5, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6, T7>> UnAssociate2And1And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>, T7> UnAssociate2And1And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5>, Either<T6, T7>> UnAssociate2And1And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6, T7> UnAssociate2And1And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4, T5>, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6, T7>> UnAssociate2And1And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>, T7> UnAssociate2And1And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, Either<T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, T5, Either<T6, T7>> UnAssociate2And1And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, T5, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6, T7> UnAssociate2And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, Either<T3, T4>, T5, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6, T7>> UnAssociate2And1And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>, T7> UnAssociate2And1And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5>, Either<T6, T7>> UnAssociate2And1And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5>, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6, T7> UnAssociate2And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, Either<T4, T5>, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6, T7>> UnAssociate2And1And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>, T7> UnAssociate2And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, Either<T5, T6>, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, T5, Either<T6, T7>> UnAssociate2And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, T5, Either<T6, T7>>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate2And1And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<Either<T0, T1>, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<Either<T0, T1>, T2, T3, T4, T5, T6, T7> UnAssociate2And1And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<Either<T0, T1>, T2, T3, T4, T5, T6, T7>>(x => (Either<T0, T1>)x, x => (Either<T0, T1>)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And7<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5, T6, T7>> UnAssociate1And7<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x, x => (Either<T1, T2, T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And6And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5, T6>, T7> UnAssociate1And6And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (Either<T1, T2, T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And5And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5>, Either<T6, T7>> UnAssociate1And5And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And5And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4, T5>, T6, T7> UnAssociate1And5And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4, T5>, T6, T7>>(x => (T0)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (Either<T1, T2, T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And4And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6, T7>> UnAssociate1And4And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And4And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>, T7> UnAssociate1And4And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, Either<T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And4And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, T5, Either<T6, T7>> UnAssociate1And4And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, T5, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And4And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3, T4>, T5, T6, T7> UnAssociate1And4And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3, T4>, T5, T6, T7>>(x => (T0)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (Either<T1, T2, T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6, T7>> UnAssociate1And3And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>, T7> UnAssociate1And3And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5>, Either<T6, T7>> UnAssociate1And3And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6, T7> UnAssociate1And3And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, Either<T4, T5>, T6, T7>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6, T7>> UnAssociate1And3And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>, T7> UnAssociate1And3And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, Either<T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, T5, Either<T6, T7>> UnAssociate1And3And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, T5, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And3And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2, T3>, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2, T3>, T4, T5, T6, T7> UnAssociate1And3And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2, T3>, T4, T5, T6, T7>>(x => (T0)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (Either<T1, T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6, T7>> UnAssociate1And2And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>, T7> UnAssociate1And2And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5>, Either<T6, T7>> UnAssociate1And2And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6, T7> UnAssociate1And2And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4, T5>, T6, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6, T7>> UnAssociate1And2And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>, T7> UnAssociate1And2And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, Either<T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, T5, Either<T6, T7>> UnAssociate1And2And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, T5, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6, T7> UnAssociate1And2And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, Either<T3, T4>, T5, T6, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6, T7>> UnAssociate1And2And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>, T7> UnAssociate1And2And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5>, Either<T6, T7>> UnAssociate1And2And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6, T7> UnAssociate1And2And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, Either<T4, T5>, T6, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6, T7>> UnAssociate1And2And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>, T7> UnAssociate1And2And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, Either<T5, T6>, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, T5, Either<T6, T7>> UnAssociate1And2And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, T5, Either<T6, T7>>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And2And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, Either<T1, T2>, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, Either<T1, T2>, T3, T4, T5, T6, T7> UnAssociate1And2And1And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, Either<T1, T2>, T3, T4, T5, T6, T7>>(x => (T0)x, x => (Either<T1, T2>)x, x => (Either<T1, T2>)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And6<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5, T6, T7>> UnAssociate1And1And6<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x, x => (Either<T2, T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And5And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5, T6>, T7> UnAssociate1And1And5And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (Either<T2, T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And4And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5>, Either<T6, T7>> UnAssociate1And1And4And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And4And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4, T5>, T6, T7> UnAssociate1And1And4And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4, T5>, T6, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (Either<T2, T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And3And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6, T7>> UnAssociate1And1And3And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And3And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>, T7> UnAssociate1And1And3And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, Either<T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And3And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, T5, Either<T6, T7>> UnAssociate1And1And3And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, T5, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And3And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3, T4>, T5, T6, T7> UnAssociate1And1And3And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3, T4>, T5, T6, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (Either<T2, T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6, T7>> UnAssociate1And1And2And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>, T7> UnAssociate1And1And2And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>> UnAssociate1And1And2And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6, T7> UnAssociate1And1And2And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, Either<T4, T5>, T6, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6, T7>> UnAssociate1And1And2And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>, T7> UnAssociate1And1And2And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, Either<T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, T5, Either<T6, T7>> UnAssociate1And1And2And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, T5, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, Either<T2, T3>, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, Either<T2, T3>, T4, T5, T6, T7> UnAssociate1And1And2And1And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, Either<T2, T3>, T4, T5, T6, T7>>(x => (T0)x, x => (T1)x, x => (Either<T2, T3>)x, x => (Either<T2, T3>)x, x => (T4)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5, T6, T7>> UnAssociate1And1And1And5<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x, x => (Either<T3, T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5, T6>, T7> UnAssociate1And1And1And4And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (Either<T3, T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5>, Either<T6, T7>> UnAssociate1And1And1And3And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4, T5>, T6, T7> UnAssociate1And1And1And3And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4, T5>, T6, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (Either<T3, T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6, T7>> UnAssociate1And1And1And2And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>, T7> UnAssociate1And1And1And2And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, Either<T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4>, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, T5, Either<T6, T7>> UnAssociate1And1And1And2And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, T5, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, Either<T3, T4>, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, Either<T3, T4>, T5, T6, T7> UnAssociate1And1And1And2And1And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, Either<T3, T4>, T5, T6, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (Either<T3, T4>)x, x => (Either<T3, T4>)x, x => (T5)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, Either<T4, T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5, T6, T7>> UnAssociate1And1And1And1And4<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x, x => (Either<T4, T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, Either<T4, T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5, T6>, T7> UnAssociate1And1And1And1And3And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (Either<T4, T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, Either<T4, T5>, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5>, Either<T6, T7>> UnAssociate1And1And1And1And2And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5>, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, Either<T4, T5>, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, Either<T4, T5>, T6, T7> UnAssociate1And1And1And1And2And1And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, Either<T4, T5>, T6, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (Either<T4, T5>)x, x => (Either<T4, T5>)x, x => (T6)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, Either<T5, T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, T4, Either<T5, T6, T7>> UnAssociate1And1And1And1And1And3<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, Either<T5, T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x, x => (Either<T5, T6, T7>)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, Either<T5, T6>, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, T4, Either<T5, T6>, T7> UnAssociate1And1And1And1And1And2And1<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, Either<T5, T6>, T7>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (Either<T5, T6>)x, x => (Either<T5, T6>)x, x => (T7)x);
		}
		public static Either<T0, T1, T2, T3, T4, T5, T6, T7> Associate1And1And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, Either<T6, T7>> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, T6, T7>>(x => x, x => x, x => x, x => x, x => x, x => x, x => x);
		}

		public static Either<T0, T1, T2, T3, T4, T5, Either<T6, T7>> UnAssociate1And1And1And1And1And1And2<T0, T1, T2, T3, T4, T5, T6, T7>(this Either<T0, T1, T2, T3, T4, T5, T6, T7> parameter)
		{
			return parameter.Apply<Either<T0, T1, T2, T3, T4, T5, Either<T6, T7>>>(x => (T0)x, x => (T1)x, x => (T2)x, x => (T3)x, x => (T4)x, x => (T5)x, x => (Either<T6, T7>)x, x => (Either<T6, T7>)x);
		}
#endregion Associate for 8

		//TODO: Commutative operations
	}
}