class CardRenderer
{
    public static void DrawGame(Cards cards) { // rysowanie kart w kąsoli
        Console.WriteLine("A\tB\tC\tD\tE\tF\tG\tH\tI");
        Console.BackgroundColor = ConsoleColor.White;
                
        for (int i = 0; i < 20; i++)
        {
            Console.ForegroundColor = ConsoleColor.Black;

            if (i == 0) { // wypisywanie tali do dokładania
                Console.Write("▒▒");
            } else if (i == 1) { 
                if (cards.LeftTile[cards.LeftTilePosition] % 2 == 1) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(GetCardSymbol(cards.LeftTile[cards.LeftTilePosition]) + "");
            } 

            for (int j = 0; j < 7; j++) // wypisywwaanie środkowych tali
            {
                if (cards.CentralCards[j, i] == 0)
                {
                    Console.Write("\t");
                    continue;
                }
                string Symbol = GetCardSymbol(cards.CentralCards[j, i]);
                if (cards.CentralCards[j, i] % 2 == 1) // sprawdzanie jaki karta ma kolor
                {
                    if (Symbol != "??") Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Black;
                } else {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t" + Symbol);
            }

            if (i % 2 == 0) { // rysowanie tali końcowych
                int id = i / 2;
                if (id <= 3) { 
                    byte StosCard = cards.RightTiles[id];
                    if (StosCard == 0) {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t▒▒");
                    } else {
                        if (StosCard % 2 == 1) {
                            Console.ForegroundColor = ConsoleColor.Red;
                        } else {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write("\t" + GetCardSymbol(StosCard));
                    }
                }
            }

            Console.Write("\n");
        }
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
    }

    public static string GetCardSymbol(byte id) // branie obrazu karty
    {
        byte CardNumber = (byte)(id / 10); // wartość karty 1-13
        byte CardID = (byte)(id % 10);  // kolor 1-4

        string CardTag = "";
        if (CardNumber != 1 && CardNumber <= 10) CardTag = "" + CardNumber;
        else {
            switch (CardNumber)
            {
                case 1: {
                    CardTag = "A";
                    break;
                }
                case 11: {
                    CardTag = "J";
                    break;
                }
                case 12: {
                    CardTag = "Q";
                    break;
                }
                case 13: {
                    CardTag = "K";
                    break;
                }
            }
        }

        string CardSymbol;
        switch (CardID) { 
            case 1: {
                CardSymbol = "♥";
                break;
            }
            case 2: {
                CardSymbol = "♣";
                break;
            }
            case 3: {
                CardSymbol = "♦";
                break;
            }
            case 4: {
                CardSymbol = "♠";
                break;
            }
            default: {
                return "??";
            }
        }

        return CardTag + CardSymbol;
    }
}