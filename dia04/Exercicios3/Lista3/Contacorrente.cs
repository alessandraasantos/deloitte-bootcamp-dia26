public class ContaCorrente
{
    public string Numero { get; }
    public decimal Saldo { get; private set; }
    public bool EhEspecial { get; }
    public decimal Limite { get; private set; }

    public ContaCorrente(string numero, decimal saldoInicial, bool especial, decimal limite)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("Número da conta é obrigatório.", nameof(numero));

        if (limite < 0)
            throw new ArgumentOutOfRangeException(nameof(limite), "Limite não pode ser negativo.");

        Numero = numero;
        Saldo = saldoInicial;
        EhEspecial = especial;
        Limite = limite;
    }

    public bool Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "Valor de saque deve ser positivo.");

        
        if (!EhEspecial)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
                return true;
            }
            return false;
        }

        if (Saldo + Limite >= valor)
        {
            Saldo -= valor;
            return true;
        }

        return false;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "Valor de depósito deve ser positivo.");

        Saldo += valor;
    }

    public decimal ConsultarSaldo() => Saldo;

    public bool EstaUsandoChequeEspecial() => Saldo < 0;

    public override string ToString()
    {
        return $"Conta: {Numero}, Saldo: {Saldo:C}, Especial: {(EhEspecial ? "Sim" : "Não")}, Limite: {Limite:C}";
    }
}
