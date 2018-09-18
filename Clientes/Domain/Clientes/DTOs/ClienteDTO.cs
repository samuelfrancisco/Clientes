using System;

namespace Clientes.Domain.Clientes.DTOs
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public decimal Salario { get; set; }
    }
}
