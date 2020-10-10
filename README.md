# Studentby
## Úkoly
- vlastní JWT middleware a refresh token??
- striky a bany
- filtery a sorty pro endpointy
- auto mapping
- SMTP
- najít práce a zdroje
- validace DTO
- testing (unit, integration)
---------------------------------------------------------
## Procesy
### Ostatní
- **login**
    - 🐌 přidat refresh token
### Student
- **registrace**
    - 🐌 unikátní email (možná už dělá EF)
- **prohlížení nabídek**
    - ✔ skýt minulé
    - ✔ skrýt již přihlášené
- **přihlášení k nabídce**
    - ✔ pokud je na nabídce ještě volné místo
    - ✔ pokud již nezačala
    - ✔ pokud se nekryje s jinou přihláškou
    - ✔ pokud se na ni student již nepřihlásil
- **zrušení přihlášky**
    - ✔ pokud je nezpracovaná (pending)
    - ❗ pokud je přijatá, tak pouze za strike
- **prohlížení odměn**
    - 🐌 vymyslet koncept
- **prohlížení účtu**
    - 🐌 vymyslet koncept
- **úprava účtu**
    - 🐌 vymyslet koncept
### Operátor
- **vytvoření skupiny**
    - 🐌 přidat další informace o skupině (info, adresa, typ, kontakt)
- **přidání účtu ke skupině**
    - 🐌 generace dočasného hesla - poslání mailem
    - 🐌 unikátní email (možná dělá EF)
- **aktivace studenta**
    - 🐌 pokud nemá ban
    - ✔ pokud je deaktivovaný
- **přijetí/zamítnutí přihlášky**
    - ✔ pokud je nezpracovaná (pending)
    - ✔ pokud nabídka již nezačala
- **zapsání práce studenta**
    - ✔ pokud je validní přechod stavu přihlášky
    - ✔ pokud práce již skončila
- **deaktivace studenta**
    - ✔ zrušení přijatých a nezpracovaných přihlášek -> zamítnuto
- **úprava skupiny**
    - 🐌 vymyslet koncept
- **deaktivace zákazníka**
    - 🐌 vymyslet koncept
### Zákazník
- **vytvoření nabídky**
    - 🐌 min. 3 dny do budoucna -> DTO
    - 🐌 max. 3 měsíce do budoucna -> DTO
- **zrušení nabídky**
    - ❗ strike - pokud jsou přijaté přihlášky
- **prohlížení účtu**
    - 🐌 vymyslet koncept
- **prohlížení skupiny**
    - 🐌 vymyslet koncept
- **úprava účtu**
    - 🐌 vymyslet koncept
---------------------------------------------------------
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
- scheduler
    - upozornění na nezpracované příhlášky (app i att)
    - platby -> jobApplication state = paid

## Nápady
- We stand by by STUDENTBY
- Things you can buy thanks to STUDENTBY

## Brainstrom
- ✔ rozdělení controllerů podle rolí, aby bylo umožněno pro různé role různé DTO na requestu
    - NEE!! DTO na requestu nikdy není jiné
    - pro DTO na response využít Interface
- ✔ samotnou logiku dělá pouze services
- ✔ nelze upravovat nabídku - složitá BL
    - mohou pouze zrušit
- ✔ pro potvrzení práce využít jobApplication
    - přibylo by: počet hodin a stavy (attended, unattended)
    - PROČ NE?
        - komplikovanost úpravy
- ❓ vystavovat nový endpoint jen pro o trochu větší DTO ?
    - JobApplicationService - GetResult vs. GetDetailOperator
- ❓ související logika v /entities?
    - např. jobOffer free space nebo nechat v _jobOfferService
    - spíš v service -> entity by musela mít přístup k dbContext