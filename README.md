## Program Pasjans w Konsoli
- w nazwie repozytorium powinno być C# ale github nie może tego używać więc jest Csharp
### Odpalenie Programu
1. najpierw trzeba pobrać repozytorium jako zip, czyli trzeba najechać myszką zielony przycisk "code" i wybrać opcje pobierania zip.
2. następnie trzeba odpakować zip.
3. potem trzeba otworzyć folder projektu, który powinien się znajdować "13-15_C#_jakub_lazarek.sln" i następnie otworzyć ten plik.
4. następnie na górze ekranu rozwinąć panel gdzie pisze "Debug" i wybrać opcje "Relace"
5. następnie trzeba nacisnąć szarą strzałkę z zielonym obramowaniem na prawo od panelu i projekt powinien się odpalić (zamiast tego można nacisnąć control + f5).
### sterowanie
- kolumna A to talia do dokładania
- kolumny od B do H to kolumny środkowe
- kolumna I to talie końcowe
#### Przenoszenie kart
jeżeli chcemy przenieść na przykład z kolumny b do kolumny g to trzeba napisać bg działa z innymi cyframi.  
Jeśli chcemy przenieść kilka kart to po np. bg trzeba napisać ilość kart np. bg2, aby przenieść 2 karty z b do g.
#### odkrywanie kart z tali do dobierania  
aby odsłonić następną karte z tali dobierania tżeba napisać "f"
#### odkładanie kart ta stos końcowy  
aby przenieść na stosy końcowe tżeba dać do kolumny I i program wybierze miejsce dla tej karty.
#### reszta
* "ShowAll" odkrywa wszystkie ukryte karty.
* "exit" wyłącza gre
* "restart" restartuje gre
### Wytłumaczenie kodu
#### Przechowywanie kart
ten program przechowuje karty w byte  
karty są przechowywane, że 2 ostatnie cyfry oznaczają kartę a pierwsza cyfra oznacza numer.  
Numer karty jest przeorywany od 1 do 9.  
numery 1 - 4 to odsłonięte karty a 5 - 9 to zakryte karty  
aby odsłonić karte wystarczy odjąć 4.  
np. 123 to K
#### funkcje
Większość kodu ma komentarze.
* plik "Program"
ma w sobie wejście programu.
* plik "CardGenerator"
generuje wszystkie karty i je miesza.
* plik "CardRenderer"
wypisuje wszystkie karty do konsoli.
* plik "UnderstundNotation" i "CardTransferFunctions"
zajmują się zrozumieniem i wykonaniem komend użytkownika.
