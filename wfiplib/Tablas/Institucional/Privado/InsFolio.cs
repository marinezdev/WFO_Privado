using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas.Institucional.Privado
{
    public class InsFolio
    {
        BaseDeDatos d = new BaseDeDatos();

        public int FolioAgregar(wfiplib.tramiteP tramite)
        {
            string abreviatura = "IN";
            string consulta = "INSERT INTO Folio(IdTramite, TramitePP, TipoTramite, IdPromotoria, Folio, FolioCompuesto) " +
            "VALUES(" +
            "@id, " +
            "@tipotramite, " +
            "@abreviatura, " +
            "@idpromotoria, " +
            "(SELECT ISNULL(MAX(Folio),0) + 1 FROM Folio WHERE TramitePP=@tipotramite AND TipoTramite=@abreviatura), " +
            "@abreviatura + " +
            "FORMAT(GETDATE(),'yyMMdd') + " +
            "RIGHT('000' + " +
            "Ltrim(Rtrim(@idpromotoria)),4) + " +
            "RIGHT('00000000' + " +
            "Ltrim(Rtrim(ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE[TramitePP] = @tipotramite AND [TipoTramite] = @abreviatura ),0) + 1)),8) " +
            ")";
            d.ExecuteCommandQuery(consulta);
            d.AddParameter("@id", tramite.Id, SqlDbType.Int);
            d.AddParameter("@tipotramite", tramite.TipoTramite, SqlDbType.VarChar, 50);
            d.AddParameter("@abreviatura", abreviatura, SqlDbType.VarChar, 2);
            d.AddParameter("@idpromotoria", tramite.IdPromotoria, SqlDbType.VarChar, 10);
            return d.InsertUpdateDeleteWithTransaction();
        }

        public string FlioAgregarPrueba()
        {
            string consulta = "INSERT INTO Folio(IdTramite, TramitePP, TipoTramite, IdPromotoria, Folio, FolioCompuesto) " +
            "VALUES(" +
            "111, " +
            "1, " +
            "'IN', " +
            "363, " +
            "(SELECT TOP 1 Folio FROM Folio WHERE TramitePP='INSTITUCIONAL PRIVADO' AND TipoTramite='IN' ORDER BY Folio DESC) + 1, " +
            "'IN' + FORMAT(GETDATE(),'yyMMdd') + RIGHT('000' + Ltrim(Rtrim(363)),4) + RIGHT('00000000' + Ltrim(Rtrim((SELECT TOP 1 Folio FROM Folio WHERE TramitePP='INSTITUCIONAL PRIVADO' AND TipoTramite='IN' ORDER BY Folio DESC)+1)),8) " +
            ") " +
            "SELECT FolioCompuesto FROM Folio WHERE IdFolio=@@Identity "; 
            d.ExecuteCommandQuery(consulta);
            return d.SelectWithReturnValuePrueba();
        }
    }
}
