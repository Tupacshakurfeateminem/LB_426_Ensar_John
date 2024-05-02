# Lb_426


### User Stories

| US-№ | Verbindlichkeit | Typ  | Beschreibung                       |
| ---- | --------------- | ---- | ---------------------------------- |
| 1    |  Funktional     | Muss | Als Spieler möchte ich beim Start der Casino-Software meinen gewünschten Betrag in Jetons eingeben, damit ich diesen für den Einsatz in den Spielen verwenden kann.|
| 2    |  Funktional     | Muss | Als Spieler möchte ich die Auswahl zwischen verschiedenen Spielen haben (Bingo und Blackjack), damit ich das Spiel meiner Wahl spielen kann.|   
| 3    |  Funktional     | muss | Als Spieler möchte ich nach jedem spiel ein erneutes spiel spielen können oder das spiel verlassen können.|
| 4    |  Funktional     | muss | Als Spieler möchte ich beim Bingo-Spiel und beim Roulette-Spiel den Einsatz selbst entscheiden können, um die Spannung und die potenziellen Gewinne zu varieren.|
| 5    |  Funktional     | Kann | Als Spieler möchte ich beim Roulette-Spiel Beträge auf verschiedene Wettmöglichkeiten setzen können, um verschiedene Gewinnchancen und Gewinnbeträge zu erhalten.|
| 6    |  Funktional     | muss | Als Spieler möchte ich, dass die bereits im Bingo-Spiel gezogenen Zahlen markiert und durchgestrichen werden, so dass ich leicht erkennen kann, welche Zahlen bereits aufgerufen wurden.|

### Factory Method Pattern 

Die Verwendung des Factory Method Patterns sorgt dafür, dass die Hauptanwendung nicht eng an die spezifischen Implementierungsdetails der Spiele gebunden ist. Dadurch wird es einfacher, das System zu warten und zu erweitern, da neue Spiele problemlos hinzugefügt werden können, ohne den Hauptcode zu verändern.
