using System.Numerics;

namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainers(int count)
        {
            List<Container> containers = new List<Container>();

            for (int i = 0; i < count; i++)
            {
                int randomWeight = rand.Next(Container.EmptyWeight, Container.MaxWeight);
                bool isValuable = rand.Next(0, 40) == 1; // Randomly true or false
                bool needsCooling = rand.Next(0, 50) == 1; // Randomly true or false

                containers.Add(new Container(randomWeight, isValuable, needsCooling));
            }

            containers = containers
                .OrderByDescending(c => c.NeedsCooling)
                .ThenByDescending(c => c.IsValuable)
                .ThenByDescending(c => c.Weight)
                .ToList();

            return containers;
        }
    }
}
