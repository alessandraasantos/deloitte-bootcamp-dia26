using System;
using System.Collections.Generic;
using controle_estoque.Models;

namespace controle_estoque
{
    class Program
    {
        static List<Produto> estoque = new List<Produto>();

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                try
                {
                    Console.WriteLine("\n=== Menu de Estoque ===");
                    Console.WriteLine("1 - Adicionar produto");
                    Console.WriteLine("2 - Editar produto");
                    Console.WriteLine("3 - Remover produto");
                    Console.WriteLine("4 - Listar produtos");
                    Console.WriteLine("0 - Sair");
                    Console.Write("Escolha uma opção: ");

                    string opcao = Console.ReadLine() ?? "";

                    try
                    {
                        if (opcao == "1")
                            AdicionarProduto();
                        else if (opcao == "2")
                            EditarProduto();
                        else if (opcao == "3")
                            RemoverProduto();
                        else if (opcao == "4")
                            ListarProdutos();
                        else if (opcao == "0")
                            continuar = false;
                        else
                            Console.WriteLine("Opção inválida!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Erro ao executar a operação: {e.Message}");
                    }
                }
                catch
                {
                    Console.WriteLine("Ocorreu um erro inesperado no menu. Tente novamente.");
                }
            }

            Console.WriteLine("Programa finalizado.");
        }

        static void AdicionarProduto()
        {
            try
            {
                Console.Write("Nome do produto: ");
                string nome = Console.ReadLine() ?? "";

                double preco = LerDouble("Preço do produto: ");
                int quantidade = LerInt("Quantidade em estoque: ");

                Produto produto = new Produto(nome, preco, quantidade);

                if (produto.ProdutoValido())
                {
                    estoque.Add(produto);
                    Console.WriteLine("Produto cadastrado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro: dados inválidos. Produto não cadastrado.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao adicionar produto: {e.Message}");
            }
        }

        static void EditarProduto()
        {
            try
            {
                Console.Write("Digite o nome do produto que deseja editar: ");
                string nome = Console.ReadLine() ?? "";

                Produto produto = estoque.Find(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    return;
                }

                double novoPreco = LerDouble("Digite o novo preço: ");
                int novaQuantidade = LerInt("Digite a nova quantidade: ");

                produto.AtualizarPreco(novoPreco);
                produto.AtualizarEstoque(novaQuantidade);

                Console.WriteLine("Produto atualizado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao editar produto: {e.Message}");
            }
        }

        static void RemoverProduto()
        {
            try
            {
                Console.Write("Digite o nome do produto que deseja remover: ");
                string nome = Console.ReadLine() ?? "";

                Produto produto = estoque.Find(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    return;
                }

                estoque.Remove(produto);
                Console.WriteLine("Produto removido com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao remover produto: {e.Message}");
            }
        }

        static void ListarProdutos()
        {
            try
            {
                if (estoque.Count == 0)
                {
                    Console.WriteLine("Nenhum produto cadastrado.");
                    return;
                }

                Console.WriteLine("\nProdutos cadastrados:");
                foreach (var p in estoque)
                {
                    Console.WriteLine($"Nome: {p.Nome} | Preço: {p.Preco} | Quantidade: {p.Quantidade}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao listar produtos: {e.Message}");
            }
        }

        static double LerDouble(string mensagem)
        {
            double valor;
            while (true)
            {
                try
                {
                    Console.Write(mensagem);
                    valor = double.Parse(Console.ReadLine() ?? "");
                    if (valor <= 0)
                        throw new Exception("O valor deve ser maior que zero.");
                    break;
                }
                catch
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                }
            }
            return valor;
        }

        static int LerInt(string mensagem)
        {
            int valor;
            while (true)
            {
                try
                {
                    Console.Write(mensagem);
                    valor = int.Parse(Console.ReadLine() ?? "");
                    if (valor < 0)
                        throw new Exception("O valor não pode ser negativo.");
                    break;
                }
                catch
                {
                    Console.WriteLine("Entrada inválida. Tente novamente.");
                }
            }
            return valor;
        }
    }
}
