using System.Collections.ObjectModel;

namespace ContainerVervoer.Classes
{
    public class Stack
    {
        private List<Container> _containers = new List<Container>();
        public ReadOnlyCollection<Container> Containers => _containers.AsReadOnly();
        public bool IsCooled { get; set; }
        public bool HasValuable => Containers.Any(container => container.IsValuable);
        public static readonly int StackCapacity = 120;

        public Stack(bool isCooled)
        {
            IsCooled = isCooled;
        }

        public bool CanSupportWeight(Container container)
        {
            if (Containers.Count > 0)
            {
                int totalWeight = CalculateTotalWeight();
                if (totalWeight - Containers[0].Weight + container.Weight <= StackCapacity)
                {
                    return true;
                }
            }
            else
            {
                if (container.Weight <= StackCapacity)
                {
                    return true;
                }
            }

            return false;
        }

        public int CalculateTotalWeight()
        { 
            int totalWeight = 0;
            foreach (Container container in Containers)
            {
                totalWeight += container.Weight;
            }

            return totalWeight;
        }

        public bool TryToAddContainer(Container container)
        {
            if (container.NeedsCooling && !IsCooled)
            {
                return false;
            }

            if (HasValuable)
            {
                return false;
            }

            if (CanSupportWeight(container))
            {
                _containers.Add(container);
                return true;
            }

            return false;
        }

        public bool TryToRemoveContainer(Container container)
        {
            return _containers.Remove(container);
        }
    }
}
