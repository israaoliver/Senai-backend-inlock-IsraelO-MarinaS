using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class JogoDomain
    {

        public int IdJogo { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Jogo")]
        [DataType(DataType.Text)]
        public string NomeJogo { get; set; }

        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        public double Valor { get; set; }

        public int IdEstudio { get; set; }

        public EstudioDomain Estudio { get; set; }

        public JogoDomain()
        {
            this.Estudio = new EstudioDomain();
        }


    }
}
