using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UserHub.Model.Helpers
{
    /// <summary>
    /// tá duplicado mesmo, vamos decidir como resolver esse problema, sei lá, criando um projeto pra pasta Code.
    /// </summary>
    internal static class StringHelper
    {
        public static string GetTagName(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            var tag = RemoveDiacritics(text).ToLower();

            tag = tag.Replace("_", " ");
            tag = new string(tag.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))).ToArray());
            tag = tag.Trim().Replace(" ", "-");

            while (tag.Contains("--"))
                tag = tag.Replace("--", "-");

            return tag;
        }

        public static string RemoveDiacritics(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Retorna o texto passado, porém com a primeira letra sempre em maiúscula
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            if (input == null) throw new ArgumentNullException("input");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
    }
}
