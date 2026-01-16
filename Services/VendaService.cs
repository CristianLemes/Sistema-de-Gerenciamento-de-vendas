using ProjetoTCN.Data;
using ProjetoTCN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Services
{
    internal class VendaService
    {
        private readonly GerenciadorDados gerenciador;

        public VendaService(GerenciadorDados gerenciador)
        {
            this.gerenciador = gerenciador;
        }

        public void RealizarVenda()
        {
            Console.Clear();
            Console.WriteLine("=== REALIZAR VENDA ===");

            Console.Write("Nome do cliente: ");
            string nome = Console.ReadLine();

            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            var venda = new Venda
            {
                Data = DateTime.Now,
                NomeCliente = nome,
                EnderecoCliente = endereco,
                TelefoneCliente = telefone
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

                if (quantidade > produto.QuantidadeProduto)
                {
                    Console.WriteLine("Quantidade insuficiente em estoque!");
                    continue;
                }

                venda.Itens.Add(new ItemVenda
                {
                    Produto = produto,
                    Quantidade = quantidade
                });

                Console.WriteLine($"Produto '{produto.NomeProduto}' adicionado!");
            }

            if (venda.Itens.Any())
            {
                gerenciador.AdicionarVenda(venda);
                ImprimirVenda(venda);
                Console.WriteLine("\nVenda realizada com sucesso!");
            }
            else
            {
                Console.WriteLine("\nVenda cancelada - nenhum produto adicionado.");
            }

            Console.ReadKey();
        }

        public void ListarVendas()
        {
            Console.Clear();
            Console.WriteLine("=== VENDAS REALIZADAS ===\n");

            var vendas = gerenciador.ObterVendas();

            if (vendas.Count == 0)
            {
                Console.WriteLine("Nenhuma venda realizada.");
            }
            else
            {
                foreach (var venda in vendas)
                {
                    ImprimirVenda(venda);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        private void ImprimirVenda(Venda venda)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("                    VENDA");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"ID Venda: {venda.IdVenda}");
            Console.WriteLine($"Data: {venda.Data:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Cliente: {venda.NomeCliente}");
            Console.WriteLine($"Endereço: {venda.EnderecoCliente}");
            Console.WriteLine($"Telefone: {venda.TelefoneCliente}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("ITENS:");

            foreach (var item in venda.Itens)
            {
                Console.WriteLine($"{item.Produto.NomeProduto} - Qtd: {item.Quantidade} x R$ {item.Produto.ValorProduto:F2} = R$ {item.ValorTotal:F2}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"TOTAL: R$ {venda.Total:F2}");
            Console.WriteLine(new string('=', 50));
        }
    }
}