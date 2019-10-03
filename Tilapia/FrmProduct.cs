using CapaNegocio;
using CapaDatos;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;
using System.Drawing.Printing;

namespace Tilapia
{
    public partial class FrmProduct : DevExpress.XtraEditors.XtraForm
    {
        Presentacion pres = new Presentacion();
        Producto prod = new Producto();
     
        bool a = false;
        bool save;
        bool edit;
        private long m_lImageFileLength = 0;
        private byte[] m_barrImg;
        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            inhabilitar();
            tooltip();
            gridControl1.DataSource = prod.MostrarProd();
            
           

        }
        private static FrmProduct m_FormDefInstance;
        public static FrmProduct DefInstance
        {
            get
            {
                if (m_FormDefInstance == null || m_FormDefInstance.IsDisposed)
                    m_FormDefInstance = new FrmProduct();
                return m_FormDefInstance;
            }
            set
            {
                m_FormDefInstance = value;
            }
        }

        
        public void inhabilitar()
        {
            txtCodBarra.Enabled = false;
            txtProducto.Enabled = false;
            txtCientifico.Enabled = false;
            txtmin.Enabled = false;
            cmbPresent.Enabled = false;
            simpleButton1.Enabled = false;
            simpleButton3.Enabled = false;
        }
        public void habilitar()
        {

            txtProducto.Enabled = true;
            txtCientifico.Enabled = true;
            txtmin.Enabled = true;
            cmbPresent.Enabled = true;
            simpleButton1.Enabled = true;
            simpleButton4.Enabled = true;
            cargarPresentacion();
            navBarItem2.Enabled = true;
            btnlimpiar.Enabled = true;
            navBarItem4.Enabled = true;
        }

        #region cargar imagen

