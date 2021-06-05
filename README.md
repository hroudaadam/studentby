# Studentby
## Analýza
### Ostatní
- ✅ **login** ( POST /login) 
### Student
- ✅ **registrace** (POST /students)
    - ✅ vytvoří User s rolí StudentInact
    - ✅ ověřit unikátnost emailu
- ✅ **seznam nabídek** (GET /job-offers)
- ✅ **detail nabídky** (GET /job-offers/1/student-view)
- ✅ **přihlášení k nabídce** (POST /job-applications)
    - ✅ pokud je na nabídce ještě volné místo
    - ✅ pokud nabídka již nezačala
    - ✅ pokud se přihláška nekryje s jinou přihláškou
    - ✅ pokud se na ni student již nepřihlásil
- ✅ **seznam přihlášek** (GET /job-applications/student-view) 
- ✅ **detail přihlášky** (GET /job-applications/1/student-view)
    - ✅ pokud přihláška patří studentovi
- ✅ **zrušení přihlášky** (DELETE /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
- ✅ **prohlížení účtu**
### Operátor
- ✅ **seznam skupin** (GET /groups)
- ✅ **detail skupiny** (GET /groups/1)
    - ✅ seznam uživatelů
- ✅ **vytvoření skupiny** (POST /groups)
- ✅ **přidání účtu ke skupině** (POST /customers)
    - ✅ generace dočasného hesla
- ✅ **seznam studentů** (GET /students) 
- ✅ **detail studenta** (GET /students/1)
- ✅ **aktivace studenta** (PUT /students/1)
    - ✅ pokud je deaktivovaný
- ✅ **deaktivace studenta** (PUT /students/1)
    - ✅ pokud je aktivovaný
- ✅ **seznam přihlášek** (GET /job-applications/operator-view) 
- ✅ **detail přihlášky** (GET /job-applications/1/operator-view)
- ✅ **přijetí přihlášky** (PUT /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
    - ✅ pokud nabídka již nezačala
    - ✅ pokud je volné místo
- ✅ **zamítnutí přihlášky** (PUT /job-applications/1)
    - ✅ pokud je nezpracovaná (pending)
    - ✅ pokud nabídka již nezačala
- ✅ **seznam nabídek** (GET /job-offers)
- ✅ **detail nabídky** (GET /job-offers/1/operator-view)
    - ✅ seznam přihlášek
- ✅ **zapsání práce studenta** (PUT /job-applications/1)
    - ✅ pokud je validní přechod stavu přihlášky
    - ✅ pokud práce již skončila
### Zákazník
- ✅ **seznam nabídek** (GET /job-offers/customer-view)
    - ✅ pouze patřící dané skupině
- ✅ **detail nabídky** (GET /job-offers/1/customer-view)
    - ✅ pokud patří dané skupině
- ✅ **vytvoření nabídky** (POST /job-offers)
- ✅ **smazání nabídky** (DELETE /job-offers)
    - ✅ nabídka musí patřit skupině
    - ✅ nabídka ještě nezapočala
    - ✅ nabídka nemá potvrzené přihlášky
- ✅ **prohlížení účtu**
---------------------------------------------------------
## Možné dodělávky
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- response cache
- auto mapping
- časové informace o vzniku a modifikaci entit
- informace o autorovi pracovní nabídky (customer)
- striky a bany
- refresh token
- vlastní JWT middleware - pro customizace error response
- promyslet hodnocení
    - zrušení nabídky
    - nenastoupení na práci
    - pozdní zrušení přihlášky
    - celkové bodování studenta i skupiny
- scheduler
    - upozornění na nezpracované příhlášky
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
- ✅ související logika v /entities?
    - např. jobOffer free space nebo nechat v _jobOfferService
    - v service -> entity by musela mít přístup k dbContext
- ✅ vystavovat nový endpoint pro data, která lze získat pomocí dvou endpointů? 
    - ano, spíš vystavovat nový
---------------------------------------------------------
## Brand
- We stand by by Studentby
- Thing you can buy thanks to Studentby

## Účty
- student
    - jan.novy@abc.cz (heslo123)
    - eva.mala@abc.cz (heslo123)
    - vit.sery@abc.cz (heslo123)
    - ales.opl@abc.cz (heslo123)
    - dan.tuhy@abc.cz (heslo123)
- operator
    - operator@abc.cz (heslo123)
- customer 
    - iva.leva@abc.cz (heslo123)
    - vit.jakl@abc.cz (heslo123)    
    - jan.rudy@abc.cz (heslo123)
    - tran.vu@abc.cz (heslo123)

## Deploy
- SQL
    - studentby-sql-server
    - studentby-admin
    - GS2.yae1q
- server
    - DESKTOP-TH6CF4J\SQLEXPRESS
    - studentby.database.windows.net
- connStr (azure)
    Data Source=studentby.database.windows.net;User ID=studentby-admin;Password=GS2.yae1q;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

- connStr (local)
    Data Source=DESKTOP-TH6CF4J\\SQLEXPRESS;Initial Catalog=StudentbyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
