using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobSearch.APP.Utility.Converters
{
    public class TextToDoubleConverter
    {
        public static double ToDouble(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = RemoveExtraText(value);
                return Double.Parse(value);
            }
            else
            {
                return 0;
            }
        }

        private static string RemoveExtraText(string value)
        {
            var numeros = "01234567890.,";
            return new string(value.Where(c => numeros.Contains(c)).ToArray());
        }
    }
}
