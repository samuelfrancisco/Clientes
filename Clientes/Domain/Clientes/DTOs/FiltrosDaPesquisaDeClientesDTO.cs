using System;

namespace Clientes.Domain.Clientes.DTOs
{
    public class FiltrosDaPesquisaDeClientesDTO
    {
        public string Nome { get; set; }
        public DateTime? DataDeNascimentoDe { get; set; }
        public DateTime? DataDeNascimentoAte { get; set; }
        public decimal? SalarioDe { get; set; }
        public decimal? SalarioAte { get; set; }
    }
}
