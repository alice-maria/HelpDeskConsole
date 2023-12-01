using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Entities
{
    internal class Comentarios
    {
        public string Texto { get; set; }
        public Chamado Chamado { get; set; }
        public DateTime Date { get; set; }
        public Comentarios() { }
        public Comentarios(Chamado chamado, String texto)
        {
            Chamado = chamado;
            Texto = texto;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return "----------------Atualização do chamado: " + Chamado.Id + "----------------\n\n"
                + Texto + "\n"
                + Date;
        }
    }
}
