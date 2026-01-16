using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {


        public int IdUsuario { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime  FechaNacimiento { get; set; }
        [Required]
        public string SEXO { get; set; }
        [Required]
        public string Telefono { get; set; }
            public string Celular { get; set; }
        [Required]
        public bool Estatus { get; set; }
            public string CURP { get; set; }
            public byte[] Imagen { get; set; }

        
        public ML.Rol Rol { get; set; }
        
        public List<object> Usuarios {  get; set; }

      
        public ML.Direccion Direccion { get; set; }
        
    }
}

