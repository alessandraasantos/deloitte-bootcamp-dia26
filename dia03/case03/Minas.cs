public class Mina
{
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public decimal Capacidade { get; private set; }

    public Mina(string codigo, string nome, decimal capacidade)
    {
        Codigo = codigo;
        Nome = nome;
        Capacidade = capacidade;
    }

    public Minerio AcessarExtrairMinerio(bool isGestorMina)
    {
        if (!isGestorMina)
            throw new UnauthorizedAccessException("Usuário não é gestor da mina");

        return ExtrairMinerio();
    }

    private Minerio ExtrairMinerio()
    {
        return new Minerio("M-001", "Ouro");
    }
}
