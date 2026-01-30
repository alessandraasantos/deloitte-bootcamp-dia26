using System.Dynamic;
using System.Security;
using Microsoft.VisualBasic;

public class Minas
{

    Minerio minerio = new Minerio();
    public string codigoMinas { get; set; } = "001";
    public string Nome { get; set; } = "Mina da Vale";
    public decimal Capacidade { get; set; } = 1000;
    string acessarextrairMinerio()
    {
        minerio.codigo = "1";
        minerio.Tipo = "Ouro";

        return "Minerio";
    }

      
     public Minerio extrairminerio2()
    {
        if (GestorMina)
        return this.extrairminerio2();
         else
         minerio minerio = new minerio();
        
        return minerio;
    }


public string getCodigoMinas()
    {
        return this.codigoMinas;
    }

public string setCodigoMinas(string codigoMinas)
    {
        
    }


}

public class Producao
{
    int id;
    public string Nome { get; set; } = "Mina da Vale";
    public Minas CodigoMinas { get; set; }
    public decimal Capacidade { get; set; } = 1000;
    public DateAndTime Ano { get; set; } = 2026;
    public decimal Quantidade { get; set; }
     public string encaminharParaEstoque()
    {
        return "Encaminhado para o estoque";
    }
    decimal getVolume()
    {
        return this.Volume;
    }

    decimal setVolume(decimal Volume)
    {
        this.Volume = Volume;
    }   

    //getter e setter 

    public int refinarMinerio(Minerio minerio)
    {
        
        return quantidadeFinalRefinamento(minerio);
    }
   private int quantidadeFinalRefinamento(Minerio minerio)
    {
        
        return quantidadeRefinada;
    }
}

private class Estoque
{
    int id;
    int ProducaoId;
    public DateAndTime Ano { get; set; } = 2026;
    public string Tipo { get; set; }
    public decimal Quantidade { get; set; }
    public Minas CodigoMinas { get; set; }

    public string distribuirMinerio()
    {
        return "Distribuido para venda";
    }
    

}