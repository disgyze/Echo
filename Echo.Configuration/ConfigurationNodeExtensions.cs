using System;
using System.ComponentModel;
using System.Linq;

namespace Echo.Configuration
{
    public static class ConfigurationNodeExtensions
    {
        public static string? GetValueAsString(this ConfigurationNode node, string key, string? defaultValue = default)
        {
            return node.Properties.TryGetValue(key, out var value) ? value?.ToString() : defaultValue;
        }

        public static Guid GetValueAsGuid(this ConfigurationNode node, string key, Guid defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    Guid typedValue => typedValue,
                    string stringValue when Guid.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static bool GetValueAsBoolean(this ConfigurationNode node, string key, bool defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    bool typedValue => typedValue,
                    string stringValue when bool.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static int GetValueAsInt32(this ConfigurationNode node, string key, int defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    int typedValue => typedValue,
                    string stringValue when int.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static long GetValueAsInt64(this ConfigurationNode node, string key, long defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    long typedValue => typedValue,
                    string stringValue when long.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static byte GetValueAsByte(this ConfigurationNode node, string key, byte defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    byte typedValue => typedValue,
                    string stringValue when byte.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static float GetValueAsFloat(this ConfigurationNode node, string key, float defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    float typedValue => typedValue,
                    string stringValue when float.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static double GetValueAsDouble(this ConfigurationNode node, string key, double defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    double typedValue => typedValue,
                    string stringValue when double.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static decimal GetValueAsDecimal(this ConfigurationNode node, string key, decimal defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    decimal typedValue => typedValue,
                    string stringValue when decimal.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static DateOnly GetValueAsDateOnly(this ConfigurationNode node, string key, DateOnly defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    DateOnly typedValue => typedValue,
                    string stringValue when DateOnly.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static TimeOnly GetValueAsTimeOnly(this ConfigurationNode node, string key, TimeOnly defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    TimeOnly typedValue => typedValue,
                    string stringValue when TimeOnly.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static DateTime GetValueAsDateTime(this ConfigurationNode node, string key, DateTime defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    DateTime typedValue => typedValue,
                    string stringValue when DateTime.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static DateTimeOffset GetValueAsDateTimeOffset(this ConfigurationNode node, string key, DateTimeOffset defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    DateTimeOffset typedValue => typedValue,
                    string stringValue when DateTimeOffset.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static TimeSpan GetValueAsTimeSpan(this ConfigurationNode node, string key, TimeSpan defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    TimeSpan typedValue => typedValue,
                    string stringValue when TimeSpan.TryParse(stringValue, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static TValue GetValueAsEnum<TValue>(this ConfigurationNode node, string key, TValue defaultValue = default) where TValue : struct, Enum
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    TValue typedValue => typedValue,
                    string stringValue when Enum.TryParse<TValue>(stringValue, true, out var result) => result,
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static TValue? GetValue<TValue>(this ConfigurationNode node, string key, TValue? defaultValue = default)
        {
            if (node.Properties.TryGetValue(key, out var value))
            {
                return value switch
                {
                    TValue typedValue => typedValue,
                    string stringValue => (TValue?)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFrom(stringValue),
                    _ => defaultValue
                };
            }
            return defaultValue;
        }

        public static bool TryGetValue<TValue>(this ConfigurationNode node, string key, out TValue? value, TValue? defaultValue = default)
        {
            try
            {
                value = node.GetValue(key, defaultValue);
                return true;
            }
            catch
            {
                value = defaultValue;
                return false;
            }
        }

        public static ConfigurationNode AddProperty(this ConfigurationNode node, string key, object? value)
        {
            return node with { Properties = node.Properties.Add(key, value) };
        }

        public static ConfigurationNode SetProperty(this ConfigurationNode node, string key, object? value = default)
        {
            return node with { Properties = node.Properties.SetItem(key, value) };
        }

        public static ConfigurationNode RemoveProperty(this ConfigurationNode node, string key)
        {
            return node with { Properties = node.Properties.Remove(key) };
        }

        public static ConfigurationNode? GetNode(this ConfigurationNode node, string key)
        {
            return node.Children.FirstOrDefault(child => ConfigurationNodeKeyComparer.Default.Equals(child.Key, key));
        }

        public static ConfigurationNode GetNodeOrEmpty(this ConfigurationNode node, string key)
        {
            return node.GetNode(key) ?? ConfigurationNode.Empty;
        }

        public static ConfigurationNode AddNode(this ConfigurationNode node, ConfigurationNode child)
        {
            return node with { Children = node.Children.Add(child) };
        }

        public static ConfigurationNode RemoveNode(this ConfigurationNode node, ConfigurationNode child)
        {
            return node with { Children = node.Children.Remove(child) };
        }

        public static ConfigurationNode ReplaceNode(this ConfigurationNode source, ConfigurationNode oldNode, ConfigurationNode newNode)
        {
            return source with { Children = source.Children.Replace(oldNode, newNode) };
        }
    }
}