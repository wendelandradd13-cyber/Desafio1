using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio1
{
    public class ContaEmpresarial : ContaBancaria
    {
        public decimal LimiteEmprestimo { get; private set; }

        public ContaEmpresarial(int numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
            : base(numeroConta, titular, saldoInicial)
        {
            LimiteEmprestimo = limiteEmprestimo;
        }

        public override void Sacar(decimal valor)
        {
            if (valor > Saldo)
                throw new InvalidOperationException($"Saldo insuficiente na Conta Empresarial {NumeroConta}. Saldo atual: R$ {Saldo:F2}");
            Saldo -= valor;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado na Conta Empresarial {NumeroConta}. Novo Saldo: R$ {Saldo:F2}");
        }

        public void RealizarEmprestimo(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("O valor do empréstimo deve ser maior que zero."); 

            if (valor > LimiteEmprestimo)
                throw new InvalidOperationException($"Valor solicitado (R$ {valor:F2}) excede o limite disponível (R$ {LimiteEmprestimo:F2}).");

            Saldo += valor;
            LimiteEmprestimo -= valor;
            Console.WriteLine($"Empréstimo de R$ {valor:F2} aprovado! Saldo atual: R$ {Saldo:F2}. Limite restante: R$ {LimiteEmprestimo:F2}");
        }
    }
}
