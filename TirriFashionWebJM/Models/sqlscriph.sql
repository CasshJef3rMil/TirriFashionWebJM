CREATE DATABASE  TirriFashionWebJM
-- Tabla Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Edad INT,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Telefono VARCHAR(20),
    Contraseña VARCHAR(10)
);

-- Tabla Categoria
CREATE TABLE Categoria (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

-- Tabla Catalogo
CREATE TABLE Catalogo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Imagen IMAGE NULL,
    Descripcion VARCHAR(200),
    AñoFabricacion DATE,
    IdUsuario INT NOT NULL,
    IdCategoria INT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(Id) 
);

-- Tabla Reseñas
CREATE TABLE Reseñas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCatalogo INT NOT NULL,
    IdUsuario INT NOT NULL,
    Comentario VARCHAR(200),
    Calificacion  VARCHAR(10)NOT NULL,
    FOREIGN KEY (IdCatalogo) REFERENCES Catalogo(Id),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
);
