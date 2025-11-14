using System;
using System.Threading.Tasks;
using api.Services;
using apitests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apitests
{
    [TestClass]
    public class ConversionServiceTests
    {
        private IConversionService _sut;
        private IAccountService _mockAccountService;
        private IExchangeRateService _mockExchangeRateService;

        [TestInitialize]
        public void Initialize()
        {
            _mockAccountService = new MockAccountService();
            _mockExchangeRateService = new MockExchangeRateService();
            _sut = GetConversionService();
        }

        private IConversionService GetConversionService()
        {
            return new ConversionService(_mockAccountService, _mockExchangeRateService, NullLogger<ConversionService>.Instance);
        }

        [TestMethod]
        public async Task Given_Account_And_ExchangeRate_When_Converting_A_Service_Then_It_Should_Convert_On_Found_Exchange_Rate()
        {
            var actual = await _sut.GetConvertedAccount("DKK");

            Assert.IsNotNull(actual);
            Assert.AreEqual("DKK", actual.Currency);
            Assert.AreEqual(110, actual.Balance);
        }

        [TestMethod]
        public async Task Given_Account_And_ExchangeRate_When_Converting_A_Service_With_Not_Found_Currency_It_Should_Throw()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.GetConvertedAccount("NOT_VALID"));
        }
    }
}