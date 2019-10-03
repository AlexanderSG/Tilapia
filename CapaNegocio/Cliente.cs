using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.IO;

namespace Capanegocio.Orm
{
    public class Cliente
    {
        public int id { get; set; }

        public string Nombre { get; set; }




        public void Crear(Cliente cli)
        {
            try
            {
                Conexion.GDatos.Ejecutar("InsertarCliente", cli.id,cli.Nombre);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataTable MostrarCliente()
        {

            return Conexion.GDatos.TraerDataTable("MostrarCliente");
        }

        public static void Cargar()
        {

             Conexion.GDatos.TraerDataTable("MostrarCliente");
        }


        // public DataTable Buscar()
        // {
        //return Conexion.GDatos.TraerDataTableSql("mostrarClienteXNombre");
        // }



        //public DataTable Listar(string Procedure, string parametro)
        //{
        //    return ConexionEntrar.BaseCo.TraerDataTable(Procedure, parametro);
        //}
        //public DataTable MostrarVendedor()
        //{
        //    return ConexionEntrar.BaseCo.TraerDataTable("cargo");
        //    }


        //public Empleados GetFotografia(int IdEmpleado)
        //{
        //    var empleados = new Empleados();
        //    DataTable dt = Conexion.ConexionEntrar.BaseCo.TraerDataTable("BuscarFotoEmpleado", IdEmpleado);

        //    if (dt.Rows.Count > 0)
        //    {
        //        empleados.Foto = (byte[])(dt.Rows[0][0]);
        //        return empleados;
        //    }
        //    return null;

        //}
    }


}

