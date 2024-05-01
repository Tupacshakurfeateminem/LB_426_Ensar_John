namespace LB_426_Emsar_John;
using System;

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

    // Hauptmethode
    public static void Main(string[] args)
    {
        Console.WriteLine("Willkommen im Casino!");
        Console.WriteLine("Bitte geben Sie den Betrag in Euro ein, den Sie in Jetons umtauschen möchten:");

        int eingabeBetrag = Convert.ToInt32(Console.ReadLine());

        Casino casino = new Casino();
        casino.GeldZuJetons(eingabeBetrag);
    }
}
