using System;
using System.Text;

namespace ZhongTool.Common
{
    public class Tools
    {
        public static  string GetFormatText(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            //去除点
            input = input.Replace(".", "");
            string[] arrText = input.Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrText.Length <= 0)
            {
                return input;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var text in arrText)
            {
                sb.Append(UpperFirstCharacter(text));
            }

            return sb.ToString();
        }

        public static string UpperFirstCharacter(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            return s[0].ToString().ToUpper() + s.Substring(1, s.Length - 1).ToLower();
        }
    }
}