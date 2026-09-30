using System;
using System.Collections.Generic;

namespace Desafio1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ContaBancaria> banco = new List<ContaBancaria>();
            bool executando = true;

            while (executando)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("       SISTEMA BANCÁRIO            ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Cadastrar Conta Corrente");
                Console.WriteLine("2. Cadastrar Conta Poupança");
                Console.WriteLine("3. Cadastrar Conta Empresarial");
                Console.WriteLine("4. Listar Todas as Contas");
                Console.WriteLine("5. Acessar uma Conta"); // MENU GERENCIAL
                Console.WriteLine("0. Sair");
                Console.WriteLine("====================================");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            CadastrarContaCorrente(banco);
                            break;
                        case "2":
                            CadastrarContaPoupanca(banco);
                            break;
                        case "3":
                            CadastrarContaEmpresarial(banco);
                            break;
                        case "4":
                            ListarContas(banco);
                            break;
                        case "5":
                            AcessarConta(banco); // Abre o menu da conta específica
                            break;
                        case "0":
                            executando = false;
                            Console.WriteLine("\nObrigado por utilizar o nosso sistema!");
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida! Pressione ENTER para tentar novamente.");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n[Erro]: Digite um valor numérico válido!");
                    Pausar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro]: {ex.Message}");
                    Pausar();
                }
            }
        }

        // --- SUBMENU OPERACIONAL DA CONTA ---

        static void AcessarConta(List<ContaBancaria> banco)
        {
            Console.Clear();
            Console.WriteLine("=== ACESSAR CONTA ===");
            Console.Write("Digite o número da conta: ");

            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("[Erro]: Número de conta inválido!");
                Pausar();
                return;
            }

            ContaBancaria conta = banco.Find(c => c.NumeroConta == numero);

            if (conta == null)
            {
                Console.WriteLine("[Erro]: Conta não encontrada!");
                Pausar();
                return;
            }

            // Abre o menu com as opções exclusivas da conta encontrada
            MenuOperacoesConta(conta);
        }

        static void MenuOperacoesConta(ContaBancaria conta)
        {
            bool noSubMenu = true;

            while (noSubMenu)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine($"  CONTA #{conta.NumeroConta} - {conta.Titular.ToUpper()}");
                Console.WriteLine($"  Saldo Atual: R$ {conta.Saldo:F2}");

                // Exibe informação de limite se for Conta Empresarial
                if (conta is ContaEmpresarial emp)
                {
                    Console.WriteLine($"  Limite de Empréstimo: R$ {emp.LimiteEmprestimo:F2}");
                }

                Console.WriteLine("====================================");
                Console.WriteLine("1. Realizar Depósito");
                Console.WriteLine("2. Realizar Saque");

                // Opções exclusivas renderizadas dinamicamente
                if (conta is ContaPoupanca)
                {
                    Console.WriteLine("3. Aplicar Rendimento");
                }
                else if (conta is ContaEmpresarial)
                {
                    Console.WriteLine("3. Solicitar Empréstimo");
                }

                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.WriteLine("====================================");
                Console.Write("Escolha uma operação: ");

                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Write("\nDigite o valor do depósito: R$ ");
                            decimal valDeposito = decimal.Parse(Console.ReadLine());
                            conta.Depositar(valDeposito);
                            Pausar();
                            break;

                        case "2":
                            Console.Write("\nDigite o valor do saque: R$ ");
                            decimal valSaque = decimal.Parse(Console.ReadLine());
                            conta.Sacar(valSaque);
                            Pausar();
                            break;

                        case "3":
                            if (conta is ContaPoupanca poupanca)
                            {
                                Console.Write("\nDigite a taxa de rendimento (%): ");
                                decimal percentual = decimal.Parse(Console.ReadLine());
                                poupanca.AplicarRendimento(percentual);
                            }
                            else if (conta is ContaEmpresarial empresarial)
                            {
                                Console.Write("\nDigite o valor do empréstimo: R$ ");
                                decimal valorEmprestimo = decimal.Parse(Console.ReadLine());
                                empresarial.RealizarEmprestimo(valorEmprestimo);
                            }
                            else
                            {
                                Console.WriteLine("\nOpção inválida para Conta Corrente!");
                            }
                            Pausar();
                            break;

                        case "0":
                            noSubMenu = false;
                            break;

                        default:
                            Console.WriteLine("\nOpção inválida!");
                            Pausar();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n[Erro]: Digite um valor numérico válido!");
                    Pausar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Erro]: {ex.Message}");
                    Pausar();
                }
            }
        }

        // --- MÉTODOS AUXILIARES DO MENU ---

        static void CadastrarContaCorrente(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- CADASTRO DE CONTA CORRENTE ---");
            int numero = LerNumeroContaValido(banco);

            Console.Write("Titular: ");
            string titular = Console.ReadLine();

            Console.Write("Saldo Inicial: R$ ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            banco.Add(new ContaCorrente(numero, titular, saldo));
            Console.WriteLine("\nConta Corrente cadastrada com sucesso!");
            Pausar();
        }

        static void CadastrarContaPoupanca(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- CADASTRO DE CONTA POUPANÇA ---");
            int numero = LerNumeroContaValido(banco);

            Console.Write("Titular: ");
            string titular = Console.ReadLine();

            Console.Write("Saldo Inicial: R$ ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            banco.Add(new ContaPoupanca(numero, titular, saldo));
            Console.WriteLine("\nConta Poupança cadastrada com sucesso!");
            Pausar();
        }

        static void CadastrarContaEmpresarial(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- CADASTRO DE CONTA EMPRESARIAL ---");
            int numero = LerNumeroContaValido(banco);

            Console.Write("Titular (Nome da Empresa): ");
            string titular = Console.ReadLine();

            Console.Write("Saldo Inicial: R$ ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            Console.Write("Limite de Empréstimo: R$ ");
            decimal limite = decimal.Parse(Console.ReadLine());

            banco.Add(new ContaEmpresarial(numero, titular, saldo, limite));
            Console.WriteLine("\nConta Empresarial cadastrada com sucesso!");
            Pausar();
        }

        static void ListarContas(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- CONTAS CADASTRADAS ---");
            if (banco.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada até ao momento.");
            }
            else
            {
                foreach (var conta in banco)
                {
                    Console.WriteLine(conta);
                }
            }
            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        static int LerNumeroContaValido(List<ContaBancaria> banco)
        {
            while (true)
            {
                Console.Write("Número da Conta: ");
                if (!int.TryParse(Console.ReadLine(), out int numero) || numero <= 0)
                {
                    Console.WriteLine("[Erro]: Digite um número positivo válido!");
                    continue;
                }

                if (banco.Exists(c => c.NumeroConta == numero))
                {
                    Console.WriteLine($"[Erro]: Já existe uma conta cadastrada com o número {numero}! Tente outro.");
                    continue;
                }

                return numero;
            }
        }
    }
}