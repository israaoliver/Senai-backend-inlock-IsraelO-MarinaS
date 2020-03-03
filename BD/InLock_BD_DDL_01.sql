CREATE Database Inlock_Games_Tarde
go
use Inlock_Games_Tarde
go
CREATE TABLE Estudios(
	IdEstudio INT PRIMARY KEY IDENTITY,
	NomeEstudio VARCHAR(255) NOT NULL UNIQUE
	);

CREATE TABLE Jogos(
	IdJogos INT PRIMARY KEY IDENTITY,
	NomeJogo VARCHAR(255) NOT NULL,
	Descricao VARCHAR(255),
	DataLancamento DATE ,
	Valor FLOAT,
	IdEstudio INT FOREIGN KEY REFERENCES Estudios (IdEstudio)
	);

CREATE TABLE TiposUsuarios(
	IdTipoUsuario INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(255) NOT NULL UNIQUE
	);

CREATE TABLE Usuarios(
	IdUsuario INT PRIMARY KEY IDENTITY,
	Email VARCHAR(225) NOT NULL UNIQUE,
	Senha VARCHAR(255) NOT NULL,
	IdTipoUsuario INT FOREIGN KEY REFERENCES TiposUsuarios (IdTipoUsuario)
	);

