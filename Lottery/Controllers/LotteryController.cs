using Lottery.Models;
using Lottery.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Lottery.Controllers
{
    public class LotteryController : ApiController
    {
        ILotteryGeneratorService _lotteryGeneratorService;
        public int LotterySetCount { get { return Convert.ToInt32(ConfigurationManager.AppSettings.Get("LotterySetCount")); } }

        public LotteryController(ILotteryGeneratorService lotteryGeneratorService)
        {
            _lotteryGeneratorService = lotteryGeneratorService;
        }
        
        public IEnumerable<LotteryNumber> GetResults()
        {
            var lotterySet = _lotteryGeneratorService.GenerateLotteryNumberSet(LotterySetCount);
            return lotterySet;
        }
                
    }
}
