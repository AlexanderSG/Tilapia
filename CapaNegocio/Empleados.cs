using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.IO;



namespace Capanegocio.Orm
{
    public class Empleados
    {
        public string Codigo { get; set; }
        public int Idempleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Salida { get; set; }
        public bool Estado { get; set; }
        public string POS { get; set; }
        public int iddist { get; set; }
        
       

       
        public void Crear(Empleados empleados)
        {
            try
            {
                Conexion.GDatos.Ejecutar("InsertarEmpleado",empleados.Idempleado, empleados.Nombre
                    ,empleados.Apellido,empleados.cedula, empleados.Direccion, empleados.Entrada, empleados.Estado, empleados.POS, empleados.iddist);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public void EntradaEmpl(Empleados empl)
        {
            Conexion.GDatos.Ejecutar("EntradaEmpl", empl.Idempleado, empl.Entrada);
        }
        public void Bajaempl(Empleados empl)
        {
            Conexion.GDatos.Ejecutar("BajaEmpl", empl.Idempleado, empl.Salida);
        }
            
        public DataTable MostrarEmpleados()
        {          

            return Conexion.GDatos.TraerDataTable("MostrarEmpleados");
            }
        public DataTable MostrarEmpleadosXid(Empleados emp)
        {

            return Conexion.GDatos.TraerDataTable("MostrarEmpleadosXid", emp.iddist);
        }
        public DataTable MostrarEmpleadosXid2(Empleados emp)
        {

            return Conexion.GDatos.TraerDataTable("MostrarEmpleadosXid2", emp.iddist);
        }
        public DataTable MostrarPunt()
        {

            return Conexion.GDatos.TraerDataTable("MostrarDist-Bod");
        }


      
        
        //public DataTable Buscar(string parametro)
        //{
        //    return ConexionEntrar.BaseCo.TraerDataTable("MostraEmpleadosXNombre", parametro);
        //}

        

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

  