using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Usuario.svc or Usuario.svc.cs at the Solution Explorer and start debugging.
    public class Usuario : IUsuario
    {
        public SL_WCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            SL_WCF.Result resultAdd = new SL_WCF.Result();
            resultAdd.Correct  = result.Correct;
            resultAdd.ErrorMessage = result.ErrorMessage;
            resultAdd.Object = result.Object;
            resultAdd.Objects = result.Objects;


            return resultAdd;
        }

     
        public SL_WCF.Result Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Update(usuario);
            SL_WCF.Result resultUpdate = new SL_WCF.Result();
            resultUpdate.Correct = result.Correct;
            resultUpdate.ErrorMessage = result.ErrorMessage;
            resultUpdate.Object = result.Object;
            resultUpdate.Objects = result.Objects;
            return resultUpdate;
        }

        public SL_WCF.Result Delete(int id) { 
        
            ML.Result result = BL.Usuario.DeleteEF(id);
            SL_WCF.Result resultDelete =  new SL_WCF.Result();
            resultDelete.Correct = result.Correct;
            resultDelete.ErrorMessage = result.ErrorMessage;
            resultDelete.Object = result.Object;    
            resultDelete.Objects = result.Objects;

            return resultDelete;
        
        } 

        public SL_WCF.Result GetAll(ML.Usuario usuarioBusqueda) {
            ML.Result result = BL.Usuario.GetAll(usuarioBusqueda);
            SL_WCF.Result resultGetAll = new SL_WCF.Result();
            resultGetAll.Correct = result.Correct;
            resultGetAll.ErrorMessage = result.ErrorMessage;
            resultGetAll.Object = result.Object;
            resultGetAll.Objects = result.Objects;

            return resultGetAll;
        }

        public SL_WCF.Result GetById(int id)
        {
            ML.Result result = BL.Usuario.GetById(id);
            SL_WCF.Result resultGetById = new SL_WCF.Result();
            resultGetById.Correct = result.Correct;
            resultGetById.ErrorMessage = result.ErrorMessage;
            resultGetById.Object = result.Object;
            resultGetById.Objects = result.Objects;

            return resultGetById;
        }

    }
}
