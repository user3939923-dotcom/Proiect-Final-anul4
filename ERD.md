#Diagrama Entity-Relationship — RestoPro

##Entități și atribute

+---------------------+        +---------------------+
|        MASA         |        |       PRODUS        |
+---------------------+        +---------------------+
| PK IdMasa INT       |        | PK IdProdus INT     |
|    NumarMasa INT    |        |    Denumire VARCHAR |
|    Capacitate INT   |        |    Categorie VARCHAR|
|    Zona VARCHAR     |        |    Pret DECIMAL     |
+---------------------+        |    Disponibil BIT   |
           |                   +---------------------+
           |                              |
           | 1                            | 1
           |                              |
           | N                            | N
           +--------------+---------------+
                          |
                          v
               +----------------------+
               |       COMANDA        |
               +----------------------+
               | PK IdComanda INT     |
               | FK IdMasa INT        |
               | FK IdProdus INT      |
               |    DataComanda DATE  |
               |    Cantitate INT     |
               |    StatusPlata VARCH |
               +----------------------+

##Relații
-MASA (1) ────────── (N) COMANDA
O masă poate avea mai multe comenzi înregistrate de-a lungul timpului.

-PRODUS (1) ────────── (N) COMANDA
Un produs poate apărea în mai multe comenzi diferite (fiind comandat de mai mulți clienți).