namespace ContainerVervoer.Classes
{
    public class ContainerFactory
    {
        public static List<Container> CreateContainer(int count, int weight, bool isValuable, bool needsCooling)
        {
            List<Container> containers = new List<Container>();

            // Create the specified number of containers
            for (int i = 0; i < count; i++)
            {
                containers.Add(new Container(weight, isValuable, needsCooling));
            }

            return containers;
        }
    }
}
