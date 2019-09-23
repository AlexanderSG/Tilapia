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


namespace Tilapia
{

    public partial class FrmPresentacion : DevExpress.XtraEditors.XtraForm
    {
        Presentacion pres = new Presentacion();
        Boolean exis=false;
        
       
        public FrmPresentacion()
        {
            InitializeComponent();
            txtPresentacion.Focus();
            Cargar();
            
        }

        private void FrmPresentacion_Load(object sender, EventArgs e)
        {
            txtPresentacion.Focus();
           
        }

        public void Cargar()
        {
            gridPresentacion.DataSource = pres.mostrarDatosPresentacion();
        }

        //metodo que recorre los controles 
        public void limpiar(GroupBox gb)
        {
            foreach (var txt in gb.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
            }
        }
        // metodo que verifica que no haya otro dato identico
        public void verificar(string nom, int id)
        {
            exis = false;
            for (int i = 0; i < gridView1.RowCount; i++) 
            {
                int fila = gridView1.GetVisibleRowHandle(i);
                if (nom == Convert.ToString(gridView1.GetRowCellValue(fila, "Empaque").ToString()) && Convert.ToInt32(gridView1.GetRowCellValue(fila, "IdPresentacion")) != id)
                {
                    exis = true;
                    i = gridView1.RowCount;

                }
            }
        }

        public void guardar()
        {
            if (txtPresentacion.Text == "")
            {
                errorProvider1.SetError(txtPresentacion, "Obligatorio");
            }

            else
            {
                errorProvider1.Clear();
                verificar(txtPresentacion.Text, Convert.ToInt32(txtId.Text));

                if (exis == false)
                {
                    pres.idPresentacion = Convert.ToInt32(txtId.Text);
                    pres.nombrePresentacion = txtPresentacion.Text;
                    pres.insertarPresentacion(pres);
                    limpiar(groupBox1);
                    Cargar();
                    txtPresentacion.Focus();
                }

                else
                {
                    MessageBox.Show("Este nombre de Empaque ya existe", "TILAPIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            guardar();
            
          
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmPresentacion_Activated(object sender, EventArgs e)
        {

            this.txtPresentacion.Focus();
        }

        private void txtPresentacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                guardar();
            }
            else if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void FrmPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void FrmPresentacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
