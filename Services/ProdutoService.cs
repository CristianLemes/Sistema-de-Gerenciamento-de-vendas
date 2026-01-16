using ProjetoTCN.Data;
using ProjetoTCN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Services
{
    internal class ProdutoService
    {
        private readonly GerenciadorDados gerenciador;

        public ProdutoService(GerenciadorDados gerenciador)
        {
            this.gerenciador = gerenciador;
        }

        public void CadastrarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRAR PRODUTO ===");

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Valor do produto: R$ ");
            decimal valor = decimal.Parse(Console.ReadLine());

            Console.Write("Quantidade em estoque: ");
            int quantidade = int.Parse(Console.ReadLine());

            var produto = new Produto
            {
                NomeProduto = nome,
                ValorProduto = valor,
                QuantidadeProduto = quantidade
            };

            gerenciador.AdicionarProduto(produto);
            Console.WriteLine("\nProduto cadastrado com sucesso!");
            Console.ReadKey();
        }

        public void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("=== PRODUTOS CADASTRADOS ===\n");

            var produtos = gerenciador.ObterProdutos();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            else
            {
                foreach (var p in produtos)
                {
                    Console.WriteLine($"ID: {p.IdProduto}");
                    Console.WriteLine($"Nome: {p.NomeProduto}");
                    Console.WriteLine($"Valor: R$ {p.ValorProduto:F2}");
                    Console.WriteLine($"Quantidade: {p.QuantidadeProduto}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.ReadKey();
        }

        public void AlterarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== ALTERAR PRODUTO ===");

            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());

            var produto = gerenciador.BuscarProduto(id);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nProduto atual: {produto.NomeProduto}");
            Console.Write("Novo nome (Enter para manter): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrEmpty(nome)) produto.NomeProduto = nome;

            Console.Write("Novo valor (Enter para manter): ");
            string valorStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(valorStr)) produto.ValorProduto = decimal.Parse(valorStr);

            Console.Write("Nova quantidade (Enter para manter): ");
            string qtdStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(qtdStr)) produto.QuantidadeProduto = int.Parse(qtdStr);

            gerenciador.AtualizarProduto(produto);
            Console.WriteLine("\nProduto atualizado com sucesso!");
            Console.ReadKey();
        }

        public void DeletarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== DELETAR PRODUTO ===");

            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine());

            var produto = gerenciador.BuscarProduto(id);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
            }
            else
            {
                Console.Write($"\nConfirma exclusão de '{produto.NomeProduto}'? (S/N): ");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    gerenciador.DeletarProduto(id);
                    Console.WriteLine("Produto deletado com sucesso!");
                }
            }

            Console.ReadKey();
        }
    }
}

