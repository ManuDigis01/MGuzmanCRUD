using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsuario" in both code and config file together.
    [ServiceContract]
    public interface IUsuario
    {
        [OperationContract]
        SL_WCF.Result Add(ML.Usuario usuario);

        [OperationContract]
        SL_WCF.Result Update(ML.Usuario usuario);

        [OperationContract]
        SL_WCF.Result Delete(int id);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SL_WCF.Result GetAll(ML.Usuario usuarioBusqueda);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SL_WCF.Result GetById(int id);

    }
}
