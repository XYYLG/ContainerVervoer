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

        public bool TryToAddContainer(Container container)
        {
            int middleIndex = Width / 2;

            if (Width % 2 != 0)
            {
                if (_rows[middleIndex].TryToAddContainer(container))
                {
                    try
                    {
                        IsBalanced();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
                    }
                }
            }

            int leftWeight = CalculateLeftWeight();
            int rightWeight = CalculateRightWeight();

            if (leftWeight < rightWeight)
            {
                foreach (Row row in _rows.Take(middleIndex))
                {
                    if (row.TryToAddContainer(container))
                    {
                        try
                        {
                            IsBalanced();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
                        }
                    }
                }
            }
            else
            {
                foreach (Row row in _rows.Skip(middleIndex))
                {
                    if (row.TryToAddContainer(container))
                    {
                        try
                        {
                            IsBalanced();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
                        }
                    }
                }
            }

            return false;
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

        public void IsBalanced()
        {
            int totalWeight = CalculateTotalWeight();
            double difference = Math.Abs(CalculateLeftWeight() - CalculateRightWeight()) / (double)totalWeight * 100; // berekent het % verschil van links & rechts

            if (difference > 20)
            {
                throw new Exception("Het gewicht is niet eerlijk verdeeld");
            }
        }

        public int CalculateLeftWeight()
        {
            int leftWeight = 0;
            for (int i = 0; i < Width / 2; i++)
            {
                leftWeight += _rows[i].CalculateTotalWeight();
            }
            return leftWeight;
        }

        public int CalculateRightWeight()
        {
            int rightWeight = 0;
            for (int i = Width / 2; i < Width; i++)
            {
                rightWeight += _rows[i].CalculateTotalWeight();
            }
            return rightWeight;
        }
    }
}
