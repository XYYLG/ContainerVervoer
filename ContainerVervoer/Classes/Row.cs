namespace ContainerVervoer.Classes
{
    public class Row
    {
        public List<Stack> Stacks { get; set; } =new List<Stack>();
        public bool IsCoolingRow { get; set; }

        public Row(bool isCoolingRow) 
        { 
            this.IsCoolingRow = isCoolingRow; 
        }

        public bool CanAddContainerToCoolingRow(Container container)
        {
            if (!container.NeedsCooling) 
            { 
                return true;
            }
            if (container.NeedsCooling && IsCoolingRow) 
            { 
                return true; 
            }
            return false;
        }

        public bool AddContainerToStack(Stack stack, Container container) 
        { 
            return CanAddContainerToCoolingRow(container) && stack.canSupportWeight(container); 
        }
    }
}
