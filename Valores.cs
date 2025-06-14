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

        private static string _idusuario;

        public static string idusuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }

        private static string _cargo;

        public static string cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        private static string _idfuncionario;

        public static string idfuncionario
        {
            get { return _idfuncionario; }
            set { _idfuncionario = value; }
        }
    }
}
