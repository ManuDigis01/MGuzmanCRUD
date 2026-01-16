using ML;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Runtime.Remoting.Messaging;

namespace BL
{
    public  class CargaMasiva
    {

        public static ML.ResultTXT Validar(string path)
        {
            ML.ResultTXT resultTXT = new ML.ResultTXT();
            resultTXT.ErroresEncontrados = new List<object>();
            resultTXT.Correctos = new List<object>();

            using(StreamReader reader = new StreamReader(path))
            {

                reader.ReadLine();
                string linea = "";
                int contadorRegistros = 1;
                ML.ResultTXT listaDeErrores = new ML.ResultTXT();
                listaDeErrores.ErroresEncontrados = new List<object>();
                listaDeErrores.Correctos = new List<object>();

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] valores = linea.Split('|');

                    ML.ResultTXT error = new ML.ResultTXT();

                    error.NumeroRegistro = contadorRegistros;

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();

                    usuario.UserName = valores[0];
                    usuario.Nombre = valores[1];
                    usuario.ApellidoPaterno = valores[2];
                    usuario.ApellidoMaterno = valores[3];
                    usuario.Email = valores[4];
                    usuario.Password = valores[5];
                    usuario.FechaNacimiento = DateTime.Parse( valores[6]);
                    usuario.SEXO = valores[7];
                    usuario.Telefono = valores[8];
                    usuario.Celular = valores[9];
                    usuario.Estatus = Convert.ToBoolean(valores[10]);
                    usuario.CURP = valores[11];
                    usuario.Imagen = Encoding.Unicode.GetBytes(valores[12]);

                    usuario.Rol.IdRol = ParseNullableInt(valores[13]);

                    usuario.Direccion.Calle = valores[14];
                    usuario.Direccion.NumeroInterior = valores[15];
                    usuario.Direccion.NumeroExterior = valores[16];
                    usuario.Direccion.Colonia.IdColonia = ParseNullableInt(valores[17]);

                    var soloAlfanumericos = new Regex(@"^[A-Za-z0-9\s]+$");

                    if (!soloAlfanumericos.IsMatch(usuario.UserName))
                    {
                        error.Error += "En el campo UsernName, solo se permite letras y números \n";
                    }

                    var soloLetras = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$");

                    if (!soloLetras.IsMatch(usuario.Nombre))
                    {
                        error.Error += "En el campo Nombre, solo se permite letras \n";
                    }
                    if (!soloLetras.IsMatch(usuario.ApellidoPaterno))
                    {
                        error.Error += "En el campo Apellido Paterno, solo se permite letras \n";
                    }
                    if (!soloLetras.IsMatch(usuario.ApellidoMaterno))
                    {
                        error.Error += "En el campo Apellido Materno, solo se permite letras \n";
                    }


                    var soloEmail = new Regex(@"[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                    if (soloEmail.IsMatch(usuario.Email))
                    {
                        error.Error += "En el Campo Email, Solo se acepta caracteres (@) \n";
                    }

                    var validacionPassword = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@!%*?&])[A-Za-z\d$@@!%*?&]{8,15}$");
                    if (!validacionPassword.IsMatch(usuario.Password))
                    {
                        error.Error += "En el Campo Contraseña, debe tener 8-15 caracteres, al menos una mayúscula, una minúscula, un número y un carácter especial ($@@!%*?&) \n";
                    }

                    var validacionFecha = new Regex(@"(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$");

                    //if (!validacionFecha.IsMatch(usuario.FechaNacimiento))
                    //{
                    //    error.Error = "En el campo Fecha, solo se acepta yyyy/mm/dd \n";
                    //}

                    if (usuario.SEXO != "H" && usuario.SEXO != "M")
                    {
                        error.Error += "En el campo Sexo, solo se permite la letra H (Hombre) o M (Mujer).\n";
                    }

                    var soloTelefonia = new Regex(@"^\d{10}$");

                    if (!soloTelefonia.IsMatch(usuario.Telefono))
                    {
                        error.Error += "En el Campo Teléfono, solo se Aceptan 10 números y no letras. \n";
                    }
                    if (!soloTelefonia.IsMatch(usuario.Celular))
                    {
                        error.Error += "En el Campo Celular, solo se Aceptan 10 numeros y no letras. \n";
                    }

                    if (usuario.Estatus == true && usuario.Estatus == false)
                    {
                        error.Error += "En el Campo Estatus, Solo se acepta true y false. \n";

                    }

                    var validaconCurp = new Regex(@"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$");
                    if (!validaconCurp.IsMatch(usuario.CURP))
                    {
                        error.Error += "En el campo CURP, solo se acepta alfanuméricos y 18 Caracteres \n";
                    }
                    if (usuario.Rol.IdRol != null && usuario.Rol.IdRol < 1 || usuario.Rol.IdRol > 3)
                    {
                        error.Error += "En el campo Rol, solo ahi un rango del 1 al 3 \n";
                    }

