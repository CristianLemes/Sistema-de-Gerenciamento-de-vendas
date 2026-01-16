using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Models
{
    internal class Dados
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Venda> Vendas { get; set; } = new List<Venda>();
        public List<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();
    }
}
