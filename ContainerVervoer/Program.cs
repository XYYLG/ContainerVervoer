using System;
using ContainerVervoer.Classes;

int length = 5;
int width = 6;
int maximumWeight = 4500;

Ship shipOne = new Ship(length, width, maximumWeight);


Container container1 = new Container(1, 2, false, true);
Container container2 = new Container(2, 1, true, false); 
Container container3 = new Container(3, 4, false, false);

int totalWeight = shipOne.CalculateTotalWeight();

shipOne.AddContainerToShip(container1);
shipOne.AddContainerToShip(container2);
shipOne.AddContainerToShip(container3);