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
    public class EstatusManager
    {
        #region obtenerEstatus
        public List<Estatus> obtenerEstatus()
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
        //#region Resgistrar Estatus

        //public string agregarEstatus(Estatus estatus, long idSesion)
        //{
        //    try
        //    {
        //        using (EnerCable.Models.DB.EnercableConexion db = new EnerCable.Models.DB.EnercableConexion())
        //        {
        //            int _x = db.guardares(persona.IdPersona, persona.Nombre, persona.Paterno, persona.Materno, persona.IdEstatus, idSesion);
        //        }
        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }
        //}
        //#endregion
    }
}