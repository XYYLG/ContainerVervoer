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

                // Verhoogde kans op waardevolle en gekoelde containers
                bool isValuable = rand.Next(0, 3) == 1; // Grotere kans op true
                bool needsCooling = rand.Next(0, 3) == 1; // Grotere kans op true

                containers.Add(new Container(randomWeight, isValuable, needsCooling));
            }

            // Sorteer containers
            containers = containers
                .OrderByDescending(c => c.NeedsCooling)
                .ThenByDescending(c => c.IsValuable)
                .ThenByDescending(c => c.Weight)
                .ToList();

            return containers;
        }
    }
}
