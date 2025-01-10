using System.Numerics;

namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainers(int count, bool isValuable, bool needsCooling, int weight = 0)
        {
            List<Container> containers = new List<Container>();

            for (int i = 0; i < count; i++)
            {
                int randomWeight = 0;
                if (weight < Container.EmptyWeight || weight > Container.MaxWeight)
                {
                   randomWeight  = rand.Next(Container.EmptyWeight, Container.MaxWeight);
                }
                else
                {
                    randomWeight = weight;
                }
                containers.Add(new Container(randomWeight, isValuable, needsCooling));
            }

            return containers.OrderByDescending(container => container.Weight).ToList();
        }
    }
}
