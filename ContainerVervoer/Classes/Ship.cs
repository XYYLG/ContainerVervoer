using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;

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
            List<Row> leftRows = new List<Row>();
            Row? middleRow = null;
            List<Row> rightRows = new List<Row>();

            for (int i = 0; i < Width / 2; i++)
            {
                leftRows.Add(Rows[i]);
            }

            for (int i = Width / 2 + Width % 2; i < Width; i++)
            {
                rightRows.Add(Rows[i]);
            }

            if (Width % 2 != 0)
            {
                middleRow = Rows[Width / 2];

                Row leftMiddleRow = new Row(Length/2);
                Row rightMiddleRow = new Row(Length / 2 + Length % 2);
                for (int i = 0; i < Length/2; i++)
                {
                    leftMiddleRow.Stacks[i] = middleRow.Stacks[i];
                }

                leftRows.Add(leftMiddleRow);
                

                for (int i = 0; i < Length/2+Length%2; i++)
                {
                    rightMiddleRow.Stacks[i] = middleRow.Stacks[Length / 2 + i];
                }

                rightRows.Add(rightMiddleRow);
            }


            int leftWeight = CalculateLeftWeight(); 
            int rightWeight = CalculateRightWeight();

            if (leftWeight < rightWeight)
            {
                foreach (Row left in leftRows)
                {
                    if (left.TryToAddContainer(container))
                    {
                        return true;
                    }
                    
                }
            }
            else
            {
                foreach (Row right in rightRows)
                {
                    if (right.TryToAddContainer(container))
                    {
                        return true;
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
            for (int i = Width / 2 + Width % 2; i < Width; i++)
            {
                rightWeight += _rows[i].CalculateTotalWeight();
            }
            return rightWeight;
        }
    }
}
