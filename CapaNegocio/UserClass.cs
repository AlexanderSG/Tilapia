//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Conexion;
//using System.Data;

//namespace CapaNegocio
//{
//    public class UserClass
//    {

//        public int codigo { get; set; }
//        public string Usuario { get; set; }
//        public string Password { get; set; }
//        public string BaseDatos { get; set; }
//        public string Permisos { get; set; }

//        //Agrega el Usuario al inicio de Sesion de SQL
//        public bool CrearUsuarioSQl(UserClass user)
//        {
//            try
//            {
//                Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_addlogin", user.Usuario, user.Password, user.BaseDatos);
//                return true;
//            }
//            catch (Exception)
//            {

//                return false;
//            }

//        }
//        public DataTable Listar(string parametro)
//        { 
//            return Conexion.ConexionEntrar.BaseCo.TraerDataTable("uspListarUsuariosXNombre", parametro);
//        }

//        //Agrega el Usuario a la Base de Datos con la que se trabaja
//        public bool CreaUsuariodeBasedeDatos(UserClass user)
//        {
//            try
//            {
//                Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_adduser", user.Usuario, user.Usuario);
//                return true;
//            }
//            catch (Exception)
//            {

//                return false;
//            }


//        }
//        public bool AsignaRolesAUsuarios(UserClass user)
//        {
//            try
//            {
//                Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_addrolemember", "db_datareader", user.Usuario);//Para solo consultas
//                Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_addrolemember", "db_datawriter", user.Usuario);//Para Update, Insert y Delete
//                string Consulta = string.Format("Use "+ Conexion.ConexionEntrar.BaseCo.Bdatos+" grant execute to {0}", user.Usuario); //Asigna permisos a todos los procedimienos almcenados de la base de datos
//                Conexion.ConexionEntrar.BaseCo.EjecutarSql(Consulta);
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }


//        }


//        //Guarda Usuario en la tabla
//        public bool GuardaUsuario(UserClass user)
//        {
//            try
//            {
//                Conexion.ConexionEntrar.BaseCo.Ejecutar("uspGuardaUsuario", user.codigo, user.Usuario, user.Password, user.Permisos);
//                return true;
//            }
//            catch (Exception)
//            {

//                return false;
//            }

//        }

//        // Verifica Permisos en la para acceso al sistema

//        public bool VerificaPermiso(UserClass user)
//        {
//            bool Encuentra = false;
//            try
//            {
//                DataTable dt = new DataTable();

//                dt = Conexion.ConexionEntrar.BaseCo.TraerDataTable("uspVerificarPermisos", user.Usuario, user.Password); //Guarda lo quie mnda el procedimiento en un datatable

//                if (dt.Rows.Count > 0)//Si la fila es 1 entonces encontro al usuario
//                {
//                    Encuentra = true;
//                    GlobalClass.TipoPermiso = dt.Rows[0][2].ToString(); //Guarda el tipo permiso del usuario
//                }

//                return Encuentra;//Retorna True si encontro el formulario
//            }
//            catch (Exception)
//            {

//                return Encuentra;//Si no encuentra el usuario manda false
//            }

//        }
//        public void DrpLogin(UserClass usua)
//        {
//            Conexion.ConexionEntrar.BaseCo.Ejecutar("DropLoginDatabase", usua.Usuario);
//        }
//        public void SuperUsuario(UserClass usua, string rol)
//        {
//            Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_addsrvrolemember", usua.Usuario, rol);
//        }
//        public void modificarUsuario(UserClass user, string OldUser)
//        {
//            Conexion.ConexionEntrar.BaseCo.Ejecutar("uspGuardaUsuario", user.codigo, user.Usuario, user.Password, user.Permisos);
//            Conexion.ConexionEntrar.BaseCo.EjecutarSql("ALTER LOGIN " + OldUser + " with NAME =" + user.Usuario);
//            Conexion.ConexionEntrar.BaseCo.EjecutarSql("ALTER USER " + OldUser + " WITH NAME =" + user.Usuario + ";	");

//        }
//        public void modificarUsuarioPass(UserClass user, string oldPass)
//        {
//            Conexion.ConexionEntrar.BaseCo.Ejecutar("sp_password", oldPass, user.Password, user.Usuario);
//        }
//        public DataTable TraerPass(int Cod)
//        {
//            return Conexion.ConexionEntrar.BaseCo.TraerDataTable("uspTraerPass",Cod);
//        }

//    }
//}
