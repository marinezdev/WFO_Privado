using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class Checks
    {
        private int mIdDocCheck = 0;

        private int mIdTramite = 0;

        private int mIdDocRequerido = 0;

        public int IdDocCheck
        {
            get
            {
                return this.mIdDocCheck;
            }
            set
            {
                this.mIdDocCheck = value;
            }
        }

        public int IdTramite
        {
            get
            {
                return this.mIdTramite;
            }
            set
            {
                this.mIdTramite = value;
            }
        }

        public int IdDocRequerido
        {
            get
            {
                return this.mIdDocRequerido;
            }
            set
            {
                this.mIdDocRequerido = value;
            }
        }

    }
}
