﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class UsuarioDomain
    {

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe o e-mail do usuário")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A senha deve conter no mínimo 5 e no máximo 30 caracteres")]
        public string Senha { get; set; }

        public int IdTipoUsuario { get; set; }

        public TiposUsuarioDomain TipoUsuario { get; set; }

        UsuarioDomain()
        {
            this.TipoUsuario = new TiposUsuarioDomain();
        }
    }
}
