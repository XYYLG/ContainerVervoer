namespace ContainerVervoer.Classes
{
    public class Stack
    {
        private List<Container> Containers = new List<Container>();

        public bool canSupportWeight (Container container)
        {
            if (Containers.Count < 0)
            {
                 if(CalculateTotalWeight() - Containers[0].Weight  >120)
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

        //trytoadd anmaken
    }
}
