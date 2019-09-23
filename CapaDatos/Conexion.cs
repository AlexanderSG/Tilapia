using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace CapaDatos
{
    public class Conexion
    {
        private static string cadenaConexion;
        #region sql
        public static GDatos GDatos;
       // mando a llamar abrir cesion
        public static bool IniciarSesion(string nombreServidor, string baseDatos, string usuario, string password)
        {
            GDatos = new SqlServer(nombreServidor, baseDatos, usuario, password);
            return GDatos.Autenticar(usuario, password);
        } //fin inicializa sesion
       
        //mando a cerrar cesion
        public static void FinalizarSesion()
        {
            GDatos.CerrarConexion();
        } //fin FinalizaSesion

        #endregion
        

       
    }
}
