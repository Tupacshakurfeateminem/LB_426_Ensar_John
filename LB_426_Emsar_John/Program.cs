using System;

namespace LB_426_Emsar_John
{
    // Schnittstelle für Spiele
    public interface IGame
    {
        void Start();
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
}
