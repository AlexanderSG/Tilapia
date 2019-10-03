using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class Pedidos
    {

        public int idPedido { get; set; }
        public int idDetalle { get; set; }
        public string Fecha_Solic { get; set; }
        public string Fecha_Entrega { get; set; }
        public int idCliente { get; set; }
        public string Notas { get; set; }

        public int idProd { get; set; }
        public int cantidad { get; set; }


        public int idSalida { get; set; }
        public bool Estado { get; set; }

        public int Generar()
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalarSql("select count(*) from TblPedidos"));

        }


        public int GuardarPedido(Pedidos ped)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("InsertarPedido", -1, ped.Fecha_Solic, ped.Fecha_Entrega, ped.idCliente,ped.Notas));
        }

      

        //public void ActualizarSalidaPedido(Pedidos ped)
        //{
        //    Conexion.GDatos.TraerValorEscalar("ActualizarSalida",ped.idSalida, ped.cantidad);
        //}
        public void ActualizarExistencia(Pedidos ped)
        {
            Conexion.GDatos.TraerValorEscalar("ActualizarExistencia", ped.idDetalle, ped.cantidad, ped.idSalida);
        }

        public DataTable TraerDetalleParaActualizarSalida(Pedidos ped)
        {
            return Conexion.GDatos.TraerDataTable("TraerDetalleParaActualizarSalida", ped.idProd);
        }
         public int TraerExistenciaBodegaXDetalle(Pedidos ped)
         {
             return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("TraerExistenciaDetalleBodega", ped.idDetalle));
         }

        public void GuardarDetallePedido(Pedidos ped)
        {
            Conexion.GDatos.TraerValorEscalar("InsertarDetallePedido", -1, ped.idPedido, ped.idProd, ped.cantidad);
        }
        public DataTable MostrarPedidos()
        {
           return Conexion.GDatos.TraerDataTable("mostrarPedido");
        }

    }
}
