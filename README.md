# BibliotecaApp

Aplicación web ASP.NET WebForms (C#) con capa de negocio (BIZ) y SQL Server.

## Características

- **Autenticación**: login y registro de usuarios con niveles de permiso (0=admin, 1=usuario).  
- **Recuperación de contraseña** vía email con token (validez 20 minutos).  
- **Perfil de usuario**: edición de nombre, apellido, email y contraseña.  
- **Catálogo de libros**:  
  - CRUD completo desde panel de administrador.  
  - Listado público con búsqueda por título o autor.  
- **Interfaz** responsiva con Bootstrap y SweetAlert2 para confirmaciones.

## Requisitos

- Visual Studio 2022 o superior  
- .NET Framework 4.7.2 o 4.8  
- SQL Server (Express o completo)  
- Navegador moderno (Chrome, Edge, Firefox)

## Estructura de Carpetas
```
/ (root)
├─ TP1Avanzada.sln
├─ Scripts/
│ └─ InitializeDatabase.sql
├─ TP1Avanzada/ (proyecto WebForms)
│ ├─ Site.Master
│ ├─ Default.aspx, Default.aspx.cs
│ ├─ Login.aspx, Login.aspx.cs
│ ├─ Register.aspx, Register.aspx.cs
│ ├─ Recovery.aspx, Recovery.aspx.cs
│ ├─ ResetPassword.aspx, ResetPassword.aspx.cs
│ ├─ ProfilePage.aspx, ProfilePage.aspx.cs
│ ├─ AdminPage.aspx, AdminPage.aspx.cs
│ ├─ Logout.aspx, Logout.aspx.cs
│ └─ web.config
├─ BIZ/ (capa de negocio)
│ ├─ DatosLogin.cs
│ ├─ DatosRegistro.cs
│ ├─ PasswordResetService.cs
│ ├─ BooksService.cs
│ └─ Entidades (User.cs, Book.cs, PasswordResetToken.cs)
└─ Content/
└─ Site.css (estilos globales)
```

## Configuración

1. Ejecutar `Scripts/InitializeDatabase.sql` en SQL Server Management Studio.  
2. Ajustar la cadena de conexión en `TP1Avanzada\web.config`:

   ```xml
   <connectionStrings>
     <add name="SQLCLASE"
          connectionString="Data Source=TU_SERVIDOR;
                            Initial Catalog=avanzada;
                            User ID=TU_USUARIO;
                            Password=TU_PASS" />
   </connectionStrings>
   ```

**Inicializar y Ejecutar**
1. Abrir `TP1Avanzada.sln` en Visual Studio.
2. Marcar `TP1Avanzada` como proyecto de inicio.
3. Presionar F5 para compilar y ejecutar con IIS Express.
4. En el navegador ir a `http://localhost:xxxx/`.

**Uso**
- **Registro**: crear usuario con nombre, apellido, email, contraseña.  
- **Login**: iniciar sesión con usuario/email y contraseña.  
- **Panel de Admin** (`/AdminPage.aspx`): CRUD de libros (solo nivel 0).  
- **Catálogo público** (`/Default.aspx`): ver y buscar libros.  
- **Perfil** (`/ProfilePage.aspx`): editar datos personales.  
- **Recuperación de contraseña**: enviar email con enlace de restablecimiento.  

## Scripts de BD de ejemplo

```sql
USE master;
GO
IF DB_ID('avanzada') IS NULL CREATE DATABASE avanzada;
GO
USE avanzada;
GO
IF OBJECT_ID('dbo.PasswordResetTokens','U') IS NOT NULL DROP TABLE dbo.PasswordResetTokens;
IF OBJECT_ID('dbo.Users','U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Books','U') IS NOT NULL DROP TABLE dbo.Books;
GO
CREATE TABLE dbo.Users (
  UserId INT IDENTITY(1,1) PRIMARY KEY,
  Username NVARCHAR(100) NOT NULL,
  Email NVARCHAR(256) NOT NULL,
  Password NVARCHAR(512) NOT NULL,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  PermissionLevel TINYINT NOT NULL DEFAULT 1,
  CreatedAt DATETIME NOT NULL DEFAULT(GETDATE()),
  CONSTRAINT UQ_Users_Username UNIQUE(Username),
  CONSTRAINT UQ_Users_Email UNIQUE(Email),
  CONSTRAINT CK_Users_PermissionLevel CHECK(PermissionLevel IN(0,1))
);
GO
CREATE TABLE dbo.PasswordResetTokens (
  Token UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
  UserId INT NOT NULL,
  Expiration DATETIME NOT NULL,
  Used BIT NOT NULL DEFAULT 0,
  FOREIGN KEY(UserId) REFERENCES dbo.Users(UserId)
);
GO
CREATE TABLE dbo.Books (
  BookId INT IDENTITY(1,1) PRIMARY KEY,
  Title NVARCHAR(200) NOT NULL,
  Author NVARCHAR(200) NOT NULL,
  YearPub INT NULL,
  ISBN NVARCHAR(50) NULL,
  CreatedAt DATETIME NOT NULL DEFAULT(GETDATE())
);
GO
INSERT INTO dbo.Users (Username,Email,Password,FirstName,LastName,PermissionLevel)
  VALUES('admin','admin@dominio.com','admin123','Super','Admin',0),
        ('usuario1','usuario1@dominio.com','pass123','Juan','Pérez',1),
        ('usuario2','usuario2@dominio.com','abc123','María','Gómez',1);
GO
INSERT INTO dbo.PasswordResetTokens(Token,UserId,Expiration,Used)
  VALUES(NEWID(),1,DATEADD(MINUTE,20,GETDATE()),0),
        (NEWID(),2,DATEADD(MINUTE,20,GETDATE()),0);
GO
INSERT INTO dbo.Books (Title,Author,YearPub,ISBN) VALUES
  ('Don Quijote de la Mancha','Miguel de Cervantes',1605,'978-8420470946'),
  ('Cien años de soledad','Gabriel García Márquez',1967,'978-0307474728'),
  ('La Odisea','Homero',-800,'978-0140268867'),
  ('El principito','Antoine de Saint-Exupéry',1943,'978-0156012195'),
  ('1984','George Orwell',1949,'978-0451524935');
GO

```
