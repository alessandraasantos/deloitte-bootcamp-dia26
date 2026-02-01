public class Minerio
{
    public string Codigo { get; private set; }
    public string Tipo { get; private set; }

    public Minerio(string codigo, string tipo)
    {
        Codigo = codigo;
        Tipo = tipo;
    }
}
