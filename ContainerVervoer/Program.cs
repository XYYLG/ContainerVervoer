using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 6;
int width = 5;

Ship shipOne = new Ship(length, width);
Random rand = new Random();

List<Container> containers = new List<Container>();

for (int i = 0; i < 5; i++) //koeling containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), false, true));
}
for (int i = 0; i < 10; i++) //koeling & waardevolle containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), true, true));
}

for (int i = 0; i < 15; i++) //normale containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), false, false));
}

for (int i = 0; i < 10; i++) //waardevolle containers
{
    containers.Add(new Container(rand.Next(Container.EmptyWeight, Container.MaxWeight), true, false));
}




foreach (Container container in containers)
{
    shipOne.TryToAddContainer(container);
    shipOne.IsProperlyLoaded();
}

UrlGenerator urlGen = new UrlGenerator();
string url = urlGen.GetUrl(shipOne);
Process.Start(new ProcessStartInfo() //Opent link automatisch
{
    FileName = url,
    UseShellExecute = true
});
Console.WriteLine(url);
Console.ReadLine();