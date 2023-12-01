using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Entities
{
    internal class TramiteChamado
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }

        public TramiteChamado(string nome, DateTime data, string mensagem)
        {
            Nome = nome;
            Data = data;
            Mensagem = mensagem;
        }

        public override string ToString()
        {
            return Nome + ", " + Data.ToString("dd:MM:yyyy HH:mm:ss") + ": " + Mensagem + ".";
        }
    }
}
