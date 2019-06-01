using Cede_Dotnet_CSharp_Workshop2._01.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Cede_Dotnet_CSharp_Workshop2._03.Interfaces;

namespace Cede_Dotnet_CSharp_Workshop2
{
    public partial class Form1 : Form
    {
        public IPersonas personas { get; set; }

        List<ListaPersonas> personsDB { get; set; } = new List<ListaPersonas>();

        public Form1()
        {
            InitializeComponent();
            grdPersons.DataSource = personas;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                ListaPersonas listapersonas = new ListaPersonas();               

                //validación cédula
                ValidarCedula(txtCedula.Text);

                //Validacion Nombre
                ValidarNombre(txtNombre.Text.Length);
                
                //Envio datos a clase personas

                listapersonas.Cedula = Convert.ToInt32(txtCedula.Text);
                listapersonas.Nombre = txtNombre.Text;

                personsDB.Add(listapersonas);

                errorProvider1.SetError(txtCedula, "");
                errorProvider1.SetError(txtNombre, "");
                               
                txtCedula.Text = txtNombre.Text = "";
                grdPersons.DataSource = personsDB.ToList();
                MessageBox.Show("Registro guardado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ValidarCedula(string texto)
        {
            int cedula = 0;
            bool esNum = int.TryParse(texto, out cedula);
                if (!esNum)
                    errorProvider1.SetError(txtCedula, "Ingrese solo números");
                if (texto.Length == 0)
                    errorProvider1.SetError(txtCedula, "El campo no puede estar vacío");
        }

        public void ValidarNombre(int n)
        {
            if (n == 0)
                    errorProvider1.SetError(txtNombre, "El campo no puede estar vacío");
        }
    }
}