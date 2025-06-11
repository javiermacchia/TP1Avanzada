USE master;
GO

IF DB_ID('avanzada') IS NULL
    CREATE DATABASE avanzada;
GO

USE avanzada;
GO

IF OBJECT_ID('dbo.PasswordResetTokens', 'U') IS NOT NULL
    DROP TABLE dbo.PasswordResetTokens;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Users (
    UserId          INT             IDENTITY(1,1) PRIMARY KEY,
    Username        NVARCHAR(100)   NOT NULL,
    Email           NVARCHAR(256)   NOT NULL,
    Password        NVARCHAR(512)   NOT NULL,
    FirstName       NVARCHAR(100)   NOT NULL,
    LastName        NVARCHAR(100)   NOT NULL,
    PermissionLevel TINYINT         NOT NULL DEFAULT 1,
    CreatedAt       DATETIME        NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT UQ_Users_Username UNIQUE(Username),
    CONSTRAINT UQ_Users_Email    UNIQUE(Email),
    CONSTRAINT CK_Users_PermissionLevel CHECK (PermissionLevel IN (0,1))
);
GO

CREATE TABLE dbo.PasswordResetTokens (
    Token       UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    UserId      INT               NOT NULL,
    Expiration  DATETIME          NOT NULL,
    Used        BIT               NOT NULL DEFAULT 0,
    CONSTRAINT FK_PasswordResetTokens_Users
        FOREIGN KEY(UserId) REFERENCES dbo.Users(UserId)
);
GO

INSERT INTO dbo.Users (Username, Email, Password, FirstName, LastName, PermissionLevel)
VALUES
  ('admin',    'admin@dominio.com',    'admin123', 'Super',   'Admin',     0),
  ('usuario1', 'usuario1@dominio.com', 'pass123',  'Juan',     'Pérez',     1),
  ('usuario2', 'usuario2@dominio.com', 'abc123',   'María',    'Gómez',     1);
GO

INSERT INTO dbo.PasswordResetTokens (Token, UserId, Expiration, Used)
VALUES
  (NEWID(), 1, DATEADD(MINUTE, 20, GETDATE()), 0),
  (NEWID(), 2, DATEADD(MINUTE, 20, GETDATE()), 0);
GO

-- 8) Eliminar tabla Books si existe
IF OBJECT_ID('dbo.Books', 'U') IS NOT NULL
    DROP TABLE dbo.Books;
GO

-- 9) Crear tabla Books
CREATE TABLE dbo.Books (
    BookId    INT             IDENTITY(1,1) PRIMARY KEY,
    Title     NVARCHAR(200)   NOT NULL,
    Author    NVARCHAR(200)   NOT NULL,
    YearPub   INT             NULL,
    ISBN      NVARCHAR(50)    NULL,
    CreatedAt DATETIME        NOT NULL DEFAULT(GETDATE())
);
GO

-- 10) Insertar datos de prueba en Books
INSERT INTO dbo.Books (Title, Author, YearPub, ISBN) VALUES
  ('Don Quijote de la Mancha', 'Miguel de Cervantes', 1605, '978-8420470946'),
  ('Cien años de soledad',     'Gabriel García Márquez', 1967, '978-0307474728'),
  ('La Odisea',                'Homero',  -800,            '978-0140268867'),
  ('El principito',            'Antoine de Saint-Exupéry', 1943, '978-0156012195'),
  ('1984',                     'George Orwell',           1949, '978-0451524935');
GO

-- 11) Verificar los datos insertados
SELECT * FROM dbo.Users;
SELECT * FROM dbo.PasswordResetTokens;
SELECT * FROM dbo.Books;
GO
