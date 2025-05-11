using System;
using System.Reflection;

class Cards
{
    public byte[,] CentralCards;
    public byte[] LeftTile;
    public byte[] RightTiles;
    public byte ShownId;
}


class Game {
    static int Main() { // główna funkcja
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Title = "Gra w Pasiansa";
        Console.Clear();
        
        game(); // odpalanie gry
        return 0;
    }


    static void game() {
        Cards cards = CardGenerator.GenerateCards(); // twożenie kart
        while (true) {
            CardRenderer.DrawGame(cards); // rysowanie kart w kąsoli
            Console.Write("command: "); 
            string action = Console.ReadLine(); // wczytywanie komendy
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            

            UnderstundNotation.UnderstandNotation(action,cards);
            revealcards(cards);

            if (cards.ShownId >= cards.LeftTile.Length) cards.ShownId = 0;

            if (action.ToLower() == "exit") return;
        }
    }

    static void revealcards(Cards cards) {

        for (int i = 0; i < 7; i++)
        {
            int aumanty = UnderstundNotation.CountCentralCards(cards.CentralCards, i) - 1;
            if (aumanty < 0) continue;
            byte loko = cards.CentralCards[i, aumanty];
            if (loko % 10 > 4) {
                cards.CentralCards[i, aumanty] = (byte)(loko - 4);
            }
        }
    }

}






