namespace ContainerVervoer.Classes
{
    public class Container
    {
        public static readonly int EmptyWeight = 4;
        public static readonly int MaxWeight = 30;
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }
        public bool NeedsCooling { get; private set; }

        public Container(int weight, bool isValuable, bool needsCooling)
        {
            if (weight < EmptyWeight)
            {
                throw new Exception("Gewicht kan niet minder zijn dan 4 ton");
            }
            else if (weight > MaxWeight)
            {
                throw new Exception("Gewicht kan niet meer zijn dan 30 ton");
            }
            else
            {
                this.Weight = weight;
            }
           
            this.IsValuable = isValuable;
            this.NeedsCooling = needsCooling;
        }
    }
}
