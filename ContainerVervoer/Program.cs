using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 2;
int width = 2;

Ship shipOne = new Ship(length, width);
Random rand = new Random();
int containerID = 0;


List<Container> containers = new List<Container>();

for (int i = 0; i < 500; i++)
{
    containers.Add(new Container(containerID, rand.Next(4000, 30000), false, false));
    containerID++;
}
for (int i = 0; i < 50; i++)
{
    containers.Add(new Container(containerID, rand.Next(4000, 30000), true, false));
    containerID++;
}


foreach (Container container in containers)
{
    shipOne.TryToAddContainer(container);
}

int totalWeight = shipOne.CalculateTotalWeight();

UrlGenerator urlGen = new UrlGenerator();
string url = urlGen.GetUrl(shipOne);
Process.Start(new ProcessStartInfo() //Opent link automatisch
{
    FileName = url,
    UseShellExecute = true
});
Console.WriteLine(url);
Console.ReadLine();