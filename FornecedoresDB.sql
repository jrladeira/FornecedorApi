DROP TABLE IF EXISTS Fornecedor;

CREATE TABLE Fornecedor (
    Id INTEGER NOT NULL,
    Nome TEXT NOT NULL,
    Email TEXT NOT NULL,
    CONSTRAINT PK_Fornecedor PRIMARY KEY (Id)
);

INSERT INTO Fornecedor (Nome, Email) VALUES
("Fornecedor A", "fornecedor.a@gmail.com"),
("Fornecedor B", "fornecedor.b@gmail.com"),
("Fornecedor C", "fornecedor.c@gmail.com");

SELECT * FROM Fornecedor;
