using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoGaleria
{
    public class Foto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }//Se guardara el nombre de la imagen

        public string RutaImagen { get; set; }//Se guardara la ruta de la imagen

        public DateTime Fecha { get; set; }//Se guardadar la fecha de la imagen

        public int UsuarioId { get; set; }// Clave externa para el usuario
    }
}
