using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capanegocio;
using CapaDatos;
using DevExpress.XtraScheduler.Native;

namespace Tilapia
{
    public partial class FrmCliente : Form
    {
        Capanegocio.Orm.Cliente client = new Capanegocio.Orm.Cliente();
        public delegate void Midelegado();

        bool bandera;

        public FrmCliente()
        {
            InitializeComponent();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            guardar();
            textBox2.Text = "-1";
            this.Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable d = Conexion.GDatos.TraerDataTable("mostrarClienteXNombre", textBox1.Text);

                foreach (DataRow row in d.Rows)
                {
                    string a= (Convert.ToString(row["Nombre"]));
                    
                    if (textBox1.Text == a && bandera==true)
                    {
                       
                            MessageBox.Show("Este Cliente ya esta registrado", "Tilapia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Text = "";
                            break;
                        
                       
                    }
                }

                                                           

            }
            catch (Exception)
            {

                throw;
            }


        }




        public static DataTable MostrarCliente()
        {

            return Conexion.GDatos.TraerDataTable("mostrarCliente");
        }




        private void FrmCliente_Load(object sender, EventArgs e)
        {
            //
            // Cargo los datos al TEXTO
            //
           
            textBox1.AutoCompleteCustomSource = load();
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            gridControl1.DataSource = MostrarCliente(); 
         
        }
        public static AutoCompleteStringCollection load()
        {
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            DataTable d = MostrarCliente();
            foreach (DataRow row in d.Rows)
            {
                stringCol.Add(Convert.ToString(row["Nombre"]));
            }

            return stringCol;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length == 0)

                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];

            else if (textBox1.Text.Length > 0)

                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bandera = true;
            textBox1.Enabled = true;
            barButtonItem2.Enabled = true;
            barButtonItem3.Enabled = true;
            barButtonItem1.Enabled = false;
            barButtonItem5.Enabled = false;
        }
        public void guardar()
        {
            client.id = Convert.ToInt32(textBox2.Text);
            client.Nombre = textBox1.Text;
            client.Crear(client);
            gridControl1.DataSource = client.MostrarCliente();
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bandera = true;
            guardar();
            textBox1.Text = "";
            gridControl1.DataSource = client.MostrarCliente();
           
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bandera = false;
            textBox1.Enabled = true;
            textBox2.Text=gridView1.GetFocusedRowCellValue("idCliente").ToString();
            textBox1.Text= gridView1.GetFocusedRowCellValue("Nombre").ToString();
            barButtonItem2.Enabled = false;
            barButtonItem3.Enabled = true;
            barButtonItem1.Enabled = false;
           

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            barButtonItem5.Enabled = true;
        }

        private void FrmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
    
}
