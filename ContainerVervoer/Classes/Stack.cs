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
            int totalWeight = CalculateTotalWeight();
            return totalWeight + container.Weight <= StackCapacity;
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
                Console.WriteLine($"Container needs cooling, but stack is not cooled.");
                return false;
            }

            if (HasValuable)
            {
                Console.WriteLine($"Stack already has a valuable container.");
                return false;
            }

            if (CanSupportWeight(container))
            {
                _containers.Add(container);
                Console.WriteLine($"Container added: Weight={container.Weight}, IsCooled={IsCooled}, HasValuable={HasValuable}");
                return true;
            }

            Console.WriteLine($"Container cannot be added due to weight.");
            return false;
        }


        public bool TryToRemoveContainer(Container container)
        {
            return _containers.Remove(container);
        }
    }
}
