using ADMINISTRACION.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADMINISTRACION
{
    public partial class FrmProveedor : Form
    {
        public FrmProveedor()
        {
            InitializeComponent();
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();
            
            Proveedor p = new Proveedor();
            p.Nombre = TxtNombre.Text;

            db.Add(p);
            db.SaveChanges();

            TxtNombre.Text = "";

            LlenarGrilla();

        }

        private void LlenarGrilla()
        {
            using var db = new AdministracionContext();

            var query = from a in db.Proveedors
                        orderby a.ProveedorId
                        select a;

            DgvProveedores.Rows.Clear();

            foreach (var p in query)
            {
                DgvProveedores.Rows.Add(p.ProveedorId.ToString(), p.Nombre);
            }
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            LlenarGrilla();
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            using var db = new AdministracionContext();

            int id = Convert.ToInt32(TxtProveedorId.Text);

            var query = from a in db.Proveedors
                        where a.ProveedorId == id
                        select a;

            if(query.Any())
            {
                var prov = query.First();
                db.Remove(prov);
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
