using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VLibraries.SharedCode.Extensions
{
    public static class PostcodeExtensions
    {
        private const string postcodeRegex = "([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})";

        public static string SplitPostcode(this string postcode)
        {
            Regex rgx = new Regex(postcodeRegex);

            string firstHalf = rgx.Split(postcode)[4]; //#4 Is always the first half of postcode 

            string secondHalf = postcode.Replace(firstHalf, "");

            string splitPostcode = $"{firstHalf} {secondHalf}";

            return splitPostcode;
        }

        public static bool IsPostcode(this string input)
        {
            Regex rgx = new Regex(postcodeRegex);

            return rgx.IsMatch(input);
        }
    }
}
