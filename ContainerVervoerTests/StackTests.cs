using ContainerVervoer.Classes;

namespace ContainerVervoerTests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void StackConstructorTest_ShouldSetIsCooledToTrue_WhenParameterIsTrue()
        {
            // Arrange
            bool isCooled = true;

            // Act
            Stack stack = new Stack(isCooled);

            // Assert
            Assert.IsTrue(stack.IsCooled, "De stapel zou gekoeld moeten zijn wanneer de parameter true is.");
        }

        [TestMethod]
        public void StackConstructorTest_ShouldSetIsCooledToFalse_WhenParameterIsFalse()
        {
            // Arrange
            bool isCooled = false;

            // Act
            Stack stack = new Stack(isCooled);

            // Assert
            Assert.IsFalse(stack.IsCooled, "De stapel zou niet gekoeld moeten zijn wanneer de parameter false is.");
        }

        [TestMethod]
        public void CanSupportWeight_ShouldReturnTrue_WhenStackIsEmpty()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);

            // Act
            bool result = stack.CanSupportWeight(container);

            // Assert
            Assert.IsTrue(result, "De stapel moet het gewicht ondersteunen wanneer deze leeg is.");
        }

        [TestMethod]
        public void CanSupportWeight_ShouldReturnTrue_WhenAddingContainerKeepsWeightBelowCapacity()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container existingContainer = new Container(10, false, false);
            stack.TryToAddContainer(existingContainer);
            Container newContainer = new Container(5, false, false);

            // Act
            bool result = stack.CanSupportWeight(newContainer);

            // Assert
            Assert.IsTrue(result, "De stapel moet het gewicht ondersteunen wanneer het totale gewicht onder de capaciteit blijft.");
        }

        [TestMethod]
        public void CanSupportWeight_ShouldReturnFalse_WhenAddingContainerExceedsCapacity()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container existingContainer = new Container(30, false, false);
            stack.TryToAddContainer(existingContainer);
            stack.TryToAddContainer(existingContainer);
            stack.TryToAddContainer(existingContainer);
            stack.TryToAddContainer(existingContainer);
            stack.TryToAddContainer(existingContainer);
            Container newContainer = new Container(10, false, false); // net boven de capaciteit

            // Act
            bool result = stack.CanSupportWeight(newContainer);

            // Assert
            Assert.IsFalse(result, "De stapel moet het gewicht niet ondersteunen wanneer het totale gewicht boven de capaciteit uitkomt.");
        }

        [TestMethod]
        public void CanSupportWeight_ShouldReturnTrue_WhenAddingContainerIsExactlyAtCapacity()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container existingContainer = new Container(10, false, false);
            stack.TryToAddContainer(existingContainer);
            int stackCapacity = 30; // Bijvoorbeeld capaciteit van 30
            Container newContainer = new Container(stackCapacity - 10, false, false); // exact gelijk aan de capaciteit

            // Act
            bool result = stack.CanSupportWeight(newContainer);

            // Assert
            Assert.IsTrue(result, "De stapel moet het gewicht ondersteunen wanneer het totale gewicht precies gelijk is aan de capaciteit.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnZero_WhenStackIsEmpty()
        {
            // Arrange
            Stack stack = new Stack(false);

            // Act
            int totalWeight = stack.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(0, totalWeight, "Het totale gewicht moet 0 zijn wanneer de stapel leeg is.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnContainerWeight_WhenStackHasOneContainer()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);
            stack.TryToAddContainer(container);

            // Act
            int totalWeight = stack.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(10, totalWeight, "Het totale gewicht moet gelijk zijn aan het gewicht van de enkele container in de stapel.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnSumOfContainerWeights_WhenStackHasMultipleContainers()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container1 = new Container(10, false, false);
            Container container2 = new Container(20, false, false);
            stack.TryToAddContainer(container1);
            stack.TryToAddContainer(container2);

            // Act
            int totalWeight = stack.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(30, totalWeight, "Het totale gewicht moet gelijk zijn aan de som van de gewichten van alle containers in de stapel.");
        }

        [TestMethod]
        public void CalculateTotalWeight_ShouldReturnSumOfVariousContainerWeights_WhenStackHasDifferentWeights()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container1 = new Container(5, false, false);
            Container container2 = new Container(15, false, false);
            Container container3 = new Container(25, false, false);
            stack.TryToAddContainer(container1);
            stack.TryToAddContainer(container2);
            stack.TryToAddContainer(container3);

            // Act
            int totalWeight = stack.CalculateTotalWeight();

            // Assert
            Assert.AreEqual(45, totalWeight, "Het totale gewicht moet gelijk zijn aan de som van de verschillende gewichten van de containers in de stapel.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenContainerNeedsCoolingAndStackIsNotCooled()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, true); // Container heeft koeling nodig

            // Act
            bool result = stack.TryToAddContainer(container);

            // Assert
            Assert.IsFalse(result, "De container zou niet toegevoegd moeten worden wanneer deze koeling nodig heeft en de stapel niet gekoeld is.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenStackHasValuableContainer()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container valuableContainer = new Container(10, true, false);
            stack.TryToAddContainer(valuableContainer); // Voeg waardevolle container toe
            Container newContainer = new Container(5, false, false);

            // Act
            bool result = stack.TryToAddContainer(newContainer);

            // Assert
            Assert.IsFalse(result, "De container zou niet toegevoegd moeten worden wanneer de stapel al een waardevolle container heeft.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnTrue_WhenWeightIsSupported()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);

            // Act
            bool result = stack.TryToAddContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou toegevoegd moeten worden wanneer het gewicht ondersteund wordt.");
            CollectionAssert.Contains(stack.Containers, container, "De container zou toegevoegd moeten zijn aan de stapel.");
        }

        [TestMethod]
        public void TryToAddContainer_ShouldReturnFalse_WhenAddingContainerExceedsCapacity()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container existingContainer = new Container(25, false, false);
            Container existingContainer2 = new Container(25, false, false);
            Container existingContainer3 = new Container(25, false, false);
            Container existingContainer4 = new Container(25, false, false);
            Container existingContainer5 = new Container(25, false, false);

            stack.TryToAddContainer(existingContainer);
            stack.TryToAddContainer(existingContainer2);
            stack.TryToAddContainer(existingContainer3);
            stack.TryToAddContainer(existingContainer4);
            stack.TryToAddContainer(existingContainer5);

            Container newContainer = new Container(25, false, false); // Net boven de capaciteit (120-10+1=111)

            // Act
            bool result = stack.TryToAddContainer(newContainer);

            // Assert
            Assert.IsFalse(result, "De container zou niet toegevoegd moeten worden wanneer het totale gewicht boven de capaciteit uitkomt.");
            CollectionAssert.DoesNotContain(stack.Containers, newContainer, "De container zou niet toegevoegd moeten zijn aan de stapel.");
        }

        [TestMethod]
        public void TryToRemoveContainer_ShouldReturnTrue_WhenContainerIsRemoved()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);
            stack.TryToAddContainer(container);

            // Act
            bool result = stack.TryToRemoveContainer(container);

            // Assert
            Assert.IsTrue(result, "De container zou succesvol verwijderd moeten worden.");
            CollectionAssert.DoesNotContain(stack.Containers, container, "De container zou niet meer aanwezig moeten zijn in de stapel.");
        }

        [TestMethod]
        public void TryToRemoveContainer_ShouldReturnFalse_WhenContainerIsNotInStack()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);

            // Act
            bool result = stack.TryToRemoveContainer(container);

            // Assert
            Assert.IsFalse(result, "De container zou niet verwijderd moeten kunnen worden wanneer deze niet aanwezig is in de stapel.");
        }

        [TestMethod]
        public void TryToRemoveContainer_ShouldReturnFalse_WhenStackIsEmpty()
        {
            // Arrange
            Stack stack = new Stack(false);
            Container container = new Container(10, false, false);

            // Act
            bool result = stack.TryToRemoveContainer(container);

            // Assert
            Assert.IsFalse(result, "De container zou niet verwijderd moeten kunnen worden wanneer de stapel leeg is.");
        }

        [TestMethod]
        public void TryToRemoveContainer_ShouldReturnTrue_WhenOneContainerIsRemovedFromMultipleContainers()
        {
            // Arrange
            Stack stack = new Stack(false);

            Container container1 = new Container(10, false, false);
            Container container2 = new Container(20, false, false);

            stack.TryToAddContainer(container1);
            stack.TryToAddContainer(container2);

            // Act
            bool result = stack.TryToRemoveContainer(container1);

            // Assert
            Assert.IsTrue(result, "De container zou succesvol verwijderd moeten worden.");
            CollectionAssert.DoesNotContain(stack.Containers, container1, "De container zou niet meer aanwezig moeten zijn in de stapel.");
            CollectionAssert.Contains(stack.Containers, container2, "De andere container zou nog aanwezig moeten zijn in de stapel.");
        }

    }
}
