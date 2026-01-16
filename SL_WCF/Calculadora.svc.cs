using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Calculadora" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Calculadora.svc or Calculadora.svc.cs at the Solution Explorer and start debugging.
    public class Calculadora : ICalculadora
    {

         public float Suma(float numero1, float numero2)
        {
 
            
            return numero1 + numero2;
        }
        public float Restar(float numero1, float numero2)
        {
            return numero1 - numero2;
        }
        public float Multiplicar(float numero1, float numero2)
        {
            return numero1 * numero2;
        }
        public float Dividir(float numero1, float numero2)
        {
            return numero1 / numero2;
        }
        

        public void DoWork()
        {
        }
    }
}
