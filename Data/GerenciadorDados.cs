using ProjetoTCN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetoTCN.Data
{
    internal class GerenciadorDados
    {
        private const string ArquivoJson = "dados.json";
        private Dados dados;

        public GerenciadorDados()
        {
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (File.Exists(ArquivoJson))
            {
                string json = File.ReadAllText(ArquivoJson);
                dados = JsonSerializer.Deserialize<Dados>(json) ?? new Dados();
            }
            else
            {
                dados = new Dados();
            }
        }

        public void SalvarDados()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(dados, options);
            File.WriteAllText(ArquivoJson, json);
        }

        public List<Produto> ObterProdutos() => dados.Produtos;
        public List<Venda> ObterVendas() => dados.Vendas;
        public List<Orcamento> ObterOrcamentos() => dados.Orcamentos;

        public void AdicionarProduto(Produto produto)
        {
            produto.IdProduto = dados.Produtos.Any() ? dados.Produtos.Max(p => p.IdProduto) + 1 : 1;
            dados.Produtos.Add(produto);
            SalvarDados();
        }

        public void AtualizarProduto(Produto produto)
        {
            var index = dados.Produtos.FindIndex(p => p.IdProduto == produto.IdProduto);
            if (index != -1)
            {
                dados.Produtos[index] = produto;
                SalvarDados();
            }
        }

        public void DeletarProduto(int id)
        {
            dados.Produtos.RemoveAll(p => p.IdProduto == id);
            SalvarDados();
        }

        public void AdicionarVenda(Venda venda)
        {
            venda.IdVenda = dados.Vendas.Any() ? dados.Vendas.Max(v => v.IdVenda) + 1 : 1;
            dados.Vendas.Add(venda);

            // Atualizar estoque
            foreach (var item in venda.Itens)
            {
                var produto = dados.Produtos.FirstOrDefault(p => p.IdProduto == item.Produto.IdProduto);
                if (produto != null)
                {
                    produto.QuantidadeProduto -= item.Quantidade;
                }
            }

            SalvarDados();
        }

        public void AdicionarOrcamento(Orcamento orcamento)
        {
            orcamento.IdOrcamento = dados.Orcamentos.Any() ? dados.Orcamentos.Max(o => o.IdOrcamento) + 1 : 1;
            dados.Orcamentos.Add(orcamento);
            SalvarDados();
        }

        public Produto BuscarProduto(int id)
        {
            return dados.Produtos.FirstOrDefault(p => p.IdProduto == id);
        }
    }
}

