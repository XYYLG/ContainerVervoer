using ContainerVervoer.Classes;
using System;
namespace ContainerVervoerTests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void ContainerConstructorTest_WithWeightLessThenEmptyWeight_ShouldSetWeightToEmptyWeight()
        {
            // Arrange
            int weight = 2;
            bool isValuable = true;
            bool needsCooling = false;
            int expectedWeight = Container.EmptyWeight;

            // Act
            Exception exception = Assert.ThrowsException<Exception>(() => new Container(weight, isValuable, needsCooling));

            //Assert
            Assert.AreEqual("Gewicht kan niet minder zijn dan 4 ton", exception.Message);
        }

        [TestMethod]
        public void ContainerConstructorTest_WithWeightMorethenMaxWeight_ShouldSetWeightToMaxWeight() //aanpassen
        {
            // Arrange
            int weight = 40;
            bool isValuable = false;
            bool needsCooling = true;
            int expectedWeight = Container.MaxWeight;

            //Act
            Exception exception = Assert.ThrowsException<Exception>(() => new Container(weight, isValuable, needsCooling));

            //Assert
            Assert.AreEqual("Gewicht kan niet meer zijn dan 30 ton", exception.Message);
        }

        [TestMethod]
        public void ContainerConstructorTest_WithWeightWithinRange_ShouldSetWeightToGivenValue()
        {
            //Arrange
            int weight = 20; 
            bool isValuable = true; 
            bool needsCooling = true; 
            int expectedWeight = weight;

            //Act
            Container container = new Container(weight, isValuable, needsCooling);

            //Assert
            Assert.AreEqual(expectedWeight, container.Weight);
        }

        [TestMethod]
        public void ContainerConstructorTest_ShouldSetPropertiesCorrectly()
        {
            //Arrange
            int weight = 10;
            bool isValuable = false; 
            bool needsCooling = true;

            //Act
            Container container = new Container(weight, isValuable, needsCooling);

            //Assert
            Assert.AreEqual(weight, container.Weight);
            Assert.AreEqual(isValuable, container.IsValuable);
            Assert.AreEqual(needsCooling, container.NeedsCooling);
        }
    }
}