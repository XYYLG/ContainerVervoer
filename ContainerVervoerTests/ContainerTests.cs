using ContainerVervoer.Classes;
namespace ContainerVervoerTests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void ContainerConstructorTest_WithWeightLessThanEmptyWeight_ShouldSetWeightToEmptyWeight()
        {
            Container container = new Container(2, false, false);

            Assert.AreEqual(Container.EmptyWeight, container.Weight);
        }
    }
}