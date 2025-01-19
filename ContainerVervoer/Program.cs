﻿using System;
using System.Diagnostics;
using ContainerVervoer.Classes;

int length = 10;
int width = 5;

Ship shipOne = new Ship(length, width);
Random random = new Random();
int randomCount = random.Next(100, 500);

List<Container> containers = ContainerFactory.CreateContainers(randomCount); 

// Verdeel containers in categorieën
List<Container> coolingContainers = containers.Where(c => c.NeedsCooling && !c.IsValuable).ToList();
List<Container> valuableCoolingContainers = containers.Where(c => c.NeedsCooling && c.IsValuable).ToList();
List<Container> normalContainers = containers.Where(c => !c.NeedsCooling && !c.IsValuable).ToList();
List<Container> valuableContainers = containers.Where(c => !c.NeedsCooling && c.IsValuable).ToList();

// Voeg containers in specifieke volgorde toe
List<Container> orderedContainers = new List<Container>();
orderedContainers.AddRange(coolingContainers);
orderedContainers.AddRange(valuableCoolingContainers);
orderedContainers.AddRange(normalContainers);
orderedContainers.AddRange(valuableContainers);

bool allContainersAdded = shipOne.TryToAddAllContainers(orderedContainers);

Console.WriteLine($"All containers added: {allContainersAdded}");

try
{
    if (!shipOne.IsProperlyLoaded())
    {
        throw new Exception("Gewicht is te laag.");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    if (!shipOne.IsBalanced())
    {
        throw new Exception("Het gewicht is niet eerlijk verdeel");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// Start URL-generator en open de link
UrlGenerator urlGen = new UrlGenerator();
string url = urlGen.GetUrl(shipOne);
Process.Start(new ProcessStartInfo() //Opent link automatisch
{
    FileName = url,
    UseShellExecute = true
});
Console.WriteLine(url);
Console.ReadLine();
