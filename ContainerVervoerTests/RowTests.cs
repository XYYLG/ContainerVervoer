using ContainerVervoer.Classes;

namespace ContainerVervoerTests
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void RowConstructorTest_ShouldInitializeStacksCorrectly()
        {
            // Arrange
            int length = 3;

            // Act
            Row row = new Row(length);

            // Assert
            Assert.AreEqual(length, row.Stacks.Count);
            Assert.IsTrue(row.Stacks[0].IsCooled);
            for (int i = 1; i < length; i++)
            {
                Assert.IsFalse(row.Stacks[i].IsCooled);
            }
        }

        [TestMethod]
        public void RowConstructorTest_ShouldHandleZeroLength()
        {
            // Arrange
            int length = 0;

            // Act
            Row row = new Row(length);

            // Assert
            Assert.AreEqual(length, row.Stacks.Count);
        }

        [TestMethod]
        public void RowConstructorTest_ShouldInitializeWithDefaultLength()
        {
            // Arrange
            int defaultLength = 5; // de standaard lengte is in dit scenario 5

            // Act
            Row row = new Row(defaultLength);

            // Assert
            Assert.AreEqual(defaultLength, row.Stacks.Count);
            Assert.IsTrue(row.Stacks[0].IsCooled);
            for (int i = 1; i < defaultLength; i++)
            {
                Assert.IsFalse(row.Stacks[i].IsCooled);
            }
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnZero_WhenStacksAreEmpty()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight);
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnCorrectWeight_WhenStacksHaveWeights()
        {
            // Arrange
            Row row = new Row(3);

            row.Stacks[0].TryToAddContainer(new Container(10, false, false));
            row.Stacks[1].TryToAddContainer(new Container(20, false, false));
            row.Stacks[2].TryToAddContainer(new Container(30, false, false));

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(60, totalWeight);

        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnWeightOfSingleStack()
        {
            // Arrange
            Row row = new Row(1);
            row.Stacks[0].TryToAddContainer(new Container(25, false, false));

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(25, totalWeight);
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnZero_WhenNoStacksPresent()
        {
            // Arrange
            Row row = new Row(0);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnTrue_WhenContainerAddedToNonValuableStack()
        {
            // Arrange
            Row row = new Row(3);
            Container container = new Container(10, false, false); // Gewone container 

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.Contains(row.Stacks[0].Containers, container);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnTrue_WhenValuableContainerAdded()
        {
            // Arrange
            Row row = new Row(3);
            Container container = new Container(10, true, false); // Waardevolle container 

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.Contains(row.Stacks[0].Containers, container);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenRowIsFull()
        {
            // Arrange
            Row row = new Row(1);

            // Voeg containers toe totdat de rij vol is
            for (int i = 0; i < 5; i++)
            {
                Container container = new Container(Container.MaxWeight, false, false); // Maak een container met een gewicht
                row.Stacks[0].TryToAddContainer(container);
            }

            Container newContainer = new Container(10, false, false);

            // Act
            bool result = row.TryToAddContainer(newContainer);

            // Assert
            Assert.IsFalse(result, "Er zou geen container toegevoegd moeten worden als de rij vol is");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenPreviousAndNextNotReachable()
        {
            // Arrange
            Row row = new Row(5);
            Container container = new Container(10, true, false);

            // Voeg containers toe aan de eerste en derde stapel zodat ze niet bereikbaar zijn
            row.Stacks[0].TryToAddContainer(new Container(10, true, false));
            row.Stacks[1].TryToAddContainer(new Container(10, true, false));
            row.Stacks[3].TryToAddContainer(new Container(10, true, false));
            row.Stacks[4].TryToAddContainer(new Container(10, true, false));

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result, "Expected TryToAddContainer to return false because previous and next stacks are not reachable.");
            CollectionAssert.DoesNotContain(row.Stacks[1].Containers, container, "Container should not be added to stack 1.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenValuableContainerAndStackNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            Container container = new Container(10, true, false);

            // Voeg containers toe aan de eerste en derde stapel zodat de tweede niet bereikbaar zijn
            row.Stacks[0].TryToAddContainer(new Container(10, true, false));
            row.Stacks[2].TryToAddContainer(new Container(10, true, false));

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result, "De container werd toegevoegd terwijl dit niet zou moeten gebeuren.");
            CollectionAssert.DoesNotContain(row.Stacks[1].Containers, container, "De container is ten onrechte toegevoegd aan de tweede stapel.");
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrue_WhenStackIsFirstOrLast()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool resultFirst = row.IsStackReachable(0);
            bool resultLast = row.IsStackReachable(2);

            // Assert
            Assert.IsTrue(resultFirst, "Eerste stapel moet bereikbaar zijn.");
            Assert.IsTrue(resultLast, "Laatste stapel moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrue_WhenCurrentHeightIsZero()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel met hoogte 0 moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrue_WhenStackHasNoValuableContainer()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[1].TryToAddContainer(new Container(10, false, false)); // Voeg een gewone container toe

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel zonder waardevolle container moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrue_WhenCurrentHeightGreaterThanNextAndPreviousHeight()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[1].TryToAddContainer(new Container(10, false, false)); // Voeg een waardevolle container toe
            row.Stacks[1].TryToAddContainer(new Container(10, true, false)); // Voeg nog een container toe om de hoogte te verhogen
            row.Stacks[0].TryToAddContainer(new Container(5, false, false)); // Voeg een gewone container toe aan de vorige stapel
            row.Stacks[2].TryToAddContainer(new Container(5, false, false)); // Voeg een gewone container toe aan de volgende stapel

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel met grotere hoogte dan de volgende en vorige stapel moet niet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachable_ShouldReturnTrue_WhenCurrentHeightMoreThanNextOrPreviousHeight()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[0].TryToAddContainer(new Container(20, false, false)); // Voeg een container toe aan de vorige stapel om de hoogte te verhogen
            row.Stacks[1].TryToAddContainer(new Container(10, false, false)); // Voeg een normale container toe
            row.Stacks[1].TryToAddContainer (new Container(10, true, false)); // Voeg een waardevolle container toe
            row.Stacks[2].TryToAddContainer (new Container(10, true, false)); // Voeg een waardevolle container toe

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel met kleinere hoogte dan de volgende of vorige stapel moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnTrue_WhenBothAreReachable()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsTrue(result, "Zowel de vorige als de volgende stapel moeten bereikbaar zijn.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnFalse_WhenPreviousIsNotReachable()
        {
            // Arrange
            Row row = new Row(5);
            row.Stacks[0].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de vorige stapel
            row.Stacks[1].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de vorige stapel
            row.Stacks[2].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de vorige stapel

            // Act
            bool result = row.IsPreviousAndNextReachable(2);

            // Assert
            Assert.IsFalse(result, "De vorige stapel is niet bereikbaar.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnFalse_WhenNextIsNotReachable()
        {
            // Arrange
            Row row = new Row(5);
            row.Stacks[2].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de volgende stapel
            row.Stacks[3].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de volgende stapel
            row.Stacks[4].TryToAddContainer(new Container(10, true, false)); // Voeg een waardevolle container toe aan de volgende stapel

            // Act
            bool result = row.IsPreviousAndNextReachable(2);

            // Assert
            Assert.IsFalse(result, "De volgende stapel is niet bereikbaar.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnTrue_WhenCheckingFirstStack()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsPreviousAndNextReachable(0);

            // Assert
            Assert.IsTrue(result, "De eerste stapel moet bereikbaar zijn omdat er geen vorige stapel is.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachable_ShouldReturnTrue_WhenCheckingLastStack()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsPreviousAndNextReachable(2);

            // Assert
            Assert.IsTrue(result, "De laatste stapel moet bereikbaar zijn omdat er geen volgende stapel is.");
        }

    }
}
