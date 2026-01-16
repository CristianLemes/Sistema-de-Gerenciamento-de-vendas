using ProjetoTCN.Data;
using ProjetoTCN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Services
{
    internal class OrcamentoService
    {
        private readonly GerenciadorDados gerenciador;

        public OrcamentoService(GerenciadorDados gerenciador)
        {
            this.gerenciador = gerenciador;
        }

        public void CriarOrcamento()
        {
            Console.Clear();
            Console.WriteLine("=== CRIAR ORÇAMENTO ===");

            Console.Write("Nome do cliente: ");
            string nome = Console.ReadLine();

            var orcamento = new Orcamento
            {
                Data = DateTime.Now,
                NomeCliente = nome
            };

            while (true)
            {
                Console.Write("\nID do produto (0 para finalizar): ");
                int idProduto = int.Parse(Console.ReadLine());

                if (idProduto == 0) break;

                var produto = gerenciador.BuscarProduto(idProduto);

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado!");
                    continue;
                }

                Console.Write("Quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());

                orcamento.Itens.Add(new ItemOrcamento
                {
                    Produto = produto,
                    Quantidade = quantidade
                });

                Console.WriteLine($"Produto '{produto.NomeProduto}' adicionado!");
            }

            if (orcamento.Itens.Any())
            {
                gerenciador.AdicionarOrcamento(orcamento);
                ImprimirOrcamento(orcamento);
                Console.WriteLine("\nOrçamento criado com sucesso!");
            }
            else
            {
                Console.WriteLine("\nOrçamento cancelado - nenhum produto adicionado.");
            }

            Console.ReadKey();
        }

        public void ListarOrcamentos()
        {
            Console.Clear();
            Console.WriteLine("=== ORÇAMENTOS CRIADOS ===\n");

            var orcamentos = gerenciador.ObterOrcamentos();

            if (orcamentos.Count == 0)
            {
                Console.WriteLine("Nenhum orçamento criado.");
            }
            else
            {
                foreach (var orcamento in orcamentos)
                {
                    ImprimirOrcamento(orcamento);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        private void ImprimirOrcamento(Orcamento orcamento)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("                  ORÇAMENTO");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"ID Orçamento: {orcamento.IdOrcamento}");
            Console.WriteLine($"Data: {orcamento.Data:dd/MM/yyyy}");
            Console.WriteLine($"Cliente: {orcamento.NomeCliente}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("ITENS:");

            foreach (var item in orcamento.Itens)
            {
                Console.WriteLine($"{item.Produto.NomeProduto} - Qtd: {item.Quantidade} x R$ {item.Produto.ValorProduto:F2} = R$ {item.ValorTotal:F2}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"TOTAL: R$ {orcamento.Total:F2}");
            Console.WriteLine(new string('=', 50));
        }
    }
}
