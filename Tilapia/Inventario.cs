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
using DevExpress.XtraEditors;

namespace Tilapia
{
    public partial class Inventario : DevExpress.XtraEditors.XtraForm
    {
        Producto prod = new Producto();
        Invntario inv = new Invntario();
        public Inventario()
        {
            InitializeComponent();
            LlenaGridVacia();
            txtCodBarra.Text = CapaNegocio.DatosTemporales.caracter;
            tooltip();
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            txtCodBarra.Focus();
            tooltip();
        }
        private void habilitar() {
            
            dateEdit1.Enabled = true;
            dateEdit2.Enabled = true;
            txtactualizar.Enabled = true;
        }

        private void txtCodBarra_TextChanged(object sender, EventArgs e)
        {
            
            

            
        }

        private void txtCodBarra_TextChanged_1(object sender, EventArgs e)
        {
            
        }
        public void tooltip()
        {
            ToolTip tt = new ToolTip();
            tt.AutoPopDelay = 2000;
            tt.InitialDelay = 100;
            tt.ReshowDelay = 500;
            tt.ShowAlways = true;
            tt.SetToolTip(this.txtactualizar, "Ingresa la Cantidad de Producto y da enter");
            tt.SetToolTip(this.dateEdit1, "Añade la fecha");
            tt.SetToolTip(this.dateEdit2, "Añade la fecha");
            tt.SetToolTip(this.gridControl1, "Seleccione elemento a remover");
        }
        private void txtCodBarra_TextChanged_2(object sender, EventArgs e)
        {
           

            prod.codBarra = txtCodBarra.Text;            

            if (!string.IsNullOrEmpty(txtCodBarra.Text)) 
            {
                
                if (txtCodBarra.Text.Length==9)
                {
                     
                    var b = prod.VerificarProd(prod);
                    if (b == 1)
                    {
                        habilitar();
                        DataTable DT = prod.TraerProd(prod);
                        txtid.Text = DT.Rows[0][0].ToString();
                        txtProducto.Text = DT.Rows[0][2].ToString();
                        prod.idProducto = Convert.ToInt32(txtid.Text);
                        try
                        {
                            var c = prod.VerificarID(prod);
                            if (c == 1)
                            {
                                txtalmacen.Text = Conexion.GDatos.TraerValorEscalar("TraerExistencia", txtid.Text).ToString();
                                    
                                //txtalmacen.Text = TE.Rows[0][0].ToString();
                            }
                            else
                            {
                                txtalmacen.Text = "0";
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        
                       
                        txtCodBarra.Enabled = false;
                        dateEdit1.Focus();

                    }
                    else
                    {
                        MessageBox.Show("No existe producto con este Codigo de Barra", "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodBarra.Clear();
                        
                    }
                }
                    
                }
                
            }

        private void txtactualizar_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtactualizar_KeyDown(object sender, KeyEventArgs e)
        {

            
        }

        private void txtactualizar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;



        }

        private void guardar()
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                int yrow = gridView1.GetVisibleRowHandle(i);
                inv.idProd = Convert.ToInt32(gridView1.GetRowCellValue(yrow, "id").ToString());
                prod.idProducto = inv.idProd;
                var c= prod.VerificarID(prod);

                if (c == 1)
                {
                    var d = Conexion.GDatos.TraerValorEscalarSql("select Cod_Bodega from TblBodega where idProducto=" + inv.idProd);
                    // captura la entrada del producto a la bodega 
                    inv.fecha_entrada = DateTime.Now.ToString("d");
                    inv.Cant_Entrada = Convert.ToInt32(gridView1.GetRowCellValue(yrow, "Entrada").ToString());;
                    inv.exist = Convert.ToInt32(gridView1.GetRowCellValue(yrow, "Entrada").ToString()); ;
                    //detalles bodega 

                    inv.produccion = gridView1.GetRowCellValue(yrow, "Produccion").ToString();
                    inv.vencimiento = gridView1.GetRowCellValue(yrow, "Vencimiento").ToString();
                    Conexion.GDatos.Ejecutar("InsertarDetalleBodega", d, inv.produccion, inv.vencimiento, inv.fecha_entrada, inv.Cant_Entrada,inv.exist);


                }
                else
                {

                    inv.idBodga = Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("InsertarProdXBodega", inv.idProd));

                    // captura la entrada del producto a la bodega 
                    inv.fecha_entrada = DateTime.Now.ToString("d");
                    inv.Cant_Entrada = Convert.ToInt32(gridView1.GetRowCellValue(yrow, "Entrada").ToString());
                    inv.exist = Convert.ToInt32(gridView1.GetRowCellValue(yrow, "Entrada").ToString());

                    //detalles bodega 

                    inv.produccion = gridView1.GetRowCellValue(yrow, "Produccion").ToString();
                    inv.vencimiento = gridView1.GetRowCellValue(yrow, "Vencimiento").ToString();
                    Conexion.GDatos.Ejecutar("InsertarDetalleBodega", inv.idBodga, inv.produccion, inv.vencimiento, inv.fecha_entrada, inv.Cant_Entrada,inv.exist);
                }




           
            }
               
                
        }


        

        //public void verificar2(string ID2)
        //{
        //    exis2 = false;
        //    for (int i = 0; i < gridView2.RowCount; i++)
        //    {
        //        int fila = gridView2.GetVisibleRowHandle(i);
        //        string a2 = gridView2.GetRowCellValue(fila, "Imei").ToString();
        //        if (a2 != "")
        //        {
        //            if (ID2 == Convert.ToString(gridView2.GetRowCellValue(fila, "Imei").ToString()))
        //            {
        //                exis2 = true;
        //                i = gridView2.RowCount;


