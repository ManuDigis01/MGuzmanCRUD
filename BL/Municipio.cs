using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetById(int IdEstado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.MunicipioGetByIdEstado(IdEstado).ToList();

                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in registros)
                        {


                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = registro.IdMunicipio;
                            municipio.Nombre = registro.Nombre;

                            result.Objects.Add(municipio);
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
