using System.Numerics;

namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainers(int count)
        {
            List<Container> containers = new List<Container>();
            int coolingCount = 0;
            int valuableCoolingCount = 0;
            int normalCount = 0;
            int valuableCount = 0;

            for (int i = 0; i < count; i++)
            {
                int randomWeight = rand.Next(Container.EmptyWeight, Container.MaxWeight);
                bool isValuable = rand.Next(0, 3) == 1; // Grotere kans op true
                bool needsCooling = rand.Next(0, 3) == 1; // Grotere kans op true

                Container container = new Container(randomWeight, isValuable, needsCooling);
                containers.Add(container);

                // Tel de categorieën
                if (needsCooling && isValuable)
                {
                    valuableCoolingCount++;
                }
                else if (needsCooling)
                {
                    coolingCount++;
                }
                else if (isValuable)
                {
                    valuableCount++;
                }
                else
                {
                    normalCount++;
                }
            }

            containers = containers
                .OrderByDescending(c => c.NeedsCooling)
                .ThenByDescending(c => c.IsValuable)
                .OrderByDescending(c => c.Weight)
                .ToList();

            // Print totalen per categorie
            Console.WriteLine($"Total Cooling Containers: {coolingCount}");
            Console.WriteLine($"Total Valuable Cooling Containers: {valuableCoolingCount}");
            Console.WriteLine($"Total Normal Containers: {normalCount}");
            Console.WriteLine($"Total Valuable Containers: {valuableCount}");
            Console.WriteLine($"Total Containers: {valuableCount + normalCount + valuableCoolingCount + coolingCount}");

            return containers;
        }
    }
}
