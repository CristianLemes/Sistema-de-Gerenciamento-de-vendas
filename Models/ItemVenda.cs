using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Models
{
    internal class ItemVenda
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal => Produto.ValorProduto * Quantidade;
    }
}
