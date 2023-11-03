using System.Reflection;

namespace PEHeaderReader.StructuredData
{
    public class ConsoleDataPrinter
    {
        public static void PrintStruct(object structure)
        {
            Console.WriteLine(structure.GetType().Name);
            PrintStruct(structure, 0);
        }

        private static void PrintStruct(object structure, int indent)
        {
            Type type = structure.GetType();

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                object fieldValue = fieldInfo.GetValue(structure);
                Type fieldType = fieldInfo.FieldType;

                string formattedValue = FormatFieldValue(fieldType, fieldValue);

                Console.WriteLine($"{new string(' ', indent)}{fieldInfo.Name,-30}: " +
                    $"{formattedValue}");

                if (fieldType.IsValueType && !fieldType.IsPrimitive)
                {
                    PrintStruct(fieldValue, indent + 8);
                }
            }
            Console.WriteLine();
        }

        private static string FormatFieldValue(Type fieldType, object fieldValue)
        {
            if (IsNumericType(fieldType))
            {
                byte[] bytes = GetBytesFromNumericValue(fieldType, fieldValue);
                return BytesToHexString(bytes);
            }
            else if (fieldType == typeof(Half))
            {
                ushort halfValue = (ushort)(float)(dynamic)fieldValue;
                byte[] bytes = BitConverter.GetBytes(halfValue);
                return BytesToHexString(bytes);
            }
            else if (fieldType == typeof(char[]))
            {
                return ConvertCharArrayToString(fieldValue);
            }
            else
            {
                return fieldValue.GetType().Name;
            }
        }
        public static string ConvertCharArrayToString(object fieldValue)
        {
            if (fieldValue is char[] charArray)
            {
                string charString = new string(charArray);
                return charString;
            }
            return "fieldValue не содержит массив char[]";
        }
        private static string BytesToHexString(byte[] bytes)
        {
            return string.Join(" ", bytes.Select(b => b.ToString("X2")));
        }

        private static bool IsNumericType(Type type)
        {
            return type == typeof(int)
                || type == typeof(uint)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(byte);
        }

        private static byte[] GetBytesFromNumericValue(Type type, object value)
        {
            if (type == typeof(byte))
            {
                return new byte[] { (byte)value };
            }
            return BitConverter.GetBytes((dynamic)value);
        }

    }
}
