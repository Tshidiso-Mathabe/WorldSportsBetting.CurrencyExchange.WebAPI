using Moq;
using Newtonsoft.Json;
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

        [TestMethod]
        public async Task When_ConvertAsyncIsInvoked_Given_TheCurrenyIsNotAvailable_Should_GracefullyErrorHandle_Async()
        {
            // Arrange
            string baseCurrency = "USD";
            string targetCurrency = "StillNotHotBadCurrency";
            string expectedResult = $"'{targetCurrency}' currency is not available.";
            LatestResponseDto latestResponseDto =
                new()
                {
                    Base = baseCurrency,
                    Rates = new Dictionary<string, double> { ["TestCurrency"] = 1, ["TestCurrency1"] = 8.5, ["TestCurrency2"] = 13, ["TestCurrency3"] = 5.1 }
                };
            _currencyRatesServiceMock.Setup(x => x.GetLatestCurrencyRatesAsync(baseCurrency, default)).Returns(Task.FromResult(latestResponseDto));
            ConvertService convertService = new(_currencyRatesServiceMock.Object);

            // Act
            CalcConvertResponseDto actualResult = await convertService.ConvertAsync(baseCurrency, targetCurrency, 13, default);

            // Assert
            Assert.IsFalse(actualResult.Success);
            Assert.AreEqual(expectedResult, actualResult.ErrorMessage);
        }

        [TestMethod]
        public async Task When_ConvertAsyncIsInvoked_Given_ThereIsAnError_Should_GracefullyErrorHandle_Async()
        {
            // Arrange
            string baseCurrency = "ZAR";
            string targetCurrency = "ItWasLateTiredBadCurrency";
            LatestResponseDto latestResponseDto =
                new()
                {
                    Base = baseCurrency,
                    HasError = true,
                    ErrorDetails = new LatestErrorResponseDto { Message = "Exception", Result = "Third Party API downtime schedule." }
                };
            string expectedErrorDetailsResult = JsonConvert.SerializeObject(latestResponseDto.ErrorDetails);
            _currencyRatesServiceMock.Setup(x => x.GetLatestCurrencyRatesAsync(baseCurrency, default)).Returns(Task.FromResult(latestResponseDto));
            ConvertService convertService = new(_currencyRatesServiceMock.Object);

            // Act
            CalcConvertResponseDto actualResult = await convertService.ConvertAsync(baseCurrency, targetCurrency, 13, default);

            // Assert
            Assert.IsFalse(actualResult.Success);
            Assert.AreEqual(expectedErrorDetailsResult, actualResult.ErrorMessage);
        }
    }
}