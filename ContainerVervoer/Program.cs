using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 4;
int width = 3;

Ship shipOne = new Ship(length, width);

List<Container> containers = new List<Container>();

containers.AddRange(ContainerFactory.CreateContainers(15, false, true));  // koeling containers
containers.AddRange(ContainerFactory.CreateContainers(15, true, true));  // koeling & waardevolle containers
containers.AddRange(ContainerFactory.CreateContainers(90, false, false)); // normale containers
containers.AddRange(ContainerFactory.CreateContainers(35, true, false)); // waardevolle containers





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
    //throw new Exception("Gewicht is te laag.", ex);
}
try
{
    shipOne.IsBalanced();
}
catch (Exception ex)
{
    //throw new Exception("Het gewicht is niet eerlijk verdeel", ex);
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