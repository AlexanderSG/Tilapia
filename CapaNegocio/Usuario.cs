using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
   public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string contraseña { get; set; }
        public Boolean Administrador { get; set; }

        public void GuadarUsuario(Usuario cate)
        {
            Conexion.GDatos.Ejecutar("GuardarUsuario", cate.IdUsuario, cate.NombreUsuario, cate.Nombre, cate.Apellido, cate.contraseña, cate.Administrador);
        }

        public DataTable MostrarUsuario()
        {
            return Conexion.GDatos.TraerDataTable("MostrarUsuario");
        }
        public DataTable VerificarAdmin(Usuario us)
        {
            return Conexion.GDatos.TraerDataTable("VerificarAdmin", us.IdUsuario);
        }
        public DataTable VerificarContraseña(Usuario us)
        {
            return Conexion.GDatos.TraerDataTable("VerificarContraseña", us.IdUsuario);
        }
    }
}
