using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class Concentrado
    {
        private credencial _credencial;
        private tramiteP _tramite;
        private TipoTramite _tipotramite;
        private List<ConfiguracionGeneralEntity> _configuracionGeneral;
        private mesa _mesa;
        private tramiteMesa _tramitemesa;
        private Promotoria _promotoria;
        private List<PermisosEntity> _permisos;
        private ArchivosDependenciasAsignadosPropiedades _archivosdependenciasasignados;

        private string _Token;

        /* Token de Autentificacion */
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        public credencial Credencial
        {
            get { return _credencial; }
            set { _credencial = value; }
        }

        public tramiteP Tramite
        {
            get { return _tramite; }
            set { _tramite = value; }
        }

        public TipoTramite TipoTramite
        {
            get { return _tipotramite; }
            set { _tipotramite = value; }
        }

        public List<ConfiguracionGeneralEntity> ConfiguracionGeneral
        {
            get { return _configuracionGeneral; }
            set { _configuracionGeneral = value; }
        }

        public mesa Mesa
        {
            get { return _mesa; }
            set { _mesa = value; }
        }

        public tramiteMesa TramiteMesa
        {
            get { return _tramitemesa; }
            set { _tramitemesa = value; }
        }

        public Promotoria Promotoria
        {
            get { return _promotoria; }
            set { _promotoria = value; }
        }

        public List<PermisosEntity> Permisos
        {
            get { return _permisos; }
            set { _permisos = value; }
        }

        public ArchivosDependenciasAsignadosPropiedades ArchivosDependenciasAsignados
        {
            get { return _archivosdependenciasasignados; }
            set { _archivosdependenciasasignados = value; }
        }

        public void Inicializar()
        {
            _credencial = new credencial();
            _tramite = new tramiteP();
            _tipotramite = new TipoTramite();
            _configuracionGeneral = new List<ConfiguracionGeneralEntity>();
            _mesa = new mesa();
            _tramitemesa = new tramiteMesa();
            _promotoria = new Promotoria();
            _permisos = new List<PermisosEntity>();
            _archivosdependenciasasignados = new ArchivosDependenciasAsignadosPropiedades();
            _Token = Token;
        }
    }
}
