namespace ContainerVervoer.Classes
{
    public class Container
    {
        public const int EmptyWeight = 4;
        public const int MaxWeight = 30;
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }
        public bool NeedsCooling { get; private set; }

        public Container(int weight, bool isValuable, bool needsCooling)
        {
            if (weight < EmptyWeight)
            {
                this.Weight = EmptyWeight;
            }
            else if (weight > MaxWeight)
            {
                this.Weight = MaxWeight;
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
