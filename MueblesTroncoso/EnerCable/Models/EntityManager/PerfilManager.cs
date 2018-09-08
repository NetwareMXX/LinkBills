using System;
using System.Linq;
using EnerCable.Models.DB;
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
namespace Troncoso.Models.EntityManager

{
    public class PerfilManager
    {
        #region obtenerPerfiles

        public List<Perfiles> obtenerPerfiles()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.Perfiles

                           select usu;


                return menu.OrderBy(x => x.Perfil).ToList();

            }
        }
        #endregion
        #region obtenerStatus

        public List<Estatus> obtenerStatus()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.Estatus
                           select usu;


                return menu.OrderBy(x => x.Estatus1).ToList();

            }
        }
        #endregion
        #region obtenerPermisosPorPerfil
        public string obtenerPermisosPorPeril(int _idPerfil)
        {
            StringBuilder _html = new StringBuilder();
            List<MiMenuNivel> _lista = new List<MiMenuNivel>();
            List<MiMenuNivel> _lista2 = new List<MiMenuNivel>();
            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {

                //Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                //Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _mimenu = (
                                    from cab in db.Cabeceras
                                    join mod in db.Modulos on cab.IdCabecera equals mod.IdCabecera
                                    join permond in db.PermisosPerfiles on mod.IdModulo equals permond.IdModulo
                                    join per in db.Perfiles on permond.IdPerfil equals per.IdPerfil
                                    where per.IdPerfil.Equals(_idPerfil)
                                    select new ViewModel.MiMenuNivel()
                                    {
                                        CveMenu = mod.IdModulo,
                                        Nombre = mod.Modulo,
                                        Grupo = cab.IdCabecera,
                                        Cabecera = cab.Nombre,
                                        EnRol = false,
                                        Nivel = per.IdPerfil
                                    });
                var _mimenu2 =
          (
         from
             cab in db.Cabeceras
         join s in db.Modulos on cab.IdCabecera equals s.IdCabecera


         select new ViewModel.MiMenuNivel()
         {
             CveMenu = s.IdModulo,
             Nombre = s.Modulo,
             Grupo = cab.IdCabecera,
             Cabecera = cab.Nombre,
             EnRol = false,
             Nivel = 0
         });

                _lista = _mimenu.ToList();
                _lista2 = _mimenu2.ToList();
                _lista = _lista.Where(x => x.Nivel == _idPerfil).ToList();

                _lista2 = _lista2.Where(x =>

                 !_lista.Select(z => z.CveMenu).Distinct().Contains(

                               x.CveMenu

                   )).ToList();

                _lista.ForEach(c => c.EnRol = true);

                _lista = _lista.Union(_lista2).ToList();
                _lista =
                            (
                                from x in _lista
                                orderby x.Cabecera, x.Nombre
                                select x
                            ).ToList();
                //_lista.OrderBy(m => m.Cabecera,mbox => m.nombre).ToList();
                //_lista = _lista.OrderBy(m => m.Nombre).ToList();
            }
            return this.PintarHtmlNiveles(_lista);
        }
        public string PintarHtmlNiveles(List<ViewModel.MiMenuNivel> _niveles)
        {
            StringBuilder _html = new StringBuilder();


            List<ViewModel.MiMenuNivel> _lista = _niveles.Where(x => x.EnRol == true).ToList();



            _html.Append("<table class='table'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("<th><input id='cchMain'  type='checkbox' " + (_niveles.Count == _lista.Count ? "checked='checked'" : "") + " onclick='checkTodo(this);' class='chkMain filled-in'><label for='cchMain'>Seleccionar Todos</label></th>");
            _html.Append("              <th>Menu</th>");
            //_html.Append("              <th>Permiso</th>");
            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (ViewModel.MiMenuNivel _nivel in _niveles)
            {
                _html.Append("<tr>");
                _html.Append("<td scope='row'><input id='chk" + _contado + "' valor='" + _nivel.CveMenu + "' type='checkbox' " + (_nivel.EnRol ? "checked='checked'" : "") + " class='chkPer filled-in'><label for='chk" + _contado + "'>" + _nivel.Nombre + "</label></td>");
                _html.Append("<td>" + _nivel.Cabecera.Replace("cab", "") + "</td>");
                //_html.Append("<td>" + _nivel.Nombre + "</td>");


                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();

        }
        #endregion


        #region obtenerVwPerfiles

        public List<vwPerfiles> obtenerVwPerfiles()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
            {
                var menu = from usu in db.vwPerfiles

                           select usu;


                return menu.OrderBy(x => x.Perfil).ToList();

            }
        }
        #endregion
        #region Resgistrar Perfil

        public string agregarPerfil(Perfiles perfil, long idSesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {
                    int _x = db.GuardarPerfil(perfil.IdPerfil, perfil.IdEstatus, perfil.Perfil, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region Guardar Permiso

        public string guardarPermiso(string idpermiso, int idperfil, long idsesion)
        {
            try
            {
                using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
                {

                    int _x = 0;
                    var _mimenu = (
                                   from  p in db.PermisosPerfiles
                                   where p.IdPerfil.Equals(idperfil)
                                   select p.IdPermisoPerfil
                                   );

                    List<int> _idpermisos = _mimenu.ToList();
                    foreach (int _id in _idpermisos)
                    {
                        _x = db.EliminarPermisoPerfil(_id, idsesion);
                    }

                    if (idpermiso.Trim() != string.Empty)
                    {
                        foreach (string _idpermiso in idpermiso.Split(','))
                        {
                            _x = db.GuardarPermisoPerfil(int.Parse(_idpermiso), idperfil, idsesion);
                        }
                    }

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