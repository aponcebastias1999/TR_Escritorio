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
    public class CliActualizarService
    {

        internal static Cliente Actualizar( Cliente dpto )
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                 p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                 p.Add(name: "inID",         direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.ID_CLIENTE);
                 p.Add(name: "inNOMBRES",    direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.NOMBRES   );
                 p.Add(name: "inAPELLIDOS",  direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.APELLIDOS );
                 p.Add(name: "inRUT",        direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.RUT       );
                 p.Add(name: "inCELULAR",    direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.CELULAR   );
                 p.Add(name: "inCORREO",     direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.CORREO    );
                 p.Add(name: "inDIRECCION",  direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.DIRECCION );
                 p.Add(name: "inCLAVE",      direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.CLAVE     );

                try
                {
                    var resultado = connection.Query<Cliente>(@"SP_ACTUALIZACLI"
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

