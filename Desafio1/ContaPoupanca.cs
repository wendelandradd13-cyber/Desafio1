using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio1
{
    public class ContaPoupanca : ContaBancaria
    {
        public ContaPoupanca(int numeroConta, string titular, decimal saldoInicial)
            : base(numeroConta, titular, saldoInicial) { }

        public override void Sacar(decimal valor)
        {
            if (valor > Saldo)
                throw new InvalidOperationException($"Saldo insuficiente na Conta Poupança {NumeroConta}. Saldo atual: R$ {Saldo:F2}");

            Saldo -= valor;
            Console.WriteLine($"Saque de R$ {valor:F2} realizado na Conta Poupança {NumeroConta}. Novo Saldo: R$ {Saldo:F2}");
        }

        public void AplicarRendimento(decimal percentual)
        {
            if (percentual <= 0)
                throw new ArgumentException("O percentual de rendimento deve ser maior que zero.");

            decimal rendimento = Saldo * (percentual / 100);
            Saldo += rendimento;
            Console.WriteLine($"Rendimento de {percentual}% aplicado na Conta Poupança {NumeroConta}. Valor ganho: R$ {rendimento:F2}. Saldo atual: R$ {Saldo:F2}");
        }
    }
}