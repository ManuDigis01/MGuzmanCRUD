using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class rol
    {
        public static ML.Result GetAllAdo()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = context.CreateCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "RolGetAll";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Rol marca = new ML.Rol();

                            marca.IdRol = Convert.ToInt32(row[0]);
                            marca.Nombre = row[1].ToString();

                            result.Objects.Add(marca);
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay informacion";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.RolGetAll().ToList();

                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var registro in registros)
                        {
                            ML.Rol rol = new ML.Rol();
                            

                            rol.IdRol = registro.IdRol;
                            rol.Nombre = registro.Nombre;
                            

                            result.Objects.Add(rol);


                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay Registro";
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

