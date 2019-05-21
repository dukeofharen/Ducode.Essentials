using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ducode.Essentials.Mvc
{
    /// <summary>
    /// A static class that contains several random MVC helper utilities.
    /// </summary>
    public static class MvcUtilities
    {
        /// <summary>
        /// Converts a <see cref="ModelStateDictionary"/> into a string array.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <returns>A string array containing the errors.</returns>
        public static string[] ToStringArray(this ModelStateDictionary modelState)
        {
            return modelState
                .Values
                .Where(v => v.Errors.Count > 0)
                .SelectMany(ms => ms.Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();
        }

        /// <summary>
        /// Converts a collection of uploaded files to a list of filename / byte array combinations.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>A list of uploaded files.</returns>
        public static IEnumerable<(string, byte[])> Convert(this ICollection<IFormFile> files)
        {
            var result = new List<(string, byte[])>();
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var stream = file.OpenReadStream();
                        stream.CopyTo(memoryStream);
                        var contents = memoryStream.ToArray();
                        result.Add((file.FileName, contents));
                    }
                }
            }

            return result;
        }
    }
}
