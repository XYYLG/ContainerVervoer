using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 10;
int width = 5;

Ship shipOne = new Ship(length, width);

List<Container> containers = new List<Container>();

containers.AddRange(ContainerFactory.CreateContainers(20, false, true, Container.MaxWeight));  // koeling containers
containers.AddRange(ContainerFactory.CreateContainers(5, true, true, Container.MaxWeight));  // koeling & waardevolle containers
containers.AddRange(ContainerFactory.CreateContainers(150, false, false, Container.MaxWeight)); // normale containers
containers.AddRange(ContainerFactory.CreateContainers(30, true, false, Container.MaxWeight)); // waardevolle containers

foreach (Container container in containers)
{
    shipOne.TryToAddContainer(container);
}

try
{
    shipOne.IsProperlyLoaded();
}
catch (Exception ex)
{
    throw new Exception("Gewicht is te laag.", ex);
}
try
{
    shipOne.IsBalanced();
}
catch (Exception ex)
{
    throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
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