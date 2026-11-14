using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
    {
        public static T Build<T>() where T : new()
        {
            T obj = new T();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.GetSetMethod() == null)
                {
                    continue;
                }

                if (prop.Name == "Id" || prop.Name == "Status")
                {
                    continue;
                }

                string promptText = BuildPrompt(prop);

                bool isSuccessfullySet = false;

                while (!isSuccessfullySet)
                {
                    Console.Write($"{prop.Name} ({promptText}): ");
                    string input = Console.ReadLine();

                    try
                    {
                        object convertedValue;

                        Type targetType = prop.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(targetType);
                        if (underlyingType != null)
                        {
                            targetType = underlyingType;
                        }

                        if (targetType.IsEnum)
                        {
                            convertedValue = Enum.Parse(targetType, input, true);
                        }
                        else
                        {
                            convertedValue = Convert.ChangeType(input, targetType);
                        }

                        prop.SetValue(obj, convertedValue);
                        isSuccessfullySet = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                    }
                }
            }

            return obj;
        }

        private static string BuildPrompt(PropertyInfo prop)
        {
            List<string> parts = new List<string>();

            RequiredAttribute reqAttr = prop.GetCustomAttribute<RequiredAttribute>();
            if (reqAttr != null)
            {
                parts.Add("обов'язкове");
            }

            MaxLengthAttribute maxAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxAttr != null)
            {
                parts.Add($"макс. {maxAttr.Length}");
            }

            MinValueAttribute minAttr = prop.GetCustomAttribute<MinValueAttribute>();
            if (minAttr != null)
            {
                parts.Add($"мін. {minAttr.Min}");
            }

            return string.Join(", ", parts.ToArray());
        }
    }
