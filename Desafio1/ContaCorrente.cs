using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio1
{
    public class ContaCorrente : ContaBancaria
    {
        public decimal TaxaSaque { get; private set; } = 2.50m;


        public ContaCorrente(int numeroConta, string titular, decimal saldoInicial)
            : base(numeroConta, titular, saldoInicial) { }

        public override void Sacar(decimal valor)
        {
            decimal valorTotal = valor + TaxaSaque;

            if (valorTotal > Saldo)
                throw new InvalidOperationException($"Saldo insuficiente. Saldo atual: R$ {Saldo:F2}, Valor com taxa (R$ {TaxaSaque:F2}): R$ {valorTotal:F2}");

            Saldo -= valorTotal;
            Console.WriteLine($"Saque de R$ {valor:F2} (Taxa: R$ {TaxaSaque:F2}) realizado na Conta Corrente {NumeroConta}. Novo Saldo: R$ {Saldo:F2}");

        }
    }
}
