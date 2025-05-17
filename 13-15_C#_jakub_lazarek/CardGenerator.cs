class CardGenerator
{
    public static Cards GenerateCards()
    { // z wszystkig możliwyh kart pżydziela kture są gdzie
        Cards cards = new Cards();
        byte[] FreeCards = GenerateShufledCards();
        byte CardNumber = 0;
        cards.CentralCards = new byte[7, 20];
        for (int i = 0; i < 7; i++) {
            for (int j = 0; j <= i; j++) {
                cards.CentralCards[i, j] = (byte)(FreeCards[CardNumber++] + ((j == i) ? 0 : 4));

            }
        }
        cards.LeftTile = new byte[25];
        for (int i = CardNumber; i < 52; i++) {
            cards.LeftTile[i - CardNumber] = FreeCards[i];
        }
        cards.RightTiles = new byte[4];
        cards.LeftTilePosition = 0;
        return cards;
    }

    public static byte[] GenerateShufledCards() {
        Random rand = new Random();
        byte[] car = GeneratePosibleCards();

        for (int i = 0; i < rand.Next(1,19); i++) {
            car = ShuffleCards(car);
        }
        return car;
    }

    public static byte[] ShuffleCards(byte[] cards)
    {
        Random rand = new Random();
        int n = cards.Length;

        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1); 
                                         
            byte temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }

        return cards;
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
