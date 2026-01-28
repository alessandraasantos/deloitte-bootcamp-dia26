using System;
using System.Collections.Generic;
using controle_estoque.Models;

namespace controle_estoque
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Produto> estoque = new List<Produto>();

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            double preco = double.Parse(Console.ReadLine());

            Console.Write("Quantidade em estoque: ");
            int quantidade = int.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, preco, quantidade);

            if (produto.ProdutoValido())
            {
                estoque.Add(produto);
                Console.WriteLine("Produto cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro: dados inválidos.");
            }

            Console.WriteLine("\nProdutos cadastrados:");
            foreach (Produto p in estoque)
            {
                Console.WriteLine($"Nome: {p.Nome} | Preço: {p.Preco} | Quantidade: {p.Quantidade}");
            }
        }
    }
}
