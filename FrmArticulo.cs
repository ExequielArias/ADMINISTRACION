using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADMINISTRACION.Models; 

namespace ADMINISTRACION
{
    public partial class FrmArticulo : Form
    {
        public FrmArticulo()
        {
            InitializeComponent();
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            CargarProovedores();
            LlenarGrilla(); 
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();

            Articulo p = new Articulo();
            p.Nombre = txtNombre.Text;
            p.Precio = Convert.ToDecimal(txtPrecio.Text);
            p.ProveedorId = Convert.ToInt32(cboProovedor.SelectedValue); 


            db.Add(p);
            db.SaveChanges();

            txtNombre.Text = "";
            txtPrecio.Text = "";
            cboProovedor.SelectedIndex = 0; 

            LlenarGrilla();
        }
        private void LlenarGrilla()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Articulos
                        orderby a.ArticuloId
                        select a;

            dgvArticulos.Rows.Clear();

            foreach (var p in query)
            {
                dgvArticulos.Rows.Add(p.ArticuloId.ToString(), p.Nombre, p.Precio, p.ProveedorId);
            }
        }
        private void CargarProovedores() 
        {
            using var db = new AdministracionContext();

            var query = from a in db.Proveedors
                        orderby a.ProveedorId
                        select a;
            DataTable tabla = new DataTable();
            tabla.Columns.Add("ProveedorId", typeof(int));
            tabla.Columns.Add("Nombre", typeof(string));

            foreach (var p in query)
            {
                DataRow dr = tabla.NewRow();
                dr["ProveedorId"] = p.ProveedorId;
                dr["Nombre"] = p.Nombre;
                tabla.Rows.Add(dr); 
            }
            cboProovedor.DisplayMember = "Nombre";
            cboProovedor.ValueMember = "ProveedorId";
            cboProovedor.DataSource = tabla;
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();

            int id = Convert.ToInt32(txtIdentificador.Text);

            var query = from a in db.Articulos
                        where a.ArticuloId == id
                        select a;

            if (query.Any())
            {
                var art = query.First();
                db.Remove(art);
                db.SaveChanges();
                LlenarGrilla();

            }
            else
            {
                MessageBox.Show("No existe ese articulo", "Error"); 
            }
        }
    }
}
