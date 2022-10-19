using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Data.Infrastructure.Expressions
{
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        private static readonly MethodInfo StringConvertMethodDouble = typeof(SqlFunctions).GetMethod("StringConvert", new Type[] { typeof(double?) });
        private static readonly MethodInfo StringConvertMethodDecimal = typeof(SqlFunctions).GetMethod("StringConvert", new Type[] { typeof(decimal?) });
        
        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
           MemberExpression member = Expression.Property(param, filter.PropertyName);
           ConstantExpression constant = Expression.Constant(filter.Value);
           Expression stringExpression = null;

           switch (filter.Operation)
           {
               case Op.Equals:
                   return Expression.Equal(member, constant);

               case Op.GreaterThan:
                   return Expression.GreaterThan(member, constant);

               case Op.GreaterThanOrEqual:
                   return Expression.GreaterThanOrEqual(member, constant);

               case Op.LessThan:
                   return Expression.LessThan(member, constant);

               case Op.LessThanOrEqual:
                   return Expression.LessThanOrEqual(member, constant);

               case Op.Contains:
                   stringExpression = GetConvertToStringExpression(member);
                   return Expression.Call(stringExpression, containsMethod, constant);

               case Op.StartsWith:
                   stringExpression = GetConvertToStringExpression(member);
                   return Expression.Call(stringExpression, startsWithMethod, constant);

               case Op.EndsWith:
                   stringExpression = GetConvertToStringExpression(member);
                   return Expression.Call(stringExpression, endsWithMethod, constant);
           }

           return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }

        private static Expression GetConvertToStringExpression(Expression e)
        {
            // if property string - no cast needed
            // else - use SqlFunction.StringConvert(double?) or SqlFunction.StringConvert(decimal?);
            Expression strExpression = null;
            if (e.Type == typeof(string))
                strExpression = e;

            var systemType = Nullable.GetUnderlyingType(e.Type) ?? e.Type;

            if (systemType == typeof(int)
                || systemType == typeof(long)
                || systemType == typeof(double)
                || systemType == typeof(short)
                || systemType == typeof(byte)) // continue
            {
                // cast int to double
                var doubleExpr = Expression.Convert(e, typeof(double?));
                strExpression = Expression.Call(StringConvertMethodDouble, doubleExpr);
            }

            if (systemType == typeof(decimal))
            {
                // call decimal version of StringConvert method
                // cast to nullable decimal
                var decimalExpr = Expression.Convert(e, typeof(decimal?));
                strExpression = Expression.Call(StringConvertMethodDecimal, decimalExpr);
            }

            return strExpression;
        }
    }
}
