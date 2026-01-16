using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Core.Objects;
using DL_EF;
using Microsoft.Win32;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using System.Net;
using BL;
using System.Runtime.Remoting.Messaging;

namespace BL
{
    public class Usuario
    {

        public static bool AddPrimero(ML.Usuario Usuario)
        {
            using (SqlConnection context = new SqlConnection())
            {

                context.ConnectionString = DL.Connection.GetConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = context;
                cmd.CommandText = "UsuarioAdd";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "INSERT INTO Usuario (UserName,Nombre,ApellidoPaterno,ApellidoMaterno,Email,Password,FechaNacimiento,SEXO,Telefono,Celular,Estatus,CURP,IdRol) VALUES (@UserName,@Nombre,@ApellidoPaterno,@ApellidoMaterno,@Email,@Password,@FechaNacimiento,@SEXO,@Telefono,@Celular,@Estatus,@CURP,@IdRol)";

                cmd.Parameters.AddWithValue("UserName", Usuario.UserName);
                cmd.Parameters.AddWithValue("Nombre", Usuario.Nombre);
                cmd.Parameters.AddWithValue("ApellidoPaterno", Usuario.ApellidoPaterno);
                cmd.Parameters.AddWithValue("ApellidoMaterno", Usuario.ApellidoMaterno);
                cmd.Parameters.AddWithValue("Email", Usuario.Email);
                cmd.Parameters.AddWithValue("Password", Usuario.Password);
                cmd.Parameters.AddWithValue("FechaNacimiento", Usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("SEXO", Usuario.SEXO);
                cmd.Parameters.AddWithValue("Telefono", Usuario.Telefono);
                cmd.Parameters.AddWithValue("Celular", Usuario.Celular);
                cmd.Parameters.AddWithValue("Estatus", Usuario.Estatus);
                cmd.Parameters.AddWithValue("CURP", Usuario.CURP);
                //cmd.Parameters.AddWithValue("Imagen",null);
                cmd.Parameters.AddWithValue("IdRol", Usuario.Rol.IdRol);

                context.Open();

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool UpdatePrimero(ML.Usuario Usuario)
        {
            using (SqlConnection context = new SqlConnection())
            {
                context.ConnectionString = DL.Connection.GetConnection();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = context;
                cmd.CommandText = "UsuarioUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = @"UPDATE Usuario SET Nombre = @Nombre,ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno,Password = @Password,FechaNacimiento = @FechaNacimiento,SEXO = @SEXO,Telefono = @Telefono,Celular = @Celular,Estatus = @Estatus,CURP = @CURP,IdRol = @IdRol WHERE IdUsuario = @IdUsuario ";

                cmd.Parameters.AddWithValue("IdUsuario", Usuario.IdUsuario);

                cmd.Parameters.AddWithValue("UserName", Usuario.UserName);
                cmd.Parameters.AddWithValue("Nombre", Usuario.Nombre);
                cmd.Parameters.AddWithValue("ApellidoPaterno", Usuario.ApellidoPaterno);
                cmd.Parameters.AddWithValue("ApellidoMaterno", Usuario.ApellidoMaterno);
                cmd.Parameters.AddWithValue("Email", Usuario.Email);
                cmd.Parameters.AddWithValue("Password", Usuario.Password);
                cmd.Parameters.AddWithValue("FechaNacimiento", Usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("SEXO", Usuario.SEXO);
                cmd.Parameters.AddWithValue("Telefono", Usuario.Telefono);
                cmd.Parameters.AddWithValue("Celular", Usuario.Celular);
                cmd.Parameters.AddWithValue("Estatus", Usuario.Estatus);
                cmd.Parameters.AddWithValue("CURP", Usuario.CURP);
                //cmd.Parameters.AddWithValue("Imagen",null);
                cmd.Parameters.AddWithValue("IdRol", Usuario.Rol.IdRol);


                context.Open();

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool DeletePrimero(int id)
        {
            using (SqlConnection context = new SqlConnection())
            {
                context.ConnectionString = DL.Connection.GetConnection();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = context;
                cmd.CommandText = "UsuarioDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "Delete Usuario where IdUsuario = @IdUsuario";

                cmd.Parameters.AddWithValue("IdUsuario", id);

                context.Open();

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<object> GetAllPrimerComandos()
        {
            using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = context;
                /*cmd.CommandText = "UsuarioGetAll"*/
                ;
                cmd.CommandText = "UsuarioGetAllNombre";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "SELECT IdUsuario, UserName, Nombre, ApellidoPaterno, ApellidoMaterno, Email, Password, FechaNacimiento, Sexo, Telefono, Celular, Estatus, CURP,Imagen, IdRol FROM Usuario ";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    List<object> list = new List<object>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.IdUsuario = Convert.ToInt32(row[0]);
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(row[7].ToString());
                        usuario.SEXO = row[8].ToString();
                        usuario.Telefono = row[9].ToString();
                        usuario.Celular = row[10].ToString();
                        usuario.Estatus = bool.Parse(row[11].ToString());
                        usuario.CURP = row[12].ToString();
                        //usuario.Imagen = Encoding.UTF8.GetBytes(row[13].ToString());
                        usuario.Rol.IdRol = Convert.ToInt32(row[14]);
                        usuario.Rol.Nombre = row[15].ToString();

                        list.Add(usuario);

                    }
                    return list;
                }
                else
                {
                    return new List<object> { };
                }

            }

        }

        public static object GetByIdPrimerComandos(int id)
        {
            using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = context;
                //cmd.CommandText = "UsuarioGetById";
                cmd.CommandText = "UsuarioGetAllNombreId";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "SELECT IdUsuario,UserName,Nombre, ApellidoPaterno, ApellidoMaterno, Email, Password, FechaNacimiento, Sexo, Telefono, Celular, Estatus, CURP, Imagen,  IdRol FROM Usuario WHERE IdUsuario=@IdUsuario";

                cmd.Parameters.AddWithValue("IdUsuario", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();

                    usuario.IdUsuario = Convert.ToInt32(row[0]);
                    usuario.UserName = row[1].ToString();
                    usuario.Nombre = row[2].ToString();
                    usuario.ApellidoPaterno = row[3].ToString();
                    usuario.ApellidoMaterno = row[4].ToString();
                    usuario.Email = row[5].ToString();
                    usuario.Password = row[6].ToString();
                    usuario.FechaNacimiento = DateTime.Parse(row[7].ToString());
                    usuario.SEXO = row[8].ToString();
                    usuario.Telefono = row[9].ToString();
                    usuario.Celular = row[10].ToString();
                    usuario.Estatus = bool.Parse(row[11].ToString());
                    usuario.CURP = row[12].ToString();
                    //usuario.Imagen = Encoding.UTF8.GetBytes(row[13].ToString());
                    usuario.Rol.IdRol = Convert.ToInt32(row[14]);
                    usuario.Rol.Nombre = row[1].ToString();

                    //if (row[14].ToString() == "")
                    //{
                    //    usuario.IdRol = 0;
                    //}
                    //else
                    //{
                    //    usuario.IdRol = int.Parse(row[14].ToString());

                    //}


                    object resultado = usuario;
                    return resultado;
                }
                else
                {
                    return null;
                }
            }
        }





        // ........................Try catch.............................

        public static ML.Result GetAllAdoNet()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "UsuarioGetAll";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();

                            usuario.IdUsuario = Convert.ToInt32(row[0]);
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.FechaNacimiento = DateTime.Parse(row[7].ToString());
                            usuario.SEXO = row[8].ToString();
                            usuario.Telefono = row[9].ToString();
                            usuario.Celular = row[10].ToString();
                            usuario.Estatus = bool.Parse(row[11].ToString());
                            usuario.CURP = row[12].ToString();

                            if (row[13].ToString() != "")
                            {
                                usuario.Imagen = (byte[])row[13];

                            }
                            //usuario.Rol.IdRol = Convert.ToInt32(row[14]);
                            usuario.Rol.Nombre = row[14].ToString();

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay Registos";
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

        public static ML.Result GetByIdAdoNEt(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "UsuarioGetById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdUsuario", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();

                        usuario.IdUsuario = Convert.ToInt32(row[0]);
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(row[7].ToString());
                        usuario.SEXO = row[8].ToString();
                        usuario.Telefono = row[9].ToString();
                        usuario.Celular = row[10].ToString();
                        usuario.Estatus = bool.Parse(row[11].ToString());
                        usuario.CURP = row[12].ToString();

                        if (row[13].ToString() != "")
                        {
                            usuario.Imagen = (byte[])row[13];

                        }
                        usuario.Rol.IdRol = Convert.ToInt32(row[14]);
                        usuario.Rol.Nombre = row[15].ToString();


                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se econtro Registro";
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

        public static ML.Result DeleteAdoNet(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "UsuarioDelete";
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.AddWithValue("IdUsuario", id);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se econtro Registro";
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

        public static ML.Result UpdateAdoNet(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = context;
                    cmd.CommandText = "UsuarioUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdUsuario", Usuario.IdUsuario);

                    cmd.Parameters.AddWithValue("UserName", Usuario.UserName);
                    cmd.Parameters.AddWithValue("Nombre", Usuario.Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", Usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", Usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("Email", Usuario.Email);
                    cmd.Parameters.AddWithValue("Password", Usuario.Password);
                    cmd.Parameters.AddWithValue("FechaNacimiento", Usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("SEXO", Usuario.SEXO);
                    cmd.Parameters.AddWithValue("Telefono", Usuario.Telefono);
                    cmd.Parameters.AddWithValue("Celular", Usuario.Celular);
                    cmd.Parameters.AddWithValue("Estatus", Usuario.Estatus);
                    cmd.Parameters.AddWithValue("CURP", Usuario.CURP);
                    cmd.Parameters.AddWithValue("Imagen", Usuario.Imagen);
                    cmd.Parameters.AddWithValue("IdRol", Usuario.Rol.IdRol);


                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo";
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

        public static ML.Result AddAdoNet(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = context.CreateCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "UsuarioAdd";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("UserName", Usuario.UserName);
                    cmd.Parameters.AddWithValue("Nombre", Usuario.Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", Usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", Usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("Email", Usuario.Email);
                    cmd.Parameters.AddWithValue("Password", Usuario.Password);
                    cmd.Parameters.AddWithValue("FechaNacimiento", Usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("SEXO", Usuario.SEXO);
                    cmd.Parameters.AddWithValue("Telefono", Usuario.Telefono);
                    cmd.Parameters.AddWithValue("Celular", Usuario.Celular);
                    cmd.Parameters.AddWithValue("Estatus", Usuario.Estatus);
                    cmd.Parameters.AddWithValue("CURP", Usuario.CURP);
                    cmd.Parameters.AddWithValue("Imagen", Usuario.Imagen);
                    cmd.Parameters.AddWithValue("IdRol", Usuario.Rol.IdRol);
                    cmd.Parameters.AddWithValue("Calle", Usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("NumeroInterior", Usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("NumeroExterior", Usuario.Direccion.Colonia.IdColonia);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Agrego";
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


        // -------------------EntityFramwerk---------------



        public static ML.Result GetByIdEF(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registro = context.UsuarioGetById(id).SingleOrDefault();

                    if (registro != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = registro.IdUsuario;

                        usuario.UserName = registro.UserName;
                        usuario.Nombre = registro.UsuarioNombre;
                        usuario.ApellidoPaterno = registro.ApellidoPaterno; ;
                        usuario.ApellidoMaterno = registro.ApellidoMaterno;
                        usuario.Email = registro.Email;
                        usuario.Password = registro.Password;
                        if (registro.FechaNacimiento != null)
                        {
                            usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                        }
                        usuario.SEXO = registro.Sexo;
                        usuario.Telefono = registro.Telefono;
                        usuario.Celular = registro.Celular;
                        if (registro.Estatus != null)
                        {
                            usuario.Estatus = registro.Estatus.Value;
                        }
                        usuario.CURP = registro.CURP;
                        if (registro.Imagen != null)
                        {
                            usuario.Imagen = registro.Imagen;

                        }
                        if (registro.IdRol != null)
                        {
                            usuario.Rol.IdRol = registro.IdRol.Value;
                        }

                        usuario.Rol.Nombre = registro.RolNombre;
                        usuario.Direccion.Calle = registro.Calle;
                        usuario.Direccion.NumeroInterior = registro.NumeroInterior;
                        usuario.Direccion.NumeroExterior = registro.NumeroExterior;
                        if (registro.IdColonia != null)
                        {
                            usuario.Direccion.Colonia.IdColonia = registro.IdColonia.Value;
                        }

                        usuario.Direccion.Colonia.Nombre = registro.ColoniaNombre;
                        if (registro.IdMunicipio != null)
                        {

                            usuario.Direccion.Colonia.Municipio.IdMunicipio = registro.IdMunicipio.Value;
                        }
                        usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio.Nombre = registro.NumbreMunicipio;
                        if (registro.IdEstado != null)
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = registro.IdEstado.Value;
                        }

                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.EstadoNombre;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el usuario";
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

        public static ML.Result DeleteEF(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.UsuarioDelete(id);

                    if (registros > 0)
                    {

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se elimino";
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

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.UsuarioUpdate(
                usuario.IdUsuario,
                usuario.UserName,
                usuario.Nombre,
                usuario.ApellidoPaterno,
                usuario.ApellidoMaterno,
                usuario.Email,
                usuario.Password,
                usuario.FechaNacimiento,
                usuario.SEXO,
                usuario.Telefono,
                usuario.Celular,
                usuario.Estatus,
                usuario.CURP,
                usuario.Imagen,
                usuario.Rol.IdRol,
                        usuario.Direccion.Calle,
                        usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior,
                        usuario.Direccion.Colonia.IdColonia
);

                    if (registros > 0)
                    {

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo";
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

        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    // Parámetro OUTPUT
                    var idUsuarioParam = new ObjectParameter("IdUsuario", typeof(int));

                    var registros = context.UsuarioAdd(
                        usuario.UserName,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno,
                        usuario.Email,
                        usuario.Password,
                        usuario.FechaNacimiento,
                        usuario.SEXO,
                        usuario.Telefono,
                        usuario.Celular,
                        usuario.Estatus,
                        usuario.CURP,
                        usuario.Imagen,
                        usuario.Rol.IdRol,
                        usuario.Direccion.Calle,
                        usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior,
                        usuario.Direccion.Colonia.IdColonia

                    );

                    if (registros > 0)
                    {
                        result.Correct = true;


                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se agregó el usuario.";
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

        public static ML.Result UpdateEstatus(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var query = context.UsuarioUpdateEstatus(usuario.IdUsuario, usuario.Estatus);

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el estatus.";
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

        public static ML.Result GetAllView(ML.Usuario usuarioBusqueda)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registros = context.UsuarioGetAllViews.ToList();
                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var registro in registros)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.UserName = registro.UserName;
                            usuario.Nombre = registro.UsuarioNombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno; ;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario.Email = registro.Email;
                            usuario.Password = registro.Password;
                            if (registro.FechaNacimiento != null)
                            {
                                usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                            }
                            usuario.SEXO = registro.Sexo;
                            usuario.Telefono = registro.Telefono;
                            usuario.Celular = registro.Celular;
                            if (registro.Estatus != null)
                            {
                                usuario.Estatus = registro.Estatus.Value;
                            }
                            usuario.CURP = registro.CURP;
                            if (registro.Imagen != null)
                            {
                                usuario.Imagen = registro.Imagen;

                            }
                            if (registro.IdRol != null)
                            {
                                usuario.Rol.IdRol = registro.IdRol.Value;
                            }

                            usuario.Rol.Nombre = registro.RolNombre;
                            usuario.Direccion.Calle = registro.Calle;
                            usuario.Direccion.NumeroInterior = registro.NumeroInterior;
                            usuario.Direccion.NumeroExterior = registro.NumeroExterior;
                            if (registro.IdColonia != null)
                            {
                                usuario.Direccion.Colonia.IdColonia = registro.IdColonia.Value;
                            }

                            usuario.Direccion.Colonia.Nombre = registro.ColoniaNombre;
                            if (registro.IdMunicipio != null)
                            {

                                usuario.Direccion.Colonia.Municipio.IdMunicipio = registro.IdMunicipio.Value;
                            }
                            usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.Nombre = registro.NumbreMunicipio;
                            if (registro.IdEstado != null)
                            {
                                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = registro.IdEstado.Value;
                            }

                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.EstadoNombre;


                            result.Objects.Add(usuario);



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


        //--------------------------------linq----------------------------

        public static ML.Result GetAll(ML.Usuario usuarioBusqueda)

        {

            ML.Result result = new ML.Result();

            try

            {

                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())

                {
                   
                        if (usuarioBusqueda.Nombre == "" && usuarioBusqueda.ApellidoPaterno == "" && usuarioBusqueda.ApellidoMaterno == "" && usuarioBusqueda.Rol.IdRol == 0)

                        {

                            var registros = (from usuario in context.Usuarios

                                             join roles in context.Rols

                                                 on usuario.IdRol equals roles.IdRol

                                                 into rol

                                             from roles in rol.DefaultIfEmpty()

                                             join direccion in context.Direccions

                                                 on usuario.IdUsuario equals direccion.IdUsuario

                                                 into direcciones

                                             from direccion in direcciones.DefaultIfEmpty()

                                             join colonia in context.Colonias

                                                 on direccion.IdColonia equals colonia.IdColonia

                                                 into colonias

                                             from colonia in colonias.DefaultIfEmpty()

                                             join municipio in context.Municipios

                                           on colonia.IdMunicipio equals municipio.IdMunicipio

                                           into municipios

                                             from municipio in municipios.DefaultIfEmpty()

                                             join estado in context.Estadoes

                                             on municipio.IdEstado equals estado.IdEstado

                                             into estados

                                             from estado in estados.DefaultIfEmpty()

                                             select new

                                             {

                                                 IdUsuario = usuario.IdUsuario,

                                                 UserName = usuario.UserName,

                                                 UsuarioNombre = usuario.Nombre,

                                                 ApellidoPaterno = usuario.ApellidoPaterno,

                                                 ApellidoMaterno = usuario.ApellidoMaterno,

                                                 Email = usuario.Email,

                                                 Password = usuario.Password,

                                                 FechaNacimiento = usuario.FechaNacimiento,

                                                 Sexo = usuario.SEXO,

                                                 Telefono = usuario.Telefono,

                                                 Celular = usuario.Celular,

                                                 Estatus = usuario.Estatus,

                                                 CURP = usuario.CURP,

                                                 Imagen = usuario.Imagen,

                                                 RolNombre = roles.Nombre,

                                                 Calle = direccion.Calle,

                                                 NumeroExterior = direccion.NumeroExterior,

                                                 NumeroInterior = direccion.NumeroInterior,

                                                 NombreColonia = colonia.Nombre,

                                                 CodigoPostal = colonia.CodigoPostal,

                                                 NombreMunicipio = municipio.Nombre,

                                                 NombreEstado = estado.Nombre

                                             }).ToList();

                            if (registros.Count > 0)

                            {

                                result.Objects = new List<object>();

                                foreach (var registro in registros)

                                {

                                    ML.Usuario usuario = new ML.Usuario();

                                    usuario.Direccion = new ML.Direccion();

                                    usuario.Direccion.Colonia = new ML.Colonia();

                                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();

                                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                                    usuario.IdUsuario = registro.IdUsuario;

                                    usuario.UserName = registro.UserName;

                                    usuario.Nombre = registro.UsuarioNombre;

                                    usuario.ApellidoPaterno = registro.ApellidoPaterno;

                                    usuario.ApellidoMaterno = registro.ApellidoMaterno;

                                    usuario.Email = registro.Email;

                                    usuario.Password = registro.Password;

                                    if (registro.FechaNacimiento != null)
                                    {
                                        usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                                    }


                                    usuario.SEXO = registro.Sexo;

                                    usuario.Telefono = registro.Telefono;

                                    usuario.Celular = registro.Celular;

                                    if (registro.Estatus != null)
                                    {

                                    }
                                    usuario.Estatus = registro.Estatus.Value;

                                    usuario.CURP = registro.CURP;

                                    usuario.Imagen = registro.Imagen;

                                    usuario.Rol = new ML.Rol();

                                    usuario.Rol.Nombre = registro.RolNombre;

                                    usuario.Direccion.Calle = registro.Calle;

                                    usuario.Direccion.NumeroExterior = registro.NumeroExterior;

                                    usuario.Direccion.NumeroInterior = registro.NumeroInterior;

                                    usuario.Direccion.Colonia.Nombre = registro.NombreColonia;

                                    usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;

                                    usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;

                                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;

                                    result.Objects.Add(usuario);

                                }

                                result.Correct = true;

                            }

                            else

                            {

                                result.Correct = false;

                                result.ErrorMessage = "No hay registros en la tabla.";

                            }


                        }

                        else

                        {

                            if (usuarioBusqueda.Rol.IdRol > 0)

                            {

                                var registrosRol = (from usuario in context.Usuarios

                                                    join roles in context.Rols

                                                        on usuario.IdRol equals roles.IdRol

                                                        into rol

                                                    from roles in rol.DefaultIfEmpty()

                                                    join direccion in context.Direccions

                                                        on usuario.IdUsuario equals direccion.IdUsuario

                                                        into direcciones

                                                    from direccion in direcciones.DefaultIfEmpty()

                                                    join colonia in context.Colonias

                                                        on direccion.IdColonia equals colonia.IdColonia

                                                        into colonias

                                                    from colonia in colonias.DefaultIfEmpty()

                                                    join municipio in context.Municipios

                                                  on colonia.IdMunicipio equals municipio.IdMunicipio

                                                  into municipios

                                                    from municipio in municipios.DefaultIfEmpty()

                                                    join estado in context.Estadoes

                                                    on municipio.IdEstado equals estado.IdEstado

                                                    into estados

                                                    from estado in estados.DefaultIfEmpty()


                                                    where

                   (string.IsNullOrEmpty(usuarioBusqueda.Nombre)

                       || usuario.Nombre.Contains(usuarioBusqueda.Nombre))
               &&

                   (string.IsNullOrEmpty(usuarioBusqueda.ApellidoPaterno)

                       || usuario.ApellidoPaterno.Contains(usuarioBusqueda.ApellidoPaterno))
               &&

                   (string.IsNullOrEmpty(usuarioBusqueda.ApellidoMaterno)

                       || usuario.ApellidoMaterno.Contains(usuarioBusqueda.ApellidoMaterno))
               &&

                     (usuarioBusqueda.Rol.IdRol == 0

               || roles.IdRol == usuarioBusqueda.Rol.IdRol)

                                                    select new

                                                    {

                                                        IdUsuario = usuario.IdUsuario,

                                                        UserName = usuario.UserName,

                                                        UsuarioNombre = usuario.Nombre,

                                                        ApellidoPaterno = usuario.ApellidoPaterno,

                                                        ApellidoMaterno = usuario.ApellidoMaterno,

                                                        Email = usuario.Email,

                                                        Password = usuario.Password,

                                                        FechaNacimiento = usuario.FechaNacimiento,

                                                        Sexo = usuario.SEXO,

                                                        Telefono = usuario.Telefono,

                                                        Celular = usuario.Celular,

                                                        Estatus = usuario.Estatus,

                                                        CURP = usuario.CURP,

                                                        Imagen = usuario.Imagen,

                                                        RolNombre = roles.Nombre,

                                                        Calle = direccion.Calle,

                                                        NumeroExterior = direccion.NumeroExterior,

                                                        NumeroInterior = direccion.NumeroInterior,

                                                        NombreColonia = colonia.Nombre,

                                                        CodigoPostal = colonia.CodigoPostal,

                                                        NombreMunicipio = municipio.Nombre,

                                                        NombreEstado = estado.Nombre

                                                    }).ToList();

                                if (registrosRol.Count > 0)

                                {

                                    result.Objects = new List<object>();

                                    foreach (var registro in registrosRol)

                                    {

                                        ML.Usuario usuario = new ML.Usuario();

                                        usuario.Direccion = new ML.Direccion();

                                        usuario.Direccion.Colonia = new ML.Colonia();

                                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();

                                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                                        usuario.IdUsuario = registro.IdUsuario;

                                        usuario.UserName = registro.UserName;

                                        usuario.Nombre = registro.UsuarioNombre;

                                        usuario.ApellidoPaterno = registro.ApellidoPaterno;

                                        usuario.ApellidoMaterno = registro.ApellidoMaterno;

                                        usuario.Email = registro.Email;

                                        usuario.Password = registro.Password;

                                        if (registro.FechaNacimiento != null)
                                        {
                                            usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                                        }


                                        usuario.SEXO = registro.Sexo;

                                        usuario.Telefono = registro.Telefono;

                                        usuario.Celular = registro.Celular;

                                        if (registro.Estatus != null)
                                        {

                                        }
                                        usuario.Estatus = registro.Estatus.Value;

                                        usuario.CURP = registro.CURP;

                                        usuario.Imagen = registro.Imagen;

                                        usuario.Rol = new ML.Rol();

                                        usuario.Rol.Nombre = registro.RolNombre;

                                        usuario.Direccion.Calle = registro.Calle;

                                        usuario.Direccion.NumeroExterior = registro.NumeroExterior;

                                        usuario.Direccion.NumeroInterior = registro.NumeroInterior;

                                        usuario.Direccion.Colonia.Nombre = registro.NombreColonia;

                                        usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;

                                        usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;

                                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;

                                        result.Objects.Add(usuario);

                                    }

                                    result.Correct = true;

                                }

                                else

                                {

                                    result.Correct = false;

                                    result.ErrorMessage = "No hay registros en la tabla.";

                                }
                            }




                            else

                            {

                                var registrousuario = (from usuario in context.Usuarios

                                                       join roles in context.Rols

                                                           on usuario.IdRol equals roles.IdRol

                                                           into rol

                                                       from roles in rol.DefaultIfEmpty()

                                                       join direccion in context.Direccions

                                                           on usuario.IdUsuario equals direccion.IdUsuario

                                                           into direcciones

                                                       from direccion in direcciones.DefaultIfEmpty()

                                                       join colonia in context.Colonias

                                                           on direccion.IdColonia equals colonia.IdColonia

                                                           into colonias

                                                       from colonia in colonias.DefaultIfEmpty()

                                                       join municipio in context.Municipios

                                                     on colonia.IdMunicipio equals municipio.IdMunicipio

                                                     into municipios

                                                       from municipio in municipios.DefaultIfEmpty()

                                                       join estado in context.Estadoes

                                                       on municipio.IdEstado equals estado.IdEstado

                                                       into estados

                                                       from estado in estados.DefaultIfEmpty()

                                                       where

                          (string.IsNullOrEmpty(usuarioBusqueda.Nombre)

                          || usuario.Nombre.Contains(usuarioBusqueda.Nombre))
                      &&

                      (string.IsNullOrEmpty(usuarioBusqueda.ApellidoPaterno)

                          || usuario.ApellidoPaterno.Contains(usuarioBusqueda.ApellidoPaterno))
                      &&

                      (string.IsNullOrEmpty(usuarioBusqueda.ApellidoMaterno)

                          || usuario.ApellidoMaterno.Contains(usuarioBusqueda.ApellidoMaterno))

                                                       select new

                                                       {

                                                           IdUsuario = usuario.IdUsuario,

                                                           UserName = usuario.UserName,

                                                           UsuarioNombre = usuario.Nombre,

                                                           ApellidoPaterno = usuario.ApellidoPaterno,

                                                           ApellidoMaterno = usuario.ApellidoMaterno,

                                                           Email = usuario.Email,

                                                           Password = usuario.Password,

                                                           FechaNacimiento = usuario.FechaNacimiento,

                                                           Sexo = usuario.SEXO,

                                                           Telefono = usuario.Telefono,

                                                           Celular = usuario.Celular,

                                                           Estatus = usuario.Estatus,

                                                           CURP = usuario.CURP,

                                                           Imagen = usuario.Imagen,

                                                           RolNombre = roles.Nombre,

                                                           Calle = direccion.Calle,

                                                           NumeroExterior = direccion.NumeroExterior,

                                                           NumeroInterior = direccion.NumeroInterior,

                                                           NombreColonia = colonia.Nombre,

                                                           CodigoPostal = colonia.CodigoPostal,

                                                           NombreMunicipio = municipio.Nombre,

                                                           NombreEstado = estado.Nombre

                                                       }).ToList();

                                if (registrousuario.Count > 0)

                                {

                                    result.Objects = new List<object>();

                                    foreach (var registro in registrousuario)

                                    {

                                        ML.Usuario usuario = new ML.Usuario();

                                        usuario.Direccion = new ML.Direccion();

                                        usuario.Direccion.Colonia = new ML.Colonia();

                                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();

                                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                                        usuario.IdUsuario = registro.IdUsuario;

                                        usuario.UserName = registro.UserName;

                                        usuario.Nombre = registro.UsuarioNombre;

                                        usuario.ApellidoPaterno = registro.ApellidoPaterno;

                                        usuario.ApellidoMaterno = registro.ApellidoMaterno;

                                        usuario.Email = registro.Email;

                                        usuario.Password = registro.Password;

                                        if (registro.FechaNacimiento != null)
                                        {
                                            usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                                        }


                                        usuario.SEXO = registro.Sexo;

                                        usuario.Telefono = registro.Telefono;

                                        usuario.Celular = registro.Celular;

                                        if (registro.Estatus != null)
                                        {

                                        }
                                        usuario.Estatus = registro.Estatus.Value;

                                        usuario.CURP = registro.CURP;

                                        usuario.Imagen = registro.Imagen;

                                        usuario.Rol = new ML.Rol();

                                        usuario.Rol.Nombre = registro.RolNombre;

                                        usuario.Direccion.Calle = registro.Calle;

                                        usuario.Direccion.NumeroExterior = registro.NumeroExterior;

                                        usuario.Direccion.NumeroInterior = registro.NumeroInterior;

                                        usuario.Direccion.Colonia.Nombre = registro.NombreColonia;

                                        usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;

                                        usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;

                                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;

                                        result.Objects.Add(usuario);

                                    }

                                    result.Correct = true;

                                }

                                else

                                {

                                    result.Correct = false;

                                    result.ErrorMessage = "No hay registros en la tabla.";

                                }
                            }

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


        public static ML.Result Delete(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var buscarDireccion = (from Direccion in context.Direccions
                                           where Direccion.IdUsuario == id
                                           select Direccion).SingleOrDefault();

                    if (buscarDireccion != null)
                    {
                        context.Direccions.Remove(buscarDireccion);
                        context.SaveChanges();
                    }


                    var buscar = (from Usuario in context.Usuarios
                                  where Usuario.IdUsuario == id
                                  select Usuario).SingleOrDefault();

                    if (buscar != null)
                    {
                        context.Usuarios.Remove(buscar);
                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se elimino";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el id";
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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    if (usuario.Direccion == null || string.IsNullOrEmpty(usuario.Direccion.Calle) || string.IsNullOrEmpty(usuario.Direccion.NumeroExterior) || usuario.Direccion.Colonia == null || usuario.Direccion.Colonia.IdColonia == 0) { result.Correct = false; result.ErrorMessage = "Debe completar Dirección correctamente para poder registrar el usuario."; return result; }

                    DL_EF.Usuario usuarioBD = new DL_EF.Usuario();
                    usuarioBD.UserName = usuario.UserName;
                    usuarioBD.Nombre = usuario.Nombre;
                    usuarioBD.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioBD.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioBD.Email = usuario.Email;
                    usuarioBD.Password = usuario.Password;
                    usuarioBD.FechaNacimiento = usuario.FechaNacimiento;
                    usuarioBD.SEXO = usuario.SEXO;
                    usuarioBD.Telefono = usuario.Telefono;
                    usuarioBD.Celular = usuario.Celular;
                    usuarioBD.Estatus = usuario.Estatus;
                    usuarioBD.CURP = usuario.CURP;
                    usuarioBD.Imagen = usuario.Imagen;
                    usuarioBD.IdRol = usuario.Rol.IdRol;


                    context.Usuarios.Add(usuarioBD);
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {

                        DL_EF.Direccion direccionBD = new DL_EF.Direccion();


                        direccionBD.Calle = usuario.Direccion.Calle;
                        direccionBD.NumeroInterior = usuario.Direccion.NumeroInterior;
                        direccionBD.NumeroExterior = usuario.Direccion.NumeroExterior;
                        direccionBD.IdUsuario = usuarioBD.IdUsuario;
                        direccionBD.IdColonia = usuario.Direccion.Colonia.IdColonia;


                        context.Direccions.Add(direccionBD);
                        int filasAfectadasDic = context.SaveChanges();


                        if (filasAfectadasDic > 0)
                        {
                            result.Correct = true;

                        }


                    }
                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "No se agregó el usuario.";
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {

                    var registro = (from UsuarioUpdate in context.Usuarios
                                    where UsuarioUpdate.IdUsuario == usuario.IdUsuario
                                    select UsuarioUpdate).SingleOrDefault();

                    if (registro != null)
                    {

                        registro.UserName = usuario.UserName;
                        registro.Nombre = usuario.Nombre;
                        registro.ApellidoPaterno = usuario.ApellidoPaterno;
                        registro.ApellidoMaterno = usuario.ApellidoMaterno;
                        registro.Email = usuario.Email;
                        registro.Password = usuario.Password;
                        registro.FechaNacimiento = usuario.FechaNacimiento;
                        registro.SEXO = usuario.SEXO;
                        registro.Telefono = usuario.Telefono;
                        registro.Celular = usuario.Celular;
                        registro.Estatus = usuario.Estatus;
                        registro.CURP = usuario.CURP;
                        registro.Imagen = usuario.Imagen;
                        registro.IdRol = usuario.Rol.IdRol;


                        context.SaveChanges();


                        var registroDireccion = (from DireccionUpdate in context.Direccions
                                                 where DireccionUpdate.IdUsuario == usuario.IdUsuario
                                                 select DireccionUpdate).SingleOrDefault();

                        if (registroDireccion != null)
                        {

                            registroDireccion.Calle = usuario.Direccion.Calle;
                            registroDireccion.NumeroInterior = usuario.Direccion.NumeroInterior;
                            registroDireccion.NumeroExterior = usuario.Direccion.NumeroExterior;
                            registroDireccion.IdColonia = usuario.Direccion.Colonia.IdColonia;

                            context.SaveChanges();
                        }
                        else
                        {

                            DL_EF.Direccion direccionBD = new DL_EF.Direccion()
                            {
                                Calle = usuario.Direccion.Calle,
                                NumeroInterior = usuario.Direccion.NumeroInterior,
                                NumeroExterior = usuario.Direccion.NumeroExterior,
                                IdUsuario = usuario.IdUsuario,
                                IdColonia = usuario.Direccion.Colonia.IdColonia
                            };

                            context.Direccions.Add(direccionBD);
                            context.SaveChanges();
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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

        public static ML.Result GetById(int id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MGuzmanProgramacionNCapasEntities context = new DL_EF.MGuzmanProgramacionNCapasEntities())
                {
                    var registro = (from usuario in context.Usuarios
                                    join roles in context.Rols
                                    on usuario.IdRol equals roles.IdRol into rol
                                    from roles in rol.DefaultIfEmpty()

                                    join direccion in context.Direccions
                                    on usuario.IdUsuario equals direccion.IdUsuario into direcciones
                                    from direccion in direcciones.DefaultIfEmpty()

                                    join colonia in context.Colonias
                                    on direccion.IdColonia equals colonia.IdColonia
                                    into colonias
                                    from colonia in colonias.DefaultIfEmpty()

                                    join municipio in context.Municipios
                                    on colonia.IdMunicipio equals municipio.IdMunicipio
                                    into municipios
                                    from municipio in municipios.DefaultIfEmpty()

                                    join estado in context.Estadoes
                                    on municipio.IdEstado equals estado.IdEstado
                                    into estados
                                    from estado in estados.DefaultIfEmpty()

                                    where usuario.IdUsuario == id

                                    select new

                                    {

                                        IdUsuario = usuario.IdUsuario,
                                        UserName = usuario.UserName,
                                        UsuarioNombre = usuario.Nombre,
                                        ApellidoPaterno = usuario.ApellidoPaterno,
                                        ApellidoMaterno = usuario.ApellidoMaterno,
                                        Email = usuario.Email,
                                        Password = usuario.Password,
                                        FechaNacimiento = usuario.FechaNacimiento,
                                        Sexo = usuario.SEXO,
                                        Telefono = usuario.Telefono,
                                        Celular = usuario.Celular,
                                        Estatus = usuario.Estatus,
                                        CURP = usuario.CURP,
                                        Imagen = usuario.Imagen,
                                        IdRol = roles.IdRol,
                                        RolNombre = roles.Nombre,
                                        Calle = direccion.Calle,
                                        NumeroExterior = direccion.NumeroExterior,
                                        NumeroInterior = direccion.NumeroInterior,
                                        IdColonia = colonia.IdColonia,
                                        NombreColonia = colonia.Nombre,
                                        CodigoPostal = colonia.CodigoPostal,
                                        IdMunicipio = municipio.IdMunicipio,
                                        NombreMunicipio = municipio.Nombre,
                                        IdEstado = estado.IdEstado,
                                        NombreEstado = estado.Nombre

                                    }).SingleOrDefault();

                    if (registro != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = registro.IdUsuario;

                        usuario.UserName = registro.UserName;
                        usuario.Nombre = registro.UsuarioNombre;
                        usuario.ApellidoPaterno = registro.ApellidoPaterno; ;
                        usuario.ApellidoMaterno = registro.ApellidoMaterno;
                        usuario.Email = registro.Email;
                        usuario.Password = registro.Password;
                        if (registro.FechaNacimiento != null)
                        {
                            usuario.FechaNacimiento = registro.FechaNacimiento.Value;
                        }
                        usuario.SEXO = registro.Sexo;
                        usuario.Telefono = registro.Telefono;
                        usuario.Celular = registro.Celular;
                        if (registro.Estatus != null)
                        {
                            usuario.Estatus = registro.Estatus.Value;
                        }
                        usuario.CURP = registro.CURP;
                        if (registro.Imagen != null)
                        {
                            usuario.Imagen = registro.Imagen;

                        }


                        usuario.Rol.IdRol = registro.IdRol;


                        usuario.Rol.Nombre = registro.RolNombre;
                        usuario.Direccion.Calle = registro.Calle;
                        usuario.Direccion.NumeroInterior = registro.NumeroInterior;
                        usuario.Direccion.NumeroExterior = registro.NumeroExterior;

                        usuario.Direccion.Colonia.IdColonia = registro.IdColonia;


                        usuario.Direccion.Colonia.Nombre = registro.NombreColonia;

                        usuario.Direccion.Colonia.Municipio.IdMunicipio = registro.IdMunicipio;

                        usuario.Direccion.Colonia.CPostal = registro.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = registro.IdEstado;


                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el usuario";
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



        public static ML.Result GetAllSOAP(ML.Usuario usuarioBusqueda)
        {


            ML.Result result = new ML.Result();
            result.Objects = new List<object>();


            string action = "http://tempuri.org/IUsuario/GetAll";
            string url = "http://localhost:60835/Usuario.svc";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml; charset=utf-8";
            request.Accept = "text/xml";
            request.Method = "POST";

            string soapEnvelope = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                  xmlns:tem=""http://tempuri.org/""
                  xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"">
    <soapenv:Header/>
    <soapenv:Body>
        <tem:GetAll>
            <tem:usuarioBusqueda>
                <ml:Nombre></ml:Nombre>
                <ml:ApellidoPaterno></ml:ApellidoPaterno>
                <ml:ApellidoMaterno></ml:ApellidoMaterno>
                <ml:Rol>
                    <ml:IdRol>0</ml:IdRol>
                </ml:Rol>
            </tem:usuarioBusqueda>
        </tem:GetAll>
    </soapenv:Body>
</soapenv:Envelope>";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    XDocument xdoc = XDocument.Parse(reader.ReadToEnd());

                    var elementos = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

                    foreach (var elem in elementos)

                    {


                        ML.Usuario usuario = new ML.Usuario();

                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        int idUsuario;
                        if (elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value != null)
                        {
                            idUsuario = int.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value);
                        }
                        else
                        {
                            idUsuario = 0;
                        }

                        int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario);
                        usuario.IdUsuario = idUsuario;

                        usuario.UserName = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty;
                        usuario.Nombre = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty;
                        usuario.ApellidoPaterno = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty;
                        usuario.ApellidoMaterno = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty;
                        usuario.Email = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty;
                        usuario.Password = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty;

                        DateTime fechaNacimiento;
                        DateTime.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value, out fechaNacimiento);
                        usuario.FechaNacimiento = fechaNacimiento;

                        usuario.SEXO = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}SEXO")?.Value ?? string.Empty;
                        usuario.Telefono = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty;
                        usuario.Celular = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty;

                        bool estatus;
                        bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out estatus);
                        usuario.Estatus = estatus;

                        var imagenNode = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Imagen");

                        if (imagenNode != null && !string.IsNullOrEmpty(imagenNode.Value))
                        {
                            usuario.Imagen = Convert.FromBase64String(imagenNode.Value);
                        }
                        else
                        {
                            usuario.Imagen = null;
                        }




                        var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");


                        usuario.Rol.Nombre = (string)rol.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre");


                        usuario.CURP = (string)elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty;


                        var direccion = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");

                        usuario.Direccion.Calle = (string)direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty;
                        usuario.Direccion.NumeroExterior = (string)direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty;
                        usuario.Direccion.NumeroInterior = (string)direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty;

                        var colonia = direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                        usuario.Direccion.Colonia.Nombre = (string)colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty;
                        usuario.Direccion.Colonia.CPostal = (string)colonia.Element("{http://schemas.datacontract.org/2004/07/ML}CPostal")?.Value ?? string.Empty;

                        var municipio = colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                        usuario.Direccion.Colonia.Municipio.Nombre = (string)municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty;

                        var estado = municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = (string)estado.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty;


                        result.Objects.Add(usuario);
                    }
                    result.Correct = true;
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

        public static ML.Result SaopInsertUpdate(ML.Usuario usuario)
        {
            ML.Result resultsoap = new ML.Result();
            string url = "http://localhost:60835/Usuario.svc";

            string soapEnvelope;

            string action; // Declarar la variable action aquí 

            string imagenBase64 = usuario.Imagen != null
                              ? Convert.ToBase64String(usuario.Imagen)
                              : string.Empty;

            string fechaNacimiento =
                usuario.FechaNacimiento.ToString("yyyy-MM-ddTHH:mm:ss");


            // Verificar si IdUsuario es null o 0 (o algún valor que determines como "nuevo") 
            string estatus = usuario.Estatus == true ? "true" : "false";
            if (usuario.IdUsuario == 0)

            {

                // Crear el sobre SOAP para agregar un nuevo usuario 

                action = "http://tempuri.org/IUsuario/Add";

                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 

<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Add>
         <!--Optional:-->
         <tem:usuario>
            <!--Optional:-->
            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
            <!--Optional:-->
            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
            <!--Optional:-->
            <ml:CURP>{usuario.CURP}</ml:CURP>
            <!--Optional:-->
            <ml:Celular>{usuario.Celular}</ml:Celular>
            <!--Optional:-->
            <ml:Direccion>
               <!--Optional:-->
               <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
               <!--Optional:-->
               <ml:Colonia>
                  <!--Optional:-->
                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                  
               </ml:Colonia>
              
               <!--Optional:-->
               <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
               <!--Optional:-->
               <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
               <!--Optional:-->
               <ml:Usuario/>
            </ml:Direccion>
            <!--Optional:-->
            <ml:Email>{usuario.Email}</ml:Email>
            <!--Optional:-->
            <ml:Estatus>{estatus}</ml:Estatus>
            <!--Optional:-->
            <ml:FechaNacimiento>{fechaNacimiento}</ml:FechaNacimiento>
            <!--Optional:-->
            <ml:Imagen>{imagenBase64}</ml:Imagen>
            <!--Optional:-->
            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
            <!--Optional:-->
            <ml:Password>{usuario.Password}</ml:Password>
            <!--Optional:-->
            <ml:Rol>
               <!--Optional:-->
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
               
            </ml:Rol>
            <!--Optional:-->
            <ml:SEXO>{usuario.SEXO}</ml:SEXO>
            <!--Optional:-->
            <ml:Telefono>{usuario.Telefono}</ml:Telefono>
            <!--Optional:-->
            <ml:UserName>{usuario.UserName}</ml:UserName>
            
         </tem:usuario>
      </tem:Add>
   </soapenv:Body>
</soapenv:Envelope>";

            }

            else

            {

                // Crear el sobre SOAP para actualizar un usuario existente 

                action = "http://tempuri.org/IUsuario/Update";

                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 

<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:Update>
         <!--Optional:-->
         <tem:usuario>
            <!--Optional:-->
            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
            <!--Optional:-->
            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
            <!--Optional:-->
            <ml:CURP>{usuario.CURP}</ml:CURP>
            <!--Optional:-->
            <ml:Celular>{usuario.Celular}</ml:Celular>
            <!--Optional:-->
           <ml:Direccion>
               <!--Optional:-->
               <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
               <!--Optional:-->
               <ml:Colonia>
                  <!--Optional:-->
                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                  
               </ml:Colonia>
              
               <!--Optional:-->
               <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
               <!--Optional:-->
               <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
               <!--Optional:-->
               <ml:Usuario/>
            </ml:Direccion>
            <!--Optional:-->
           <ml:Email>{usuario.Email}</ml:Email>
            <!--Optional:-->
            <ml:Estatus>{estatus}</ml:Estatus>
            <!--Optional:-->
            <ml:FechaNacimiento>{fechaNacimiento}</ml:FechaNacimiento>
            <!--Optional:-->
            <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
            <!--Optional:-->
             <ml:Imagen>{imagenBase64}</ml:Imagen>
            <!--Optional:-->
            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
            <!--Optional:-->
            <ml:Password>{usuario.Password}</ml:Password>
            <!--Optional:-->
            <ml:Rol>
               <!--Optional:-->
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
               
            </ml:Rol>
            <!--Optional:-->
            <ml:SEXO>{usuario.SEXO}</ml:SEXO>
            <!--Optional:-->
            <ml:Telefono>{usuario.Telefono}</ml:Telefono>
            <!--Optional:-->
            <ml:UserName>{usuario.UserName}</ml:UserName>
            
         </tem:usuario>
      </tem:Update>
   </soapenv:Body>
</soapenv:Envelope>";

            }



            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action 

            request.ContentType = "text/xml;charset=\"utf-8\"";

            request.Accept = "text/xml";

            request.Method = "POST";



            // Enviar la solicitud 

            using (Stream stream = request.GetRequestStream())

            {

                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);

                stream.Write(content, 0, content.Length);

            }



            // Obtener la respuesta 

            try

            {

                using (WebResponse response = request.GetResponse())

                {

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))

                    {

                        string result = reader.ReadToEnd();

                        Console.WriteLine("Respuesta SOAP:");

                        Console.WriteLine(result);

                        // Aquí puedes manejar la respuesta según sea necesario 

                    }

                    resultsoap.Correct = true;
                }
            }
            catch (Exception ex)
            {
                resultsoap.Correct = false;
                resultsoap.ErrorMessage = ex.Message;
                resultsoap.Ex = ex;

            }



            return resultsoap; // Redirigir a la lista de usuarios después de agregar o actualizar 
        }

        public static ML.Result GetByIdSoap(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = null;



            if (IdUsuario > 0)

            {

                // Obtener el usuario por ID 

                string action = "http://tempuri.org/IUsuario/GetById";

                string url = "http://localhost:60835/Usuario.svc";



                // Crear el sobre SOAP para obtener un usuario por su ID 

                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?> 

<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:GetById>
         <!--Optional:-->
         <tem:id>{IdUsuario}</tem:id>
      </tem:GetById>
   </soapenv:Body>
</soapenv:Envelope>";




                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action 

                request.ContentType = "text/xml;charset=\"utf-8\"";

                request.Accept = "text/xml";

                request.Method = "POST";


                // Enviar la solicitud 
                using (Stream stream = request.GetRequestStream())

                {

                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);

                    stream.Write(content, 0, content.Length);

                }



                // Obtener la respuesta 

                try

                {

                    using (WebResponse response = request.GetResponse())

                    {

                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))

                        {



                            string result2 = reader.ReadToEnd();

                            Console.WriteLine("Respuesta SOAP:");

                            Console.WriteLine(result2);



                            // Deserializar el usuario 

                            usuario = GetUsuarioById(result2);
                            result.Object = usuario;
                        }

                        result.Correct = true;
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;

                }

            }

            return result;
        }


        public static ML.Usuario GetUsuarioById(string xml)
        {
            ML.Result result = new ML.Result();
            xml = xml.Trim();

            string delimitadorInicio = "<s:Envelope";
            string delimitadorFin = "</s:Envelope>";

            int indiceInicio = xml.IndexOf(delimitadorInicio);
            int indiceFinal = xml.IndexOf(delimitadorFin);
            int longitud = indiceFinal - indiceInicio + delimitadorFin.Length;

            string xmlLimpio = xml.Substring(indiceInicio, longitud);


            var xdoc = XDocument.Parse(xmlLimpio);

            XNamespace ml = "http://schemas.datacontract.org/2004/07/ML";

            var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Object" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (usuarioElement == null)
                return null;

            ML.Usuario usuario = new ML.Usuario
            {
                IdUsuario = (int?)usuarioElement.Element(ml + "IdUsuario") ?? 0,
                UserName = (string)usuarioElement.Element(ml + "UserName"),
                Nombre = (string)usuarioElement.Element(ml + "Nombre"),
                ApellidoPaterno = (string)usuarioElement.Element(ml + "ApellidoPaterno"),
                ApellidoMaterno = (string)usuarioElement.Element(ml + "ApellidoMaterno"),
                FechaNacimiento = (DateTime)usuarioElement.Element(ml + "FechaNacimiento"),
                SEXO = (string)usuarioElement.Element(ml + "Sexo"),
                CURP = (string)usuarioElement.Element(ml + "CURP"),
                Email = (string)usuarioElement.Element(ml + "Email"),
                Password = (string)usuarioElement.Element(ml + "Password"),
                Celular = (string)usuarioElement.Element(ml + "Celular"),
                Telefono = (string)usuarioElement.Element(ml + "Telefono"),
                Estatus = (bool?)usuarioElement.Element(ml + "Estatus") ?? false,
            };
            usuario.Rol = new ML.Rol
            {
                IdRol = (int?)usuarioElement.Descendants(ml + "IdRol").FirstOrDefault() ?? 0,
            };
            usuario.Direccion = new ML.Direccion()
            {
                Calle = (string)usuarioElement.Descendants(ml + "Calle").FirstOrDefault(),
                NumeroExterior = (string)usuarioElement.Descendants(ml + "NumeroExterior").FirstOrDefault(),
                NumeroInterior = (string)usuarioElement.Descendants(ml + "NumeroInterior").FirstOrDefault(),
            };
            usuario.Direccion.Colonia = new ML.Colonia
            {
                IdColonia = (int?)usuarioElement.Descendants(ml + "IdColonia").FirstOrDefault() ?? 0,
                CPostal = (string)usuarioElement.Descendants(ml + "CPostal").FirstOrDefault()
            };

            usuario.Direccion.Colonia.Municipio = new ML.Municipio
            {
                IdMunicipio = (int?)usuarioElement.Descendants(ml + "IdMunicipio").FirstOrDefault() ?? 0
            };

            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado
            {
                IdEstado = (int?)usuarioElement.Descendants(ml + "IdEstado").FirstOrDefault() ?? 0
            };


            return usuario;
        }
    }



}


