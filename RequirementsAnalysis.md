# Analiza Cerințelor — RestoPro

## Entități principale
- **Masa**: IdMasa, NumarMasa, Capacitate, Zona
- **Produs**: IdProdus, Denumire, Categorie, Pret, Disponibil
- **Comanda**: IdComanda, IdMasa (FK), IdProdus (FK),
               DataComanda, Cantitate, StatusPlata

## Cerințe funcționale
1. Proiectarea și popularea bazei de date
2. Interfața aplicației (WPF, navigare)
3. Gestionarea meselor (CRUD + căutare)
4. Gestionarea produselor din meniu (CRUD + filtrare)
5. Gestionarea comenzilor
6. Raport sumar
7. Validarea datelor și tratarea erorilor

## Tehnologii
- Limbaj: C# WPF (.NET)
- Bază de date: SQL Server Express
- Conexiune BD: Microsoft.Data.SqlClient