using CalculatorServices.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        private CalculatorService _calculatorService;

        [TestInitialize]
        public void TestInit()
        {
            _calculatorService = new CalculatorService();
        }

        [TestCleanup]
        public void TestClean()
        {
            _calculatorService = null;
        }

        [TestMethod]
        public void UpdateAllowNegativeNumbersTest()
        {
            _calculatorService.UpdateAllowNegativeNumbers('Y');
            Assert.IsTrue(_calculatorService.AllowNegativeNumbers);

            _calculatorService.UpdateAllowNegativeNumbers('y');
            Assert.IsTrue(_calculatorService.AllowNegativeNumbers);

            _calculatorService.UpdateAllowNegativeNumbers('N');
            Assert.IsTrue(!_calculatorService.AllowNegativeNumbers);

            _calculatorService.UpdateAllowNegativeNumbers('f');
            Assert.IsTrue(!_calculatorService.AllowNegativeNumbers);

            _calculatorService.UpdateAllowNegativeNumbers('x');
            Assert.IsTrue(!_calculatorService.AllowNegativeNumbers);
        }

        [TestMethod]
        public void UpdateUpperBoundaryTest()
        {
            Assert.AreEqual(_calculatorService.UpperBounds, 1000);

            _calculatorService.UpdateUpperBoundary("500");
            Assert.AreEqual(_calculatorService.UpperBounds, 500);
        }

        [TestMethod]
        public void GetArgumentsStringTest()
        {
            _calculatorService.ValidateInput(@"\n2,5");
            var argString = _calculatorService.GetArgumentsString();

            Assert.AreEqual(argString, "5 + 0 + 2 = 7");
        }

        [TestMethod]
        public void ValidateInputTest()
        {
            _calculatorService.ValidateInput(@"\n2,5");
            var argString = _calculatorService.GetArgumentsString();

            Assert.AreEqual(argString, "5 + 0 + 2 = 7");
        }

        [TestMethod]
        public void CalculateTest()
        {
            _calculatorService.ValidateInput(@"\n2,5");
            var result = _calculatorService.Calculate();

            Assert.AreEqual(result, 7);
        }
    }
}
