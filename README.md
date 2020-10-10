# Studentby

## TODO - Prio

## TODO - Backlog
- filtery a sorty pro endpointy
- auto mapping
- UI načítání
- SMTP
- najít práce a zdroje
- validace DTO
- testing (unit, integration)
-------------------------------------------
## Procesy
### Ostatní
- **login**
    - přidat refresh token
### Student
- **registrace**
    - unikátní email (možná už dělá EF)
- **prohlížení nabídek**
    - skrýt již naplněné
    - přidat stránkování, třídění a filtry
    - ✔ skýt minulé
    - ✔ skrýt již přihlášené
- **přihlášení k nabídce**
    - pokud je na nabídce ještě volné místo
    - pokud již nezačala
    - ✔ pokud se nekryje s jinou přihláškou
    - ✔ pokud se na ni student již nepřihlásil
- **zrušení přihlášky**
    - ✔ pokud je nezpracovaná (pending)
    - pokud je přijatá, tak pouze za strike
- **prohlížení odměn**
    - vymyslet koncept
- **prohlížení účtu**
    - vymyslet koncept
- **úprava účtu**
    - vymyslet koncept

### Operátor
- **vytvoření skupiny**
    - unikátní název (možná dělá EF)
    - přidat další informace o skupině (info, adresa, typ, kontakt)
- **přidání účtu ke skupině**
    - generace dočasného hesla - poslání mailem
    - unikátní email (možná dělá EF)
- **aktivace studenta**
    - pokud nemá ban ?
    - pokud je deaktivovaný
- **přijetí/zamítnutí přihlášky**
- **zapsání práce studenta (hodiny, stav)**
- **deaktivace studenta**
    - zrušení přihlášek
- **úprava skupiny**
- **deaktivace zákazníka**

### Zákazník
- **vytvoření nabídky**
- **zrušení nabídky**
    - strike - pokud jsou přihlášky
- **prohlížení nabídek skupiny**
- **prohlížení účtu**
- **prohlížení skupiny**
- **úprava účtu**

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