using ProjetoTCN.Data;
using ProjetoTCN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.UI
{
    internal class MenuPrincipal
    {
        private readonly GerenciadorDados gerenciador;
        private readonly ProdutoService produtoService;
        private readonly VendaService vendaService;
        private readonly OrcamentoService orcamentoService;

        public MenuPrincipal()
        {
            gerenciador = new GerenciadorDados();
            produtoService = new ProdutoService(gerenciador);
            vendaService = new VendaService(gerenciador);
            orcamentoService = new OrcamentoService(gerenciador);
        }

        public void Executar()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE VENDAS ===");
                Console.WriteLine("1. Gerenciar Produtos");
                Console.WriteLine("2. Realizar Venda");
                Console.WriteLine("3. Criar Orçamento");
                Console.WriteLine("4. Listar Vendas");
                Console.WriteLine("5. Listar Orçamentos");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": MenuProdutos(); break;
                    case "2": vendaService.RealizarVenda(); break;
                    case "3": orcamentoService.CriarOrcamento(); break;
                    case "4": vendaService.ListarVendas(); break;
                    case "5": orcamentoService.ListarOrcamentos(); break;
                    case "0": return;
                }
            }
        }

        private void MenuProdutos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GERENCIAR PRODUTOS ===");
                Console.WriteLine("1. Cadastrar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Alterar Produto");
                Console.WriteLine("4. Deletar Produto");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": produtoService.CadastrarProduto(); break;
                    case "2": produtoService.ListarProdutos(); break;
                    case "3": produtoService.AlterarProduto(); break;
                    case "4": produtoService.DeletarProduto(); break;
                    case "0": return;
                }
            }
        }
    }
}
