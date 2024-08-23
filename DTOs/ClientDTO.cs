using System;

namespace api.net5.DTOs
{
    public class ClientDTO
    {
        public int ClienteId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Telephone { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Complement { get; set; }
        public string BirthDate { get; set; }
    }
}
