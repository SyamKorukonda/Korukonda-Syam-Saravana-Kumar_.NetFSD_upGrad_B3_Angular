
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem1
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();

            //Write a LINQ query to search and display all products with category “FMCG”.

            var result = products.Where(p => p.ProCategory == "FMCG").ToList();

            /* or 
             var result = from p in products
                             where p.ProCategory == "FMCG"
                             select p;
            */
            foreach (var item in result)
            {
                Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            }
            Console.WriteLine("-----------------products with category “Grain”.--------------------------");

            //Write a LINQ query to search and display all products with category “Grain”.
            var r2 = products.Where(p => p.ProCategory == "Grain").ToList();// ToList() methods is used to converts a collection result  into a List
            foreach (var i in r2)
            {
                Console.WriteLine($"{i.ProCode}\t{i.ProName}\t{i.ProMrp}");
            }


            //Write a LINQ query to sort products in ascending order by product code.
            Console.WriteLine("---------------sort products in ascending order by product code.------------------");
            var r3 = products.OrderBy(p => p.ProCode);
            foreach (var p in r3)
                Console.WriteLine($"{p.ProCode}\t{p.ProName}");

            //Write a LINQ query to sort products in ascending order by product Category.
            Console.WriteLine("------sort products in ascending order by product Category-----");
            var r4 = products.OrderBy(p => p.ProCategory);
            foreach (var p in r4)
                Console.WriteLine($"{p.ProCode}\t{p.ProName}\t{p.ProCategory}");

            //Write a LINQ query to sort products in ascending order by product Mrp.
            Console.WriteLine("------sort products in ascending order by product Mrp.--------");

            var r5 = products.OrderBy(p => p.ProMrp);
            foreach (var p in r5)
                Console.WriteLine($"{p.ProName}\t{p.ProMrp}");

            //Write a LINQ query to sort products in descending order by product Mrp.
            Console.WriteLine("---------sort products in descending order by product Mrp--------");

            var r6 = products.OrderByDescending(p => p.ProMrp);
            foreach (var p in r6)
                Console.WriteLine($"{p.ProName}\t{p.ProMrp}");
            //. Write a LINQ query to display products group by product Category
            Console.WriteLine("------display products group by product Category---------");
            var r7=products.GroupBy(p =>p.ProCategory).ToList();
            foreach (var group in r7)
            {
                Console.WriteLine(group.Key);
                foreach (var p in group)
                    Console.WriteLine("  " + p.ProName);
            }

            //. Write a LINQ query to display products group by product Mrp.
            Console.WriteLine("-----display products group by product Mrp.-----");
            var r8=products.GroupBy(p => p.ProMrp).ToList();
            foreach( var group in r8)
            {
                Console.WriteLine(group.Key);
                foreach(var p in group)
                    Console.WriteLine(" "+ p.ProName);
            }


            //Write a LINQ query to display product detail with highest price in FMCG category.
            Console.WriteLine("------product detail with highest price in FMCG category.------");
            var r9 = products
                .Where(p => p.ProCategory == "FMCG")
                .OrderByDescending(p => p.ProMrp)
                .FirstOrDefault();
            Console.WriteLine($"{r9.ProName}\t{r9.ProMrp}");

            //Write a LINQ query to display count of total products.
            Console.WriteLine("-----display count of total products.---");
            Console.WriteLine(products.Count);

            //Write a LINQ query to display count of total products with category FMCG
            Console.WriteLine("------count of total products with category FMCG----------");
            Console.WriteLine(products.Count(p => p.ProCategory == "FMCG"));

            //Write a LINQ query to display Max price.
            Console.WriteLine("---------display Max price--------");
            Console.WriteLine(products.Max(p => p.ProMrp));

            //Write a LINQ query to display Min price.
            Console.WriteLine("--------display Min price.-------");
            Console.WriteLine(products.Min(p => p.ProMrp));

            //Write a LINQ query to display whether all products are below Mrp Rs.30 or not.
            Console.WriteLine("------all products are below Mrp Rs.30 or not.----");
            Console.WriteLine(products.All(p => p.ProMrp < 30));

            //Write a LINQ query to display whether any products are below Mrp Rs.30 or not.
            Console.WriteLine("-----products are below Mrp Rs.30 or no-----");
            Console.WriteLine(products.Any(p => p.ProMrp < 30));


            Console.ReadLine();
        }
    }
}
