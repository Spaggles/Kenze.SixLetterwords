using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simon.Kenze.SixLetterWords.Business.LetterWords;
using Simon.Kenze.SixLetterWords.BusinessFacade;
using Simon.Kenze.SixLetterWords.Data;
using Simon.Kenze.SixLetterWords.Objects.Filters;
using Simon.Kenze.SixLetterWords.Responses;
using System;

namespace Simon.Kenze.SixLetterWords.IntegrationTest
{
    [TestClass]
    public class CalcLetterCombinationsServiceTests
    {
        private readonly ICalcLetterCombinationsService _calcLetterCombinationsService;

        public CalcLetterCombinationsServiceTests()
        {
            var host = CreateHostBuilder(null).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            ICalcLetterCombinationsService calcLetterCombinationsService = provider.GetRequiredService<ICalcLetterCombinationsService>();

            _calcLetterCombinationsService = calcLetterCombinationsService;
        }

        [TestMethod]
        public void DoesGetPossibleCombinationsOfTwoReturnResults()
        {
            // arrange
            CalcLetterCombinationsFilter? filter = new CalcLetterCombinationsFilter { DesiredResultingCombinationStringLength = 6 };

            // act
            ListResponse<string> result = _calcLetterCombinationsService.GetPossibleCombinationsOfTwo(filter);

            // assert
            Assert.AreEqual(result.IsSuccess, true);
            Assert.IsTrue(result.Value.Count > 0);
        }

        [TestMethod]
        public void DoesGetPossibleCombinationsOfXReturnResults()
        {
            // arrange
            CalcLetterCombinationsFilter? filter = new CalcLetterCombinationsFilter { DesiredResultingCombinationStringLength = 6 };

            // act
            ListResponse<string> result = _calcLetterCombinationsService.GetPossibleCombinationsOfX(filter);

            // assert
            Assert.AreEqual(result.IsSuccess, true);
            Assert.IsTrue(result.Value.Count > 0);
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => services
                    .AddTransient<ILetterWordsRepository, LetterWordsRepository>()
                    .AddTransient<ICalcLetterCombinationsService, CalcLetterCombinationsService>());
        }
    }
}