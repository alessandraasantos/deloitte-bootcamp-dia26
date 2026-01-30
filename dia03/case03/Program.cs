using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");

Minas mina = new Minas();
Minerio minerioExtraido = mina.extrairminerio2();
Console.WriteLine(minerioExtraido.Tipo
);

Producao producao = new Producao();
producao.CodigoMinas = mina;
Console.WriteLine(producao.CodigoMinas.Nome);



Estoque estoque = new Estoque();
estoque.CodigoMinas = mina;

Console.WriteLine(estoque.CodigoMinas.Nome);