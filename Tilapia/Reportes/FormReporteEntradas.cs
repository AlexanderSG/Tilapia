using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Tilapia.Reportes
{
    public partial class FormReporteEntradas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FormReporteEntradas()
        {
            InitializeComponent();
        }
        private static FormReporteEntradas m_FormDefInstance;
        public static FormReporteEntradas DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new FormReporteEntradas();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string a = dateEdit1.Text;
            string b = dateEdit2.Text;
            Reportes.XtraReportEntradas Entradas = new XtraReportEntradas(a, b);
            Entradas.CreateDocument();
            documentViewer1.PrintingSystem = Entradas.PrintingSystem;
        }
    }
}