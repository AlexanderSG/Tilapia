using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data;
namespace CapaNegocio.Orm
{
    public class Punto
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int iddist { get; set; }

        public void Crear(Punto punto)
        {

            Conexion.GDatos.Ejecutar("InsertarDist", punto.id, punto.Nombre, punto.Ubicacion);
            
        }

        public DataTable Mostrar()
        {
            return Conexion.GDatos.TraerDataTable("MostrarDist");
        }
        public DataTable Mostrar2()
        {
            return Conexion.GDatos.TraerDataTable("MostrarDist-tiendadiriamba");
        }
        public DataTable Mostrar3()
        {
            return Conexion.GDatos.TraerDataTable("MostrarDist-Bod");
        }
        public DataTable MostrarProdXPunto(Punto pt)
        {
            return Conexion.GDatos.TraerDataTable("MostrarProdXPunto",pt.iddist);
        }


    }
}
