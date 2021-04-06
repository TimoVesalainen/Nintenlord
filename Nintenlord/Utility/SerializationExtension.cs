using System.Runtime.Serialization;

namespace Nintenlord.Utility
{
    public static class SerializationExtension
    {
        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }
    }
}
