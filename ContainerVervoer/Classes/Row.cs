using System.ComponentModel;

namespace ContainerVervoer.Classes
{
    public class Row
    {
        public List<Stack> Stacks = new List<Stack>();
        //private int width;

        public Row(int length)
        {
            for (int j = 0; j < length; j++)
            {
                bool isCooled = j == 0; //kijkt of hij op de eerste plek in de rij is
                Stacks.Add(new Stack(isCooled));
            }
        }

        public int CalculateTotalWeight()
        {
            int totalWeight = 0;
            foreach (Stack stack in Stacks)
            {
                totalWeight += stack.CalculateTotalWeight();
            }
            return totalWeight;
        }

        public bool TryToAddContainer(Container container)
        {
            foreach (Stack stack in Stacks)
            {
                if (stack.TryToAddContainer(container))
                {
                    return true;

                }
            }
            return false;
        }
    }
}
