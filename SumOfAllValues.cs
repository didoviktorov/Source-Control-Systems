namespace _13.SumOfAllValues
{
    using System;
    using System.Text.RegularExpressions;

    public class SumOfAllValues
    {
        static void Main(string[] args)
        {
            string keys = Console.ReadLine();
            string text = Console.ReadLine();

            string findKeys = @"^([A-Za-z_]+)[0-9]+.*?[0-9]([A-Za-z_]+)$";
            Regex regex = new Regex(findKeys);
            Match match = regex.Match(keys);

            if (match.Groups.Count != 3)
            {
                Console.WriteLine("<p>A key is missing</p>");
            }
            else
            {
                string startKey = match.Groups[1].Value;
                string endKey = match.Groups[2].Value;
                // Find even numbers like .01 which is like 0.01 in decimal ?!?!?!?
                string searchInText = startKey + @"([0-9]*.?[0-9]+)" + endKey;
                var matches = Regex.Matches(text, searchInText);
                if (matches.Count == 0)
                {
                    Console.WriteLine("<p>The total value is: <em>nothing</em></p>");
                }
                else
                {
                    decimal sum = 0;
                    foreach (Match match1 in matches)
                    {
                        sum += decimal.Parse(match1.Groups[1].ToString());
                    }

                    Console.WriteLine("<p>The total value is: <em>{0}</em></p>", sum);
                }
            }
        }
    }
}
