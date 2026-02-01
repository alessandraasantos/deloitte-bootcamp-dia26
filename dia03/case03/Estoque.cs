public class Estoque
{
    public int Id { get; set;}
    public Producao Producao {get; private set;}
    public decimal Quantidade {get; private set;}
    public string Local {get; private set;}

    public Estoque(int id, Producao producao, decimal quantidade, string local)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade inválida");

        Id = id;
        Producao = producao;    
        Quantidade = quantidade;
        Local = local;


    }

public void Retirar(decimal quantidade)
    {
        if (quantidade > Quantidade)
            throw new InvalidOperationException("Estoque insuficiente");

        Quantidade -= quantidade;
    }

    public decimal CalcularValor(decimal precoPorTonelada)
    {
        return Quantidade * precoPorTonelada;
    }
}