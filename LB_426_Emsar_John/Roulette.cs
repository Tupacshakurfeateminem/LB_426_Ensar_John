using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_426_Emsar_John
{
    public class Roulette : IGame
    {
        public void Start()
        {
            Console.WriteLine("Roulette-Spiel wird gestartet...");
            PlayRoulette();
        }

        private void PlayRoulette()
        {
            // Begrüßung und Erklärung des Spiels
            Console.WriteLine("Willkommen beim Roulette-Spiel!");
            Console.WriteLine("Gib dein Startkapital in CHF ein.");
            Console.WriteLine("Danach wähle eine Zahl (0-36), Rot (R), Schwarz (S), Gerade (G) oder Ungerade (U).");
            Console.WriteLine("Gib 'exit' ein, um das Spiel zu beenden.");

            // Startkapital des Spielers
            double kapital = 0;

            // Kapital des Spielers eingeben
            while (true)
            {
                Console.Write("Gib dein Startkapital in CHF ein: ");
                string kapitalInput = Console.ReadLine();

                if (double.TryParse(kapitalInput, out kapital) && kapital > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine positive Zahl ein.");
                }
            }

            // Spielablauf
            while (true)
            {
                Console.WriteLine($"Dein aktuelles Kapital: {kapital} CHF");

                // Spieler wählt einen Einsatz
                Console.Write("Setze deinen Einsatz: ");
                string einsatzInput = Console.ReadLine();

                // Überprüfe, ob das Spiel beendet werden soll
                if (einsatzInput.ToLower() == "exit")
                {
                    Console.WriteLine("Das Spiel wird beendet.");
                    break;
                }

                // Konvertiere den Einsatz zu einem Double-Wert
                if (!double.TryParse(einsatzInput, out double einsatz) || einsatz <= 0)
                {
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine positive Zahl ein.");
                    continue;
                }

                // Überprüfe, ob der Spieler genügend Kapital hat, um den Einsatz zu tätigen
                if (einsatz > kapital)
                {
                    Console.WriteLine("Du hast nicht genügend Kapital für diesen Einsatz.");
                    continue;
                }

                // Spieler wählt eine Zahl
                Console.Write("Wähle eine Zahl (0-36), Rot (R), Schwarz (S), Gerade (G) oder Ungerade (U): ");
                string bet = Console.ReadLine();

                // Wette auswerten und Zufallszahl generieren
                EvaluateBet(bet, einsatz, ref kapital);

                // Überprüfe, ob das Kapital aufgebraucht ist
                if (kapital <= 0)
                {
                    Console.WriteLine("Dein Kapital ist aufgebraucht.");
                    Console.Write("Möchtest du weitermachen? (ja/nein): ");
                    string weitermachen = Console.ReadLine();

                    if (weitermachen.ToLower() == "ja")
                    {
                        // Kapital des Spielers erneut eingeben
                        while (true)
                        {
                            Console.Write("Gib dein neues Kapital in CHF ein: ");
                            string neuesKapitalInput = Console.ReadLine();

                            if (double.TryParse(neuesKapitalInput, out kapital) && kapital > 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Ungültige Eingabe! Bitte gib eine positive Zahl ein.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Das Spiel wird beendet.");
                        break;
                    }
                }
            }

            Console.ReadLine(); // Warten, bevor das Programm beendet wird
        }

        private void EvaluateBet(string bet, double einsatz, ref double kapital)
        {
            // Generiere eine Zufallszahl zwischen 0 und 36 für das Roulette-Rad
            Random random = new Random();
            int randomNumber = random.Next(0, 37);

            // Gewinnchancen berechnen
            double gewinnchance = 0.0;

            if (bet.ToLower() == "r" || bet.ToLower() == "s" || bet.ToLower() == "g" || bet.ToLower() == "u")
            {
                gewinnchance = 18.0 / 37.0; // Gewinnchance für Rot, Schwarz, Gerade und Ungerade
            }
            else if (int.TryParse(bet, out int zahl))
            {
                gewinnchance = 1.0 / 37.0; // Gewinnchance für eine bestimmte Zahl
            }

            // Wette auswerten und Gewinn/Verlust anzeigen
            if (bet.ToLower() == "r")
            {
                if (IsRed(randomNumber))
                {
                    double gewinn = einsatz * 2;
                    kapital += gewinn;
                    Console.WriteLine($"Gewonnen! Die Zufallszahl ist {randomNumber} (Rot). Du gewinnst {gewinn} CHF.");
                }
                else
                {
                    kapital -= einsatz;
                    Console.WriteLine($"Verloren! Die Zufallszahl ist {randomNumber} (Schwarz). Du verlierst {einsatz} CHF.");
                }
            }
            else if (bet.ToLower() == "s")
            {
                if (!IsRed(randomNumber))
                {
                    double gewinn = einsatz * 2;
                    kapital += gewinn;
                    Console.WriteLine($"Gewonnen! Die Zufallszahl ist {randomNumber} (Schwarz). Du gewinnst {gewinn} CHF.");
                }
                else
                {
                    kapital -= einsatz;
                    Console.WriteLine($"Verloren! Die Zufallszahl ist {randomNumber} (Rot). Du verlierst {einsatz} CHF.");
                }
            }
            else if (bet.ToLower() == "g")
            {
                if (randomNumber % 2 == 0 && randomNumber != 0)
                {
                    double gewinn = einsatz * 2;
                    kapital += gewinn;
                    Console.WriteLine($"Gewonnen! Die Zufallszahl ist {randomNumber} (Gerade). Du gewinnst {gewinn} CHF.");
                }
                else
                {
                    kapital -= einsatz;
                    Console.WriteLine($"Verloren! Die Zufallszahl ist {randomNumber} (Ungerade). Du verlierst {einsatz} CHF.");
                }
            }
            else if (bet.ToLower() == "u")
            {
                if (randomNumber % 2 != 0)
                {
                    double gewinn = einsatz * 2;
                    kapital += gewinn;
                    Console.WriteLine($"Gewonnen! Die Zufallszahl ist {randomNumber} (Ungerade). Du gewinnst {gewinn} CHF.");
                }
                else
                {
                    kapital -= einsatz;
                    Console.WriteLine($"Verloren! Die Zufallszahl ist {randomNumber} (Gerade). Du verlierst {einsatz} CHF.");
                }
            }
            else if (int.TryParse(bet, out int number))
            {
                if (number == randomNumber)
                {
                    double gewinn = einsatz * 36;
                    kapital += gewinn;
                    Console.WriteLine($"Gewonnen! Die Zufallszahl ist {randomNumber}. Du gewinnst {gewinn} CHF.");
                }
                else
                {
                    kapital -= einsatz;
                    Console.WriteLine($"Verloren! Die Zufallszahl ist {randomNumber}. Du verlierst {einsatz} CHF.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Wette! Bitte geben Sie eine Zahl zwischen 0 und 36, R, S, G oder U ein.");
            }
        }

        private bool IsRed(int number)
        {
            // Überprüfe, ob die Zahl Rot ist (1, 3, 5, ..., 35)
            return (number >= 1 && number <= 10) || (number >= 19 && number <= 28);
        }
    }
}
