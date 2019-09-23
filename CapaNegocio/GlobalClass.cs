using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaNegocio
{
    public static class GlobalClass
    {
        private static string userglobal;

        public static string UsuarioGlogal
        {
            get { return userglobal; }
            set { userglobal = value; }
        }
        public static string TipoPermiso { get; set; }

    }
}
