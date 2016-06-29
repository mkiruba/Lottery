using Lottery.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Reflection;

namespace Lottery.Services.Tests
{
    [TestClass()]
    public class LotteryGeneratorServiceTests
    {
        public ILotteryGeneratorService mockLotteryGeneratorService;
        public LotteryNumber lotteryNumber;

        [TestInitialize]
        public void Initialize()
        {
            mockLotteryGeneratorService = MockRepository.GenerateMock<ILotteryGeneratorService>();
            lotteryNumber = new LotteryNumber();
        }

        [TestMethod()]
        public void Mock_GenerateLotteryNumberSet_CheckObjects()
        {
            //Arrange
            int count = 2;
            List<LotteryNumber> lotteryNumbers = new List<LotteryNumber>()
            {
                new LotteryNumber() { Number = 10, Colour = Colour.RED},
                new LotteryNumber() { Number = 20, Colour = Colour.GREEN}
            };           

            mockLotteryGeneratorService.Expect(x => x.GenerateLotteryNumberSet(count))
                .Return(lotteryNumbers);
            mockLotteryGeneratorService.Expect(x => x.FilePath)
               .Return(Assembly.GetExecutingAssembly().Location);
            //Act
            ILotteryGeneratorService lotteryGeneratorService = new LotteryGeneratorService();
            var lotterySet = mockLotteryGeneratorService.GenerateLotteryNumberSet(count);

            //Assert
            Assert.AreEqual(lotterySet, lotteryNumbers);
        }        
    }
}