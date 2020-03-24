using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FavPizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            // using newtonsoft.json to take json files and turn it into c#.
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            // give list of toppings and order it alphabetically
            var toppingLists = pizzas.Select(p => string.Join(",", p.Toppings.OrderBy(t => t)));

            // distinct linq method giving unique topping methods
            //var distinctToppingCombos = toppingLists.Distinct();

            var countOfCombos = new Dictionary<string, int>();
            foreach (var combination in toppingLists)
            {
                // loop over combos and if it doesn't contain the combination have it as 1
                // but if the combination already exists add 1 more to it
                if (!countOfCombos.ContainsKey(combination))
                {
                    countOfCombos.Add(combination, 1);
                }
                else
                {
                    countOfCombos[combination] += 1;
                }
            }

            // order it by descending based on the number of times it was ordered and Take the first 20
            var mostOrdered = countOfCombos.OrderByDescending(item => item.Value).Take(20);

            foreach (var (combination, count)  in mostOrdered)
            {
                Console.WriteLine($"The topping combination of {combination} was ordered {count} times.");
            }

        }
    }
}
