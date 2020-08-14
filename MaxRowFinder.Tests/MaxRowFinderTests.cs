using NUnit.Framework;

namespace MaxRowFinder.Tests
{
    [TestFixture]
    public class MaxRowFinderTests
    {
        [TestCase("1.4,2.5,6.7,4.0,5", ExpectedResult = true)]
        [TestCase("3.5,1.1,5.2", ExpectedResult = true)]
        [TestCase("1.9", ExpectedResult = true)]
        public bool IsNumericRow_ValidNumericRows_True(string line)
        {
            // Arrange
            var maxRowFinder = new MaxRowFinder();

            // Act
            bool result = maxRowFinder.IsNumericRow(line);

            // Assert
            return result;
        }

        [TestCase("1.4%,2.5,6.#7,4.0,5", ExpectedResult = false)]
        [TestCase("3.Qw5,1.1,3@5.2", ExpectedResult = false)]
        public bool IsNumericRow_InValidNumericRows_False(string line)
        {
            // Arrange
            var maxRowFinder = new MaxRowFinder();

            // Act
            bool result = maxRowFinder.IsNumericRow(line);

            // Assert
            return result;
        }

        [TestCase("1.4,2.5,6.5", ExpectedResult = 10.4)]
        [TestCase("1.9", ExpectedResult = 1.9)]
        public double CheckRowSum_ValidNumericRow_ValidSum(string line)
        {
            // Arrange
            var maxRowFinder = new MaxRowFinder();

            // Act
            var result = maxRowFinder.GetRowSum(line);

            // Assert
            return result;
        }

        [Test]
        public void GetMaxElementSumRow_ValidNumericRows_ReturnValidRowNumber()
        {
            // Arrange
            var maxRowFinder = new MaxRowFinder();
            var fileRows = new string[]
            {
                "1.4,2.5,6.5",
                "1.9,3.0,2",
                "1.5"
            };

            // Act
            var result = maxRowFinder.GetMaxElementSumRow(fileRows);

            // Assert
            Assert.AreEqual(result, 0);
        }
    }
}
