using CapaDatos;
using CapaNegocio;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmInventario : Form
    {
        Invntario inv = new Invntario();
        public FrmInventario()
        {
            InitializeComponent();
            LlenaGridVacia();
            DataTable mostrarProductoXExist = Conexion.GDatos.TraerDataTable("mostrarProductoXExist");
            if (mostrarProductoXExist.Rows.Count != 0)
            {
                gridControl1.DataSource = mostrarProductoXExist;
            }
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private static FrmInventario m_FormDefInstance;
        public static FrmInventario DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new FrmInventario();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            CapaNegocio.DatosTemporales.caracter = gridView1.GetFocusedRowCellValue("Codigo").ToString();
            Inventario frm = new Inventario();
            frm.ShowDialog();
           gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoXExist"); 
	        
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        public void LlenaGridVacia()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Empaque");
            dt.Columns.Add("Almacen");

            gridControl1.DataSource = dt;

        }
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // codigo que hace un coloreo a las filas de la grilla dependiendo de su existencia
            if (gridView1.SelectedRowsCount!=0)
            {
                GridView view = sender as GridView;

                int exist = Convert.ToInt32(view.GetRowCellDisplayText(e.RowHandle, view.Columns["Almacen"]));
                int verificar = Convert.ToInt32(Conexion.GDatos.TraerValorEscalar("VerificarExMin", view.GetRowCellDisplayText(e.RowHandle, view.Columns["Codigo"])));

                if (exist == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(80, Color.LightSalmon);

                }
                else if (exist <= verificar)
                {
                    e.Appearance.BackColor = Color.FromArgb(80, Color.Yellow);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(80, Color.LightGreen);

                }
            }
            
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {
           
            
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            LlenaGridVacia();
            gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoSINExist");
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            LlenaGridVacia();
            gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoXExist");
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            LlenaGridVacia();
            gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoConExist");
           
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

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
            // añademos columna al gridcontrol
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Empaque");
            dt.Columns.Add("Almacen");
            dt.Columns.Add("Expiracion");
            dt.Columns.Add("Dias");

            gridControl1.DataSource = dt;

            //traemos procedimiento almacenado 
            DataTable dt1 = Conexion.GDatos.TraerDataTable("mostrarProductoXVencimiento");

            //ciclo que añade fila y recorre el total de campos de la tabla del Proc Almac 
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                gridView1.AddNewRow();
                gridView1.MoveLast();
                gridView1.MoveLast();
                #region Agregar a Grid

                string index = ArrayToString(this.gridView1.GetSelectedRows());
                int fila = Convert.ToInt32(index);
                
                // otro ciclo que permite recorrer las filas de la grid 
                // es necesario para obtener los datos de la fecha como tambien si hay datos 
                //con el mismo codigo y mismo vencimiento pero diferente almacen
                for (int j = 0; j < gridView1.RowCount; j++)
                {
                    int yrow = gridView1.GetVisibleRowHandle(j);                   
                    
                    string f2 = gridView1.GetRowCellValue(yrow, "Expiracion").ToString();                                

                    if (string.IsNullOrEmpty(f2))
                    {
                       
                        gridView1.SetRowCellValue(fila, "Codigo", dt1.Rows[i][0].ToString());
                        gridView1.SetRowCellValue(fila, "Producto", dt1.Rows[i][1].ToString());
                        gridView1.SetRowCellValue(fila, "Empaque", dt1.Rows[i][2].ToString());
                        gridView1.SetRowCellValue(fila, "Almacen", dt1.Rows[i][3].ToString());
                        DateTime f3 = Convert.ToDateTime(dt1.Rows[i][4]);
                        DateTime fSistema = DateTime.Now;
                        string fecha3 = f3.ToShortDateString();
                        gridView1.SetRowCellValue(fila, "Expiracion", fecha3);

                        TimeSpan tSpan = f3 - fSistema;
                        int dias = tSpan.Days;
                        gridView1.SetRowCellValue(fila, "Dias", dias.ToString());



                    }
                    else
                    {
                        DateTime f = Convert.ToDateTime(dt1.Rows[i][4]);
                        DateTime venc = Convert.ToDateTime(gridView1.GetRowCellValue(yrow, "Expiracion").ToString());
                        // si la fecha de la BD es la misma que ya agregada del mismo prod se le suma el almacen 
                        
                            if (venc.ToShortDateString() == f.ToShortDateString())
                            {
                            BorrarFilasSeleccionadas(gridView1);
                            int a= Convert.ToInt32(gridView1.GetRowCellValue(yrow, "Almacen").ToString());
                            int b = Convert.ToInt32(dt1.Rows[i][3]);
                            int suma = a + b;
                            gridView1.SetRowCellValue(yrow, "Almacen", suma.ToString());
                            

                            }
                            else
                            {
                           
                            #region empleocod
                            gridView1.SetRowCellValue(fila, "Codigo", dt1.Rows[i][0].ToString());
                                gridView1.SetRowCellValue(fila, "Producto", dt1.Rows[i][1].ToString());
                                gridView1.SetRowCellValue(fila, "Empaque", dt1.Rows[i][2].ToString());
                                gridView1.SetRowCellValue(fila, "Almacen", dt1.Rows[i][3].ToString());
                                DateTime f3 = Convert.ToDateTime(dt1.Rows[i][4]);
                            DateTime fSistema = DateTime.Now;
                            gridView1.SetRowCellValue(fila, "Expiracion", f3.ToShortDateString());

                            TimeSpan tSpan = f3 - fSistema;
                            int dias = tSpan.Days;
                            gridView1.SetRowCellValue(fila, "Dias", dias.ToString());
                            #endregion
                        }
                        break;
                    }
           
                }
                                
                              #endregion

            }




            

        }

        private void navBarItem1_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn)
            {
                textEdit1.Text = "";
            }
            else
            {
                textEdit1.Text = "";
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
          
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textEdit1.Text) && ( toggleSwitch1.IsOn ))
            {
                gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoXFiltro", textEdit1.Text);
            }
            else if (!string.IsNullOrEmpty(textEdit1.Text) && ( toggleSwitch1.IsOn==false ))
            {
                gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoXFiltroCodigo", textEdit1.Text);
            }
        }
    }
}
