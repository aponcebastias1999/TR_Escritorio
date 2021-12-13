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
    public class ActualizarDptoService
    {

        internal static Departamento Actualizar( Departamento dpto )
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                 p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                 p.Add(name: "inID", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:              dpto.ID_DEPTO);
                 p.Add(name: "inZONA",    direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:         dpto.ZONA_ID_ZONA);
                 p.Add(name: "inDORMITORIOS",     direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.DORMITORIOS);
                 p.Add(name: "inBAÑOS",           direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.BAÑOS);
                 p.Add(name: "inCOCINA",           direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:dpto.COCINA);
                 p.Add(name: "inSALA_DE_ESTAR",   direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.SALA_DE_ESTAR);
                 p.Add(name: "inBALCON",          direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.BALCON);
                 p.Add(name: "inMETROS_CUADRADOS",direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.METROS_CUADRADOS);
                 p.Add(name: "inMANTENCION",      direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.MANTENCION);
                 p.Add(name: "inDISPONIBLE", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:         dpto.DISPONIBLE);
                 p.Add(name: "inVIGENTE",         direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.VIGENTE);
                 p.Add(name: "inVALOR_DIA",       direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.VALOR_DIA);
                 p.Add(name: "inRESEÑA", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.RESEÑA);


                try
                {
                    var resultado = connection.Query<Departamento>(@"SP_ACTUALIZARDPTO"
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

