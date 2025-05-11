using System;
using System.Diagnostics;

class notationPrams {
    public byte TakeColumn = 0;
    public byte TakeAmaunt = 0;
    public byte PutColumn = 0;
}

class UnderstundNotation {
    public static void UnderstandNotation(string notation, Cards cards) {
        try {
            if (notation == "ShowAll") {
                for (int i = 0; i < 7; i++) {
                    for (int j = 0; j < 20; j++) {
                        byte loko = cards.CentralCards[i, j];
                        if (loko % 10 > 4) {
                            cards.CentralCards[i, j] = (byte)(loko - 4);
                        }
                    }
                }
                return;
            }

            if (notation.ToLower() == "f") {
                cards.ShownId++;
                int i = 0;
                while (cards.LeftTile[cards.ShownId] == 0 && i <= 30) {
                    i++;
                    if (cards.ShownId >= cards.LeftTile.Length) {
                        cards.ShownId = 0;
                        CardGenerator.ShuffleCards(cards.LeftTile);
                    }
                    cards.ShownId++;
                }
                return;
            }

            notationPrams prams = GeneratePrams(notation);
            if (prams != null) {
                byte[] take;
                if (prams.TakeColumn == 0) take = TakeFromLeft(cards, prams);
                else take = TakeFromCenter(cards, prams);

                if (prams.PutColumn == 8) PutInRight(cards, prams, take);
                else PutInCenter(cards, prams, take,true);
            }

        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }


    static byte[] TakeFromCenter(Cards cards, notationPrams prams) {
        int amanty = prams.TakeAmaunt;
        int col = prams.TakeColumn - 1;

        int lencards = CountCentralCards(cards.CentralCards, col);

        byte[] cardos = new byte[amanty];

        if (amanty > lencards) {
            Console.WriteLine("Za mało kart do przeniesienia z kolumny " + col);
            return cardos;
        }

        for (int i = 0; i < amanty; i++) {
            int index = lencards - amanty + i;
            cardos[i] = cards.CentralCards[col, index];
            cards.CentralCards[col, index] = 0;
        }

        return cardos;
    }


    static byte[] TakeFromLeft(Cards cards, notationPrams prams) {
        cards.ShownId++;
        byte[] dedos = new byte[1] { cards.LeftTile[cards.ShownId - 1] };
        cards.LeftTile[cards.ShownId - 1] = 0;
        return dedos;
    }

    static void PutInCenter(Cards cards, notationPrams prams, byte[] cards_taken,bool checkforcorect) {
        int coun = CountCentralCards(cards.CentralCards, prams.PutColumn - 1);
        for (int i = 0; i < cards_taken.Length; i++) {
            cards.CentralCards[prams.PutColumn - 1, i + coun] = cards_taken[i];
        }
    }
    static void PutInRight(Cards cards, notationPrams prams, byte[] cards_taken) {
        if (cards_taken.Length > 1) { 
            //TODO: error handeling
        } else {
            //TODO: error handeling again
            cards.RightTiles[cards_taken[0] % 10 % 4] = cards_taken[0];
        }
    }

    static void PutBackLeft(Cards cards, notationPrams prams) {
    }

    static notationPrams GeneratePrams(string notation) { 
        notationPrams prams = new notationPrams();
        try {
            char from = char.ToUpper(notation[0]);
            char to = char.ToUpper(notation[1]);
            byte take = byte.Parse(notation.Substring(2));

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
    

    static void TransferCard_old(string notation, Cards cards) {

        if (notation.ToLower() == "f") {
            cards.ShownId++;
            int i = 0;
            while (cards.LeftTile[cards.ShownId] == 0 && i <= 30) {
                i++;
                if (cards.ShownId >= cards.LeftTile.Length) {
                    cards.ShownId = 0;
                    CardGenerator.ShuffleCards(cards.LeftTile);
                }
                cards.ShownId++;
            }
        }

        Cards result = cards;

        // Rozbij notację (np. "BC2")
        char from = char.ToUpper(notation[0]);
        char to = char.ToUpper(notation[1]);
        int take = int.Parse(notation.Substring(2));

        int fromCol = from - 'B';
        int toCol = to - 'B';

        if (fromCol == toCol) return;

        int fromCount;

        // Liczba kart w kolumnach
        if (fromCol == -1)
            fromCount = -1;
        else fromCount = 0;// CountCards(result.CentralCards, fromCol);
        int toCount = 0;// CountCards(result.CentralCards, toCol);

        if (take > fromCount && fromCount != -1) {
            Console.WriteLine("Za mało kart do przeniesienia z kolumny " + from);
            return;
        }



        // Pobierz karty z końca kolumny źródłowej
        byte motor = result.CentralCards[fromCol, fromCount - take];
        bool shouldPlace = false;
        if (toCount - 1 < 0) {
            shouldPlace = (motor - motor % 10 == 130);
        } else {
            byte target = result.CentralCards[toCol, toCount - 1];
            shouldPlace = (motor % 10 % 2 != target % 10 % 2) &&
                          (motor - motor % 10 + 10 == target - target % 10) &&
                          (motor % 10 <= 4);
            if (!shouldPlace) {
                Console.WriteLine(notation + " To nielegalny Ruch");
                return;
            }
        }

        if (shouldPlace) {
            byte[] toMove = new byte[take];
            if (fromCount == -1) {
                toMove[0] = result.LeftTile[result.ShownId];
                result.LeftTile[result.ShownId] = 0;
                cards.ShownId++;
            } else {
                for (int i = 0; i < take; i++) {
                    int index = fromCount - take + i;
                    toMove[i] = result.CentralCards[fromCol, index];
                    result.CentralCards[fromCol, index] = 0;
                }
            }


            for (int i = 0; i < take; i++) {
                result.CentralCards[toCol, toCount + i] = toMove[i];
            }
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
