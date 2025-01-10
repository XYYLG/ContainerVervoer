namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        private static readonly Random rand = new Random();

        public static List<Container> CreateContainers(int count, bool isValuable, bool needsCooling)
        {
            List<Container> containers = new List<Container>();

            for (int i = 0; i < count; i++)
            {
                int weight = rand.Next(Container.EmptyWeight, Container.MaxWeight);
                containers.Add(new Container(weight, isValuable, needsCooling));
            }

            return containers;
        }
    }
}
