using System;
using System.Collections;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

namespace _04._09._2017_UniversalComparer
{
    public class UniversalComparer : IComparer
    {
        public string CompareString { get; }
        public bool NullValuesSmallest { get; }

        public UniversalComparer(string compareString, bool nullValuesSmallest = false)
        {
            CompareString = compareString;
            NullValuesSmallest = nullValuesSmallest;
        }

        /// <summary>
        /// Get field value of an object
        /// </summary>
        /// <param name="obj">Instance of an object</param>
        /// <param name="member">Member part to parse</param>
        /// <returns></returns>
        private object ParseMember(object obj, string member)
        {
            var parts = member.Split('.');
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var memberToGet = obj.GetType().GetMember(parts[i])[0];
                obj = (memberToGet as FieldInfo) == null ? ((PropertyInfo)memberToGet).GetValue(obj) :
                    ((FieldInfo)memberToGet).GetValue(obj);
            }

            var fieldInfo = obj?.GetType().GetField(parts.Last());

            if (fieldInfo == null)
            {
                return obj?.GetType().GetProperty(parts.Last())?.GetValue(obj);
            }

            return fieldInfo.GetValue(obj);
        }

        /// <summary>
        /// Checks if x less than object y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True, if x less than y, otherwise false</returns>
        private bool LessThan(object x, object y)
        {
            return Expression.Lambda<Func<bool>>(Expression.LessThan(Expression.Constant(x), Expression.Constant(y)))
                .Compile()();
        }

        /// <summary>
        /// Checks if x equals y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True, if x equals y, otherwise false</returns>
        private bool Equal(object x, object y)
        {
            return Expression.Lambda<Func<bool>>(Expression.Equal(Expression.Constant(x), Expression.Constant(y)))
                .Compile()();
        }

        /// <summary>
        /// Compare two values in ascending order
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns></returns>
        private int CompareAsc(object value1, object value2)
        {
            return LessThan(value1, value2) ? -1 : 1;
        }

        /// <summary>
        /// Compare two values in descending order
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns></returns>
        private int CompareDesc(object value1, object value2)
        {
            return LessThan(value1, value2) ? 1 : -1;
        }

        public int Compare(object x, object y)
        {
            int result = 0;
            if (x == null & y == null) return result;
            
            // Remove all leading and trailing whitespaces and enumerate thru properties in compareString
            foreach (var parameter in CompareString.Split(',').Select(part => part.Trim()).Select(part => part.Split(' ')))
            {
                string property = parameter[0], sorting = "asc";

                // Getting sorting direction
                if (parameter.Length == 2)
                {
                    sorting = parameter[1];
                    if(sorting != "asc" & sorting != "desc")
                        throw new ArgumentException("Sorting parameter is invalid");
                }

                object value1 = ParseMember(x, property);
                object value2 = ParseMember(y, property);

                if (value1 == null & value2 == null)
                {
                    result = 0;
                    continue;
                }
                
                if (value1 == null || value2 == null)
                {
                    if (NullValuesSmallest)
                    {
                        return value1 == null ? -1 : 1;
                    }
                    return value1 == null ? 1 : -1;
                }

                if (Equal(value1, value2))
                {
                    result = 0;
                    continue;
                }

                return (sorting == "asc") ? CompareAsc(value1, value2) : CompareDesc(value1, value2);
            }

            return result;
        }
    }
}
