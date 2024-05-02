using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_426_Emsar_John
{
    public class SpielSchliessen : IGame
    {
        public void Start()
        {
            Console.WriteLine("Vielen Dank für Ihren Besuch. Auf Wiedersehen!");
            Environment.Exit(0); // Schliessen der Anwendung 
        }
    }
}
