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

                // Verlaagde kans op normale containers
                bool isValuable = rand.Next(0, 2) == 1; // 50% kans op waardevol
                bool needsCooling = rand.Next(0, 2) == 1; // 50% kans op koeling

                // Als de container niet waardevol of gekoeld is, heeft het 25% kans om normaal te zijn
                if (!isValuable && !needsCooling && rand.Next(0, 4) != 0)
                {
                    isValuable = rand.Next(0, 2) == 1; // 50% kans op waardevol als niet gekoeld
                    needsCooling = rand.Next(0, 2) == 1; // 50% kans op koeling als niet waardevol
                }

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
