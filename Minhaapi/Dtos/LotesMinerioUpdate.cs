namespace MinhaApi.Dtos
{
    public record LotesMinerioUpdate(
        int Id,
        string CodigoLote,
        string MinaOrigem,
        decimal TeorFe,
        decimal Umidade,
        decimal? SiO2,
        decimal? P,
        decimal Toneladas,
        DateTime DataProducao,      
        int Status,
        string LocalizacaoAtual
    );
}