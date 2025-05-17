class CardTransferFunctions {
    public static byte[] readFromCenter(Cards cards, NotationParamiters paramiters) { // wyczytuje karty z środka
        int TakeAmaunt = paramiters.TakeAmaunt;
        int Column = paramiters.TakeColumn - 1;

        int LenCards = UnderstundNotation.CountCentralCards(cards.CentralCards, Column);

        byte[] CardsToTake = new byte[TakeAmaunt];

        if (TakeAmaunt > LenCards) {
            Console.WriteLine("Za mało kart do przeniesienia z kolumny " + (char)(paramiters.TakeColumn + 'A'));
            return CardsToTake;
        }

        for (int i = 0; i < TakeAmaunt; i++) {
            int index = LenCards - TakeAmaunt + i;
            CardsToTake[i] = cards.CentralCards[Column, index];
        }
        return CardsToTake;
    }

    public static void clearFromCenter(Cards cards, NotationParamiters paramiters) { // wyczyscza miejsce po starych kartach
        int TakeAmaunt = paramiters.TakeAmaunt;
        int Column = paramiters.TakeColumn - 1;

        int LenCards = UnderstundNotation.CountCentralCards(cards.CentralCards, Column);

        if (TakeAmaunt > LenCards) {
            return;
        }

        for (int i = 0; i < TakeAmaunt; i++) {
            int index = LenCards - TakeAmaunt + i;
            cards.CentralCards[Column, index] = 0;
        }
    }

    public static bool PutInCenter(Cards cards, NotationParamiters paramiters, byte[] cards_taken) { // kładzie karty na swoje nowe miensca jeżeli są zgodne z zasadamu
        int CountOfCards = UnderstundNotation.CountCentralCards(cards.CentralCards, paramiters.PutColumn - 1);

        // zasady kładzenia na środku

        byte last = cards_taken[0];
        
        if (CountOfCards == 0) {
            if (last - last % 10 != 130) return false;
        } else {
            byte put = cards.CentralCards[paramiters.PutColumn - 1, CountOfCards - 1];
            if (last % 2 == put % 2) return false; // sprawdzanie czy mają ten sam kolor
            if (last - last % 10 != put - put % 10 - 10) return false; // sprawdzanie czy mniejszy numer
            if (last % 10 > 4) return false; // sprawdzanie czy jest odkrytą kartą
        }

        for (int i = 0; i < cards_taken.Length; i++) {
            cards.CentralCards[paramiters.PutColumn - 1, i + CountOfCards] = cards_taken[i];
        }
        return true;
    }

    public static byte[] readFromLeft(Cards cards, NotationParamiters paramiters) { // czyta z tali do dokładania
        if (cards.LeftTile[cards.LeftTilePosition] == 0) { Console.WriteLine("zabrakło kart z lewej kolumny"); return new byte[0]; }
        return new byte[1] { cards.LeftTile[cards.LeftTilePosition] };
    }

    public static void clearFromLeft(Cards cards, NotationParamiters paramiters) { // wyczyscza z tali do dokładania
        cards.LeftTile[cards.LeftTilePosition] = 0;
        UnderstundNotation.FlipLeftTile(cards);
    }
    
    

    public static bool PutInRight(Cards cards, NotationParamiters paramiters, byte[] cards_taken) { // kładzie na stosah końcowych
        // sprawdzanie czy poprawne z zasadami
        // sprawdzanie czy pojedyńcza karta w "checkforerrors" w notation
        Console.WriteLine("to rith");
        byte card = cards_taken[0];
        Console.WriteLine("valid 1: " + card);
        Console.WriteLine("Debug 1: " + (card % 10 % 4));
        byte put = cards.RightTiles[card % 10 % 4];
        Console.WriteLine("valid 2: " + put);

        if (card % 10 > 4) return false;
        //sprawdzanie czy jest pierwszą kartą
        if (put == 0) {
            if (card - card % 10 != 10) return false; // sprawdzanie czy jest asem
        } else {
            if (card != put + 10) return false; // sprawdzanie czy karta jest większa
        }
        Console.WriteLine("confirm");
        cards.RightTiles[cards_taken[0] % 10 % 4] = cards_taken[0];
        return true;
    }
}