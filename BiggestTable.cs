namespace BiggestTableRow
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class BiggestTable
    {
        static void Main()
        {
            string pattern = @"^<tr><td>([A-Za-z]+)" + 
                @"<.+?>(-?[0-9]+.?[0-9]+|-|-?[0-9]+)" + 
                @"<.+?>(-?[0-9]+.?[0-9]+|-|-?[0-9]+)<.+?>" +
                @"(-?[0-9]+.?[0-9]+|-|-?[0-9]+)<\/td><\/tr>";
            Regex regex = new Regex(pattern);

            var rows = new Dictionary<string, List<string>>();
            string table = Console.ReadLine();
            while (table != "</table>")
            {
                var match = regex.Match(table);
                if (match.Success)
                {
                    string city = match.Groups[1].Value;
                    string store1 = match.Groups[2].Value;
                    string store2 = match.Groups[3].Value;
                    string store3 = match.Groups[4].Value;

                    if (!rows.ContainsKey(city))
                    {
                        rows[city] =  new List<string>();
                    }

                    if (store1 != "-")
                    {
                        rows[city].Add(store1);
                    }

                    if (store2 != "-")
                    {
                        rows[city].Add(store2);
                    }

                    if (store3 != "-")
                    {
                        rows[city].Add(store3);
                    }
                }

                table = Console.ReadLine();
            }

            if (rows.Values.All(v => v.Count == 0))
            {
                Console.WriteLine("no data");
            }
            else
            {
                var newRows = new Dictionary<decimal, string>();
                foreach (var key in rows)
                {
                    decimal sum = 0;
                    sum += key.Value.Sum(a => decimal.Parse(a));
                    if (!newRows.ContainsKey(sum))
                    {
                        newRows[sum] = string.Empty;
                        string equation = string.Empty;
                        if (key.Value.Count == 1)
                        {
                            equation += string.Join("", key.Value);
                            newRows[sum] += equation;
                        }
                        else
                        {
                            equation = string.Join(" + ", key.Value);
                            newRows[sum] += equation;
                        }
                    }
                }

                decimal maxKey = newRows.Max(k => k.Key);

                Console.WriteLine("{0:G29} = {1}", maxKey, newRows[maxKey]);
            }          
        }
    }
}
