namespace DNNGamification
{
    using System;
    using System.Linq;
    using System.Web;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Auxiliary class.
    /// </summary>
    public class Utils
    {
        #region Public Methods

        /// <summary>
        /// Silently converts to type.
        /// </summary>
        public static T ConvertTo<T>(object value)
        {
            T result = default(T); Type type = typeof(T);

            if (value is DBNull) return result; // return default if DBNull

            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) return result; type = Nullable.GetUnderlyingType(type);
            }
            if (type.IsEnum) // check if enum then use specified method to convert from object to typed enum
            {
                if (value != null) return (T)Enum.ToObject(type, value);
            }

            result = (T)Convert.ChangeType(value, type);
            {
                return result;
            }
        }

        #endregion
    }
}