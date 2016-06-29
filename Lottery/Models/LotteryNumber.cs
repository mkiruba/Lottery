using System.Configuration;

namespace Lottery.Models
{
    public enum Colour
    {
        RED,
        BLUE,
        GREEN,
        ORANGE,
        PURPLE,
        INVALID
    }
    public class LotteryNumber
    {
        public int Number { get; set; }
        public Colour Colour { get; set; }
        public string HexColour { get
            { return ConfigurationManager.AppSettings.Get(Colour.ToString()); } }
    }
}