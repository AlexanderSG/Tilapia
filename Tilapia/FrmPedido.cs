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
    public partial class FrmPedido : Form
    {
        Pedidos ped = new Pedidos();
        Capanegocio.Orm.Cliente client = new Capanegocio.Orm.Cliente();
        FrmProduct frm = new FrmProduct();
        Producto prod = new Producto();
        bool exis;
        bool verif;
        bool valid;
        bool nuevo;
        bool edit;
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
            simpleButton4.Enabled = true;
            cmbProducto.Enabled = true;
            btnGuardarNuevo.Enabled = true;
            btnlimpiar.Enabled = true;
            btnGuardarCerrar.Enabled = true;
            simpleButton1.Enabled = true;
            simpleButton3.Enabled = true;
        }
        public void cargarcomboCliente() {
            cmbCliente.Properties.DataSource = client.MostrarCliente();
            cmbCliente.Properties.DisplayMember = "idCliente";
            cmbCliente.Properties.ValueMember = "idCliente";
        }
        #region tooltip
        // ventana que aparece al poner el curso en un elemento especifico
        public void tooltip()
        {
            ToolTip tt = new ToolTip();
            tt.AutoPopDelay = 2000;
            tt.InitialDelay = 100;
            tt.ReshowDelay = 500;
            tt.ShowAlways = true;
            tt.SetToolTip(this.cmbCliente, "Selecciona el Cliente");
            tt.SetToolTip(this.cmbProducto, "Selecciona el Producto");
            tt.SetToolTip(this.BtnAgregar, "Añade el producto a la Grilla");
            tt.SetToolTip(this.BtnQuitar, "Quita el producto seleccionado");
        }
        #endregion


        public void cargarcomboProducto()        {

            cmbProducto.Properties.DataSource = Conexion.GDatos.TraerDataTable("mostrarProductoBodega");
            cmbProducto.Properties.DisplayMember = "Codigo";
            cmbProducto.Properties.ValueMember = "Codigo";
        }
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            nuevo = true;
            edit = false;
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

        public void limpiar()
        {
            cmbCliente.Text = "";
            dateEdit2.Text = "";
            gridControl1.DataSource = null;
            textBox3.Text = "INGRESAR NOTAS EN EL PEDIDO";
            txtNombreCliente.Text = "";
            gridControl1.DataSource = llenar();
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
            if (!string.IsNullOrEmpty(dateEdit2.Text))
            {
                if (Convert.ToDateTime(textBox2.Text) >= Convert.ToDateTime(dateEdit2.Text))
                {
                    dateEdit2.Text = "";
                }
            }
            
        }

        private void cmbProducto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbProducto.Text) && cmbProducto.Text != "[Vacío]")
            {
                DataTable dt = Conexion.GDatos.TraerDataTable("mostrarProductoxCodigo", cmbProducto.Text);
               txtidProd.Text = dt.Rows[0][0].ToString();
                txtProducto.Text = dt.Rows[0][2].ToString();
                txtCantidad.Enabled = true;
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
            dt.Columns.Add("CodBarra");
            dt.Columns.Add("NombreProd");
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

                if (gridView1.SelectedRowsCount != 0)
                {
                    exis = false;
                    do
                    {
                        for (int m = 0; m < gridView1.RowCount; m++)
                        {
                            #region Verificar si no hay mismo producto en la grilla

                            int row = gridView1.GetVisibleRowHandle(m);
                            string a = gridView1.GetRowCellValue(row, "id").ToString();

                            if (Convert.ToInt32(txtidProd.Text) == Convert.ToInt32(a))
                            {
                                if (MessageBox.Show("Este Producto esta en la Grilla" + "\n" + "¿Deseas agregar la cantidad?", "Nicalapia", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    int cantidad = Convert.ToInt32(gridView1.GetRowCellValue(row, "Cantidad")),
                                        nuevacantidad = Convert.ToInt32(txtCantidad.Text),
                                        suma = cantidad + nuevacantidad;


                                    if (suma> Convert.ToInt32(gridView1.GetRowCellValue(row, "Almacen")))
                                    {
                                        MessageBox.Show("La cantidad total es mayor de lo que hay en almacen", "Nicalapia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtCantidad.Text = "0";
                                        verif = true;
                                        break;
                                    }
                                    else
                                    {
                                        gridView1.SetRowCellValue(row, "Cantidad", suma);
                                        exis = true;
                                        cmbProducto.Text = "";
                                        txtProducto.Text = "";
                                        txtCantidad.Text = "0";
                                    }

                               }

                                else
                                {
                                    txtCantidad.Text = "0";
                                    break;
                                }

                            }
                            //else
                            //{
                            //    m++;
                            //}

                            #endregion
                        }
                        break;

                    } while (exis==true);

                    if ((exis==false) &&(verif== false))
                    {
                        DataTable dt = Conexion.GDatos.TraerDataTable("TraerProductoPedido", txtidProd.Text);

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
                        gridView1.SetRowCellValue(fila, "CodBarra", dt.Rows[0][1].ToString());
                        gridView1.SetRowCellValue(fila, "NombreProd", dt.Rows[0][2].ToString());
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

                else
                {
                    DataTable dt = Conexion.GDatos.TraerDataTable("TraerProductoPedido", txtidProd.Text);

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
                    gridView1.SetRowCellValue(fila, "CodBarra", dt.Rows[0][1].ToString());
                    gridView1.SetRowCellValue(fila, "NombreProd", dt.Rows[0][2].ToString());
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
          }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //verifica si el formulario producto esta abierto, si no es asi lo abre
            // sino lo trae hacia el frente 
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmProduct").SingleOrDefault<Form>();
            
            if (existe != null)
            {                
                existe.BringToFront();

            }
            else
            {
                              
                frm.MdiParent = MdiParent;
                frm.Show();
            }

            
        }

        public void verificarEx()
        {
            if (txtCantidad.Text != "0")
            {

                int a = Convert.ToInt32(txtCantidad.Text);
                int b = Convert.ToInt32(txtidProd.Text);
                var c = Conexion.GDatos.TraerDataTable("VerificarEx", b, a);
                if (c.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("La cantidad es mayor a la existencia total del producto" + " " + txtProducto.Text, "Nicalapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // string d = txtCantidad.Text.Substring(0, txtCantidad.Text.Length - 1);
                    txtCantidad.Text = "0";
                    txtCantidad.Focus();
                    return;
                }
                else
                {
                    BtnAgregar.Enabled = true;
                }


            }
        }
        private void cmbProducto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            verificarEx();
        }


        public void verificar(int ID)
        {
            exis = false;
           


        }


        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount!= 0)
            {
                BorrarFilasSeleccionadas(gridView1);
            }
    
        }

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            //cargarcomboCliente();
            //cargarcomboProducto();
        }

        private void FrmPedido_Activated(object sender, EventArgs e)
        {
            //cargarcomboCliente();
            //cargarcomboProducto();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            cargarcomboCliente();
            cargarcomboProducto();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            cargarcomboProducto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cargarcomboCliente();
        }


        public void validaciones() {

             if (string.IsNullOrEmpty(cmbCliente.Text))
                    {
                        errorProvider1.Clear();
                        errorProvider1.SetError(cmbCliente, "Obligatorio");
                        valid = false;
                        
                    }
                   
                    else if (string.IsNullOrEmpty(dateEdit2.Text))
                    {
                        errorProvider1.Clear();
                        errorProvider1.SetError(dateEdit2, "Obligatorio");
                        valid = false;
                    }
                    else if (gridView1.SelectedRowsCount==0)
                    {
                      errorProvider1.Clear();
                        errorProvider1.SetError(BtnAgregar, "Obligatorio");
                         valid = false;
                    }

            else
            {
                errorProvider1.Clear();
                valid = true;

            }

        }




        private void btnGuardarNuevo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            validaciones();
            if (valid==true)
            {
                ped.Fecha_Solic = textBox2.Text;
                ped.Fecha_Entrega =dateEdit2.Text;
                ped.idCliente = Convert.ToInt32(cmbCliente.Text);
                ped.Notas = textBox3.Text;
                int a= ped.GuardarPedido(ped),
                    h=0;
                   

                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ped.idPedido = a;
                    ped.idProd = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));
                    ped.cantidad= Convert.ToInt32(gridView1.GetRowCellValue(i, "Cantidad"));
                    ped.GuardarDetallePedido(ped);
                    DataTable dt = Conexion.GDatos.TraerDataTable("TraerProductoProximoVencimiento", ped.idProd);

                    for (int f = 0; f < dt.Rows.Count; f++)
                    {
                        int cantidadBodega = Convert.ToInt32(dt.Rows[f][4]);
                        DataTable Detalle = ped.TraerDetalleParaActualizarSalida(ped);
                        ped.idDetalle = Convert.ToInt32(Detalle.Rows[f][0]);

                        if (cantidadBodega < ped.cantidad)
                        {
                            h += cantidadBodega;
                            if (h< ped.cantidad)
                            {
                               
                                Conexion.GDatos.Ejecutar("ActualizarEstadoPedido", ped.idDetalle);
                                Conexion.GDatos.Ejecutar("insertarPedidosSalida", ped.idDetalle, ped.Fecha_Solic, cantidadBodega);

                               
                            }
                            else
                            {
                                return;
                            }

                        

                        }
                        else
                        {

                            //DataTable dt2 = Conexion.GDatos.TraerDataTable("TraerProductoPedido", ped.idProd);
                            //int cantTotal = Convert.ToInt32(dt2.Rows[0][4]),
                            //    sumaTotal = cantTotal - sumaCiclo ;
                                              

                            //ped.cantidad = sumaTotal;

                           
                            









                            // int cantidadAlmacen = Convert.ToInt32(dt.Rows[0][4]),
                            //  total = cantidadAlmacen - ped.cantidad;
                            //ped.idDetalle = ped.TraerDetalleParaActualizarSalida(ped);
                            // ped.idSalida = ped.SalidaBodegaXPedido(ped);

                            // ped.ActualizarSalidaPedido2(ped);
                        }
                        //int existencia = Convert.ToInt32(dt.Rows[0][4]);
                        //int nuevaExistencia = existencia - ped.cantidad;


                    }








                }

                limpiar();



            }
        }

        private void btnGuardarCerrar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            validaciones();
            if (valid == true)
            {
                ped.Fecha_Solic = textBox2.Text;
                ped.Fecha_Entrega = dateEdit2.Text;
                ped.idCliente = Convert.ToInt32(cmbCliente.Text);
                ped.Notas = textBox3.Text;
                int a = ped.GuardarPedido(ped);

                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ped.idPedido = a;
                    ped.idProd = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));
                    ped.cantidad = Convert.ToInt32(gridView1.GetRowCellValue(i, "Cantidad"));
                    ped.GuardarDetallePedido(ped);
                    //ped.idSalida = ped.SalidaBodegaXPedido(ped);
                    //ped.ActualizarSalidaPedido(ped);


                }

                this.Close();
            }
            }

        private void btnlimpiar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (nuevo)
            {
                limpiar();
            }
            else if (edit)
            {
                
            }
           
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            nuevo = false;
            edit = true;
            navBarItem3.Enabled = false;
            FormPed frmpedido = new FormPed();
            frmpedido.ShowDialog();
            if (frmpedido.DialogResult == DialogResult.OK)
            {
                int a = CapaNegocio.DatosTemporales.entero;

                habilitar();
                btnGuardarNuevo.Enabled = false;
                btnlimpiar.Enabled = false;
                DataTable dt = Conexion.GDatos.TraerDataTable("mostrarPedidoXID", a);
                cargarcomboCliente();
                cargarcomboProducto();
                string cliente = dt.Rows[0][1].ToString();
                cmbCliente.Text = cliente;
                txtNombreCliente.Text= dt.Rows[0][2].ToString();
                textBox2.Text = dt.Rows[0][3].ToString();
                dateEdit2.Text= dt.Rows[0][4].ToString();
                textBox3.Text = dt.Rows[0][5].ToString();
                gridControl1.DataSource = Conexion.GDatos.TraerDataTable("mostrarDetallePedidoXid", a);


                //DataTable dt = Conexion.GDatos.TraerDataTable("mostrarClienteXId", a);
                //txtNombreCliente.Text = dt.Rows[0][0].ToString();

            }
            else 
            {
                navBarItem3.Enabled = true;
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (nuevo)
            {
                if (!string.IsNullOrEmpty(cmbCliente.Text))
                {
                    DataTable dt = Conexion.GDatos.TraerDataTable("mostrarClienteXId", Convert.ToInt32(cmbCliente.Text));
                    txtNombreCliente.Text = dt.Rows[0][0].ToString();
                }
            }
            
        }

        private void navBarItem6_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }
    }
}
