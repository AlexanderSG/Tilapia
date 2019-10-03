using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tilapia
{
    public partial class FormPed : Form
    {

        CapaNegocio.Pedidos ped = new CapaNegocio.Pedidos();

        public FormPed()
        {
            InitializeComponent();
        }

        private void FormPed_Load(object sender, EventArgs e)
        {
            searchLookUpEdit1.Properties.DataSource = ped.MostrarPedidos();
            searchLookUpEdit1.Properties.ValueMember = "idPedido";
            searchLookUpEdit1.Properties.DisplayMember = "Nombre";

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void searchLookUpEdit1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchLookUpEdit1.Text) && ( searchLookUpEdit1.Text != "[vacío]" ))
            {
                ped.idPedido = Convert.ToInt32(searchLookUpEdit1.EditValue);
               //object member = searchLookUpEdit1.Properties.GetDisplayValueByKeyValue(searchLookUpEdit1.EditValue);
                gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarDetallePedidoXid", ped.idPedido);
            }
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CapaNegocio.DatosTemporales.entero = Convert.ToInt32(searchLookUpEdit1.EditValue);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
