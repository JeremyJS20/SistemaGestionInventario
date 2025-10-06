using System.ComponentModel.DataAnnotations;

namespace SistemaGestionInventario.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El codigo es requerido.")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "La categoria es requerida.")]
        public int Category { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio es requerido.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "El stock es requerido.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El sotck minimo es requerido.")]
        public int MinimumStock { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public string? State { get; set; }
    }
}
