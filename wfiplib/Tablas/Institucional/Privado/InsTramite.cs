using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas.Institucional.Privado
{
    public class InsTramite
    {
        BaseDeDatos b = new BaseDeDatos();

        public void TramiteAgregar(tramiteP tramite)
        {
            string consulta = "INSERT INTO tramite (Id, IdTipoTramite, FechaRegistro," +
                "Estado, " +
                "IdPromotoria, " +
                "IdUsuario, " +
                "AgenteClave, " +
                "NumeroOrden, " +
                "FechaSolicitud, " +
                "TipoTramite" +
                ") " +
            "VALUES(" +
            "@id, " +
            "@idtipotramite, " +
            "@fecharegistro, " +
            "@estado, " +
            "@idpromotoria, " +
            "@idusuario, " +
            "@agenteclave, " +
            "@numeroorden, " +
            "@fechasolicitud, " +
            "@tipotramite" +
            ")";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", tramite.Id, SqlDbType.Int);
            b.AddParameter("@idtipotramite", (tramite.IdTipoTramite), SqlDbType.Int); 
            b.AddParameter("@fecharegistro", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), SqlDbType.DateTime);
            b.AddParameter("@estado", (E_Estado)tramite.Estado, SqlDbType.Int); 
            b.AddParameter("@idpromotoria", tramite.IdPromotoria, SqlDbType.Int);
            b.AddParameter("@idusuario", tramite.IdUsuario, SqlDbType.Int);
            b.AddParameter("@agenteclave", tramite.AgenteClave, SqlDbType.Int);
            b.AddParameter("@numeroorden", tramite.NumeroOrden.ToString(), SqlDbType.VarChar, 50);
            b.AddParameter("@fechasolicitud", tramite.FechaSolicitud.ToString(), SqlDbType.VarChar, 50);
            b.AddParameter("@tipotramite", tramite.TipoTramite.ToString(), SqlDbType.VarChar, 50);
            b.ConnectionOpenToTransaction();
            b.InsertUpdateDeleteWithTransaction();
        }
    }
}
