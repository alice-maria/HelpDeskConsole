using HelpDesk.Entities;
using System;
using System.Net.Mail;
using System.Threading.Channels;
using HelpDesk.Entities.Enums;
using System.Runtime.InteropServices.ComTypes;

namespace HelpDesk
{
    public class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();
        static List<Chamado> chamados = new List<Chamado>();
        static void Main(string[] args)
        {

            Adm admin = new Adm("Maria Alice", "maria@administrador.com.br", "1234");

            Cliente c1 = new Cliente("João Augusto", "joao@helpdesk.comm.br", "1234");
            Cliente c2 = new Cliente("Helena Moura", "helena@helpdesk.comm.br", "1234");
            Cliente c3 = new Cliente("Anna Paula", "anna@helpdesk.comm.br", "1234");

            Atendente att1 = new Atendente("Marcia", "marcia@empresa.com.br", "1234");
            Atendente att2 = new Atendente("Alex André Junior", "alex@empresa.com.br", "1234");
            Atendente att3 = new Atendente("Joana Santos", "joana@empresa.com.br", "1234");
            Atendente att4 = new Atendente("Leonardo Soares", "leonardo@empresa.com.br", "1234");

            usuarios.Add(c1);
            usuarios.Add(c2);
            usuarios.Add(c3);

            usuarios.Add(att1);
            usuarios.Add(att2);
            usuarios.Add(att3);
            usuarios.Add(att4);

            usuarios.Add(admin);

            Chamado cha1 = new Chamado(chamados.Count + 1, "Teste1 ", "erro internet", c3);
            Chamado cha2 = new Chamado(chamados.Count + 1, "Teste2 ", "erro internet", c3);
            Chamado cha3 = new Chamado(chamados.Count + 1, "Teste3 ", "correção de template", c2);
            Chamado cha4 = new Chamado(chamados.Count + 1, "Teste4 ", "valor da nota incorreta", c2);
            Chamado cha5 = new Chamado(chamados.Count + 1, "Teste5 ", "nota pendente", c3);
            Chamado cha6 = new Chamado(chamados.Count + 1, "Teste6 ", "sistema parou", c2);

            chamados.Add(cha1);
            chamados.Add(cha2);
            chamados.Add(cha3);
            chamados.Add(cha4);
            chamados.Add(cha5);
            chamados.Add(cha6);

            Comentarios cm = new Comentarios(cha1, "Ainda não foi resolvido");
            cha1.AdicionaComentario(cm);
            Comentarios cm2 = new Comentarios(cha4, "Qual o número da nota?, precisamos dessa informação");
            cha4.AdicionaComentario(cm2);
            Comentarios cm3 = new Comentarios(cha6, "Desde de que horas?");
            cha6.AdicionaComentario(cm3); ;

            Console.Clear();


            Login();

        }
        static void Login()
        {
            do
            {
                Console.WriteLine("-----ACESSO HELPDESK-----");
                Console.WriteLine();
                Console.Write("Login: ");
                string email = Console.ReadLine();

                Console.WriteLine();

                Console.Write("Senha:");
                string senha = Console.ReadLine();

                Console.Clear();

                var usuarioLogado = Usuario.ValidarLogin(email, senha, usuarios);

                if (usuarioLogado == null)
                {
                    Console.WriteLine("USUÁRIO OU SENHA INCORRETOS");

                }
                else
                {
                    if (usuarioLogado.Tipo == TipoUsuario.Atendente)
                    {
                        Atendente c = (Atendente)usuarioLogado;
                        Atendente(c);
                    }
                    else if (usuarioLogado.Tipo == TipoUsuario.Adm)
                    {
                        Adm c = (Adm)usuarioLogado;
                        Adm(c);
                    }
                    else
                    {
                        Cliente c = (Cliente)usuarioLogado;
                        Cliente(c);
                    }
                }

            } while (true);
        }

