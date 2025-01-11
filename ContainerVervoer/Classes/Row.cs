using System.Reflection;
using System;

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
            List<(Stack stack, int index)> indexedStacks = Stacks.Select((stack, index) => (stack, index))
                .OrderBy(tuple => tuple.stack.CalculateTotalWeight()) // bakje met meerdere objecten
                .ToList();

            foreach ((Stack stack, int index) in indexedStacks)
            {
                if (stack.TryToAddContainer(container))
                {
                    if (!container.IsValuable)
                    {
                        if (IsPreviousAndNextReachable(index))
                        {
                            return true;
                        }
                        stack.TryToRemoveContainer(container);
                    }
                    else
                    {
                        if (IsStackReachable(index) && IsPreviousAndNextReachable(index))
                        {
                            return true;
                        }
                        stack.TryToRemoveContainer(container);
                    }
                }
            }
            return false;
        }

        public bool IsStackReachable(int index)
        {
            if (index == 0 || index == Stacks.Count - 1)
            {
                return true;
            }

            int currentHeight = Stacks[index].Containers.Count;

            if (currentHeight == 0)
            {
                return true;
            }

            if (!Stacks[index].HasValuable)
            {
                return true;
            }

            int nextHeight = Stacks[index + 1].Containers.Count;
            int previousHeight = Stacks[index - 1].Containers.Count;

            return currentHeight <= nextHeight && currentHeight <= previousHeight;
        }


        public bool IsPreviousAndNextReachable(int index)
        {
            bool previousIsReachable = true;
            bool nextIsReachable = true;

            if (index - 1 >= 0) // Controleren of groter of gelijk aan 0
            {
                previousIsReachable = IsStackReachable(index - 1);
            }

            if (index + 1 < Stacks.Count) // Controleren of kleiner dan het aantal stapels
            {
                nextIsReachable = IsStackReachable(index + 1);
            }

            return previousIsReachable && nextIsReachable;
        }

        //public bool TryToRemoveContainer(Container container)
        //{
        //    foreach (Stack stack in Stacks)
        //    {
        //        if (stack.Containers.Remove(container))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
