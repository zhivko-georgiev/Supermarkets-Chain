namespace CustomExtensions
{
    public static class StringExtension
    {
        public static string MysqlEscape(this string value)
        {
            return value.Replace("'", @"\'");
        }
    }
}