        static void Atendente(Atendente atend)
        {
            int opcao = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Pessoa: Atendente");
                Console.WriteLine("Bem - vindo {0}", atend.Nome);

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Escolha uma opção do menu: ");
                Console.WriteLine();
                Console.WriteLine("1 - Assumir um novo chamado");
                Console.WriteLine("2 - Fechar chamado");
                Console.WriteLine("3 - Adicionar tramite");
                Console.WriteLine("4 - Visualizar chamados abertos");
                Console.WriteLine("5 - Visualizar chamados encerrados");
                Console.WriteLine("6 - Visualizar um chamado em especifico");
                Console.WriteLine("7 - Cadasatrar novo cliente");
                Console.WriteLine("8 - Visualizar comentarios de um chamado");
                Console.WriteLine("9 - SAIR");
                opcao = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (opcao)
                {
                    case 1:

                        Console.Write("Digite o id do chamado que você deseja assumir: ");
                        int idChamado = int.Parse(Console.ReadLine());
                        Console.ReadKey();
                        bool encontrouChamado = false;

                        foreach (Chamado chamado in chamados)
                        {
                            if (chamado.Id == idChamado)
                            {
                                Console.Clear();
                                chamado.AssumirChamado(atend);
                                Console.WriteLine("Chamado {0} direcionado para: {1}!", chamado.Id, atend.Nome);
                                encontrouChamado = true;
                                Console.ReadKey();

                            }
                        }

                        if (!encontrouChamado)
                        {
                            Console.WriteLine("Chamado não encontrado");
                        }

                        Console.ReadKey();
                        break;

                    case 2:
                        EncerrarChamado();
                        break;

                    case 3:
                        AddComentario();
                        break;

                    case 4:
                        Console.Clear();

                        StatusChamado[] chamadosSttsAberto = new StatusChamado[] { StatusChamado.Iniciado, StatusChamado.Andamento };

                        List<Chamado> chamadosAbertos;

                        chamadosAbertos = chamados.Where(x => chamadosSttsAberto.Contains(x.Status)).ToList();


                        if (chamadosAbertos.Count > 0)
                        {
                            foreach (var chamado in chamadosAbertos)
                            {
                                Console.WriteLine(chamado.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado aberto foi encontrado");
                        }

                        Console.ReadKey();
                        break;

                    case 5:
                        StatusChamado[] chamadosSttsEncerrados = new StatusChamado[] { StatusChamado.Concluido };

                        List<Chamado> chamadosEncerrados;


                        chamadosAbertos = chamados.Where(x => chamadosSttsEncerrados.Contains(x.Status)).ToList();

                        if (chamadosAbertos.Count > 0)
                        {
                            foreach (var chamado in chamadosAbertos)
                            {
                                Console.WriteLine(chamado.ToString());
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado encerrado foi encontrado para o seu usuário");
                        }

                        Console.ReadKey();
                        break;

                    case 6:
                        UmChamado();
                        break;

                    case 7:

                        CadastrarCliente();
                        break;

                    case 8:
                        Console.WriteLine("Id do chamado");
                        int id = int.Parse(Console.ReadLine());

                        foreach(Chamado ch in chamados)
                        {
                            if(id == ch.Id)
                            {
                                Console.WriteLine(ch.VisualizaComentarios);
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;

                    case 9:
                        SairLogin();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida! Por Favor digite uma das opções do menu");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (opcao != 0);

        }

        static void Cliente(Cliente client)
        {
            int opcao = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Pessoa: Cliente");
                Console.WriteLine("Bem - vindo {0}", client.Nome);

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Escolha uma opção do menu: ");
                Console.WriteLine();
                Console.WriteLine("1 - Abrir um novo chamado");
                Console.WriteLine("2 - Fechar chamado");
                Console.WriteLine("3 - Adicionar tramite");
                Console.WriteLine("4 - Adicionar nota para o atendente");
                Console.WriteLine("5 - Visualizar chamados abertos");
                Console.WriteLine("6 - Visualizar chamados encerrados");
                Console.WriteLine("7 - Visualizar um chamado em especifico");
                Console.WriteLine("8 - SAIR");
                opcao = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("ABRINDO UM NOVO CHAMADO...");
                        Console.WriteLine();;

                        Console.Write("Digite o titulo: ");
                        string titulo = Console.ReadLine();

                        Console.Write("Descreva o problema: ");
                        string contexto = Console.ReadLine();

                        Chamado ticket = new Chamado(chamados.Count + 1, titulo, contexto, client);

                        Console.WriteLine("Deseja adicionar um comentario? s/n");
                        string comentario = Console.ReadLine();

                        if (comentario.Equals("s"))
                        {
                            Console.WriteLine("ADICIONAR COMENTARIO:");
                            string texto = Console.ReadLine();

                            Comentarios comentarios = new Comentarios(ticket, texto);

                            ticket.AdicionaComentario(comentarios);

                            Console.Clear();
                            Console.WriteLine("Comentario adicionado ao chamado {0}", chamados.Count+1);
                            Console.WriteLine(texto);
                            Console.ReadKey();
                        }

                        chamados.Add(ticket);
                        break;

                    case 2:
                        EncerrarChamado();

                        break;

                    case 3:
                        AddComentario();

                        break;

                    case 4:
                        Console.WriteLine("Digite o id do chamado: ");
                        int idChamado = int.Parse(Console.ReadLine());
                        Console.Write("Qual a nota para este chamado? ");
                        int nota = int.Parse(Console.ReadLine());

                        foreach (var chamado in chamados)
                        {
                            if (chamado.Id == idChamado)
                            {
                                chamado.AdicionaNota(nota);
                                break;
                            }

                        }

                        Console.WriteLine($"Nota {nota} adicionado ao chamado {idChamado}");

                        break;

                    case 5:

                        StatusChamado[] chamadosSttsAberto = new StatusChamado[] { StatusChamado.Iniciado, StatusChamado.Andamento };

                        List<Chamado> chamadosAbertos;


                        chamadosAbertos = chamados.Where(x => x.Cliente.Email == client.Email && chamadosSttsAberto.Contains(x.Status)).ToList();

                        if (chamadosAbertos.Count > 0)
                        {
                            foreach (var chamado in chamadosAbertos)
                            {
                                Console.WriteLine(chamado.ToString());
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado aberto foi encontrado para o seu usuário");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        break;

                    case 6:
                        StatusChamado[] chamadosSttsEncerrados = new StatusChamado[] { StatusChamado.Concluido };

                        List<Chamado> chamadosEncerrados;


                        chamadosAbertos = chamados.Where(x => x.Cliente.Email == client.Email && chamadosSttsEncerrados.Contains(x.Status)).ToList();

                        if (chamadosAbertos.Count > 0)
                        {
                            foreach (var chamado in chamadosAbertos)
                            {
                                Console.WriteLine(chamado.ToString());
                                Console.ReadKey();
                                Console.Clear();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado encerrado foi encontrado para o seu usuário");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        break;
                    case 7:
                        UmChamado();

                        break;
                    case 8:
                        SairLogin();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida! Por Favor digite uma das opções do menu");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (opcao != 0);


        }
        static void Adm(Adm adm)
        {
            int opcao = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Pessoa: Administrador");
                Console.WriteLine("Bem - vindo {0}", adm.Nome);

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Escolha uma opção do menu: ");
                Console.WriteLine();
                Console.WriteLine("1 - Visualizar chamados em aberto");
                Console.WriteLine("2 - Visualizar chamados encerrados");
                Console.WriteLine("3 - Visualizar um chamado em especifico");
                Console.WriteLine("4 - Cadasatrar novo cliente");
                Console.WriteLine("5 - Cadastrar novo atendente");
                Console.WriteLine("6 - Remover Usuario");
                Console.WriteLine("7 - SAIR");
                opcao = int.Parse(Console.ReadLine());
                Console.Clear();


                switch (opcao)
                {
                    case 1:

                        StatusChamado[] chamadosSttsAberto = new StatusChamado[] { StatusChamado.Iniciado, StatusChamado.Andamento };

                        List<Chamado> chamadosAbertos;

                        chamadosAbertos = chamados.Where(x => chamadosSttsAberto.Contains(x.Status)).ToList();


                        if (chamadosAbertos.Count > 0)
                        {
                            foreach (var chamado in chamadosAbertos)
                            {
                                Console.WriteLine(chamado.ToString());
                                Console.ReadKey();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado aberto foi encontrado");
                            Console.ReadKey();
                        }

                        break;

                    case 2:

                        StatusChamado[] chamadosSttsEncerrados = new StatusChamado[] { StatusChamado.Concluido };

                        List<Chamado> chamadosEncerrados;

                        chamadosEncerrados = chamados.Where(x => chamadosSttsEncerrados.Contains(x.Status)).ToList();


                        if (chamadosEncerrados.Count > 0)
                        {
                            foreach (var chamado in chamadosEncerrados)
                            {
                                Console.WriteLine(chamado.ToString());
                                Console.ReadKey();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum chamado encerrado foi encontrado");
                            Console.ReadKey();
                        }
                        Console.WriteLine();
                        Console.WriteLine("Toque para voltar ao menu");
                        break;

                    case 3:
                        UmChamado();
                        break;

                    case 4:
                        CadastrarCliente();
                        break;

                    case 5:
                        CadastrarAtendente();
                        break;

                    case 6:
                        Console.WriteLine("Digite o email do Usuario que deseja excluir");
                        string email = Console.ReadLine();
                        bool encontrouUsuario = false;

                        foreach (Usuario usuario in usuarios)
                        {
                            if (usuario.Email.Equals(email))
                            {
                                usuarios.Remove(usuario);

                                Console.Clear();
                                Console.WriteLine("Usuário excluido com sucesso!");
                                encontrouUsuario |= true;
                                Console.ReadKey();
                                break;
                            }
                        }

                        if (!encontrouUsuario)
                        {
                            Console.WriteLine("Usuário não encontrado");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Toque para voltar ao menu");
                        Console.ReadKey();

                        break;

                    case 7:
                        SairLogin();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida! Por Favor digite uma das opções do menu");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != 0);

        }

        static void SairLogin()
        {
            Console.Clear();
            Console.WriteLine("Até mais!");
            Console.ReadKey();
            Console.Clear();
            Login();
        }
        static void CadastrarCliente()
        {
            Console.Clear();
            Console.Write("Digite o nome do Cliente: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string emailCliente = Console.ReadLine();

            Console.Write("Digite uma senha para o usuário do cliente: ");
            string senhaCliente = Console.ReadLine();

            Cliente n = new Cliente(nome, emailCliente, senhaCliente);

            usuarios.Add(n);

            Console.Clear();
            Console.WriteLine("Cliente adicionado com sucesso!");
            Console.WriteLine(n.ToString());
            Console.WriteLine();
            Console.WriteLine("Toque para voltar ao menu");
            Console.ReadKey();
        }
        static void CadastrarAtendente()
        {
            Console.Clear();
            Console.Write("Digite o nome do Atendente: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string emailAtendente = Console.ReadLine();

            Console.Write("Digite uma senha para o usuário do cliente: ");
            string senha = Console.ReadLine();

            Atendente a = new Atendente(nome, emailAtendente, senha);

            usuarios.Add(a);

            Console.Clear();
            Console.WriteLine("Atendente Adicionado com sucesso!");
            Console.WriteLine(a.ToString());
            Console.WriteLine();
            Console.WriteLine("Toque para voltar ao menu");
            Console.ReadKey();
        }
        static void EncerrarChamado()
        {
            Console.Clear();
            Console.Write("Digite o número do chamado que você encerrar: ");
            int idChamado = int.Parse(Console.ReadLine());
            bool encontrouChamado = false;

            foreach (Chamado chamado in chamados)
            {
                if (chamado.Id == idChamado)
                {
                    Console.Write("Qual a nota para este chamado? ");
                    double nota = double.Parse(Console.ReadLine());
                    foreach (var ch in chamados)
                    {
                        if (ch.Id == idChamado)
                        {
                            ch.AdicionaNota(nota);
                        }
                    }

                    chamado.ChamadoFinalizado();
                    Console.Clear();
                    Console.WriteLine("Chamado encerrado com sucesso!!!!!");
                    Console.WriteLine();
                    Console.WriteLine(chamado.ToString());
                    encontrouChamado = true;
                    break;
                }
            }

            if (!encontrouChamado)
            {
                Console.WriteLine("Chamado não encontrado");
            }

            Console.WriteLine();
            Console.WriteLine("Toque para voltar ao menu");
            Console.ReadKey();
        }

        static void AddComentario()
        {
            Console.Clear();
            Console.Write("Digite o número do chamado que você deseja adicionar um comentário: ");
            int idChamado = int.Parse(Console.ReadLine());

            Console.WriteLine("Comentário: ");
            string comment = Console.ReadLine();
            Console.ReadKey();

            foreach (Chamado chamado in chamados)
            {
                if (chamado.Id == idChamado)
                {
                    Comentarios comentarios = new Comentarios(chamado, comment);

                    chamado.AdicionaComentario(comentarios);
                    Console.WriteLine("Comentario adicionado ao chamado {0}", idChamado);
                    Console.WriteLine(comentarios.ToString());
                    Console.ReadKey();
                    break;
                }
            }
        }

        static void UmChamado()
        {
            Console.Write("Digite o Id do chamado que deseja visualizar: ");
            int idTeste = int.Parse(Console.ReadLine());

            bool encontrouChamado = false;
            foreach (var ch in chamados)
            {
                if (ch.Id == idTeste)
                {
                    Console.Clear();
                    Console.WriteLine($"--------------------Dados do chamado {idTeste}--------------------");
                    Console.WriteLine();
                    Console.WriteLine(ch.ToString());
                    encontrouChamado = true;
                    break;
                }
            }

            if (!encontrouChamado)
            {
                Console.Clear();
                Console.WriteLine("Chamado não encontrado");
            }

            Console.WriteLine("Toque para voltar ao menu");
            Console.ReadKey();
        }
    }
}