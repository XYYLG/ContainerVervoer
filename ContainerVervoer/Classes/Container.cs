namespace ContainerVervoer.Classes
{
    public class Container
    {
        private const int emptyw = 4;
        private const int maxw = 30;
        public int Id { get; private set; }
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }
        public bool NeedsCooling { get; private set; }

        public Container(int id, int weight, bool isValuable, bool needsCooling)
        {
            this.Id = id;
            this.Weight = weight;
            this.IsValuable = isValuable;
            this.NeedsCooling = needsCooling;
        }
    }
}
