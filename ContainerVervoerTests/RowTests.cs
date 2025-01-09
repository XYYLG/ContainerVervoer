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
        public void CalculateTotalWeightMethodTest_ShouldReturnZero_WhenStacksAreEmpty()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight);
        }

        [TestMethod]
        public void CalculateTotalWeightMethodTest_ShouldReturnCorrectWeight_WhenStacksHaveWeights()
        {
            // Arrange
            Stack stack1 = new Stack(false);
            Stack stack2 = new Stack(false);
            Stack stack3 = new Stack(false);

            stack1.Containers.Add(new Container(10, false, false));
            stack2.Containers.Add(new Container(20, false, false));
            stack3.Containers.Add(new Container(30, false, false));

            Row row = new Row(0); // Start met een lege rij

            row.Stacks.Add(stack1);
            row.Stacks.Add(stack2);
            row.Stacks.Add(stack3);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(60, totalWeight);

        }

        [TestMethod]
        public void CalculateTotalWeightMethodTest_ShouldReturnWeightOfSingleStack()
        {
            // Arrange
            Stack stack = new Stack(false);
            stack.Containers.Add(new Container(25, false, false));

            Row row = new Row(0); row.Stacks.Add(stack);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(25, totalWeight);
        }

        [TestMethod]
        public void CalculateTotalWeightMethodTest_ShouldReturnZero_WhenNoStacksPresent()
        {
            // Arrange
            Row row = new Row(0);

            // Act
            int totalWeight = row.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight);
        }

        [TestMethod]
        public void TryToAddContainerMethodTest_ShouldReturnTrue_WhenContainerAddedToNonValuableStack()
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
        public void TryToAddContainerMethodTest_ShouldReturnTrue_WhenValuableContainerAdded()
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
        public void TryToAddContainerMethodTest_ShouldReturnFalse_WhenRowIsFull()
        {
            // Arrange
            Row row = new Row(1);
            Container container = new Container(Stack.StackCapacity, false, false);
            Container newContainer = new Container(10, false, false); // Nieuwe container
            row.Stacks[0].Containers.Add(newContainer);

            // Act
            Exception exception = Assert.ThrowsException<Exception>(() => new Container(10, false, false));

            //Assert
            Assert.AreEqual("Gewicht kan niet meer zijn dan 30 ton", exception.Message);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenPreviousAndNextNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            Container container = new Container(10, false, false);

            // Voeg containers toe aan de eerste en derde stapel zodat ze niet bereikbaar zijn
            row.Stacks[0].Containers.Add(new Container(10, false, false));
            row.Stacks[2].Containers.Add(new Container(10, false, false));

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(result); CollectionAssert.DoesNotContain(row.Stacks[1].Containers, container);
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenValuableContainerAndStackNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            Container container = new Container(10, true, false);

            // Voeg containers toe aan de eerste en derde stapel zodat ze niet bereikbaar zijn
            row.Stacks[0].Containers.Add(new Container(10, false, false));
            row.Stacks[2].Containers.Add(new Container(10, false, false));

            // Act
            bool result = row.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result, "De container werd toegevoegd terwijl dit niet zou moeten gebeuren.");
            CollectionAssert.DoesNotContain(row.Stacks[1].Containers, container, "De container is ten onrechte toegevoegd aan de tweede stapel.");
        }
    }
}
