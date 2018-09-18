using System;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Application.Clientes.ViewModels
{
    public class CadastroDeClienteViewModel
    {
        [Required(ErrorMessage = "Favor informar o nome do cliente.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor informar a data de nascimento do cliente.")]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "Favor informar o salário do cliente.")]
        public decimal Salario { get; set; }
    }
}
