using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> Lisar();

        void Cadastrar(UsuarioDomain user);

        void Deletar(int id);

        UsuarioDomain Autenticando(LoginViewModel login);
    }
}
