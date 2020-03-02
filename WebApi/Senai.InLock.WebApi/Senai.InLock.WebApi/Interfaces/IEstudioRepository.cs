using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface IEstudioRepository
    {
        List<EstudioDomain> Listar();

        List<EstudioDomain> BuscarEstudio(string nome);

        void Cadastrar(EstudioDomain estudio);


    }
}
