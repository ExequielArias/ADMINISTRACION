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
    public partial class frmReporte : Form
    {
        public frmReporte()
        {
            InitializeComponent();
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {
            CargarProovedores(); 
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
            cboProveedorR.DisplayMember = "Nombre";
            cboProveedorR.ValueMember = "ProveedorId";
            cboProveedorR.DataSource = tabla;
        }
        private void LlenarGrilla()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Articulos
                        orderby a.ArticuloId
                        select a;

            dgvArt.Rows.Clear();

            foreach (var p in query)
            {
                dgvArt.Rows.Add(p.ArticuloId.ToString(), p.Nombre, p.Precio, p.ProveedorId);
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();
            int pro = Convert.ToInt32(cboProveedorR.SelectedValue);
            var query = from a in db.Articulos
                        orderby a.ArticuloId
                        where a.ProveedorId == pro
                        select a;
            DataTable tabla2 = new DataTable();
            tabla2.Columns.Add("ProveedorId", typeof(int));
            tabla2.Columns.Add("Nombre", typeof(string));

            if (query.Any())
            {
                var repor = query.First();
                db.Remove(repor);
                db.SaveChanges();
                LlenarGrilla();

            }
        }
    }
}
