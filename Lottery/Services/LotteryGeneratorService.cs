using System;
using System.Collections.Generic;
using System.Web;
using Lottery.Models;
using System.Configuration;
using System.IO;
using System.Text;

namespace Lottery.Services
{
    public class LotteryGeneratorService : ILotteryGeneratorService
    {
        private object objLock = new object();
        private Random r = new Random();

        public string FilePath { get { return HttpRuntime.BinDirectory; } }

        public IEnumerable<LotteryNumber> GenerateLotteryNumberSet(int setCount)
        {
            List<LotteryNumber> lotto = new List<LotteryNumber>();            
            int randNumber;
            for (int i = 0; i < setCount; i++)
            {
                randNumber = GetRandom(lotto);
                lotto.Add(new LotteryNumber()
                {
                    Number = randNumber,
                    Colour = GetColour(randNumber)
                });
            }
            WriteToFile(lotto);
            return lotto;
        }

        private void WriteToFile(List<LotteryNumber> lotto)
        {
            StringBuilder sb = new StringBuilder();
            var filename = string.Format("{0}\\{1}{2}", FilePath, ConfigurationManager.AppSettings.Get("FileName"), ".txt");
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using (var writer = new StreamWriter(filename, true))
            {
                foreach (var item in lotto)
                {
                    sb.Append(string.Format("{0},",item.Number));
                }
                writer.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString(), sb.ToString()));
            }
        }

        private int GetRandom(List<LotteryNumber> lotto)
        {            
            lock(objLock)
            {
                int randNumber;
                do
                {
                    randNumber = r.Next(1, 49);
                } while (IsDuplicate(lotto, randNumber));
                
                return randNumber;
            }
        }

        private bool IsDuplicate(List<LotteryNumber> lotto, int randNumber)
        {
            bool isDuplicate = false;
            foreach (var item in lotto)
            {
                if (item.Number == randNumber)
                {
                    isDuplicate = true;
                }
            }
            return isDuplicate;
        }

        private Colour GetColour(int randNumber)
        {
            if (randNumber >= 1 && randNumber <= 9)
            {
                return Colour.RED;
            }
            if (randNumber >= 10 && randNumber <= 19)
            {
                return Colour.BLUE;
            }
            if (randNumber >= 20 && randNumber <= 29)
            {
                return Colour.GREEN;
            }
            if (randNumber >= 30 && randNumber <= 39)
            {
                return Colour.ORANGE;
            }
            if (randNumber >= 40 && randNumber <= 49)
            {
                return Colour.PURPLE;
            }
            return Colour.INVALID;
        }        
    }
}