using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetById(int IdMunicipio)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();

                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in registros)
                        {


                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = registro.IdColonia;
                            colonia.Nombre = registro.Nombre;
                         
                           

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el municipio.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
