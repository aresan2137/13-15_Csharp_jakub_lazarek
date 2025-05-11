class CardGenerator
{
    public static Cards GenerateCards()
    { // z wszystkig możliwyh kart pżydziela kture są gdzie
        Cards cards = new Cards();
        byte[] freecards = GenerateShufledCards(); // Generowanie wszystkih kart
        byte free = 0;
        cards.CentralCards = new byte[7, 20];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                cards.CentralCards[i, j] = (byte)(freecards[free++] + ((j == i) ? 0 : 4));

            }
        }
        cards.LeftTile = new byte[52 - free];
        for (int i = free; i < 52; i++)
        {
            cards.LeftTile[i - free] = freecards[i];
        }
        cards.RightTiles = new byte[4];
        cards.ShownId = 0;
        return cards;
    }

    public static byte[] GenerateShufledCards() { 
        return ShuffleCards(ShuffleCards(ShuffleCards(GeneratePosibleCards())));
    }

    public static byte[] ShuffleCards(byte[] cards)
    {
        Random rand = new Random();
        int n = cards.Length;

        // Algorytm Fishera-Yatesa
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1); // Losowanie indeksu od 0 do i
                                         // Zamiana miejscami
            byte temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }

        return cards;  // Zwracamy pomieszaną tablicę
    }



    static byte[] GeneratePosibleCards()
    {
        byte[] cards = new byte[52];
        byte index = 0;
        for (int i = 1; i < 14; i++)
            for (int j = 1; j < 5; j++)
            {
                cards[index++] = (byte)(i * 10 + j);
            }

        return cards;
    }
}
