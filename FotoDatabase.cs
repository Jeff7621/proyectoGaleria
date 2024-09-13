using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoGaleria
{
    class FotoDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public FotoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Foto>().Wait();
            _database.ExecuteAsync("ALTER TABLE Foto ADD COLUMN Fecha TEXT");
            _database.CreateTableAsync<Usuario>().Wait();
        }

        //Autenticacion
        public Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            return _database.InsertAsync(usuario);
        }

        public Task<Usuario> GetUsuarioAsync(string email, string contraseña)
        {
            return _database.Table<Usuario>()
                            .Where(u => u.Email == email && u.Contraseña == contraseña)
                            .FirstOrDefaultAsync();
        }

        public Task<Usuario> GetUsuarioByCorreoAsync(string email)
        {
            return _database.Table<Usuario>()
                            .Where(u => u.Email == email)
                            .FirstOrDefaultAsync();
        }

        //Tabla de Fotos
        public Task<List<Foto>> GetFotosAsync()
        {
            return _database.Table<Foto>().ToListAsync();
        }

        public Task<int> SaveFotoAsync(Foto foto)
        {
            return _database.InsertAsync(foto);
        }

        public Task<int> DeleteFotoAsync(Foto foto)
        {
            return _database.DeleteAsync(foto);
        }



    }
}
