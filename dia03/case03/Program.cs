var mina = new Mina("001", "Mina Alpha", 1000);

// Gestor acessando
var minerio = mina.AcessarExtrairMinerio(true);
Console.WriteLine($"Minério extraído: {minerio.Tipo}");

var producao = new Producao(
    1,
    mina,
    DateTime.Now,
    500,
    1000
);

var estoque = new Estoque(
    1,
    producao,
    300,
    "Galpão Central"
);

estoque.Retirar(50);

Console.WriteLine($"Estoque restante: {estoque.Quantidade}");
Console.WriteLine($"Valor total: {estoque.CalcularValor(200)}");
