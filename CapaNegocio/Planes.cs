using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data;

namespace CapaNegocio.Orm
{
    public class Planes
    {
        public string Nombre  { get; set; }
        public string SMS { get; set; }
        public int idcliente { get; set; }
        public string Nombre2 { get; set; }
        public DateTime Fcha { get; set; }
        public string contrato { get; set; }
        public int idplan { get; set; }
        public string Internet { get; set; }
        public int idmodem { get; set; }
        public int idmod { get; set; }
        public decimal Renta { get; set; }
        public string codigoprod { get; set; }
        public string solic { get; set; }
       
        
        public int ID { get; set; }

        public void Crear(Planes plan)
        {

            Conexion.GDatos.Ejecutar("InsertarPospago", plan.Nombre, plan.SMS,plan.Internet,plan.Renta);
            
        }
        public void Pospago(Planes plan)
        {

            Conexion.GDatos.Ejecutar("InsertarPospago2", plan.idcliente, plan.Fcha, plan.contrato, plan.idplan, plan.idmod, plan.codigoprod, plan.solic);

        }
        public void Multimedia(Planes plan)
        {

            Conexion.GDatos.Ejecutar("InsertarMultimedia2", plan.idcliente, plan.Fcha, plan.contrato, plan.idplan,plan.idmodem,plan.solic);

        }
        public void Estadomodem(Planes plan)
        {

            Conexion.GDatos.Ejecutar("EstModem", plan.ID);

        }
        public void EstadInv(Planes plan)
        {

            Conexion.GDatos.Ejecutar("EstInv", plan.idmod);

        }
        public int MostrarModemInternet2(Planes plan)
        {

           return Convert.ToInt32(  Conexion.GDatos.TraerValorEscalar("MostrarModemInternet2", plan.ID));

        }
        
        
        
        public DataTable MostrarCliente()
        {

            return Conexion.GDatos.TraerDataTable("MostrarCliente");
        }
        
        public void Crear2(Planes plan)
        {

            Conexion.GDatos.Ejecutar("InsertarMultimedia", plan.Nombre2,plan.Nombre, plan.Renta);

        }
        public DataTable MostrarPospago()
        {
            return Conexion.GDatos.TraerDataTable("MostrarPospago");
        }
        public DataTable MostrarPospago2()
        {
            return Conexion.GDatos.TraerDataTable("MostrarPospago2");
        }
        public DataTable MostrarMultimedia()
        {
            return Conexion.GDatos.TraerDataTable("MostrarMultimedia");
        }
        public DataTable MultimediaXTelef()
        {
            return Conexion.GDatos.TraerDataTable("MultimediaXTelef");
        }
        public DataTable MultimediaXTelev()
        {
            return Conexion.GDatos.TraerDataTable("MultimediaXTelev");
        }
        public DataTable MostrarMultimedia2()
        {
            return Conexion.GDatos.TraerDataTable("MostrarMultimedia2");
        }
        public DataTable MostrarModem()
        {
            return Conexion.GDatos.TraerDataTable("MostrarModemInternet");
        }
        public int FiltrarModem(Planes plan)
        {
            return Convert.ToInt32( Conexion.GDatos.TraerValorEscalar("FiltrarModem", plan.Nombre2));
        }
    }
}
