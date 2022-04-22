using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable ConvertIfStatementToSwitchExpression

namespace BaseUnitTestProject.LanguageExtensions
{
    public static partial class LanguageExtensions
    {

        /// <summary>
        /// Find variances between two objects
        /// </summary>
        /// <typeparam name="TEntity">Model</typeparam>
        /// <param name="value1">first to compare to second</param>
        /// <param name="value2">second to compare to first</param>
        /// <returns>Any differences between the two values in <see cref="TEntity"/> </returns>
        public static List<Variance> DetailedCompare<TEntity>(this TEntity value1, TEntity value2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] properties = value1.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                Variance variance = new Variance
                {
                    PropertyName = propertyInfo.Name,
                    valueA = propertyInfo.GetValue(value1),
                    valueB = propertyInfo.GetValue(value2)
                };

                if (!Equals(variance.valueA, variance.valueB))
                {
                    variances.Add(variance);
                }
            }

            return variances;
        }
    }
}
