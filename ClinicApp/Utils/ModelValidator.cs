using ClinicApp.Models;
using ClinicApp.Attributes;
using System.Reflection;

namespace ClinicApp.Utils;

static public class ModelValidator
{
    public static ValidationResult Validate(object obj)
        {
            ValidationResult result = new ValidationResult();

            if (obj == null)
            {
                result.AddError("Object cannot be null.");
                return result;
            }

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(obj);

                // 1. Перевірка RequiredAttribute
                RequiredAttribute requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttr != null)
                {
                    if (value == null || (value is string strVal && string.IsNullOrWhiteSpace(strVal)))
                    {
                        result.AddError($"{prop.Name}: {requiredAttr.ErrorMessage}");
                    }
                }
                
                MaxLengthAttribute maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLengthAttr != null && value is string str)
                {
                    if (str.Length > maxLengthAttr.Length)
                    {
                        result.AddError($"{prop.Name}: {maxLengthAttr.ErrorMessage}");
                    }
                }
                
                MinValueAttribute minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
                if (minValueAttr != null && value != null)
                {
                    try
                    {
                        double numericValue = Convert.ToDouble(value);
                        if (numericValue < minValueAttr.Min)
                        {
                            result.AddError($"{prop.Name}: {minValueAttr.ErrorMessage}");
                        }
                    }
                    catch (FormatException) { }
                    catch (InvalidCastException) { }
                }
            }

            return result;
        }

        public static void PrintInfo(Type type)
        {
            if (type == null) return;

            Console.WriteLine($"Type: {type.Name}");
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                Console.WriteLine($" - Property: {prop.Name} ({prop.PropertyType.Name})");
                
                object[] attributes = prop.GetCustomAttributes(true);
                foreach (object attr in attributes)
                {
                    Console.WriteLine($"   * Attribute: {attr.GetType().Name}");
                }
            }
        }
}