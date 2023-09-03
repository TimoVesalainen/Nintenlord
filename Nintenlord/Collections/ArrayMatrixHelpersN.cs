using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.Collections
{
    public static partial class ArrayMatrixHelpers
    {
        public static TOut[,] TensorProduct<TOut, TIn0, TIn1>(this List<TIn0> list0, List<TIn1> list1, Func<TIn0, TIn1, TOut> product)
        {
            TOut[,] result = new TOut[list1.Count, list0.Count];

            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x1, x0] = product(list0[x0], list1[x1]);
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,] items, int x0, int x1)
        {
            if (x1 < 0 || x1 >= items.GetLength(0) || x0 < 0 || x0 >= items.GetLength(1))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x1, x0];
            }
        }
        public static TOut[,,] TensorProduct<TOut, TIn0, TIn1, TIn2>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, Func<TIn0, TIn1, TIn2, TOut> product)
        {
            TOut[,,] result = new TOut[list2.Count, list1.Count, list0.Count];

            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x2, x1, x0] = product(list0[x0], list1[x1], list2[x2]);
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,] items, int x0, int x1, int x2)
        {
            if (x2 < 0 || x2 >= items.GetLength(0) || x1 < 0 || x1 >= items.GetLength(1) || x0 < 0 || x0 >= items.GetLength(2))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x2, x1, x0];
            }
        }
        public static TOut[,,,] TensorProduct<TOut, TIn0, TIn1, TIn2, TIn3>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, List<TIn3> list3, Func<TIn0, TIn1, TIn2, TIn3, TOut> product)
        {
            TOut[,,,] result = new TOut[list3.Count, list2.Count, list1.Count, list0.Count];

            for (int x3 = 0; x3 < list3.Count; x3++)
            {
            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x3, x2, x1, x0] = product(list0[x0], list1[x1], list2[x2], list3[x3]);
            }
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,,] items, int x0, int x1, int x2, int x3)
        {
            if (x3 < 0 || x3 >= items.GetLength(0) || x2 < 0 || x2 >= items.GetLength(1) || x1 < 0 || x1 >= items.GetLength(2) || x0 < 0 || x0 >= items.GetLength(3))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x3, x2, x1, x0];
            }
        }
        public static TOut[,,,,] TensorProduct<TOut, TIn0, TIn1, TIn2, TIn3, TIn4>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, List<TIn3> list3, List<TIn4> list4, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TOut> product)
        {
            TOut[,,,,] result = new TOut[list4.Count, list3.Count, list2.Count, list1.Count, list0.Count];

            for (int x4 = 0; x4 < list4.Count; x4++)
            {
            for (int x3 = 0; x3 < list3.Count; x3++)
            {
            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x4, x3, x2, x1, x0] = product(list0[x0], list1[x1], list2[x2], list3[x3], list4[x4]);
            }
            }
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,,,] items, int x0, int x1, int x2, int x3, int x4)
        {
            if (x4 < 0 || x4 >= items.GetLength(0) || x3 < 0 || x3 >= items.GetLength(1) || x2 < 0 || x2 >= items.GetLength(2) || x1 < 0 || x1 >= items.GetLength(3) || x0 < 0 || x0 >= items.GetLength(4))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x4, x3, x2, x1, x0];
            }
        }
        public static TOut[,,,,,] TensorProduct<TOut, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, List<TIn3> list3, List<TIn4> list4, List<TIn5> list5, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TOut> product)
        {
            TOut[,,,,,] result = new TOut[list5.Count, list4.Count, list3.Count, list2.Count, list1.Count, list0.Count];

            for (int x5 = 0; x5 < list5.Count; x5++)
            {
            for (int x4 = 0; x4 < list4.Count; x4++)
            {
            for (int x3 = 0; x3 < list3.Count; x3++)
            {
            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x5, x4, x3, x2, x1, x0] = product(list0[x0], list1[x1], list2[x2], list3[x3], list4[x4], list5[x5]);
            }
            }
            }
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,,,,] items, int x0, int x1, int x2, int x3, int x4, int x5)
        {
            if (x5 < 0 || x5 >= items.GetLength(0) || x4 < 0 || x4 >= items.GetLength(1) || x3 < 0 || x3 >= items.GetLength(2) || x2 < 0 || x2 >= items.GetLength(3) || x1 < 0 || x1 >= items.GetLength(4) || x0 < 0 || x0 >= items.GetLength(5))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x5, x4, x3, x2, x1, x0];
            }
        }
        public static TOut[,,,,,,] TensorProduct<TOut, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, List<TIn3> list3, List<TIn4> list4, List<TIn5> list5, List<TIn6> list6, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> product)
        {
            TOut[,,,,,,] result = new TOut[list6.Count, list5.Count, list4.Count, list3.Count, list2.Count, list1.Count, list0.Count];

            for (int x6 = 0; x6 < list6.Count; x6++)
            {
            for (int x5 = 0; x5 < list5.Count; x5++)
            {
            for (int x4 = 0; x4 < list4.Count; x4++)
            {
            for (int x3 = 0; x3 < list3.Count; x3++)
            {
            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x6, x5, x4, x3, x2, x1, x0] = product(list0[x0], list1[x1], list2[x2], list3[x3], list4[x4], list5[x5], list6[x6]);
            }
            }
            }
            }
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,,,,,] items, int x0, int x1, int x2, int x3, int x4, int x5, int x6)
        {
            if (x6 < 0 || x6 >= items.GetLength(0) || x5 < 0 || x5 >= items.GetLength(1) || x4 < 0 || x4 >= items.GetLength(2) || x3 < 0 || x3 >= items.GetLength(3) || x2 < 0 || x2 >= items.GetLength(4) || x1 < 0 || x1 >= items.GetLength(5) || x0 < 0 || x0 >= items.GetLength(6))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x6, x5, x4, x3, x2, x1, x0];
            }
        }
        public static TOut[,,,,,,,] TensorProduct<TOut, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(this List<TIn0> list0, List<TIn1> list1, List<TIn2> list2, List<TIn3> list3, List<TIn4> list4, List<TIn5> list5, List<TIn6> list6, List<TIn7> list7, Func<TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> product)
        {
            TOut[,,,,,,,] result = new TOut[list7.Count, list6.Count, list5.Count, list4.Count, list3.Count, list2.Count, list1.Count, list0.Count];

            for (int x7 = 0; x7 < list7.Count; x7++)
            {
            for (int x6 = 0; x6 < list6.Count; x6++)
            {
            for (int x5 = 0; x5 < list5.Count; x5++)
            {
            for (int x4 = 0; x4 < list4.Count; x4++)
            {
            for (int x3 = 0; x3 < list3.Count; x3++)
            {
            for (int x2 = 0; x2 < list2.Count; x2++)
            {
            for (int x1 = 0; x1 < list1.Count; x1++)
            {
            for (int x0 = 0; x0 < list0.Count; x0++)
            {
                result[x7, x6, x5, x4, x3, x2, x1, x0] = product(list0[x0], list1[x1], list2[x2], list3[x3], list4[x4], list5[x5], list6[x6], list7[x7]);
            }
            }
            }
            }
            }
            }
            }
            }

            return result;
        }

        public static Maybe<T> GetSafe<T>(this T[,,,,,,,] items, int x0, int x1, int x2, int x3, int x4, int x5, int x6, int x7)
        {
            if (x7 < 0 || x7 >= items.GetLength(0) || x6 < 0 || x6 >= items.GetLength(1) || x5 < 0 || x5 >= items.GetLength(2) || x4 < 0 || x4 >= items.GetLength(3) || x3 < 0 || x3 >= items.GetLength(4) || x2 < 0 || x2 >= items.GetLength(5) || x1 < 0 || x1 >= items.GetLength(6) || x0 < 0 || x0 >= items.GetLength(7))
            {
                return Maybe<T>.Nothing;
            }
            else
            {
                return items[x7, x6, x5, x4, x3, x2, x1, x0];
            }
        }
    }
}