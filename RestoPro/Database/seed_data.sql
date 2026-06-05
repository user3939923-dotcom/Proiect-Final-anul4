USE RestoPro;
GO

-- Date de test: Mese
INSERT INTO Masa (NumarMasa, Capacitate, Zona) VALUES
(1, 2, 'Terasa'),
(2, 4, 'Interior'),
(3, 4, 'Interior'),
(4, 6, 'Interior'),
(5, 2, 'Terasa'),
(6, 8, 'Salon Privat');
GO

-- Date de test: Produse
INSERT INTO Produs (Denumire, Categorie, Pret, Disponibil) VALUES
('Ciorbă de burtă',   'Supe',       32.00, 1),
('Friptură de porc',  'Feluri Principale', 65.00, 1),
('Salată Caesar',     'Salate',     28.00, 1),
('Tiramisu',          'Deserturi',  22.00, 1),
('Limonadă',          'Băuturi',    15.00, 1),
('Pui la grătar',     'Feluri Principale', 55.00, 1);
GO

-- Date de test: Comenzi
INSERT INTO Comanda
    (IdMasa, IdProdus, DataComanda, Cantitate, StatusPlata)
VALUES
(1, 1, '2025-05-01', 2, 'Achitat'),
(1, 5, '2025-05-01', 2, 'Achitat'),
(2, 2, '2025-05-02', 1, 'Neachitat'),
(2, 3, '2025-05-02', 1, 'Achitat'),
(3, 4, '2025-05-03', 3, 'Achitat'),
(4, 6, '2025-05-04', 2, 'Neachitat'),
(5, 1, '2025-05-05', 1, 'Achitat'),
(6, 2, '2025-05-06', 4, 'Achitat');
GO