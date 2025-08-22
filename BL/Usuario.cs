using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        private readonly UserAngularDbContext _context;

        public Usuario(UserAngularDbContext context) => _context = context;


        public ML.Result Add(ML.Usuario usuario) {
            
            var result = new ML.Result();

            try
            {

                var img = new SqlParameter("@Imagen", System.Data.SqlDbType.VarBinary);
                if (usuario.ImagenBytes != null)
                {
                    img.Value = usuario.ImagenBytes;
                }
                else
                {
                    img.Value = DBNull.Value;
                }


                int filasAfectadas = _context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}',@Imagen", img);

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else { 
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un problema al agregar Usuario";
                }

            }
            catch (Exception ex) {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }


            return result;
        
        }


        public ML.Result GetAll() {

            var result = new ML.Result()
            {
                Objects = new List<object>()
            };

            try {

                var query = _context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();

                if (query != null) {

                    foreach (var item in query) { 
                        
                        var usuario = new ML.Usuario();
                        usuario.IdUsuario = item.IdUsuario;
                        usuario.Nombre = item.Nombre;
                        usuario.ApellidoPaterno = item.ApellidoPaterno;
                        usuario.Email = item.Email;
                        usuario.Password = item.Password;
                        usuario.FechaNacimiento = Convert.ToString(item.FechaNacimiento);
                        usuario.ImagenBytes = item.Imagen;


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
        


    }
}
