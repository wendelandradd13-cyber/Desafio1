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
                Console.WriteLine("    SISTEMA BANCÁRIO     ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Cadastrar Conta Corrente");
                Console.WriteLine("2. Cadastrar Conta Poupança");
                Console.WriteLine("3. Cadastrar Conta Empresarial");
                Console.WriteLine("4. Listar Todas as Contas");
                Console.WriteLine("5. Realizar Depósito");
                Console.WriteLine("6. Realizar Saque");
                Console.WriteLine("7. Aplicar Rendimento na Poupança");
                Console.WriteLine("8. Solicitar Empréstimo (Empresarial)");
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
                            Depositar(banco);
                            break;
                        case "6":
                            Sacar(banco);
                            break;
                        case "7":
                            AplicarRendimentoPoupanca(banco);
                            break;
                        case "8":
                            RealizarEmprestimo(banco);
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

        // --- MÉTODOS AUXILIARES DO MENU ---

        static void CadastrarContaCorrente(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- CADASTRO DE CONTA CORRENTE ---");
            Console.Write("Número da Conta: ");
            int numero = int.Parse(Console.ReadLine());

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

        static void Depositar(List<ContaBancaria> banco)
        {
            Console.Write("\nDigite o número da conta para depósito: ");
            int numero = int.Parse(Console.ReadLine());

            ContaBancaria conta = banco.Find(c => c.NumeroConta == numero);

            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
            }
            else
            {
                Console.Write("Digite o valor do depósito: R$ ");
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.Depositar(valor);
            }
            Pausar();
        }

        static void Sacar(List<ContaBancaria> banco)
        {
            Console.Write("\nDigite o número da conta para saque: ");
            int numero = int.Parse(Console.ReadLine());

            ContaBancaria conta = banco.Find(c => c.NumeroConta == numero);

            if (conta == null)
            {
                Console.WriteLine("Conta não encontrada!");
            }
            else
            {
                Console.Write("Digite o valor do saque: R$ ");
                decimal valor = decimal.Parse(Console.ReadLine());
                conta.Sacar(valor);
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

                // Verifica se já existe alguma conta na lista com esse mesmo número
                if (banco.Exists(c => c.NumeroConta == numero))
                {
                    Console.WriteLine($"[Erro]: Já existe uma conta cadastrada com o número {numero}! Tente outro.");
                    continue;
                }

                return numero; // Número é válido e não está duplicado!
            }
        }

        static void AplicarRendimentoPoupanca(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- APLICAR RENDIMENTO NA POUPANÇA ---");
            Console.Write("Digite o número da Conta Poupança: ");

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
            }
            else if (conta is ContaPoupanca poupanca)
            {
                Console.Write("Digite a taxa de rendimento (%): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal percentual))
                {
                    try
                    {
                        poupanca.AplicarRendimento(percentual);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Erro ao aplicar rendimento]: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("[Erro]: Digite um valor percentual válido!");
                }
            }
            else
            {
                Console.WriteLine("[Erro]: Esta conta não é uma Conta Poupança!");
            }
            Pausar();
        }

        static void RealizarEmprestimo(List<ContaBancaria> banco)
        {
            Console.WriteLine("\n--- REALIZAR EMPRÉSTIMO ---");
            Console.Write("Digite o número da Conta: ");
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
            }
            else if (conta is ContaEmpresarial empresarial)
            {
                Console.WriteLine($"Limite disponível para empréstimo: R$ {empresarial.LimiteEmprestimo:F2}\n");
                Console.Write("Digite o valor do empréstimo: R$ ");

                if (decimal.TryParse(Console.ReadLine(), out decimal valor))
                {
                    try
                    {
                        empresarial.RealizarEmprestimo(valor);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Erro ao realizar empréstimo]: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("[Erro]: Digite um valor válido para o empréstimo!");
                }
            }
            else
            {
                Console.WriteLine("[Erro]: Esta conta não é uma Conta Empresarial!");
            }
            Pausar();

        }
    }

}
