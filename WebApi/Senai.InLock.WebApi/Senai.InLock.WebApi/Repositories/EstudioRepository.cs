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
        private string conexao = "Data Source=DEV9\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132;";

        public List<EstudioDomain> BuscarEstudio(string nome)
        {
            List<EstudioDomain> estudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                string busca = $"SELECT IdEstudio, NomeEstudio FROM Estudios WHERE NomeEstudio LIKE '%{nome}%'";

                using (SqlCommand cmd = new SqlCommand(busca, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            EstudioDomain e = new EstudioDomain
                            {
                                IdEstudio = Convert.ToInt32(rdr[0]),
                                NomeEstudio = rdr[1].ToString()
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
            throw new NotImplementedException();
        }

        public List<EstudioDomain> Listar()
        {
            List<EstudioDomain> estudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                string query = $"SELECT IdEstudio, NomeEstudio FROM Estudios";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        EstudioDomain e = new EstudioDomain
                        {
                            IdEstudio = Convert.ToInt32(rdr[0]),
                            NomeEstudio = rdr[1].ToString()
                        };

                        estudios.Add(e);
                    }
                }
            }
            return estudios;
        }
    }
}
