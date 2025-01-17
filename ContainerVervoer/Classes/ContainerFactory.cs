using System.Numerics;

namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainers(int count)
        {
            List<Container> containers = new List<Container>();

            int weigth = (Container.EmptyWeight + Container.MaxWeight) / 2;

            for (int i = 0; i < count; i++)
            {
                int randomWeight = rand.Next(weigth, Container.MaxWeight);
                bool isValuable = rand.Next(0, 40) == 1; // Randomly true or false
                bool needsCooling = rand.Next(0, 50) == 1; // Randomly true or false

                containers.Add(new Container(randomWeight, isValuable, needsCooling));
            }

            return containers.OrderByDescending(container => container.Weight).ToList();
        }
    }
}
