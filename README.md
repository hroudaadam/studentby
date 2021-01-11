# Studentby
## Úkoly
- filtery, sorty, stránkování pro endpointy
- vlastní JWT middleware a refresh token??
- striky a bany
- auto mapping
- SMTP
- validace DTO
- testing (unit, integration)
- dokumentace API
---------------------------------------------------------
## Analýza
### Ostatní
- **login** ( POST /login)
    - 🐌 implementovat refresh token
### Student
- **registrace** (POST /students)
    - ✅ vytvoří USER s rolí StudentInact
    - 🐌 ověřit unikátnost emailu
- **seznam nabídek** (GET /job-offers)
    - ❗ skýt minulé (nesmí nebo nemá?)
    - ❗ skrýt již přihlášené (nesmí nebo nemá?)
- **detail nabídky** (GET /job-offers/1)
    - ❗ pokud není v minulosti
- **přihlášení k nabídce** (POST /job-applications)
    - ✅ pokud je na nabídce ještě volné místo
    - ✅ pokud nabídka již nezačala
    - ✅ pokud se přihláška nekryje s jinou přihláškou
    - ✅ pokud se na ni student již nepřihlásil
- **seznam přihlášel** (GET /job-applications)
- **detail přihlášky** (GET /job-applications/1)
    - ✅ pokud přihláška patří studentovi
- **zrušení přihlášky** (DELETE /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
    - ❗ pokud je přijatá, tak pouze za strike
- **prohlížení odměn**
    - 🐌 vymyslet koncept
- **prohlížení účtu**
    - 🐌 vymyslet koncept
- **úprava účtu**
    - 🐌 vymyslet koncept
### Operátor
- **seznam skupin** (GET /groups)
- **detail skupiny** (GET /groups/1)
    - ✅ i seznam uživatelů
- **vytvoření skupiny** (POST /groups)
    - 🐌 přidat další informace o skupině (info, adresa, typ, kontakt)
    - 🐌 ověřit unikátnost názvu
- **přidání účtu ke skupině** (POST /customers)
    - 🐌 generace dočasného hesla - poslání mailem
    - 🐌 unikátní email (možná dělá EF)
- **seznam studentů** (GET /students)
- **detail studenta** (GET /students/1)
- **aktivace studenta** (PUT /students/1)
    - 🐌 pokud nemá ban
    - ✅ pokud je deaktivovaný
- **deaktivace studenta**
    - ❗ pokud je aktivovaný
    - ✅ zrušení přijatých a nezpracovaných přihlášek -> zamítnuto
- **seznam přihlášek** (GET /job-applications)
    - ❓ možnost filtru na pouze nezracované 
- **detail přihlášky** (GET /job-applications/1)
- **přijetí přihlášky** (PUT /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
    - ✅ pokud nabídka již nezačala
    - ❗ pokud je volné místo
- **zamítnutí přihlášky** (PUT /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
    - ✅ pokud nabídka již nezačala
- **seznam nabídek** (GET /job-offers)
- **detail nabídky** (GET /job-offers/1)
- **zapsání práce studenta** (PUT /job-applications/1)
    - ✅ pokud je validní přechod stavu přihlášky
    - ✅ pokud práce již skončila
- **úprava skupiny** (PUT /groups/1)
    - 🐌 vymyslet koncept
- **deaktivace zákazníka** 
    - 🐌 vymyslet koncept
### Zákazník
- **seznam nabídek** (GET /job-offers)
    - ✅ pouze patřící dané skupině
- **detail nabídky** (GET /job-offers/1)
    - ✅ pokud patří dané skupině
- **vytvoření nabídky** (POST /job-offers)
    - 🐌 min. 3 dny do budoucna -> DTO
    - 🐌 max. 3 měsíce do budoucna -> DTO
- **prohlížení účtu**
    - 🐌 vymyslet koncept
- **prohlížení skupiny**
    - 🐌 vymyslet koncept
- **úprava účtu**
    - 🐌 vymyslet koncept
---------------------------------------------------------
## Endpointy
- POST /login
- POST /students

- ! GET /job-offers
    - 
- ! GET /job-offers/1
- POST /job-offers

- ! GET /job-applications
- ! GET /job-applications/1
- POST /job-applications
- PUT /job-applications/1
- DELETE /job-applications/1

- GET /groups
- GET /groups/1
- POST /groups
- PUT /groups/1

- POST /customers

- GET /students
- GET /students/1
- PUT /students/1

---------------------------------------------------------
## Možné dodělávky
- chybový response jako JSON
- mail client
- validace vstupů na serveru
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- promyslet hodnocení
    - zrušení nabídky
    - nenastoupení na práci
    - pozdní zrušení přihlášky
    - celkové bodování studenta i skupiny
- scheduler
    - upozornění na nezpracované příhlášky (app i att)
    - platby -> jobApplication state = paid
---------------------------------------------------------
## Brainstrom
- ✅ rozdělení controllerů podle rolí, aby bylo umožněno pro různé role různé DTO na requestu
    - NEE!! DTO na requestu nikdy není jiné
    - pro DTO na response využít Interface
- ✅ samotnou logiku dělá pouze services
- ✅ nelze upravovat nabídku - složitá BL
    - mohou pouze zrušit
- ✅ pro potvrzení práce využít jobApplication
    - přibylo by: počet hodin a stavy (attended, unattended)
    - PROČ NE?
        - komplikovanost úpravy
- ❓ vystavovat nový endpoint jen pro o trochu větší DTO ?
    - JobApplicationService - GetResult vs. GetDetailOperator
- ❓ související logika v /entities?
    - např. jobOffer free space nebo nechat v _jobOfferService
    - spíš v service -> entity by musela mít přístup k dbContext