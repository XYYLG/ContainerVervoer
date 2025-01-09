﻿using System.Collections.ObjectModel;

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
            int leftWeight = CalculateLeftWeight();
            int rightWeight = CalculateRightWeight();
            int middleWeight = CalculateMiddleWeight();
            int minWeight = Math.Min(leftWeight, Math.Min(rightWeight, middleWeight));
            int middleIndex = Width / 2;

            // Probeer de container toe te voegen aan de middelste rij als het een oneven breedte is en de middelste rij het minst zwaar is
            if (Width % 2 != 0 && middleWeight == minWeight)
            {
                if (_rows[middleIndex].TryToAddContainer(container)) 
                {
                    return true; 
                } 
            }
            // Voeg de container toe aan de rij met het minste gewicht
            if (leftWeight == minWeight)
            {
                foreach (Row row in _rows.Take(middleIndex))
                {
                    if (row.TryToAddContainer(container))
                    {
                        return true;
                    }
                }
            }
            else if (rightWeight == minWeight)
            {
                foreach (Row row in _rows.Skip(middleIndex + Width % 2))
                {
                    if (row.TryToAddContainer(container))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public bool LoadContainers(List<Container> containers) 
        { 
            foreach (Container container in containers)
            { 
                if (!TryToAddContainer(container)) 
                { 
                    throw new Exception("Kon container niet toevoegen");
                } 
            } 
            IsBalanced();
            return true; 
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
            for (int i = Width / 2 + Width % 2; i < Width; i++)
            {
                rightWeight += _rows[i].CalculateTotalWeight();
            }
            return rightWeight;
        }

        public int CalculateMiddleWeight()
        {
            int middleWeight = 0; 
            
            for (int i = Width / 2; i < Width / 2 + Width % 2; i++) 
            { 
                middleWeight += _rows[i].CalculateTotalWeight(); 
            }
            return middleWeight;
        }
    }
}
