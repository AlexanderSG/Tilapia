using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.IO;
using System.Data;

namespace CapaNegocio
{
   public class Presentacion
    {
        public int idPresentacion { get; set; }
        public string nombrePresentacion { get; set; }


        public void insertarPresentacion(Presentacion pres)
        {
            try 
	        {
                Conexion.GDatos.Ejecutar("insertarPresentacion", pres.idPresentacion ,pres.nombrePresentacion);
	        }
	        catch (Exception)
	        {
		
	    	throw;
	        }
        }

        public DataTable mostrarDatosPresentacion()
        {
            return Conexion.GDatos.TraerDataTable("mostrarPresentacion");
        }

    }
}
