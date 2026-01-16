using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ResultTXT
    {
        public int NumeroRegistro {  get; set; }
        public string Error { get; set; }   
       
        public List<object> ErroresEncontrados { get; set; }
        public List<object> Correctos { get; set; }
    }
}
