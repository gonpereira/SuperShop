using System;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
    public class Product:IEntity
    {

        public int Id { get; set; } //detecta que é um id inteiro então cria-o como chave primaria tem de ser ID

        //[Key]
        //public int IdProduct { get; set; } //se quisesse que isto fosse chave primaria teria que usar o "[key]" a "DataAnnotations" transformar numa chave primaria
        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)] //formata isto com duas casas decimais em modo moeda mas em modo edicao não aplica formato nenhum
        public decimal Price { get; set; }

        [Display(Name = "Image")] //como este campo se vai chamar na página da web
        public string ImageUrl { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

    }
}
