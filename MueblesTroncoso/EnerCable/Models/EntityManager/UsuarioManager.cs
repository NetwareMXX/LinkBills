using System;
using System.Linq;

using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Troncoso.Security;
using Troncoso.Models.ViewModel;
using System.Web;
using System.Web.Mvc;
using EnerCable.Models.DB;
namespace Troncoso.Models.EntityManager
{
    public class UsuarioManager
    {
        #region obtenerUsuarios

        public List<vwUsuarios> obtenerUsuarios()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.vwUsuarios
                           select usu;


                List<vwUsuarios> _usuario = menu.OrderBy(x => x.Nombre).ToList();
                _usuario.ForEach(x => { x.Password = new Encriptacion().Desencriptar(x.Password); });
                return _usuario;

            }
        }
        #endregion
        #region Resgistrar Usuario

        public string agregarUsuario(Usuarios usuario, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarUsuario(usuario.IdUsuario, usuario.Usuario, new Encriptacion().Encriptar(usuario.Password), usuario.IdPerfil, usuario.Nombre, usuario.Paterno, usuario.Materno == null ? "" : usuario.Materno, usuario.Correo, usuario.Telefono == null ? "" : usuario.Telefono, usuario.IdEstatus, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
    }
}