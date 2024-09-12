using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoGaleria
{
    internal class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, NotNull]
        public string NombreUsuario { get; set; }//Nombre de usuario

        [Unique, NotNull]
        public string Email { get; set; }//Email de usuario

        [Unique]
        public string Contraseña { get; set; }//Contraseño de usuario
    }
}
