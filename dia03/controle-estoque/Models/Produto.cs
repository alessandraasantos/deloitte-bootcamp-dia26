namespace controle_estoque.Models
{
    public class Produto
    {
        public string Nome { get; private set; } = string.Empty;
        public double Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Produto(string nome, double preco, int quantidade)
        {
            // Validação das regras de negócio
            if (string.IsNullOrWhiteSpace(nome)) return;
            if (preco <= 0) return;
            if (quantidade < 0) return;

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public bool ProdutoValido()
        {
            return !string.IsNullOrWhiteSpace(Nome) && Preco > 0 && Quantidade >= 0;
        }

        public void AtualizarPreco(double novoPreco)
        {
            if (novoPreco > 0)
                Preco = novoPreco;
        }

        public void AtualizarEstoque(int novaQuantidade)
        {
            if (novaQuantidade >= 0)
                Quantidade = novaQuantidade;
        }
    }
}
