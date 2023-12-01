using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Entities
{
    internal class Atendente : Usuario
    {
        public Atendente() { }
        public Atendente(string nome, string email, string senha) : base(nome, email, senha, Enums.TipoUsuario.Atendente)
        {

        }
        public override string ToString()
        {
            return "Nome:\t" + Nome + "\n" +
               "Email:\t" + Email;
        }
    }
}
