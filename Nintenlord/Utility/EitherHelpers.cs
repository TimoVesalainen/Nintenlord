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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1>, TOut> Combine<T0, T1, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1)
		{
			return either => either.Apply(func0, func1);
		}
		
//TODO
//		public static IObservable<Either<T0, T1>> MergeEither<T0, T1>(this IObservable<T0> observable0, IObservable<T1> observable1)
//		{
//			return observable0.Select(Either<T0, T1>.From0).Merge(observable1.Select(Either<T0, T1>.From1));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2>, TOut> Combine<T0, T1, T2, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2)
		{
			return either => either.Apply(func0, func1, func2);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2>> MergeEither<T0, T1, T2>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2)
//		{
//			return observable0.MergeEither(observable1).Select(x => null).Merge(observable2.Select(Either<T0, T1, T2>.From2));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2, T3>, TOut> Combine<T0, T1, T2, T3, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3)
		{
			return either => either.Apply(func0, func1, func2, func3);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2, T3>> MergeEither<T0, T1, T2, T3>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3)
//		{
//			return observable0.MergeEither(observable1, observable2).Select(x => null).Merge(observable3.Select(Either<T0, T1, T2, T3>.From3));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2, T3, T4>, TOut> Combine<T0, T1, T2, T3, T4, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4)
		{
			return either => either.Apply(func0, func1, func2, func3, func4);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2, T3, T4>> MergeEither<T0, T1, T2, T3, T4>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4)
//		{
//			return observable0.MergeEither(observable1, observable2, observable3).Select(x => null).Merge(observable4.Select(Either<T0, T1, T2, T3, T4>.From4));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2, T3, T4, T5>, TOut> Combine<T0, T1, T2, T3, T4, T5, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2, T3, T4, T5>> MergeEither<T0, T1, T2, T3, T4, T5>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5)
//		{
//			return observable0.MergeEither(observable1, observable2, observable3, observable4).Select(x => null).Merge(observable5.Select(Either<T0, T1, T2, T3, T4, T5>.From5));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2, T3, T4, T5, T6>, TOut> Combine<T0, T1, T2, T3, T4, T5, T6, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5, Func<T6, TOut> func6)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5, func6);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2, T3, T4, T5, T6>> MergeEither<T0, T1, T2, T3, T4, T5, T6>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5, IObservable<T6> observable6)
//		{
//			return observable0.MergeEither(observable1, observable2, observable3, observable4, observable5).Select(x => null).Merge(observable6.Select(Either<T0, T1, T2, T3, T4, T5, T6>.From6));
//		}

		//Associative operations
		//Commutative operations
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

		
		//TODO: SelectMany, Where, MultiSelectMany

		public static Func<Either<T0, T1, T2, T3, T4, T5, T6, T7>, TOut> Combine<T0, T1, T2, T3, T4, T5, T6, T7, TOut>(this Func<T0, TOut> func0, Func<T1, TOut> func1, Func<T2, TOut> func2, Func<T3, TOut> func3, Func<T4, TOut> func4, Func<T5, TOut> func5, Func<T6, TOut> func6, Func<T7, TOut> func7)
		{
			return either => either.Apply(func0, func1, func2, func3, func4, func5, func6, func7);
		}
		
//TODO
//		public static IObservable<Either<T0, T1, T2, T3, T4, T5, T6, T7>> MergeEither<T0, T1, T2, T3, T4, T5, T6, T7>(this IObservable<T0> observable0, IObservable<T1> observable1, IObservable<T2> observable2, IObservable<T3> observable3, IObservable<T4> observable4, IObservable<T5> observable5, IObservable<T6> observable6, IObservable<T7> observable7)
//		{
//			return observable0.MergeEither(observable1, observable2, observable3, observable4, observable5, observable6).Select(x => null).Merge(observable7.Select(Either<T0, T1, T2, T3, T4, T5, T6, T7>.From7));
//		}

		//Associative operations
		//Commutative operations
	}
}