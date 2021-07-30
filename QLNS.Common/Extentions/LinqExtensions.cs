using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common.Extentions
{
    public static class LinqExtensions
    {
        public static Expression<Func<T, bool>> GetFilter<T>(this KendoGridFilterModel filter)
        {
            var predicate = PredicateBuilder.Create<T>(s => true);
            if (filter.FilterItems == null)
            {
                return predicate;
            }

            foreach (var filterItem in filter.FilterItems)
            {
                for (var i = 0; i < filterItem.Filters.Count; i++)
                {
                    var item = filterItem.Filters[i];

                    if (item.Field.Split('.').Length > 1)
                    {
                        continue;
                    }

                    var type = typeof(T);
                    var field = type.GetProperty(item.Field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (field != null)
                    {
                        var fieldName = field.Name;

                        if (field.IsNumericType())
                        {
                            predicate = AndNumber(predicate, item, fieldName);
                        }
                        else if (field.IsDateType())
                        {
                            continue;
                            //predicate = AndDate(predicate, item, fieldName);
                        }
                        else if (field.IsBoolType())
                        {
                            predicate = AndBoolean(predicate, item, fieldName);
                        }
                        else
                        {
                            predicate = AndString<T>(predicate, item, fieldName);
                        }
                    }
                }
            }
            return predicate;
        }

        private static Expression<Func<T, bool>> AndNumber<T>(Expression<Func<T, bool>> predicate, Filters item, string fieldName)
        {
            object value = typeof(T).GetProperty(fieldName).GetValueNumeric(item.Value);

            Expression expression = null;
            var parameter = Expression.Parameter(typeof(T), "s");
            var member = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value, (typeof(T).GetProperty(fieldName)).PropertyType);

            if (FilterInfo.EqualStrings.Contains(item.Operator))
            {
                expression = Expression.Equal(member, constant);
            }
            else if (FilterInfo.GreaterOrEqualStrings.Contains(item.Operator))
            {
                expression = Expression.GreaterThanOrEqual(member, constant);
            }
            else if (FilterInfo.LessOrEqualStrings.Contains(item.Operator))
            {
                expression = Expression.LessThanOrEqual(member, constant);
            }


            if (expression != null)
            {
                var expressionWhere = Expression.Lambda<Func<T, bool>>(expression, parameter);
                predicate = predicate.And(expressionWhere);
            }

            return predicate;
        }


        private static Expression<Func<T, bool>> AndString<T>(Expression<Func<T, bool>> predicate, Filters item, string fieldName)
        {
            var value = item.Value;
            Expression expression = null;
            var parameter = Expression.Parameter(typeof(T), "s");
            var member = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value, (typeof(T).GetProperty(fieldName)).PropertyType);
            if (FilterInfo.EqualStrings.Contains(item.Operator))
            {
                Expression left = Expression.Call(member, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                Expression right = Expression.Call(constant, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                expression = Expression.Equal(left, right);
            }
            else if (FilterInfo.ContainsStrings.Contains(item.Operator))
            {
                Expression left = Expression.Call(member, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                Expression right = Expression.Call(constant, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                MethodInfo method = (typeof(T).GetProperty(fieldName)).PropertyType.GetMethod("Contains", new[] { typeof(string) });
                expression = Expression.Call(left, method, right);
            }

            if (expression != null)
            {
                var expressionWhere = Expression.Lambda<Func<T, bool>>(expression, parameter);
                predicate = predicate.And(expressionWhere);
            }

            return predicate;
        }

        private static Expression<Func<T, bool>> AndBoolean<T>(Expression<Func<T, bool>> predicate, Filters item, string fieldName)
        {
            bool? value = null;
            if (!string.IsNullOrEmpty(item.Value))
            {
                value = bool.Parse(item.Value);
            }
            Expression expression = null;
            var parameter = Expression.Parameter(typeof(T), "s");
            var member = Expression.Property(parameter, fieldName);
            var constant = Expression.Constant(value, (typeof(T).GetProperty(fieldName)).PropertyType);
            if (FilterInfo.EqualStrings.Contains(item.Operator))
            {
                expression = Expression.Equal(member, constant);
            }

            if (expression != null)
            {
                var expressionWhere = Expression.Lambda<Func<T, bool>>(expression, parameter);
                predicate = predicate.And(expressionWhere);
            }

            return predicate;
        }


        public static T GetModel<T>(this KendoGridFilterModel filter) where T : BaseModelFilter, new()
        {
            T model = new T();
            model.CreatedDate = DateTime.MinValue;
            foreach (var filterItem in filter.FilterItems)
            {
                for (var i = 0; i < filterItem.Filters.Count; i++)
                {
                    var item = filterItem.Filters[i];

                    if (item.Field.Split('.').Length > 1)
                    {
                        continue;
                    }

                    var type = typeof(T);
                    var field = type.GetProperty(item.Field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (field != null)
                    {
                        if (field.IsNumericType())
                        {
                            object value = field.GetValueNumeric(item.Value);
                            field.SetValue(model, value);
                        }
                        else if (field.IsDateType())
                        {
                            DateTime? date = null;
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                var dateString = item.Value.Substring(4, 20);
                                date = dateString.ToDate(null, "MMM dd yyyy HH:mm:ss");
                            }
                            field.SetValue(model, date);
                        }
                        else if (field.IsBoolType())
                        {
                            bool? value = null;
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                value = bool.Parse(item.Value);
                            }
                            field.SetValue(model, value);
                        }
                        else
                        {
                            field.SetValue(model, item.Value);
                        }
                    }
                }
            }
            return model;
        }
    }
}
