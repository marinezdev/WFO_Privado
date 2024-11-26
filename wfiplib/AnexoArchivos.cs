using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class AnexoArchivos
    {
        private string mAgenteClave = "";

        private bool mCheckInsumos = false;

        private List<String> mCheckDocumento = new List<String>();

        private string mTipoPersona = "";

        private string mIdTipoTramite = "";

        public string IdTipoTramite
        {
            get
            {
                return this.mIdTipoTramite;
            }
            set
            {
                this.mIdTipoTramite = value;
            }
        }

        public List<String> CheckDocumento
        {
            get
            {
                return this.mCheckDocumento;
            }
            set
            {
                this.mCheckDocumento =  value;
            }
        }
        public string TipoPersona
        {
            get
            {
                return this.mTipoPersona;
            }
            set
            {
                this.mTipoPersona = value;
            }
        }

        /*private string mCheckDocumento = "";

        public string CheckDocumento
        {
            get
            {
                return this.mCheckDocumento;
            }
            set
            {
                this.mCheckDocumento = value;
            }
        }*/

        public string AgenteClave
        {
            get
            {
                return this.mAgenteClave;
            }
            set
            {
                this.mAgenteClave = value;
            }

        }
        public bool CheckInsumos
        {
            get
            {
                return this.mCheckInsumos;
            }
            set
            {
                this.mCheckInsumos = value;
            }
        }

        
    }
}
