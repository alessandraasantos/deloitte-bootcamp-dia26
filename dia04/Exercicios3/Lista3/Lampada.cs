
public class Lampada
{
    private bool isligada;

    public Lampada()
    {
        isligada = false;
    }

    public void Ligar()
    {
        isligada = true;
        Console.WriteLine("Lampada ligada.");
    }

    public void Desligada()
    {
        isligada = false;
        Console.WriteLine("Lampada desligada.");
    }

    public bool EstaLigada()
    {
        return isligada;
    }

}