
namespace ControleDeVisitantes
{

    public class Visitante
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime HorarioChegada { get; set; }
        public DateTime? HorarioSaida { get; set; }
        public bool PrimeiraVisita { get; set; }

        public Visitante(int id, string nome, string documento, DateTime horarioChegada, bool primeiraVisita)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            HorarioChegada = horarioChegada;
            PrimeiraVisita = primeiraVisita;
        }
    }

      public class ControleVisitantes
    {
        private List<Visitante> visitantes = new List<Visitante>();
        private int proximoId = 1;

        public void CadastrarVisitante(string nome, string documento, bool primeiraVisita)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("Documento é obrigatório.");

            Visitante visitante = new Visitante(
                proximoId++,
                nome,
                documento,
                DateTime.Now,
                primeiraVisita
            );

            visitantes.Add(visitante);
            Console.WriteLine("Visitante cadastrado com sucesso!");
        }

        public void ListarVisitantes()
        {
            if (!visitantes.Any())
            {
                Console.WriteLine("Nenhum visitante cadastrado.");
                return;
            }

            foreach (var v in visitantes)
            {
                Console.WriteLine(
                    $"ID: {v.Id} | Nome: {v.Nome} | Documento: {v.Documento} | Chegada: {v.HorarioChegada} | Primeira visita: {v.PrimeiraVisita}"
                );
            }
        }

        public void BuscarPorNome(string nome)
        {
            var encontrados = visitantes
                .Where(v => v.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!encontrados.Any())
            {
                Console.WriteLine("Visitante não encontrado.");
                return;
            }

            foreach (var v in encontrados)
            {
                Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome} | Chegada: {v.HorarioChegada}");
            }
        }

        public void RegistrarSaida(int id)
        {
            var visitante = visitantes.FirstOrDefault(v => v.Id == id);

            if (visitante == null)
                throw new Exception("Visitante não encontrado.");

            visitante.HorarioSaida = DateTime.Now;
            visitantes.Remove(visitante);

            Console.WriteLine(" Saída registrada com sucesso!");
        }

        public void ListarOrdenadoPorId()
        {
            var ordenados = visitantes.OrderBy(v => v.Id);

            foreach (var v in ordenados)
            {
                Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome}");
            }
        }

        public void ListarPrimeiraVisita()
        {
            var primeiraVez = visitantes.Where(v => v.PrimeiraVisita);

            foreach (var v in primeiraVez)
            {
                Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome}");
            }
        }
    }

        class Program
    {
        static void Main(string[] args)
        {
            ControleVisitantes controle = new ControleVisitantes();
            bool continuar = true;

            while (continuar)
            {
                try
                {
                    Console.WriteLine("\n=== MENU DE VISITANTES ===");
                    Console.WriteLine("1 - Cadastrar visitante");
                    Console.WriteLine("2 - Listar visitantes");
                    Console.WriteLine("3 - Buscar visitante por nome");
                    Console.WriteLine("4 - Registrar saída");
                    Console.WriteLine("5 - Listar visitantes ordenados por ID");
                    Console.WriteLine("6 - Listar visitantes de primeira visita");
                    Console.WriteLine("0 - Sair");
                    Console.Write("Opção: ");

                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            Console.Write("Nome: ");
                            string nome = Console.ReadLine();

                            Console.Write("Documento: ");
                            string documento = Console.ReadLine();

                            Console.Write("É primeira visita? (s/n): ");
                            bool primeiraVisita = Console.ReadLine().ToLower() == "s";

                            controle.CadastrarVisitante(nome, documento, primeiraVisita);
                            break;

                        case "2":
                            controle.ListarVisitantes();
                            break;

                        case "3":
                            Console.Write("Nome para busca: ");
                            string buscaNome = Console.ReadLine();
                            controle.BuscarPorNome(buscaNome);
                            break;

                        case "4":
                            Console.Write("ID do visitante: ");
                            int id = int.Parse(Console.ReadLine());
                            controle.RegistrarSaida(id);
                            break;

                        case "5":
                            controle.ListarOrdenadoPorId();
                            break;

                        case "6":
                            controle.ListarPrimeiraVisita();
                            break;

                        case "0":
                            continuar = false;
                            Console.WriteLine("Sistema encerrado.");
                            break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Erro: {ex.Message}");
                }
            }
        }
    }
}
