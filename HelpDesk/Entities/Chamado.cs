using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HelpDesk.Entities.Enums;

namespace HelpDesk.Entities
{
    internal class Chamado
    {
        public int Id { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataEncerramento { get; set; }
        public string Title { get; set; }
        public string Contexto { get; set; }
        public StatusChamado Status { get; set; }
        public Cliente Cliente { get; set; }
        public Atendente Atendente { get; set; }
        List<Comentarios> Comentarios { get; set; } = new List<Comentarios>();
        public double? Nota { get; private set; }

        public string DataCriacaoParaVisualizacao { get { return DataAbertura.ToString("dd/MM/yyyy HH:mm:SS"); } }
        public string DataEncerramentoParaVisualizacao { get { return DataEncerramento.ToString("dd/MM/yyyy HH:mm:SS"); } }

        public Chamado() { }
        public Chamado(int id, string title, string contexto, Cliente cliente)
        {
            Id = id;
            DataAbertura = DateTime.Now;
            Title = title;
            Contexto = contexto;
            Status = StatusChamado.Iniciado;
            Cliente = cliente;
        }

        public void RemoveComentario(Comentarios comentario)
        {
            Comentarios.Remove(comentario);
        }

        public void AdicionaComentario(Comentarios comentario)
        {
            Comentarios.Add(comentario);
        }

        public void AssumirChamado(Atendente atendente)
        {
            Atendente = atendente;
        }

        public void ChamadoFinalizado()
        {
            DataEncerramento = DateTime.Now;
            Status = StatusChamado.Concluido;
        }

        public override string ToString()
        {
            if (Status == StatusChamado.Concluido)
            {
                return $"ID: {Id} - {Title} - Data Criação: {DataCriacaoParaVisualizacao} - Data Encerramento: {DataEncerramentoParaVisualizacao} {(Nota.HasValue ? "- Nota: " + Nota.Value : string.Empty)}";
            }
            else
            {
                return $"ID: {Id} - {Title} - Data Criação: {DataCriacaoParaVisualizacao}";
            }
        }

        public void AdicionaNota(double nota)
        {
            Nota = nota;
        }

        public void VisualizaComentarios()
        {
            Console.WriteLine($"Comentarios do Chamado {Id}");
            Console.WriteLine(Comentarios);
        }

    }
}
