public class Producao
{
    public int Id { get; private set; }
    public Mina Mina { get; private set; }
    public DateTime Data { get; private set; }
    public decimal Volume { get; private set; }
    public decimal CapacidadeMaxima { get; private set; }

    public Producao(int id, Mina mina, DateTime data, decimal volume, decimal capacidadeMaxima)
    {
        if (volume > capacidadeMaxima)
            throw new ArgumentException("Volume excede a capacidade máxima");

        Id = id;
        Mina = mina;
        Data = data;
        Volume = volume;
        CapacidadeMaxima = capacidadeMaxima;
    }

    public decimal RefinarMinerio(Minerio minerio, Refinamento tipo)
    {
        return tipo switch
        {
            Refinamento.Granularidade => Volume * 0.9m,
            Refinamento.Recuperacao => Volume * 0.8m,
            Refinamento.Teor => Volume * 0.95m,
            _ => throw new InvalidOperationException()
        };
    }

    public void AtualizarVolume(decimal novoVolume)
    {
        if (novoVolume > CapacidadeMaxima)
            throw new ArgumentException("Capacidade excedida");

        Volume = novoVolume;
    }
}
