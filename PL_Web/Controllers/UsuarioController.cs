using Microsoft.Ajax.Utilities;
using Microsoft.Win32;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PL_Web.Controllers
{
    public class UsuarioController : Controller
    {

        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {


            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";

            if (Session["ErroresTXT"] == null)
            {
                Session["pathArchivo"] = null;
            }


            if (usuario.Rol.IdRol != null)
            {
                usuario.Rol.IdRol = 0;



            }



            //UsuarioServiceReference.UsuarioClient client = new UsuarioServiceReference.UsuarioClient();
            //var registros = client.GetAll(usuario);

            //var registros = BL.Usuario.GetAll(usuario);

            //var registros = BL.Usuario.GetAllSOAP(usuario);
            var registros = GetAllAPI();

            if (registros.Correct)
            {
                usuario.Usuarios = registros.Objects;

                ML.Result resultRol = BL.rol.GetAll();

                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }
                else
                {
                    usuario.Rol.Roles = new List<object> { };


                }

                return View(usuario);
            }
            else
            {
                usuario.Usuarios = new List<object>();


                return View(usuario);
            }

        }


        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            usuario.Nombre = usuario.Nombre ?? "";
            usuario.ApellidoPaterno = usuario.ApellidoPaterno ?? "";
            usuario.ApellidoMaterno = usuario.ApellidoMaterno ?? "";




            var registros = BL.Usuario.GetAll(usuario);

            if (registros.Correct)
            {
                usuario.Usuarios = registros.Objects;
                ML.Result resultRol = BL.rol.GetAll();

                if (resultRol.Correct)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                }
                else
                {

                    usuario.Rol.Roles = new List<object> { };
                }

            }
            else
            {
                usuario.Usuarios = new List<object>();
            }


            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUser)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();



            ML.Result result = BL.rol.GetAll();
            usuario.Rol.Roles = result.Objects;
            ML.Result resultDireccion = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDireccion.Objects;

            if (IdUser == null)
            {

            }
            else
            {

                //ML.Result respuesta = BL.Usuario.GetById(IdUser.Value);
                //ML.Result respuesta = BL.Usuario.GetByIdSoap(IdUser.Value);
                ML.Result respuesta = GetByIdWebAPI(IdUser.Value);
                if (respuesta.Correct)
                {
                    usuario = (ML.Usuario)respuesta.Object;

                    ML.Result resultMunicipio = BL.Municipio.GetById(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    if (resultMunicipio.Correct)
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = new List<object> { };
                    }
                    ML.Result resltColonia = BL.Colonia.GetById(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    if (resltColonia.Correct) {
                        usuario.Direccion.Colonia.Colonias = resltColonia.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Colonias = new List<object> { };
                    }
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDireccion.Objects;




                }
                else
                {

                }
                usuario.Rol.Roles = result.Objects;

            }
            return View(usuario);

        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, HttpPostedFileBase imagenAuto)
        {
            if (ModelState.IsValid)
            {

                if (usuario.IdUsuario > 0)
                {

                    if (imagenAuto == null)
                    {




                    }
                    else
                    {
                        var imagen = ConvertToByteArray(imagenAuto);

                        usuario.Imagen = imagen;


                    }

                    //UsuarioServiceReference.UsuarioClient client = new UsuarioServiceReference.UsuarioClient();
                    //var repuesta = client.Update(usuario);
                    //ML.Result respuesta = BL.Usuario.SaopInsertUpdate(usuario);
                    ML.Result respuesta = UpdateRest(usuario);

                    if (respuesta.Correct)
                    {
                        ViewBag.Message = "Se Actualizo Correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "No se Actualizo - " + respuesta.ErrorMessage;
                    }


                }
                else
                {
                    if (imagenAuto == null)
                    {


                    }
                    else
                    {
                        var imagen = ConvertToByteArray(imagenAuto);

                        usuario.Imagen = imagen;


                    }
                    //UsuarioServiceReference.UsuarioClient clientAdd = new UsuarioServiceReference.UsuarioClient();
                    //var repuestaAdd = clientAdd.Add(usuario);

                    //ML.Result repuestaAdd = BL.Usuario.SaopInsertUpdate(usuario);

                    ML.Result repuestaAdd = AddRest(usuario);




                    if (repuestaAdd.Correct)
                    {


                        ViewBag.Message = "El Ususario se Agrego Correctamente";



                    }
                    else
                    {
                        ViewBag.Message = "No se Agrego - " + repuestaAdd.ErrorMessage;

                    }

                }
            }
            else
            {
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                ML.Result result = BL.rol.GetAll();
                usuario.Rol.Roles = result.Objects;
                ML.Result resultDireccion = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultDireccion.Objects;
                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                {
                    ML.Result resultMunicipio = BL.Municipio.GetById(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                }
                else
                {
                    if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                    {
                        ML.Result resltColonia = BL.Colonia.GetById(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                        usuario.Direccion.Colonia.Colonias = resltColonia.Objects;
                    }

                    return RedirectToAction("Form");
                }
            }

            return PartialView("_Model");

            
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            //UsuarioServiceReference.UsuarioClient clientDelete = new UsuarioServiceReference.UsuarioClient();
            //var respuesta = clientDelete.Delete(id);
            ML.Result respuesta = DeleteRest(id);
            if (respuesta.Correct)
            {
                ViewBag.Message = "Se Elimino Correctamente";

            }
            else
            {
                ViewBag.Message = "Ocurrió un error - " + respuesta.ErrorMessage;

            }
            return PartialView("_Model");

            //return RedirectToAction("GetAll");

        }

        [HttpGet]
        public ActionResult GetAllView()
        {
            ML.Usuario usuario = new ML.Usuario();


            var registros = BL.Usuario.GetAllView(usuario);


            if (registros.Correct)
            {
                usuario.Usuarios = registros.Objects;


            }
            else
            {
                usuario.Usuarios = new List<object>();

            }
            return View(usuario);
        }


        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetById(IdEstado);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetById(IdMunicipio);
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult UpdateEstatus(int IdUsuario, bool Estatus)
        {
            var usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            usuario.Estatus = Estatus;


            var result = BL.Usuario.UpdateEstatus(usuario);

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public static byte[] ConvertToByteArray(HttpPostedFileBase imagen)
        {
            using (Stream inputStream = imagen.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }


        [HttpPost]
        public ActionResult CargaMasiva(HttpPostedFileBase archivo) {

            if (Session["pathArchivo"] == null) {

                string extension = Path.GetExtension(archivo.FileName);

                if (extension == ".txt" || extension == ".xlsx")
                {
                    if (extension == ".txt")
                    {
                        string path = Server.MapPath("~/Content/CargaMasiva/TXT/") + Path.GetFileNameWithoutExtension(archivo.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".txt";

                        if (!System.IO.File.Exists(path))
                        {
                            archivo.SaveAs(path);

                            ML.ResultTXT resultTXT = BL.CargaMasiva.Validar(path);
                            Session["ErroresTXT"] = resultTXT;

                            if (resultTXT.ErroresEncontrados.Count == 0)
                            {
                                Session["pathArchivo"] = path;
                            }

                        }


                    }
                    else
                    {
                        if (extension == ".xlsx") {
                            string path = Server.MapPath("~/Content/CargaMasiva/EXCEL/") + Path.GetFileNameWithoutExtension(archivo.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xlsx";

                            if (!System.IO.File.Exists(path))
                            {
                                archivo.SaveAs(path);

                                ML.ResultTXT resultTXT = BL.CargaMasiva.ValidacioXLSX(path);
                                Session["ErroresTXT"] = resultTXT;

                                if (resultTXT.ErroresEncontrados.Count == 0)
                                {
                                    Session["pathArchivo"] = path;
                                }

                            }
                        }

                    }
                }
                else
                {


                }
            }
            else
            {
                if (archivo == null) {

                    string path = (string)Session["pathArchivo"];
                    if (path == ".txt")
                    {
                        ML.Result resultLeer = BL.CargaMasiva.leer(path);
                        if (resultLeer.Objects.Count > 0)
                        {
                            string errores = "";
                            int contador = 1;

                            foreach (ML.Usuario usuario in resultLeer.Objects)
                            {
                                ML.Result resultAdd = BL.Usuario.Add(usuario);
                                if (!resultAdd.Correct)
                                {
                                    errores += "Error en el registro " + contador + "° " + resultAdd.ErrorMessage + "\n";
                                }
                                contador++;

                            }
                            if (errores == null || errores == "")
                            {
                                ViewBag.Message = "Usuarios registrados correcctamente";
                            }
                            else
                            {
                                ViewBag.Message = errores;
                            }
                            Session["pathArchivo"] = null;
                            Session["ErroresTXT"] = null;

                            return PartialView("_Model");
                        }
                    }
                    else
                    {

                        ML.Result resultLeer = BL.CargaMasiva.leerXLSX(path);
                        if (resultLeer.Objects.Count > 0)
                        {
                            string errores = "";
                            int contador = 1;

                            foreach (ML.Usuario usuario in resultLeer.Objects)
                            {
                                ML.Result resultAdd = BL.Usuario.Add(usuario);
                                if (!resultAdd.Correct)
                                {
                                    errores += "Error en el registro " + contador + "° " + resultAdd.ErrorMessage + "\n";
                                }
                                contador++;

                            }
                            if (errores == null || errores == "")
                            {
                                ViewBag.Message = "Usuarios registrados correcctamente";
                            }
                            else
                            {
                                ViewBag.Message = errores;
                            }
                            Session["pathArchivo"] = null;
                            Session["ErroresTXT"] = null;

                            return PartialView("_Model");
                        }
                    }

                } else
                {
                    Session["pathArchivo"] = null;
                    string extension = Path.GetExtension(archivo.FileName);

                    if (extension == ".txt" || extension == ".xlsx")
                    {
                        if (extension == ".txt")
                        {
                            string path = Server.MapPath("~/Content/CargaMasiva/TXT/") + Path.GetFileNameWithoutExtension(archivo.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".txt";

                            if (!System.IO.File.Exists(path))
                            {
                                archivo.SaveAs(path);

                                ML.ResultTXT resultTXT = BL.CargaMasiva.Validar(path);
                                Session["ErroresTXT"] = resultTXT;

                                if (resultTXT.ErroresEncontrados.Count == 0)
                                {
                                    Session["pathArchivo"] = path;
                                }

                            }


                        }
                        else
                        {
                            if (extension == ".xlsx")
                            {
                                string path = Server.MapPath("~/Content/CargaMasiva/EXCEL/") + Path.GetFileNameWithoutExtension(archivo.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xlsx";

                                if (!System.IO.File.Exists(path))
                                {
                                    archivo.SaveAs(path);

                                    ML.ResultTXT resultTXT = BL.CargaMasiva.ValidacioXLSX(path);
                                    Session["ErroresTXT"] = resultTXT;

                                    if (resultTXT.ErroresEncontrados.Count == 0)
                                    {
                                        Session["pathArchivo"] = path;
                                    }

                                }
                            }
                        }
                    }


                }
            }


            return RedirectToAction("GetAll");

        }

        [NonAction]
        public static ML.Result GetAllAPI()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            var url = ConfigurationManager.AppSettings["UsuariosUrl"];
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);

                    var responseTask = client.GetAsync("GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Result resultAPI = readTask.Result;
                        if (resultAPI.Correct && resultAPI.Objects != null)
                        {
                            foreach (var resulItem in resultAPI.Objects)
                            {
                                ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resulItem.ToString());
                                result.Objects.Add(resultItemList);
                            }
                            result.Correct = true;
                        }      }
                    }
                }catch (Exception ex) { 
            
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }


        [NonAction]
        public static ML.Result AddRest(ML.Usuario usuario)
        {
            ML.Result ret = new ML.Result();    
            try
            {
                var url = ConfigurationManager.AppSettings["UsuariosUrl"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var postTask = client.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                postTask.Wait();

                var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ret.Correct = true;
                    } 
                }

            }catch (Exception ex)
             {

                ret.Correct = false;
                ret.ErrorMessage = ex.Message;

            }

            return ret;
            
        
    }

        [NonAction]

        public static ML.Result GetByIdWebAPI(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {
                string url = ConfigurationManager.AppSettings["UsuariosUrl"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync("GetById/" + Id);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = new ML.Usuario();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        result.Correct = true;
                    }
                   

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        
    }
        [NonAction]
        public static ML.Result UpdateRest(ML.Usuario usuario) 
        {
            ML.Result ret = new ML.Result();
            try
            {
                var url = ConfigurationManager.AppSettings["UsuariosUrl"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PutAsJsonAsync<ML.Usuario>("Update", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ret.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {

                ret.Correct = false;
                ret.ErrorMessage = ex.Message;

            }

            return ret;


        }
        [NonAction]
        public static ML.Result DeleteRest(int id)
        {
            ML.Result result = new ML.Result();
            var url = ConfigurationManager.AppSettings["UsuariosUrl"];
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.DeleteAsync("Delete?id=" + id);
                    postTask.Wait();

                    var resul = postTask.Result;
                    if (resul.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }










    } 
   
}