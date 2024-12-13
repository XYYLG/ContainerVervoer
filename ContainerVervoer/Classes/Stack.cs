namespace ContainerVervoer.Classes
{
    public class Stack
    {
        public List<Container> Containers { get; set; } = new List<Container>();
        public bool canSupportWeight (Container container)
        {
            int totalWeight = container.Weight; 
            foreach (Container containerAdded in Containers) 
            { 
                totalWeight += containerAdded.Weight; //voeg toe aan
            }
            if (totalWeight <= 120000) 
            { 
                Containers.Add(container); 
                return true;
            } 
            return false;
        }
    }
}
