using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Pedidos
    {
        public int Generar()
        {

            return Convert.ToInt32(Conexion.GDatos.TraerValorEscalarSql("select count(*) from TblPedidos"));

        }
    }
}
