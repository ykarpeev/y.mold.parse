using System.Reflection;

namespace Y.Mold.Parse
{
    /// <summary>
    /// Extensions class.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Get property value.
        /// </summary>
        /// <param name="src">Source item.</param>
        /// <param name="propName">Property name.</param>
        /// <returns>Property value object.</returns>
        public static object GetPropValue(this object src, string propName)
        {
            var t = src.GetType();
            var p = t.GetProperty(propName, BindingFlags.NonPublic | BindingFlags.Instance);
            var v = p.GetValue(src, null);
            return v;
        }
    }
}
