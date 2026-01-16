using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCN.Models
{
    internal class Orcamento
    {
        public int IdOrcamento { get; set; }
        public string NomeCliente { get; set; }
        public DateTime Data { get; set; }
        public List<ItemOrcamento> Itens { get; set; } = new List<ItemOrcamento>();
        public decimal Total => Itens.Sum(i => i.ValorTotal);
    }
}
