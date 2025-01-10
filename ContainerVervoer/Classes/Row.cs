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
                .OrderBy(tuple => tuple.stack.CalculateTotalWeight())
                .ToList();
            foreach (var (stack, index) in indexedStacks)
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
            //for (int i = 0; i < Stacks.Count; i++)
            //{
            //    Stack stack = Stacks[i];
            //    if (stack.TryToAddContainer(container))
            //    {
            //        if (!container.IsValuable)
            //        {
            //            if (IsPreviousAndNextReachable(i))
            //            {
            //                return true;
            //            }
            //            stack.TryToRemoveContainer(container);
            //        }
            //        else
            //        {
            //            if (IsStackReachable(i) && IsPreviousAndNextReachable(i))
            //            {
            //                return true;
            //            }
            //            stack.TryToRemoveContainer(container);
            //        }
            //    }
            //}
            //return false;

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

            int NextHeight = Stacks[index + 1].Containers.Count;
            int PreviousHeight = Stacks[index - 1].Containers.Count;

            return currentHeight > NextHeight || currentHeight > PreviousHeight;
        }

        public bool IsPreviousAndNextReachable(int index)
        {
            bool previousIsReachable = true;
            bool nextIsReachable = true;

            if (index - 1 > 0) //kijkt of groter is dan eerste stack
            {
                previousIsReachable = IsStackReachable(index - 1);
            }

            if (index + 1 < Stacks.Count - 1) //kijkt of kleiner is dan eerste stack
            {
                nextIsReachable = IsStackReachable(index + 1);
            }

            return previousIsReachable && nextIsReachable;
        }

        public bool TryToRemoveContainer(Container container)
        {
            foreach (Stack stack in Stacks)
            {
                if (stack.Containers.Remove(container))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
