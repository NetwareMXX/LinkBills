//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnerCable.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConfiguracionPolizas
    {
        public int IdConfiguracionPoliza { get; set; }
        public int IdTipoDePoliza { get; set; }
        public int Posicion { get; set; }
        public int Longitud { get; set; }
        public string TipoConfiguracion { get; set; }
        public string TipoExtraccion { get; set; }
        public System.DateTime FechaSistema { get; set; }
        public long IdSesion { get; set; }
    }
}
