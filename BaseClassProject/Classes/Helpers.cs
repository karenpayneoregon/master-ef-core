using System;
using System.Collections;
using System.Text;

namespace BaseClassProject.Classes
{
    /// <summary>
    /// Provides method to calculate current birthdate 
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Generic method for iterating each property, obtain name and value,
        /// if value type is DateTime, format it for short date.
        /// </summary>
        /// <typeparam name="T">type of container</typeparam>
        /// <param name="container">data to iterate</param>
        /// <returns>string with names/values</returns>
        /// <remarks>
        /// This is not suitable for classes with class properties. If needed it's
        /// a tad more code and requires strict unit testing.
        /// </remarks>
        public static string GetPropertyData<T>(T container) where T : new() // constrain to types with a default ctor
        {
            StringBuilder builder = new();

            try
            {
                foreach (var current in (IEnumerable)container)
                {

                    foreach (var propertyInfo in current.GetType().GetProperties())
                    {

                        /*
                         * The null-coalescing operator ?? returns the value of its left-hand operand if it
                         * isn't null; otherwise, it evaluates the right-hand operand and returns its result.
                         * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
                         */
                        var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                        builder.AppendLine(type == typeof(DateTime) ?
                            $"{propertyInfo.Name,-30} {Convert.ToDateTime(propertyInfo.GetValue(current, null)):d}" :
                            $"{propertyInfo.Name,-30} {propertyInfo.GetValue(current, null)}");
                    }

                    builder.AppendLine("");

                }
            }
            catch (Exception ex)
            {
                builder.AppendLine(ex.Message);
            }

            return builder.ToString();

        }

    }
}
