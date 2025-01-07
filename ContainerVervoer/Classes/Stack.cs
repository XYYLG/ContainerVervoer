namespace ContainerVervoer.Classes
{
    public class Stack
    {
        public List<Container> Containers = new List<Container>();
        public bool IsCooled { get; set; }
        public bool HasValuable { get; private set; }
        private const int StackCapacity = 120;

        public Stack(bool isCooled)
        {
            IsCooled = isCooled;
        }

        public bool canSupportWeight (Container container)
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

            if(canSupportWeight(container))
            { 
                Containers.Add(container);
                if (container.IsValuable)
                {
                    HasValuable = true;
                }
                return true;
            }
            return false;
        }
    }
}
