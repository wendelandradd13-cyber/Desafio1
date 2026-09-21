using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio1
{
    public abstract class ContaBancaria : IOperacaoBancaria
    {
        public int NumeroConta { get; private set; }
        public string Titular { get; private set; }
        public decimal Saldo { get; protected set; }

        protected ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
        {
            // Validação do número da conta
            if (numeroConta <= 0)
                throw new ArgumentException("Número de conta inválido! Digite um número maior que zero.");

            // Validação do titular
            if (string.IsNullOrWhiteSpace(titular))
                throw new ArgumentException("Nome do titular não pode ser vazio.");

            // Validação do saldo inicial
            if (saldoInicial < 0)
                throw new ArgumentException("O saldo inicial não pode ser negativo.");

            NumeroConta = numeroConta;
            Titular = titular;
            Saldo = saldoInicial;
        }

        public virtual void Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");
            }
            Saldo += valor;
            Console.WriteLine($"Depósito de R$ {valor:F2} realizado na conta {NumeroConta}. Saldo atual: R$ {Saldo:F2}");
        }

        public abstract void Sacar(decimal valor);

        public override string ToString()
        {
            return $"Conta: {NumeroConta} | Titular: {Titular} | Saldo: R$ {Saldo:F2}";
        }
    }
}
