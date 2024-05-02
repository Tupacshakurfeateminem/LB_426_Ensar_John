using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_426_Emsar_John
{
    public class GameFactory
    {
        public static IGame CreateGame(string spielName, double einsatz = 0)
        {
            switch (spielName.ToLower())
            {
                case "bingo":
                    return new Bingo(einsatz);
                case "roulette":
                    return new Roulette();
                case "schliessen":
                    return new SpielSchliessen();
                default:
                    throw new ArgumentException("Ungültiges Spiel.");
            }
        }
    }
}
