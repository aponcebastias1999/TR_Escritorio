using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Aplicacion.Biblioteca_de_clases;
using Dapper;
using Dapper.Oracle;


namespace Aplicacion.Database
{
    public class AgregarDptoService
    {

        internal static Departamento Agregar(string ZONA_ID_ZONA, string DORMITORIOS, string BAÑOS, string COCINA, string SALA_DE_ESTAR, string BALCON, string METROS_CUADRADOS, string MANTENCION,string  DISPONIBLE, string VIGENTE, string VALOR_DIA, string RESEÑA )
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                 p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                 p.Add(name: "inZONA",    direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: ZONA_ID_ZONA);
                 p.Add(name: "inDORMITORIOS",     direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: DORMITORIOS);
                 p.Add(name: "inBAÑOS",           direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: BAÑOS);
                 p.Add(name: "inCOCINA",           direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: COCINA);
                 p.Add(name: "inSALA_DE_ESTAR",   direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: SALA_DE_ESTAR);
                 p.Add(name: "inBALCON",          direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: BALCON);
                 p.Add(name: "inMETROS_CUADRADOS",direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: METROS_CUADRADOS);
                 p.Add(name: "inMANTENCION",      direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: MANTENCION);
                 p.Add(name: "inDISPONIBLE",      direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: DISPONIBLE);
                 p.Add(name: "inVIGENTE",         direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: VIGENTE);
                 p.Add(name: "inVALOR_DIA",       direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: VALOR_DIA);
                 p.Add(name: "inRESEÑA",          direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: RESEÑA);

                try
                {
                    var resultado = connection.Query<Departamento>(@"SP_AGREGARDPTO"
                            , p
                            , commandType: System.Data.CommandType.StoredProcedure)
                            .FirstOrDefault();

                    return resultado;


                }
                catch (Exception)
                {
                    

                    return null;
                }

            }

        }

    }
}
