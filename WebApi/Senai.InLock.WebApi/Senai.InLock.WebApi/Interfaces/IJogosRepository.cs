using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface IJogosRepository
    {
        List<JogoDomain> ListaTodos();

        JogoDomain BuscarId(int id);

        List<JogoDomain> BuscarPorEstudio(int idEstudio);

        void CadastrarJogo(JogoDomain jogo);

        void Deletar(int id);


    }
}
