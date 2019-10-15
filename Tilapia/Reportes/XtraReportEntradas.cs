using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using CapaDatos;

namespace Tilapia.Reportes
{
    public partial class XtraReportEntradas : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportEntradas(string a, string b)
        {
            InitializeComponent();
            DataTable dt = Conexion.GDatos.TraerDataTable("RSPMostrarEntradas", a, b);
            this.DataSource = dt;
        }

               

    }
}
