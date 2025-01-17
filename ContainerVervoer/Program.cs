using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 10;
int width = 5;

Ship shipOne = new Ship(length, width);

List<Container> containers = new List<Container>();
Random random = new Random();
int randomCount = random.Next(50, 500);

containers.AddRange(ContainerFactory.CreateContainers(randomCount));

foreach (Container container in containers) 
{
    shipOne.TryToAddContainer(container);
}

try
{
    if (!shipOne.IsProperlyLoaded())
    {
        throw new Exception();
    }
}
catch (Exception ex)
{
    throw new Exception("Gewicht is te laag.", ex);
}
try
{
    if (!shipOne.IsBalanced())
    {
        throw new Exception();
    }
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