        //            }
        //        }
        //        else
        //        {
        //            exis2 = false;
        //            return;
        //        }

        //    }


        //}

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


        public void LlenaGridVacia()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id");
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Produccion");
            dt.Columns.Add("Vencimiento");
            dt.Columns.Add("Entrada");

            gridControl1.DataSource = dt;

        }


        #endregion

        #region borrar grid
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
        #endregion

       
        public void limpiar(GroupBox gb)
        {
            foreach (var ctrl in gb.Controls)
            {
                if (ctrl is TextBox)
                {
                    ( (TextBox)ctrl ).Clear();
                    txtactualizar.Text = "0";
                }
                else if (ctrl is DateEdit)
                {
                    ( (DateEdit)ctrl ).ResetText();
                }
            }
            }

        private void txtactualizar_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Up)
            {
                txtactualizar.Text += 1.ToString();

            }
            else if ((int)e.KeyChar == (int)Keys.Enter)
            {

                if (string.IsNullOrEmpty(dateEdit1.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(dateEdit1, "Campo obligatorio");

                }
                else if (string.IsNullOrEmpty(dateEdit2.Text))
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(dateEdit2, "Campo obligatorio");
                }
                else if (txtactualizar.Text == "0")
                {
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtactualizar, "El valor no puede estar en cero");
                }
                else
                {
                    errorProvider1.Clear();

              #region Agregar a Grid
                gridView1.AddNewRow();
                gridView1.MoveLast();
                gridView1.MoveLast();

                string index = ArrayToString(this.gridView1.GetSelectedRows());
                int fila = Convert.ToInt32(index);
                int cont = gridView1.RowCount;

                gridView1.SetRowCellValue(fila, "id", Convert.ToString(txtid.Text));
                gridView1.SetRowCellValue(fila, "Codigo", Convert.ToString(txtCodBarra.Text));
                gridView1.SetRowCellValue(fila, "Producto", Convert.ToString(txtProducto.Text));
                gridView1.SetRowCellValue(fila, "Produccion", Convert.ToString(dateEdit1.Text));
                gridView1.SetRowCellValue(fila, "Vencimiento", Convert.ToString(dateEdit2.Text));
                gridView1.SetRowCellValue(fila, "Entrada", Convert.ToString(txtactualizar.Text));
                //Show the GridView's summary footer
                //gridView1.OptionsView.ShowFooter = true;
                //gridView1.Columns["Saldo"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                //gridView1.Columns["Saldo"].SummaryItem.FieldName = "Saldo";
                #endregion

                limpiar(groupBox1);
                txtCodBarra.Enabled = true;
                txtCodBarra.Focus();
                txtProducto.Enabled = false;
                dateEdit1.Enabled = false;
                dateEdit2.Enabled = false;
                txtactualizar.Enabled = false;
            }
            }
        }

       

        private void txtactualizar_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            BorrarFilasSeleccionadas(gridView1);
            simpleButton1.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(dateEdit1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(dateEdit1, "Campo obligatorio");

            }
            else if (string.IsNullOrEmpty(dateEdit2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(dateEdit2, "Campo obligatorio");
            }
            else if (txtactualizar.Text == "0")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtactualizar, "El valor no puede estar en cero");
            }
            else
            {
                
                #region Agregar a Grid
                gridView1.AddNewRow();
                gridView1.MoveLast();
                gridView1.MoveLast();

                string index = ArrayToString(this.gridView1.GetSelectedRows());
                int fila = Convert.ToInt32(index);
                int cont = gridView1.RowCount;

                gridView1.SetRowCellValue(fila, "id", Convert.ToString(txtid.Text));
                gridView1.SetRowCellValue(fila, "Codigo", Convert.ToString(txtCodBarra.Text));
                gridView1.SetRowCellValue(fila, "Producto", Convert.ToString(txtProducto.Text));
                gridView1.SetRowCellValue(fila, "Produccion", Convert.ToString(dateEdit1.Text));
                gridView1.SetRowCellValue(fila, "Vencimiento", Convert.ToString(dateEdit2.Text));
                gridView1.SetRowCellValue(fila, "Entrada", Convert.ToString(txtactualizar.Text));
                //Show the GridView's summary footer
                //gridView1.OptionsView.ShowFooter = true;
                //gridView1.Columns["Saldo"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                //gridView1.Columns["Saldo"].SummaryItem.FieldName = "Saldo";
                #endregion

                limpiar(groupBox1);
                txtCodBarra.Enabled = true;
                txtCodBarra.Focus();
                txtProducto.Enabled = false;
                dateEdit1.Enabled = false;
                dateEdit2.Enabled = false;
                txtactualizar.Enabled = false;
                simpleButton1.Enabled = true;
            }
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            guardar();
            gridControl1.DataSource = null;
            LlenaGridVacia();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            simpleButton3.Enabled = true;
        }

        private void txtactualizar_EditValueChanged_2(object sender, EventArgs e)
        {
            if (txtactualizar.Text.Contains("-"))
            {
                MessageBox.Show("No se aceptan valores negativo");
                txtactualizar.Text = "0";
            }
        }

        private void txtactualizar_KeyPress_2(object sender, KeyPressEventArgs e)
        {

        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dt1 = dateEdit1.DateTime;
            DateTime dt2 = dateEdit2.DateTime;
            if (dt2 <= dt1)
            {
                dateEdit2.Text = "";
            }
        }
    }


}

