namespace controle_estoque.Models
{
    public class Produto
    {
        public string Nome { get; private set; }
        public double Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Produto(string nome, double preco, int quantidade)
        {
            // Validação das regras de negócio
            if (string.IsNullOrWhiteSpace(nome))
                return;

            if (preco <= 0)
                return;

            if (quantidade < 0)
                return;

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public bool ProdutoValido()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }
    }
}
