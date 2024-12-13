namespace ContainerVervoer.Classes
{
    public class Ship
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public List<Row> Rows { get; set; } = new List<Row>();
        public double MaximumWeight { get; set; }

        public Ship(int length, int width, double maximumWeight)
        {
            Length = length; 
            Width = width; 
            MaximumWeight = maximumWeight;
        }


    }
}
