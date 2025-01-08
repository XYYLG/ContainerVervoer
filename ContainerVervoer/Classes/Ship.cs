using System.Collections.ObjectModel;

namespace ContainerVervoer.Classes
{
    public class Ship
    {
        public int Length { get; set; }
        public int Width { get; set; }
        private List<Row> _rows = new List<Row>();
        public ReadOnlyCollection<Row> Rows => _rows.AsReadOnly();


        public Ship(int length, int width)
        {
            Length = length;
            Width = width;

            for (int i = 0; i < width; i++) //rij aanmaken
            {
                _rows.Add(new Row(length));
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


        public void IsProperlyLoaded()
        {
            int maxWeight = Length * Width * (Stack.StackCapacity + Container.MaxWeight);
            int totalWeight = CalculateTotalWeight();
            if (totalWeight <= 0.5 * maxWeight)
            {
                throw new Exception("Het gewicht is te laag");
            }

        }

        public bool TryToAddContainer(Container container)
        {
            foreach (Row row in Rows)
            {
                if (row.TryToAddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
