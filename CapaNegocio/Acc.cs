using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.IO;

namespace Capanegocio.Orm
{
    public class Acc
    {
        public int idAcc { get; set; }
        public int idventacc { get; set; }
        public int cod_mod { get; set; }
        public int idvend { get; set; }
        public int idvntatienda { get; set; }

        public string Descripcion { get; set; }
        public DateTime fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Sub { get; set; }
        public decimal Total { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal p_compra { get; set; }
        public decimal p_vnta { get; set; }
        public int ex { get; set; }

        public int Crear(Acc acc)
        {
            try
            {
                return Convert.ToInt32( Conexion.GDatos.TraerValorEscalar("InsertarAccesorios", acc.idAcc,acc.cod_mod,acc.Nombre, acc.Cantidad,acc.p_compra,acc.p_vnta
                   ));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable MostrarCmbAcc() 
        {
            return Conexion.GDatos.TraerDataTable("MostrarAcc");
        
        }
        public DataTable MostrarAccxid()
        {
            return Conexion.GDatos.TraerDataTable("MostrarAccxid",idAcc);

        }

        public int GuardarVntaAcc(Acc acc)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("GuardarVntaAcc", acc.idvend, acc.fecha, acc.Cliente,acc.Sub,acc.Total,1,acc.idvntatienda));

        }
        public void GuardarVntaAcc2(Acc acc)
        {

            Conexion.GDatos.Ejecutar("GuardarVntaAcc2", acc.idventacc, acc.idAcc,acc.Descripcion,acc.Cantidad,acc.p_vnta);

        }
        public void DisminuirExAcc(Acc acc)
        {

            Conexion.GDatos.Ejecutar("DisminuirExAcc", acc.ex, acc.idAcc);

        }
         
    }
}
