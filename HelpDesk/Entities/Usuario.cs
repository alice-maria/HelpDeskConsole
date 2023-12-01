using HelpDesk.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Entities
{
    internal class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }

        public Usuario() { }
        public Usuario(string nome, string email, string senha, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Tipo = tipo;
        }

        public static Usuario ValidarLogin(string email, string senha, List<Usuario> usuariosSistema)
        {
            var usuarioLogado = usuariosSistema.Find(x => x.Email == email && x.Senha == senha);
            return usuarioLogado;
        }
    }


}
