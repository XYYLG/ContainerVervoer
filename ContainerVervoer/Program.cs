using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 2;
int width = 2;

Ship shipOne = new Ship(length, width);


Container container1 = new Container(1, 2, false, true);
Container container2 = new Container(2, 1, true, false); 
Container container3 = new Container(3, 4, false, false);

int totalWeight = shipOne.CalculateTotalWeight();

shipOne.TryToAddContainer(container1);
shipOne.TryToAddContainer(container2);
shipOne.TryToAddContainer(container3);

UrlGenerator urlGen = new UrlGenerator();
string url = urlGen.GetUrl(shipOne);
Process.Start(new ProcessStartInfo() //Opent link automatisch
{
    FileName = url,
    UseShellExecute = true
});
Console.WriteLine(url);
Console.ReadLine();