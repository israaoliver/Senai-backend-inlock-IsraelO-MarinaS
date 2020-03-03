USE Inlock_Games_Tarde

SELECT * FROM Estudios;
SELECT * FROM Jogos;
SELECT * FROM TiposUsuarios;
SELECT * FROM Usuarios;

SELECT Estudios.NomeEstudio, Jogos.NomeJogo FROM Estudios
INNER JOIN Jogos ON Estudios.IdEstudio = Jogos.IdEstudio


-- Procedural para pegar o jogos de um determinado estudio passando o id do estudio
CREATE PROCEDURE PegarPorEstudio
@ID INT
AS
SELECT Jogos.IdJogos, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos
       INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio WHERE Estudios.IdEstudio = @ID

Execute PegarPorEstudio 1

--Procedural para buscar um estudio pelo nome dele
CREATE PROCEDURE BuscarEstudio
@NOME VARCHAR(55)
AS
SELECT IdEstudio, NomeEstudio FROM Estudios WHERE NomeEstudio LIKE '%'+ @NOME + '%';

EXECUTE BuscarEstudio bli

--Procedural que pega todos os jogos e o nome dos estudios
CREATE PROCEDURE ListarJogos
AS
SELECT Jogos.IdJogos, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos 
INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio

EXECUTE ListarJogos

--Buscar o jogo pelo Id
CREATE PROCEDURE JogoId
@ID INT
AS
SELECT Jogos.IdJogos, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos 
INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio WHERE Jogos.IdJogos = @ID

EXECUTE JogoId 1

-- Autentica se o usuario existe no bd recebendo a senha e o email
CREATE PROCEDURE Autentica
@EMAIL VARCHAR(255) ,
@SENHA VARCHAR(255) 
AS
SELECT Usuarios.IdUsuario, Usuarios.Email,Usuarios.Senha, Usuarios.IdTipoUsuario, TiposUsuarios.Titulo FROM Usuarios 
INNER JOIN TiposUsuarios ON Usuarios.IdTipoUsuario = TiposUsuarios.IdTipoUsuario WHERE Email = @EMAIL AND Senha= @SENHA

EXECUTE Autentica 'cliente@cliente.com','cliente'

Drop procedure Autentica

