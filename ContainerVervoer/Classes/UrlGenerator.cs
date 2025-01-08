using static System.Net.WebRequestMethods;

namespace ContainerVervoer.Classes
{
    public class UrlGenerator
    {
        public string GetUrl(Ship ship)
        {
            string url = "https://app6i872272.luna.fhict.nl/?";
            url += "length=" + ship.Length + "&width=" + ship.Width;

            string stacks = string.Empty;
            string weights = string.Empty;

            for (int i = 0; i < ship.Rows.Count; i++)
            {
                Row row = ship.Rows[i];

                if (i > 0)
                {
                    stacks += "/";
                    weights += "/";
                }

                for (int j = 0; j < row.Stacks.Count; j++)
                {
                    Stack stack = row.Stacks[j];

                    if (j > 0)
                    {
                        stacks += ",";
                        weights += ",";
                    }

                    for (int k = 0; k < stack.Containers.Count; k++)
                    {
                        Container container = stack.Containers[k];
                        if (k > 0)
                        {
                            stacks += "-";
                            weights += "-";
                        }

                        stacks += GetContainerType(container);
                        weights += container.Weight;
                    }
                }
            }

            url += "&stacks=" + stacks;
            url += "&weights=" + weights;

            return url;
        }

        public int GetContainerType(Container container)
        {
            if (container.IsValuable)
            {
                if (container.NeedsCooling)
                {
                    return 4;
                }
                return 2;
            }

            if (container.NeedsCooling)
            {
                return 3;
            }

            return 1;
        }
    }


}
