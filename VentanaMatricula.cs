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

namespace Matricula2022
{
    public partial class VentanaMatricula : Form
    {
        public int total = 0;
        int posicionSeleccionada = -1;
        ListarMatricula lista = new ListarMatricula();
        public VentanaMatricula()
        {
            InitializeComponent();
            cbx_Añoescolar.SelectedIndex = 0;
            cbxturno.SelectedIndex = 0;
            cbxPagosRequeridos.SelectedIndex = 0;
            txttotal.Enabled = false;
            string error = lista.cargarDesdeArchivo();
            if (error != "")
                MessageBox.Show(error);

            actualizarData();

        }
        //Matricula2022 lista = new Matricula2022();

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (VALIDAR() == true)
            {
                DialogResult rtsus = MessageBox.Show(this, "¿Estás seguro de agregar esta matrícula?",
                  "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button2);

                if (rtsus == DialogResult.No) return;
                if (rtsus == DialogResult.Yes)
                {
                    RegistrarMatricula matriculaNuevo = new RegistrarMatricula();
                    matriculaNuevo.cboañoescolar = cbx_Añoescolar.SelectedItem.ToString();
                    matriculaNuevo.cboturnoescolar = cbxturno.SelectedItem.ToString();
                    matriculaNuevo.codigo = int.Parse(txtCodigo.Text);
                    matriculaNuevo.nombre = txtNomAp.Text;
                    matriculaNuevo.cbopagoadicional = cbxPagosRequeridos.SelectedItem.ToString();
                    matriculaNuevo.pago = double.Parse(txttotal.Text);
                    lista.AgregarMatricula(matriculaNuevo);
                    actualizarData();
                    cbx_Añoescolar.SelectedIndex = 0;
                    cbxturno.SelectedIndex = 0;
                    txtCodigo.Clear();
                    txtNomAp.Clear();
                    cbxPagosRequeridos.SelectedIndex = 0;
                    txttotal.Clear();

                    string error2 = lista.guardarEnArchivo();
                    if (error2 != "")
                        MessageBox.Show(error2);
                    string error = lista.cargarDesdeArchivo();
                    if (error != "")
                        MessageBox.Show(error);

                    actualizarData();
                }
            }
        }

        private bool VALIDAR()
        {
            if (string.IsNullOrEmpty(cbx_Añoescolar.Text) || cbx_Añoescolar.SelectedIndex == 0)
            {
                error.SetError(cbx_Añoescolar, "Debe seleccionar un año escolar.");
                return false;
            }
            error.SetError(cbx_Añoescolar, "");

            if (string.IsNullOrEmpty(cbxturno.Text) || cbxturno.SelectedIndex == 0)
            {
                error.SetError(cbxturno, "Debe seleccionar un turno.");
                return false;
            }
            error.SetError(cbxturno, "");

            if (string.IsNullOrEmpty(cbxPagosRequeridos.Text) || cbxPagosRequeridos.SelectedIndex == 0)
            {
                error.SetError(cbxPagosRequeridos, "Debe seleccionar un pago requerido.");
                return false;
            }
            error.SetError(cbxPagosRequeridos, "");

            int x;
            if (!int.TryParse(txtCodigo.Text, out x))
            {
                error.SetError(txtCodigo, "Debe ingresar el código del matriculado.");
                txtCodigo.Focus();
                return false;
            }
            error.SetError(txtCodigo, "");
            if (x < 0)
            {
                error.SetError(txtCodigo, "Debe ingresar un código positivo");
                txtCodigo.Focus();
                return false;
            }

            if (txtNomAp.Text == "")
            {
                error.SetError(txtNomAp, "Debe ingresar un nombre.");
                txtNomAp.Focus();
                return false;
            }
            error.SetError(txtNomAp, "");

            return true;
        }
        private void dgvmatriculados_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvmatriculados.SelectedRows.Count > 0)
                {
                    DataGridViewRow filaSeleccionada = dgvmatriculados.SelectedRows[0];
                    posicionSeleccionada = filaSeleccionada.Index;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void cbx_Añoescolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Añoescolar.SelectedIndex == 0)
                txtmatricula.Text = 0.ToString();
            if (cbx_Añoescolar.SelectedIndex == 1)
                txtmatricula.Text = 110.ToString();
            if (cbx_Añoescolar.SelectedIndex == 2)
                txtmatricula.Text = 210.ToString();
            if (cbx_Añoescolar.SelectedIndex == 3)
                txtmatricula.Text = 310.ToString();
            if (cbx_Añoescolar.SelectedIndex == 4)
                txtmatricula.Text = 410.ToString();
            if (cbx_Añoescolar.SelectedIndex == 5)
                txtmatricula.Text = 510.ToString();
            if (cbx_Añoescolar.SelectedIndex == 6)
                txtmatricula.Text = 610.ToString();
        }

        private void cbxPagosAdicionales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPagosRequeridos.SelectedIndex == 0)
                txtrequerimientos.Text = 0.ToString();
            if (cbxPagosRequeridos.SelectedIndex == 1)
                txtrequerimientos.Text = 50.ToString();
            if (cbxPagosRequeridos.SelectedIndex == 2)
                txtrequerimientos.Text = 100.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //nada
        }
        public void actualizarData()
        {
            List<RegistrarMatricula> matriculados = lista.getLista();
            dgvmatriculados.Rows.Clear();
            foreach (RegistrarMatricula p in matriculados)
            {
                int indiceNuevaFila = dgvmatriculados.Rows.Add();
                DataGridViewRow filaNueva = dgvmatriculados.Rows[indiceNuevaFila];
                filaNueva.Cells[0].Value = p.fecha;
                filaNueva.Cells[1].Value = p.cboañoescolar;
                filaNueva.Cells[2].Value = p.cboturnoescolar;
                filaNueva.Cells[3].Value = p.codigo;
                filaNueva.Cells[4].Value = p.nombre;
                filaNueva.Cells[5].Value = p.cbopagoadicional;
                filaNueva.Cells[6].Value = p.pago;
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult rtsus = MessageBox.Show(this, "¿Estás Seguro de Eliminar esta información?",
                  "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button2);

            if (rtsus == DialogResult.No) return;
            if (rtsus == DialogResult.Yes)
            {
                dgvmatriculados.Rows.Clear();
                if (posicionSeleccionada > -1)
                {
                    lista.eliminarProducto(posicionSeleccionada);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un producto");
                }
                lista.listar(dgvmatriculados);
                ///actualizando
                string error2 = lista.guardarEnArchivo();
                if (error2 != "")
                    MessageBox.Show(error2);
                //cargando la lista actualizada
                string error = lista.cargarDesdeArchivo();
                if (error != "")
                    MessageBox.Show(error);

                actualizarData();
            }





        }

        private void btntotal_Click(object sender, EventArgs e)
        {
            total = int.Parse(txtmatricula.Text) + int.Parse(txtrequerimientos.Text);
            txttotal.Text = total.ToString();
            btnRegistrar.Enabled = true;
        }
    }
}
