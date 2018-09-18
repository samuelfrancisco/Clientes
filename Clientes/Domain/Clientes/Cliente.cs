using Clientes.Domain.Core;
using System;

namespace Clientes.Domain.Clientes
{
    public class Cliente
    {
        protected Cliente()
        {

        }

        public Cliente(string nome, DateTime dataDeNascimento, decimal salario)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Salario = salario;
        }

        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime DataDeNascimento { get; protected set; }
        public decimal Salario { get; protected set; }

        public static Result<Cliente> RegistrarNovoCliente(string nome, DateTime dataDeNascimento, decimal salario)
        {
            var result = ValidarDados(nome, dataDeNascimento, salario);

            if (result.IsFailure)
                return result.ToResult<Cliente>();

            return result.ToResult<Cliente>(new Cliente(nome, dataDeNascimento, salario));
        }

        public Result AlterarDados(string nome, DateTime dataDeNascimento, decimal salario)
        {
            var result = ValidarDados(nome, dataDeNascimento, salario);

            if (result.IsFailure)
                return result;

            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Salario = salario;

            return result;
        }

        private static Result ValidarDados(string nome, DateTime dataDeNascimento, decimal salario)
        {
            var result = new Result();

            if (string.IsNullOrWhiteSpace(nome))
                result.AddError("Nome inválido.", "O nome não pode ser nulo ou vazio.", typeof(Cliente).FullName);

            return result;
        }
    }
}
