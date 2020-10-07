# Studentby
## TODO - Prio
- dokumentace
- validace DTO
## TODO - Backlog
- UNIT testy
- SMTP
- filtery a sorty pro endpointy
- BE - anti POST spamming
- UI načítání
- najít práce a zdroje

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

## Rešerše, teorie, atd
- načtení jen části obsahu, ať je to rychlé (stránkování)
- CORS
- ukladani dat v DB v UTC formatu
- JWT, refresh
- project strucutre
- async
## Zdroje
- REST API design rulebook - kniha (obsazená)
- Modern API design with ASP.NET Core 2 (mám)
- C# 8.0 and .NET Core 3.0 - Modern Cross-Platform Development (ebook)
- Mastering ASP.NET Web API (ebook)
- Building RESTful Web Services with .NET Core (ebook)

## Možné dodělávky
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- promyslet hodnocení
    - zrušení nabídky
    - nenastoupení na práci
    - pozdní zrušení přihlášky
    - celkové bodování studenta i skupiny
- open API

## Nápady
- We stand by by STUDENTBY
- Things you can buy thanks to STUDENTBY
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