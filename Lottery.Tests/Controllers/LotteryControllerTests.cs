using Lottery.Models;
using Lottery.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Controllers.Tests
{
    [TestClass()]
    public class LotteryControllerTests
    {
        public ILotteryGeneratorService mockLotteryGeneratorService;
        [TestInitialize]
        public void Initialize()
        {
            mockLotteryGeneratorService = MockRepository.GenerateMock<ILotteryGeneratorService>();
        }

        [TestMethod()]
        public void GetResultsTest()
        {
            List<LotteryNumber> lotteryNumbers = new List<LotteryNumber>()
            {
                new LotteryNumber() { Number = 5, Colour = Colour.RED},
                new LotteryNumber() { Number = 20, Colour = Colour.GREEN},
                new LotteryNumber() { Number = 6, Colour = Colour.RED},
                new LotteryNumber() { Number = 45, Colour = Colour.PURPLE},
                new LotteryNumber() { Number = 15, Colour = Colour.BLUE},
                new LotteryNumber() { Number = 33, Colour = Colour.ORANGE}
            };

            var controller = new LotteryController(mockLotteryGeneratorService);
            mockLotteryGeneratorService.Expect(x => x.GenerateLotteryNumberSet(6))
                .Return(lotteryNumbers);

            var result = controller.GetResults();
            Assert.AreEqual(result.Count(), lotteryNumbers.Count);
        }
    }
}