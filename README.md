# 🍽️ RestoPro

> Aplicație desktop pentru gestionarea completă a unui restaurant — mese, meniu și comenzi.

![Platform](https://img.shields.io/badge/Platform-Windows-blue?style=flat-square)
![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat-square&logo=csharp)
![Framework](https://img.shields.io/badge/Framework-WPF%20.NET-512BD4?style=flat-square&logo=dotnet)
![Database](https://img.shields.io/badge/Database-SQL%20Server%20Express-CC2927?style=flat-square&logo=microsoftsqlserver)
![Status](https://img.shields.io/badge/Status-In%20Development-yellow?style=flat-square)

---

## 📋 Cuprins

- [Despre Proiect](#-despre-proiect)
- [Funcționalități](#-funcționalități)
- [Tehnologii](#-tehnologii)
- [Structura Proiectului](#-structura-proiectului)
- [Baza de Date](#-baza-de-date)
- [Instalare și Configurare](#-instalare-și-configurare)
- [Utilizare](#-utilizare)
- [Planificare](#-planificare)

---

## 🏠 Despre Proiect

**RestoPro** este o aplicație desktop Windows dezvoltată în **C# WPF (.NET)** cu **SQL Server Express** ca sistem de gestiune a bazei de date.

Aplicația permite personalului unui restaurant să gestioneze eficient:
- **Mesele** din restaurant (zone, capacități)
- **Produsele** din meniu (categorii, prețuri, disponibilitate)
- **Comenzile** plasate la fiecare masă
- **Rapoarte** sumare cu totaluri și statistici

---

## ✨ Funcționalități

### 🪑 Gestionare Mese
- Afișarea tuturor meselor într-un tabel
- Adăugare / Modificare / Ștergere masă
- Căutare după numărul mesei sau zonă (Terasa, Interior, Salon Privat)

### 🍕 Gestionare Produse
- Afișarea meniului cu filtrare după categorie
- Adăugare / Modificare / Ștergere produs
- Validare preț (trebuie să fie > 0)
- Marcare produs ca disponibil / indisponibil

### 📋 Gestionare Comenzi
- Înregistrarea comenzilor pe mese
- Selectare masă + produs din liste derulante
- Data comenzii setată automat
- Status plată: **Achitat** / **Neachitat**
- Filtrare comenzi după masă sau status
- Anulare comandă cu confirmare

### 📊 Raport Sumar
- Raport per masă: număr comenzi + total achitat
- Statistici generale: total încasat, medie per masă, produsul cel mai vândut
- **Export în format TXT**

### 🛡️ Validări
- Câmpuri obligatorii nu pot fi goale
- Același produs nu poate fi comandat de două ori la aceeași masă
- Un produs cu comenzi asociate nu poate fi șters
- Conexiunea la BD verificată la pornirea aplicației

---

## 🛠️ Tehnologii

| Categorie | Tehnologie |
|-----------|------------|
| Limbaj | C# |
| Framework UI | WPF (.NET) |
| Bază de date | SQL Server Express |
| Conexiune BD | Microsoft.Data.SqlClient |
| IDE | Visual Studio Community Edition |

---

## 📁 Structura Proiectului

```
RestoPro/
│
├── Models/
│   ├── Masa.cs
│   ├── Produs.cs
│   ├── Comanda.cs
│   └── RaportMasa.cs
│
├── Data/
│   └── DatabaseHelper.cs        # Toate metodele CRUD + interogări
│
├── Views/
│   ├── MasePage.xaml             # Modul Mese
│   ├── MasaEditWindow.xaml       # Formular adăugare/modificare masă
│   ├── ProdusePage.xaml          # Modul Produse
│   ├── ProdusEditWindow.xaml     # Formular adăugare/modificare produs
│   ├── ComenziPage.xaml          # Modul Comenzi
│   ├── ComandaAddWindow.xaml     # Formular adăugare comandă
│   └── RaportWindow.xaml         # Raport sumar + export
│
├── Helpers/
│   └── Validator.cs              # Validare email, numere, câmpuri
│
├── Docs/
│   ├── RequirementsAnalysis.md
│   ├── Wireframes.md
│   └── Database/
│       ├── create_db.sql         # Script creare tabele
│       └── seed_data.sql         # Date de test
│
├── MainWindow.xaml               # Fereastra principală + navigare
├── App.config                    # Connection string
└── RestoPro.csproj
```

---

## 🗄️ Baza de Date

Numele bazei de date: **`RestoPro`**

### Diagrama relațiilor

```
Masa                    Comanda                  Produs
─────────────────       ────────────────────     ─────────────────────
IdMasa (PK)    ◄──────  IdMasa (FK)              IdProdus (PK)
NumarMasa               IdProdus (FK)  ──────►   Denumire
Capacitate              DataComanda              Categorie
Zona                    Cantitate                Pret
                        StatusPlata              Disponibil
```

### Tabele

<details>
<summary><b>Masa</b></summary>

| Câmp | Tip | Constrângere |
|------|-----|-------------|
| IdMasa | INT IDENTITY | PRIMARY KEY |
| NumarMasa | INT | NOT NULL, UNIQUE |
| Capacitate | INT | NOT NULL, CHECK > 0 |
| Zona | NVARCHAR(50) | NOT NULL |

</details>

<details>
<summary><b>Produs</b></summary>

| Câmp | Tip | Constrângere |
|------|-----|-------------|
| IdProdus | INT IDENTITY | PRIMARY KEY |
| Denumire | NVARCHAR(100) | NOT NULL |
| Categorie | NVARCHAR(50) | NOT NULL |
| Pret | DECIMAL(10,2) | NOT NULL, CHECK > 0 |
| Disponibil | BIT | NOT NULL, DEFAULT 1 |

</details>

<details>
<summary><b>Comanda</b></summary>

| Câmp | Tip | Constrângere |
|------|-----|-------------|
| IdComanda | INT IDENTITY | PRIMARY KEY |
| IdMasa | INT | FOREIGN KEY → Masa |
| IdProdus | INT | FOREIGN KEY → Produs |
| DataComanda | DATE | NOT NULL, DEFAULT GETDATE() |
| Cantitate | INT | NOT NULL, CHECK > 0 |
| StatusPlata | NVARCHAR(20) | CHECK IN ('Achitat','Neachitat') |

> ⚠️ Constrângere UNIQUE pe `(IdMasa, IdProdus)` — același produs nu poate fi comandat de două ori la aceeași masă.

</details>

---

## 🚀 Instalare și Configurare

### Cerințe preliminare

- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) (2019 sau mai nou)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)
- .NET Desktop Runtime (inclus în Visual Studio)

### Pași de instalare

**1. Clonează repository-ul**
```bash
git clone https://github.com/username/RestoPro.git
cd RestoPro
```

**2. Creează baza de date**

Deschide SSMS, conectează-te la `SIBAEV-LEGION\SQLEXPRESS` și execută:
```bash
# Mai întâi creează tabelele:
Docs/Database/create_db.sql

# Apoi populează cu date de test:
Docs/Database/seed_data.sql
```

**3. Verifică connection string-ul**

Deschide `App.config` și asigură-te că string-ul de conexiune corespunde instanței tale SQL Server:
```xml
<connectionStrings>
  <add name="RestoPro"
       connectionString="Data Source=SIBAEV-LEGION\SQLEXPRESS;
         Integrated Security=True;TrustServerCertificate=True;
         Initial Catalog=RestoPro"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

**4. Restaurează pachetele NuGet**

În Visual Studio: `Tools → NuGet Package Manager → Package Manager Console`
```powershell
Update-Package -reinstall
```

**5. Build & Run**

```
Build → Build Solution (Ctrl+Shift+B)
Debug → Start Debugging (F5)
```

---

## 📖 Utilizare

La pornire, aplicația verifică automat conexiunea la baza de date. Dacă conexiunea eșuează, este afișat un mesaj de eroare și aplicația se închide.

### Navigare principală

```
┌─────────────────────────────────────────────────┐
│  🍽 RestoPro   [Mese] [Produse] [Comenzi] [Raport] │
├─────────────────────────────────────────────────┤
│                                                  │
│           Conținut modul activ                   │
│                                                  │
└─────────────────────────────────────────────────┘
```

| Buton | Acțiune |
|-------|---------|
| **Mese** | Navighează la modulul de gestionare mese |
| **Produse** | Navighează la modulul de gestionare meniu |
| **Comenzi** | Navighează la modulul de gestionare comenzi |
| **Raport** | Deschide fereastra de raport (dialog separat) |

---

## 📅 Planificare

| Săptămâna | Issue | Obiectiv |
|-----------|-------|----------|
| I | #1 | Analiza cerințelor + configurare mediu + creare proiect WPF |
| I | #2 | Structura proiectului, App.config, clase placeholder |
| II | #3 | Creare BD RestoPro: tabele, relații, constrângeri |
| II | #4 | Date de test + DatabaseHelper funcțional |
| III | #5 | Wireframes pentru toate ferestrele |
| III | #6 | MainWindow cu navigare + test DataGrid end-to-end |
| IV | #7 | Modul Mese — CRUD complet + validări |
| IV | #8 | Modul Produse — CRUD + filtrare + validare preț |
| V | #9 | Modul Comenzi — înregistrare, filtrare, anulare |
| V | #10 | Raport sumar + statistici + export TXT |

---

<div align="center">
  <sub>Proiect realizat în cadrul practicii de 8 săptămâni · RestoPro · C# WPF + SQL Server</sub>
</div>
