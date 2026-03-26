using MauiAppMinhasCompras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiAppMinhasCompras.Models.Produto;


namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public object Db { get; private set; }

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "Update Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);

        }

        public Task<int> Delete(int id)
        {

            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);

        }

        public Task<List<Produto>> GetAll()
        {

            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {

            string sql = "SELECT * FROM Produto Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

        public Task<List<Produto>> GetByCategoria(string categoria)
        {
            return _conn.Table<Produto>()
                        .Where(p => p.Categoria == categoria)
                        .ToListAsync();
        }
        // Método para obter o total gasto por categoria
        /*

        public async Task<Dictionary<string, double>> GetTotalPorCategoria()
        {
            var produtos = await _conn.Table<Produto>().ToListAsync();

            return produtos
                .Where(p => !string.IsNullOrEmpty(p.Categoria))
                .GroupBy(p => p.Categoria)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(p => p.Preco * p.Quantidade));*/



        public async Task<List<RelatorioCategoria>> GetTotalPorCategoria()
        {
            var lista = await _conn.Table<Produto>()
                .ToListAsync();

            var resultado = lista
                .GroupBy(p => p.Categoria)
                .Select(g => new RelatorioCategoria
                {
                    Categoria = g.Key,
                    TotalGasto = g.Sum(p => p.Preco * p.Quantidade)
                })
                .ToList();

            return resultado;
        }
    }
}