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
using Newtonsoft.Json;

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
            checkBox1.Enabled = false;
        }

        int selectedRowIndex;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex > 0)
            {
                //DataGridViewRow editRow = grdPersons.Rows[selectedRowIndex];
                ////validación cédula
                //ValidarCedula(txtCedula.Text);

                ////Validacion Nombre
                //ValidarNombre(txtNombre.Text.Length);

                //editRow.Cells["Cedula"].Value = txtCedula.Text.ToString();
                //editRow.Cells["Nombre"].Value = txtNombre.Text;

                //errorProvider1.SetError(txtCedula, "");
                //errorProvider1.SetError(txtNombre, "");

                //txtCedula.Text = txtNombre.Text = "";

                //MessageBox.Show("Registro editadoooo!");
                //btnSave.Text = "Guardar";
                ////btnEdit.Enabled = false;

            }

            else
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
        }

        private void grdPersons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // al seleccionar la celda, automaticamente saca los datos de la grid para editar y cambia el nombre del botón a editar

                selectedRowIndex = e.RowIndex;
                //btnSave.Text = "Editar";
                btnSave.Enabled = false;
                btnEdit.Enabled = true;
                checkBox1.Enabled = true;
                DataGridViewRow row = grdPersons.Rows[selectedRowIndex];
                txtCedula.Text = row.Cells["Cedula"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var dialogResult = MessageBox.Show("¿Desea eliminar el registro?", "Confirmación", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes ||
                    dialogResult.ToString().Equals("Yes"))
                {
                    grdPersons.Rows.RemoveAt(selectedRowIndex);
                    grdPersons.DataSource = personsDB.ToList();
                    MessageBox.Show("Registro Eliminado");
                    btnEdit.Enabled = false;
                    checkBox1.Enabled = false;
                    txtCedula.Text = txtNombre.Text = "";
                }
            }

            else
            {
                DataGridViewRow editRow = grdPersons.Rows[selectedRowIndex];
                //validación cédula
                ValidarCedula(txtCedula.Text);

                //Validacion Nombre
                ValidarNombre(txtNombre.Text.Length);
                editRow.Cells["Cedula"].Value = txtCedula.Text;
                editRow.Cells["Nombre"].Value = txtNombre.Text;

                errorProvider1.SetError(txtCedula, "");
                errorProvider1.SetError(txtNombre, "");

                txtCedula.Text = txtNombre.Text = "";

                MessageBox.Show("Registro editadoooo!");
                btnSave.Text = "Guardar";
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
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

        private void btnTranformJson_Click(object sender, EventArgs e)
        {
            string output = JsonConvert.SerializeObject(grdPersons.DataSource);
            txtJson.Text = output;
            System.IO.File.WriteAllText(@"D:\JsonTest.json", output);
        }
    }

}