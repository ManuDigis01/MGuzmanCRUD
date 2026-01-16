using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Errores
    {
        public int NumeroRegistro {  get; set; }
        public string Error {  get; set; }
        public List<Errores> ErroresEncontrados { get; set; }
    }
}
