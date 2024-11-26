using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    /// <summary>
    /// Validaciones comunes para procedimientos de Institucional
    /// </summary>
    public class InstitucionalTablas
    {
        public string ContarCaracteres(string cadena)
        {
            string caracs = cadena.Length.ToString();
            return cadena + "-" + caracs;
        }

        public List<string> Dependencias()
        {
            List<string> Listado = new List<string>() {
            "ADMINISTRACION DEL PATRIMONIO DE LA BENEFICENCIA PUBLICA" ,
            "ADMINISTRACION FEDERAL DE SERVICIOS EDUCATIVOS EN EL DISTRITO FEDERAL" ,
            "AGENCIA DE SERVICIOS A LA COMERCIALIZACION Y DESARROLLO DE MERCADOS AGROPECUARIOS" ,
            "AGENCIA MEXICANA DE COOPERACION INTERNACIONAL PARA EL DESARROLLO" ,
            "AGREGADURIAS LEGALES, REGIONALES Y OFICINAS DE ENLACE" ,
            "ARCHIVO GENERAL DE LA NACION" ,
            "CAMINOS Y PUENTES FEDERALES DE INGRESOS Y SERVICIOS CONEXOS" ,
            "CENTRO DE EVALUACION Y CONTROL DE CONFIANZA" ,
            "CENTRO DE INVESTIGACION Y SEGURIDAD NACIONAL" ,
            "CENTRO DE PRODUCCION DE PROGRAMAS INFORMATIVOS Y ESPECIALES" ,
            "CENTRO FEDERAL DE PROTECCION A PERSONAS" ,
            "CENTRO NACIONAL DE EQUIDAD DE GENERO Y SALUD REPRODUCTIVA" ,
            "CENTRO NACIONAL DE EXCELENCIA TECNOLOGICA EN SALUD" ,
            "CENTRO NACIONAL DE LA TRANSFUSION SANGUINEA" ,
            "CENTRO NACIONAL DE METROLOGIA" ,
            "CENTRO NACIONAL DE PLANEACION, ANALISIS E INFORMACION PARA EL COMBATE A LA DELINCUENCIA" ,
            "CENTRO NACIONAL DE PREVENCION DE DESASTRES" ,
            "CENTRO NACIONAL DE PROGRAMAS PREVENTIVOS Y CONTROL DE ENFERMEDADES" ,
            "CENTRO NACIONAL DE TRASPLANTES" ,
            "CENTRO NACIONAL PARA LA PREVENCION Y EL CONTROL DE LAS ADICCIONES" ,
            "CENTRO NACIONAL PARA LA PREVENCION Y EL CONTROL DEL VIH/SIDA" ,
            "CENTRO NACIONAL PARA LA SALUD DE LA INFANCIA Y LA ADOLESCENCIA" ,
            "CENTRO REGIONAL DE ALTA ESPECIALIDAD DE CHIAPAS" ,
            "CENTROS DE INTEGRACION JUVENIL, A.C." ,
            "COLEGIO SUPERIOR AGROPECUARIO DEL ESTADO DE GUERRERO" ,
            "COMISION DE OPERACION Y FOMENTO DE ACTIVIDADES ACADEMICAS DEL INSTITUTO POLITECNICO NACIONAL" ,
            "COMISION EJECUTIVA DE ATENCION A VICTIMAS (ANTES PROVICTIMA)" ,
            "COMISION FEDERAL DE COMPETENCIA ECONOMICA (ANTES COMISION FEDERAL DE COMPETENCIA)" ,
            "COMISION FEDERAL DE MEJORA REGULATORIA" ,
            "COMISION FEDERAL PARA LA PROTECCION CONTRA RIESGOS SANITARIOS" ,
            "COMISION NACIONAL BANCARIA Y DE VALORES" ,
            "COMISION NACIONAL DE ACUACULTURA Y PESCA" ,
            "COMISION NACIONAL DE ARBITRAJE MEDICO" ,
            "COMISION NACIONAL DE AREAS NATURALES PROTEGIDAS" ,
            "COMISION NACIONAL DE BIOETICA" ,
            "COMISION NACIONAL DE CULTURA FISICA Y DEPORTE" ,
            "COMISION NACIONAL DE HIDROCARBUROS" ,
            "COMISION NACIONAL DE LAS ZONAS ARIDAS" ,
            "COMISION NACIONAL DE LIBROS DE TEXTO GRATUITOS" ,
            "COMISION NACIONAL DE LOS SALARIOS MINIMOS" ,
            "COMISION NACIONAL DE PROTECCION SOCIAL EN SALUD" ,
            "COMISION NACIONAL DE SEGURIDAD NUCLEAR Y SALVAGUARDIAS" ,
            "COMISION NACIONAL DE SEGUROS Y FIANZAS" ,
            "COMISION NACIONAL DE VIVIENDA" ,
            "COMISION NACIONAL DEL AGUA" ,
            "COMISION NACIONAL DEL SISTEMA DE AHORRO PARA EL RETIRO" ,
            "COMISION NACIONAL FORESTAL" ,
            "COMISION NACIONAL PARA EL DESARROLLO DE LOS PUEBLOS INDIGENAS" ,
            "COMISION NACIONAL PARA EL USO EFICIENTE DE LA ENERGIA" ,
            "COMISION NACIONAL PARA PREVENIR Y ERRADICAR LA VIOLENCIA CONTRA LAS MUJERES" ,
            "COMISION PARA LA SEGURIDAD Y EL DESARROLLO INTEGRAL EN EL ESTADO DE MICHOACAN" ,
            "COMISION REGULADORA DE ENERGIA" ,
            "COMITE NACIONAL MIXTO DE PROTECCION AL SALARIO" ,
            "COMITE NACIONAL PARA EL DESARROLLO SUSTENTABLE DE LA CAÑA DE AZUCAR" ,
            "CONSEJERIA JURIDICA DEL EJECUTIVO FEDERAL" ,
            "CONSEJO DE PROMOCION TURISTICA DE MEXICO, S.A. DE C.V." ,
            "CONSEJO NACIONAL DE EVALUACION DE LA POLITICA DE DESARROLLO SOCIAL" ,
            "CONSEJO NACIONAL PARA LA CULTURA Y LAS ARTES" ,
            "CONSEJO NACIONAL PARA PREVENIR LA DISCRIMINACION" ,
            "COORDINACION GENERAL DE LA COMISION MEXICANA DE AYUDA A REFUGIADOS" ,
            "COORDINACION NACIONAL ANTISECUESTRO" ,
            "CORPORACION DE SERVICIOS AL TURISTA ANGELES VERDES (CORPORACION ANGELES VERDES)" ,
            "DELEGACIONES" ,
            "DICONSA, S.A. DE C.V." ,
            "ESTUDIOS CHURUBUSCO AZTECA, S.A." ,
            "FIDEICOMISO DE FOMENTO MINERO" ,
            "FIDEICOMISO DE LOS SISTEMAS NORMALIZADO DE COMPETENCIA LABORAL Y DE CERTIFICACION DE COMPETENCIA LABORAL" ,
            "FIDEICOMISO DE RIESGO COMPARTIDO (*2)" ,
            "FIDEICOMISO FONDO NACIONAL DE HABITACIONES POPULARES" ,
            "FIDEICOMISO PARA LA CINETECA NACIONAL" ,
            "FINANCIERA NACIONAL DE DESARROLLO AGROPECUARIO, RURAL, FORESTAL Y PESQUERO (ANTES FINANCIERA RURAL)" ,
            "FONATUR CONSTRUCTORA, S.A. DE C.V." ,
            "FONATUR MANTENIMIENTO TURISTICO, S.A. DE C.V." ,
            "FONATUR OPERADORA PORTUARIA, S.A. DE C.V." ,
            "FONDO DE EMPRESAS EXPROPIADAS DEL SECTOR AZUCARERO" ,
            "FONDO NACIONAL DE FOMENTO AL TURISMO" ,
            "FONDO NACIONAL PARA EL FOMENTO DE LAS ARTESANIAS" ,
            "HOSPITAL GENERAL DE MEXICO" ,
            "HOSPITAL GENERAL 'DR. MANUEL GEA GONZALEZ'" ,
            "HOSPITAL INFANTIL DE MEXICO FEDERICO GOMEZ" ,
            "HOSPITAL JUAREZ DE MEXICO" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE CIUDAD VICTORIA 'BICENTENARIO 2010'" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE IXTAPALUCA (*4)" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE LA PENINSULA DE YUCATAN" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE OAXACA" ,
            "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DEL BAJIO" ,
            "INSTITUTO DE ADMINISTRACION Y AVALUOS DE BIENES NACIONALES" ,
            "INSTITUTO DE COMPETITIVIDAD TURISTICA (ANTES CENTRO DE ESTUDIOS SUPERIORES DE TURISMO)" ,
            "INSTITUTO DE FORMACION MINISTERIAL, POLICIAL Y PERICIAL" ,
            "INSTITUTO DE LOS MEXICANOS EN EL EXTERIOR" ,
            "INSTITUTO FEDERAL DE TELECOMUNICACIONES (ANTES COMISION FEDERAL DE TELECOMUNICACIONES)" ,
            "INSTITUTO MATIAS ROMERO" ,
            "INSTITUTO MEXICANO DE CINEMATOGRAFIA" ,
            "INSTITUTO MEXICANO DE TECNOLOGIA DEL AGUA" ,
            "INSTITUTO MEXICANO DEL TRANSPORTE" ,
            "INSTITUTO NACIONAL DE CANCEROLOGIA" ,
            "INSTITUTO NACIONAL DE CARDIOLOGIA IGNACIO CHAVEZ" ,
            "INSTITUTO NACIONAL DE CIENCIAS MEDICAS Y NUTRICION SALVADOR ZUBIRAN" ,
            "INSTITUTO NACIONAL DE CIENCIAS PENALES" ,
            "INSTITUTO NACIONAL DE DESARROLLO SOCIAL" ,
            "INSTITUTO NACIONAL DE ECOLOGIA Y CAMBIO CLIMATICO (ANTES INSTITUTO NACIONAL DE ECOLOGIA)" ,
            "INSTITUTO NACIONAL DE ENFERMEDADES RESPIRATORIAS ISMAEL COSIO VILLEGAS" ,
            "INSTITUTO NACIONAL DE ESTADISTICA Y GEOGRAFIA" ,
            "INSTITUTO NACIONAL DE ESTUDIOS HISTORICOS DE LAS REVOLUCIONES DE MEXICO" ,
            "INSTITUTO NACIONAL DE GERIATRIA (*3)" ,
            "INSTITUTO NACIONAL DE INVESTIGACIONES FORESTALES, AGRICOLAS Y PECUARIAS" ,
            "INSTITUTO NACIONAL DE LA ECONOMIA SOCIAL (ANTES COORDINACION NACIONAL DEL PROGRAMA NACIONAL DE APOYO PARA LAS EMPRESAS DE SOLIDARIDAD)" ,
            "INSTITUTO NACIONAL DE LA INFRAESTRUCTURA FISICA EDUCATIVA" ,
            "INSTITUTO NACIONAL DE LAS MUJERES" ,
            "INSTITUTO NACIONAL DE LAS PERSONAS ADULTAS MAYORES" ,
            "INSTITUTO NACIONAL DE LENGUAS INDIGENAS" ,
            "INSTITUTO NACIONAL DE MEDICINA GENOMICA (*1)" ,
            "INSTITUTO NACIONAL DE MIGRACION" ,
            "INSTITUTO NACIONAL DE NEUROLOGIA Y NEUROCIRUGIA MANUEL VELASCO SUAREZ" ,
            "INSTITUTO NACIONAL DE PEDIATRIA" ,
            "INSTITUTO NACIONAL DE PERINATOLOGIA ISIDRO ESPINOSA DE LOS REYES" ,
            "INSTITUTO NACIONAL DE PESCA" ,
            "INSTITUTO NACIONAL DE PSIQUIATRIA RAMON DE LA FUENTE MUÑIZ" ,
            "INSTITUTO NACIONAL DE REHABILITACION" ,
            "INSTITUTO NACIONAL DE SALUD PUBLICA" ,
            "INSTITUTO NACIONAL DE TRANSPARENCIA, ACCESO A LA INFORMACION Y PROTECCION DE DATOS PERSONALES",
            "INSTITUTO NACIONAL DEL DERECHO DE AUTOR" ,
            "INSTITUTO NACIONAL PARA EL DESARROLLO DE CAPACIDADES DEL SECTOR RURAL, A.C." ,
            "INSTITUTO NACIONAL PARA EL FEDERALISMO Y EL DESARROLLO MUNICIPAL" ,
            "INSTITUTO NACIONAL PARA LA EDUCACION DE LOS ADULTOS" ,
            "INSTITUTO NACIONAL PARA LA EVALUACION DE LA EDUCACION" ,
            "INSTITUTO POLITECNICO NACIONAL" ,
            "NOTIMEX, AGENCIA DE NOTICIAS DEL ESTADO MEXICANO" ,
            "PATRONATO DE OBRAS E INSTALACIONES DEL INSTITUTO POLITECNICO NACIONAL" ,
            "POLICIA FEDERAL" ,
            "PREVENCION Y READAPTACION SOCIAL" ,
            "PROCURADURIA AGRARIA" ,
            "PROCURADURIA DE LA DEFENSA DEL CONTRIBUYENTE" ,
            "PROCURADURIA FEDERAL DE LA DEFENSA DEL TRABAJO" ,
            "PROCURADURIA FEDERAL DE PROTECCION AL AMBIENTE" ,
            "PROCURADURIA FEDERAL DEL CONSUMIDOR" ,
            "PROCURADURIA GENERAL DE LA REPUBLICA" ,
            "PROMEXICO" ,
            "PROSPERA, PROGRAMA DE INCLUSION SOCIAL" ,
            "RADIO EDUCACION" ,
            "REGISTRO AGRARIO NACIONAL" ,
            "SECCION MEXICANA DE LA COMISION INTERNACIONAL DE LIMITES Y AGUAS ENTRE MEXICO Y ESTADOS UNIDOS" ,
            "SECCIONES MEXICANAS DE LAS COMISIONES INTERNACIONALES DE LIMITES Y AGUAS ENTRE MEXICO Y GUATEMALA, Y ENTRE MEXICO Y BELIZE" ,
            "SECRETARIA DE AGRICULTURA, GANADERIA, DESARROLLO RURAL, PESCA Y ALIMENTACION" ,
            "SECRETARIA DE COMUNICACIONES Y TRANSPORTES" ,
            "SECRETARIA DE DESARROLLO AGRARIO, TERRITORIAL Y URBANO" ,
            "SECRETARIA DE DESARROLLO SOCIAL" ,
            "SECRETARIA DE ECONOMIA" ,
            "SECRETARIA DE EDUCACION PUBLICA" ,
            "SECRETARIA DE ENERGIA" ,
            "SECRETARIA DE GOBERNACION" ,
            "SECRETARIA DE HACIENDA Y CREDITO PUBLICO" ,
            "SECRETARIA DE LA FUNCION PUBLICA" ,
            "SECRETARIA DE MARINA" ,
            "SECRETARIA DE MEDIO AMBIENTE Y RECURSOS NATURALES" ,
            "SECRETARIA DE RELACIONES EXTERIORES" ,
            "SECRETARIA DE SALUD" ,
            "SECRETARIA DE TURISMO" ,
            "SECRETARIA DEL TRABAJO Y PREVISION SOCIAL" ,
            "SECRETARIA GENERAL DEL CONSEJO NACIONAL DE POBLACION" ,
            "SECRETARIA TECNICA DE LA COMISION CALIFICADORA DE PUBLICACIONES Y REVISTAS ILUSTRADAS" ,
            "SECRETARIA TECNICA DEL CONSEJO DE COORDINACION PARA LA IMPLEMENTACION DEL SISTEMA DE JUSTICIA PENAL" ,
            "SECRETARIADO EJECUTIVO DEL SISTEMA NACIONAL DE SEGURIDAD PUBLICA" ,
            "SERVICIO DE ADMINISTRACION TRIBUTARIA" ,
            "SERVICIO DE INFORMACION AGROALIMENTARIA Y PESQUERA" ,
            "SERVICIO DE PROTECCION FEDERAL" ,
            "SERVICIO NACIONAL DE INSPECCION Y CERTIFICACION DE SEMILLAS" ,
            "SERVICIO NACIONAL DE SANIDAD, INOCUIDAD Y CALIDAD AGROALIMENTARIA" ,
            "SERVICIOS A LA NAVEGACION EN EL ESPACIO AEREO MEXICANO" ,
            "SERVICIOS DE ATENCION PSIQUIATRICA" ,
            "SISTEMA NACIONAL PARA EL DESARROLLO INTEGRAL DE LA FAMILIA" ,
            "SISTEMA PUBLICO DE RADIODIFUSION DEL ESTADO MEXICANO" ,
            "TALLERES GRAFICOS DE MEXICO" ,
            "TELEVISION METROPOLITANA, S.A. DE C.V." ,
            "TRIBUNAL FEDERAL DE CONCILIACION Y ARBITRAJE" ,
            "TRIBUNALES AGRARIOS" ,
            "UNIVERSIDAD PEDAGOGICA NACIONAL" ,
            "PRESIDENCIA DE LA REPUBLICA" };
            return Listado;
        }

        public List<string> Entidades()
        {
            List<string> Listado = new List<string>() { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32" };
            return Listado;
        }

        public List<string> NivelTabular()
        {
            List<string> Listado = new List<string>() { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "CS" };
            return Listado;
        }

        public List<string> TipoAsegurado()
        {
            List<string> Listado = new List<string>() { "T", "C", "H", "A", "S" };
            return Listado;
        }

        public List<string> SumaAseguradaBasica()
        {
            List<string> Listado = new List<string>() { "333", "295", "259", "222", "185", "148", "111", "74", "226.4" };
            return Listado;
        }

        public List<string> SumaAseguradaBasicaPotenciacion()
        {
            List<string> Listado = new List<string>() { "333", "295", "259", "222", "185", "148", "111", "74" };
            return Listado;
        }

        public List<string> SumaAseguradaPotenciada()
        {
            List<string> Listado = new List<string>() { "111", "148", "185", "222", "259", "295", "333", "444", "592", "740", "850", "1000", "34219" };
            return Listado;
        }

        public bool SumaAseguradaPorNivelTabular(string suma, string nivel)
        {
            if (nivel == "G" && suma == "333")
                return true;
            else if (nivel == "H" && suma == "295")
                return true;
            else if (nivel == "I" && suma == "295")
                return true;
            else if (nivel == "J" && suma == "295")
                return true;
            else if (nivel == "K" && suma == "259")
                return true;
            else if (nivel == "L" && suma == "222")
                return true;
            else if (nivel == "M" && suma == "185")
                return true;
            else if (nivel == "N" && suma == "148")
                return true;
            else if (nivel == "O" && suma == "111")
                return true;
            else if (nivel == "P" && suma == "74")
                return true;
            else if (nivel == "CS" && suma == "266.4")
                return true;
            else
                return false;
        }

        public List<string> ClavesCostos()
        {
            List<string> Listado = new List<string>() { "74", "111", "130", "148", "185", "222", "259", "266.4", "295", "333" };
            return Listado;
        }

        public List<NivelTabularEntity> NivelTabularParaLlenado()
        {
            return new List<NivelTabularEntity> {

            new NivelTabularEntity{IdNivelTabular="G",NivelTabular="Presidente de la RepUblica y Secretario de Estado" }
            ,new NivelTabularEntity{IdNivelTabular="H",NivelTabular="Subsecretario de estado"}
            ,new NivelTabularEntity{IdNivelTabular="I",NivelTabular="Oficial Mayor"}
            ,new NivelTabularEntity{IdNivelTabular="J",NivelTabular="Jefe de Unidad"}
            ,new NivelTabularEntity{IdNivelTabular="K",NivelTabular="Director General"}
            ,new NivelTabularEntity{IdNivelTabular="L",NivelTabular="Director General Adjunto"}
            ,new NivelTabularEntity{IdNivelTabular="M",NivelTabular="Director de Area"}
            ,new NivelTabularEntity{IdNivelTabular="N",NivelTabular="Subdirector de Area"}
            ,new NivelTabularEntity{IdNivelTabular="O",NivelTabular="Jefe de Departamento"}
            ,new NivelTabularEntity{IdNivelTabular="P",NivelTabular="Personal de Enlace"}
            ,new NivelTabularEntity{IdNivelTabular="S",NivelTabular="COnyuge SupErstite"}
            };
        }

        public bool Municipios(string entidad, string municipio)
        {
            if (entidad == "01" && municipio == "001")
                return true;
            if (entidad == "01" && municipio == "002")
                return true;
            if (entidad == "01" && municipio == "003")
                return true;
            if (entidad == "01" && municipio == "004")
                return true;
            if (entidad == "01" && municipio == "005")
                return true;
            if (entidad == "01" && municipio == "006")
                return true;
            if (entidad == "01" && municipio == "007")
                return true;
            if (entidad == "01" && municipio == "008")
                return true;
            if (entidad == "01" && municipio == "009")
                return true;
            if (entidad == "01" && municipio == "010")
                return true;
            if (entidad == "01" && municipio == "011")
                return true;
            if (entidad == "02" && municipio == "001")
                return true;
            if (entidad == "02" && municipio == "002")
                return true;
            if (entidad == "02" && municipio == "003")
                return true;
            if (entidad == "02" && municipio == "004")
                return true;
            if (entidad == "02" && municipio == "005")
                return true;
            if (entidad == "03" && municipio == "001")
                return true;
            if (entidad == "03" && municipio == "002")
                return true;
            if (entidad == "03" && municipio == "003")
                return true;
            if (entidad == "03" && municipio == "004")
                return true;
            if (entidad == "03" && municipio == "005")
                return true;
            if (entidad == "04" && municipio == "001")
                return true;
            if (entidad == "04" && municipio == "002")
                return true;
            if (entidad == "04" && municipio == "003")
                return true;
            if (entidad == "04" && municipio == "004")
                return true;
            if (entidad == "04" && municipio == "005")
                return true;
            if (entidad == "04" && municipio == "006")
                return true;
            if (entidad == "04" && municipio == "007")
                return true;
            if (entidad == "04" && municipio == "008")
                return true;
            if (entidad == "04" && municipio == "009")
                return true;
            if (entidad == "04" && municipio == "010")
                return true;
            if (entidad == "04" && municipio == "011")
                return true;
            if (entidad == "05" && municipio == "001")
                return true;
            if (entidad == "05" && municipio == "002")
                return true;
            if (entidad == "05" && municipio == "003")
                return true;
            if (entidad == "05" && municipio == "004")
                return true;
            if (entidad == "05" && municipio == "005")
                return true;
            if (entidad == "05" && municipio == "006")
                return true;
            if (entidad == "05" && municipio == "007")
                return true;
            if (entidad == "05" && municipio == "008")
                return true;
            if (entidad == "05" && municipio == "009")
                return true;
            if (entidad == "05" && municipio == "010")
                return true;
            if (entidad == "05" && municipio == "011")
                return true;
            if (entidad == "05" && municipio == "012")
                return true;
            if (entidad == "05" && municipio == "013")
                return true;
            if (entidad == "05" && municipio == "014")
                return true;
            if (entidad == "05" && municipio == "015")
                return true;
            if (entidad == "05" && municipio == "016")
                return true;
            if (entidad == "05" && municipio == "017")
                return true;
            if (entidad == "05" && municipio == "018")
                return true;
            if (entidad == "05" && municipio == "019")
                return true;
            if (entidad == "05" && municipio == "020")
                return true;
            if (entidad == "05" && municipio == "021")
                return true;
            if (entidad == "05" && municipio == "022")
                return true;
            if (entidad == "05" && municipio == "023")
                return true;
            if (entidad == "05" && municipio == "024")
                return true;
            if (entidad == "05" && municipio == "025")
                return true;
            if (entidad == "05" && municipio == "026")
                return true;
            if (entidad == "05" && municipio == "027")
                return true;
            if (entidad == "05" && municipio == "028")
                return true;
            if (entidad == "05" && municipio == "029")
                return true;
            if (entidad == "05" && municipio == "030")
                return true;
            if (entidad == "05" && municipio == "031")
                return true;
            if (entidad == "05" && municipio == "032")
                return true;
            if (entidad == "05" && municipio == "033")
                return true;
            if (entidad == "05" && municipio == "034")
                return true;
            if (entidad == "05" && municipio == "035")
                return true;
            if (entidad == "05" && municipio == "036")
                return true;
            if (entidad == "05" && municipio == "037")
                return true;
            if (entidad == "05" && municipio == "038")
                return true;
            if (entidad == "06" && municipio == "001")
                return true;
            if (entidad == "06" && municipio == "002")
                return true;
            if (entidad == "06" && municipio == "003")
                return true;
            if (entidad == "06" && municipio == "004")
                return true;
            if (entidad == "06" && municipio == "005")
                return true;
            if (entidad == "06" && municipio == "006")
                return true;
            if (entidad == "06" && municipio == "007")
                return true;
            if (entidad == "06" && municipio == "008")
                return true;
            if (entidad == "06" && municipio == "009")
                return true;
            if (entidad == "06" && municipio == "010")
                return true;
            if (entidad == "07" && municipio == "001")
                return true;
            if (entidad == "07" && municipio == "002")
                return true;
            if (entidad == "07" && municipio == "003")
                return true;
            if (entidad == "07" && municipio == "004")
                return true;
            if (entidad == "07" && municipio == "005")
                return true;
            if (entidad == "07" && municipio == "006")
                return true;
            if (entidad == "07" && municipio == "007")
                return true;
            if (entidad == "07" && municipio == "008")
                return true;
            if (entidad == "07" && municipio == "009")
                return true;
            if (entidad == "07" && municipio == "010")
                return true;
            if (entidad == "07" && municipio == "011")
                return true;
            if (entidad == "07" && municipio == "012")
                return true;
            if (entidad == "07" && municipio == "013")
                return true;
            if (entidad == "07" && municipio == "014")
                return true;
            if (entidad == "07" && municipio == "015")
                return true;
            if (entidad == "07" && municipio == "016")
                return true;
            if (entidad == "07" && municipio == "017")
                return true;
            if (entidad == "07" && municipio == "018")
                return true;
            if (entidad == "07" && municipio == "019")
                return true;
            if (entidad == "07" && municipio == "020")
                return true;
            if (entidad == "07" && municipio == "021")
                return true;
            if (entidad == "07" && municipio == "022")
                return true;
            if (entidad == "07" && municipio == "023")
                return true;
            if (entidad == "07" && municipio == "024")
                return true;
            if (entidad == "07" && municipio == "025")
                return true;
            if (entidad == "07" && municipio == "026")
                return true;
            if (entidad == "07" && municipio == "027")
                return true;
            if (entidad == "07" && municipio == "028")
                return true;
            if (entidad == "07" && municipio == "029")
                return true;
            if (entidad == "07" && municipio == "030")
                return true;
            if (entidad == "07" && municipio == "031")
                return true;
            if (entidad == "07" && municipio == "032")
                return true;
            if (entidad == "07" && municipio == "033")
                return true;
            if (entidad == "07" && municipio == "034")
                return true;
            if (entidad == "07" && municipio == "035")
                return true;
            if (entidad == "07" && municipio == "036")
                return true;
            if (entidad == "07" && municipio == "037")
                return true;
            if (entidad == "07" && municipio == "038")
                return true;
            if (entidad == "07" && municipio == "039")
                return true;
            if (entidad == "07" && municipio == "040")
                return true;
            if (entidad == "07" && municipio == "041")
                return true;
            if (entidad == "07" && municipio == "042")
                return true;
            if (entidad == "07" && municipio == "043")
                return true;
            if (entidad == "07" && municipio == "044")
                return true;
            if (entidad == "07" && municipio == "045")
                return true;
            if (entidad == "07" && municipio == "046")
                return true;
            if (entidad == "07" && municipio == "047")
                return true;
            if (entidad == "07" && municipio == "048")
                return true;
            if (entidad == "07" && municipio == "049")
                return true;
            if (entidad == "07" && municipio == "050")
                return true;
            if (entidad == "07" && municipio == "051")
                return true;
            if (entidad == "07" && municipio == "052")
                return true;
            if (entidad == "07" && municipio == "053")
                return true;
            if (entidad == "07" && municipio == "054")
                return true;
            if (entidad == "07" && municipio == "055")
                return true;
            if (entidad == "07" && municipio == "056")
                return true;
            if (entidad == "07" && municipio == "057")
                return true;
            if (entidad == "07" && municipio == "058")
                return true;
            if (entidad == "07" && municipio == "059")
                return true;
            if (entidad == "07" && municipio == "060")
                return true;
            if (entidad == "07" && municipio == "061")
                return true;
            if (entidad == "07" && municipio == "062")
                return true;
            if (entidad == "07" && municipio == "063")
                return true;
            if (entidad == "07" && municipio == "064")
                return true;
            if (entidad == "07" && municipio == "065")
                return true;
            if (entidad == "07" && municipio == "066")
                return true;
            if (entidad == "07" && municipio == "067")
                return true;
            if (entidad == "07" && municipio == "068")
                return true;
            if (entidad == "07" && municipio == "069")
                return true;
            if (entidad == "07" && municipio == "070")
                return true;
            if (entidad == "07" && municipio == "071")
                return true;
            if (entidad == "07" && municipio == "072")
                return true;
            if (entidad == "07" && municipio == "073")
                return true;
            if (entidad == "07" && municipio == "074")
                return true;
            if (entidad == "07" && municipio == "075")
                return true;
            if (entidad == "07" && municipio == "076")
                return true;
            if (entidad == "07" && municipio == "077")
                return true;
            if (entidad == "07" && municipio == "078")
                return true;
            if (entidad == "07" && municipio == "079")
                return true;
            if (entidad == "07" && municipio == "080")
                return true;
            if (entidad == "07" && municipio == "081")
                return true;
            if (entidad == "07" && municipio == "082")
                return true;
            if (entidad == "07" && municipio == "083")
                return true;
            if (entidad == "07" && municipio == "084")
                return true;
            if (entidad == "07" && municipio == "085")
                return true;
            if (entidad == "07" && municipio == "086")
                return true;
            if (entidad == "07" && municipio == "087")
                return true;
            if (entidad == "07" && municipio == "088")
                return true;
            if (entidad == "07" && municipio == "089")
                return true;
            if (entidad == "07" && municipio == "090")
                return true;
            if (entidad == "07" && municipio == "091")
                return true;
            if (entidad == "07" && municipio == "092")
                return true;
            if (entidad == "07" && municipio == "093")
                return true;
            if (entidad == "07" && municipio == "094")
                return true;
            if (entidad == "07" && municipio == "095")
                return true;
            if (entidad == "07" && municipio == "096")
                return true;
            if (entidad == "07" && municipio == "097")
                return true;
            if (entidad == "07" && municipio == "098")
                return true;
            if (entidad == "07" && municipio == "099")
                return true;
            if (entidad == "07" && municipio == "100")
                return true;
            if (entidad == "07" && municipio == "101")
                return true;
            if (entidad == "07" && municipio == "102")
                return true;
            if (entidad == "07" && municipio == "103")
                return true;
            if (entidad == "07" && municipio == "104")
                return true;
            if (entidad == "07" && municipio == "105")
                return true;
            if (entidad == "07" && municipio == "106")
                return true;
            if (entidad == "07" && municipio == "107")
                return true;
            if (entidad == "07" && municipio == "108")
                return true;
            if (entidad == "07" && municipio == "109")
                return true;
            if (entidad == "07" && municipio == "110")
                return true;
            if (entidad == "07" && municipio == "111")
                return true;
            if (entidad == "07" && municipio == "112")
                return true;
            if (entidad == "07" && municipio == "113")
                return true;
            if (entidad == "07" && municipio == "114")
                return true;
            if (entidad == "07" && municipio == "115")
                return true;
            if (entidad == "07" && municipio == "116")
                return true;
            if (entidad == "07" && municipio == "117")
                return true;
            if (entidad == "07" && municipio == "118")
                return true;
            if (entidad == "08" && municipio == "001")
                return true;
            if (entidad == "08" && municipio == "002")
                return true;
            if (entidad == "08" && municipio == "003")
                return true;
            if (entidad == "08" && municipio == "004")
                return true;
            if (entidad == "08" && municipio == "005")
                return true;
            if (entidad == "08" && municipio == "006")
                return true;
            if (entidad == "08" && municipio == "007")
                return true;
            if (entidad == "08" && municipio == "008")
                return true;
            if (entidad == "08" && municipio == "009")
                return true;
            if (entidad == "08" && municipio == "010")
                return true;
            if (entidad == "08" && municipio == "011")
                return true;
            if (entidad == "08" && municipio == "012")
                return true;
            if (entidad == "08" && municipio == "013")
                return true;
            if (entidad == "08" && municipio == "014")
                return true;
            if (entidad == "08" && municipio == "015")
                return true;
            if (entidad == "08" && municipio == "016")
                return true;
            if (entidad == "08" && municipio == "017")
                return true;
            if (entidad == "08" && municipio == "018")
                return true;
            if (entidad == "08" && municipio == "019")
                return true;
            if (entidad == "08" && municipio == "020")
                return true;
            if (entidad == "08" && municipio == "021")
                return true;
            if (entidad == "08" && municipio == "022")
                return true;
            if (entidad == "08" && municipio == "023")
                return true;
            if (entidad == "08" && municipio == "024")
                return true;
            if (entidad == "08" && municipio == "025")
                return true;
            if (entidad == "08" && municipio == "026")
                return true;
            if (entidad == "08" && municipio == "027")
                return true;
            if (entidad == "08" && municipio == "028")
                return true;
            if (entidad == "08" && municipio == "029")
                return true;
            if (entidad == "08" && municipio == "030")
                return true;
            if (entidad == "08" && municipio == "031")
                return true;
            if (entidad == "08" && municipio == "032")
                return true;
            if (entidad == "08" && municipio == "033")
                return true;
            if (entidad == "08" && municipio == "034")
                return true;
            if (entidad == "08" && municipio == "035")
                return true;
            if (entidad == "08" && municipio == "036")
                return true;
            if (entidad == "08" && municipio == "037")
                return true;
            if (entidad == "08" && municipio == "038")
                return true;
            if (entidad == "08" && municipio == "039")
                return true;
            if (entidad == "08" && municipio == "040")
                return true;
            if (entidad == "08" && municipio == "041")
                return true;
            if (entidad == "08" && municipio == "042")
                return true;
            if (entidad == "08" && municipio == "043")
                return true;
            if (entidad == "08" && municipio == "044")
                return true;
            if (entidad == "08" && municipio == "045")
                return true;
            if (entidad == "08" && municipio == "046")
                return true;
            if (entidad == "08" && municipio == "047")
                return true;
            if (entidad == "08" && municipio == "048")
                return true;
            if (entidad == "08" && municipio == "049")
                return true;
            if (entidad == "08" && municipio == "050")
                return true;
            if (entidad == "08" && municipio == "051")
                return true;
            if (entidad == "08" && municipio == "052")
                return true;
            if (entidad == "08" && municipio == "053")
                return true;
            if (entidad == "08" && municipio == "054")
                return true;
            if (entidad == "08" && municipio == "055")
                return true;
            if (entidad == "08" && municipio == "056")
                return true;
            if (entidad == "08" && municipio == "057")
                return true;
            if (entidad == "08" && municipio == "058")
                return true;
            if (entidad == "08" && municipio == "059")
                return true;
            if (entidad == "08" && municipio == "060")
                return true;
            if (entidad == "08" && municipio == "061")
                return true;
            if (entidad == "08" && municipio == "062")
                return true;
            if (entidad == "08" && municipio == "063")
                return true;
            if (entidad == "08" && municipio == "064")
                return true;
            if (entidad == "08" && municipio == "065")
                return true;
            if (entidad == "08" && municipio == "066")
                return true;
            if (entidad == "08" && municipio == "067")
                return true;
            if (entidad == "09" && municipio == "001")
                return true;
            if (entidad == "09" && municipio == "002")
                return true;
            if (entidad == "09" && municipio == "003")
                return true;
            if (entidad == "09" && municipio == "004")
                return true;
            if (entidad == "09" && municipio == "005")
                return true;
            if (entidad == "09" && municipio == "006")
                return true;
            if (entidad == "09" && municipio == "007")
                return true;
            if (entidad == "09" && municipio == "008")
                return true;
            if (entidad == "09" && municipio == "009")
                return true;
            if (entidad == "09" && municipio == "010")
                return true;
            if (entidad == "09" && municipio == "011")
                return true;
            if (entidad == "09" && municipio == "012")
                return true;
            if (entidad == "09" && municipio == "013")
                return true;
            if (entidad == "09" && municipio == "014")
                return true;
            if (entidad == "09" && municipio == "015")
                return true;
            if (entidad == "09" && municipio == "016")
                return true;
            if (entidad == "10" && municipio == "001")
                return true;
            if (entidad == "10" && municipio == "002")
                return true;
            if (entidad == "10" && municipio == "003")
                return true;
            if (entidad == "10" && municipio == "004")
                return true;
            if (entidad == "10" && municipio == "005")
                return true;
            if (entidad == "10" && municipio == "006")
                return true;
            if (entidad == "10" && municipio == "007")
                return true;
            if (entidad == "10" && municipio == "008")
                return true;
            if (entidad == "10" && municipio == "009")
                return true;
            if (entidad == "10" && municipio == "010")
                return true;
            if (entidad == "10" && municipio == "011")
                return true;
            if (entidad == "10" && municipio == "012")
                return true;
            if (entidad == "10" && municipio == "013")
                return true;
            if (entidad == "10" && municipio == "014")
                return true;
            if (entidad == "10" && municipio == "015")
                return true;
            if (entidad == "10" && municipio == "016")
                return true;
            if (entidad == "10" && municipio == "017")
                return true;
            if (entidad == "10" && municipio == "018")
                return true;
            if (entidad == "10" && municipio == "019")
                return true;
            if (entidad == "10" && municipio == "020")
                return true;
            if (entidad == "10" && municipio == "021")
                return true;
            if (entidad == "10" && municipio == "022")
                return true;
            if (entidad == "10" && municipio == "023")
                return true;
            if (entidad == "10" && municipio == "024")
                return true;
            if (entidad == "10" && municipio == "025")
                return true;
            if (entidad == "10" && municipio == "026")
                return true;
            if (entidad == "10" && municipio == "027")
                return true;
            if (entidad == "10" && municipio == "028")
                return true;
            if (entidad == "10" && municipio == "029")
                return true;
            if (entidad == "10" && municipio == "030")
                return true;
            if (entidad == "10" && municipio == "031")
                return true;
            if (entidad == "10" && municipio == "032")
                return true;
            if (entidad == "10" && municipio == "033")
                return true;
            if (entidad == "10" && municipio == "034")
                return true;
            if (entidad == "10" && municipio == "035")
                return true;
            if (entidad == "10" && municipio == "036")
                return true;
            if (entidad == "10" && municipio == "037")
                return true;
            if (entidad == "10" && municipio == "038")
                return true;
            if (entidad == "10" && municipio == "039")
                return true;
            if (entidad == "11" && municipio == "001")
                return true;
            if (entidad == "11" && municipio == "002")
                return true;
            if (entidad == "11" && municipio == "003")
                return true;
            if (entidad == "11" && municipio == "004")
                return true;
            if (entidad == "11" && municipio == "005")
                return true;
            if (entidad == "11" && municipio == "006")
                return true;
            if (entidad == "11" && municipio == "007")
                return true;
            if (entidad == "11" && municipio == "008")
                return true;
            if (entidad == "11" && municipio == "009")
                return true;
            if (entidad == "11" && municipio == "010")
                return true;
            if (entidad == "11" && municipio == "011")
                return true;
            if (entidad == "11" && municipio == "012")
                return true;
            if (entidad == "11" && municipio == "013")
                return true;
            if (entidad == "11" && municipio == "014")
                return true;
            if (entidad == "11" && municipio == "015")
                return true;
            if (entidad == "11" && municipio == "016")
                return true;
            if (entidad == "11" && municipio == "017")
                return true;
            if (entidad == "11" && municipio == "018")
                return true;
            if (entidad == "11" && municipio == "019")
                return true;
            if (entidad == "11" && municipio == "020")
                return true;
            if (entidad == "11" && municipio == "021")
                return true;
            if (entidad == "11" && municipio == "022")
                return true;
            if (entidad == "11" && municipio == "023")
                return true;
            if (entidad == "11" && municipio == "024")
                return true;
            if (entidad == "11" && municipio == "025")
                return true;
            if (entidad == "11" && municipio == "026")
                return true;
            if (entidad == "11" && municipio == "027")
                return true;
            if (entidad == "11" && municipio == "028")
                return true;
            if (entidad == "11" && municipio == "029")
                return true;
            if (entidad == "11" && municipio == "030")
                return true;
            if (entidad == "11" && municipio == "031")
                return true;
            if (entidad == "11" && municipio == "032")
                return true;
            if (entidad == "11" && municipio == "033")
                return true;
            if (entidad == "11" && municipio == "034")
                return true;
            if (entidad == "11" && municipio == "035")
                return true;
            if (entidad == "11" && municipio == "036")
                return true;
            if (entidad == "11" && municipio == "037")
                return true;
            if (entidad == "11" && municipio == "038")
                return true;
            if (entidad == "11" && municipio == "039")
                return true;
            if (entidad == "11" && municipio == "040")
                return true;
            if (entidad == "11" && municipio == "041")
                return true;
            if (entidad == "11" && municipio == "042")
                return true;
            if (entidad == "11" && municipio == "043")
                return true;
            if (entidad == "11" && municipio == "044")
                return true;
            if (entidad == "11" && municipio == "045")
                return true;
            if (entidad == "11" && municipio == "046")
                return true;
            if (entidad == "12" && municipio == "001")
                return true;
            if (entidad == "12" && municipio == "002")
                return true;
            if (entidad == "12" && municipio == "003")
                return true;
            if (entidad == "12" && municipio == "004")
                return true;
            if (entidad == "12" && municipio == "005")
                return true;
            if (entidad == "12" && municipio == "006")
                return true;
            if (entidad == "12" && municipio == "007")
                return true;
            if (entidad == "12" && municipio == "008")
                return true;
            if (entidad == "12" && municipio == "009")
                return true;
            if (entidad == "12" && municipio == "010")
                return true;
            if (entidad == "12" && municipio == "011")
                return true;
            if (entidad == "12" && municipio == "012")
                return true;
            if (entidad == "12" && municipio == "013")
                return true;
            if (entidad == "12" && municipio == "014")
                return true;
            if (entidad == "12" && municipio == "015")
                return true;
            if (entidad == "12" && municipio == "016")
                return true;
            if (entidad == "12" && municipio == "017")
                return true;
            if (entidad == "12" && municipio == "018")
                return true;
            if (entidad == "12" && municipio == "019")
                return true;
            if (entidad == "12" && municipio == "020")
                return true;
            if (entidad == "12" && municipio == "021")
                return true;
            if (entidad == "12" && municipio == "022")
                return true;
            if (entidad == "12" && municipio == "023")
                return true;
            if (entidad == "12" && municipio == "024")
                return true;
            if (entidad == "12" && municipio == "025")
                return true;
            if (entidad == "12" && municipio == "026")
                return true;
            if (entidad == "12" && municipio == "027")
                return true;
            if (entidad == "12" && municipio == "028")
                return true;
            if (entidad == "12" && municipio == "029")
                return true;
            if (entidad == "12" && municipio == "030")
                return true;
            if (entidad == "12" && municipio == "031")
                return true;
            if (entidad == "12" && municipio == "032")
                return true;
            if (entidad == "12" && municipio == "033")
                return true;
            if (entidad == "12" && municipio == "034")
                return true;
            if (entidad == "12" && municipio == "035")
                return true;
            if (entidad == "12" && municipio == "036")
                return true;
            if (entidad == "12" && municipio == "037")
                return true;
            if (entidad == "12" && municipio == "038")
                return true;
            if (entidad == "12" && municipio == "039")
                return true;
            if (entidad == "12" && municipio == "040")
                return true;
            if (entidad == "12" && municipio == "041")
                return true;
            if (entidad == "12" && municipio == "042")
                return true;
            if (entidad == "12" && municipio == "043")
                return true;
            if (entidad == "12" && municipio == "044")
                return true;
            if (entidad == "12" && municipio == "045")
                return true;
            if (entidad == "12" && municipio == "046")
                return true;
            if (entidad == "12" && municipio == "047")
                return true;
            if (entidad == "12" && municipio == "048")
                return true;
            if (entidad == "12" && municipio == "049")
                return true;
            if (entidad == "12" && municipio == "050")
                return true;
            if (entidad == "12" && municipio == "051")
                return true;
            if (entidad == "12" && municipio == "052")
                return true;
            if (entidad == "12" && municipio == "053")
                return true;
            if (entidad == "12" && municipio == "054")
                return true;
            if (entidad == "12" && municipio == "055")
                return true;
            if (entidad == "12" && municipio == "056")
                return true;
            if (entidad == "12" && municipio == "057")
                return true;
            if (entidad == "12" && municipio == "058")
                return true;
            if (entidad == "12" && municipio == "059")
                return true;
            if (entidad == "12" && municipio == "060")
                return true;
            if (entidad == "12" && municipio == "061")
                return true;
            if (entidad == "12" && municipio == "062")
                return true;
            if (entidad == "12" && municipio == "063")
                return true;
            if (entidad == "12" && municipio == "064")
                return true;
            if (entidad == "12" && municipio == "065")
                return true;
            if (entidad == "12" && municipio == "066")
                return true;
            if (entidad == "12" && municipio == "067")
                return true;
            if (entidad == "12" && municipio == "068")
                return true;
            if (entidad == "12" && municipio == "069")
                return true;
            if (entidad == "12" && municipio == "070")
                return true;
            if (entidad == "12" && municipio == "071")
                return true;
            if (entidad == "12" && municipio == "072")
                return true;
            if (entidad == "12" && municipio == "073")
                return true;
            if (entidad == "12" && municipio == "074")
                return true;
            if (entidad == "12" && municipio == "075")
                return true;
            if (entidad == "12" && municipio == "076")
                return true;
            if (entidad == "12" && municipio == "077")
                return true;
            if (entidad == "12" && municipio == "078")
                return true;
            if (entidad == "12" && municipio == "079")
                return true;
            if (entidad == "12" && municipio == "080")
                return true;
            if (entidad == "12" && municipio == "081")
                return true;
            if (entidad == "13" && municipio == "001")
                return true;
            if (entidad == "13" && municipio == "002")
                return true;
            if (entidad == "13" && municipio == "003")
                return true;
            if (entidad == "13" && municipio == "004")
                return true;
            if (entidad == "13" && municipio == "005")
                return true;
            if (entidad == "13" && municipio == "006")
                return true;
            if (entidad == "13" && municipio == "007")
                return true;
            if (entidad == "13" && municipio == "008")
                return true;
            if (entidad == "13" && municipio == "009")
                return true;
            if (entidad == "13" && municipio == "010")
                return true;
            if (entidad == "13" && municipio == "011")
                return true;
            if (entidad == "13" && municipio == "012")
                return true;
            if (entidad == "13" && municipio == "013")
                return true;
            if (entidad == "13" && municipio == "014")
                return true;
            if (entidad == "13" && municipio == "015")
                return true;
            if (entidad == "13" && municipio == "016")
                return true;
            if (entidad == "13" && municipio == "017")
                return true;
            if (entidad == "13" && municipio == "018")
                return true;
            if (entidad == "13" && municipio == "019")
                return true;
            if (entidad == "13" && municipio == "020")
                return true;
            if (entidad == "13" && municipio == "021")
                return true;
            if (entidad == "13" && municipio == "022")
                return true;
            if (entidad == "13" && municipio == "023")
                return true;
            if (entidad == "13" && municipio == "024")
                return true;
            if (entidad == "13" && municipio == "025")
                return true;
            if (entidad == "13" && municipio == "026")
                return true;
            if (entidad == "13" && municipio == "027")
                return true;
            if (entidad == "13" && municipio == "028")
                return true;
            if (entidad == "13" && municipio == "029")
                return true;
            if (entidad == "13" && municipio == "030")
                return true;
            if (entidad == "13" && municipio == "031")
                return true;
            if (entidad == "13" && municipio == "032")
                return true;
            if (entidad == "13" && municipio == "033")
                return true;
            if (entidad == "13" && municipio == "034")
                return true;
            if (entidad == "13" && municipio == "035")
                return true;
            if (entidad == "13" && municipio == "036")
                return true;
            if (entidad == "13" && municipio == "037")
                return true;
            if (entidad == "13" && municipio == "038")
                return true;
            if (entidad == "13" && municipio == "039")
                return true;
            if (entidad == "13" && municipio == "040")
                return true;
            if (entidad == "13" && municipio == "041")
                return true;
            if (entidad == "13" && municipio == "042")
                return true;
            if (entidad == "13" && municipio == "043")
                return true;
            if (entidad == "13" && municipio == "044")
                return true;
            if (entidad == "13" && municipio == "045")
                return true;
            if (entidad == "13" && municipio == "046")
                return true;
            if (entidad == "13" && municipio == "047")
                return true;
            if (entidad == "13" && municipio == "048")
                return true;
            if (entidad == "13" && municipio == "049")
                return true;
            if (entidad == "13" && municipio == "050")
                return true;
            if (entidad == "13" && municipio == "051")
                return true;
            if (entidad == "13" && municipio == "052")
                return true;
            if (entidad == "13" && municipio == "053")
                return true;
            if (entidad == "13" && municipio == "054")
                return true;
            if (entidad == "13" && municipio == "055")
                return true;
            if (entidad == "13" && municipio == "056")
                return true;
            if (entidad == "13" && municipio == "057")
                return true;
            if (entidad == "13" && municipio == "058")
                return true;
            if (entidad == "13" && municipio == "059")
                return true;
            if (entidad == "13" && municipio == "060")
                return true;
            if (entidad == "13" && municipio == "061")
                return true;
            if (entidad == "13" && municipio == "062")
                return true;
            if (entidad == "13" && municipio == "063")
                return true;
            if (entidad == "13" && municipio == "064")
                return true;
            if (entidad == "13" && municipio == "065")
                return true;
            if (entidad == "13" && municipio == "066")
                return true;
            if (entidad == "13" && municipio == "067")
                return true;
            if (entidad == "13" && municipio == "068")
                return true;
            if (entidad == "13" && municipio == "069")
                return true;
            if (entidad == "13" && municipio == "070")
                return true;
            if (entidad == "13" && municipio == "071")
                return true;
            if (entidad == "13" && municipio == "072")
                return true;
            if (entidad == "13" && municipio == "073")
                return true;
            if (entidad == "13" && municipio == "074")
                return true;
            if (entidad == "13" && municipio == "075")
                return true;
            if (entidad == "13" && municipio == "076")
                return true;
            if (entidad == "13" && municipio == "077")
                return true;
            if (entidad == "13" && municipio == "078")
                return true;
            if (entidad == "13" && municipio == "079")
                return true;
            if (entidad == "13" && municipio == "080")
                return true;
            if (entidad == "13" && municipio == "081")
                return true;
            if (entidad == "13" && municipio == "082")
                return true;
            if (entidad == "13" && municipio == "083")
                return true;
            if (entidad == "13" && municipio == "084")
                return true;
            if (entidad == "14" && municipio == "001")
                return true;
            if (entidad == "14" && municipio == "002")
                return true;
            if (entidad == "14" && municipio == "003")
                return true;
            if (entidad == "14" && municipio == "004")
                return true;
            if (entidad == "14" && municipio == "005")
                return true;
            if (entidad == "14" && municipio == "006")
                return true;
            if (entidad == "14" && municipio == "007")
                return true;
            if (entidad == "14" && municipio == "008")
                return true;
            if (entidad == "14" && municipio == "009")
                return true;
            if (entidad == "14" && municipio == "010")
                return true;
            if (entidad == "14" && municipio == "011")
                return true;
            if (entidad == "14" && municipio == "012")
                return true;
            if (entidad == "14" && municipio == "013")
                return true;
            if (entidad == "14" && municipio == "014")
                return true;
            if (entidad == "14" && municipio == "015")
                return true;
            if (entidad == "14" && municipio == "016")
                return true;
            if (entidad == "14" && municipio == "017")
                return true;
            if (entidad == "14" && municipio == "018")
                return true;
            if (entidad == "14" && municipio == "019")
                return true;
            if (entidad == "14" && municipio == "020")
                return true;
            if (entidad == "14" && municipio == "021")
                return true;
            if (entidad == "14" && municipio == "022")
                return true;
            if (entidad == "14" && municipio == "023")
                return true;
            if (entidad == "14" && municipio == "024")
                return true;
            if (entidad == "14" && municipio == "025")
                return true;
            if (entidad == "14" && municipio == "026")
                return true;
            if (entidad == "14" && municipio == "027")
                return true;
            if (entidad == "14" && municipio == "028")
                return true;
            if (entidad == "14" && municipio == "029")
                return true;
            if (entidad == "14" && municipio == "030")
                return true;
            if (entidad == "14" && municipio == "031")
                return true;
            if (entidad == "14" && municipio == "032")
                return true;
            if (entidad == "14" && municipio == "033")
                return true;
            if (entidad == "14" && municipio == "034")
                return true;
            if (entidad == "14" && municipio == "035")
                return true;
            if (entidad == "14" && municipio == "036")
                return true;
            if (entidad == "14" && municipio == "037")
                return true;
            if (entidad == "14" && municipio == "038")
                return true;
            if (entidad == "14" && municipio == "039")
                return true;
            if (entidad == "14" && municipio == "040")
                return true;
            if (entidad == "14" && municipio == "041")
                return true;
            if (entidad == "14" && municipio == "042")
                return true;
            if (entidad == "14" && municipio == "043")
                return true;
            if (entidad == "14" && municipio == "044")
                return true;
            if (entidad == "14" && municipio == "045")
                return true;
            if (entidad == "14" && municipio == "046")
                return true;
            if (entidad == "14" && municipio == "047")
                return true;
            if (entidad == "14" && municipio == "048")
                return true;
            if (entidad == "14" && municipio == "049")
                return true;
            if (entidad == "14" && municipio == "050")
                return true;
            if (entidad == "14" && municipio == "051")
                return true;
            if (entidad == "14" && municipio == "052")
                return true;
            if (entidad == "14" && municipio == "053")
                return true;
            if (entidad == "14" && municipio == "054")
                return true;
            if (entidad == "14" && municipio == "055")
                return true;
            if (entidad == "14" && municipio == "056")
                return true;
            if (entidad == "14" && municipio == "057")
                return true;
            if (entidad == "14" && municipio == "058")
                return true;
            if (entidad == "14" && municipio == "059")
                return true;
            if (entidad == "14" && municipio == "060")
                return true;
            if (entidad == "14" && municipio == "061")
                return true;
            if (entidad == "14" && municipio == "062")
                return true;
            if (entidad == "14" && municipio == "063")
                return true;
            if (entidad == "14" && municipio == "064")
                return true;
            if (entidad == "14" && municipio == "065")
                return true;
            if (entidad == "14" && municipio == "066")
                return true;
            if (entidad == "14" && municipio == "067")
                return true;
            if (entidad == "14" && municipio == "068")
                return true;
            if (entidad == "14" && municipio == "069")
                return true;
            if (entidad == "14" && municipio == "070")
                return true;
            if (entidad == "14" && municipio == "071")
                return true;
            if (entidad == "14" && municipio == "072")
                return true;
            if (entidad == "14" && municipio == "073")
                return true;
            if (entidad == "14" && municipio == "074")
                return true;
            if (entidad == "14" && municipio == "075")
                return true;
            if (entidad == "14" && municipio == "076")
                return true;
            if (entidad == "14" && municipio == "077")
                return true;
            if (entidad == "14" && municipio == "078")
                return true;
            if (entidad == "14" && municipio == "079")
                return true;
            if (entidad == "14" && municipio == "080")
                return true;
            if (entidad == "14" && municipio == "081")
                return true;
            if (entidad == "14" && municipio == "082")
                return true;
            if (entidad == "14" && municipio == "083")
                return true;
            if (entidad == "14" && municipio == "084")
                return true;
            if (entidad == "14" && municipio == "085")
                return true;
            if (entidad == "14" && municipio == "086")
                return true;
            if (entidad == "14" && municipio == "087")
                return true;
            if (entidad == "14" && municipio == "088")
                return true;
            if (entidad == "14" && municipio == "089")
                return true;
            if (entidad == "14" && municipio == "090")
                return true;
            if (entidad == "14" && municipio == "091")
                return true;
            if (entidad == "14" && municipio == "092")
                return true;
            if (entidad == "14" && municipio == "093")
                return true;
            if (entidad == "14" && municipio == "094")
                return true;
            if (entidad == "14" && municipio == "095")
                return true;
            if (entidad == "14" && municipio == "096")
                return true;
            if (entidad == "14" && municipio == "097")
                return true;
            if (entidad == "14" && municipio == "098")
                return true;
            if (entidad == "14" && municipio == "099")
                return true;
            if (entidad == "14" && municipio == "100")
                return true;
            if (entidad == "14" && municipio == "101")
                return true;
            if (entidad == "14" && municipio == "102")
                return true;
            if (entidad == "14" && municipio == "103")
                return true;
            if (entidad == "14" && municipio == "104")
                return true;
            if (entidad == "14" && municipio == "105")
                return true;
            if (entidad == "14" && municipio == "106")
                return true;
            if (entidad == "14" && municipio == "107")
                return true;
            if (entidad == "14" && municipio == "108")
                return true;
            if (entidad == "14" && municipio == "109")
                return true;
            if (entidad == "14" && municipio == "110")
                return true;
            if (entidad == "14" && municipio == "111")
                return true;
            if (entidad == "14" && municipio == "112")
                return true;
            if (entidad == "14" && municipio == "113")
                return true;
            if (entidad == "14" && municipio == "114")
                return true;
            if (entidad == "14" && municipio == "115")
                return true;
            if (entidad == "14" && municipio == "116")
                return true;
            if (entidad == "14" && municipio == "117")
                return true;
            if (entidad == "14" && municipio == "118")
                return true;
            if (entidad == "14" && municipio == "119")
                return true;
            if (entidad == "14" && municipio == "120")
                return true;
            if (entidad == "14" && municipio == "121")
                return true;
            if (entidad == "14" && municipio == "122")
                return true;
            if (entidad == "14" && municipio == "123")
                return true;
            if (entidad == "14" && municipio == "124")
                return true;
            if (entidad == "14" && municipio == "125")
                return true;
            if (entidad == "15" && municipio == "001")
                return true;
            if (entidad == "15" && municipio == "002")
                return true;
            if (entidad == "15" && municipio == "003")
                return true;
            if (entidad == "15" && municipio == "004")
                return true;
            if (entidad == "15" && municipio == "005")
                return true;
            if (entidad == "15" && municipio == "006")
                return true;
            if (entidad == "15" && municipio == "007")
                return true;
            if (entidad == "15" && municipio == "008")
                return true;
            if (entidad == "15" && municipio == "009")
                return true;
            if (entidad == "15" && municipio == "010")
                return true;
            if (entidad == "15" && municipio == "011")
                return true;
            if (entidad == "15" && municipio == "012")
                return true;
            if (entidad == "15" && municipio == "013")
                return true;
            if (entidad == "15" && municipio == "014")
                return true;
            if (entidad == "15" && municipio == "015")
                return true;
            if (entidad == "15" && municipio == "016")
                return true;
            if (entidad == "15" && municipio == "017")
                return true;
            if (entidad == "15" && municipio == "018")
                return true;
            if (entidad == "15" && municipio == "019")
                return true;
            if (entidad == "15" && municipio == "020")
                return true;
            if (entidad == "15" && municipio == "021")
                return true;
            if (entidad == "15" && municipio == "022")
                return true;
            if (entidad == "15" && municipio == "023")
                return true;
            if (entidad == "15" && municipio == "024")
                return true;
            if (entidad == "15" && municipio == "025")
                return true;
            if (entidad == "15" && municipio == "026")
                return true;
            if (entidad == "15" && municipio == "027")
                return true;
            if (entidad == "15" && municipio == "028")
                return true;
            if (entidad == "15" && municipio == "029")
                return true;
            if (entidad == "15" && municipio == "030")
                return true;
            if (entidad == "15" && municipio == "031")
                return true;
            if (entidad == "15" && municipio == "032")
                return true;
            if (entidad == "15" && municipio == "033")
                return true;
            if (entidad == "15" && municipio == "034")
                return true;
            if (entidad == "15" && municipio == "035")
                return true;
            if (entidad == "15" && municipio == "036")
                return true;
            if (entidad == "15" && municipio == "037")
                return true;
            if (entidad == "15" && municipio == "038")
                return true;
            if (entidad == "15" && municipio == "039")
                return true;
            if (entidad == "15" && municipio == "040")
                return true;
            if (entidad == "15" && municipio == "041")
                return true;
            if (entidad == "15" && municipio == "042")
                return true;
            if (entidad == "15" && municipio == "043")
                return true;
            if (entidad == "15" && municipio == "044")
                return true;
            if (entidad == "15" && municipio == "045")
                return true;
            if (entidad == "15" && municipio == "046")
                return true;
            if (entidad == "15" && municipio == "047")
                return true;
            if (entidad == "15" && municipio == "048")
                return true;
            if (entidad == "15" && municipio == "049")
                return true;
            if (entidad == "15" && municipio == "050")
                return true;
            if (entidad == "15" && municipio == "051")
                return true;
            if (entidad == "15" && municipio == "052")
                return true;
            if (entidad == "15" && municipio == "053")
                return true;
            if (entidad == "15" && municipio == "054")
                return true;
            if (entidad == "15" && municipio == "055")
                return true;
            if (entidad == "15" && municipio == "056")
                return true;
            if (entidad == "15" && municipio == "057")
                return true;
            if (entidad == "15" && municipio == "058")
                return true;
            if (entidad == "15" && municipio == "059")
                return true;
            if (entidad == "15" && municipio == "060")
                return true;
            if (entidad == "15" && municipio == "061")
                return true;
            if (entidad == "15" && municipio == "062")
                return true;
            if (entidad == "15" && municipio == "063")
                return true;
            if (entidad == "15" && municipio == "064")
                return true;
            if (entidad == "15" && municipio == "065")
                return true;
            if (entidad == "15" && municipio == "066")
                return true;
            if (entidad == "15" && municipio == "067")
                return true;
            if (entidad == "15" && municipio == "068")
                return true;
            if (entidad == "15" && municipio == "069")
                return true;
            if (entidad == "15" && municipio == "070")
                return true;
            if (entidad == "15" && municipio == "071")
                return true;
            if (entidad == "15" && municipio == "072")
                return true;
            if (entidad == "15" && municipio == "073")
                return true;
            if (entidad == "15" && municipio == "074")
                return true;
            if (entidad == "15" && municipio == "075")
                return true;
            if (entidad == "15" && municipio == "076")
                return true;
            if (entidad == "15" && municipio == "077")
                return true;
            if (entidad == "15" && municipio == "078")
                return true;
            if (entidad == "15" && municipio == "079")
                return true;
            if (entidad == "15" && municipio == "080")
                return true;
            if (entidad == "15" && municipio == "081")
                return true;
            if (entidad == "15" && municipio == "082")
                return true;
            if (entidad == "15" && municipio == "083")
                return true;
            if (entidad == "15" && municipio == "084")
                return true;
            if (entidad == "15" && municipio == "085")
                return true;
            if (entidad == "15" && municipio == "086")
                return true;
            if (entidad == "15" && municipio == "087")
                return true;
            if (entidad == "15" && municipio == "088")
                return true;
            if (entidad == "15" && municipio == "089")
                return true;
            if (entidad == "15" && municipio == "090")
                return true;
            if (entidad == "15" && municipio == "091")
                return true;
            if (entidad == "15" && municipio == "092")
                return true;
            if (entidad == "15" && municipio == "093")
                return true;
            if (entidad == "15" && municipio == "094")
                return true;
            if (entidad == "15" && municipio == "095")
                return true;
            if (entidad == "15" && municipio == "096")
                return true;
            if (entidad == "15" && municipio == "097")
                return true;
            if (entidad == "15" && municipio == "098")
                return true;
            if (entidad == "15" && municipio == "099")
                return true;
            if (entidad == "15" && municipio == "100")
                return true;
            if (entidad == "15" && municipio == "101")
                return true;
            if (entidad == "15" && municipio == "102")
                return true;
            if (entidad == "15" && municipio == "103")
                return true;
            if (entidad == "15" && municipio == "104")
                return true;
            if (entidad == "15" && municipio == "105")
                return true;
            if (entidad == "15" && municipio == "106")
                return true;
            if (entidad == "15" && municipio == "107")
                return true;
            if (entidad == "15" && municipio == "108")
                return true;
            if (entidad == "15" && municipio == "109")
                return true;
            if (entidad == "15" && municipio == "110")
                return true;
            if (entidad == "15" && municipio == "111")
                return true;
            if (entidad == "15" && municipio == "112")
                return true;
            if (entidad == "15" && municipio == "113")
                return true;
            if (entidad == "15" && municipio == "114")
                return true;
            if (entidad == "15" && municipio == "115")
                return true;
            if (entidad == "15" && municipio == "116")
                return true;
            if (entidad == "15" && municipio == "117")
                return true;
            if (entidad == "15" && municipio == "118")
                return true;
            if (entidad == "15" && municipio == "119")
                return true;
            if (entidad == "15" && municipio == "120")
                return true;
            if (entidad == "15" && municipio == "121")
                return true;
            if (entidad == "15" && municipio == "122")
                return true;
            if (entidad == "15" && municipio == "123")
                return true;
            if (entidad == "15" && municipio == "124")
                return true;
            if (entidad == "15" && municipio == "125")
                return true;
            if (entidad == "16" && municipio == "001")
                return true;
            if (entidad == "16" && municipio == "002")
                return true;
            if (entidad == "16" && municipio == "003")
                return true;
            if (entidad == "16" && municipio == "004")
                return true;
            if (entidad == "16" && municipio == "005")
                return true;
            if (entidad == "16" && municipio == "006")
                return true;
            if (entidad == "16" && municipio == "007")
                return true;
            if (entidad == "16" && municipio == "008")
                return true;
            if (entidad == "16" && municipio == "009")
                return true;
            if (entidad == "16" && municipio == "010")
                return true;
            if (entidad == "16" && municipio == "011")
                return true;
            if (entidad == "16" && municipio == "012")
                return true;
            if (entidad == "16" && municipio == "013")
                return true;
            if (entidad == "16" && municipio == "014")
                return true;
            if (entidad == "16" && municipio == "015")
                return true;
            if (entidad == "16" && municipio == "016")
                return true;
            if (entidad == "16" && municipio == "017")
                return true;
            if (entidad == "16" && municipio == "018")
                return true;
            if (entidad == "16" && municipio == "019")
                return true;
            if (entidad == "16" && municipio == "020")
                return true;
            if (entidad == "16" && municipio == "021")
                return true;
            if (entidad == "16" && municipio == "022")
                return true;
            if (entidad == "16" && municipio == "023")
                return true;
            if (entidad == "16" && municipio == "024")
                return true;
            if (entidad == "16" && municipio == "025")
                return true;
            if (entidad == "16" && municipio == "026")
                return true;
            if (entidad == "16" && municipio == "027")
                return true;
            if (entidad == "16" && municipio == "028")
                return true;
            if (entidad == "16" && municipio == "029")
                return true;
            if (entidad == "16" && municipio == "030")
                return true;
            if (entidad == "16" && municipio == "031")
                return true;
            if (entidad == "16" && municipio == "032")
                return true;
            if (entidad == "16" && municipio == "033")
                return true;
            if (entidad == "16" && municipio == "034")
                return true;
            if (entidad == "16" && municipio == "035")
                return true;
            if (entidad == "16" && municipio == "036")
                return true;
            if (entidad == "16" && municipio == "037")
                return true;
            if (entidad == "16" && municipio == "038")
                return true;
            if (entidad == "16" && municipio == "039")
                return true;
            if (entidad == "16" && municipio == "040")
                return true;
            if (entidad == "16" && municipio == "041")
                return true;
            if (entidad == "16" && municipio == "042")
                return true;
            if (entidad == "16" && municipio == "043")
                return true;
            if (entidad == "16" && municipio == "044")
                return true;
            if (entidad == "16" && municipio == "045")
                return true;
            if (entidad == "16" && municipio == "046")
                return true;
            if (entidad == "16" && municipio == "047")
                return true;
            if (entidad == "16" && municipio == "048")
                return true;
            if (entidad == "16" && municipio == "049")
                return true;
            if (entidad == "16" && municipio == "050")
                return true;
            if (entidad == "16" && municipio == "051")
                return true;
            if (entidad == "16" && municipio == "052")
                return true;
            if (entidad == "16" && municipio == "053")
                return true;
            if (entidad == "16" && municipio == "054")
                return true;
            if (entidad == "16" && municipio == "055")
                return true;
            if (entidad == "16" && municipio == "056")
                return true;
            if (entidad == "16" && municipio == "057")
                return true;
            if (entidad == "16" && municipio == "058")
                return true;
            if (entidad == "16" && municipio == "059")
                return true;
            if (entidad == "16" && municipio == "060")
                return true;
            if (entidad == "16" && municipio == "061")
                return true;
            if (entidad == "16" && municipio == "062")
                return true;
            if (entidad == "16" && municipio == "063")
                return true;
            if (entidad == "16" && municipio == "064")
                return true;
            if (entidad == "16" && municipio == "065")
                return true;
            if (entidad == "16" && municipio == "066")
                return true;
            if (entidad == "16" && municipio == "067")
                return true;
            if (entidad == "16" && municipio == "068")
                return true;
            if (entidad == "16" && municipio == "069")
                return true;
            if (entidad == "16" && municipio == "070")
                return true;
            if (entidad == "16" && municipio == "071")
                return true;
            if (entidad == "16" && municipio == "072")
                return true;
            if (entidad == "16" && municipio == "073")
                return true;
            if (entidad == "16" && municipio == "074")
                return true;
            if (entidad == "16" && municipio == "075")
                return true;
            if (entidad == "16" && municipio == "076")
                return true;
            if (entidad == "16" && municipio == "077")
                return true;
            if (entidad == "16" && municipio == "078")
                return true;
            if (entidad == "16" && municipio == "079")
                return true;
            if (entidad == "16" && municipio == "080")
                return true;
            if (entidad == "16" && municipio == "081")
                return true;
            if (entidad == "16" && municipio == "082")
                return true;
            if (entidad == "16" && municipio == "083")
                return true;
            if (entidad == "16" && municipio == "084")
                return true;
            if (entidad == "16" && municipio == "085")
                return true;
            if (entidad == "16" && municipio == "086")
                return true;
            if (entidad == "16" && municipio == "087")
                return true;
            if (entidad == "16" && municipio == "088")
                return true;
            if (entidad == "16" && municipio == "089")
                return true;
            if (entidad == "16" && municipio == "090")
                return true;
            if (entidad == "16" && municipio == "091")
                return true;
            if (entidad == "16" && municipio == "092")
                return true;
            if (entidad == "16" && municipio == "093")
                return true;
            if (entidad == "16" && municipio == "094")
                return true;
            if (entidad == "16" && municipio == "095")
                return true;
            if (entidad == "16" && municipio == "096")
                return true;
            if (entidad == "16" && municipio == "097")
                return true;
            if (entidad == "16" && municipio == "098")
                return true;
            if (entidad == "16" && municipio == "099")
                return true;
            if (entidad == "16" && municipio == "100")
                return true;
            if (entidad == "16" && municipio == "101")
                return true;
            if (entidad == "16" && municipio == "102")
                return true;
            if (entidad == "16" && municipio == "103")
                return true;
            if (entidad == "16" && municipio == "104")
                return true;
            if (entidad == "16" && municipio == "105")
                return true;
            if (entidad == "16" && municipio == "106")
                return true;
            if (entidad == "16" && municipio == "107")
                return true;
            if (entidad == "16" && municipio == "108")
                return true;
            if (entidad == "16" && municipio == "109")
                return true;
            if (entidad == "16" && municipio == "110")
                return true;
            if (entidad == "16" && municipio == "111")
                return true;
            if (entidad == "16" && municipio == "112")
                return true;
            if (entidad == "16" && municipio == "113")
                return true;
            if (entidad == "17" && municipio == "001")
                return true;
            if (entidad == "17" && municipio == "002")
                return true;
            if (entidad == "17" && municipio == "003")
                return true;
            if (entidad == "17" && municipio == "004")
                return true;
            if (entidad == "17" && municipio == "005")
                return true;
            if (entidad == "17" && municipio == "006")
                return true;
            if (entidad == "17" && municipio == "007")
                return true;
            if (entidad == "17" && municipio == "008")
                return true;
            if (entidad == "17" && municipio == "009")
                return true;
            if (entidad == "17" && municipio == "010")
                return true;
            if (entidad == "17" && municipio == "011")
                return true;
            if (entidad == "17" && municipio == "012")
                return true;
            if (entidad == "17" && municipio == "013")
                return true;
            if (entidad == "17" && municipio == "014")
                return true;
            if (entidad == "17" && municipio == "015")
                return true;
            if (entidad == "17" && municipio == "016")
                return true;
            if (entidad == "17" && municipio == "017")
                return true;
            if (entidad == "17" && municipio == "018")
                return true;
            if (entidad == "17" && municipio == "019")
                return true;
            if (entidad == "17" && municipio == "020")
                return true;
            if (entidad == "17" && municipio == "021")
                return true;
            if (entidad == "17" && municipio == "022")
                return true;
            if (entidad == "17" && municipio == "023")
                return true;
            if (entidad == "17" && municipio == "024")
                return true;
            if (entidad == "17" && municipio == "025")
                return true;
            if (entidad == "17" && municipio == "026")
                return true;
            if (entidad == "17" && municipio == "027")
                return true;
            if (entidad == "17" && municipio == "028")
                return true;
            if (entidad == "17" && municipio == "029")
                return true;
            if (entidad == "17" && municipio == "030")
                return true;
            if (entidad == "17" && municipio == "031")
                return true;
            if (entidad == "17" && municipio == "032")
                return true;
            if (entidad == "17" && municipio == "033")
                return true;
            if (entidad == "18" && municipio == "001")
                return true;
            if (entidad == "18" && municipio == "002")
                return true;
            if (entidad == "18" && municipio == "003")
                return true;
            if (entidad == "18" && municipio == "004")
                return true;
            if (entidad == "18" && municipio == "005")
                return true;
            if (entidad == "18" && municipio == "006")
                return true;
            if (entidad == "18" && municipio == "007")
                return true;
            if (entidad == "18" && municipio == "008")
                return true;
            if (entidad == "18" && municipio == "009")
                return true;
            if (entidad == "18" && municipio == "010")
                return true;
            if (entidad == "18" && municipio == "011")
                return true;
            if (entidad == "18" && municipio == "012")
                return true;
            if (entidad == "18" && municipio == "013")
                return true;
            if (entidad == "18" && municipio == "014")
                return true;
            if (entidad == "18" && municipio == "015")
                return true;
            if (entidad == "18" && municipio == "016")
                return true;
            if (entidad == "18" && municipio == "017")
                return true;
            if (entidad == "18" && municipio == "018")
                return true;
            if (entidad == "18" && municipio == "019")
                return true;
            if (entidad == "18" && municipio == "020")
                return true;
            if (entidad == "19" && municipio == "001")
                return true;
            if (entidad == "19" && municipio == "002")
                return true;
            if (entidad == "19" && municipio == "003")
                return true;
            if (entidad == "19" && municipio == "004")
                return true;
            if (entidad == "19" && municipio == "005")
                return true;
            if (entidad == "19" && municipio == "006")
                return true;
            if (entidad == "19" && municipio == "007")
                return true;
            if (entidad == "19" && municipio == "008")
                return true;
            if (entidad == "19" && municipio == "009")
                return true;
            if (entidad == "19" && municipio == "010")
                return true;
            if (entidad == "19" && municipio == "011")
                return true;
            if (entidad == "19" && municipio == "012")
                return true;
            if (entidad == "19" && municipio == "013")
                return true;
            if (entidad == "19" && municipio == "014")
                return true;
            if (entidad == "19" && municipio == "015")
                return true;
            if (entidad == "19" && municipio == "016")
                return true;
            if (entidad == "19" && municipio == "017")
                return true;
            if (entidad == "19" && municipio == "018")
                return true;
            if (entidad == "19" && municipio == "019")
                return true;
            if (entidad == "19" && municipio == "020")
                return true;
            if (entidad == "19" && municipio == "021")
                return true;
            if (entidad == "19" && municipio == "022")
                return true;
            if (entidad == "19" && municipio == "023")
                return true;
            if (entidad == "19" && municipio == "024")
                return true;
            if (entidad == "19" && municipio == "025")
                return true;
            if (entidad == "19" && municipio == "026")
                return true;
            if (entidad == "19" && municipio == "027")
                return true;
            if (entidad == "19" && municipio == "028")
                return true;
            if (entidad == "19" && municipio == "029")
                return true;
            if (entidad == "19" && municipio == "030")
                return true;
            if (entidad == "19" && municipio == "031")
                return true;
            if (entidad == "19" && municipio == "032")
                return true;
            if (entidad == "19" && municipio == "033")
                return true;
            if (entidad == "19" && municipio == "034")
                return true;
            if (entidad == "19" && municipio == "035")
                return true;
            if (entidad == "19" && municipio == "036")
                return true;
            if (entidad == "19" && municipio == "037")
                return true;
            if (entidad == "19" && municipio == "038")
                return true;
            if (entidad == "19" && municipio == "039")
                return true;
            if (entidad == "19" && municipio == "040")
                return true;
            if (entidad == "19" && municipio == "041")
                return true;
            if (entidad == "19" && municipio == "042")
                return true;
            if (entidad == "19" && municipio == "043")
                return true;
            if (entidad == "19" && municipio == "044")
                return true;
            if (entidad == "19" && municipio == "045")
                return true;
            if (entidad == "19" && municipio == "046")
                return true;
            if (entidad == "19" && municipio == "047")
                return true;
            if (entidad == "19" && municipio == "048")
                return true;
            if (entidad == "19" && municipio == "049")
                return true;
            if (entidad == "19" && municipio == "050")
                return true;
            if (entidad == "19" && municipio == "051")
                return true;
            if (entidad == "20" && municipio == "001")
                return true;
            if (entidad == "20" && municipio == "002")
                return true;
            if (entidad == "20" && municipio == "003")
                return true;
            if (entidad == "20" && municipio == "004")
                return true;
            if (entidad == "20" && municipio == "005")
                return true;
            if (entidad == "20" && municipio == "006")
                return true;
            if (entidad == "20" && municipio == "007")
                return true;
            if (entidad == "20" && municipio == "008")
                return true;
            if (entidad == "20" && municipio == "009")
                return true;
            if (entidad == "20" && municipio == "010")
                return true;
            if (entidad == "20" && municipio == "011")
                return true;
            if (entidad == "20" && municipio == "012")
                return true;
            if (entidad == "20" && municipio == "013")
                return true;
            if (entidad == "20" && municipio == "014")
                return true;
            if (entidad == "20" && municipio == "015")
                return true;
            if (entidad == "20" && municipio == "016")
                return true;
            if (entidad == "20" && municipio == "017")
                return true;
            if (entidad == "20" && municipio == "018")
                return true;
            if (entidad == "20" && municipio == "019")
                return true;
            if (entidad == "20" && municipio == "020")
                return true;
            if (entidad == "20" && municipio == "021")
                return true;
            if (entidad == "20" && municipio == "022")
                return true;
            if (entidad == "20" && municipio == "023")
                return true;
            if (entidad == "20" && municipio == "024")
                return true;
            if (entidad == "20" && municipio == "025")
                return true;
            if (entidad == "20" && municipio == "026")
                return true;
            if (entidad == "20" && municipio == "027")
                return true;
            if (entidad == "20" && municipio == "028")
                return true;
            if (entidad == "20" && municipio == "029")
                return true;
            if (entidad == "20" && municipio == "030")
                return true;
            if (entidad == "20" && municipio == "031")
                return true;
            if (entidad == "20" && municipio == "032")
                return true;
            if (entidad == "20" && municipio == "033")
                return true;
            if (entidad == "20" && municipio == "034")
                return true;
            if (entidad == "20" && municipio == "035")
                return true;
            if (entidad == "20" && municipio == "036")
                return true;
            if (entidad == "20" && municipio == "037")
                return true;
            if (entidad == "20" && municipio == "038")
                return true;
            if (entidad == "20" && municipio == "039")
                return true;
            if (entidad == "20" && municipio == "040")
                return true;
            if (entidad == "20" && municipio == "041")
                return true;
            if (entidad == "20" && municipio == "042")
                return true;
            if (entidad == "20" && municipio == "043")
                return true;
            if (entidad == "20" && municipio == "044")
                return true;
            if (entidad == "20" && municipio == "045")
                return true;
            if (entidad == "20" && municipio == "046")
                return true;
            if (entidad == "20" && municipio == "047")
                return true;
            if (entidad == "20" && municipio == "048")
                return true;
            if (entidad == "20" && municipio == "049")
                return true;
            if (entidad == "20" && municipio == "050")
                return true;
            if (entidad == "20" && municipio == "051")
                return true;
            if (entidad == "20" && municipio == "052")
                return true;
            if (entidad == "20" && municipio == "053")
                return true;
            if (entidad == "20" && municipio == "054")
                return true;
            if (entidad == "20" && municipio == "055")
                return true;
            if (entidad == "20" && municipio == "056")
                return true;
            if (entidad == "20" && municipio == "057")
                return true;
            if (entidad == "20" && municipio == "058")
                return true;
            if (entidad == "20" && municipio == "059")
                return true;
            if (entidad == "20" && municipio == "060")
                return true;
            if (entidad == "20" && municipio == "061")
                return true;
            if (entidad == "20" && municipio == "062")
                return true;
            if (entidad == "20" && municipio == "063")
                return true;
            if (entidad == "20" && municipio == "064")
                return true;
            if (entidad == "20" && municipio == "065")
                return true;
            if (entidad == "20" && municipio == "066")
                return true;
            if (entidad == "20" && municipio == "067")
                return true;
            if (entidad == "20" && municipio == "068")
                return true;
            if (entidad == "20" && municipio == "069")
                return true;
            if (entidad == "20" && municipio == "070")
                return true;
            if (entidad == "20" && municipio == "071")
                return true;
            if (entidad == "20" && municipio == "072")
                return true;
            if (entidad == "20" && municipio == "073")
                return true;
            if (entidad == "20" && municipio == "074")
                return true;
            if (entidad == "20" && municipio == "075")
                return true;
            if (entidad == "20" && municipio == "076")
                return true;
            if (entidad == "20" && municipio == "077")
                return true;
            if (entidad == "20" && municipio == "078")
                return true;
            if (entidad == "20" && municipio == "079")
                return true;
            if (entidad == "20" && municipio == "080")
                return true;
            if (entidad == "20" && municipio == "081")
                return true;
            if (entidad == "20" && municipio == "082")
                return true;
            if (entidad == "20" && municipio == "083")
                return true;
            if (entidad == "20" && municipio == "084")
                return true;
            if (entidad == "20" && municipio == "085")
                return true;
            if (entidad == "20" && municipio == "086")
                return true;
            if (entidad == "20" && municipio == "087")
                return true;
            if (entidad == "20" && municipio == "088")
                return true;
            if (entidad == "20" && municipio == "089")
                return true;
            if (entidad == "20" && municipio == "090")
                return true;
            if (entidad == "20" && municipio == "091")
                return true;
            if (entidad == "20" && municipio == "092")
                return true;
            if (entidad == "20" && municipio == "093")
                return true;
            if (entidad == "20" && municipio == "094")
                return true;
            if (entidad == "20" && municipio == "095")
                return true;
            if (entidad == "20" && municipio == "096")
                return true;
            if (entidad == "20" && municipio == "097")
                return true;
            if (entidad == "20" && municipio == "098")
                return true;
            if (entidad == "20" && municipio == "099")
                return true;
            if (entidad == "20" && municipio == "100")
                return true;
            if (entidad == "20" && municipio == "101")
                return true;
            if (entidad == "20" && municipio == "102")
                return true;
            if (entidad == "20" && municipio == "103")
                return true;
            if (entidad == "20" && municipio == "104")
                return true;
            if (entidad == "20" && municipio == "105")
                return true;
            if (entidad == "20" && municipio == "106")
                return true;
            if (entidad == "20" && municipio == "107")
                return true;
            if (entidad == "20" && municipio == "108")
                return true;
            if (entidad == "20" && municipio == "109")
                return true;
            if (entidad == "20" && municipio == "110")
                return true;
            if (entidad == "20" && municipio == "111")
                return true;
            if (entidad == "20" && municipio == "112")
                return true;
            if (entidad == "20" && municipio == "113")
                return true;
            if (entidad == "20" && municipio == "114")
                return true;
            if (entidad == "20" && municipio == "115")
                return true;
            if (entidad == "20" && municipio == "116")
                return true;
            if (entidad == "20" && municipio == "117")
                return true;
            if (entidad == "20" && municipio == "118")
                return true;
            if (entidad == "20" && municipio == "119")
                return true;
            if (entidad == "20" && municipio == "120")
                return true;
            if (entidad == "20" && municipio == "121")
                return true;
            if (entidad == "20" && municipio == "122")
                return true;
            if (entidad == "20" && municipio == "123")
                return true;
            if (entidad == "20" && municipio == "124")
                return true;
            if (entidad == "20" && municipio == "125")
                return true;
            if (entidad == "20" && municipio == "126")
                return true;
            if (entidad == "20" && municipio == "127")
                return true;
            if (entidad == "20" && municipio == "128")
                return true;
            if (entidad == "20" && municipio == "129")
                return true;
            if (entidad == "20" && municipio == "130")
                return true;
            if (entidad == "20" && municipio == "131")
                return true;
            if (entidad == "20" && municipio == "132")
                return true;
            if (entidad == "20" && municipio == "133")
                return true;
            if (entidad == "20" && municipio == "134")
                return true;
            if (entidad == "20" && municipio == "135")
                return true;
            if (entidad == "20" && municipio == "136")
                return true;
            if (entidad == "20" && municipio == "137")
                return true;
            if (entidad == "20" && municipio == "138")
                return true;
            if (entidad == "20" && municipio == "139")
                return true;
            if (entidad == "20" && municipio == "140")
                return true;
            if (entidad == "20" && municipio == "141")
                return true;
            if (entidad == "20" && municipio == "142")
                return true;
            if (entidad == "20" && municipio == "143")
                return true;
            if (entidad == "20" && municipio == "144")
                return true;
            if (entidad == "20" && municipio == "145")
                return true;
            if (entidad == "20" && municipio == "146")
                return true;
            if (entidad == "20" && municipio == "147")
                return true;
            if (entidad == "20" && municipio == "148")
                return true;
            if (entidad == "20" && municipio == "149")
                return true;
            if (entidad == "20" && municipio == "150")
                return true;
            if (entidad == "20" && municipio == "151")
                return true;
            if (entidad == "20" && municipio == "152")
                return true;
            if (entidad == "20" && municipio == "153")
                return true;
            if (entidad == "20" && municipio == "154")
                return true;
            if (entidad == "20" && municipio == "155")
                return true;
            if (entidad == "20" && municipio == "156")
                return true;
            if (entidad == "20" && municipio == "157")
                return true;
            if (entidad == "20" && municipio == "158")
                return true;
            if (entidad == "20" && municipio == "159")
                return true;
            if (entidad == "20" && municipio == "160")
                return true;
            if (entidad == "20" && municipio == "161")
                return true;
            if (entidad == "20" && municipio == "162")
                return true;
            if (entidad == "20" && municipio == "163")
                return true;
            if (entidad == "20" && municipio == "164")
                return true;
            if (entidad == "20" && municipio == "165")
                return true;
            if (entidad == "20" && municipio == "166")
                return true;
            if (entidad == "20" && municipio == "167")
                return true;
            if (entidad == "20" && municipio == "168")
                return true;
            if (entidad == "20" && municipio == "169")
                return true;
            if (entidad == "20" && municipio == "170")
                return true;
            if (entidad == "20" && municipio == "171")
                return true;
            if (entidad == "20" && municipio == "172")
                return true;
            if (entidad == "20" && municipio == "173")
                return true;
            if (entidad == "20" && municipio == "174")
                return true;
            if (entidad == "20" && municipio == "175")
                return true;
            if (entidad == "20" && municipio == "176")
                return true;
            if (entidad == "20" && municipio == "177")
                return true;
            if (entidad == "20" && municipio == "178")
                return true;
            if (entidad == "20" && municipio == "179")
                return true;
            if (entidad == "20" && municipio == "180")
                return true;
            if (entidad == "20" && municipio == "181")
                return true;
            if (entidad == "20" && municipio == "182")
                return true;
            if (entidad == "20" && municipio == "183")
                return true;
            if (entidad == "20" && municipio == "184")
                return true;
            if (entidad == "20" && municipio == "185")
                return true;
            if (entidad == "20" && municipio == "186")
                return true;
            if (entidad == "20" && municipio == "187")
                return true;
            if (entidad == "20" && municipio == "188")
                return true;
            if (entidad == "20" && municipio == "189")
                return true;
            if (entidad == "20" && municipio == "190")
                return true;
            if (entidad == "20" && municipio == "191")
                return true;
            if (entidad == "20" && municipio == "192")
                return true;
            if (entidad == "20" && municipio == "193")
                return true;
            if (entidad == "20" && municipio == "194")
                return true;
            if (entidad == "20" && municipio == "195")
                return true;
            if (entidad == "20" && municipio == "196")
                return true;
            if (entidad == "20" && municipio == "197")
                return true;
            if (entidad == "20" && municipio == "198")
                return true;
            if (entidad == "20" && municipio == "199")
                return true;
            if (entidad == "20" && municipio == "200")
                return true;
            if (entidad == "20" && municipio == "201")
                return true;
            if (entidad == "20" && municipio == "202")
                return true;
            if (entidad == "20" && municipio == "203")
                return true;
            if (entidad == "20" && municipio == "204")
                return true;
            if (entidad == "20" && municipio == "205")
                return true;
            if (entidad == "20" && municipio == "206")
                return true;
            if (entidad == "20" && municipio == "207")
                return true;
            if (entidad == "20" && municipio == "208")
                return true;
            if (entidad == "20" && municipio == "209")
                return true;
            if (entidad == "20" && municipio == "210")
                return true;
            if (entidad == "20" && municipio == "211")
                return true;
            if (entidad == "20" && municipio == "212")
                return true;
            if (entidad == "20" && municipio == "213")
                return true;
            if (entidad == "20" && municipio == "214")
                return true;
            if (entidad == "20" && municipio == "215")
                return true;
            if (entidad == "20" && municipio == "216")
                return true;
            if (entidad == "20" && municipio == "217")
                return true;
            if (entidad == "20" && municipio == "218")
                return true;
            if (entidad == "20" && municipio == "219")
                return true;
            if (entidad == "20" && municipio == "220")
                return true;
            if (entidad == "20" && municipio == "221")
                return true;
            if (entidad == "20" && municipio == "222")
                return true;
            if (entidad == "20" && municipio == "223")
                return true;
            if (entidad == "20" && municipio == "224")
                return true;
            if (entidad == "20" && municipio == "225")
                return true;
            if (entidad == "20" && municipio == "226")
                return true;
            if (entidad == "20" && municipio == "227")
                return true;
            if (entidad == "20" && municipio == "228")
                return true;
            if (entidad == "20" && municipio == "229")
                return true;
            if (entidad == "20" && municipio == "230")
                return true;
            if (entidad == "20" && municipio == "231")
                return true;
            if (entidad == "20" && municipio == "232")
                return true;
            if (entidad == "20" && municipio == "233")
                return true;
            if (entidad == "20" && municipio == "234")
                return true;
            if (entidad == "20" && municipio == "235")
                return true;
            if (entidad == "20" && municipio == "236")
                return true;
            if (entidad == "20" && municipio == "237")
                return true;
            if (entidad == "20" && municipio == "238")
                return true;
            if (entidad == "20" && municipio == "239")
                return true;
            if (entidad == "20" && municipio == "240")
                return true;
            if (entidad == "20" && municipio == "241")
                return true;
            if (entidad == "20" && municipio == "242")
                return true;
            if (entidad == "20" && municipio == "243")
                return true;
            if (entidad == "20" && municipio == "244")
                return true;
            if (entidad == "20" && municipio == "245")
                return true;
            if (entidad == "20" && municipio == "246")
                return true;
            if (entidad == "20" && municipio == "247")
                return true;
            if (entidad == "20" && municipio == "248")
                return true;
            if (entidad == "20" && municipio == "249")
                return true;
            if (entidad == "20" && municipio == "250")
                return true;
            if (entidad == "20" && municipio == "251")
                return true;
            if (entidad == "20" && municipio == "252")
                return true;
            if (entidad == "20" && municipio == "253")
                return true;
            if (entidad == "20" && municipio == "254")
                return true;
            if (entidad == "20" && municipio == "255")
                return true;
            if (entidad == "20" && municipio == "256")
                return true;
            if (entidad == "20" && municipio == "257")
                return true;
            if (entidad == "20" && municipio == "258")
                return true;
            if (entidad == "20" && municipio == "259")
                return true;
            if (entidad == "20" && municipio == "260")
                return true;
            if (entidad == "20" && municipio == "261")
                return true;
            if (entidad == "20" && municipio == "262")
                return true;
            if (entidad == "20" && municipio == "263")
                return true;
            if (entidad == "20" && municipio == "264")
                return true;
            if (entidad == "20" && municipio == "265")
                return true;
            if (entidad == "20" && municipio == "266")
                return true;
            if (entidad == "20" && municipio == "267")
                return true;
            if (entidad == "20" && municipio == "268")
                return true;
            if (entidad == "20" && municipio == "269")
                return true;
            if (entidad == "20" && municipio == "270")
                return true;
            if (entidad == "20" && municipio == "271")
                return true;
            if (entidad == "20" && municipio == "272")
                return true;
            if (entidad == "20" && municipio == "273")
                return true;
            if (entidad == "20" && municipio == "274")
                return true;
            if (entidad == "20" && municipio == "275")
                return true;
            if (entidad == "20" && municipio == "276")
                return true;
            if (entidad == "20" && municipio == "277")
                return true;
            if (entidad == "20" && municipio == "278")
                return true;
            if (entidad == "20" && municipio == "279")
                return true;
            if (entidad == "20" && municipio == "280")
                return true;
            if (entidad == "20" && municipio == "281")
                return true;
            if (entidad == "20" && municipio == "282")
                return true;
            if (entidad == "20" && municipio == "283")
                return true;
            if (entidad == "20" && municipio == "284")
                return true;
            if (entidad == "20" && municipio == "285")
                return true;
            if (entidad == "20" && municipio == "286")
                return true;
            if (entidad == "20" && municipio == "287")
                return true;
            if (entidad == "20" && municipio == "288")
                return true;
            if (entidad == "20" && municipio == "289")
                return true;
            if (entidad == "20" && municipio == "290")
                return true;
            if (entidad == "20" && municipio == "291")
                return true;
            if (entidad == "20" && municipio == "292")
                return true;
            if (entidad == "20" && municipio == "293")
                return true;
            if (entidad == "20" && municipio == "294")
                return true;
            if (entidad == "20" && municipio == "295")
                return true;
            if (entidad == "20" && municipio == "296")
                return true;
            if (entidad == "20" && municipio == "297")
                return true;
            if (entidad == "20" && municipio == "298")
                return true;
            if (entidad == "20" && municipio == "299")
                return true;
            if (entidad == "20" && municipio == "300")
                return true;
            if (entidad == "20" && municipio == "301")
                return true;
            if (entidad == "20" && municipio == "302")
                return true;
            if (entidad == "20" && municipio == "303")
                return true;
            if (entidad == "20" && municipio == "304")
                return true;
            if (entidad == "20" && municipio == "305")
                return true;
            if (entidad == "20" && municipio == "306")
                return true;
            if (entidad == "20" && municipio == "307")
                return true;
            if (entidad == "20" && municipio == "308")
                return true;
            if (entidad == "20" && municipio == "309")
                return true;
            if (entidad == "20" && municipio == "310")
                return true;
            if (entidad == "20" && municipio == "311")
                return true;
            if (entidad == "20" && municipio == "312")
                return true;
            if (entidad == "20" && municipio == "313")
                return true;
            if (entidad == "20" && municipio == "314")
                return true;
            if (entidad == "20" && municipio == "315")
                return true;
            if (entidad == "20" && municipio == "316")
                return true;
            if (entidad == "20" && municipio == "317")
                return true;
            if (entidad == "20" && municipio == "318")
                return true;
            if (entidad == "20" && municipio == "319")
                return true;
            if (entidad == "20" && municipio == "320")
                return true;
            if (entidad == "20" && municipio == "321")
                return true;
            if (entidad == "20" && municipio == "322")
                return true;
            if (entidad == "20" && municipio == "323")
                return true;
            if (entidad == "20" && municipio == "324")
                return true;
            if (entidad == "20" && municipio == "325")
                return true;
            if (entidad == "20" && municipio == "326")
                return true;
            if (entidad == "20" && municipio == "327")
                return true;
            if (entidad == "20" && municipio == "328")
                return true;
            if (entidad == "20" && municipio == "329")
                return true;
            if (entidad == "20" && municipio == "330")
                return true;
            if (entidad == "20" && municipio == "331")
                return true;
            if (entidad == "20" && municipio == "332")
                return true;
            if (entidad == "20" && municipio == "333")
                return true;
            if (entidad == "20" && municipio == "334")
                return true;
            if (entidad == "20" && municipio == "335")
                return true;
            if (entidad == "20" && municipio == "336")
                return true;
            if (entidad == "20" && municipio == "337")
                return true;
            if (entidad == "20" && municipio == "338")
                return true;
            if (entidad == "20" && municipio == "339")
                return true;
            if (entidad == "20" && municipio == "340")
                return true;
            if (entidad == "20" && municipio == "341")
                return true;
            if (entidad == "20" && municipio == "342")
                return true;
            if (entidad == "20" && municipio == "343")
                return true;
            if (entidad == "20" && municipio == "344")
                return true;
            if (entidad == "20" && municipio == "345")
                return true;
            if (entidad == "20" && municipio == "346")
                return true;
            if (entidad == "20" && municipio == "347")
                return true;
            if (entidad == "20" && municipio == "348")
                return true;
            if (entidad == "20" && municipio == "349")
                return true;
            if (entidad == "20" && municipio == "350")
                return true;
            if (entidad == "20" && municipio == "351")
                return true;
            if (entidad == "20" && municipio == "352")
                return true;
            if (entidad == "20" && municipio == "353")
                return true;
            if (entidad == "20" && municipio == "354")
                return true;
            if (entidad == "20" && municipio == "355")
                return true;
            if (entidad == "20" && municipio == "356")
                return true;
            if (entidad == "20" && municipio == "357")
                return true;
            if (entidad == "20" && municipio == "358")
                return true;
            if (entidad == "20" && municipio == "359")
                return true;
            if (entidad == "20" && municipio == "360")
                return true;
            if (entidad == "20" && municipio == "361")
                return true;
            if (entidad == "20" && municipio == "362")
                return true;
            if (entidad == "20" && municipio == "363")
                return true;
            if (entidad == "20" && municipio == "364")
                return true;
            if (entidad == "20" && municipio == "365")
                return true;
            if (entidad == "20" && municipio == "366")
                return true;
            if (entidad == "20" && municipio == "367")
                return true;
            if (entidad == "20" && municipio == "368")
                return true;
            if (entidad == "20" && municipio == "369")
                return true;
            if (entidad == "20" && municipio == "370")
                return true;
            if (entidad == "20" && municipio == "371")
                return true;
            if (entidad == "20" && municipio == "372")
                return true;
            if (entidad == "20" && municipio == "373")
                return true;
            if (entidad == "20" && municipio == "374")
                return true;
            if (entidad == "20" && municipio == "375")
                return true;
            if (entidad == "20" && municipio == "376")
                return true;
            if (entidad == "20" && municipio == "377")
                return true;
            if (entidad == "20" && municipio == "378")
                return true;
            if (entidad == "20" && municipio == "379")
                return true;
            if (entidad == "20" && municipio == "380")
                return true;
            if (entidad == "20" && municipio == "381")
                return true;
            if (entidad == "20" && municipio == "382")
                return true;
            if (entidad == "20" && municipio == "383")
                return true;
            if (entidad == "20" && municipio == "384")
                return true;
            if (entidad == "20" && municipio == "385")
                return true;
            if (entidad == "20" && municipio == "386")
                return true;
            if (entidad == "20" && municipio == "387")
                return true;
            if (entidad == "20" && municipio == "388")
                return true;
            if (entidad == "20" && municipio == "389")
                return true;
            if (entidad == "20" && municipio == "390")
                return true;
            if (entidad == "20" && municipio == "391")
                return true;
            if (entidad == "20" && municipio == "392")
                return true;
            if (entidad == "20" && municipio == "393")
                return true;
            if (entidad == "20" && municipio == "394")
                return true;
            if (entidad == "20" && municipio == "395")
                return true;
            if (entidad == "20" && municipio == "396")
                return true;
            if (entidad == "20" && municipio == "397")
                return true;
            if (entidad == "20" && municipio == "398")
                return true;
            if (entidad == "20" && municipio == "399")
                return true;
            if (entidad == "20" && municipio == "400")
                return true;
            if (entidad == "20" && municipio == "401")
                return true;
            if (entidad == "20" && municipio == "402")
                return true;
            if (entidad == "20" && municipio == "403")
                return true;
            if (entidad == "20" && municipio == "404")
                return true;
            if (entidad == "20" && municipio == "405")
                return true;
            if (entidad == "20" && municipio == "406")
                return true;
            if (entidad == "20" && municipio == "407")
                return true;
            if (entidad == "20" && municipio == "408")
                return true;
            if (entidad == "20" && municipio == "409")
                return true;
            if (entidad == "20" && municipio == "410")
                return true;
            if (entidad == "20" && municipio == "411")
                return true;
            if (entidad == "20" && municipio == "412")
                return true;
            if (entidad == "20" && municipio == "413")
                return true;
            if (entidad == "20" && municipio == "414")
                return true;
            if (entidad == "20" && municipio == "415")
                return true;
            if (entidad == "20" && municipio == "416")
                return true;
            if (entidad == "20" && municipio == "417")
                return true;
            if (entidad == "20" && municipio == "418")
                return true;
            if (entidad == "20" && municipio == "419")
                return true;
            if (entidad == "20" && municipio == "420")
                return true;
            if (entidad == "20" && municipio == "421")
                return true;
            if (entidad == "20" && municipio == "422")
                return true;
            if (entidad == "20" && municipio == "423")
                return true;
            if (entidad == "20" && municipio == "424")
                return true;
            if (entidad == "20" && municipio == "425")
                return true;
            if (entidad == "20" && municipio == "426")
                return true;
            if (entidad == "20" && municipio == "427")
                return true;
            if (entidad == "20" && municipio == "428")
                return true;
            if (entidad == "20" && municipio == "429")
                return true;
            if (entidad == "20" && municipio == "430")
                return true;
            if (entidad == "20" && municipio == "431")
                return true;
            if (entidad == "20" && municipio == "432")
                return true;
            if (entidad == "20" && municipio == "433")
                return true;
            if (entidad == "20" && municipio == "434")
                return true;
            if (entidad == "20" && municipio == "435")
                return true;
            if (entidad == "20" && municipio == "436")
                return true;
            if (entidad == "20" && municipio == "437")
                return true;
            if (entidad == "20" && municipio == "438")
                return true;
            if (entidad == "20" && municipio == "439")
                return true;
            if (entidad == "20" && municipio == "440")
                return true;
            if (entidad == "20" && municipio == "441")
                return true;
            if (entidad == "20" && municipio == "442")
                return true;
            if (entidad == "20" && municipio == "443")
                return true;
            if (entidad == "20" && municipio == "444")
                return true;
            if (entidad == "20" && municipio == "445")
                return true;
            if (entidad == "20" && municipio == "446")
                return true;
            if (entidad == "20" && municipio == "447")
                return true;
            if (entidad == "20" && municipio == "448")
                return true;
            if (entidad == "20" && municipio == "449")
                return true;
            if (entidad == "20" && municipio == "450")
                return true;
            if (entidad == "20" && municipio == "451")
                return true;
            if (entidad == "20" && municipio == "452")
                return true;
            if (entidad == "20" && municipio == "453")
                return true;
            if (entidad == "20" && municipio == "454")
                return true;
            if (entidad == "20" && municipio == "455")
                return true;
            if (entidad == "20" && municipio == "456")
                return true;
            if (entidad == "20" && municipio == "457")
                return true;
            if (entidad == "20" && municipio == "458")
                return true;
            if (entidad == "20" && municipio == "459")
                return true;
            if (entidad == "20" && municipio == "460")
                return true;
            if (entidad == "20" && municipio == "461")
                return true;
            if (entidad == "20" && municipio == "462")
                return true;
            if (entidad == "20" && municipio == "463")
                return true;
            if (entidad == "20" && municipio == "464")
                return true;
            if (entidad == "20" && municipio == "465")
                return true;
            if (entidad == "20" && municipio == "466")
                return true;
            if (entidad == "20" && municipio == "467")
                return true;
            if (entidad == "20" && municipio == "468")
                return true;
            if (entidad == "20" && municipio == "469")
                return true;
            if (entidad == "20" && municipio == "470")
                return true;
            if (entidad == "20" && municipio == "471")
                return true;
            if (entidad == "20" && municipio == "472")
                return true;
            if (entidad == "20" && municipio == "473")
                return true;
            if (entidad == "20" && municipio == "474")
                return true;
            if (entidad == "20" && municipio == "475")
                return true;
            if (entidad == "20" && municipio == "476")
                return true;
            if (entidad == "20" && municipio == "477")
                return true;
            if (entidad == "20" && municipio == "478")
                return true;
            if (entidad == "20" && municipio == "479")
                return true;
            if (entidad == "20" && municipio == "480")
                return true;
            if (entidad == "20" && municipio == "481")
                return true;
            if (entidad == "20" && municipio == "482")
                return true;
            if (entidad == "20" && municipio == "483")
                return true;
            if (entidad == "20" && municipio == "484")
                return true;
            if (entidad == "20" && municipio == "485")
                return true;
            if (entidad == "20" && municipio == "486")
                return true;
            if (entidad == "20" && municipio == "487")
                return true;
            if (entidad == "20" && municipio == "488")
                return true;
            if (entidad == "20" && municipio == "489")
                return true;
            if (entidad == "20" && municipio == "490")
                return true;
            if (entidad == "20" && municipio == "491")
                return true;
            if (entidad == "20" && municipio == "492")
                return true;
            if (entidad == "20" && municipio == "493")
                return true;
            if (entidad == "20" && municipio == "494")
                return true;
            if (entidad == "20" && municipio == "495")
                return true;
            if (entidad == "20" && municipio == "496")
                return true;
            if (entidad == "20" && municipio == "497")
                return true;
            if (entidad == "20" && municipio == "498")
                return true;
            if (entidad == "20" && municipio == "499")
                return true;
            if (entidad == "20" && municipio == "500")
                return true;
            if (entidad == "20" && municipio == "501")
                return true;
            if (entidad == "20" && municipio == "502")
                return true;
            if (entidad == "20" && municipio == "503")
                return true;
            if (entidad == "20" && municipio == "504")
                return true;
            if (entidad == "20" && municipio == "505")
                return true;
            if (entidad == "20" && municipio == "506")
                return true;
            if (entidad == "20" && municipio == "507")
                return true;
            if (entidad == "20" && municipio == "508")
                return true;
            if (entidad == "20" && municipio == "509")
                return true;
            if (entidad == "20" && municipio == "510")
                return true;
            if (entidad == "20" && municipio == "511")
                return true;
            if (entidad == "20" && municipio == "512")
                return true;
            if (entidad == "20" && municipio == "513")
                return true;
            if (entidad == "20" && municipio == "514")
                return true;
            if (entidad == "20" && municipio == "515")
                return true;
            if (entidad == "20" && municipio == "516")
                return true;
            if (entidad == "20" && municipio == "517")
                return true;
            if (entidad == "20" && municipio == "518")
                return true;
            if (entidad == "20" && municipio == "519")
                return true;
            if (entidad == "20" && municipio == "520")
                return true;
            if (entidad == "20" && municipio == "521")
                return true;
            if (entidad == "20" && municipio == "522")
                return true;
            if (entidad == "20" && municipio == "523")
                return true;
            if (entidad == "20" && municipio == "524")
                return true;
            if (entidad == "20" && municipio == "525")
                return true;
            if (entidad == "20" && municipio == "526")
                return true;
            if (entidad == "20" && municipio == "527")
                return true;
            if (entidad == "20" && municipio == "528")
                return true;
            if (entidad == "20" && municipio == "529")
                return true;
            if (entidad == "20" && municipio == "530")
                return true;
            if (entidad == "20" && municipio == "531")
                return true;
            if (entidad == "20" && municipio == "532")
                return true;
            if (entidad == "20" && municipio == "533")
                return true;
            if (entidad == "20" && municipio == "534")
                return true;
            if (entidad == "20" && municipio == "535")
                return true;
            if (entidad == "20" && municipio == "536")
                return true;
            if (entidad == "20" && municipio == "537")
                return true;
            if (entidad == "20" && municipio == "538")
                return true;
            if (entidad == "20" && municipio == "539")
                return true;
            if (entidad == "20" && municipio == "540")
                return true;
            if (entidad == "20" && municipio == "541")
                return true;
            if (entidad == "20" && municipio == "542")
                return true;
            if (entidad == "20" && municipio == "543")
                return true;
            if (entidad == "20" && municipio == "544")
                return true;
            if (entidad == "20" && municipio == "545")
                return true;
            if (entidad == "20" && municipio == "546")
                return true;
            if (entidad == "20" && municipio == "547")
                return true;
            if (entidad == "20" && municipio == "548")
                return true;
            if (entidad == "20" && municipio == "549")
                return true;
            if (entidad == "20" && municipio == "550")
                return true;
            if (entidad == "20" && municipio == "551")
                return true;
            if (entidad == "20" && municipio == "552")
                return true;
            if (entidad == "20" && municipio == "553")
                return true;
            if (entidad == "20" && municipio == "554")
                return true;
            if (entidad == "20" && municipio == "555")
                return true;
            if (entidad == "20" && municipio == "556")
                return true;
            if (entidad == "20" && municipio == "557")
                return true;
            if (entidad == "20" && municipio == "558")
                return true;
            if (entidad == "20" && municipio == "559")
                return true;
            if (entidad == "20" && municipio == "560")
                return true;
            if (entidad == "20" && municipio == "561")
                return true;
            if (entidad == "20" && municipio == "562")
                return true;
            if (entidad == "20" && municipio == "563")
                return true;
            if (entidad == "20" && municipio == "564")
                return true;
            if (entidad == "20" && municipio == "565")
                return true;
            if (entidad == "20" && municipio == "566")
                return true;
            if (entidad == "20" && municipio == "567")
                return true;
            if (entidad == "20" && municipio == "568")
                return true;
            if (entidad == "20" && municipio == "569")
                return true;
            if (entidad == "20" && municipio == "570")
                return true;
            if (entidad == "21" && municipio == "001")
                return true;
            if (entidad == "21" && municipio == "002")
                return true;
            if (entidad == "21" && municipio == "003")
                return true;
            if (entidad == "21" && municipio == "004")
                return true;
            if (entidad == "21" && municipio == "005")
                return true;
            if (entidad == "21" && municipio == "006")
                return true;
            if (entidad == "21" && municipio == "007")
                return true;
            if (entidad == "21" && municipio == "008")
                return true;
            if (entidad == "21" && municipio == "009")
                return true;
            if (entidad == "21" && municipio == "010")
                return true;
            if (entidad == "21" && municipio == "011")
                return true;
            if (entidad == "21" && municipio == "012")
                return true;
            if (entidad == "21" && municipio == "013")
                return true;
            if (entidad == "21" && municipio == "014")
                return true;
            if (entidad == "21" && municipio == "015")
                return true;
            if (entidad == "21" && municipio == "016")
                return true;
            if (entidad == "21" && municipio == "017")
                return true;
            if (entidad == "21" && municipio == "018")
                return true;
            if (entidad == "21" && municipio == "019")
                return true;
            if (entidad == "21" && municipio == "020")
                return true;
            if (entidad == "21" && municipio == "021")
                return true;
            if (entidad == "21" && municipio == "022")
                return true;
            if (entidad == "21" && municipio == "023")
                return true;
            if (entidad == "21" && municipio == "024")
                return true;
            if (entidad == "21" && municipio == "025")
                return true;
            if (entidad == "21" && municipio == "026")
                return true;
            if (entidad == "21" && municipio == "027")
                return true;
            if (entidad == "21" && municipio == "028")
                return true;
            if (entidad == "21" && municipio == "029")
                return true;
            if (entidad == "21" && municipio == "030")
                return true;
            if (entidad == "21" && municipio == "031")
                return true;
            if (entidad == "21" && municipio == "032")
                return true;
            if (entidad == "21" && municipio == "033")
                return true;
            if (entidad == "21" && municipio == "034")
                return true;
            if (entidad == "21" && municipio == "035")
                return true;
            if (entidad == "21" && municipio == "036")
                return true;
            if (entidad == "21" && municipio == "037")
                return true;
            if (entidad == "21" && municipio == "038")
                return true;
            if (entidad == "21" && municipio == "039")
                return true;
            if (entidad == "21" && municipio == "040")
                return true;
            if (entidad == "21" && municipio == "041")
                return true;
            if (entidad == "21" && municipio == "042")
                return true;
            if (entidad == "21" && municipio == "043")
                return true;
            if (entidad == "21" && municipio == "044")
                return true;
            if (entidad == "21" && municipio == "045")
                return true;
            if (entidad == "21" && municipio == "046")
                return true;
            if (entidad == "21" && municipio == "047")
                return true;
            if (entidad == "21" && municipio == "048")
                return true;
            if (entidad == "21" && municipio == "049")
                return true;
            if (entidad == "21" && municipio == "050")
                return true;
            if (entidad == "21" && municipio == "051")
                return true;
            if (entidad == "21" && municipio == "052")
                return true;
            if (entidad == "21" && municipio == "053")
                return true;
            if (entidad == "21" && municipio == "054")
                return true;
            if (entidad == "21" && municipio == "055")
                return true;
            if (entidad == "21" && municipio == "056")
                return true;
            if (entidad == "21" && municipio == "057")
                return true;
            if (entidad == "21" && municipio == "058")
                return true;
            if (entidad == "21" && municipio == "059")
                return true;
            if (entidad == "21" && municipio == "060")
                return true;
            if (entidad == "21" && municipio == "061")
                return true;
            if (entidad == "21" && municipio == "062")
                return true;
            if (entidad == "21" && municipio == "063")
                return true;
            if (entidad == "21" && municipio == "064")
                return true;
            if (entidad == "21" && municipio == "065")
                return true;
            if (entidad == "21" && municipio == "066")
                return true;
            if (entidad == "21" && municipio == "067")
                return true;
            if (entidad == "21" && municipio == "068")
                return true;
            if (entidad == "21" && municipio == "069")
                return true;
            if (entidad == "21" && municipio == "070")
                return true;
            if (entidad == "21" && municipio == "071")
                return true;
            if (entidad == "21" && municipio == "072")
                return true;
            if (entidad == "21" && municipio == "073")
                return true;
            if (entidad == "21" && municipio == "074")
                return true;
            if (entidad == "21" && municipio == "075")
                return true;
            if (entidad == "21" && municipio == "076")
                return true;
            if (entidad == "21" && municipio == "077")
                return true;
            if (entidad == "21" && municipio == "078")
                return true;
            if (entidad == "21" && municipio == "079")
                return true;
            if (entidad == "21" && municipio == "080")
                return true;
            if (entidad == "21" && municipio == "081")
                return true;
            if (entidad == "21" && municipio == "082")
                return true;
            if (entidad == "21" && municipio == "083")
                return true;
            if (entidad == "21" && municipio == "084")
                return true;
            if (entidad == "21" && municipio == "085")
                return true;
            if (entidad == "21" && municipio == "086")
                return true;
            if (entidad == "21" && municipio == "087")
                return true;
            if (entidad == "21" && municipio == "088")
                return true;
            if (entidad == "21" && municipio == "089")
                return true;
            if (entidad == "21" && municipio == "090")
                return true;
            if (entidad == "21" && municipio == "091")
                return true;
            if (entidad == "21" && municipio == "092")
                return true;
            if (entidad == "21" && municipio == "093")
                return true;
            if (entidad == "21" && municipio == "094")
                return true;
            if (entidad == "21" && municipio == "095")
                return true;
            if (entidad == "21" && municipio == "096")
                return true;
            if (entidad == "21" && municipio == "097")
                return true;
            if (entidad == "21" && municipio == "098")
                return true;
            if (entidad == "21" && municipio == "099")
                return true;
            if (entidad == "21" && municipio == "100")
                return true;
            if (entidad == "21" && municipio == "101")
                return true;
            if (entidad == "21" && municipio == "102")
                return true;
            if (entidad == "21" && municipio == "103")
                return true;
            if (entidad == "21" && municipio == "104")
                return true;
            if (entidad == "21" && municipio == "105")
                return true;
            if (entidad == "21" && municipio == "106")
                return true;
            if (entidad == "21" && municipio == "107")
                return true;
            if (entidad == "21" && municipio == "108")
                return true;
            if (entidad == "21" && municipio == "109")
                return true;
            if (entidad == "21" && municipio == "110")
                return true;
            if (entidad == "21" && municipio == "111")
                return true;
            if (entidad == "21" && municipio == "112")
                return true;
            if (entidad == "21" && municipio == "113")
                return true;
            if (entidad == "21" && municipio == "114")
                return true;
            if (entidad == "21" && municipio == "115")
                return true;
            if (entidad == "21" && municipio == "116")
                return true;
            if (entidad == "21" && municipio == "117")
                return true;
            if (entidad == "21" && municipio == "118")
                return true;
            if (entidad == "21" && municipio == "119")
                return true;
            if (entidad == "21" && municipio == "120")
                return true;
            if (entidad == "21" && municipio == "121")
                return true;
            if (entidad == "21" && municipio == "122")
                return true;
            if (entidad == "21" && municipio == "123")
                return true;
            if (entidad == "21" && municipio == "124")
                return true;
            if (entidad == "21" && municipio == "125")
                return true;
            if (entidad == "21" && municipio == "126")
                return true;
            if (entidad == "21" && municipio == "127")
                return true;
            if (entidad == "21" && municipio == "128")
                return true;
            if (entidad == "21" && municipio == "129")
                return true;
            if (entidad == "21" && municipio == "130")
                return true;
            if (entidad == "21" && municipio == "131")
                return true;
            if (entidad == "21" && municipio == "132")
                return true;
            if (entidad == "21" && municipio == "133")
                return true;
            if (entidad == "21" && municipio == "134")
                return true;
            if (entidad == "21" && municipio == "135")
                return true;
            if (entidad == "21" && municipio == "136")
                return true;
            if (entidad == "21" && municipio == "137")
                return true;
            if (entidad == "21" && municipio == "138")
                return true;
            if (entidad == "21" && municipio == "139")
                return true;
            if (entidad == "21" && municipio == "140")
                return true;
            if (entidad == "21" && municipio == "141")
                return true;
            if (entidad == "21" && municipio == "142")
                return true;
            if (entidad == "21" && municipio == "143")
                return true;
            if (entidad == "21" && municipio == "144")
                return true;
            if (entidad == "21" && municipio == "145")
                return true;
            if (entidad == "21" && municipio == "146")
                return true;
            if (entidad == "21" && municipio == "147")
                return true;
            if (entidad == "21" && municipio == "148")
                return true;
            if (entidad == "21" && municipio == "149")
                return true;
            if (entidad == "21" && municipio == "150")
                return true;
            if (entidad == "21" && municipio == "151")
                return true;
            if (entidad == "21" && municipio == "152")
                return true;
            if (entidad == "21" && municipio == "153")
                return true;
            if (entidad == "21" && municipio == "154")
                return true;
            if (entidad == "21" && municipio == "155")
                return true;
            if (entidad == "21" && municipio == "156")
                return true;
            if (entidad == "21" && municipio == "157")
                return true;
            if (entidad == "21" && municipio == "158")
                return true;
            if (entidad == "21" && municipio == "159")
                return true;
            if (entidad == "21" && municipio == "160")
                return true;
            if (entidad == "21" && municipio == "161")
                return true;
            if (entidad == "21" && municipio == "162")
                return true;
            if (entidad == "21" && municipio == "163")
                return true;
            if (entidad == "21" && municipio == "164")
                return true;
            if (entidad == "21" && municipio == "165")
                return true;
            if (entidad == "21" && municipio == "166")
                return true;
            if (entidad == "21" && municipio == "167")
                return true;
            if (entidad == "21" && municipio == "168")
                return true;
            if (entidad == "21" && municipio == "169")
                return true;
            if (entidad == "21" && municipio == "170")
                return true;
            if (entidad == "21" && municipio == "171")
                return true;
            if (entidad == "21" && municipio == "172")
                return true;
            if (entidad == "21" && municipio == "173")
                return true;
            if (entidad == "21" && municipio == "174")
                return true;
            if (entidad == "21" && municipio == "175")
                return true;
            if (entidad == "21" && municipio == "176")
                return true;
            if (entidad == "21" && municipio == "177")
                return true;
            if (entidad == "21" && municipio == "178")
                return true;
            if (entidad == "21" && municipio == "179")
                return true;
            if (entidad == "21" && municipio == "180")
                return true;
            if (entidad == "21" && municipio == "181")
                return true;
            if (entidad == "21" && municipio == "182")
                return true;
            if (entidad == "21" && municipio == "183")
                return true;
            if (entidad == "21" && municipio == "184")
                return true;
            if (entidad == "21" && municipio == "185")
                return true;
            if (entidad == "21" && municipio == "186")
                return true;
            if (entidad == "21" && municipio == "187")
                return true;
            if (entidad == "21" && municipio == "188")
                return true;
            if (entidad == "21" && municipio == "189")
                return true;
            if (entidad == "21" && municipio == "190")
                return true;
            if (entidad == "21" && municipio == "191")
                return true;
            if (entidad == "21" && municipio == "192")
                return true;
            if (entidad == "21" && municipio == "193")
                return true;
            if (entidad == "21" && municipio == "194")
                return true;
            if (entidad == "21" && municipio == "195")
                return true;
            if (entidad == "21" && municipio == "196")
                return true;
            if (entidad == "21" && municipio == "197")
                return true;
            if (entidad == "21" && municipio == "198")
                return true;
            if (entidad == "21" && municipio == "199")
                return true;
            if (entidad == "21" && municipio == "200")
                return true;
            if (entidad == "21" && municipio == "201")
                return true;
            if (entidad == "21" && municipio == "202")
                return true;
            if (entidad == "21" && municipio == "203")
                return true;
            if (entidad == "21" && municipio == "204")
                return true;
            if (entidad == "21" && municipio == "205")
                return true;
            if (entidad == "21" && municipio == "206")
                return true;
            if (entidad == "21" && municipio == "207")
                return true;
            if (entidad == "21" && municipio == "208")
                return true;
            if (entidad == "21" && municipio == "209")
                return true;
            if (entidad == "21" && municipio == "210")
                return true;
            if (entidad == "21" && municipio == "211")
                return true;
            if (entidad == "21" && municipio == "212")
                return true;
            if (entidad == "21" && municipio == "213")
                return true;
            if (entidad == "21" && municipio == "214")
                return true;
            if (entidad == "21" && municipio == "215")
                return true;
            if (entidad == "21" && municipio == "216")
                return true;
            if (entidad == "21" && municipio == "217")
                return true;
            if (entidad == "22" && municipio == "001")
                return true;
            if (entidad == "22" && municipio == "002")
                return true;
            if (entidad == "22" && municipio == "003")
                return true;
            if (entidad == "22" && municipio == "004")
                return true;
            if (entidad == "22" && municipio == "005")
                return true;
            if (entidad == "22" && municipio == "006")
                return true;
            if (entidad == "22" && municipio == "007")
                return true;
            if (entidad == "22" && municipio == "008")
                return true;
            if (entidad == "22" && municipio == "009")
                return true;
            if (entidad == "22" && municipio == "010")
                return true;
            if (entidad == "22" && municipio == "011")
                return true;
            if (entidad == "22" && municipio == "012")
                return true;
            if (entidad == "22" && municipio == "013")
                return true;
            if (entidad == "22" && municipio == "014")
                return true;
            if (entidad == "22" && municipio == "015")
                return true;
            if (entidad == "22" && municipio == "016")
                return true;
            if (entidad == "22" && municipio == "017")
                return true;
            if (entidad == "22" && municipio == "018")
                return true;
            if (entidad == "23" && municipio == "001")
                return true;
            if (entidad == "23" && municipio == "002")
                return true;
            if (entidad == "23" && municipio == "003")
                return true;
            if (entidad == "23" && municipio == "004")
                return true;
            if (entidad == "23" && municipio == "005")
                return true;
            if (entidad == "23" && municipio == "006")
                return true;
            if (entidad == "23" && municipio == "007")
                return true;
            if (entidad == "23" && municipio == "008")
                return true;
            if (entidad == "23" && municipio == "009")
                return true;
            if (entidad == "23" && municipio == "010")
                return true;
            if (entidad == "24" && municipio == "001")
                return true;
            if (entidad == "24" && municipio == "002")
                return true;
            if (entidad == "24" && municipio == "003")
                return true;
            if (entidad == "24" && municipio == "004")
                return true;
            if (entidad == "24" && municipio == "005")
                return true;
            if (entidad == "24" && municipio == "006")
                return true;
            if (entidad == "24" && municipio == "007")
                return true;
            if (entidad == "24" && municipio == "008")
                return true;
            if (entidad == "24" && municipio == "009")
                return true;
            if (entidad == "24" && municipio == "010")
                return true;
            if (entidad == "24" && municipio == "011")
                return true;
            if (entidad == "24" && municipio == "012")
                return true;
            if (entidad == "24" && municipio == "013")
                return true;
            if (entidad == "24" && municipio == "014")
                return true;
            if (entidad == "24" && municipio == "015")
                return true;
            if (entidad == "24" && municipio == "016")
                return true;
            if (entidad == "24" && municipio == "017")
                return true;
            if (entidad == "24" && municipio == "018")
                return true;
            if (entidad == "24" && municipio == "019")
                return true;
            if (entidad == "24" && municipio == "020")
                return true;
            if (entidad == "24" && municipio == "021")
                return true;
            if (entidad == "24" && municipio == "022")
                return true;
            if (entidad == "24" && municipio == "023")
                return true;
            if (entidad == "24" && municipio == "024")
                return true;
            if (entidad == "24" && municipio == "025")
                return true;
            if (entidad == "24" && municipio == "026")
                return true;
            if (entidad == "24" && municipio == "027")
                return true;
            if (entidad == "24" && municipio == "028")
                return true;
            if (entidad == "24" && municipio == "029")
                return true;
            if (entidad == "24" && municipio == "030")
                return true;
            if (entidad == "24" && municipio == "031")
                return true;
            if (entidad == "24" && municipio == "032")
                return true;
            if (entidad == "24" && municipio == "033")
                return true;
            if (entidad == "24" && municipio == "034")
                return true;
            if (entidad == "24" && municipio == "035")
                return true;
            if (entidad == "24" && municipio == "036")
                return true;
            if (entidad == "24" && municipio == "037")
                return true;
            if (entidad == "24" && municipio == "038")
                return true;
            if (entidad == "24" && municipio == "039")
                return true;
            if (entidad == "24" && municipio == "040")
                return true;
            if (entidad == "24" && municipio == "041")
                return true;
            if (entidad == "24" && municipio == "042")
                return true;
            if (entidad == "24" && municipio == "043")
                return true;
            if (entidad == "24" && municipio == "044")
                return true;
            if (entidad == "24" && municipio == "045")
                return true;
            if (entidad == "24" && municipio == "046")
                return true;
            if (entidad == "24" && municipio == "047")
                return true;
            if (entidad == "24" && municipio == "048")
                return true;
            if (entidad == "24" && municipio == "049")
                return true;
            if (entidad == "24" && municipio == "050")
                return true;
            if (entidad == "24" && municipio == "051")
                return true;
            if (entidad == "24" && municipio == "052")
                return true;
            if (entidad == "24" && municipio == "053")
                return true;
            if (entidad == "24" && municipio == "054")
                return true;
            if (entidad == "24" && municipio == "055")
                return true;
            if (entidad == "24" && municipio == "056")
                return true;
            if (entidad == "24" && municipio == "057")
                return true;
            if (entidad == "24" && municipio == "058")
                return true;
            if (entidad == "25" && municipio == "001")
                return true;
            if (entidad == "25" && municipio == "002")
                return true;
            if (entidad == "25" && municipio == "003")
                return true;
            if (entidad == "25" && municipio == "004")
                return true;
            if (entidad == "25" && municipio == "005")
                return true;
            if (entidad == "25" && municipio == "006")
                return true;
            if (entidad == "25" && municipio == "007")
                return true;
            if (entidad == "25" && municipio == "008")
                return true;
            if (entidad == "25" && municipio == "009")
                return true;
            if (entidad == "25" && municipio == "010")
                return true;
            if (entidad == "25" && municipio == "011")
                return true;
            if (entidad == "25" && municipio == "012")
                return true;
            if (entidad == "25" && municipio == "013")
                return true;
            if (entidad == "25" && municipio == "014")
                return true;
            if (entidad == "25" && municipio == "015")
                return true;
            if (entidad == "25" && municipio == "016")
                return true;
            if (entidad == "25" && municipio == "017")
                return true;
            if (entidad == "25" && municipio == "018")
                return true;
            if (entidad == "26" && municipio == "001")
                return true;
            if (entidad == "26" && municipio == "002")
                return true;
            if (entidad == "26" && municipio == "003")
                return true;
            if (entidad == "26" && municipio == "004")
                return true;
            if (entidad == "26" && municipio == "005")
                return true;
            if (entidad == "26" && municipio == "006")
                return true;
            if (entidad == "26" && municipio == "007")
                return true;
            if (entidad == "26" && municipio == "008")
                return true;
            if (entidad == "26" && municipio == "009")
                return true;
            if (entidad == "26" && municipio == "010")
                return true;
            if (entidad == "26" && municipio == "011")
                return true;
            if (entidad == "26" && municipio == "012")
                return true;
            if (entidad == "26" && municipio == "013")
                return true;
            if (entidad == "26" && municipio == "014")
                return true;
            if (entidad == "26" && municipio == "015")
                return true;
            if (entidad == "26" && municipio == "016")
                return true;
            if (entidad == "26" && municipio == "017")
                return true;
            if (entidad == "26" && municipio == "018")
                return true;
            if (entidad == "26" && municipio == "019")
                return true;
            if (entidad == "26" && municipio == "020")
                return true;
            if (entidad == "26" && municipio == "021")
                return true;
            if (entidad == "26" && municipio == "022")
                return true;
            if (entidad == "26" && municipio == "023")
                return true;
            if (entidad == "26" && municipio == "024")
                return true;
            if (entidad == "26" && municipio == "025")
                return true;
            if (entidad == "26" && municipio == "026")
                return true;
            if (entidad == "26" && municipio == "027")
                return true;
            if (entidad == "26" && municipio == "028")
                return true;
            if (entidad == "26" && municipio == "029")
                return true;
            if (entidad == "26" && municipio == "030")
                return true;
            if (entidad == "26" && municipio == "031")
                return true;
            if (entidad == "26" && municipio == "032")
                return true;
            if (entidad == "26" && municipio == "033")
                return true;
            if (entidad == "26" && municipio == "034")
                return true;
            if (entidad == "26" && municipio == "035")
                return true;
            if (entidad == "26" && municipio == "036")
                return true;
            if (entidad == "26" && municipio == "037")
                return true;
            if (entidad == "26" && municipio == "038")
                return true;
            if (entidad == "26" && municipio == "039")
                return true;
            if (entidad == "26" && municipio == "040")
                return true;
            if (entidad == "26" && municipio == "041")
                return true;
            if (entidad == "26" && municipio == "042")
                return true;
            if (entidad == "26" && municipio == "043")
                return true;
            if (entidad == "26" && municipio == "044")
                return true;
            if (entidad == "26" && municipio == "045")
                return true;
            if (entidad == "26" && municipio == "046")
                return true;
            if (entidad == "26" && municipio == "047")
                return true;
            if (entidad == "26" && municipio == "048")
                return true;
            if (entidad == "26" && municipio == "049")
                return true;
            if (entidad == "26" && municipio == "050")
                return true;
            if (entidad == "26" && municipio == "051")
                return true;
            if (entidad == "26" && municipio == "052")
                return true;
            if (entidad == "26" && municipio == "053")
                return true;
            if (entidad == "26" && municipio == "054")
                return true;
            if (entidad == "26" && municipio == "055")
                return true;
            if (entidad == "26" && municipio == "056")
                return true;
            if (entidad == "26" && municipio == "057")
                return true;
            if (entidad == "26" && municipio == "058")
                return true;
            if (entidad == "26" && municipio == "059")
                return true;
            if (entidad == "26" && municipio == "060")
                return true;
            if (entidad == "26" && municipio == "061")
                return true;
            if (entidad == "26" && municipio == "062")
                return true;
            if (entidad == "26" && municipio == "063")
                return true;
            if (entidad == "26" && municipio == "064")
                return true;
            if (entidad == "26" && municipio == "065")
                return true;
            if (entidad == "26" && municipio == "066")
                return true;
            if (entidad == "26" && municipio == "067")
                return true;
            if (entidad == "26" && municipio == "068")
                return true;
            if (entidad == "26" && municipio == "069")
                return true;
            if (entidad == "26" && municipio == "070")
                return true;
            if (entidad == "26" && municipio == "071")
                return true;
            if (entidad == "26" && municipio == "072")
                return true;
            if (entidad == "27" && municipio == "001")
                return true;
            if (entidad == "27" && municipio == "002")
                return true;
            if (entidad == "27" && municipio == "003")
                return true;
            if (entidad == "27" && municipio == "004")
                return true;
            if (entidad == "27" && municipio == "005")
                return true;
            if (entidad == "27" && municipio == "006")
                return true;
            if (entidad == "27" && municipio == "007")
                return true;
            if (entidad == "27" && municipio == "008")
                return true;
            if (entidad == "27" && municipio == "009")
                return true;
            if (entidad == "27" && municipio == "010")
                return true;
            if (entidad == "27" && municipio == "011")
                return true;
            if (entidad == "27" && municipio == "012")
                return true;
            if (entidad == "27" && municipio == "013")
                return true;
            if (entidad == "27" && municipio == "014")
                return true;
            if (entidad == "27" && municipio == "015")
                return true;
            if (entidad == "27" && municipio == "016")
                return true;
            if (entidad == "27" && municipio == "017")
                return true;
            if (entidad == "28" && municipio == "001")
                return true;
            if (entidad == "28" && municipio == "002")
                return true;
            if (entidad == "28" && municipio == "003")
                return true;
            if (entidad == "28" && municipio == "004")
                return true;
            if (entidad == "28" && municipio == "005")
                return true;
            if (entidad == "28" && municipio == "006")
                return true;
            if (entidad == "28" && municipio == "007")
                return true;
            if (entidad == "28" && municipio == "008")
                return true;
            if (entidad == "28" && municipio == "009")
                return true;
            if (entidad == "28" && municipio == "010")
                return true;
            if (entidad == "28" && municipio == "011")
                return true;
            if (entidad == "28" && municipio == "012")
                return true;
            if (entidad == "28" && municipio == "013")
                return true;
            if (entidad == "28" && municipio == "014")
                return true;
            if (entidad == "28" && municipio == "015")
                return true;
            if (entidad == "28" && municipio == "016")
                return true;
            if (entidad == "28" && municipio == "017")
                return true;
            if (entidad == "28" && municipio == "018")
                return true;
            if (entidad == "28" && municipio == "019")
                return true;
            if (entidad == "28" && municipio == "020")
                return true;
            if (entidad == "28" && municipio == "021")
                return true;
            if (entidad == "28" && municipio == "022")
                return true;
            if (entidad == "28" && municipio == "023")
                return true;
            if (entidad == "28" && municipio == "024")
                return true;
            if (entidad == "28" && municipio == "025")
                return true;
            if (entidad == "28" && municipio == "026")
                return true;
            if (entidad == "28" && municipio == "027")
                return true;
            if (entidad == "28" && municipio == "028")
                return true;
            if (entidad == "28" && municipio == "029")
                return true;
            if (entidad == "28" && municipio == "030")
                return true;
            if (entidad == "28" && municipio == "031")
                return true;
            if (entidad == "28" && municipio == "032")
                return true;
            if (entidad == "28" && municipio == "033")
                return true;
            if (entidad == "28" && municipio == "034")
                return true;
            if (entidad == "28" && municipio == "035")
                return true;
            if (entidad == "28" && municipio == "036")
                return true;
            if (entidad == "28" && municipio == "037")
                return true;
            if (entidad == "28" && municipio == "038")
                return true;
            if (entidad == "28" && municipio == "039")
                return true;
            if (entidad == "28" && municipio == "040")
                return true;
            if (entidad == "28" && municipio == "041")
                return true;
            if (entidad == "28" && municipio == "042")
                return true;
            if (entidad == "28" && municipio == "043")
                return true;
            if (entidad == "29" && municipio == "001")
                return true;
            if (entidad == "29" && municipio == "002")
                return true;
            if (entidad == "29" && municipio == "003")
                return true;
            if (entidad == "29" && municipio == "004")
                return true;
            if (entidad == "29" && municipio == "005")
                return true;
            if (entidad == "29" && municipio == "006")
                return true;
            if (entidad == "29" && municipio == "007")
                return true;
            if (entidad == "29" && municipio == "008")
                return true;
            if (entidad == "29" && municipio == "009")
                return true;
            if (entidad == "29" && municipio == "010")
                return true;
            if (entidad == "29" && municipio == "011")
                return true;
            if (entidad == "29" && municipio == "012")
                return true;
            if (entidad == "29" && municipio == "013")
                return true;
            if (entidad == "29" && municipio == "014")
                return true;
            if (entidad == "29" && municipio == "015")
                return true;
            if (entidad == "29" && municipio == "016")
                return true;
            if (entidad == "29" && municipio == "017")
                return true;
            if (entidad == "29" && municipio == "018")
                return true;
            if (entidad == "29" && municipio == "019")
                return true;
            if (entidad == "29" && municipio == "020")
                return true;
            if (entidad == "29" && municipio == "021")
                return true;
            if (entidad == "29" && municipio == "022")
                return true;
            if (entidad == "29" && municipio == "023")
                return true;
            if (entidad == "29" && municipio == "024")
                return true;
            if (entidad == "29" && municipio == "025")
                return true;
            if (entidad == "29" && municipio == "026")
                return true;
            if (entidad == "29" && municipio == "027")
                return true;
            if (entidad == "29" && municipio == "028")
                return true;
            if (entidad == "29" && municipio == "029")
                return true;
            if (entidad == "29" && municipio == "030")
                return true;
            if (entidad == "29" && municipio == "031")
                return true;
            if (entidad == "29" && municipio == "032")
                return true;
            if (entidad == "29" && municipio == "033")
                return true;
            if (entidad == "29" && municipio == "034")
                return true;
            if (entidad == "29" && municipio == "035")
                return true;
            if (entidad == "29" && municipio == "036")
                return true;
            if (entidad == "29" && municipio == "037")
                return true;
            if (entidad == "29" && municipio == "038")
                return true;
            if (entidad == "29" && municipio == "039")
                return true;
            if (entidad == "29" && municipio == "040")
                return true;
            if (entidad == "29" && municipio == "041")
                return true;
            if (entidad == "29" && municipio == "042")
                return true;
            if (entidad == "29" && municipio == "043")
                return true;
            if (entidad == "29" && municipio == "044")
                return true;
            if (entidad == "29" && municipio == "045")
                return true;
            if (entidad == "29" && municipio == "046")
                return true;
            if (entidad == "29" && municipio == "047")
                return true;
            if (entidad == "29" && municipio == "048")
                return true;
            if (entidad == "29" && municipio == "049")
                return true;
            if (entidad == "29" && municipio == "050")
                return true;
            if (entidad == "29" && municipio == "051")
                return true;
            if (entidad == "29" && municipio == "052")
                return true;
            if (entidad == "29" && municipio == "053")
                return true;
            if (entidad == "29" && municipio == "054")
                return true;
            if (entidad == "29" && municipio == "055")
                return true;
            if (entidad == "29" && municipio == "056")
                return true;
            if (entidad == "29" && municipio == "057")
                return true;
            if (entidad == "29" && municipio == "058")
                return true;
            if (entidad == "29" && municipio == "059")
                return true;
            if (entidad == "29" && municipio == "060")
                return true;
            if (entidad == "30" && municipio == "001")
                return true;
            if (entidad == "30" && municipio == "002")
                return true;
            if (entidad == "30" && municipio == "003")
                return true;
            if (entidad == "30" && municipio == "004")
                return true;
            if (entidad == "30" && municipio == "005")
                return true;
            if (entidad == "30" && municipio == "006")
                return true;
            if (entidad == "30" && municipio == "007")
                return true;
            if (entidad == "30" && municipio == "008")
                return true;
            if (entidad == "30" && municipio == "009")
                return true;
            if (entidad == "30" && municipio == "010")
                return true;
            if (entidad == "30" && municipio == "011")
                return true;
            if (entidad == "30" && municipio == "012")
                return true;
            if (entidad == "30" && municipio == "013")
                return true;
            if (entidad == "30" && municipio == "014")
                return true;
            if (entidad == "30" && municipio == "015")
                return true;
            if (entidad == "30" && municipio == "016")
                return true;
            if (entidad == "30" && municipio == "017")
                return true;
            if (entidad == "30" && municipio == "018")
                return true;
            if (entidad == "30" && municipio == "019")
                return true;
            if (entidad == "30" && municipio == "020")
                return true;
            if (entidad == "30" && municipio == "021")
                return true;
            if (entidad == "30" && municipio == "022")
                return true;
            if (entidad == "30" && municipio == "023")
                return true;
            if (entidad == "30" && municipio == "024")
                return true;
            if (entidad == "30" && municipio == "025")
                return true;
            if (entidad == "30" && municipio == "026")
                return true;
            if (entidad == "30" && municipio == "027")
                return true;
            if (entidad == "30" && municipio == "028")
                return true;
            if (entidad == "30" && municipio == "029")
                return true;
            if (entidad == "30" && municipio == "030")
                return true;
            if (entidad == "30" && municipio == "031")
                return true;
            if (entidad == "30" && municipio == "032")
                return true;
            if (entidad == "30" && municipio == "033")
                return true;
            if (entidad == "30" && municipio == "034")
                return true;
            if (entidad == "30" && municipio == "035")
                return true;
            if (entidad == "30" && municipio == "036")
                return true;
            if (entidad == "30" && municipio == "037")
                return true;
            if (entidad == "30" && municipio == "038")
                return true;
            if (entidad == "30" && municipio == "039")
                return true;
            if (entidad == "30" && municipio == "040")
                return true;
            if (entidad == "30" && municipio == "041")
                return true;
            if (entidad == "30" && municipio == "042")
                return true;
            if (entidad == "30" && municipio == "043")
                return true;
            if (entidad == "30" && municipio == "044")
                return true;
            if (entidad == "30" && municipio == "045")
                return true;
            if (entidad == "30" && municipio == "046")
                return true;
            if (entidad == "30" && municipio == "047")
                return true;
            if (entidad == "30" && municipio == "048")
                return true;
            if (entidad == "30" && municipio == "049")
                return true;
            if (entidad == "30" && municipio == "050")
                return true;
            if (entidad == "30" && municipio == "051")
                return true;
            if (entidad == "30" && municipio == "052")
                return true;
            if (entidad == "30" && municipio == "053")
                return true;
            if (entidad == "30" && municipio == "054")
                return true;
            if (entidad == "30" && municipio == "055")
                return true;
            if (entidad == "30" && municipio == "056")
                return true;
            if (entidad == "30" && municipio == "057")
                return true;
            if (entidad == "30" && municipio == "058")
                return true;
            if (entidad == "30" && municipio == "059")
                return true;
            if (entidad == "30" && municipio == "060")
                return true;
            if (entidad == "30" && municipio == "061")
                return true;
            if (entidad == "30" && municipio == "062")
                return true;
            if (entidad == "30" && municipio == "063")
                return true;
            if (entidad == "30" && municipio == "064")
                return true;
            if (entidad == "30" && municipio == "065")
                return true;
            if (entidad == "30" && municipio == "066")
                return true;
            if (entidad == "30" && municipio == "067")
                return true;
            if (entidad == "30" && municipio == "068")
                return true;
            if (entidad == "30" && municipio == "069")
                return true;
            if (entidad == "30" && municipio == "070")
                return true;
            if (entidad == "30" && municipio == "071")
                return true;
            if (entidad == "30" && municipio == "072")
                return true;
            if (entidad == "30" && municipio == "073")
                return true;
            if (entidad == "30" && municipio == "074")
                return true;
            if (entidad == "30" && municipio == "075")
                return true;
            if (entidad == "30" && municipio == "076")
                return true;
            if (entidad == "30" && municipio == "077")
                return true;
            if (entidad == "30" && municipio == "078")
                return true;
            if (entidad == "30" && municipio == "079")
                return true;
            if (entidad == "30" && municipio == "080")
                return true;
            if (entidad == "30" && municipio == "081")
                return true;
            if (entidad == "30" && municipio == "082")
                return true;
            if (entidad == "30" && municipio == "083")
                return true;
            if (entidad == "30" && municipio == "084")
                return true;
            if (entidad == "30" && municipio == "085")
                return true;
            if (entidad == "30" && municipio == "086")
                return true;
            if (entidad == "30" && municipio == "087")
                return true;
            if (entidad == "30" && municipio == "088")
                return true;
            if (entidad == "30" && municipio == "089")
                return true;
            if (entidad == "30" && municipio == "090")
                return true;
            if (entidad == "30" && municipio == "091")
                return true;
            if (entidad == "30" && municipio == "092")
                return true;
            if (entidad == "30" && municipio == "093")
                return true;
            if (entidad == "30" && municipio == "094")
                return true;
            if (entidad == "30" && municipio == "095")
                return true;
            if (entidad == "30" && municipio == "096")
                return true;
            if (entidad == "30" && municipio == "097")
                return true;
            if (entidad == "30" && municipio == "098")
                return true;
            if (entidad == "30" && municipio == "099")
                return true;
            if (entidad == "30" && municipio == "100")
                return true;
            if (entidad == "30" && municipio == "101")
                return true;
            if (entidad == "30" && municipio == "102")
                return true;
            if (entidad == "30" && municipio == "103")
                return true;
            if (entidad == "30" && municipio == "104")
                return true;
            if (entidad == "30" && municipio == "105")
                return true;
            if (entidad == "30" && municipio == "106")
                return true;
            if (entidad == "30" && municipio == "107")
                return true;
            if (entidad == "30" && municipio == "108")
                return true;
            if (entidad == "30" && municipio == "109")
                return true;
            if (entidad == "30" && municipio == "110")
                return true;
            if (entidad == "30" && municipio == "111")
                return true;
            if (entidad == "30" && municipio == "112")
                return true;
            if (entidad == "30" && municipio == "113")
                return true;
            if (entidad == "30" && municipio == "114")
                return true;
            if (entidad == "30" && municipio == "115")
                return true;
            if (entidad == "30" && municipio == "116")
                return true;
            if (entidad == "30" && municipio == "117")
                return true;
            if (entidad == "30" && municipio == "118")
                return true;
            if (entidad == "30" && municipio == "119")
                return true;
            if (entidad == "30" && municipio == "120")
                return true;
            if (entidad == "30" && municipio == "121")
                return true;
            if (entidad == "30" && municipio == "122")
                return true;
            if (entidad == "30" && municipio == "123")
                return true;
            if (entidad == "30" && municipio == "124")
                return true;
            if (entidad == "30" && municipio == "125")
                return true;
            if (entidad == "30" && municipio == "126")
                return true;
            if (entidad == "30" && municipio == "127")
                return true;
            if (entidad == "30" && municipio == "128")
                return true;
            if (entidad == "30" && municipio == "129")
                return true;
            if (entidad == "30" && municipio == "130")
                return true;
            if (entidad == "30" && municipio == "131")
                return true;
            if (entidad == "30" && municipio == "132")
                return true;
            if (entidad == "30" && municipio == "133")
                return true;
            if (entidad == "30" && municipio == "134")
                return true;
            if (entidad == "30" && municipio == "135")
                return true;
            if (entidad == "30" && municipio == "136")
                return true;
            if (entidad == "30" && municipio == "137")
                return true;
            if (entidad == "30" && municipio == "138")
                return true;
            if (entidad == "30" && municipio == "139")
                return true;
            if (entidad == "30" && municipio == "140")
                return true;
            if (entidad == "30" && municipio == "141")
                return true;
            if (entidad == "30" && municipio == "142")
                return true;
            if (entidad == "30" && municipio == "143")
                return true;
            if (entidad == "30" && municipio == "144")
                return true;
            if (entidad == "30" && municipio == "145")
                return true;
            if (entidad == "30" && municipio == "146")
                return true;
            if (entidad == "30" && municipio == "147")
                return true;
            if (entidad == "30" && municipio == "148")
                return true;
            if (entidad == "30" && municipio == "149")
                return true;
            if (entidad == "30" && municipio == "150")
                return true;
            if (entidad == "30" && municipio == "151")
                return true;
            if (entidad == "30" && municipio == "152")
                return true;
            if (entidad == "30" && municipio == "153")
                return true;
            if (entidad == "30" && municipio == "154")
                return true;
            if (entidad == "30" && municipio == "155")
                return true;
            if (entidad == "30" && municipio == "156")
                return true;
            if (entidad == "30" && municipio == "157")
                return true;
            if (entidad == "30" && municipio == "158")
                return true;
            if (entidad == "30" && municipio == "159")
                return true;
            if (entidad == "30" && municipio == "160")
                return true;
            if (entidad == "30" && municipio == "161")
                return true;
            if (entidad == "30" && municipio == "162")
                return true;
            if (entidad == "30" && municipio == "163")
                return true;
            if (entidad == "30" && municipio == "164")
                return true;
            if (entidad == "30" && municipio == "165")
                return true;
            if (entidad == "30" && municipio == "166")
                return true;
            if (entidad == "30" && municipio == "167")
                return true;
            if (entidad == "30" && municipio == "168")
                return true;
            if (entidad == "30" && municipio == "169")
                return true;
            if (entidad == "30" && municipio == "170")
                return true;
            if (entidad == "30" && municipio == "171")
                return true;
            if (entidad == "30" && municipio == "172")
                return true;
            if (entidad == "30" && municipio == "173")
                return true;
            if (entidad == "30" && municipio == "174")
                return true;
            if (entidad == "30" && municipio == "175")
                return true;
            if (entidad == "30" && municipio == "176")
                return true;
            if (entidad == "30" && municipio == "177")
                return true;
            if (entidad == "30" && municipio == "178")
                return true;
            if (entidad == "30" && municipio == "179")
                return true;
            if (entidad == "30" && municipio == "180")
                return true;
            if (entidad == "30" && municipio == "181")
                return true;
            if (entidad == "30" && municipio == "182")
                return true;
            if (entidad == "30" && municipio == "183")
                return true;
            if (entidad == "30" && municipio == "184")
                return true;
            if (entidad == "30" && municipio == "185")
                return true;
            if (entidad == "30" && municipio == "186")
                return true;
            if (entidad == "30" && municipio == "187")
                return true;
            if (entidad == "30" && municipio == "188")
                return true;
            if (entidad == "30" && municipio == "189")
                return true;
            if (entidad == "30" && municipio == "190")
                return true;
            if (entidad == "30" && municipio == "191")
                return true;
            if (entidad == "30" && municipio == "192")
                return true;
            if (entidad == "30" && municipio == "193")
                return true;
            if (entidad == "30" && municipio == "194")
                return true;
            if (entidad == "30" && municipio == "195")
                return true;
            if (entidad == "30" && municipio == "196")
                return true;
            if (entidad == "30" && municipio == "197")
                return true;
            if (entidad == "30" && municipio == "198")
                return true;
            if (entidad == "30" && municipio == "199")
                return true;
            if (entidad == "30" && municipio == "200")
                return true;
            if (entidad == "30" && municipio == "201")
                return true;
            if (entidad == "30" && municipio == "202")
                return true;
            if (entidad == "30" && municipio == "203")
                return true;
            if (entidad == "30" && municipio == "204")
                return true;
            if (entidad == "30" && municipio == "205")
                return true;
            if (entidad == "30" && municipio == "206")
                return true;
            if (entidad == "30" && municipio == "207")
                return true;
            if (entidad == "30" && municipio == "208")
                return true;
            if (entidad == "30" && municipio == "209")
                return true;
            if (entidad == "30" && municipio == "210")
                return true;
            if (entidad == "30" && municipio == "211")
                return true;
            if (entidad == "30" && municipio == "212")
                return true;
            if (entidad == "31" && municipio == "001")
                return true;
            if (entidad == "31" && municipio == "002")
                return true;
            if (entidad == "31" && municipio == "003")
                return true;
            if (entidad == "31" && municipio == "004")
                return true;
            if (entidad == "31" && municipio == "005")
                return true;
            if (entidad == "31" && municipio == "006")
                return true;
            if (entidad == "31" && municipio == "007")
                return true;
            if (entidad == "31" && municipio == "008")
                return true;
            if (entidad == "31" && municipio == "009")
                return true;
            if (entidad == "31" && municipio == "010")
                return true;
            if (entidad == "31" && municipio == "011")
                return true;
            if (entidad == "31" && municipio == "012")
                return true;
            if (entidad == "31" && municipio == "013")
                return true;
            if (entidad == "31" && municipio == "014")
                return true;
            if (entidad == "31" && municipio == "015")
                return true;
            if (entidad == "31" && municipio == "016")
                return true;
            if (entidad == "31" && municipio == "017")
                return true;
            if (entidad == "31" && municipio == "018")
                return true;
            if (entidad == "31" && municipio == "019")
                return true;
            if (entidad == "31" && municipio == "020")
                return true;
            if (entidad == "31" && municipio == "021")
                return true;
            if (entidad == "31" && municipio == "022")
                return true;
            if (entidad == "31" && municipio == "023")
                return true;
            if (entidad == "31" && municipio == "024")
                return true;
            if (entidad == "31" && municipio == "025")
                return true;
            if (entidad == "31" && municipio == "026")
                return true;
            if (entidad == "31" && municipio == "027")
                return true;
            if (entidad == "31" && municipio == "028")
                return true;
            if (entidad == "31" && municipio == "029")
                return true;
            if (entidad == "31" && municipio == "030")
                return true;
            if (entidad == "31" && municipio == "031")
                return true;
            if (entidad == "31" && municipio == "032")
                return true;
            if (entidad == "31" && municipio == "033")
                return true;
            if (entidad == "31" && municipio == "034")
                return true;
            if (entidad == "31" && municipio == "035")
                return true;
            if (entidad == "31" && municipio == "036")
                return true;
            if (entidad == "31" && municipio == "037")
                return true;
            if (entidad == "31" && municipio == "038")
                return true;
            if (entidad == "31" && municipio == "039")
                return true;
            if (entidad == "31" && municipio == "040")
                return true;
            if (entidad == "31" && municipio == "041")
                return true;
            if (entidad == "31" && municipio == "042")
                return true;
            if (entidad == "31" && municipio == "043")
                return true;
            if (entidad == "31" && municipio == "044")
                return true;
            if (entidad == "31" && municipio == "045")
                return true;
            if (entidad == "31" && municipio == "046")
                return true;
            if (entidad == "31" && municipio == "047")
                return true;
            if (entidad == "31" && municipio == "048")
                return true;
            if (entidad == "31" && municipio == "049")
                return true;
            if (entidad == "31" && municipio == "050")
                return true;
            if (entidad == "31" && municipio == "051")
                return true;
            if (entidad == "31" && municipio == "052")
                return true;
            if (entidad == "31" && municipio == "053")
                return true;
            if (entidad == "31" && municipio == "054")
                return true;
            if (entidad == "31" && municipio == "055")
                return true;
            if (entidad == "31" && municipio == "056")
                return true;
            if (entidad == "31" && municipio == "057")
                return true;
            if (entidad == "31" && municipio == "058")
                return true;
            if (entidad == "31" && municipio == "059")
                return true;
            if (entidad == "31" && municipio == "060")
                return true;
            if (entidad == "31" && municipio == "061")
                return true;
            if (entidad == "31" && municipio == "062")
                return true;
            if (entidad == "31" && municipio == "063")
                return true;
            if (entidad == "31" && municipio == "064")
                return true;
            if (entidad == "31" && municipio == "065")
                return true;
            if (entidad == "31" && municipio == "066")
                return true;
            if (entidad == "31" && municipio == "067")
                return true;
            if (entidad == "31" && municipio == "068")
                return true;
            if (entidad == "31" && municipio == "069")
                return true;
            if (entidad == "31" && municipio == "070")
                return true;
            if (entidad == "31" && municipio == "071")
                return true;
            if (entidad == "31" && municipio == "072")
                return true;
            if (entidad == "31" && municipio == "073")
                return true;
            if (entidad == "31" && municipio == "074")
                return true;
            if (entidad == "31" && municipio == "075")
                return true;
            if (entidad == "31" && municipio == "076")
                return true;
            if (entidad == "31" && municipio == "077")
                return true;
            if (entidad == "31" && municipio == "078")
                return true;
            if (entidad == "31" && municipio == "079")
                return true;
            if (entidad == "31" && municipio == "080")
                return true;
            if (entidad == "31" && municipio == "081")
                return true;
            if (entidad == "31" && municipio == "082")
                return true;
            if (entidad == "31" && municipio == "083")
                return true;
            if (entidad == "31" && municipio == "084")
                return true;
            if (entidad == "31" && municipio == "085")
                return true;
            if (entidad == "31" && municipio == "086")
                return true;
            if (entidad == "31" && municipio == "087")
                return true;
            if (entidad == "31" && municipio == "088")
                return true;
            if (entidad == "31" && municipio == "089")
                return true;
            if (entidad == "31" && municipio == "090")
                return true;
            if (entidad == "31" && municipio == "091")
                return true;
            if (entidad == "31" && municipio == "092")
                return true;
            if (entidad == "31" && municipio == "093")
                return true;
            if (entidad == "31" && municipio == "094")
                return true;
            if (entidad == "31" && municipio == "095")
                return true;
            if (entidad == "31" && municipio == "096")
                return true;
            if (entidad == "31" && municipio == "097")
                return true;
            if (entidad == "31" && municipio == "098")
                return true;
            if (entidad == "31" && municipio == "099")
                return true;
            if (entidad == "31" && municipio == "100")
                return true;
            if (entidad == "31" && municipio == "101")
                return true;
            if (entidad == "31" && municipio == "102")
                return true;
            if (entidad == "31" && municipio == "103")
                return true;
            if (entidad == "31" && municipio == "104")
                return true;
            if (entidad == "31" && municipio == "105")
                return true;
            if (entidad == "31" && municipio == "106")
                return true;
            if (entidad == "32" && municipio == "001")
                return true;
            if (entidad == "32" && municipio == "002")
                return true;
            if (entidad == "32" && municipio == "003")
                return true;
            if (entidad == "32" && municipio == "004")
                return true;
            if (entidad == "32" && municipio == "005")
                return true;
            if (entidad == "32" && municipio == "006")
                return true;
            if (entidad == "32" && municipio == "007")
                return true;
            if (entidad == "32" && municipio == "008")
                return true;
            if (entidad == "32" && municipio == "009")
                return true;
            if (entidad == "32" && municipio == "010")
                return true;
            if (entidad == "32" && municipio == "011")
                return true;
            if (entidad == "32" && municipio == "012")
                return true;
            if (entidad == "32" && municipio == "013")
                return true;
            if (entidad == "32" && municipio == "014")
                return true;
            if (entidad == "32" && municipio == "015")
                return true;
            if (entidad == "32" && municipio == "016")
                return true;
            if (entidad == "32" && municipio == "017")
                return true;
            if (entidad == "32" && municipio == "018")
                return true;
            if (entidad == "32" && municipio == "019")
                return true;
            if (entidad == "32" && municipio == "020")
                return true;
            if (entidad == "32" && municipio == "021")
                return true;
            if (entidad == "32" && municipio == "022")
                return true;
            if (entidad == "32" && municipio == "023")
                return true;
            if (entidad == "32" && municipio == "024")
                return true;
            if (entidad == "32" && municipio == "025")
                return true;
            if (entidad == "32" && municipio == "026")
                return true;
            if (entidad == "32" && municipio == "027")
                return true;
            if (entidad == "32" && municipio == "028")
                return true;
            if (entidad == "32" && municipio == "029")
                return true;
            if (entidad == "32" && municipio == "030")
                return true;
            if (entidad == "32" && municipio == "031")
                return true;
            if (entidad == "32" && municipio == "032")
                return true;
            if (entidad == "32" && municipio == "033")
                return true;
            if (entidad == "32" && municipio == "034")
                return true;
            if (entidad == "32" && municipio == "035")
                return true;
            if (entidad == "32" && municipio == "036")
                return true;
            if (entidad == "32" && municipio == "037")
                return true;
            if (entidad == "32" && municipio == "038")
                return true;
            if (entidad == "32" && municipio == "039")
                return true;
            if (entidad == "32" && municipio == "040")
                return true;
            if (entidad == "32" && municipio == "041")
                return true;
            if (entidad == "32" && municipio == "042")
                return true;
            if (entidad == "32" && municipio == "043")
                return true;
            if (entidad == "32" && municipio == "044")
                return true;
            if (entidad == "32" && municipio == "045")
                return true;
            if (entidad == "32" && municipio == "046")
                return true;
            if (entidad == "32" && municipio == "047")
                return true;
            if (entidad == "32" && municipio == "048")
                return true;
            if (entidad == "32" && municipio == "049")
                return true;
            if (entidad == "32" && municipio == "050")
                return true;
            if (entidad == "32" && municipio == "051")
                return true;
            if (entidad == "32" && municipio == "052")
                return true;
            if (entidad == "32" && municipio == "053")
                return true;
            if (entidad == "32" && municipio == "054")
                return true;
            if (entidad == "32" && municipio == "055")
                return true;
            if (entidad == "32" && municipio == "056")
                return true;
            if (entidad == "32" && municipio == "057")
                return true;
            if (entidad == "32" && municipio == "058")
                return true;
            else
                return false;
        }

        public void ValidacionCeldaAlerta(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Red;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        public void ValidacionCeldaVacia(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Navy;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        public void ValidacionCeldaAdvertencia(TableCell celda, string tooltip)
        {
            celda.BackColor = System.Drawing.Color.Orange;
            celda.ForeColor = System.Drawing.Color.White;
            celda.ToolTip = tooltip;
        }

        public string TarifaQuincenalTitulares(string nivel, double sumaasegurada, string tipoasegurado)
        {
            string calculo = "";
            double a = 0;

            double tarifaNivelG = 0;
            double tarifaNivelH = 0; //H,I,J
            double tarifaNivelK = 0;
            double tarifaNivelL = 0;
            double tarifaNivelM = 0;
            double tarifaNivelN = 0;
            double tarifaNivelO = 0;
            double tarifaNivelP = 0;
#pragma warning disable CS0219 // La variable 'tarifaNivelC' está asignada pero su valor nunca se usa
            double tarifaNivelC = 0;
#pragma warning restore CS0219 // La variable 'tarifaNivelC' está asignada pero su valor nunca se usa

            if (tipoasegurado == "T")
            {
                tarifaNivelG = 307.40;
                tarifaNivelH = 303.92; //H,I,J
                tarifaNivelK = 300.44;
                tarifaNivelL = 294.64;
                tarifaNivelM = 288.84;
                tarifaNivelN = 280.72;
                tarifaNivelO = 269.12;
                tarifaNivelP = 257.52;
            }
            if (tipoasegurado == "C")
            {
                tarifaNivelG = 502.28;
                tarifaNivelH = 496.48; //H,I,J
                tarifaNivelK = 487.20;
                tarifaNivelL = 477.92;
                tarifaNivelM = 466.32;
                tarifaNivelN = 454.72;
                tarifaNivelO = 436.16;
                tarifaNivelP = 416.44;
                tarifaNivelC = 488.36;
            }
            if (tipoasegurado == "H")
            {
                tarifaNivelG = 190.24;
                tarifaNivelH = 184.44; //H,I,J
                tarifaNivelK = 180.96;
                tarifaNivelL = 169.36;
                tarifaNivelM = 151.96;
                tarifaNivelN = 102.08;
                tarifaNivelO = 99.76;
                tarifaNivelP = 93.96;
            }

            switch (nivel)
            {
                case "G":
                    a = tarifaNivelG * 6;
                    calculo = a.ToString();
                    break;
                case "H":
                case "I":
                case "J":
                    a = tarifaNivelH * 6;
                    calculo = a.ToString();
                    break;
                case "K":
                    a = tarifaNivelK * 6;
                    calculo = a.ToString();
                    break;
                case "L":
                    a = tarifaNivelL * 6;
                    calculo = a.ToString();
                    break;
                case "M":
                    a = tarifaNivelM * 6;
                    calculo = a.ToString();
                    break;
                case "N":
                    a = tarifaNivelN * 6;
                    calculo = a.ToString();
                    break;
                case "O":
                    a = tarifaNivelO * 6;
                    calculo = a.ToString();
                    break;
                case "P":
                    a = tarifaNivelP * 6;
                    calculo = a.ToString();
                    break;
                default:
                    calculo = "0";
                    break;

            }
            return calculo;
        }

        public string TarifaPotenciada(string niveltabular, string sumaseguradatotal, string tipoasegurado)
        {
            string devuelto = string.Empty;

            if (tipoasegurado == "T")
            {
                foreach (var valor in TabaCalculoPotenciadaTitulares())
                {
                    if (valor.nivelTabular.Contains(niveltabular) && valor.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor.primaPotenciada;
                    }
                }
            }
            else if (tipoasegurado == "T")
            {
                foreach (var valor2 in TablaCalculoPotenciadaConyuges())
                {
                    if (valor2.nivelTabular.Contains(niveltabular) && valor2.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor2.primaPotenciada;
                    }
                }
            }
            else if (tipoasegurado == "H")
            {
                foreach (var valor3 in TablaCalculoPotenciadaHijos())
                {
                    if (valor3.nivelTabular.Contains(niveltabular) && valor3.sumaAseguradaTotal.Contains(sumaseguradatotal))
                    {
                        devuelto = valor3.primaPotenciada;
                    }
                }
            }
            else
                devuelto = "";

            return devuelto;
        }

        public List<TablasPotenciacion> TabaCalculoPotenciadaTitulares()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "40.60" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "95.12" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "99.76" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "212.28" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "17.40" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "26.68" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "97.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "103.24" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "215.76" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "30.16" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "58.00" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "149.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "157.76" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "339.88" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "64.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "154.28" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "162.40" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "359.60" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "20.88" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "45.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "160.08" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "168.20" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "382.80" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "52.20" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "66.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "78.88" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "89.32" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "153.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "162.40" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "380.48" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "37.12" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "71.92" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "84.68" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "93.96" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "147.32" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "155.44" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "393.24" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal= "111", primaPotenciada = "13.92"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "96.28" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "106.72" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "151.96" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "160.08" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "397.88" }

            };
            return listado;
        }

        public List<TablasPotenciacion> TablaCalculoPotenciadaConyuges()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "134.56" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "68.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "142.68" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "102.08" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "107.88" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "233.16" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "31.32" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "44.08" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "54.52" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "110.20" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "116.00" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "257.52" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "9.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "20.88" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "44.60" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "54.52" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "118.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "284.20" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "8.12" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "29.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "46.40" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "117.16" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "293.48" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "67.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "118.32" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "125.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "329.44" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal= "111", primaPotenciada = "13.92"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "25.52" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "47.56" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "52.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "55.68" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "89.32" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "126.44" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "133.40" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "332.92" },

            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal= "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "295", primaPotenciada = "4.64" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "333", primaPotenciada = "6.96" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "444", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "592", primaPotenciada = "34.80" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "740", primaPotenciada = "45.24" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "850", primaPotenciada = "98.60" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "1000", primaPotenciada = "104.40" },
            new TablasPotenciacion { nivelTabular = "C", sumaAseguradaTotal = "34219", primaPotenciada = "221.56" }

            };
            return listado;
        }

        public List<TablasPotenciacion> TablaCalculoPotenciadaHijos()
        {
            List<TablasPotenciacion> listado = new List<TablasPotenciacion>() {
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "333", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "444", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "592", primaPotenciada = "8.12" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "740", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "850", primaPotenciada = "17.40" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "1000", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "G", sumaAseguradaTotal = "34219", primaPotenciada = "49.88" },

            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "295", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "333", primaPotenciada = "1.16" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "444", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "592", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "740", primaPotenciada = "15.08" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "850", primaPotenciada = "19.72" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "1000", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "H", sumaAseguradaTotal = "34219", primaPotenciada = "54.52" },

            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "259", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "295", primaPotenciada = "2.32" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "333", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "444", primaPotenciada = "11.60" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "592", primaPotenciada = "18.56" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "740", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "850", primaPotenciada = "31.32" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "1000", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "K", sumaAseguradaTotal = "34219", primaPotenciada = "88.16" },

            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "222", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "259", primaPotenciada = "10.44" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "295", primaPotenciada = "12.76" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "333", primaPotenciada = "16.24" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "444", primaPotenciada = "22.04" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "592", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "740", primaPotenciada = "33.64" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "850", primaPotenciada = "38.28" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "1000", primaPotenciada = "42.92" },
            new TablasPotenciacion { nivelTabular = "L", sumaAseguradaTotal = "34219", primaPotenciada = "102.08" },

            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "185", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "222", primaPotenciada = "15.08" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "259", primaPotenciada = "24.36" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "295", primaPotenciada = "27.84" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "333", primaPotenciada = "30.16" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "444", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "592", primaPotenciada = "41.76" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "740", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "850", primaPotenciada = "53.36" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "1000", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "M", sumaAseguradaTotal = "34219", primaPotenciada = "126.44" },

            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "148", primaPotenciada = "" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "185", primaPotenciada = "34.80" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "222", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "259", primaPotenciada = "58.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "295", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "333", primaPotenciada = "63.80" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "444", primaPotenciada = "69.60" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "592", primaPotenciada = "74.24" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "740", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "850", primaPotenciada = "87.00" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "1000", primaPotenciada = "97.44" },
            new TablasPotenciacion { nivelTabular = "N", sumaAseguradaTotal = "34219", primaPotenciada = "185.60" },

            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "111", primaPotenciada = ""},
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "148", primaPotenciada = "3.48" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "185", primaPotenciada = "35.96" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "222", primaPotenciada = "48.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "259", primaPotenciada = "56.84" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "295", primaPotenciada = "59.16" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "333", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "444", primaPotenciada = "67.28" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "592", primaPotenciada = "71.92" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "740", primaPotenciada = "77.72" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "850", primaPotenciada = "83.52" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "1000", primaPotenciada = "91.64" },
            new TablasPotenciacion { nivelTabular = "O", sumaAseguradaTotal = "34219", primaPotenciada = "271.44" },

            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "111", primaPotenciada = "3.48"},
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "148", primaPotenciada = "5.80" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "185", primaPotenciada = "39.44" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "222", primaPotenciada = "51.04" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "259", primaPotenciada = "60.32" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "295", primaPotenciada = "62.64" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "333", primaPotenciada = "64.96" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "444", primaPotenciada = "70.76" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "592", primaPotenciada = "75.40" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "740", primaPotenciada = "81.20" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "850", primaPotenciada = "85.84" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "1000", primaPotenciada = "92.80" },
            new TablasPotenciacion { nivelTabular = "P", sumaAseguradaTotal = "34219", primaPotenciada = "288.84" }

            };
            return listado;
        }

        [Obsolete]
        public string Retenedor(string dependencia)
        {
            string ret = string.Empty;            

            switch (dependencia)
            {
                case "PRESIDENCIA DE LA REPUBLICA":
                    ret = "002999";
                    break;
                case "SECRETARIA DE GOBERNACION":
                    ret = "004999";
                    break;
                case "SECRETARIA DE RELACIONES EXTERIORES":
                    ret = "005999";
                    break;
                case "SECRETARIA DE HACIENDA Y CREDITO PUBLICO":
                    ret = "006999";
                    break;
                case "SECRETARIA DE AGRICULTURA, GANADERIA, DESARROLLO RURAL, PESCA Y ALIMENTACION":
                    ret = "008109";
                    break;
                case "SECRETARIA DE COMUNICACIONES Y TRANSPORTES":
                    ret = "009009";
                    break;
                case "SECRETARIA DE ECONOMIA":
                    ret = "010999";
                    break;
                case "SECRETARIA DE EDUCACION PUBLICA":
                    ret = "11_134_138";
                    break;
                case "SECRETARIA DE SALUD":
                    ret = "012009";
                    break;
                case "SECRETARIA DE MARINA":
                    ret = "013999";
                    break;
                case "SECRETARIA DEL TRABAJO Y PREVISION SOCIAL":
                    ret = "014999";
                    break;
                case "SECRETARIA DE DESARROLLO AGRARIO, TERRITORIAL Y URBANO":
                    ret = "015999";
                    break;
                case "SECRETARIA DEL MEDIO AMBIENTE Y RECURSOS NATURALES":
                    ret = "016999";
                    break;
                case "PROCURADURIA GENERAL DE LA REPUBLICA":
                    ret = "017902";
                    break;
                case "SECRETARIA DE ENERGIA":
                    ret = "018999";
                    break;
                case "SECRETARIA DE DESARROLLO SOCIAL":
                    ret = "020009";
                    break;
                case "SECRETARIA DE TURISMO":
                    ret = "021999";
                    break;
                case "SECRETARIA DE LA FUNCION PUBLICA":
                    ret = "027999";
                    break;
                case "TRIBUNAL SUPERIOR AGRARIO":
                    ret = "226009";
                    break;
                case "CONSEJERIA JURIDICA DEL EJECUTIVO FEDERAL":
                    ret = "631009";
                    break;
                case "COMISION REGULADORA DE ENERGIA":
                    ret = "248009";
                    break;
                case "COMISION NACIONAL DE HIDROCARBUROS":
                    ret = "128509";
                    break;
                case "SECRETARIA DE GOBERNACION ORGANO ADMINISTRATIVO DESCONCENTRADO PREVENCION Y READAPTACION SOCIAL":
                    ret = "562009";
                    break;
                case "SECRETARIA DE GOBERNACION ORGANO ADMINISTRATIVO DESCONCENTRADO POLICIA FEDERAL":
                    ret = "153009";
                    break;
                case "SERVICIO DE PROTECCION FEDERAL":
                    ret = "183009";
                    break;
                case "COMISION NACIONAL BANCARIA Y DE VALORES":
                    ret = "050641";
                    break;
                case "COMISION NACIONAL DE SEGUROS Y FIANZAS":
                    ret = "125009";
                    break;
                case "COMISION NACIONAL DEL SISTEMA DE AHORRO PARA EL RETIRO":
                    ret = "006091";
                    break;
                case "SERVICIO DE ADMINISTRACION TRIBUTARIA":
                    ret = "310009";
                    break;
                case "SERVICIO NACIONAL DE SANIDAD, INOCUIDAD Y CALIDAD AGROALIMENTARIA":
                    ret = "337999";
                    break;
                case "SERVICIO NACIONAL DE INSPECCION Y CERTIFICACION DE SEMILLAS":
                    ret = "369009";
                    break;
                case "COLEGIO SUPERIOR AGROPECUARIO DEL ESTADO DE GUERRERO":
                    ret = "008901";
                    break;
                case "AGENCIA DE SERVICIOS A LA COMERCIALIZACION Y DESARROLLO DE MERCADOS AGROPECUARIOS":
                    ret = "114009";
                    break;
                case "SERVICIO DE INFORMACION AGROALIMENTARIA Y PESQUERA":
                    ret = "368009";
                    break;
                case " COMISION NACIONAL DE ACUACULTURA Y PESCA":
                    ret = "796025";
                    break;
                case "INSTITUTO MEXICANO DEL TRANSPORTE":
                    ret = "009901";
                    break;
                case "SERVICIOS A LA NAVEGACION EN EL ESPACIO AEREO MEXICANO":
                    ret = "036009";
                    break;
                case "COMISION FEDERAL DE MEJORA REGULATORIA":
                    ret = "583009";
                    break;
                case "UNIVERSIDAD PEDAGOGICA NACIONAL":
                    ret = "077901";
                    break;
                case "INSTITUTO POLITECNICO NACIONAL":
                    ret = "630009";
                    break;
                case "ADMINISTRACION FEDERAL DE SERVICIOS EDUCATIVOS EN EL DISTRITO FEDERAL":
                    ret = "011133";
                    break;
                case "SECRETARIA DE CULTURA":
                    ret = "600209";
                    break;
                case "PROCURADURIA FEDERAL DE LA DEFENSA DEL TRABAJO":
                    ret = "040340";
                    break;
                case "COMITE NACIONAL MIXTO DE PROTECCION AL SALARIO":
                    ret = "014901";
                    break;
                case "REGISTRO AGRARIO NACIONAL":
                    ret = "385009";
                    break;
                case "COMISION NACIONAL DEL AGUA":
                    ret = "038009";
                    break;
                case "PROCURADURIA FEDERAL DE PROTECCION AL AMBIENTE":
                    ret = "201009";
                    break;
                case "SECRETARIA DE MEDIO AMBIENTE Y RECURSOS NATURALES, COMISION NACIONAL DE AREAS NATURALES PROTEGIDAS":
                    ret = "652999";
                    break;
                case "AGENCIA NACIONAL DE SEGURIDAD INDUSTRIAL Y DE PROTECCION ALMEDIO AMBIENTE DEL SECTOR HIDROCARBUROS":
                    ret = "193009";
                    break;
                case "COMISION NACIONAL DE SEGURIDAD NUCLEAR Y SALVAGUARDIAS":
                    ret = "444009";
                    break;
                case "COMISION NACIONAL PARA EL USO EFICIENTE DE LA ENERGIA":
                    ret = "489009";
                    break;
                case "INSTITUTO NACIONAL DE DESARROLLO SOCIAL":
                    ret = "195709";
                    break;
                case "COORDINACION NACIONAL DE PROSPERA PROGRAMA DE INCLUSION SOCIAL":
                    ret = "020903";
                    break;
                case "INSTITUTO NACIONAL DE LA ECONOMIA SOCIAL":
                    ret = "010902";
                    break;
                case "INSTITUTO DE ADMINISTRACION Y AVALUOS DE BIENES NACIONALES":
                    ret = "380009";
                    break;
                case "TALLERES GRAFICOS DE MEXICO":
                    ret = "389091";
                    break;
                case "ARCHIVO GENERAL DE LA NACION":
                    ret = "196709";
                    break;
                case "CONSEJO NACIONAL PARA PREVENIR LA DISCRIMINACION":
                    ret = "856009";
                    break;
                case "FINANCIERA NACIONAL DE DESARROLLO AGROPECUARIO, RURAL, FORESTAL Y PESQUERO":
                    ret = "784009";
                    break;
                case "INSTITUTO DE SEGURIDAD SOCIAL PARA LAS FUERZAS ARMADAS MEXICANAS":
                    ret = "533009";
                    break;
                case "COMISION NACIONAL DE LAS ZONAS ARIDAS":
                    ret = "061005";
                    break;
                case "INSTITUTO NACIONAL DE INVESTIGACIONES FORESTALES, AGRICOLAS, Y PECUARIAS":
                    ret = "341531";
                    break;
                case "INSTITUTO NACIONAL DE PESCA":
                    ret = "319009";
                    break;
                case "INSTITUTO NACIONAL PARA EL DESARROLLO DE CAPACIDADES DEL SECTOR RURAL, A.C.":
                    ret = "824009";
                    break;
                case "FIDEICOMISO DE RIESGO COMPARTIDO":
                    ret = "159609";
                    break;
                case "FONDO DE EMPRESAS EXPROPIADAS DEL SECTOR AZUCARERO":
                    ret = "786091";
                    break;
                case "CAMINOS Y PUENTES FEDERALES DE INGRESOS Y SERVICIOS CONEXOS":
                    ret = "509009";
                    break;
                case "GRUPO AEROPORTUARIO DE LA CIUDAD DE MEXICO, S.A DE C.V.":
                    ret = "507109";
                    break;
                case "CENTRO NACIONAL DE METROLOGIA":
                    ret = "372022";
                    break;
                case "PROCURADURIA FEDERAL DEL CONSUMIDOR":
                    ret = "054700";
                    break;
                case "FIDEICOMISO DE FOMENTO MINERO":
                    ret = "113799";
                    break;
                case "PROMEXICO":
                    ret = "116509";
                    break;
                case "COMISION DE OPERACION Y FOMENTO DE ACTIVIDADES ACADEMICAS DECOMISION DE OPERACION Y FOMENTO DE ACTIVIDADES ACADEMICAS DEL IPN":
                    ret = "065999";
                    break;
                case "COMISION NACIONAL DE CULTURA FISICA Y DEPORTE":
                    ret = "123009";
                    break;
                case "COMISION NACIONAL DE LIBROS DE TEXTO GRATUITOS":
                    ret = "078090";
                    break;
                case "INSTITUTO NACIONAL PARA LA EDUCACION DE LOS ADULTOS":
                    ret = "044097";
                    break;
                case "INSTITUTO NACIONAL DE LENGUAS INDIGENAS":
                    ret = "121009";
                    break;
                case "INSTITUTO MEXICANO DE CINEMATOGRAFIA":
                    ret = "279009";
                    break;
                case "INSTITUTO NACIONAL DE LA INFRAESTRUCTURA FISICA EDUCATIVA":
                    ret = "120009";
                    break;
                case "PATRONATO DE OBRAS E INSTALACIONES DEL INSTITUTO POLITECNICO NACIONAL":
                    ret = "499091";
                    break;
                case "ESTUDIOS CHURUBUSCO AZTECA, S.A":
                    ret = "500092";
                    break;
                case "TELEVISION METROPOLITANA, S.A. DE C.V.":
                    ret = "580009";
                    break;
                case "FIDEICOMISO DE LOS SISTEMAS NORMALIZADO DE COMPETENCIA LABORAL Y CERTIFICACION DE COMPETENCIA LABORAL":
                    ret = "120409";
                    break;
                case "BANCO NACIONAL DE OBRAS Y SERVICIOS PUBLICOS, S.N.C. FIDEICOMISO PARA LA CINETECA NACIONAL":
                    ret = "420009";
                    break;
                case "CENTRO REGIONAL DE ALTA ESPECIALIDAD DE CHIAPAS":
                    ret = "012307";
                    break;
                case "INSTITUTO NACIONAL DE PSIQUIATRIA RAMON DE LA FUENTE MUNIZ":
                    ret = "088009";
                    break;
                case "HOSPITAL JUAREZ DE MEXICO":
                    ret = "948099";
                    break;
                case "HOSPITAL GENERAL DR. MANUEL GEA GONZALEZ":
                    ret = "083099";
                    break;
                case "HOSPITAL GENERAL DE MEXICO DR. EDUARDO LICEAGA":
                    ret = "329009";
                    break;
                case "HOSPITAL INFANTIL DE MEXICO FEDERICO GOMEZ":
                    ret = "087009";
                    break;
                case "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DEL BAJIO":
                    ret = "012211";
                    break;
                case "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE OAXACA":
                    ret = "012220";
                    break;
                case "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE LA PENINSULA DE YUCATAN":
                    ret = "012231";
                    break;
                case "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE CIUDAD VICTORIA BICENTENARIO 2010":
                    ret = "147128";
                    break;
                case "HOSPITAL REGIONAL DE ALTA ESPECIALIDAD DE IXTAPALUCA":
                    ret = "169815";
                    break;
                case "INSTITUTO NACIONAL DE CANCEROLOGIA":
                    ret = "085009";
                    break;
                case "INSTITUTO NACIONAL DE CARDIOLOGIA IGNACIO CHAVEZ":
                    ret = "082423";
                    break;
                case "INSTITUTO NACIONAL DE ENFERMEDADES RESPIRATORIAS ISMAEL COSIO VILLEGAS":
                    ret = "084099";
                    break;
                case "INSTITUTO NACIONAL DE GERIATRIA":
                    ret = "164709";
                    break;
                case "INSTITUTO NACIONAL DE CIENCIAS MEDICAS Y NUTRICION SALVADORZUBIRAN":
                    ret = "090009";
                    break;
                case "INSTITUTO NACIONAL DE MEDICINA GENOMICA":
                    ret = "945009";
                    break;
                case "INSTITUTO NACIONAL DE NEUROLOGIA Y NEUROCIRUGIA MANUEL VELASCO SUAREZ":
                    ret = "066422";
                    break;
                case "INSTITUTO NACIONAL DE PEDIATRIA":
                    ret = "034421";
                    break;
                case "INSTITUTO NACIONAL DE PERINATOLOGIA ISIDRO ESPINOSA DE LOS REYES":
                    ret = "049099";
                    break;
                case "INSTITUTO NACIONAL DE REHABILITACION":
                    ret = "949009";
                    break;
                case "INSTITUTO NACIONAL DE SALUD PUBLICA":
                    ret = "141017";
                    break;
                case "SISTEMA NACIONAL PARA EL DESARROLLO INTEGRAL DE LA FAMILIA":
                    ret = "026999";
                    break;
                case "CENTROS DE INTEGRACION JUVENIL, A.C.":
                    ret = "109009";
                    break;
                case "COMISION NACIONAL DE LOS SALARIOS MINIMOS":
                    ret = "527009";
                    break;
                case "COMISION NACIONAL DE VIVIENDA":
                    ret = "104209";
                    break;
                case "PROCURADURIA AGRARIA":
                    ret = "327009";
                    break;
                case "FIDEICOMISO FONDO NACIONAL DE HABITACIONES POPULARES":
                    ret = "775009";
                    break;
                case "COMISION NACIONAL FORESTAL":
                    ret = "627014";
                    break;
                case "INSTITUTO MEXICANO DE TECNOLOGIA DEL AGUA":
                    ret = "103170";
                    break;
                case "INSTITUTO NACIONAL DE ECOLOGIA Y CAMBIO CLIMATICO":
                    ret = "318009";
                    break;
                case "INSTITUTO NACIONAL DE CIENCIAS PENALES":
                    ret = "443009";
                    break;
                case "CENTRO NACIONAL DE CONTROL DEL GAS NATURAL":
                    ret = "197409";
                    break;
                case "INSTITUTO NACIONAL DE LAS PERSONAS ADULTAS MAYORES":
                    ret = "086090";
                    break;
                case "CONSEJO NACIONAL DE EVALUACION DE LA POLITICA DE DESARROLLO SOCIAL":
                    ret = "992009";
                    break;
                case "DICONSA, S.A. DE C.V.":
                    ret = "136009";
                    break;
                case "FONDO NACIONAL PARA EL FOMENTO DE LAS ARTESANIAS":
                    ret = "108409";
                    break;
                case "FONATUR CONSTRUCTORA S.A. DE C.V.":
                    ret = "125709";
                    break;
                case "CONSEJO DE PROMOCION TURISTICA DE MEXICO, S.A. DE C.V.":
                    ret = "519092";
                    break;
                case "FONATUR MANTENIMIENTO TURISTICO, S.A. DE C.V.":
                    ret = "125909";
                    break;
                case "FONATUR OPERADORA PORTUARIA S.A. DE C.V.":
                    ret = "125809";
                    break;
                case "FONDO NACIONAL DE FOMENTO AL TURISMO":
                    ret = "120909";
                    break;
                case "COMISION NACIONAL PARA EL DESARROLLO DE LOS PUEBLOS INDIGENAS":
                    ret = "387009";
                    break;
                case "NOTIMEX, AGENCIA DE NOTICIAS DEL ESTADO MEXICANO":
                    ret = "106009";
                    break;
                case "PROCURADURIA DE LA DEFENSA DEL CONTRIBUYENTE":
                    ret = "006093";
                    break;
                case "COMISION EJECUTIVA DE ATENCION A VICTIMAS":
                    ret = "145809";
                    break;
                case "SISTEMA PUBLICO DE RADIODIFUSION DEL ESTADO MEXICANO":
                    ret = "144709";
                    break;
                case "INSTITUTO NACIONAL DE LAS MUJERES":
                    ret = "628009";
                    break;
                case "INSTITUTO NACIONAL DE ESTADISTICA Y GEOGRAFIA":
                    ret = "118601";
                    break;
                case "COMISION FEDERAL DE COMPETENCIA ECONOMICA":
                    ret = "010901";
                    break;
                case "INSTITUTO NACIONAL PARA LA EVALUACION DE LA EDUCACION":
                    ret = "965009";
                    break;
                case "INSTITUTO FEDERAL DE TELECOMUNICACIONES":
                    ret = "111009";
                    break;
                case "INSTITUTO NACIONAL DE TRANSPARENCIA, ACCESO A LA INFORMACION Y PROTECCION DE DATOS PERSONALES":
                    ret = "793009";
                    break;
                case "AGENCIA REGULADORA DE TRANSPORTE FERROVIARIO":
                    ret = "601609";
                    break;
                case "RADIO EDUCACION":
                    ret = "6002_309";
                    break;
                case "SECRETARIA DE CULTURA (INDAUTOR)":
                    ret = "6002_109";
                    break;
                case "SECRETARIA DE CULTURA (INHERM)":
                    ret = "6002_209";
                    break;
                case "CONSEJO NACIONAL PARA EL DESARROLLO Y LA INCLUSION DE LAS PERSONAS CON DISCAPACIDAD":
                    ret = "602909";
                    break;
                case "AUTORIDAD FEDERAL PARA EL DESARROLLO DE LAS ZONAS ECONOMICAS ESPECIALES":
                    ret = "603109";
                    break;
                case "SECRETARIA EJECUTIVA DEL SISTEMA NACIONAL ANTICORRUPCION":
                    ret = "602709";
                    break;
                case "CENTRO NACIONAL DE CONTROL DE ENERGIA":
                    ret = "602009";
                    break;
                case "ORGANISMO PROMOTOR DE INVERSIONES EN TELECOMUNICACIONES":
                    ret = "603509";
                    break;
                default:
                    ret = "Error!";
                    break;
            }
            return ret;
        }
    }

    public class NivelTabularEntity
    {
        public string IdNivelTabular { get; set; }
        public string NivelTabular { get; set; }
    }

    public class TablasPotenciacion
    {
        public string nivelTabular {get; set;}
        public string sumaAseguradaTotal { get; set; }
        public string primaPotenciada { get; set; }

    }


}
