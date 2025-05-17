
class NotationParamiters { // pżehowuje parametry uzywane podczas pżenoszenia kart
    public byte TakeColumn = 0;
    public byte TakeAmaunt = 0;
    public byte PutColumn = 0;
}

class UnderstundNotation {
    public static void UnderstandNotation(string notation, Cards cards) {

        if (notation == "ShowAll") { // kod ktury odsłąnia wszystkie ukryte karty
            for (int i = 0; i < 7; i++) {
                for (int j = 0; j < 20; j++) {
                    byte CardNumber = cards.CentralCards[i, j];
                    if (CardNumber % 10 > 4) {
                        cards.CentralCards[i, j] = (byte)(CardNumber - 4);
                    }
                }
            }
            return;
        }

        if (notation.ToLower() == "f") { // odsłania następną karte z tali do dokładania
            FlipLeftTile(cards);
            return;
        }

        NotationParamiters paramiters = GenerateParamiters(notation); // analizuje notacje
        if (paramiters != null)
            paramiters = CheckIfValidParamiters(paramiters, cards); 
        if (paramiters != null) {
            try {
                // skanują karty kture sie bieże
                byte[] take;
                if (paramiters.TakeColumn == 0) take = CardTransferFunctions.readFromLeft(cards, paramiters);
                else take = CardTransferFunctions.readFromCenter(cards, paramiters);

                bool taken;
                // sprawdzają czy da sie położyć jeśli tak to kładą
                if (paramiters.PutColumn == 8) taken = CardTransferFunctions.PutInRight(cards, paramiters, take);
                else taken = CardTransferFunctions.PutInCenter(cards, paramiters, take);

                if (taken) { // jeżeli położenie sie udało to usuwają karty kture zostały wcześniej skanowane
                    if (paramiters.TakeColumn == 0) CardTransferFunctions.clearFromLeft(cards, paramiters);
                    else CardTransferFunctions.clearFromCenter(cards, paramiters);
                } else Console.WriteLine(notation + " to nielegalny ruch");
            } catch { }
        } else Console.WriteLine(notation + " to nielegalny ruch");
    }

    static NotationParamiters CheckIfValidParamiters(NotationParamiters prams, Cards cards) { // poprawia parametry
        bool pass = true;

        // wymuszanie jednej karty jeżeli dajee sie na stos końcowy
        if (prams.TakeColumn == 8) prams.TakeAmaunt = 1;

        return (pass) ? prams : null;
    }

    static NotationParamiters GenerateParamiters(string notation) { // analizuje notacje i generuje "NotationParamiters"
        NotationParamiters prams = new NotationParamiters();
        try {
            char from = char.ToUpper(notation[0]);
            char to = char.ToUpper(notation[1]);
            string tomate = notation.Substring(2);
            byte take;
            if (tomate == "") take = 1;
            else take = byte.Parse(tomate);

            byte fromCol = (byte)(from - 'A');
            byte toCol = (byte)(to - 'A');
            prams.TakeColumn = fromCol;
            prams.PutColumn = toCol;
            prams.TakeAmaunt = take;
        } catch {
            return null;
        }
        
        return prams;
    }

    public static void FlipLeftTile(Cards cards) { // odsłania lewą talie
        cards.LeftTilePosition++;
        int i = 0;
        while (true) { // jeżeli karty niema wtedy idzie do następnej ktura jest
            if (cards.LeftTile[cards.LeftTilePosition] != 0) break;
            if (cards.LeftTilePosition >= 24) cards.LeftTilePosition = 0;
            if (i > 30) break;
            i++;
            cards.LeftTilePosition++;
        }
    }

    public static int CountCentralCards(byte[,] central, int col) {
        for (int i = 0; i < central.GetLength(1); i++) {
            if (central[col, i] == 0)
                return i;
        }
        return central.GetLength(1);
    }
}
