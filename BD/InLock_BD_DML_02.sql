USE Inlock_Games_Tarde
GO
INSERT INTO Estudios (NomeEstudio)
VALUES      ('Blizzard'),
			('Rockstar Studios'),
			('Square Enix');

INSERT INTO Jogos (NomeJogo, Descricao, DataLancamento, Valor, IdEstudio)
VALUES	('Diablo 3', '� um jogo que cont�m bastante a��o e � viciante, seja voc� um novato ou um f�', '15/05/2012', 99.00, 1),
		('Red Dead Redemption II', 'Jogo eletr�nico de ac�o-aventura Western', '26/10/2018', 120.00 , 2);

INSERT INTO TiposUsuarios (Titulo)
VALUES  ('Comum'),
		('Administrador');

INSERT INTO Usuarios (Email, Senha, IdTipoUsuario)
VALUES  ('admin@admin.com', 'admin', 2),
		('cliente@cliente.com', 'cliente', 1);

