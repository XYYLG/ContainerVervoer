namespace ContainerVervoer.Classes
{
    public class Stack
    {
        public List<Container> Containers = new List<Container>(); 
        public bool IsCooled { get; set; }
        public bool HasValuable => Containers.Any(container => container.IsValuable);
        public static readonly int StackCapacity = 120;

        public Stack(bool isCooled)
        {
            IsCooled = isCooled;
        }

        public bool CanSupportWeight (Container container)
        {
            if (Containers.Count > 0)
            {
                 if(CalculateTotalWeight() - Containers[0].Weight + container.Weight > StackCapacity)
                 return false;
                
            }
            return true;
               
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

            if(CanSupportWeight(container))
            { 
                Containers.Add(container);
                return true;
            }
            return false;
        }

        public bool TryToRemoveContainer(Container container)
        {

            return Containers.Remove(container);
        }
    }
}
