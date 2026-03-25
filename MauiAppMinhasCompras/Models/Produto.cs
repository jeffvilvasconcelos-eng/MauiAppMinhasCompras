using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get => _descricao; 
            set
            {
                if (value == null)
                {
                    throw new Exception("A descrição do produto é obrigatória.");
                }
                _descricao = value;
            }
        
        }
        public double Quantidade
        {
            get => _quantidade;

            set
            {
                if (value > 0)
                {
                    throw new Exception("Insira a quantidade do produto.");
                }
                _quantidade = value;
            }
        }
public double Preco { get; set; }
        public double Total { get => Quantidade * Preco;  }
        public string Categoria { get; set; }

    }
}
