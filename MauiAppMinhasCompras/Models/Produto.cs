using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;
namespace MauiAppMinhasCompras.Models;


public class Produto
{
    string _descricao;
    double _quantidade;
    private SQLiteAsyncConnection _db;

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Descricao
    {
        get => _descricao;
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
            if (value == 0)
            {
                throw new Exception("Insira a quantidade do produto.");
            }
            _quantidade = value;
        }
    }
    public double Preco { get; set; }
    public double Total { get => Quantidade * Preco; }
    public string Categoria { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }


    // Construtor público sem parâmetros necessário para SQLite
    public Produto()
    {
    }

    // Construtor para injetar a conexão SQLite
    public Produto(SQLiteAsyncConnection db)
    {
        _db = db;
    }

    public Task<List<Produto>> GetByCategoria(string categoria)
    {
        return _db.Table<Produto>().Where(p => p.Categoria == categoria).ToListAsync();
    }

    public async Task<Dictionary<string, double>> GetTotalPorCategoria()
    {
        var produtos = await _db.Table<Produto>().ToListAsync();
        return produtos.GroupBy(p => p.Categoria)
                       .ToDictionary(g => g.Key, g => g.Sum(p => p.Preco * p.Quantidade));
    }
    public class RelatorioCategoria
    {
        public string Categoria { get; set; }
        public DateTime DataCadastro { get; set; }
        public double Total { get; set; }
    }


             
}



