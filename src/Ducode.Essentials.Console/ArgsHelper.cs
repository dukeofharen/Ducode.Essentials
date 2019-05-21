using System;
using System.Collections.Generic;
using System.Linq;

namespace Ducode.Essentials.Console
{
    /// <summary>
    /// A static class that contains several methods for working with console application arguments.
    /// </summary>
    public static class ArgsHelper
    {
        /// <summary>
        /// Parses the command line arguments.
        /// Pass the arguments like this: --arg1 value1 --arg2 value2
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A dictionary containing the parsed keys and values.</returns>
        public static IDictionary<string, string> Parse(this string[] args)
        {
            var subResult = new Dictionary<string, List<string>>();

            string varPointer = string.Empty;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    varPointer = arg.Replace("--", string.Empty);
                    subResult.Add(varPointer, new List<string>());
                }
                else
                {
                    if (subResult.ContainsKey(varPointer))
                    {
                        subResult[varPointer].Add(arg);
                    }
                }
            }

            return subResult
                .ToDictionary(d => d.Key, d => string.Join(" ", d.Value));
        }

        /// <summary>
        /// Returns a value from the parsed dictionary. Returns null if value is not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as string.</returns>
        public static string GetValue(this IDictionary<string, string> args, string key)
        {
            args.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary. Returns the default value if value is not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as string.</returns>
        public static string GetValue(this IDictionary<string, string> args, string key, string defaultValue)
        {
            args.TryGetValue(key, out string value);
            return value ?? defaultValue;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary. Returns the default value if value is not found. Adds the value to the dictionary if it was not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as string.</returns>
        public static string GetAndSetValue(this IDictionary<string, string> args, string key, string defaultValue)
        {
            if(!args.TryGetValue(key, out string value))
            {
                args.Add(key, defaultValue);
            }

            return value ?? defaultValue;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary and tries to parse it as int. Returns the default value if value is no int or if value is not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as int.</returns>
        public static int GetValue(this IDictionary<string, string> args, string key, int defaultValue)
        {
            if (!args.TryGetValue(key, out string value))
            {
                return defaultValue;
            }

            if (!int.TryParse(value, out int result))
            {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary and tries to parse it as int. Returns the default value if value is no int or if value is not found. Adds the value to the dictionary if it was not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as int.</returns>
        public static int GetAndSetValue(this IDictionary<string, string> args, string key, int defaultValue)
        {
            if (!args.TryGetValue(key, out string value))
            {
                args.Add(key, defaultValue.ToString());
                return defaultValue;
            }

            if (!int.TryParse(value, out int result))
            {
                args.Add(key, defaultValue.ToString());
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary and tries to parse it as bool. Returns the default value if value is no bool or if value is not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as bool.</returns>
        public static bool GetValue(this IDictionary<string, string> args, string key, bool defaultValue)
        {
            if (!args.TryGetValue(key, out string value))
            {
                return defaultValue;
            }

            if (string.Equals(value, "1") || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(value, "0") || string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns a value from the parsed dictionary and tries to parse it as bool. Returns the default value if value is no bool or if value is not found. Adds the value to the dictionary if it was not found.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value as bool.</returns>
        public static bool GetAndSetValue(this IDictionary<string, string> args, string key, bool defaultValue)
        {
            if (!args.TryGetValue(key, out string value))
            {
                args.Add(key, defaultValue.ToString());
                return defaultValue;
            }

            if (string.Equals(value, "1") || string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(value, "0") || string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return defaultValue;
        }
    }
}
