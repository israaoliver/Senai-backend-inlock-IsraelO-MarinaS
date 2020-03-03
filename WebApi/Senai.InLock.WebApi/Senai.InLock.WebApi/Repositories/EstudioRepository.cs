using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private string conexao = "Data Source=DESKTOP-16CG1FL\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; user Id=sa; pwd=sa@132;";

        private IJogosRepository _jogosRepository { get; set; }

        public EstudioRepository()
        {
            _jogosRepository = new JogosRepository();
        }

        public List<EstudioDomain> BuscarEstudio(string nome)
        {
            List<EstudioDomain> estudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                // SELECT Jogos.IdJogos, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Jogos.IdEstudio, Estudios.NomeEstudio FROM Jogos
                //INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio WHERE Estudios.IdEstudio = @ID
                string busca = $"EXECUTE BuscarEstudio {nome}";

                using (SqlCommand cmd = new SqlCommand(busca, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            EstudioDomain e = new EstudioDomain
                            {
                                IdEstudio = Convert.ToInt32(rdr[0]),
                                NomeEstudio = rdr[1].ToString(),
                                ListaDeJogos = _jogosRepository.BuscarPorEstudio(Convert.ToInt32(rdr[0]))
                            };

                            estudios.Add(e);
                        }

                    }
                }
            }
            return estudios;
        }

        public void Cadastrar(EstudioDomain estudio)
        {
            using(SqlConnection con = new SqlConnection(conexao))
            {
                string cadastrar = $"INSERT INTO Estudios(NomeEstudio) VALUES ('{estudio.NomeEstudio}')";

                using (SqlCommand cmd = new SqlCommand(cadastrar, con))
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudioDomain> Listar()
        {
            List<EstudioDomain> estudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                string query = $"SELECT IdEstudio, NomeEstudio FROM Estudios";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        EstudioDomain e = new EstudioDomain
                        {
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            NomeEstudio = rdr[1].ToString(),
                            ListaDeJogos = _jogosRepository.BuscarPorEstudio(Convert.ToInt32(rdr[0]))
                        };

                        estudios.Add(e);
                    }
                }
            }
            return estudios;
        }
    }
}
