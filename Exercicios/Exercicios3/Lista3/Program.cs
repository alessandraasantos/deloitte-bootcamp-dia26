//{
//Lampada lampada = new Lampada();

//lampada.Ligar();
//Console.WriteLine("Estado de lampada: " + (lampada.EstaLigada() ? "Ligada" : "Desligada"));
//lampada.Desligada();
//Console.WriteLine("Estado de lampada: " + (lampada.EstaLigada() ? "Ligada" : "Desligada"));
//}//


/// 


       var contaUsuario = new ContaCorrente(numero: "S45674-L", saldoInicial: 500, especial: false, limite: 0);
       Console.WriteLine("Conta do Usuário:");
       Console.WriteLine(contaUsuario);


       Console.WriteLine("Tentando sacar R$ 600,00 (deve falhar)");
       bool sacou = contaUsuario.Sacar(700);
       Console.WriteLine($"Saque realizado? {(sacou ? "Sim" : "Não")}. Saldo: {contaUsuario.ConsultarSaldo():F2}");

       Console.WriteLine("Depositando R$ 300,00");
       contaUsuario.Depositar(300);
       Console.WriteLine($"Saldo após depósito: {contaUsuario.ConsultarSaldo():F2}");
       Console.WriteLine("Usando cheque especial? " + (contaUsuario.EstaUsandoChequeEspecial() ? "Sim" : "Não"));

       Console.WriteLine(contaUsuario);