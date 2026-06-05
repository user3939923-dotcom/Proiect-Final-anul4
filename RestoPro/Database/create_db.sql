-- =============================================
-- RestoPro - Script creare baza de date
-- =============================================

CREATE DATABASE RestoPro;
GO

USE RestoPro;
GO

-- Tabelul Masa
CREATE TABLE Masa (
    IdMasa      INT IDENTITY(1,1) PRIMARY KEY,
    NumarMasa   INT NOT NULL UNIQUE,
    Capacitate  INT NOT NULL CHECK (Capacitate > 0),
    Zona        NVARCHAR(50) NOT NULL
);
GO

-- Tabelul Produs
CREATE TABLE Produs (
    IdProdus    INT IDENTITY(1,1) PRIMARY KEY,
    Denumire    NVARCHAR(100) NOT NULL,
    Categorie   NVARCHAR(50)  NOT NULL,
    Pret        DECIMAL(10,2) NOT NULL CHECK (Pret > 0),
    Disponibil  BIT NOT NULL DEFAULT 1
);
GO

-- Tabelul Comanda
CREATE TABLE Comanda (
    IdComanda   INT IDENTITY(1,1) PRIMARY KEY,
    IdMasa      INT NOT NULL,
    IdProdus    INT NOT NULL,
    DataComanda DATE NOT NULL DEFAULT GETDATE(),
    Cantitate   INT NOT NULL CHECK (Cantitate > 0),
    StatusPlata NVARCHAR(20) NOT NULL
        CHECK (StatusPlata IN ('Achitat','Neachitat')),
    CONSTRAINT FK_Comanda_Masa
        FOREIGN KEY (IdMasa) REFERENCES Masa(IdMasa),
    CONSTRAINT FK_Comanda_Produs
        FOREIGN KEY (IdProdus) REFERENCES Produs(IdProdus),
    CONSTRAINT UQ_Comanda_Masa_Produs
        UNIQUE (IdMasa, IdProdus)
);
GO