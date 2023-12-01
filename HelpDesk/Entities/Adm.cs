using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Entities
{
    internal class Adm : Usuario
    {
        public Adm() { }
        public Adm(string nome, string email, string senha) : base(nome, email, senha, Enums.TipoUsuario.Adm)
        {

        }

        public override string ToString()
        {
            return "Nome:\t" + Nome + "\n" +
               "Email:\t" + Email;
        }

    }
}