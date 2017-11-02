using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaIntercambio
{
    [Serializable()]
    public class ObjIntercambio1
    {
        public String cadena;
        public int numInt;

        public ObjIntercambio1(String cad, int n)
        {
            cadena = cad;
            numInt = n;
        }

        public ObjIntercambio1()
        {
        }
    }
}
