using System;
using System.Linq;
using Troncoso.Models;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Troncoso.Security;
using Troncoso.Models.ViewModel;
using EnerCable.Models.DB;
namespace Troncoso.Models.EntityManager
{
    public class SeguridadManager
    {
        #region Usuario

        #region IsLoginNameExist

        public bool IsLoginNameExist(string loginName)
        {
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                return db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName.ToLower()) && o.IdEstatus == _es.IdEstatus).Any();
            }
        }
        #endregion
        #region GetUserPassword
        public string GetUserPassword(string loginName)
        {
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {


                var user = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName.ToLower()));
                if (user.Any())
                    return user.FirstOrDefault().Password;
                else
                    return string.Empty;
            }
        }
        #endregion

        #region IsUserInRole
        public bool IsUserInRole(string loginName, string roleName)
        {
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {


                Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                if (SU != null)
                {
                    var roles = from mod in db.Modulos
                                join permod in db.PermisosPerfiles on mod.IdModulo equals permod.IdModulo
                                join per in db.Perfiles on permod.IdPerfil equals per.IdPerfil
                                join us in db.Usuarios on per.IdPerfil equals us.IdPerfil
                                where mod.Modulo.Equals(roleName) && us.Usuario.ToLower().Equals(SU.Usuario.ToLower())
                                select mod.Modulo;

                    if (roles != null)
                    {
                        return roles.Any();
                    }
                }

                return false;
            }
        }
        #endregion

        #region getMenu
        public string getMenu(string loginName, string menu, string subMenu)
        {
            StringBuilder _html = new StringBuilder();
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {

                Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _cabeceras = from cab in db.Cabeceras
                                 join mod in db.Modulos on cab.IdCabecera equals mod.IdCabecera
                                 join permond in db.PermisosPerfiles on mod.IdModulo equals permond.IdModulo
                                 join per in db.Perfiles on permond.IdPerfil equals per.IdPerfil
                                 where per.IdPerfil.Equals(SU.IdPerfil)
                                 select cab;

                List<Cabeceras> _lstCabeceras = _cabeceras.Distinct().OrderBy(x => x.Nombre).ToList();

                _html.Append("<ul class='list'>");
                _html.Append("  <li class='header'>MENU</li>");
                _html.Append("      <li " + (menu == "Home" ? " class='active'" : "") + ">");
                _html.Append("          <a href='" + AuthorizeRolesAttribute.Nodo() + "/Usuario/Index'>");
                _html.Append("              <i class='material-icons'>home</i>");
                _html.Append("              <span>Home</span>");
                _html.Append("          </a>");
                _html.Append("      </li>");

                foreach (Cabeceras _cab in _lstCabeceras)
                {
                    var _subMenus = from _mod in db.Modulos
                                    join _sub in db.PermisosPerfiles on _mod.IdModulo equals _sub.IdModulo
                                    join _per in db.Perfiles on _sub.IdPerfil equals _per.IdPerfil
                                    where _mod.IdCabecera.Equals(_cab.IdCabecera) && _per.IdPerfil.Equals(SU.IdPerfil)
                                    select _mod;

                    List<Modulos> _perfiles = _subMenus.OrderBy(x => x.Modulo).ToList();

                    _html.Append("      <li " + (menu == _cab.Nombre ? " class='active'" : "") + ">");
                    _html.Append("          <a href='javascript:void(0);' " + (_perfiles.Count > 0 ? "class='menu-toggle'" : "") + ">");
                    _html.Append("              <i class='material-icons'>" + _cab.Icono + "</i>");
                    _html.Append("              <span>" + _cab.Nombre + "</span>");
                    _html.Append("          </a>");
                    _html.Append(this.getSubMenu(_perfiles, subMenu));
                    _html.Append("      </li>");

                }
                _html.Append("      <li>");
                _html.Append("          <a href='" + AuthorizeRolesAttribute.Nodo() + "/Usuario/LogOut'>");
                _html.Append("              <i class='material-icons'>exit_to_app</i>");
                _html.Append("              <span>Cerrar Sesion</span>");
                _html.Append("          </a>");
                _html.Append("      </li>");

                _html.Append("</ul>");



            }
            return _html.ToString();
        }
        public string getSubMenu(List<Modulos> _perfiles, string submenu)
        {
            StringBuilder _html = new StringBuilder();
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                //join mod in db.Modulos on cab.IdCabecera equals mod.IdCabecera



                if (_perfiles.Count > 0) _html.Append("<ul class='ml-menu'>");
                foreach (Modulos _perfil in _perfiles)
                {
                    _html.Append("      <li " + (_perfil.Modulo == submenu ? " class='active'" : "") + ">");
                    _html.Append("            <a href ='" + AuthorizeRolesAttribute.Nodo() +  _perfil.Url + "'>" + _perfil.Modulo + "  </a>");
                    _html.Append("      </li>");

                }
                if (_perfiles.Count > 0) _html.Append("</ul>");
            }
            return _html.ToString();

        }
        #endregion

        #region getDataUsuario
        public void getDataUsuario(string usuario, ref string nombre, ref string correo)
        {
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(usuario))?.FirstOrDefault();
                if (SU != null)
                {
                    nombre = SU.Nombre + " " + SU.Paterno + " " + SU.Materno;
                    correo = SU.Correo;
                }

            }
        }
        public void getIdUsuario(string usuario, ref int IdUsuario)
        {
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(usuario))?.FirstOrDefault();
                if (SU != null)
                {
                    IdUsuario = SU.IdUsuario;
                }

            }
        }
        #endregion

        #region insertarSesion
        public ResultadoSesionView crearSesion(string usuario, string password, string ip)
        {
            ResultadoSesionView _resultado = new ResultadoSesionView();
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {


                //   var rutas = ((IObjectContextAdapter)context).ObjectContext.Translate<Cabeceras>(reader, "Rutas", System.Data.Entity.Core.Objects.MergeOption.AppendOnly).ToList();

                var output = db.CrearSesion(usuario, password, ip);

                foreach (var outp in output)
                {
                    _resultado.IdSesion = outp.IdSesion.Value;
                    _resultado.Resultado = outp.Resultado;
                }






            }
            return _resultado;
        }
        #endregion

        #endregion
    }
}