namespace ContainerVervoer.Classes
{
    public class Stack
    {
        public List<Container> Containers = new List<Container>();
        public bool IsCooled { get; set; }

        public Stack(bool isCooled)
        {
            IsCooled = isCooled;
        }

        public bool canSupportWeight (Container container)
        {
            if (Containers.Count < 0)
            {
                 if(CalculateTotalWeight() - Containers[0].Weight >120)
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
            if (container.NeedsCooling && !this.IsCooled)
            { 
                return false; 
            }
            if(canSupportWeight(container) == true)
            { 
                Containers.Add(container);
                return true;
            }
            return false;
        }
    }
}
