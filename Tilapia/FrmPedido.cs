using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;

namespace Tilapia
{
    public partial class FrmPedido : Form
    {
        Pedidos ped = new Pedidos();
        Capanegocio.Orm.Cliente client = new Capanegocio.Orm.Cliente();
        Producto prod = new Producto();
        public FrmPedido()
        {
            InitializeComponent();
        }

        private static FrmPedido m_FormDefInstance;
        public static FrmPedido DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new FrmPedido();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }
        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

             public string generarCodigo()
        {
                       int total;
            try
            {
                total = ped.Generar();
                total++;                
                
                return total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el codigo: ", ex.ToString());
                throw;
            }
        }

        public void habilitar()
        {
            simpleButton2.Enabled = true;
            cmbCliente.Enabled = true;
            dateEdit2.Enabled = true;
            textBox3.Enabled = true;
            simpleButton1.Enabled = true;
            simpleButton4.Enabled = true;
            cmbProducto.Enabled = true;

        }
        public void cargarcomboCliente() {
            cmbCliente.Properties.DataSource = client.MostrarCliente();
            cmbCliente.Properties.DisplayMember = "idCliente";
            cmbCliente.Properties.ValueMember = "idCliente";
        }
        public void cargarcomboProducto()
        {

            cmbProducto.Properties.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoBodega");
            cmbProducto.Properties.DisplayMember = "Codigo";
            cmbProducto.Properties.ValueMember = "Codigo";
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            habilitar();
            cargarcomboCliente();
            cargarcomboProducto();
            gridControl1.DataSource = llenar();
            txtCodBarra.Text = generarCodigo();
            DateTime fechaHoy = DateTime.Now;
            textBox2.Text= fechaHoy.ToString("d");
         
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FrmCliente frm = new FrmCliente();
            frm.ShowDialog();
            cargarcomboCliente();

            
            


       
    }

        private void cmbCliente_EditValueChanged(object sender, EventArgs e)
        {
           
            
        }

        private void cmbCliente_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCliente.Text) && cmbCliente.Text != "[Vacío]")
            {
                DataTable dt = Conexion.GDatos.TraerDataTable("mostrarClienteXId", Convert.ToInt32(cmbCliente.Text));
                txtNombreCliente.Text = dt.Rows[0][0].ToString();
            }
        }

        private void dateEdit2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text==dateEdit2.Text)
            {
                dateEdit2.Text = "";
            }
        }

        private void cmbProducto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbProducto.Text) && cmbProducto.Text != "[Vacío]")
            {
                DataTable dt = Conexion.GDatos.TraerDataTable("mostrarProductoxCodigo", cmbProducto.Text);
                txtidProd.Text = dt.Rows[0][0].ToString();
                txtProducto.Text = dt.Rows[0][2].ToString();
                BtnAgregar.Enabled = true;
            }
        }
        #region Arreglo para grid
        public static string ArrayToString(int[] arr)
        {
            string text = "";
            if (arr == null)
            {
                text = "Empty...";
            }
            else
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    int num = arr[i];
                    text = text + ( ( text == "" ) ? "" : ";" ) + num.ToString();
                }
                //text += ".";
            }
            return text;
        }
        #endregion
        #region PARA LLENAR GRID VACIA Y AGREGAR FILAS A LA GRID

        private DataTable llenar()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Codigo Barra");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Empaque");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Almacen");
            return dt;
        }

        #endregion
        private void BorrarFilasSeleccionadas(DevExpress.XtraGrid.Views.Grid.GridView view)
        {

            if (view == null || view.SelectedRowsCount == 0) return;

            DataRow[] rows = new DataRow[view.SelectedRowsCount];

            for (int i = 0; i < view.SelectedRowsCount; i++)

                rows[i] = view.GetDataRow(view.GetSelectedRows()[i]);

            view.BeginSort();

            try
            {
                foreach (DataRow row in rows)
                    row.Delete();
            }
            finally
            {
                view.EndSort();
            }

        }
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text) && txtCantidad.Text!="0")
            {
            DataTable dt = Conexion.GDatos.TraerDataTable("mostrarProductoxCodigo", cmbProducto.Text);
                    
            #region Agregar a Grid
            gridView1.AddNewRow();
            gridView1.MoveLast();
            gridView1.MoveLast();


            string index = ArrayToString(this.gridView1.GetSelectedRows());
            int fila = Convert.ToInt32(index);
            int cont = gridView1.RowCount;

            #endregion
            #region empleocod
            gridView1.SetRowCellValue(fila, "id", dt.Rows[0][0].ToString());
            gridView1.SetRowCellValue(fila, "Codigo Barra", dt.Rows[0][1].ToString());
            gridView1.SetRowCellValue(fila, "Producto", dt.Rows[0][2].ToString());
            gridView1.SetRowCellValue(fila, "Cantidad", txtCantidad.Text);
            gridView1.SetRowCellValue(fila, "Empaque", dt.Rows[0][3].ToString());
            gridView1.SetRowCellValue(fila, "Almacen", dt.Rows[0][4].ToString());

                #endregion
                cmbProducto.Text = "";
                txtCantidad.Text = "0";
                txtProducto.Text = "";
                BtnAgregar.Enabled = false;
                BtnQuitar.Enabled = true;
            }
          }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "0")
            {
                BtnAgregar.Enabled = true;
            }

            else
            {
                BtnAgregar.Enabled = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmProduct").SingleOrDefault<Form>();


            if (existe != null)
            {
                
                existe.BringToFront();

            }
            else
            {

                FrmProduct frm = new FrmProduct();
                frm.MdiParent = MdiParent;
                frm.Show();
            }





        }
    }
}
