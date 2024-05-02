﻿using System;

namespace LB_426_Emsar_John
{
    // Schnittstelle für Spiele
    public interface IGame
    {
        void Start();
    }

    // Bingo-Spielklasse
    public class Bingo : IGame
    {
        private double einsatz;

        public Bingo(double einsatz)
        {
            this.einsatz = einsatz;
        }

        public void Start()
        {
            Console.WriteLine($"Bingo-Spiel wird gestartet mit einem Einsatz von {einsatz} Euro...");
            PlayBingo(einsatz);
        }

        private void PlayBingo(double einsatz)
        {
            int[,] playerCard = GeneratePlayerCard();

            while (!IsGameOver(playerCard))
            {
                int calledNumber = CallNumber();
                UpdatePlayerCard(playerCard, calledNumber);
                DisplayGameStatus(playerCard);

                if (IsBingo(playerCard))
                {
                    Console.WriteLine("Bingo! Du hast gewonnen!");
                    Console.WriteLine($"Du erhältst {einsatz * 2} Euro!"); // Gewinn entsprechend dem Einsatz
                    break;
                }

                Console.WriteLine("Nächste Runde (zum Beenden 'beenden' eingeben)...");
                if (Console.ReadLine().ToLower() == "beenden")
                {
                    Console.WriteLine("Spiel abgebrochen.");
                    break;
                }
            }
        }

        private int[,] GeneratePlayerCard()
        {
            Random random = new Random();
            int[,] card = new int[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    card[i, j] = random.Next(1, 76);
                }
            }
            return card;
        }

        private bool IsGameOver(int[,] playerCard)
        {
            return IsBingo(playerCard);
        }

        private int CallNumber()
        {
            Random random = new Random();
            return random.Next(1, 76);
        }

        private void UpdatePlayerCard(int[,] playerCard, int calledNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (playerCard[i, j] == calledNumber)
                    {
                        playerCard[i, j] = -1;
                        return;
                    }
                }
            }
        }

        private void DisplayGameStatus(int[,] playerCard)
        {
            Console.WriteLine("Deine Bingokarte:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write("{0,4}", playerCard[i, j]);
                }
                Console.WriteLine();
            }
        }

        private bool IsBingo(int[,] playerCard)
        {
            for (int i = 0; i < 5; i++)
            {
                bool bingo = true;
                for (int j = 0; j < 5; j++)
                {
                    if (playerCard[i, j] != -1)
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    return true;
                }
            }

            for (int j = 0; j < 5; j++)
            {
                bool bingo = true;
                for (int i = 0; i < 5; i++)
                {
                    if (playerCard[i, j] != -1)
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    return true;
                }
            }

            bool diagonalBingo1 = true;
            bool diagonalBingo2 = true;
            for (int i = 0; i < 5; i++)
            {
                if (playerCard[i, i] != -1)
                {
                    diagonalBingo1 = false;
                }
                if (playerCard[i, 4 - i] != -1)
                {
                    diagonalBingo2 = false;
                }
            }
            if (diagonalBingo1 || diagonalBingo2)
            {
                return true;
            }

            return false;
        }
    }

    // Roulette-Spielklasse
    public class Roulette : IGame
    {
        public void Start()
        {
            Console.WriteLine("Roulette-Spiel wird gestartet...");
            PlayRoulette();
        }

        private void PlayRoulette()
        {
            // Begrüssung und Erklärung des Spieles
            Console.WriteLine("Willkommen beim Roulette-Spiel!");
            Console.WriteLine("Gib dein Startkapital in CHF ein.");
            Console.WriteLine("Danach wähle Rot (R) oder Schwarz (S) aus.");
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

                // Spieler wählt Rot oder Schwarz
                Console.Write("Wähle Rot (R) oder Schwarz (S): ");
                string bet = Console.ReadLine();

                // Überprüfe, ob die Wette gültig ist
                if (bet.ToLower() != "r" && bet.ToLower() != "s")
                {
                    Console.WriteLine("Ungültige Wette! Bitte wähle Rot (R) oder Schwarz (S).");
                    continue;
                }

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
        }

        private bool IsRed(int number)
        {
            // Überprüfe, ob die Zahl Rot ist (1, 3, 5, ..., 35)
            return (number >= 1 && number <= 10) || (number >= 19 && number <= 28);
        }
    }

    // Hauptklasse für das Casino
    public class Casino
    {
        private int jetons; // Jetons des Benutzers

        public Casino()
        {
            jetons = 0; // Start mit 0 Jetons
        }

        // Methode zum Umtausch von Geld in Jetons
        public void GeldZuJetons(int geld)
        {
            jetons += geld;
            Console.WriteLine($"Sie haben {geld} Euro in Jetons umgetauscht. Aktueller Jeton-Guthaben: {jetons}");
        }

        // Methode zur Auswahl eines Spiels
        public void SpielAuswahl()
        {
            Console.WriteLine("Bitte wählen Sie ein Spiel:");
            Console.WriteLine("1. Bingo");
            Console.WriteLine("2. Roulette");
            Console.WriteLine("3. Spiel schliessen");

            int auswahl = Convert.ToInt32(Console.ReadLine());

            switch (auswahl)
            {
                case 1:
                    StartGame("bingo");
                    break;
                case 2:
                    StartGame("roulette");
                    break;
                case 3:
                    StartGame("schliessen");
                    break;
                default:
                    Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie erneut.");
                    SpielAuswahl();
                    break;
            }
        }

        // Methode zum Starten eines Spiels
        private void StartGame(string spielName)
        {
            if (spielName == "bingo")
            {
                Console.Write("Gib deinen Einsatz für Bingo ein: ");
                double einsatz = Convert.ToDouble(Console.ReadLine());
                IGame spiel = GameFactory.CreateGame(spielName, einsatz);
                spiel.Start();
            }
            else
            {
                IGame spiel = GameFactory.CreateGame(spielName);
                spiel.Start();
            }

            if (spielName != "schliessen")
            {
                ZurückZurAuswahl();
            }
        }

        // Methode zum Zurückkehren zur Spielwahl
        private void ZurückZurAuswahl()
        {
            Console.WriteLine("Möchten Sie ein weiteres Spiel spielen? (Ja/Nein)");
            string antwort = Console.ReadLine();

            if (antwort.ToLower() == "ja")
            {
                SpielAuswahl();
            }
            else
            {
                Console.WriteLine("Vielen Dank für Ihren Besuch. Auf Wiedersehen!");
            }
        }

        // Hauptmethode
        public static void Main(string[] args)
        {
            Console.WriteLine("Willkommen im Casino!");
            Console.WriteLine("Bitte geben Sie den Betrag in Euro ein, den Sie in Jetons umtauschen möchten:");

            int eingabeBetrag = Convert.ToInt32(Console.ReadLine());

            Casino casino = new Casino();
            casino.GeldZuJetons(eingabeBetrag);

            casino.SpielAuswahl();
        }
    }

    // Factory-Klasse für Spiele
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

    // Spiel zum Schliessen der Anwendung
    public class SpielSchliessen : IGame
    {
        public void Start()
        {
            Console.WriteLine("Vielen Dank für Ihren Besuch. Auf Wiedersehen!");
            Environment.Exit(0); // Schliessen der Anwendung 
        }
    }
}
