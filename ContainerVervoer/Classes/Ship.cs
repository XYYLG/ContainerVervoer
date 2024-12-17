namespace ContainerVervoer.Classes
{
    public class Ship
    {
        public int Length { get; set; }
        public int Width { get; set; }
        private List<Row> Rows = new List<Row>();
        private int MaximumWeight;

        public Ship(int length, int width, int maximumWeight)
        {
            Length = length;
            Width = width;
            MaximumWeight = maximumWeight;

            Rows.Add(new Row(true));
            for (int i = 1; i < length; i++)
            { 
                Rows.Add(new Row(false));               
            }
        }



        public int CalculateTotalWeight()
        {
            int totalWeight = 0;
            foreach (Row row in Rows)
            {
                totalWeight += row.CalculateTotalWeight();
            }
            return totalWeight;
        }

        public bool AddContainerToShip(Container container)
        {
            foreach (var row in Rows)
            {
                foreach (Stack stack in row.Stacks)
                {
                    if (row.CanAddContainerToCoolingRow(container) && stack.canSupportWeight(container))
                    {
                        stack.Containers.Add(container);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
