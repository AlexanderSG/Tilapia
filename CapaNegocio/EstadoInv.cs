using System;
using System.Collections.Generic;
using System.Linq;
using CapaDatos;
using CapaNegocio;
using System.Data;

namespace CapaNegocio.Orm
{
    public class EstadoInv
    {
        public string Codigoprod { get; set; }

        public void Crear2(EstadoInv plan)
        {

            Conexion.GDatos.Ejecutar("EstInv", plan.Codigoprod);

        }
        
    }
}
