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
            // File is taking file contents and turning it into string
            // DeserializeObject is taking string and turning it into thing
            var pizzas = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText(@"./pizzas.json"));

            // from list of pizzas select the toppings, join a comma in between them, and alphabetize it by itself
            var toppingLists = pizzas.Select(p => string.Join(",", p.Toppings.OrderBy(t => t)));

            // distinct linq method giving unique topping methods
            //var distinctToppingCombos = toppingLists.Distinct();

            // string is topping combination and int will be how many times combo has appeared
            var countOfCombos = new Dictionary<string, int>();
            foreach (var combination in toppingLists)
            {
                // loop over toppingLists and if it doesn't exist, initialize the combination to 1
                // else if the combination already exists with that key add 1 to it
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
            // start with the most ordered -> least ordered
            var mostOrdered = countOfCombos.OrderByDescending(item => item.Value).Take(20);

            foreach (var (combination, count)  in mostOrdered)
            {
                Console.WriteLine($"The topping combination of {combination} was ordered {count} times.");
            }

        }
    }
}
