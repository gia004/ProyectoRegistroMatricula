using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula2022
{
    class RegistrarMatricula
    {
        private static char DIVISOR = ',';

        public string fecha = DateTime.Now.ToString("dd-MM-yyyy");
        public string cboañoescolar { get; set; }
        public string cboturnoescolar { get; set; }
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string cbopagoadicional { get; set; }
        public double pago { get; set; }

        public string DataParaArchivo
        {
            get
            {
                return fecha + DIVISOR + cboañoescolar + DIVISOR
                    + cboturnoescolar + DIVISOR
                    + codigo + DIVISOR + nombre + DIVISOR + cbopagoadicional + DIVISOR + pago;
            }
        }
        public RegistrarMatricula()
        {

        }
        public RegistrarMatricula(string linea)
        {
            string[] datos = linea.Split(DIVISOR);
            fecha = datos[0];
            cboañoescolar = datos[1];
            cboturnoescolar = datos[2];
            codigo = int.Parse(datos[3]);
            nombre = datos[4];
            cbopagoadicional = datos[5];
            pago = double.Parse(datos[6]);
        }
    }
}
