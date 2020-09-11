# Studentby
## TODO - nejbližší
- úprava nabídky
- smazání nabídky
- úprava skupiny
- smazání skupiny
- zrušení účtu
## TODO - dlouhodové
- úprava JobApplication - stavy (Done, Issue), počet hodin
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- promyslet hodnocení
- promyslet datamodel po vykonání práce (počet hodin,...)
- filtery a sorty pro endpointy
## Poznámky
- rozdělení controllerů podle rolí, aby bylo umožněno pro různé role různé DTO na requestu
    - NEE!! DTO na requestu nikdy není jiné
    - pro DTO na response využít Interface
- samotnou logiku dělá pouze services