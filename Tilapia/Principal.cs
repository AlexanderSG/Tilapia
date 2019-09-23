using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using CapaDatos;
using CapaNegocio;

namespace Tilapia
{
    public partial class Principal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Principal()
        {
            InitializeComponent();
            Conexion.IniciarSesion(".", "Tilapia", "sa", "12345");
            mostrarProductosaVencerse();
            mostrarProductosVencidos();



        }


        public void mostrarProductosaVencerse()
        {
            DataTable dt = Conexion.GDatos.TraerDataTableSql("mostrarProductoaVencer");
            string PV = string.Format("Productos a vencerse: " + "\n\r");

            if (dt.Rows.Count!= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    PV += string.Format(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString() + " " + "con existencia" + " " + dt.Rows[i][3].ToString() + "\n\r");


                }
                MessageBox.Show("Los " + PV, "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           
        }
        public void mostrarProductosVencidos()
        {
            DataTable dt = Conexion.GDatos.TraerDataTableSql("mostrarProductoVencidos");
            string PV = string.Format("Productos Vencidos: " + "\n\r");
            string mensaje = "Desea Sacar estos Productos de la Base de datos" + "\n\r" + "Verifique la Bodega";

            if (dt.Rows.Count != 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    PV += string.Format(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString() + " " + "con existencia" + " " + dt.Rows[i][3].ToString() + "\n\r");


                }

                if (MessageBox.Show("Los " + PV + "\n\r" + mensaje, "Tilapia", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    Invntario inv = new Invntario();

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        inv.vencimiento = DateTime.Now.ToShortDateString();
                        inv.exist = Convert.ToInt32(dt.Rows[j][3].ToString());
                        inv.idSalida = Convert.ToInt32(inv.insertarSalida(inv));
                        inv.idBodga = Convert.ToInt32( dt.Rows[j][5].ToString() );

                       inv.actualizarSalida(inv);

                        
                    }
                }

                   
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmProduct").SingleOrDefault<Form>();


            if (existe != null)
            {

                existe.BringToFront();

            }
            else
            {
                FrmProduct.DefInstance.MdiParent = this;
                FrmProduct.DefInstance.Show();
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {

                      


        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmInventario.DefInstance.MdiParent = this;
            FrmInventario.DefInstance.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            FrmPedido.DefInstance.MdiParent = this;
            FrmPedido.DefInstance.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmCliente frm = new FrmCliente();
            frm.ShowDialog();
        }
    }
}