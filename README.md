# Studentby
## TODO - Prio
- úprava JobApplication
## TODO - Backlog
- promyslet datamodel po vykonání práce (počet hodin,...)
- SMTP
- filtery a sorty pro endpointy
- UNIT testy
- BE - logger
- BE - anti POST spamming
- UI načítání
## Brainstrom
- rozdělení controllerů podle rolí, aby bylo umožněno pro různé role různé DTO na requestu
    - NEE!! DTO na requestu nikdy není jiné
    - pro DTO na response využít Interface
- samotnou logiku dělá pouze services
- nelze upravovat nabídku - složitá BL
    - mohou pouze zrušit
- pro potvrzení práce využít jobApplication
    - přibylo by: počet hodin a stavy (attended, unattended)
    - PROČ NE?
        - komplikovanost úpravy
## Analýza - pohled studenta
- registrace
    - BE
        - ověřovací email
        - validace DTO
        - unikátní email
- login
    - BE
        - refresh token
    - FE
        - zobrazit roli
- prohlížení nabídek
    - FE
        - stránkování, filtry, sorty
    - BE
        - stránkování, filtry, sorty
- přihlášení k nabídce
- zrušení přihlášky
- prohlížení odměn
- prohlížení účtu
- úprava účtu
## Analýza - pohled operatora
- login
- vytvoření skupiny
    - BE
        - další informace (info, adresa, typ, kontakt)
- přidání účtu ke skupině
    - BE
        - generace hesla - poslání mailem
- aktivace studenta
- přijetí/zamítnutí přihlášky
- zapsání práce studenta (hodiny, stav)
- deaktivace studenta
    - BE
        - zrušení přihlášek
- úprava skupiny
- deaktivace zákazníka
## Analýza - pohled zákazníka
- login
- vytvoření nabídky
- zrušení nabídky
    - BE
        - strike - pokud jsou přihlášky
- prohlížení nabídek skupiny
- prohlížení účtu
- prohlížení skupiny
- úprava účtu
## Možné dodělávky
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- promyslet hodnocení
    - zrušení nabídky
    - nenastoupení na práci
    - pozdní zrušení přihlášky
    - celkové bodování studenta i skupiny

