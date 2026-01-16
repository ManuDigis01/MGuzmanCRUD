using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL
{
    public class Program
    {

        static void Main(string[] args)
        
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + @"C:\Users\digis\Documents\Manuel Alejandro Guzman Canales\Usuarios.xlsx";

           

            using (OleDbConnection context = new OleDbConnection(connectionString))
            {
                ML.Result result = new Result();
                

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
                        usuario.Direccion =  new ML.Direccion();
                        usuario.Direccion.Colonia =new ML.Colonia();

                        usuario.UserName = row[0].ToString();   
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();    
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.Email = row[4].ToString();
                        usuario.Password = row[5].ToString();
                        usuario.FechaNacimiento = Convert.ToDateTime(row[6].ToString());
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

                        ////List<object> list = new List<object>(); 
                        //list.Add(usuario);

                        Console.WriteLine(usuario.UserName+" - "+usuario.Nombre+" - "+usuario.ApellidoPaterno+" - "+usuario.ApellidoMaterno+" - "+usuario.Email+" - "+usuario.Password+" - "+usuario.FechaNacimiento+" - "+usuario.SEXO+" - "+usuario.Telefono+" - "+usuario.Celular+" - "+usuario.Estatus+" - "+usuario.CURP+" - "+ usuario.Rol.IdRol+" - "+usuario.Direccion.Calle+" - "+usuario.Direccion.NumeroExterior+" - "+usuario.Direccion.NumeroInterior+" - "+usuario.Direccion.Colonia.IdColonia);
                       
                      
                    }
                }
                   
            }





















            //string path = @"C:\Users\digis\Documents\Manuel Alejandro Guzman Canales\Usuarios.txt";


            //ML.ResultTXT resultValidacion = BL.CargaMasiva.Validar(path);

            //if (resultValidacion.ErroresEncontrados.Count > 0)
            //{
            //    string errores = @"C:\Users\digis\Documents\Manuel Alejandro Guzman Canales\MGuzmanCRUD - Copy\PL\Errores\ErrorTXT" + DateTime.Now.ToString(" HH mm yyyy MM") + ".txt";
            //    using (StreamWriter writer = new StreamWriter(errores))
            //    {
            //        foreach (ML.ResultTXT errorLista in resultValidacion.ErroresEncontrados)
            //        {
            //            string LineaTXT = "En el registro Numero " + errorLista.NumeroRegistro + " se encontraron estos errores: \n" + errorLista.Error;
            //            Console.WriteLine(LineaTXT);
            //            Console.WriteLine(errorLista.Error);

            //            string separador = "\n-----------------------------------------------------------------";
            //            writer.WriteLine(LineaTXT + separador);
            //            Console.WriteLine(separador);
            //            Console.ReadLine();


            //        }
            //    }
            //}
            //else
            //{
            //}


            //if (resultValidacion.Correctos.Count > 0)
            //{
            //    string correcto = @"C:\Users\digis\Documents\Manuel Alejandro Guzman Canales\MGuzmanCRUD - Copy\PL\Correctos\CorrectoTXT" + DateTime.Now.ToString(" HH mm yyyy MM") + ".txt";
            //    using (StreamWriter writer = new StreamWriter(correcto))
            //    {
            //        foreach (ML.ResultTXT listaCorrectos in resultValidacion.Correctos)
            //        {

            //            //string LineaTXT = usuarioTXT.UserName + "|" + usuarioTXT.Nombre + "|"+usuarioTXT.ApellidoPaterno+"|"+usuarioTXT.ApellidoMaterno+"|"+usuarioTXT.Email+"|"+usuarioTXT.Password+"|"+usuarioTXT.FechaNacimiento+"|"+usuarioTXT.SEXO+"|"+usuarioTXT.Telefono+"|"+usuarioTXT.Celular+"|"+usuarioTXT.Estatus+"|"+usuarioTXT.CURP+"|"+"NULL"+"|"+usuarioTXT.Rol.IdRol+"|"+usuarioTXT.Direccion.Calle+"|"+usuarioTXT.Direccion.NumeroInterior+"|"+usuarioTXT.Direccion.NumeroExterior+"|"+usuarioTXT.Direccion.Colonia.IdColonia;
            //            //Console.WriteLine(LineaTXT);


            //            string LineaTXT = "Este Registro es Correcto: " + listaCorrectos.NumeroRegistro;
            //            Console.WriteLine(LineaTXT);



            //            writer.WriteLine(LineaTXT);

            //            Console.ReadLine();


            //        }
            //    }
            //}








            //ML.Usuario usuario = new ML.Usuario();
            //usuario.Rol = new Rol();
            //int opcion = 0;


            //do
            //{

            //    Console.WriteLine("--------------");
            //    Console.WriteLine("1: Insert");
            //    Console.WriteLine("2: Update");
            //    Console.WriteLine("3: Delete");
            //    Console.WriteLine("4: Select");
            //    Console.WriteLine("5: Exit");
            //    Console.WriteLine("--------------");


            //    opcion = Int32.Parse(Console.ReadLine());
            //    Console.Clear();

            //    if (opcion == 1)
            //    {
            //        Console.WriteLine("Escribe tu User Name: ");
            //        usuario.UserName = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Nombre: ");
            //        usuario.Nombre = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Apellido Paterno: ");
            //        usuario.ApellidoPaterno = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Apellido Materno: ");
            //        usuario.ApellidoMaterno = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Email: ");
            //        usuario.Email = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Password: ");
            //        usuario.Password = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Fecha de Nacimineto aa/mm/yyyy: ");
            //        usuario.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            //        Console.WriteLine("Escribe Sexo Hombre(H) o Muejer(M): ");
            //        usuario.SEXO = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Numero de Telefono: ");
            //        usuario.Telefono = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Numero Celular: ");
            //        usuario.Celular = Console.ReadLine();
            //        Console.WriteLine("Escribe tu Estatus (True o False): ");
            //        usuario.Estatus = bool.Parse(Console.ReadLine());
            //        Console.WriteLine("Escribe tu CRUP: ");
            //        usuario.CURP = Console.ReadLine();
            //        //Console.WriteLine("Insert imagen: ");
            //        //usuario.Imagen = Encoding.UTF8.GetBytes(Console.ReadLine());
            //        Console.WriteLine("Escribe tu Rol: ");
            //        usuario.Rol.IdRol = int.Parse(Console.ReadLine());
            //        Console.Clear();
            //    }
            //    else if (opcion == 3)
            //    {
            //        Console.WriteLine("Escribe el ID: ");
            //        usuario.IdUsuario = int.Parse(Console.ReadLine());
            //        Console.Clear();
            //    }
            //    else { }


            //    switch (opcion)
            //    {

            //        case 1:
            //            bool respuestaAdd = BL.Usuario.Add(usuario);
            //            if (respuestaAdd)
            //            {
            //                Console.WriteLine("Se Registro Correctamente");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Error.......");
            //            }
            //            break;
            //        case 2:
            //            Console.WriteLine("Escribe el ID: ");
            //            int id = int.Parse(Console.ReadLine());

            //            object resultadoS = BL.Usuario.GetById(id);
            //            ML.Usuario userBy = (ML.Usuario)resultadoS;

            //            Console.WriteLine("User name: " + userBy.UserName);
            //            Console.WriteLine("Nombre: " + userBy.Nombre);
            //            Console.WriteLine("Apellido Paterno: " + userBy.ApellidoPaterno);
            //            Console.WriteLine("Apellido Materno: " + userBy.ApellidoMaterno);
            //            Console.WriteLine("Email: " + userBy.Email);
            //            Console.WriteLine("Password: " + userBy.Password);
            //            Console.WriteLine("Fecha de Nacimineto: " + userBy.FechaNacimiento);
            //            Console.WriteLine("Sexo: " + userBy.SEXO);
            //            Console.WriteLine("Telefono: " + userBy.Telefono);
            //            Console.WriteLine("Celular: " + userBy.Celular);
            //            Console.WriteLine("Estatus: " + userBy.Estatus);
            //            Console.WriteLine("Curp: " + userBy.CURP);
            //            //Console.WriteLine("Imagen: " + userBy.Imagen);
            //            Console.WriteLine("Id Rol: " + userBy.Rol.IdRol);

            //            Console.WriteLine("-----------------------------");
            //            Console.WriteLine("Actualizar Registro:");

            //            ML.Usuario userUpdate = new ML.Usuario();
            //            userUpdate.Rol = new Rol();


            //            if (userUpdate != null)
            //            {

            //                userUpdate.IdUsuario = (id);
            //                Console.WriteLine("Escribe tu User Name: ");
            //                userUpdate.UserName = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Nombre: ");
            //                userUpdate.Nombre = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Apellido Paterno: ");
            //                userUpdate.ApellidoPaterno = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Apellido Materno: ");
            //                userUpdate.ApellidoMaterno = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Email: ");
            //                userUpdate.Email = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Password: ");
            //                userUpdate.Password = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Fecha de Nacimineto: ");
            //                userUpdate.FechaNacimiento = DateTime.Parse(Console.ReadLine());
            //                Console.WriteLine("Escribe Sexo Hombre(H) o Muejer(M): ");
            //                userUpdate.SEXO = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Numero de Telefono: ");
            //                userUpdate.Telefono = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Numero Celular: ");
            //                userUpdate.Celular = Console.ReadLine();
            //                Console.WriteLine("Escribe tu Estatus (True o False): ");
            //                userUpdate.Estatus = bool.Parse(Console.ReadLine());
            //                Console.WriteLine("Escribe tu CRUP: ");
            //                userUpdate.CURP = Console.ReadLine();
            //                //Console.WriteLine("Insert imagen: ");
            //                //usuario.Imagen = Encoding.UTF8.GetBytes(Console.ReadLine());
            //                Console.WriteLine("Escribe tu Rol: ");
            //                userUpdate.Rol.IdRol = int.Parse(Console.ReadLine());
            //                Console.Clear();

            //                bool respuetaUp = BL.Usuario.Update(userUpdate);

            //                if (respuetaUp)
            //                {
            //                    Console.WriteLine("Se Actualizo Correctamente");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Error.......");
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Error.......");
            //            }



            //            break;
            //        case 3:
            //            bool respuesta = BL.Usuario.Delete(usuario.IdUsuario);
            //            if (respuesta)
            //            {
            //                Console.WriteLine("Se elimino Correctamente");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Error.......");
            //            }
            //            break;

            //        case 4:
            //            int opcionS;
            //            do
            //            {


            //                Console.WriteLine("1: Selecionar solo un Usuario");
            //                Console.WriteLine("2: Mostrar todo los Usuarios");
            //                Console.WriteLine("3: exit");
            //                opcionS = Int32.Parse(Console.ReadLine());
            //                Console.Clear();

            //                if (opcionS == 1)
            //                {
            //                    Console.WriteLine("Escribe el ID: ");
            //                    usuario.IdUsuario = int.Parse(Console.ReadLine());
            //                    Console.Clear();
            //                }
            //                else
            //                {

            //                }


            //                switch (opcionS)
            //                {
            //                    case 1:

            //                        object resultado = BL.Usuario.GetById(usuario.IdUsuario);
            //                        ML.Usuario userByS = (ML.Usuario)resultado;

            //                        Console.WriteLine("UserUsuario: " + userByS.UserName);
            //                        Console.WriteLine("Nombre: " + userByS.Nombre);
            //                        Console.WriteLine("Apellido Paterno: " + userByS.ApellidoPaterno);
            //                        Console.WriteLine("Apellido Materno: " + userByS.ApellidoMaterno);
            //                        Console.WriteLine("Email: " + userByS.Email);
            //                        Console.WriteLine("Password: " + userByS.Password);
            //                        Console.WriteLine("Fecha de Nacimineto: " + userByS.FechaNacimiento);
            //                        Console.WriteLine("Sexo: " + userByS.SEXO);
            //                        Console.WriteLine("Telefono: " + userByS.Telefono);
            //                        Console.WriteLine("Celular: " + userByS.Celular);
            //                        Console.WriteLine("Estatus: " + userByS.Estatus);
            //                        Console.WriteLine("Curp: " + userByS.CURP);
            //                        Console.WriteLine("Imagen: " + userByS.Imagen);
            //                        Console.WriteLine("Nombre Rol: " + userByS.Rol.Nombre);

            //                        Console.ReadKey();
            //                        Console.Clear();
            //                        break;
            //                    case 2:

            //                        List<object> registros = BL.Usuario.GetAll();

            //                        foreach (ML.Usuario user in registros)
            //                        {

            //                            Console.WriteLine("UserUsuario: " + user.UserName);
            //                            Console.WriteLine("Nombre: " + user.Nombre);
            //                            Console.WriteLine("Apellido Paterno: " + user.ApellidoPaterno);
            //                            Console.WriteLine("Apellido Materno: " + user.ApellidoMaterno);
            //                            Console.WriteLine("Email: " + user.Email);
            //                            Console.WriteLine("Password: " + user.Password);
            //                            Console.WriteLine("Fecha de Nacimineto: " + user.FechaNacimiento);
            //                            Console.WriteLine("Sexo: " + user.SEXO);
            //                            Console.WriteLine("Telefono: " + user.Telefono);
            //                            Console.WriteLine("Celular: " + user.Celular);
            //                            Console.WriteLine("Estatus: " + user.Estatus);
            //                            Console.WriteLine("Curp: " + user.CURP);
            //                            Console.WriteLine("Imagen: " + user.Imagen);
            //                            Console.WriteLine("Nombre Rol: " + user.Rol.Nombre);
            //                            Console.WriteLine("-------------------------------------------------");


            //                        }
            //                        Console.ReadKey();
            //                        Console.Clear();

            //                        break;

            //                    default:
            //                        break;
            //                }
            //            } while (opcionS != 3);


            //            break;
            //        default:

            //            break;
            //    }

            //} while (opcion != 5);




        }


    }

}

