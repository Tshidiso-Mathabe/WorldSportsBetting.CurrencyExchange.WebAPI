using Moq;
using WorldSportsBetting.CurrencyExchange.Core.Services;
using WorldSportsBetting.CurrencyExchange.Domain.DataTransferObjects;

namespace WorldSportsBetting.CurrencyExchange.Test.TestServices
{
    [TestClass]
    public class FunctionalUnitTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ICurrencyRatesService> _currencyRatesServiceMock;

        public FunctionalUnitTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _currencyRatesServiceMock = _mockRepository.Create<ICurrencyRatesService>();
        }

        [TestMethod]
        public async Task When_ConvertAsyncIsInvoked_Given_ValidBaseTargetAmount_Should_ConvertCurrencyAmountToTargetCurrencyAmount_Async()
        {
            // Arrange
            double expectedResult = 169;
            string baseCurrency = "USD";
            string targetCurrency = "TestTargetCurrency";
            LatestResponseDto latestResponseDto =
                new()
                {
                    Base = baseCurrency,
                    Rates = new Dictionary<string, double> { ["TestCurrency"] = 1, ["TestCurrency1"] = 8.5, [targetCurrency] = 13, ["TestCurrency3"] = 5.1 }
                };
            _currencyRatesServiceMock.Setup(x => x.GetLatestCurrencyRatesAsync(baseCurrency, default)).Returns(Task.FromResult(latestResponseDto));
            ConvertService convertService = new(_currencyRatesServiceMock.Object);

            // Act
            CalcConvertResponseDto actualResult = await convertService.ConvertAsync(baseCurrency, targetCurrency, 13, default);

            // Assert
            Assert.AreEqual(expectedResult, actualResult.Result);
        }
    }
}