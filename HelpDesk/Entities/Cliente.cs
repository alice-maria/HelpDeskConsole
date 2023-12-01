using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Entities.Enums;

namespace HelpDesk.Entities
{
    internal class Cliente : Usuario
    {
        public Cliente() { }
        public Cliente(string nome, string email, string senha) : base(nome, email, senha, Enums.TipoUsuario.Cliente)
        {

        }

        public override string ToString()
        {
            return "Nome:\t" + Nome + "\n" +
               "Email:\t" + Email;
        }
    }
}
