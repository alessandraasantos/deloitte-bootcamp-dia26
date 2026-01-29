using  System.ComponentModel.DataAnnotations;
namespace api_piloto.DTOs
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(1, ErrorMessage = "O nome não pode ser vazio.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A idade deve ser maior que 0")]
        public int Idade { get; set; }
    }
}