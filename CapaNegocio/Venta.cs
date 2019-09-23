using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data.SqlClient;
using System.Data;

namespace CapaNegocio.Orm
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string cedula { get; set; }
        public string Imei2 { get; set; }
        public string fecha { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string CondicionPago { get; set; }
        public string CodigoProducto { get; set; }
        public string Observaciones { get; set; }
        public decimal SubTotal { get; set; }        
        public decimal Desc { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int idvend { get; set; }
        public int idmod { get; set; }
        public string Descripcion { get; set; }
        public decimal P_Venta { get; set; }
        public string Num { get; set; }
        public int idprodvend { get; set; }
        public int idacc { get; set; }
        public int iddist { get; set; }
        public int idpuntventa { get; set; }

        public int InsertarOrdenCliente(Venta v)
        {
        
            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("InsertarOrdenCliente", v.IdVenta, v.FechaVenta, v.Cliente,v.cedula,v.Observaciones, v.SubTotal,v.Iva, v.Desc , v.Total, v.idvend,1));
        }

        public void InsertarDetalles(Venta v)
        {
            Conexion.GDatos.Ejecutar("InsertarDetalle", v.IdVenta, v.idmod, v.Descripcion, v.CodigoProducto, v.P_Venta, v.Num, v.Imei2);

        }

        public int GuardarVntaVend(Venta v) 
        {
            return Convert.ToInt32( Conexion.GDatos.TraerValorEscalar("GuardarVntaVend", v.idvend, v.FechaVenta, v.SubTotal, v.Desc, v.Total));

        }



        public DataTable MostrarDetalleVntaProd(Venta vta)
        {
            return Conexion.GDatos.TraerDataTable("MostrarDetalleVntaProd", vta.IdVenta);
        }

       
        public DataTable MostrarDetalleVntaAcc(Venta vta)
        {
            return Conexion.GDatos.TraerDataTable("MostrarDetalleVntaAcc", vta.idacc);
        }

        //public int  MostrarIdVenta()  
        //{
        //    return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("MostrarIdVenta"));
        //}
        public DataTable MostrarEmplDiriamba()
        {
            return Conexion.GDatos.TraerDataTable("MostrarEmplDiriamba");
        }
        public int GuardarVntaPunto(Venta v)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("GuardarVntaPunto", v.iddist, v.fecha, v.SubTotal, v.Iva, v.Desc, v.Total));
        }
        public void GuardarVntaPunto2(Venta v)
        {
            Conexion.GDatos.Ejecutar("GuardarVntaPunto2", v.idpuntventa, v.CodigoProducto,v.Descripcion, v.P_Venta);

        }


      }
}
