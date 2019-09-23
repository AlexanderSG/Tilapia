using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocio;
using CapaDatos;

namespace CapaNegocio.Orm 
{
    public class Estado
    {
        public string codprod { get; set; }
        public int Num { get; set; }
        public int idvend { get; set; }

        public void EstSim(Estado est)
        {

            Conexion.GDatos.Ejecutar("EstSim", est.codprod, est.Num);

        }
        public int EstImei(Estado est)
        {

            return Convert.ToInt32( Conexion.GDatos.TraerValorEscalar("VerificarImei", est.codprod));

        }
        public int TraerCodVenProd(Estado est)
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("TraerCodVenProd", est.idvend));

        }
    }
}
