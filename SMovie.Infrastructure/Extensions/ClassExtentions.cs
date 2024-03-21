using System.Reflection;

namespace SMovie.Infrastructure.Extensions
{
    public static class ClassExtentions
    {
        public static bool HasAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttribute<T>() != null;
        }

        public static bool IsDateTimeProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsAssignableFrom(typeof(DateTime));
        }

    }
}
