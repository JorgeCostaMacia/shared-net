using Shared.Service.Domain;
#if !NET9_0 || !NET8_0|| !NET7_0|| !NET6_0
using System;
#endif

namespace Shared.Data.Converters.Infrastructure
{
    public sealed class GuidInvertBytesConverter : IService
    {
        public static byte[] ConvertToBytes(Guid guid)
        {
            byte[] Value = guid.ToByteArray();

            Array.Reverse(Value, 0, 4);
            Array.Reverse(Value, 4, 2);
            Array.Reverse(Value, 6, 2);

            return Value;
        }

        public static Guid ConvertFromBytes(byte[] guid)
        {
            byte[] Value = (byte[])guid.Clone();

            Array.Reverse(Value, 0, 4);
            Array.Reverse(Value, 4, 2);
            Array.Reverse(Value, 6, 2);

            return new Guid(Value);
        }
    }
}