        protected void LoadImage()
        {

            try
            {
                this.openFileDialog1.ShowDialog(this);
                string strFn = this.openFileDialog1.FileName;
                this.pictureBox1.Image = Image.FromFile(strFn);
                FileInfo fiImage = new FileInfo(strFn);
                this.m_lImageFileLength = fiImage.Length;
                FileStream fs = new FileStream(strFn, FileMode.Open, FileAccess.Read, FileShare.Read);
                m_barrImg = new byte[Convert.ToInt32(this.m_lImageFileLength)];
                int iBytesRead = fs.Read(m_barrImg, 0, Convert.ToInt32(this.m_lImageFileLength));
                fs.Close();
                if (pictureBox1.Image != null)
                {
                    pictureBox2.Visible = false;
                    simpleButton2.Enabled = true;
                }
                else
                {
                    pictureBox2.Visible = true;
                    simpleButton2.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region validar

        public void Validar(GroupControl gb)
        {

            foreach (var txt in gb.Controls)
            {
                var mask = txt as MaskedTextBox;

                if (mask != null)
                {
                    if (mask.Text.Contains(" "))
                    {
                        errorProvider1.Clear();
                        errorProvider1.SetError((MaskedTextBox)txt, "Obligatorio");
                        break;
                    }
                }
                else
                {

                    if (txtProducto.Text == "")
                    {
                        errorProvider1.Clear();
                        errorProvider1.SetError(txtProducto, "Obligatorio");
                        break;
                    }
                    else
                    {
                        if (cmbPresent.Text == "")
                        {
                            errorProvider1.Clear();
                            errorProvider1.SetError(cmbPresent, "Obligatorio");
                            break;
                        }

                        else
                        {
                            errorProvider1.Clear();
                            a = true;
                        }


                    }

                }

            }

        }
        #endregion
        public void cargarPresentacion()
        {
            cmbPresent.DataSource = pres.mostrarDatosPresentacion();
            cmbPresent.DisplayMember = "Empaque";
            cmbPresent.ValueMember = "idPresentacion";
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
            tt.SetToolTip(this.txtProducto, "Coloca la descripcion del Producto");
            tt.SetToolTip(this.txtCientifico, "Opcional");
            tt.SetToolTip(this.simpleButton1, "Opcional agregar Imagen Producto");
            tt.SetToolTip(this.gridControl1, "Click sobre el elemento a Editar");
            tt.SetToolTip(this.cmbPresent, "Seleccionar Empaque");
        }
        #endregion

        public void Limpiar()
        {
                      
            txtCientifico.Text = "";
            txtProducto.Text = "";
            cmbPresent.Text = "";
            pictureBox1.Image = null;
            pictureBox2.Visible = true;
            simpleButton2.Enabled = false;
            a = false;
            txtmin.Text = "0";
            txtProducto.Focus();
            panel1.Image = null;
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        public string generarCodigo()
        {
            string codigo="";
            int total;
            try
            {
                total = prod.Generar();
                if (total<10)
                {
                    codigo = "NT505000" + total.ToString();
                }
                else if (total<100)
                {
                    codigo = "NT50500" + total.ToString();
                }
                else if (total < 1000)
                {
                    codigo = "NT5050" + total.ToString();
                }
               
                
           
                return codigo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el codigo: ",ex.ToString());
                throw ;
            }
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            habilitar();
            btnlimpiar.Enabled = true;
            save = true;
            edit = false;
            txtCodBarra.Text = generarCodigo();
            txtmin.Text = "0";
            txtProducto.Focus();
            navBarItem1.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            m_barrImg = null;
            pictureBox1.Image = null;
            pictureBox2.Visible = true;
            simpleButton2.Enabled = false;
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Validar(groupControl1);
            if (a == true)
            {
                prod.codBarra = txtCodBarra.Text;
                var b = prod.VerificarProd(prod);

                if (save)
                {
                    if (b == 1)
                    {
                        MessageBox.Show("Ya existe producto con este Codigo de Barra", "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Limpiar();
                    }
                    else
                    {
                        guardar();
                        imprimir();
                        Limpiar();
                        gridControl1.DataSource = prod.MostrarProd();
                        txtCodBarra.Text = generarCodigo();
                        txtProducto.Focus();
                        save = true;
                        edit = false;
                        navBarItem5.Enabled = false;

                       
                    }
                  
                }
                else if (edit)
                {
                    guardar();
                    Limpiar();
                    gridControl1.DataSource = prod.MostrarProd();
                    txtid.Text = "-1";
                    txtCodBarra.Text = generarCodigo();
                    txtProducto.Focus();
                    save = true;
                    edit = false;
                    btnImprimir.Visible = false;
                }

                
                
            }
            
        }

        public void imprimir()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        public void guardar()
        {
            prod.idProducto = Convert.ToInt32(txtid.Text);
            prod.codBarra = txtCodBarra.Text;
            prod.nombreProducto = txtProducto.Text;
            prod.nombreCientifico = txtCientifico.Text;
            prod.Imagen = m_barrImg;
            prod.idPresentacion = Convert.ToInt32(cmbPresent.SelectedValue.ToString());
            prod.exmin = Convert.ToInt32(txtmin.Text);
            prod.CrearProducto(prod);

        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Validar(groupControl1);
            if (a == true)
            {
                prod.codBarra = txtCodBarra.Text;
                var b = prod.VerificarProd(prod);
                if (save)
                {
                    if (b == 1)
                    {
                        MessageBox.Show("Ya existe producto con este Codigo de Barra", "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Limpiar();
                    }
                    else
                    {

                        guardar();
                        imprimir();
                        this.Close();

                    }
                   
                }
                else if (edit)
                {
                    guardar();
                    this.Close();
                }

              
            }
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            Limpiar();
        }

    

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FrmPresentacion frm = new FrmPresentacion();
            frm.ShowDialog();
            cargarPresentacion();
        }

        private void txtCodBarra_TextChanged(object sender, EventArgs e)
        {
           
                prod.codBarra = txtCodBarra.Text;
                var b = prod.VerificarProd(prod);
                if (b == 1)
                {
                    MessageBox.Show("Ya existe producto con este Codigo de Barra", "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodBarra.Text = "";
                    txtCodBarra.Focus();
                }
           
            }

        private void btnbarra_Click(object sender, EventArgs e)
        {
           

    
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bit = new Bitmap(panel1.Width,panel1.Height);
            panel1.DrawToBitmap(bit, new Rectangle(0, 0, panel1.Width, panel1.Height));
            e.Graphics.DrawImage(bit, 0, 0);
            bit.Dispose();
        }

        private void txtCodBarra_TextChanged_1(object sender, EventArgs e)
        {
          
        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                errorProvider1.Clear();
                BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
                codigo.AlternateLabel = txtProducto.Text;
                codigo.IncludeLabel = true;
                panel1.Image = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodBarra.Text, Color.Black, Color.White, 240, 135);
               

                if (panel1.Image != null)
                {
                    var COD = txtCodBarra.Text;

                    Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    Font myfont = new Font("Calibri", 10, FontStyle.Bold);
                    Bitmap BM = new Bitmap(panel1.Image);
                    Graphics DIBUJO = Graphics.FromImage(BM);
                    DIBUJO.TranslateTransform(30, 20);
                    DIBUJO.RotateTransform(-90);
                    DIBUJO.DrawString(COD, myfont, mybrush, -70,12);
                    this.panel1.Image = BM;                    
                   
                    
                }


            }
        }

        private void verticalText(object sender, PaintEventArgs e)
        {
           
       
            
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
           
           

        }

        private void label1_Paint_1(object sender, PaintEventArgs e)
        {            
              
            
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

            navBarItem5.Enabled = true;
            }


           
        

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            habilitar();
            save = false;
            edit = true;
            txtid.Text = Convert.ToString(gridView1.GetFocusedRowCellValue("id"));
            txtCodBarra.Text = Convert.ToString(gridView1.GetFocusedRowCellValue("CodBarra"));
            txtProducto.Text = Convert.ToString(gridView1.GetFocusedRowCellValue("NombreProd"));
            txtCientifico.Text = Convert.ToString(gridView1.GetFocusedRowCellValue("NombreCientifico"));
            cargarPresentacion();
            cmbPresent.Text= Convert.ToString(gridView1.GetFocusedRowCellValue("Empaque"));
            txtmin.Text= Convert.ToString(gridView1.GetFocusedRowCellValue("ExMinima"));
            btnImprimir.Visible = true;
            btnlimpiar.Enabled = false;
            navBarItem1.Enabled = false;
            

        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("id"));
            string cod = gridView1.GetFocusedRowCellValue("CodBarra").ToString();
            string Prod = gridView1.GetFocusedRowCellValue("NombreProd").ToString();


            if (MessageBox.Show( "¿Deseas eliminar el Producto" +"  "+ cod +"  "+ Prod + "?", "Tilapia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Conexion.GDatos.Ejecutar("EliminarProducto", id);
                gridControl1.DataSource = prod.MostrarProd();

            }
            
            
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void FrmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmPedido").SingleOrDefault<Form>();

            if (existe != null)
            {
                // existe.BringToFront();
                FrmPedido frm = new FrmPedido();
                frm.DialogResult = DialogResult.OK;
            }
           
        }
    }
        }


