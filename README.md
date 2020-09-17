# Studentby
## TODO - Prio
- aktivace studenta
- úprava skupiny
- smazání skupiny
- zrušení účtu
## TODO - Backlog
- promyslet finance pro zákazníka (balíčky - počet nabídek/míst na měsíc)
- promyslet hodnocení
    - zrušení nabídky
    - nenastoupení na práci
    - pozdní zrušení přihlášky
    - celkové bodování studenta i skupiny
- promyslet datamodel po vykonání práce (počet hodin,...)
- filtery a sorty pro endpointy
- UI načítání
- UNIT testy
- BE - logger
- BE - anti POST spamming
## Brainstrom
- rozdělení controllerů podle rolí, aby bylo umožněno pro různé role různé DTO na requestu
    - NEE!! DTO na requestu nikdy není jiné
    - pro DTO na response využít Interface
- samotnou logiku dělá pouze services
- nelze upravovat nabídku - složitá BL
    - mohou pouze zrušit