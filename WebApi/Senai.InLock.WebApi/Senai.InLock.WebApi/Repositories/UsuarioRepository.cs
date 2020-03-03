using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.ViewModels;

namespace Senai.InLock.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string conexao = "Data Source=DESKTOP-16CG1FL\\SQLEXPRESS; initial catalog=Inlock_Games_Tarde; user Id=sa; pwd=sa@132;";

        public UsuarioDomain Autenticando(LoginViewModel login)
        {
            using (SqlConnection con = new SqlConnection(conexao))
            {
                string aut = $"EXECUTE Autentica '{login.Email}', '{login.Senha}'";

                using (SqlCommand cmd = new SqlCommand(aut, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if(rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            UsuarioDomain user = new UsuarioDomain
                            {
                                IdUsuario = Convert.ToInt32(rdr[0]),
                                Email = rdr[1].ToString(),
                                Senha = rdr[2].ToString(),
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                TipoUsuario =
                                {
                                    IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                    Titulo = rdr[4].ToString()
                                }
                            };

                            return user;
                        }
                        
                    }
                    return null;
                }
            }

        }

        public UsuarioDomain BuscarId(int id)
        {
            using (SqlConnection con = new SqlConnection(conexao))
            {
                string querybusca = $"SELECT U.IdUsuario, U.Email,U.Senha, U.IdTipoUsuario," +
                    $"T.Titulo FROM Usuarios U INNER JOIN TiposUsuarios T ON T.IdTipoUsuario = U.IdTipoUsuario WHERE U.IdUsuario = {id}";

                using (SqlCommand cmd = new SqlCommand(querybusca, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain user = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Email = rdr[1].ToString(),
                            Senha = rdr[2].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[3]),
                            TipoUsuario =
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                Titulo = rdr[4].ToString()
                            }
                        };

                        return user;
                    }
                    return null;
                }
            }

            ;
        }

        public void Cadastrar(UsuarioDomain user)
        {
            using (SqlConnection con = new SqlConnection(conexao))
            {
                string cadastrar = $"INSERT INTO Usuarios (Email,Senha,IdTipoUsuario)" +
                    $"VALUES ('{user.Email}','{user.Senha}',{user.IdTipoUsuario})";

                using (SqlCommand cmd = new SqlCommand(cadastrar, con))
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<UsuarioDomain> Listar()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(conexao))
            {
                string query = $"SELECT U.IdUsuario, U.Email, U.Senha, U.IdTipoUsuario," +
                    $"T.Titulo FROM Usuarios U INNER JOIN TiposUsuarios T ON T.IdTipoUsuario = U.IdTipoUsuario";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        UsuarioDomain u = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Email = rdr[1].ToString(),
                            Senha = rdr[2].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[3]),
                            TipoUsuario =
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                Titulo = rdr[4].ToString()
                            }
                        };

                        usuarios.Add(u);
                    }
                }
                

            }

            return usuarios;
        }

        public void Remover(int id)
        {
            using(SqlConnection con = new SqlConnection(conexao))
            {
                string remove = $"DELETE FROM Usuarios WHERE IdUsuario = {id}";

                using (SqlCommand cmd = new SqlCommand(remove, con))
                {
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