                    if (!soloAlfanumericos.IsMatch(usuario.Direccion.Calle))
                    {
                        error.Error += "En el Campo Calle, solo se permite letras y numeros \n";
                    }
                    var soloNumeros = new Regex(@"^[0-9]+$");

                    if (!soloNumeros.IsMatch(usuario.Direccion.NumeroInterior))
                    {
                        error.Error += "En el Campo Numero Interior, solo se permite  numeros \n";
                    }

                    if (!soloNumeros.IsMatch(usuario.Direccion.NumeroExterior))
                    {
                        error.Error += "En el Campo Numero Exterior, solo se permite numeros \n";
                    }

                    if (usuario.Direccion.Colonia.IdColonia < 1 || usuario.Direccion.Colonia.IdColonia > 2355 && usuario.Direccion.Colonia.IdColonia == null)
                    {
                        error.Error += "En el campo Colonia,  el rango de 1 a 2355. \n";
                    }





                    if (error.Error != "" && error.Error != null)
                    {
                        
                        resultTXT.ErroresEncontrados.Add(error);
                    }
                    else
                    {
                        resultTXT.Correctos.Add(error);
                       
                    }


                    contadorRegistros++;
                }
                return resultTXT;
            }
        }

        public static ML.Result leer(string path)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (StreamReader reader = new StreamReader(path))
            {

                string linea = reader.ReadLine();
                

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] valores = linea.Split('|');

                   
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();

                    usuario.UserName = valores[0];
                    usuario.Nombre = valores[1];
                    usuario.ApellidoPaterno = valores[2];
                    usuario.ApellidoMaterno = valores[3];
                    usuario.Email = valores[4];
                    usuario.Password = valores[5];
                    usuario.FechaNacimiento = DateTime.Parse(valores[6]);
                    usuario.SEXO = valores[7];
                    usuario.Telefono = valores[8];
                    usuario.Celular = valores[9];
                    usuario.Estatus = Convert.ToBoolean(valores[10]);
                    usuario.CURP = valores[11];
                    usuario.Imagen = Encoding.Unicode.GetBytes(valores[12]);

                    usuario.Rol.IdRol = ParseNullableInt(valores[13]);

                    usuario.Direccion.Calle = valores[14];
                    usuario.Direccion.NumeroInterior = valores[15];
                    usuario.Direccion.NumeroExterior = valores[16];
                    usuario.Direccion.Colonia.IdColonia = ParseNullableInt(valores[17]);

                    result.Objects.Add(usuario);
                }
               
            }

            return result;
           
        }


        public static ML.ResultTXT ValidacioXLSX(string path)
        {
           
            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + path;

            ML.ResultTXT resultTXT = new ML.ResultTXT();
            using (OleDbConnection context = new OleDbConnection(connectionString))
            {


                resultTXT.ErroresEncontrados = new List<object>();
                resultTXT.Correctos = new List<object>();

                int contadorRegistros = 1;
                ML.ResultTXT listaDeErrores = new ML.ResultTXT();
                listaDeErrores.ErroresEncontrados = new List<object>();
                listaDeErrores.Correctos = new List<object>();


                OleDbCommand cmd = context.CreateCommand();
                cmd.Connection = context;
                cmd.CommandText = "SELECT * FROM [Sheet1$]";

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                   

                    foreach (DataRow row in dt.Rows)
                    {
                        ML.ResultTXT error = new ML.ResultTXT();

                        error.NumeroRegistro = contadorRegistros;

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();

                        usuario.UserName = row[0].ToString();
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.Email = row[4].ToString();
                        usuario.Password = row[5].ToString();
                        usuario.FechaNacimiento = DateTime.Parse( row[6].ToString());
                        usuario.SEXO = row[7].ToString();
                        usuario.Telefono = row[8].ToString();
                        usuario.Celular = row[9].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[10].ToString());
                        usuario.CURP = row[11].ToString();


                        usuario.Rol.IdRol = Convert.ToInt32(row[12].ToString());

                        usuario.Direccion.Calle = row[13].ToString();
                        usuario.Direccion.NumeroInterior = row[14].ToString();
                        usuario.Direccion.NumeroExterior = row[15].ToString();
                        usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[16].ToString());


                        var soloAlfanumericos = new Regex(@"^[A-Za-z0-9\s]+$");

                        if (!soloAlfanumericos.IsMatch(usuario.UserName))
                        {
                            error.Error += "En el campo UsernName, solo se permite letras y números \n";
                        }

                        var soloLetras = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$");

                        if (!soloLetras.IsMatch(usuario.Nombre))
                        {
                            error.Error += "En el campo Nombre, solo se permite letras \n";
                        }
                        if (!soloLetras.IsMatch(usuario.ApellidoPaterno))
                        {
                            error.Error += "En el campo Apellido Paterno, solo se permite letras \n";
                        }
                        if (!soloLetras.IsMatch(usuario.ApellidoMaterno))
                        {
                            error.Error += "En el campo Apellido Materno, solo se permite letras \n";
                        }


                        var soloEmail = new Regex(@"[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                        if (soloEmail.IsMatch(usuario.Email))
                        {
                            error.Error += "En el Campo Email, Solo se acepta caracteres (@) \n";
                        }

                        var validacionPassword = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@@!%*?&])[A-Za-z\d$@@!%*?&]{8,15}$");
                        if (!validacionPassword.IsMatch(usuario.Password))
                        {
                            error.Error += "En el Campo Contraseña, debe tener 8-15 caracteres, al menos una mayúscula, una minúscula, un número y un carácter especial ($@@!%*?&) \n";
                        }

                        var validacionFecha = new Regex(@"(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$");

                        //if (!validacionFecha.IsMatch(usuario.FechaNacimiento))
                        //{
                        //    error.Error = "En el campo Fecha, solo se acepta yyyy/mm/dd \n";
                        //}

                        if (usuario.SEXO != "H" && usuario.SEXO != "M")
                        {
                            error.Error += "En el campo Sexo, solo se permite la letra H (Hombre) o M (Mujer).\n";
                        }

                        var soloTelefonia = new Regex(@"^\d{10}$");

                        if (!soloTelefonia.IsMatch(usuario.Telefono))
                        {
                            error.Error += "En el Campo Teléfono, solo se Aceptan 10 números y no letras. \n";
                        }
                        if (!soloTelefonia.IsMatch(usuario.Celular))
                        {
                            error.Error += "En el Campo Celular, solo se Aceptan 10 numeros y no letras. \n";
                        }

                        if (usuario.Estatus == true && usuario.Estatus == false)
                        {
                            error.Error += "En el Campo Estatus, Solo se acepta true y false. \n";

                        }

                        var validaconCurp = new Regex(@"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$");
                        if (!validaconCurp.IsMatch(usuario.CURP))
                        {
                            error.Error += "En el campo CURP, solo se acepta alfanuméricos y 18 Caracteres \n";
                        }
                        if (usuario.Rol.IdRol != null && usuario.Rol.IdRol < 1 || usuario.Rol.IdRol > 3)
                        {
                            error.Error += "En el campo Rol, solo ahi un rango del 1 al 3 \n";
                        }

                        if (!soloAlfanumericos.IsMatch(usuario.Direccion.Calle))
                        {
                            error.Error += "En el Campo Calle, solo se permite letras y numeros \n";
                        }
                        var soloNumeros = new Regex(@"^[0-9]+$");

                        if (!soloNumeros.IsMatch(usuario.Direccion.NumeroInterior))
                        {
                            error.Error += "En el Campo Numero Interior, solo se permite  numeros \n";
                        }

                        if (!soloNumeros.IsMatch(usuario.Direccion.NumeroExterior))
                        {
                            error.Error += "En el Campo Numero Exterior, solo se permite numeros \n";
                        }

                        if (usuario.Direccion.Colonia.IdColonia < 1 || usuario.Direccion.Colonia.IdColonia > 2355 && usuario.Direccion.Colonia.IdColonia == null)
                        {
                            error.Error += "En el campo Colonia,  el rango de 1 a 2355. \n";
                        }





                        if (error.Error != "" && error.Error != null)
                        {

                            resultTXT.ErroresEncontrados.Add(error);
                        }
                        else
                        {
                            resultTXT.Correctos.Add(error);

                        }


                        contadorRegistros++;

                    }
                    

                }

            }

            return resultTXT;
        }


        public static ML.Result leerXLSX(string path)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + path;

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            using (OleDbConnection context = new OleDbConnection(connectionString))
       {


                OleDbCommand cmd = context.CreateCommand();
                cmd.Connection = context;
                cmd.CommandText = "SELECT * FROM [Sheet1$]";

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow row in dt.Rows)
                    {


                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();

                        usuario.UserName = row[0].ToString();
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.Email = row[4].ToString();
                        usuario.Password = row[5].ToString();
                        usuario.FechaNacimiento = DateTime.Parse(row[6].ToString());
                        usuario.SEXO = row[7].ToString();
                        usuario.Telefono = row[8].ToString();
                        usuario.Celular = row[9].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[10].ToString());
                        usuario.CURP = row[11].ToString();


                        usuario.Rol.IdRol = Convert.ToInt32(row[12].ToString());

                        usuario.Direccion.Calle = row[13].ToString();
                        usuario.Direccion.NumeroInterior = row[14].ToString();
                        usuario.Direccion.NumeroExterior = row[15].ToString();
                        usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[16].ToString());

                        result.Objects.Add(usuario);


                    }


                }

            }

            return result;
        }





        public static int? ParseNullableInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null; // Si es null, vacío o solo espacios, devuelve null

            if (int.TryParse(value, out int result))
                return result; // Si se puede convertir a entero, devuelve el número

            return null; // Si no se puede convertir, devuelve null
       
        
        }


    }
}



