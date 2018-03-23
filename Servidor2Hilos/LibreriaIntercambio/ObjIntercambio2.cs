using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Comandos: órdenes que se van a mandar del cliente (control) al servidor (coche) para que 
// este actúe.

namespace LibreriaIntercambio
{

    [Serializable()]
    public class ObjIntercambio2
    {
        public String cadena2;
        public int numInt2;

        public ObjIntercambio2(String cad, int n)
        {
            cadena2 = cad;
            numInt2 = n;
        }

        public ObjIntercambio2()
        {
        }
    }
}
