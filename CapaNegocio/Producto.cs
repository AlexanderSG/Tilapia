using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public class Producto
    {
        public int idProducto{ get; set; }
        public string codBarra { get; set; }
        public string nombreProducto { get; set; }
        public string nombreCientifico { get; set; }
        public byte[] Imagen { get; set; }
        public int idPresentacion { get; set; }

        public int exmin { get; set; }
        public int id { get; set; }

        public void CrearProducto(Producto prod)
        {

             Conexion.GDatos.Ejecutar("InsertarProd", prod.idProducto, prod.codBarra, prod.nombreProducto, prod.nombreCientifico, prod.Imagen, prod.idPresentacion,prod.exmin);
                
        
        }
        public void InsertarProdInv(Producto prod)
        {

            Conexion.GDatos.Ejecutar("InsertarProdXBodega", -1, prod.codBarra, 0,prod.exmin, prod.id);

        }

        public int  Generar()
        {

           return   Convert.ToInt32(Conexion.GDatos.TraerValorEscalarSql("select count(*) from TblProducto"));

        }
        public int VerificarProd(Producto prod)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("VerificarProd", prod.codBarra));

        }
        public int VerificarID(Producto prod)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("VerificarID", prod.idProducto));

        }

        public DataTable TraerProd(Producto prod)
        {

            return Conexion.GDatos.TraerDataTable("TraerProd", prod.codBarra);

        }
        
        public DataTable MostrarProd()
        {
            return Conexion.GDatos.TraerDataTable("mostrarProducto");
        }






        // public DataTable MostrarProdAsignados(Producto prod)
        // {
        //     return Conexion.GDatos.TraerDataTable("mostrarProductosAsignado", prod.idvend);
        // }
        // public DataTable BodsXPunto(Producto prod)
        // {
        //     return Conexion.GDatos.TraerDataTable("BodsXPunto",prod.empxid);
        // }
        // public DataTable MostrarPunto()
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarDist");
        // }
        // public DataTable MostrarVend(Producto prodd)
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarEmpleadosXid",prodd.empxid);
        // }

        // public DataTable MostrarCat()
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarCat");
        // }
        // public DataTable MostrarProdCel()
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarCelXid");
        // }
        // public DataTable MostrarProdCelxMod(Producto prod)
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarProdCelxMod",prod.idmod);
        // }
        // public DataTable MostrarProdCel2(Producto prod)
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarCelXid2",prod.idmod);
        // }
        // public DataTable MostrarProdSim()
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarSimXid");
        // }
        //public DataTable MostrarUsbSim()
        // {
        //     return Conexion.GDatos.TraerDataTable("MostrarUsbXid");
        // }
        //public DataTable MostrarProdInt()
        //{
        //    return Conexion.GDatos.TraerDataTable("MostrarIntXid");
        //}
    }
}
