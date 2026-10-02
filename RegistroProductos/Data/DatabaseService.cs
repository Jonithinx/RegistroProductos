using SQLite;
using RegistroProductos.Models;

namespace RegistroProductos.Data
{
    public class DatabaseService
    {
        SQLiteAsyncConnection _db;

        async Task Init()
        {
            if (_db is not null) return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "RegistroProductos.db3");
            _db = new SQLiteAsyncConnection(databasePath);
            await _db.CreateTableAsync<Producto>();
        }

        public async Task<int> GuardarProductoAsync(Producto producto)
        {
            await Init();
            return await _db.InsertAsync(producto);
        }
    }
}