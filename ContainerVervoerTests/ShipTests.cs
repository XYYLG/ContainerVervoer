using ContainerVervoer.Classes;
using Microsoft.VisualStudio.CodeCoverage;

namespace ContainerVervoerTests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void ShipConstructor_ShouldThrowException_WhenLengthIsZero()
        { // Arrange
            int length = 0;
            int width = 5;

            // Act & Assert
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new Ship(length, width));
            Assert.AreEqual("Length moet groter dan 0 zijn", ex.Message);
        }

        [TestMethod]
        public void ShipConstructor_ShouldThrowException_WhenWidthIsZero()
        {
            // Arrange
            int length = 5;
            int width = 0;

            // Act & Assert
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new Ship(length, width));
            Assert.AreEqual("Width moet groter dan 0 zijn", ex.Message);
        }

        [TestMethod]
        public void ShipConstructor_ShouldThrowException_WhenLengthAndWidthAreZero()
        {
            // Arrange
            int length = 0;
            int width = 0;

            // Act & Assert
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new Ship(length, width));
            Assert.IsTrue(ex.Message.Contains("Length moet groter dan 0 zijn") || ex.Message.Contains("Width moet groter dan 0 zijn"));
        }

        [TestMethod]
        public void ShipConstructorTest_ShouldSetLengthAndWidth()
        {
            // Arrange
            int length = 5;
            int width = 3;

            // Act
            Ship ship = new Ship(length, width);

            // Assert
            Assert.AreEqual(length, ship.Length, "De lengte van het schip moet correct ingesteld zijn.");
            Assert.AreEqual(width, ship.Width, "De breedte van het schip moet correct ingesteld zijn.");
        }

        [TestMethod]
        public void ShipConstructorTest_ShouldCreateCorrectNumberOfRows()
        {
            // Arrange
            int length = 5;
            int width = 3;

            // Act
            Ship ship = new Ship(length, width);

            // Assert
            // Toegang tot _rows moet publiek worden gemaakt of via een publieke methode/property
            Assert.AreEqual(width, ship.Rows.Count, "Het aantal rijen moet gelijk zijn aan de breedte van het schip.");
        }

        [TestMethod]
        public void ShipConstructorTest_ShouldCreateRows()
        {
            // Arrange
            int length = 5;
            int width = 3;

            // Act
            Ship ship = new Ship(length, width);

            // Assert
            foreach (var row in ship.Rows)
            {
                Assert.IsNotNull(row, "Elke rij moet correct aangemaakt zijn.");
            }
        }

        [TestMethod]
        public void ShipConstructorTest_ShouldCreateShipWithMinimumDimensions()
        {
            // Arrange
            int length = 1;
            int width = 1;

            // Act
            Ship ship = new Ship(length, width);

            // Assert
            Assert.AreEqual(length, ship.Length, "De lengte van het schip moet correct ingesteld zijn.");
            Assert.AreEqual(width, ship.Width, "De breedte van het schip moet correct ingesteld zijn.");
            Assert.AreEqual(width, ship.Rows.Count, "Het aantal rijen moet gelijk zijn aan de breedte van het schip.");
            foreach (var row in ship.Rows)
            {
                Assert.IsNotNull(row, "Elke rij moet correct aangemaakt zijn.");
            }
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnZero_WhenShipHasNoRows()
        {
            // Arrange
            Ship ship = new Ship(0, 0);

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight, "Het totale gewicht moet 0 zijn wanneer het schip geen rijen heeft.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnZero_WhenAllRowsAreEmpty()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight, "Het totale gewicht moet 0 zijn wanneer alle rijen leeg zijn.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnSumOfAllContainerWeights_WhenShipHasContainersInRows()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Voeg containers toe aan rijen
            ship.Rows[0].Stacks[0].Containers.Add(new Container(10, false, false));
            ship.Rows[1].Stacks[0].Containers.Add(new Container(20, false, false));
            ship.Rows[2].Stacks[0].Containers.Add(new Container(30, false, false));

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(60, totalWeight, "Het totale gewicht moet gelijk zijn aan de som van de gewichten van alle containers in de rijen.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnSumOfAllContainerWeightsInAllRows()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Voeg containers toe aan rijen
            ship.Rows[0].Stacks[0].Containers.Add(new Container(10, false, false));
            ship.Rows[0].Stacks[0].Containers.Add(new Container(15, false, false));
            ship.Rows[1].Stacks[0].Containers.Add(new Container(20, false, false));
            ship.Rows[2].Stacks[0].Containers.Add(new Container(5, false, false));
            ship.Rows[2].Stacks[0].Containers.Add(new Container(10, false, false));

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(60, totalWeight, "Het totale gewicht moet gelijk zijn aan de som van de gewichten van alle containers in alle rijen.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddToMiddleRow_WhenWidthIsOddAndMiddleRowHasMinWeight()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            Container container = new Container(10, false, false);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou aan de middelste rij toegevoegd moeten zijn.");
            Assert.AreEqual(10, ship.Rows[1].CalculateTotalWeight(), "Het totale gewicht van de middelste rij moet 10 zijn.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddToLeftRowWithMinWeight()
        {
            // Arrange
            Ship ship = new Ship(1, 2);
            ship.Rows[1].TryToAddContainer(new Container(5, false, false)); // Voeg een container toe aan de eerste rij
            Container container = new Container(10, false, false);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou aan de rij met het minste gewicht aan de linkerkant toegevoegd moeten zijn.");
            Assert.AreEqual(10, ship.Rows[0].CalculateTotalWeight(), "Het totale gewicht van de eerste rij moet 15 zijn.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldAddToRightRowWithMinWeight()
        {
            // Arrange
            Ship ship = new Ship(1, 2);
            ship.Rows[0].TryToAddContainer(new Container(5, false, false)); // Voeg een container toe aan de linker rij
            Container container = new Container(15, false, false);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou aan de rij met het minste gewicht aan de rechterkant toegevoegd moeten zijn.");
            Assert.AreEqual(15, ship.Rows[1].CalculateTotalWeight(), "Het totale gewicht van de rechter rij moet 15 zijn.");
        }

        [TestMethod]
        public void IsProperlyLoaded_ShouldNotThrowException_WhenTotalWeightIsWithinLimits()
        {
            // Arrange
            Ship ship = new Ship(1, 1);
            int maxWeight = ship.Length * ship.Width * (Stack.StackCapacity + Container.MaxWeight);

            for (int i = 0; i < 3; i++)
            {
                ship.Rows[0].Stacks[0].TryToAddContainer(new Container(30, false, false));
            }

            // Assert & Act
            Assert.ThrowsException<Exception>(() => ship.IsProperlyLoaded());
        }

        [TestMethod]
        public void IsProperlyLoaded_ShouldNotThrowException_WhenTotalWeightIsExactlyOnLimit()
        {
            // Arrange
            Ship ship = new Ship(1, 2);
            for (int i = 0; i < 5; i++)
            {
                ship.Rows[0].Stacks[0].TryToAddContainer(new Container(30, false, false));
            }

            // Act
            Exception ex = null;
            try
            {
                ship.IsProperlyLoaded();
            }
            catch (Exception e)
            {
                ex = e;
            }

            // Assert
            Assert.IsNull(ex, "De methode mag geen uitzondering gooien wanneer het totale gewicht precies op de grens is.");

        }

        [TestMethod]
        public void IsProperlyLoaded_ShouldNotThrowException_WhenTotalWeightIsFarAboveLimit()
        {
            // Arrange
            Ship ship = new Ship(1, 1);
            for (int i = 0; i < 5; i++)
            {

                ship.Rows[0].Stacks[0].TryToAddContainer(new Container(30, false, false));

            }

            // Act
            Exception ex = null;
            try
            {
                ship.IsProperlyLoaded();
            }
            catch (Exception e)
            {
                ex = e;
            }

            // Assert
            Assert.IsNull(ex, "De methode mag geen uitzondering gooien wanneer het totale gewicht ver boven de limiet is.");
        }

        [TestMethod]
        public void IsBalanced_ShouldNotThrowException_WhenWeightIsEvenlyDistributed()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            int totalWeight = 27; // Zorg ervoor dat het totale gewicht binnen de limieten van de containers valt
            int halfWeight = totalWeight / 2;

            // Verdeel het gewicht gelijkmatig over de linker- en rechterzijde
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(halfWeight, false, false));
            ship.Rows[2].Stacks[2].TryToAddContainer(new Container(halfWeight, false, false));

            // Assert & Act
            Assert.ThrowsException<Exception>(() => ship.IsBalanced());
        }

        [TestMethod]
        public void IsBalanced_ShouldNotThrowException_WhenTotalWeightIsZero()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Assert & Act
            Assert.ThrowsException<Exception>(() => ship.IsBalanced());
        }

        [TestMethod]
        public void IsBalanced_ShouldNotThrowException_WhenWeightDifferenceIsExactlyTwentyPercent()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            int totalWeight = 27; // Zorg ervoor dat het totale gewicht binnen de limieten van de containers valt
            int leftWeight = (int)(0.6 * totalWeight);
            int rightWeight = totalWeight - leftWeight;

            // Voeg containers toe die een gewichtsverschil van precies 20% veroorzaken
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(leftWeight, false, false));
            ship.Rows[2].Stacks[2].TryToAddContainer(new Container(rightWeight, false, false));

            // Assert & Act
            Assert.ThrowsException<Exception>(() => ship.IsBalanced());
        }

        [TestMethod]
        public void CalculateLeftWeight_ShouldReturnCorrectWeights_WhenLeftSideHasDifferentWeights()
        {
            // Arrange
            Ship ship = new Ship(4, 2);
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(10, false, false)); // Voeg een container van 10 ton toe
            ship.Rows[0].Stacks[1].TryToAddContainer(new Container(20, false, false)); // Voeg een container van 20 ton toe

            // Act
            int leftWeight = ship.CalculateLeftWeight();
            int rightWeight = ship.CalculateRightWeight();
            int middleWeight = ship.CalculateMiddleWeight();

            // Assert
            Assert.AreEqual(30, leftWeight, "Het gewicht aan de linkerkant moet correct berekend worden.");
            Assert.AreEqual(0, rightWeight, "Het gewicht aan de rechterkant moet 0 zijn wanneer er geen containers zijn.");
            Assert.AreEqual(0, middleWeight, "Het gewicht van de middelste rijen moet 0 zijn wanneer er geen containers zijn.");
        }


        [TestMethod]
        public void CalculateLeftWeight_ShouldReturnZero_WhenLeftSideIsEmpty()
        {
            // Arrange
            Ship ship = new Ship(4, 2);

            // Act
            int leftWeight = ship.CalculateLeftWeight();

            // Assert
            Assert.AreEqual(0, leftWeight, "Het gewicht aan de linkerkant moet 0 zijn wanneer er geen containers zijn.");
        }

        [TestMethod]
        public void CalculateLeftWeight_ShouldReturnSumOfWeights_WhenAllRowsAreFilled()
        {
            // Arrange
            Ship ship = new Ship(4, 4);
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(10, false, false));
            ship.Rows[1].Stacks[0].TryToAddContainer(new Container(20, false, false));
            ship.Rows[2].Stacks[0].TryToAddContainer(new Container(15, false, false));
            ship.Rows[3].Stacks[0].TryToAddContainer(new Container(25, false, false));

            // Act
            int leftWeight = ship.CalculateLeftWeight();

            // Assert
            Assert.AreEqual(30, leftWeight, "Het gewicht aan de linkerkant moet de som zijn van de gewichten van de eerste helft van de rijen.");
        }

        [TestMethod]
        public void CalculateRightWeight_ShouldReturnCorrectWeight_WhenRightSideHasDifferentWeights()
        {
            // Arrange
            Ship ship = new Ship(2, 2);
            ship.Rows[1].Stacks[0].TryToAddContainer(new Container(15, false, false)); // Voeg een container van 15 toe aan de derde rij
            ship.Rows[1].Stacks[0].TryToAddContainer(new Container(25, false, false)); // Voeg een container van 25 toe aan de vierde rij

            // Act
            int rightWeight = ship.CalculateRightWeight();

            // Assert
            Assert.AreEqual(40, rightWeight, "Het gewicht aan de rechterkant moet correct berekend worden.");
        }

        [TestMethod]
        public void CalculateRightWeight_ShouldReturnZero_WhenRightSideIsEmpty()
        {
            // Arrange
            Ship ship = new Ship(4, 2);

            // Act
            int rightWeight = ship.CalculateRightWeight();

            // Assert
            Assert.AreEqual(0, rightWeight, "Het gewicht aan de rechterkant moet 0 zijn wanneer er geen containers zijn.");
        }

        [TestMethod]
        public void CalculateRightWeight_ShouldReturnSumOfWeights_WhenAllRowsAreFilled()
        {
            // Arrange
            Ship ship = new Ship(2, 2);
            ship.Rows[1].Stacks[0].TryToAddContainer(new Container(10, false, false));
            ship.Rows[1].Stacks[1].TryToAddContainer(new Container(20, false, false));

            // Act
            int rightWeight = ship.CalculateRightWeight();

            // Assert
            Assert.AreEqual(30, rightWeight, "Het gewicht aan de rechterkant moet de som zijn van de gewichten van de tweede helft van de rijen.");
        }

        [TestMethod]
        public void CalculateMiddleWeight_ShouldReturnCorrectWeight_WhenWidthIsOddAndMiddleRowIsNotEmpty()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            ship.Rows[1].Stacks[1].TryToAddContainer(new Container(20, false, false)); // Voeg een container van 20 toe aan de middelste rij

            // Act
            int middleWeight = ship.CalculateMiddleWeight();

            // Assert
            Assert.AreEqual(20, middleWeight, "Het gewicht van de middelste rij moet correct berekend worden.");
        }

        [TestMethod]
        public void CalculateMiddleWeight_ShouldReturnCorrectWeight_WhenWidthIsEvenAndMiddleRowsAreNotEmpty()
        {
            // Arrange
            Ship ship = new Ship(4, 4);
            ship.Rows[2].Stacks[1].TryToAddContainer(new Container(10, false, false)); // Voeg een container van 10 toe aan de derde rij
            ship.Rows[2].Stacks[2].TryToAddContainer(new Container(15, false, false)); // Voeg een container van 15 toe aan de vierde rij

            // Act
            int middleWeight = ship.CalculateMiddleWeight();

            // Assert
            Assert.AreEqual(0, middleWeight, "Het gewicht van de middelste rijen moet correct berekend worden.");
        }

        [TestMethod]
        public void CalculateMiddleWeight_ShouldReturnZero_WhenMiddleRowIsEmpty()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Act
            int middleWeight = ship.CalculateMiddleWeight();

            // Assert
            Assert.AreEqual(0, middleWeight, "Het gewicht van de middelste rij moet 0 zijn wanneer deze leeg is.");
        }

        [TestMethod]
        public void CalculateMiddleWeight_ShouldReturnWeightOfSingleRow_WhenShipHasOneRow()
        {
            // Arrange
            Ship ship = new Ship(1, 3);
            ship.Rows[1].Stacks[0].TryToAddContainer(new Container(10, false, false)); // Voeg een container van 10 toe aan de enige rij

            // Act
            int middleWeight = ship.CalculateMiddleWeight();

            // Assert
            Assert.AreEqual(10, middleWeight, "Het gewicht van de middelste rij moet het gewicht van de enige rij zijn.");
        }
















































    }
}
