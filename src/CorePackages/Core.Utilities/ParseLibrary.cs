using System.Globalization;

namespace Core.Utilities
{
    public static class ParseLibrary
    {
        public static DateTime TryDatetimeParse(this string dateString)
        {
            DateTime date;
            try
            {
                CultureInfo trTR = new CultureInfo("tr-TR");
                bool result = DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm:ss",
                        trTR,
                        System.Globalization.DateTimeStyles.None,
                        out date);
                if (!result)
                    date = DateTime.MinValue;

            }
            catch (Exception)
            {
                date = DateTime.MinValue;
            }

            return date;
        }

        public static int TryInt32Parse(this string intString)
        {
            int i;

            try
            {
                bool result = int.TryParse(intString, out i);
                if (!result)
                    i = 0;
            }
            catch (Exception)
            {
                i = 0;
            }

            return i;
        }
    }
}