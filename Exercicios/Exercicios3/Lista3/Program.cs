//{
//Lampada lampada = new Lampada();

//lampada.Ligar();
//Console.WriteLine("Estado de lampada: " + (lampada.EstaLigada() ? "Ligada" : "Desligada"));
//lampada.Desligada();
//Console.WriteLine("Estado de lampada: " + (lampada.EstaLigada() ? "Ligada" : "Desligada"));
//}//


/// 

var contaUsuario = new ContaCorrente(
    numero: "S45674-L",
    saldoInicial: 500m,
    especial: false,
    limite: 0m
);

Console.WriteLine("Conta do Usuário:");
Console.WriteLine(contaUsuario);

Console.WriteLine("\nTentando sacar R$ 700,00 (deve falhar)");
bool sacou = contaUsuario.Sacar(700m);
Console.WriteLine($"Saque realizado? {(sacou ? "Sim" : "Não")}. Saldo: {contaUsuario.ConsultarSaldo():C}");

Console.WriteLine("\nDepositando R$ 300,00");
contaUsuario.Depositar(300m);
Console.WriteLine($"Saldo após depósito: {contaUsuario.ConsultarSaldo():C}");

Console.WriteLine("Usando cheque especial? " +
    (contaUsuario.EstaUsandoChequeEspecial() ? "Sim" : "Não"));

Console.WriteLine("\n" + contaUsuario);
