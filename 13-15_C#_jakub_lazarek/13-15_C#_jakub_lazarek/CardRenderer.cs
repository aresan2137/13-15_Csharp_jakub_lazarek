class CardRenderer
{
    public static void DrawGame(Cards cards) { // rysowanie kart w kąsoli
        string[] leftpile = { "▒▒", GetCardSymbol(cards.LeftTile[cards.ShownId]) + "" }; // left pile render
        string[] center = new string[20];
        Console.WriteLine("A\tB\tC\tD\tE\tF\tG\tH\tI");
        Console.BackgroundColor = ConsoleColor.White;
        for (int i = 0; i < 20; i++)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            if (i == 0 || i == 1) {
                if (i == 1 && cards.LeftTile[cards.ShownId]  % 2 == 1) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(leftpile[i]);
            } 
            for (int j = 0; j < 7; j++)
            {
                if (cards.CentralCards[j, i] == 0)
                {
                    Console.Write("\t");
                    continue;
                }
                string symbo = GetCardSymbol(cards.CentralCards[j, i]);
                if (cards.CentralCards[j, i] % 2 == 1)
                {
                    if (symbo != "??") Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Black;
                } else {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t" + symbo);
            }
            if (i % 2 == 0) {
                int id = i / 2;
                if (id <= 3) { 
                    byte io = cards.RightTiles[id];
                    if (io == 0) {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t▒▒");
                    } else {
                        if (io % 2 == 1) {
                            Console.ForegroundColor = ConsoleColor.Red;
                        } else {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write("\t" + GetCardSymbol(io));
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
        byte cardnum = (byte)(id / 10); // wartość karty 1-13
        byte cardId = (byte)(id % 10);  // kolor 1-4

        string valueStr = "";
        if (cardnum != 1 && cardnum <= 10) valueStr = "" + cardnum;
        else {
            switch (cardnum)
            {
                case 1: {
                    valueStr = "A";
                    break;
                }
                case 11: {
                    valueStr = "J";
                    break;
                }
                case 12: {
                    valueStr = "Q";
                    break;
                }
                case 13: {
                    valueStr = "K";
                    break;
                }
            }
        }

        string suitStr = "";
        switch (cardId) { 
            case 1: {
                suitStr = "♥";
                break;
            }
            case 2: {
                suitStr = "♣";
                break;
            }
            case 3: {
                suitStr = "♦";
                break;
            }
            case 4: {
                suitStr = "♠";
                break;
            }
            default: {
                return "??";
            }
        }

        return valueStr + suitStr;
    }
}