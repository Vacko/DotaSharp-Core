using System;

namespace DotaSharp.DotaSharpKernel
{
    internal static class InterfaceVersions
    {
        #region Static Methods

        public static string GetInterfaceIdentifier(Type steamClass)
        {
            foreach (InterfaceVersionsAttribute attribute in steamClass.GetCustomAttributes(
                typeof(InterfaceVersionsAttribute), false)) return attribute.Identifier;

            return string.Empty;
        }

        #endregion
    }
}