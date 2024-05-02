using System;

namespace LB_426_Emsar_John
{
    // Interface für Spiele
    public interface IGame
    {
        void Start(int einsatz); // Einsatze hinzugefügt
    }

    // Bingo-Spielklasse
    public class Bingo : IGame
    {
        public void Start(int einsatz) // Einsatze hinzugefügt
        {
            Console.WriteLine("Bingo-Spiel wird gestartet...");
            PlayBingo(einsatz); // Einsatze hinzugefügt
        }

        private void PlayBingo(int einsatz) // Einsatze hinzugefügt
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
        public void Start(int einsatz) // Einsatze hinzugefügt
        {
            Console.WriteLine("Roulette-Spiel wird gestartet...");
            // Implementieren Sie hier die Roulette-Spiellogik
        }
    }

    // Spiel zum Schliessen der Anwendung
    public class SpielSchliessen : IGame
    {
        public void Start(int einsatz) // Einsatze hinzugefügt
        {
            Console.WriteLine("Vielen Dank für Ihren Besuch. Auf Wiedersehen!");
            Environment.Exit(0); // Schliessen der Anwendung
        }
    }

    // Factory-Klasse für Spiele
    public class GameFactory
    {
        public static IGame CreateGame(string spielName)
        {
            switch (spielName.ToLower())
            {
                case "bingo":
                    return new Bingo();
                case "roulette":
                    return new Roulette();
                case "schliessen":
                    return new SpielSchliessen();
                default:
                    throw new ArgumentException("Ungültiges Spiel.");
            }
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
                    Console.WriteLine("Bitte geben Sie Ihren Einsatz für Bingo ein:");
                    int einsatzBingo = Convert.ToInt32(Console.ReadLine());
                    StartGame("bingo", einsatzBingo);
                    break;
                case 2:
                    Console.WriteLine("Bitte geben Sie Ihren Einsatz für Roulette ein:");
                    int einsatzRoulette = Convert.ToInt32(Console.ReadLine());
                    StartGame("roulette", einsatzRoulette);
                    break;
                case 3:
                    Console.WriteLine("Bitte geben Sie Ihren Einsatz für das Schliessen ein:");
                    int einsatzSchliessen = Convert.ToInt32(Console.ReadLine());
                    StartGame("schliessen", einsatzSchliessen);
                    break;
                default:
                    Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie erneut.");
                    SpielAuswahl();
                    break;
            }
        }

        // Methode zum Starten eines Spiels
        private void StartGame(string spielName, int einsatz) // Einsatze hinzugefügt
        {
            IGame spiel = GameFactory.CreateGame(spielName);
            spiel.Start(einsatz); // Einsatze hinzugefügt

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
}

