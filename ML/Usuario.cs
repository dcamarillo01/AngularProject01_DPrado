using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Imagen { get; set; } // Aquí llega el Base64
        public byte[]? ImagenBytes { get; set; } // Aquí conviertes a byte[]
        public string? FechaNacimiento { get; set; }
        public List<object>? Usuarios { get; set; }

    }
}
