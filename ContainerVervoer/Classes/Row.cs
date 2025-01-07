using System.ComponentModel;

namespace ContainerVervoer.Classes
{
    public class Row
    {
        public List<Stack> Stacks = new List<Stack>();

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

        //checks whether the stack at this index is higher than the previous or the next stack
        public bool IsStackReachable(int index)
        {
            bool result = true;

            int currentHeight = Stacks[index].Containers.Count;
            if (currentHeight == 0)
            {
                return result;
            }

            int NextHeight = Stacks[index + 1].Containers.Count;
            int PreviousHeight = Stacks[index - 1].Containers.Count;

            result =  currentHeight > NextHeight || currentHeight < PreviousHeight;

            return result;
        }
    }
}
