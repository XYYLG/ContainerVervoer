using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 5;
int width = 1;

Ship shipOne = new Ship(length, width);
Random rand = new Random();
int containerID = 0;


List<Container> containers = new List<Container>();

for (int i = 0; i < 0; i++) //koeling containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), false, true));
    containerID++;
}
for (int i = 0; i < 0; i++) //koeling & waardevolle containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), true, true));
    containerID++;
}

for (int i = 0; i < 0; i++) //normale containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), false, false));
    containerID++;
}

for (int i = 0; i < 4; i++) //waardevolle containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), true, false));
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