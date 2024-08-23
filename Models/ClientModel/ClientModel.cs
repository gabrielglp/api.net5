using System.ComponentModel.DataAnnotations;
using System;

namespace api.net5.Models.ClientModel
{
    public class ClientModel
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string UF { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string District { get; set; }

        public string Complement { get; set; }
    }
}
