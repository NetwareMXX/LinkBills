﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EnercableConexion : DbContext
    {
        public EnercableConexion()
            : base("name=EnercableConexion")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bitacoras> Bitacoras { get; set; }
        public virtual DbSet<Cabeceras> Cabeceras { get; set; }
        public virtual DbSet<ConfiguracionOrdenSer> ConfiguracionOrdenSer { get; set; }
        public virtual DbSet<ConfiguracionPolizas> ConfiguracionPolizas { get; set; }
        public virtual DbSet<CorreccionMovimientos> CorreccionMovimientos { get; set; }
        public virtual DbSet<CuentasCostos> CuentasCostos { get; set; }
        public virtual DbSet<CuentasVentas> CuentasVentas { get; set; }
        public virtual DbSet<Estatus> Estatus { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<PermisosPerfiles> PermisosPerfiles { get; set; }
        public virtual DbSet<Sesiones> Sesiones { get; set; }
        public virtual DbSet<Tablas> Tablas { get; set; }
        public virtual DbSet<TipoCuentas> TipoCuentas { get; set; }
        public virtual DbSet<TipoDocumentos> TipoDocumentos { get; set; }
        public virtual DbSet<TiposDePolizas> TiposDePolizas { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<MovimientosCostos> MovimientosCostos { get; set; }
        public virtual DbSet<MovimientosVentas> MovimientosVentas { get; set; }
        public virtual DbSet<vwPerfiles> vwPerfiles { get; set; }
        public virtual DbSet<vwUsuarios> vwUsuarios { get; set; }
        public virtual DbSet<vwCuentasCostos> vwCuentasCostos { get; set; }
        public virtual DbSet<vwCuentasVentas> vwCuentasVentas { get; set; }
        public virtual DbSet<vwConfiguracionPolizas> vwConfiguracionPolizas { get; set; }
    
        [DbFunction("EnercableConexion", "DividirCadena")]
        public virtual IQueryable<DividirCadena_Result> DividirCadena(string cadena, string divisor)
        {
            var cadenaParameter = cadena != null ?
                new ObjectParameter("Cadena", cadena) :
                new ObjectParameter("Cadena", typeof(string));
    
            var divisorParameter = divisor != null ?
                new ObjectParameter("Divisor", divisor) :
                new ObjectParameter("Divisor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<DividirCadena_Result>("[EnercableConexion].[DividirCadena](@Cadena, @Divisor)", cadenaParameter, divisorParameter);
        }
    
        public virtual int BuscarFacturasEnOtrasPolizasPorSerie(string idMovimientosCostos, string tipoDePolizas, Nullable<int> idUsuario, Nullable<int> idTabla)
        {
            var idMovimientosCostosParameter = idMovimientosCostos != null ?
                new ObjectParameter("IdMovimientosCostos", idMovimientosCostos) :
                new ObjectParameter("IdMovimientosCostos", typeof(string));
    
            var tipoDePolizasParameter = tipoDePolizas != null ?
                new ObjectParameter("TipoDePolizas", tipoDePolizas) :
                new ObjectParameter("TipoDePolizas", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var idTablaParameter = idTabla.HasValue ?
                new ObjectParameter("IdTabla", idTabla) :
                new ObjectParameter("IdTabla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuscarFacturasEnOtrasPolizasPorSerie", idMovimientosCostosParameter, tipoDePolizasParameter, idUsuarioParameter, idTablaParameter);
        }
    
        public virtual int BuscarFacturasEnOtrasPolizasPorSerieVentas(string idMovimientosVentas, string tipoDePolizas, Nullable<int> idUsuario, Nullable<int> idTabla)
        {
            var idMovimientosVentasParameter = idMovimientosVentas != null ?
                new ObjectParameter("IdMovimientosVentas", idMovimientosVentas) :
                new ObjectParameter("IdMovimientosVentas", typeof(string));
    
            var tipoDePolizasParameter = tipoDePolizas != null ?
                new ObjectParameter("TipoDePolizas", tipoDePolizas) :
                new ObjectParameter("TipoDePolizas", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var idTablaParameter = idTabla.HasValue ?
                new ObjectParameter("IdTabla", idTabla) :
                new ObjectParameter("IdTabla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuscarFacturasEnOtrasPolizasPorSerieVentas", idMovimientosVentasParameter, tipoDePolizasParameter, idUsuarioParameter, idTablaParameter);
        }
    
        public virtual int BuscarSerieEnOtrasPolizasPorFactura(string idMovimientosCostos, string tipoDePolizas, Nullable<int> idUsuario, Nullable<int> idTabla)
        {
            var idMovimientosCostosParameter = idMovimientosCostos != null ?
                new ObjectParameter("IdMovimientosCostos", idMovimientosCostos) :
                new ObjectParameter("IdMovimientosCostos", typeof(string));
    
            var tipoDePolizasParameter = tipoDePolizas != null ?
                new ObjectParameter("TipoDePolizas", tipoDePolizas) :
                new ObjectParameter("TipoDePolizas", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var idTablaParameter = idTabla.HasValue ?
                new ObjectParameter("IdTabla", idTabla) :
                new ObjectParameter("IdTabla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuscarSerieEnOtrasPolizasPorFactura", idMovimientosCostosParameter, tipoDePolizasParameter, idUsuarioParameter, idTablaParameter);
        }
    
        public virtual int BuscarSerieEnOtrasPolizasPorFacturaVentas(string idMovimientosVentas, string tipoDePolizas, Nullable<int> idUsuario, Nullable<int> idTabla)
        {
            var idMovimientosVentasParameter = idMovimientosVentas != null ?
                new ObjectParameter("IdMovimientosVentas", idMovimientosVentas) :
                new ObjectParameter("IdMovimientosVentas", typeof(string));
    
            var tipoDePolizasParameter = tipoDePolizas != null ?
                new ObjectParameter("TipoDePolizas", tipoDePolizas) :
                new ObjectParameter("TipoDePolizas", typeof(string));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var idTablaParameter = idTabla.HasValue ?
                new ObjectParameter("IdTabla", idTabla) :
                new ObjectParameter("IdTabla", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuscarSerieEnOtrasPolizasPorFacturaVentas", idMovimientosVentasParameter, tipoDePolizasParameter, idUsuarioParameter, idTablaParameter);
        }
    
        public virtual ObjectResult<CrearSesion_Result> CrearSesion(string usuario, string contraseña, string ip)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            var ipParameter = ip != null ?
                new ObjectParameter("Ip", ip) :
                new ObjectParameter("Ip", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CrearSesion_Result>("CrearSesion", usuarioParameter, contraseñaParameter, ipParameter);
        }
    
        public virtual int EliminarPermisoPerfil(Nullable<int> idPermisoPerfil, Nullable<long> idSesion)
        {
            var idPermisoPerfilParameter = idPermisoPerfil.HasValue ?
                new ObjectParameter("IdPermisoPerfil", idPermisoPerfil) :
                new ObjectParameter("IdPermisoPerfil", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarPermisoPerfil", idPermisoPerfilParameter, idSesionParameter);
        }
    
        public virtual int GenerarCostos(Nullable<int> idMes, Nullable<int> idUsuario)
        {
            var idMesParameter = idMes.HasValue ?
                new ObjectParameter("IdMes", idMes) :
                new ObjectParameter("IdMes", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerarCostos", idMesParameter, idUsuarioParameter);
        }
    
        public virtual int GenerarVentas(Nullable<int> idMes, Nullable<int> idUsuario)
        {
            var idMesParameter = idMes.HasValue ?
                new ObjectParameter("IdMes", idMes) :
                new ObjectParameter("IdMes", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerarVentas", idMesParameter, idUsuarioParameter);
        }
    
        public virtual int GuardarBitacora(string accion, Nullable<long> idSesion)
        {
            var accionParameter = accion != null ?
                new ObjectParameter("Accion", accion) :
                new ObjectParameter("Accion", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarBitacora", accionParameter, idSesionParameter);
        }
    
        public virtual int GuardarConfiguracionOrdenSer(Nullable<int> longitudSerieEnSer_Orden, Nullable<long> idSesion)
        {
            var longitudSerieEnSer_OrdenParameter = longitudSerieEnSer_Orden.HasValue ?
                new ObjectParameter("LongitudSerieEnSer_Orden", longitudSerieEnSer_Orden) :
                new ObjectParameter("LongitudSerieEnSer_Orden", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarConfiguracionOrdenSer", longitudSerieEnSer_OrdenParameter, idSesionParameter);
        }
    
        public virtual int GuardarConfiguracionPoliza(Nullable<int> idConfiguracionPoliza, Nullable<int> idTipoDePoliza, Nullable<int> posicion, Nullable<int> longitud, string tipoConfiguracion, string tipoExtraccion, Nullable<long> idSesion)
        {
            var idConfiguracionPolizaParameter = idConfiguracionPoliza.HasValue ?
                new ObjectParameter("IdConfiguracionPoliza", idConfiguracionPoliza) :
                new ObjectParameter("IdConfiguracionPoliza", typeof(int));
    
            var idTipoDePolizaParameter = idTipoDePoliza.HasValue ?
                new ObjectParameter("IdTipoDePoliza", idTipoDePoliza) :
                new ObjectParameter("IdTipoDePoliza", typeof(int));
    
            var posicionParameter = posicion.HasValue ?
                new ObjectParameter("Posicion", posicion) :
                new ObjectParameter("Posicion", typeof(int));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("Longitud", longitud) :
                new ObjectParameter("Longitud", typeof(int));
    
            var tipoConfiguracionParameter = tipoConfiguracion != null ?
                new ObjectParameter("TipoConfiguracion", tipoConfiguracion) :
                new ObjectParameter("TipoConfiguracion", typeof(string));
    
            var tipoExtraccionParameter = tipoExtraccion != null ?
                new ObjectParameter("TipoExtraccion", tipoExtraccion) :
                new ObjectParameter("TipoExtraccion", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarConfiguracionPoliza", idConfiguracionPolizaParameter, idTipoDePolizaParameter, posicionParameter, longitudParameter, tipoConfiguracionParameter, tipoExtraccionParameter, idSesionParameter);
        }
    
        public virtual int GuardarPerfil(Nullable<int> idPerfil, Nullable<int> idEstatus, string perfil, Nullable<long> idSesion)
        {
            var idPerfilParameter = idPerfil.HasValue ?
                new ObjectParameter("IdPerfil", idPerfil) :
                new ObjectParameter("IdPerfil", typeof(int));
    
            var idEstatusParameter = idEstatus.HasValue ?
                new ObjectParameter("IdEstatus", idEstatus) :
                new ObjectParameter("IdEstatus", typeof(int));
    
            var perfilParameter = perfil != null ?
                new ObjectParameter("Perfil", perfil) :
                new ObjectParameter("Perfil", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarPerfil", idPerfilParameter, idEstatusParameter, perfilParameter, idSesionParameter);
        }
    
        public virtual int GuardarPermisoPerfil(Nullable<int> idModulo, Nullable<int> idPerfil, Nullable<long> idSesion)
        {
            var idModuloParameter = idModulo.HasValue ?
                new ObjectParameter("IdModulo", idModulo) :
                new ObjectParameter("IdModulo", typeof(int));
    
            var idPerfilParameter = idPerfil.HasValue ?
                new ObjectParameter("IdPerfil", idPerfil) :
                new ObjectParameter("IdPerfil", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarPermisoPerfil", idModuloParameter, idPerfilParameter, idSesionParameter);
        }
    
        public virtual int GuardarUsuario(Nullable<int> idUsuario, string usuario, string password, Nullable<int> idPerfil, string nombre, string paterno, string materno, string correo, string telefono, Nullable<int> idEstatus, Nullable<long> idSesion)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var idPerfilParameter = idPerfil.HasValue ?
                new ObjectParameter("IdPerfil", idPerfil) :
                new ObjectParameter("IdPerfil", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var paternoParameter = paterno != null ?
                new ObjectParameter("Paterno", paterno) :
                new ObjectParameter("Paterno", typeof(string));
    
            var maternoParameter = materno != null ?
                new ObjectParameter("Materno", materno) :
                new ObjectParameter("Materno", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var idEstatusParameter = idEstatus.HasValue ?
                new ObjectParameter("IdEstatus", idEstatus) :
                new ObjectParameter("IdEstatus", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarUsuario", idUsuarioParameter, usuarioParameter, passwordParameter, idPerfilParameter, nombreParameter, paternoParameter, maternoParameter, correoParameter, telefonoParameter, idEstatusParameter, idSesionParameter);
        }
    
        public virtual int ProcesarLibro(Nullable<int> idMes, Nullable<int> idUsuario)
        {
            var idMesParameter = idMes.HasValue ?
                new ObjectParameter("IdMes", idMes) :
                new ObjectParameter("IdMes", typeof(int));
    
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ProcesarLibro", idMesParameter, idUsuarioParameter);
        }
    
        public virtual int EliminarCuentasCostos(Nullable<int> idCuentaCosto, Nullable<long> idSesion)
        {
            var idCuentaCostoParameter = idCuentaCosto.HasValue ?
                new ObjectParameter("IdCuentaCosto", idCuentaCosto) :
                new ObjectParameter("IdCuentaCosto", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarCuentasCostos", idCuentaCostoParameter, idSesionParameter);
        }
    
        public virtual int EliminarCuentasVentas(Nullable<int> idCuentaVenta, Nullable<long> idSesion)
        {
            var idCuentaVentaParameter = idCuentaVenta.HasValue ?
                new ObjectParameter("IdCuentaVenta", idCuentaVenta) :
                new ObjectParameter("IdCuentaVenta", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarCuentasVentas", idCuentaVentaParameter, idSesionParameter);
        }
    
        public virtual int GuardarConfiguracionPolizas(Nullable<int> idConfiguracionPoliza, Nullable<int> idTipoDePoliza, Nullable<int> posicion, Nullable<int> longitud, string tipoConfiguracion, string tipoExtraccion, Nullable<long> idSesion)
        {
            var idConfiguracionPolizaParameter = idConfiguracionPoliza.HasValue ?
                new ObjectParameter("IdConfiguracionPoliza", idConfiguracionPoliza) :
                new ObjectParameter("IdConfiguracionPoliza", typeof(int));
    
            var idTipoDePolizaParameter = idTipoDePoliza.HasValue ?
                new ObjectParameter("IdTipoDePoliza", idTipoDePoliza) :
                new ObjectParameter("IdTipoDePoliza", typeof(int));
    
            var posicionParameter = posicion.HasValue ?
                new ObjectParameter("Posicion", posicion) :
                new ObjectParameter("Posicion", typeof(int));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("Longitud", longitud) :
                new ObjectParameter("Longitud", typeof(int));
    
            var tipoConfiguracionParameter = tipoConfiguracion != null ?
                new ObjectParameter("TipoConfiguracion", tipoConfiguracion) :
                new ObjectParameter("TipoConfiguracion", typeof(string));
    
            var tipoExtraccionParameter = tipoExtraccion != null ?
                new ObjectParameter("TipoExtraccion", tipoExtraccion) :
                new ObjectParameter("TipoExtraccion", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarConfiguracionPolizas", idConfiguracionPolizaParameter, idTipoDePolizaParameter, posicionParameter, longitudParameter, tipoConfiguracionParameter, tipoExtraccionParameter, idSesionParameter);
        }
    
        public virtual int GuardarCuentasCostos(Nullable<int> idCuentaCosto, string cuenta, string nombre, Nullable<int> idTipoCuenta, Nullable<long> idSesion)
        {
            var idCuentaCostoParameter = idCuentaCosto.HasValue ?
                new ObjectParameter("IdCuentaCosto", idCuentaCosto) :
                new ObjectParameter("IdCuentaCosto", typeof(int));
    
            var cuentaParameter = cuenta != null ?
                new ObjectParameter("Cuenta", cuenta) :
                new ObjectParameter("Cuenta", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var idTipoCuentaParameter = idTipoCuenta.HasValue ?
                new ObjectParameter("IdTipoCuenta", idTipoCuenta) :
                new ObjectParameter("IdTipoCuenta", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarCuentasCostos", idCuentaCostoParameter, cuentaParameter, nombreParameter, idTipoCuentaParameter, idSesionParameter);
        }
    
        public virtual int GuardarCuentasVentas(Nullable<int> idCuentaVenta, string cuenta, string nombre, Nullable<int> idTipoCuenta, Nullable<long> idSesion)
        {
            var idCuentaVentaParameter = idCuentaVenta.HasValue ?
                new ObjectParameter("IdCuentaVenta", idCuentaVenta) :
                new ObjectParameter("IdCuentaVenta", typeof(int));
    
            var cuentaParameter = cuenta != null ?
                new ObjectParameter("Cuenta", cuenta) :
                new ObjectParameter("Cuenta", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var idTipoCuentaParameter = idTipoCuenta.HasValue ?
                new ObjectParameter("IdTipoCuenta", idTipoCuenta) :
                new ObjectParameter("IdTipoCuenta", typeof(int));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarCuentasVentas", idCuentaVentaParameter, cuentaParameter, nombreParameter, idTipoCuentaParameter, idSesionParameter);
        }
    
        public virtual int GuardarTablas(Nullable<int> idTabla, string tabla, Nullable<int> ejercicio, Nullable<bool> activo, string tablaNCRBON, Nullable<long> idSesion)
        {
            var idTablaParameter = idTabla.HasValue ?
                new ObjectParameter("IdTabla", idTabla) :
                new ObjectParameter("IdTabla", typeof(int));
    
            var tablaParameter = tabla != null ?
                new ObjectParameter("Tabla", tabla) :
                new ObjectParameter("Tabla", typeof(string));
    
            var ejercicioParameter = ejercicio.HasValue ?
                new ObjectParameter("Ejercicio", ejercicio) :
                new ObjectParameter("Ejercicio", typeof(int));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("Activo", activo) :
                new ObjectParameter("Activo", typeof(bool));
    
            var tablaNCRBONParameter = tablaNCRBON != null ?
                new ObjectParameter("TablaNCRBON", tablaNCRBON) :
                new ObjectParameter("TablaNCRBON", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarTablas", idTablaParameter, tablaParameter, ejercicioParameter, activoParameter, tablaNCRBONParameter, idSesionParameter);
        }
    
        public virtual int GuardarTiposDePolizas(Nullable<int> idTipoDePoliza, string tipoDePoliza, string aplicaNombreCuenta, string aplicaCondicionNombreCuenta, Nullable<int> posicion, Nullable<int> longitud, string tipoExtraccion, string datoValidar, string cate, string aplicaFacturaEnSerie, Nullable<long> idSesion)
        {
            var idTipoDePolizaParameter = idTipoDePoliza.HasValue ?
                new ObjectParameter("IdTipoDePoliza", idTipoDePoliza) :
                new ObjectParameter("IdTipoDePoliza", typeof(int));
    
            var tipoDePolizaParameter = tipoDePoliza != null ?
                new ObjectParameter("TipoDePoliza", tipoDePoliza) :
                new ObjectParameter("TipoDePoliza", typeof(string));
    
            var aplicaNombreCuentaParameter = aplicaNombreCuenta != null ?
                new ObjectParameter("AplicaNombreCuenta", aplicaNombreCuenta) :
                new ObjectParameter("AplicaNombreCuenta", typeof(string));
    
            var aplicaCondicionNombreCuentaParameter = aplicaCondicionNombreCuenta != null ?
                new ObjectParameter("AplicaCondicionNombreCuenta", aplicaCondicionNombreCuenta) :
                new ObjectParameter("AplicaCondicionNombreCuenta", typeof(string));
    
            var posicionParameter = posicion.HasValue ?
                new ObjectParameter("Posicion", posicion) :
                new ObjectParameter("Posicion", typeof(int));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("Longitud", longitud) :
                new ObjectParameter("Longitud", typeof(int));
    
            var tipoExtraccionParameter = tipoExtraccion != null ?
                new ObjectParameter("TipoExtraccion", tipoExtraccion) :
                new ObjectParameter("TipoExtraccion", typeof(string));
    
            var datoValidarParameter = datoValidar != null ?
                new ObjectParameter("DatoValidar", datoValidar) :
                new ObjectParameter("DatoValidar", typeof(string));
    
            var cateParameter = cate != null ?
                new ObjectParameter("Cate", cate) :
                new ObjectParameter("Cate", typeof(string));
    
            var aplicaFacturaEnSerieParameter = aplicaFacturaEnSerie != null ?
                new ObjectParameter("AplicaFacturaEnSerie", aplicaFacturaEnSerie) :
                new ObjectParameter("AplicaFacturaEnSerie", typeof(string));
    
            var idSesionParameter = idSesion.HasValue ?
                new ObjectParameter("IdSesion", idSesion) :
                new ObjectParameter("IdSesion", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarTiposDePolizas", idTipoDePolizaParameter, tipoDePolizaParameter, aplicaNombreCuentaParameter, aplicaCondicionNombreCuentaParameter, posicionParameter, longitudParameter, tipoExtraccionParameter, datoValidarParameter, cateParameter, aplicaFacturaEnSerieParameter, idSesionParameter);
        }
    }
}
