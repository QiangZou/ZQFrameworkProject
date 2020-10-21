using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace ZQFramwork
{
    /// <summary>
    /// 枚举相等比较器
    /// 适用于使用枚举作为字典的key
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public class EnumComparer<T> : IEqualityComparer<T> where T : struct
    {
        public bool Equals(T x, T y)
        {
            var firstParam = Expression.Parameter(typeof(T), "x");

            var secondParam = Expression.Parameter(typeof(T), "y");

            var equalExpression = Expression.Equal(firstParam, secondParam);

            return Expression.Lambda<Func<T, T, bool>>

                (equalExpression, new[] { firstParam, secondParam }).

                Compile().Invoke(x, y);
        }

        public int GetHashCode(T obj)
        {
            var parameter = Expression.Parameter(typeof(T), "obj");

            var convertExpression = Expression.Convert(parameter, typeof(int));

            return Expression.Lambda<Func<T, int>>

                (convertExpression, new[] { parameter }).

                Compile().Invoke(obj);
        }
    }
}

