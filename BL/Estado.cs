using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class Estado
    {
        public static ML.Result GetAll()
        {
           ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.EstadoGetAll().ToList();

                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in registros)
                        {
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = registro.IdEstado;
                            estado.Nombre = registro.Nombre;

                            result.Objects.Add(estado);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }

            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;  
        }
    }
}
