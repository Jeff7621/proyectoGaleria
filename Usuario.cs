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

        [NotNull]
        public string NombreUsuario { get; set; }//Nombre de usuario

        [Unique, NotNull]
        public string Email { get; set; }//Email de usuario

     
        public string Contraseña { get; set; }//Contraseño de usuario
    }
}
