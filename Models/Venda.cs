using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Models
{
    internal class Venda
    {
        public int IdVenda { get; set; }
        public DateTime Data { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
        public decimal Total => Itens.Sum(i => i.ValorTotal);
    }
}
