﻿using ContainerVervoer.Classes;

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
        public void TryToAddContainerMethodTest_ShouldReturnFalse_WhenPreviousAndNextNotReachable()
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
            Assert.IsFalse(result, "Expected TryToAddContainer to return false because previous and next stacks are not reachable.");
            CollectionAssert.DoesNotContain(row.Stacks[0].Containers, container, "Container should not be added to stack 0.");
            CollectionAssert.DoesNotContain(row.Stacks[1].Containers, container, "Container should not be added to stack 1.");
            CollectionAssert.DoesNotContain(row.Stacks[2].Containers, container, "Container should not be added to stack 2.");
        }



        [TestMethod]
        public void TryToAddContainerMethodTest_ShouldReturnFalse_WhenValuableContainerAndStackNotReachable()
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

        [TestMethod]
        public void IsStackReachableMethodTest_ShouldReturnTrue_WhenStackIsFirstOrLast()
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
        public void IsStackReachableMethodTest_ShouldReturnTrue_WhenCurrentHeightIsZero()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel met hoogte 0 moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachableMethodTest_ShouldReturnTrue_WhenStackHasNoValuableContainer()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[1].Containers.Add(new Container(10, false, false)); // Voeg een gewone container toe

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel zonder waardevolle container moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsStackReachableMethodTest_ShouldReturnFalse_WhenCurrentHeightGreaterThanNextAndPreviousHeight()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[1].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe
            row.Stacks[1].Containers.Add(new Container(10, true, false)); // Voeg nog een container toe om de hoogte te verhogen
            row.Stacks[0].Containers.Add(new Container(5, false, false)); // Voeg een gewone container toe aan de vorige stapel
            row.Stacks[2].Containers.Add(new Container(5, false, false)); // Voeg een gewone container toe aan de volgende stapel

            // Act
            bool result = row.IsStackReachable(1);

            // Debugging informatie
            int currentHeight = row.Stacks[1].Containers.Count;
            int nextHeight = row.Stacks[2].Containers.Count;
            int previousHeight = row.Stacks[0].Containers.Count;

            Console.WriteLine($"currentHeight: {currentHeight}, nextHeight: {nextHeight}, previousHeight: {previousHeight}");

            // Assert
            Assert.IsFalse(result, "Stapel met grotere hoogte dan de volgende en vorige stapel moet niet bereikbaar zijn.");
        }


        [TestMethod]
        public void IsStackReachableMethodTest_ShouldReturnTrue_WhenCurrentHeightLessThanNextOrPreviousHeight()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[1].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe
            row.Stacks[0].Containers.Add(new Container(20, false, false)); // Voeg een container toe aan de vorige stapel om de hoogte te verhogen
            row.Stacks[2].Containers.Add(new Container(5, false, false));  // Voeg een gewone container toe aan de volgende stapel

            // Act
            bool result = row.IsStackReachable(1);

            // Assert
            Assert.IsTrue(result, "Stapel met kleinere hoogte dan de volgende of vorige stapel moet bereikbaar zijn.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnTrue_WhenBothAreReachable()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsTrue(result, "Zowel de vorige als de volgende stapel moeten bereikbaar zijn.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnFalse_WhenPreviousIsNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[0].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe aan de vorige stapel

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsFalse(result, "De vorige stapel is niet bereikbaar.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnFalse_WhenNextIsNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[2].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe aan de volgende stapel

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsFalse(result, "De volgende stapel is niet bereikbaar.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnFalse_WhenBothAreNotReachable()
        {
            // Arrange
            Row row = new Row(3);
            row.Stacks[0].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe aan de vorige stapel
            row.Stacks[2].Containers.Add(new Container(10, true, false)); // Voeg een waardevolle container toe aan de volgende stapel

            // Act
            bool result = row.IsPreviousAndNextReachable(1);

            // Assert
            Assert.IsFalse(result, "Zowel de vorige als de volgende stapel zijn niet bereikbaar.");
        }



        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnTrue_WhenCheckingFirstStack()
        {
            // Arrange
            Row row = new Row(3);

            // Act
            bool result = row.IsPreviousAndNextReachable(0);

            // Assert
            Assert.IsTrue(result, "De eerste stapel moet bereikbaar zijn omdat er geen vorige stapel is.");
        }

        [TestMethod]
        public void IsPreviousAndNextReachableMethodTest_ShouldReturnTrue_WhenCheckingLastStack()
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
