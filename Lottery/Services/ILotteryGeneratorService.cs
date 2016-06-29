using Lottery.Models;
using System.Collections.Generic;

namespace Lottery.Services
{
    public interface ILotteryGeneratorService
    {
        string FilePath { get; }
        IEnumerable<LotteryNumber> GenerateLotteryNumberSet(int setCount);
    }
}
