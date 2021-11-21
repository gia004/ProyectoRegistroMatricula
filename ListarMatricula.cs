using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matricula2022
{
    class ListarMatricula
    {
        List<RegistrarMatricula> matriculados;
        public void AgregarMatricula(RegistrarMatricula matriculaNuevo)
        {
            matriculados.Add(matriculaNuevo);
        }
        public List<RegistrarMatricula> getLista()
        {
            return matriculados;
        }
        public int cantidadMatricula()
        {
            return matriculados.Count;
        }
        public string guardarEnArchivo()
        {
            string error = "";
            try
            {

                StreamWriter sw = new StreamWriter("matri.txt");

                foreach (RegistrarMatricula ax in matriculados)
                {
                    sw.WriteLine(ax.DataParaArchivo);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return error;
        }
        public string cargarDesdeArchivo()
        {
            string error = "";
            try
            {

                matriculados = new List<RegistrarMatricula>();

                StreamReader sr = new StreamReader("matri.txt");
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    matriculados.Add(new RegistrarMatricula(linea));
                }
                sr.Close();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            return error;
        }
        public void listar(DataGridView dgvmatriculados)
        {
            dgvmatriculados.Rows.Clear();
            dgvmatriculados.Rows.Add();
            for (int i = 0; i < matriculados.Count; i++)
            {
                dgvmatriculados.Rows.Add(matriculados.ElementAt(i).fecha,
                                      matriculados.ElementAt(i).cboañoescolar,
                                      matriculados.ElementAt(i).cboturnoescolar,
                                      matriculados.ElementAt(i).codigo,
                                      matriculados.ElementAt(i).nombre,
                                      matriculados.ElementAt(i).cbopagoadicional,
                                      matriculados.ElementAt(i).pago);
            }
        }
        public void eliminarProducto(int posicion)
        {
            matriculados.RemoveAt(posicion);
        }
    }
}
