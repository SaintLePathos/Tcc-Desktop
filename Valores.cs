using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaTardigrado
{
    class Valores
    {
        private static string _idpedido;

        public static string idped
        {
            get { return _idpedido; }
            set { _idpedido = value; }
        }
    }
}
