using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;

namespace CapaNegocio.Orm
{
   public class Categoria
    {

        public int idcat { get; set; }
        public string categoria { get; set; }

        public void Crear(Categoria cat)
        {

            Conexion.GDatos.Ejecutar("InsertarCat", cat.idcat, cat.categoria);
        }
        public DataTable Mostrar()
        {
            return Conexion.GDatos.TraerDataTable("MostrarCat");
        }

    }
}
