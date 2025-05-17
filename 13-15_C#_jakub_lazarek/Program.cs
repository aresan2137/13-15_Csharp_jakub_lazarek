class Cards {
    public byte[,] CentralCards;
    public byte[] LeftTile;
    public byte[] RightTiles;
    public byte LeftTilePosition;
}

// niewiem czy jak biore z lewej tali to ma iść do pszodu czy do tyłu

class Game {
    static int Main() { // główna funkcja
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Title = "Gra w Pasiansa";
        Console.Clear();
        
        bool result = game(); // odpalanie gry
        if (result) {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Gratuluje wygranej");
            Console.ReadLine();
        }
        return 0;
    }


    static bool game() {
        Cards cards = CardGenerator.GenerateCards(); // twożenie kart
        while (true) {
            CardRenderer.DrawGame(cards); // rysowanie kart w kąsoli
            Console.Write("command: "); 
            string action = Console.ReadLine(); // wczytywanie komendy
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            if (action.ToLower() == "exit") return false;
            if (action.ToLower() == "reset") { cards = CardGenerator.GenerateCards(); continue; }

            UnderstundNotation.UnderstandNotation(action,cards);
            RevealShowibleCards(cards);

            if (cards.LeftTilePosition >= cards.LeftTile.Length) cards.LeftTilePosition = 0;

            if (cards.RightTiles[0] - cards.RightTiles[0] % 10 == 130 &&
                cards.RightTiles[1] - cards.RightTiles[1] % 10 == 130 &&
                cards.RightTiles[2] - cards.RightTiles[2] % 10 == 130 &&
                cards.RightTiles[3] - cards.RightTiles[3] % 10 == 130) return true;

        }
    }

    static void RevealShowibleCards(Cards cards) {

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