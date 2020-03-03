using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogosRepository : IJogosRepository
    {

        private string conexao = "Data Source=DEV9\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; user Id=sa; pwd=sa@132;";

        public JogoDomain BuscarId(int id)
        {
            using (SqlConnection con = new SqlConnection(conexao))
            {
                string busca = $"EXECUTE JogoId {id}";

                using (SqlCommand cmd = new SqlCommand(busca, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        JogoDomain j = new JogoDomain
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),
                            NomeJogo = rdr[1].ToString(),
                            Descricao = rdr[2].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr[3]),
                            Valor = Convert.ToDouble(rdr[4]),
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            Estudio =
                            {
                                IdEstudio = Convert.ToInt32(rdr[5]),
                                NomeEstudio = rdr[6].ToString()
                            }
                        };

                        return j;
                    }
                }
            }

            return null;
        }

        public List<JogoDomain> BuscarPorEstudio(int idEstudio)
        {
            List<JogoDomain> jogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                // query que busca no bd apenas os jogos que o o id do estudio seja o mesmo
                //SELECT Jogos.IdJogos, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos
                //INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio WHERE Estudios.IdEstudio = @ID
                string queryPorEstudio = $"EXECUTE PegarPorEstudio {idEstudio}";

                using (SqlCommand cmd = new SqlCommand(queryPorEstudio, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain j = new JogoDomain
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),
                            NomeJogo = rdr[1].ToString(),
                            Descricao = rdr[2].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr[3]),
                            Valor = Convert.ToDouble(rdr[4]),
                            IdEstudio = Convert.ToInt32(rdr[5]),
                        };

                        jogos.Add(j);

                    }
                }
            }
            return jogos;
        }

        public void CadastrarJogo(JogoDomain jogo)
        {
            using(SqlConnection con = new SqlConnection(conexao))
            {
                string cadas = $"INSERT INTO Jogos(NomeJogo,Descricao,DataLancamento,Valor,IdEstudio)" +
                    $"VALUES ('{jogo.NomeJogo}','{jogo.Descricao}','{jogo.DataLancamento}',{jogo.Valor},{jogo.IdEstudio})";

                using (SqlCommand cmd = new SqlCommand(cadas, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(conexao))
            {
                string del = $"DELETE FROM Jogos WHERE IdJogos = {id}";

                using (SqlCommand cmd = new SqlCommand(del, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListaTodos()
        {
            List<JogoDomain> jogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                // query que busca todos os jogos do BD
                string queryPorEstudio = $"EXECUTE ListarJogos ";

                using (SqlCommand cmd = new SqlCommand(queryPorEstudio, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain j = new JogoDomain
                        {
                            IdJogo = Convert.ToInt32(rdr[0]),
                            NomeJogo = rdr[1].ToString(),
                            Descricao = rdr[2].ToString(),
                            DataLancamento = Convert.ToDateTime(rdr[3]),
                            Valor = Convert.ToDouble(rdr[4]),
                            IdEstudio = Convert.ToInt32(rdr[5]),
                            Estudio =
                            {
                                IdEstudio = Convert.ToInt32(rdr[5]),
                                NomeEstudio = rdr[6].ToString()
                            }
                        };

                        jogos.Add(j);

                    }
                }
            }
            return jogos;
        }
    }
}
