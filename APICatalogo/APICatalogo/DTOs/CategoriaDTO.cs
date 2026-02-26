using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class CategoriaDTO
    {
        [Key]//Anotação para definir a chave primaria, mas é opcional nesse caso
        public int CategoriaId { get; set; } //Entity Framework entende que Id= chave primaria, e nome da classe+id tambem (Categoria+Id)

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

    }
}
