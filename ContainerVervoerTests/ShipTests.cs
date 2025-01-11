using ContainerVervoer.Classes;

namespace ContainerVervoerTests
{
    [TestClass]
    public class ShipTests
    {
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
        public void CalculateTotalWeightMethodTest_ShouldReturnZero_WhenShipHasNoRows()
        {
            // Arrange
            Ship ship = new Ship(0, 0);

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight, "Het totale gewicht moet 0 zijn wanneer het schip geen rijen heeft.");
        }

        [TestMethod]
        public void CalculateTotalWeightMethodTest_ShouldReturnZero_WhenAllRowsAreEmpty()
        {
            // Arrange
            Ship ship = new Ship(3, 3);

            // Act
            int totalWeight = ship.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight, "Het totale gewicht moet 0 zijn wanneer alle rijen leeg zijn.");
        }

        [TestMethod]
        public void CalculateTotalWeightMethodTest_ShouldReturnSumOfAllContainerWeights_WhenShipHasContainersInRows()
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
        public void CalculateTotalWeightMethodTest_ShouldReturnSumOfAllContainerWeightsInAllRows()
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
        public void TryToAddContainerMethodTest_ShouldAddToMiddleRow_WhenWidthIsOddAndMiddleRowHasMinWeight()
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
        public void TryToAddContainerMethodTest_ShouldAddToLeftRowWithMinWeight()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            ship.Rows[0].TryToAddContainer(new Container(5, false, false)); // Voeg een container toe aan de eerste rij
            Container container = new Container(10, false, false);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou aan de rij met het minste gewicht aan de linkerkant toegevoegd moeten zijn.");
            Assert.AreEqual(15, ship.Rows[0].CalculateTotalWeight(), "Het totale gewicht van de eerste rij moet 15 zijn.");
        }

        [TestMethod]
        public void TryToAddContainerMethodTest_ShouldAddToRightRowWithMinWeight()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            ship.Rows[2].TryToAddContainer(new Container(5, false, false)); // Voeg een container toe aan de derde rij
            Container container = new Container(10, false, false);

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou aan de rij met het minste gewicht aan de rechterkant toegevoegd moeten zijn.");
            Assert.AreEqual(15, ship.Rows[2].CalculateTotalWeight(), "Het totale gewicht van de derde rij moet 15 zijn.");
        }


        [TestMethod]
        public void TryToAddContainerMethodTest_ShouldReturnFalse_WhenContainerIsTooHeavy()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            Container container = new Container(1000, false, false); // Container met een zeer hoog gewicht

            // Act
            bool result = ship.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result, "De container zou niet toegevoegd moeten worden omdat deze te zwaar is.");
            foreach (var row in ship.Rows)
            {
                Assert.AreEqual(0, row.CalculateTotalWeight(), "Het totale gewicht van elke rij moet 0 zijn.");
            }
        }

        [TestMethod]
        public void IsProperlyLoadedMethodTest_ShouldThrowException_WhenTotalWeightIsTooLow()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(10, false, false)); // Voeg containers toe

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
            Assert.IsNotNull(ex, "Een uitzondering moet worden gegooid wanneer het totale gewicht te laag is.");
            Assert.AreEqual("Het gewicht is te laag", ex.Message, "De methode moet een uitzondering gooien met het bericht 'Het gewicht is te laag' wanneer het totale gewicht te laag is.");
        }

        [TestMethod]
        public void IsProperlyLoadedMethodTest_ShouldNotThrowException_WhenTotalWeightIsWithinLimits()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            int maxWeight = ship.Length * ship.Width * (Stack.StackCapacity + Container.MaxWeight);
            int adequateWeight = (int)(0.6 * maxWeight); // Een gewicht dat binnen de limieten valt
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(adequateWeight, false, false)); // Voeg containers toe

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
            Assert.IsNull(ex, "Gewicht kan niet meer zijn dan 30 ton");
        }

        [TestMethod]
        public void IsProperlyLoadedMethodTest_ShouldNotThrowException_WhenTotalWeightIsExactlyOnLimit()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            int maxWeight = ship.Length * ship.Width * (Stack.StackCapacity + Container.MaxWeight);
            int limitWeight = (int)(0.5 * maxWeight); // Exact op de grens
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(limitWeight, false, false)); // Voeg containers toe

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
            Assert.IsNull(ex, "Gewicht kan niet meer zijn dan 30 ton");
        }

        [TestMethod]
        public void IsProperlyLoadedMethodTest_ShouldNotThrowException_WhenTotalWeightIsFarAboveLimit()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            int maxWeight = ship.Length * ship.Width * (Stack.StackCapacity + Container.MaxWeight);
            int highWeight = (int)(0.8 * maxWeight); // Een gewicht dat ver boven de limiet ligt
            ship.Rows[0].Stacks[0].TryToAddContainer(new Container(highWeight, false, false)); // Voeg containers toe

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
            Assert.IsNull(ex, "Gewicht kan niet meer zijn dan 30 ton");
        }


    }
}
