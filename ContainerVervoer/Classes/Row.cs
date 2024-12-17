namespace ContainerVervoer.Classes
{
    public class Row
    {
        private List<Stack> Stacks = new List<Stack>();
        private bool IsCoolingRow;
        private int width;

        public Row(bool isCoolingRow)
        {
            this.IsCoolingRow = isCoolingRow;
            for (int j = 0; j < width; j++)
            {
                Stacks.Add(new Stack());
            }
        }

        //public bool CanAddContainerToCoolingRow(Container container)
        //{
        //    if (!container.NeedsCooling) 
        //    { 
        //        return true;
        //    }
        //    if (container.NeedsCooling && IsCoolingRow) 
        //    { 
        //        return true; 
        //    }
        //    return false;
        //}

        //public bool AddContainerToStack(Stack stack, Container container) 
        //{ 
        //    return CanAddContainerToCoolingRow(container) && stack.canSupportWeight(container); 
        //}

        public int CalculateTotalWeight()
        {
            int totalWeight = 0;
            foreach (Stack stack in Stacks)
            {
                totalWeight += stack.CalculateTotalWeight();
            }
            return totalWeight;
        }
    }
}
