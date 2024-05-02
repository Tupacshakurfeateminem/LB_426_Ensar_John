using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;

namespace LB_426_Emsar_John
{
    // Schnittstelle für Beobachter
    public interface IGameObserver
    {
        void Update(string message);
    }

    // Beobachterklasse für Bingo
    public class BingoObserver : IGameObserver
    {
        public void Update(string message)
        {
            Console.WriteLine("Bingo-Observer: " + message);
        }
    }

    // Bingo-Spielklasse
    public class Bingo : IGame
    {
        private double einsatz;
        private List<IGameObserver> observers = new List<IGameObserver>();

        public Bingo(double einsatz)
        {
            this.einsatz = einsatz;
        }

        public void Start()
        {
            NotifyObservers($"Bingo-Spiel wird gestartet mit einem Einsatz von {einsatz} Euro...");
            PlayBingo(einsatz);
        }

        // Beobachterregistrierungsmethode
        public void RegisterObserver(IGameObserver observer)
        {
            observers.Add(observer);
        }

        // Beobachterentfernungsmethode
        public void RemoveObserver(IGameObserver observer)
        {
            observers.Remove(observer);
        }

        // Benachrichtigungsmethode für Beobachter
        private void NotifyObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        private void PlayBingo(double einsatz)
        {
            string[,] playerCard = GeneratePlayerCard();

            while (!IsGameOver(playerCard))
            {
                int calledNumber = CallNumber();
                UpdatePlayerCard(playerCard, calledNumber);
                DisplayGameStatus(playerCard);

                if (IsBingo(playerCard))
                {
                    NotifyObservers("Bingo! Du hast gewonnen!");
                    NotifyObservers($"Du erhältst {einsatz * 2} Euro!"); // Gewinn entsprechend dem Einsatz
                    break;
                }

                Console.WriteLine("Nächste Runde (zum Beenden 'beenden' eingeben)...");
                if (Console.ReadLine().ToLower() == "beenden")
                {
                    NotifyObservers("Spiel abgebrochen.");
                    break;
                }
            }
        }

        private string[,] GeneratePlayerCard()
        {
            Random random = new Random();
            string[,] card = new string[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    card[i, j] = random.Next(1, 76).ToString();
                }
            }
            return card;
        }

        private bool IsGameOver(string[,] playerCard)
        {
            return IsBingo(playerCard);
        }

        private int CallNumber()
        {
            Random random = new Random();
            return random.Next(1, 76);
        }

        private void UpdatePlayerCard(string[,] playerCard, int calledNumber)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (playerCard[i, j] == calledNumber.ToString())
                    {
                        playerCard[i, j] = "x";
                        return;
                    }
                }
            }
        }

        private void DisplayGameStatus(string[,] playerCard)
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

        private bool IsBingo(string[,] playerCard)
        {
            for (int i = 0; i < 5; i++)
            {
                bool bingo = true;
                for (int j = 0; j < 5; j++)
                {
                    if (playerCard[i, j] != "x")
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
                    if (playerCard[i, j] != "x")
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
                if (playerCard[i, i] != "x")
                {
                    diagonalBingo1 = false;
                }
                if (playerCard[i, 4 - i] != "x")
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
}
