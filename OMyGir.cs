namespace _20.OMyGirl_
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class OMyGir
    {
        static void Main()
        {
            string keyValue = Console.ReadLine();
            string key = string.Empty;
            char[] specialSymbols = { '[', ']', '|', '*', '.', '+', '?', '(', ')', '{', '}', '\\', '^', '$' };

            if (keyValue[0].Equals(specialSymbols.FirstOrDefault(s => s == keyValue[0])))
            {
                key += string.Format("\\{0}", keyValue[0]);
            }
            else 
            {
                key += keyValue[0]; 
            }

            for (int index = 1; index < keyValue.Length - 1; index++)
            {
                char symbol = keyValue[index];
                if (char.IsDigit(symbol))
                {
                    string digit = @"[0-9]*?";
                    key += digit;
                }
                else if(char.IsUpper(symbol))
                {
                    string upper = @"[A-Z]*?";
                    key += upper;
                }
                else if (char.IsLower(symbol))
                {
                    string lower = @"[a-z]*?";
                    key += lower;
                }
                else
                {
                    if (symbol.Equals(specialSymbols.FirstOrDefault(s => s == symbol)))
                    {
                        key += string.Format("\\{0}", symbol);
                    }
                    else
                    {
                        key += symbol;
                    }                    
                }
            }

            if (keyValue[keyValue.Length - 1].Equals(specialSymbols.FirstOrDefault(s => s == keyValue[keyValue.Length - 1])))
            {
                key += string.Format("\\{0}+", keyValue[keyValue.Length - 1]);
            }
            else
            {
                key += keyValue[keyValue.Length - 1] + "+";
            }

            string line = Console.ReadLine();
            string text = string.Empty;
            while (line != "END")
            {
                text += line;

                line = Console.ReadLine();
            }

            string pattern = key + @"(\w+?.+?)" + key;
            MatchCollection matches = Regex.Matches(text, pattern);
            string address = string.Empty;
            foreach (Match match in matches)
            {
                address += match.Groups[1].Value;
            }

            Console.WriteLine(address);
        }
    }
}
