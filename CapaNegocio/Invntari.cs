using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Invntario
    {
       
        public int idBodga { get; set; }
        public int idSalida { get; set; }
        public int idProd { get; set; }
        public int id_entrada { get; set; }
        public string fecha_entrada { get; set; }
        public int Cant_Entrada { get; set; }
       
        public int exist { get; set; }
        public string produccion { get; set; }
       
        public string vencimiento { get; set; }
        
       
         
        public int insertarSalida(Invntario inv)
        {

        return Convert.ToInt32( Conexion.GDatos.TraerValorEscalar("insertarVencidos", inv.vencimiento, inv.exist));
        }

        public void actualizarSalida(Invntario inv)
        {

            Conexion.GDatos.Ejecutar("ActualizarSalida", inv.idSalida, inv.idBodga);
        }

    }
}
