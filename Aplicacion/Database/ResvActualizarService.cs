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
    public class ResvActualizarService
    {

        internal static Reserva Actualizar( Reserva dpto )
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                 p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                 p.Add(name: "inID", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:                    dpto.ID_RESER);
                 p.Add(name: "inDEPARTAMENTO_ID_DEPTO", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.DEPARTAMENTO_ID_DEPTO);
                 p.Add(name: "inCLIENTE_ID_CLIENTE   ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.CLIENTE_ID_CLIENTE);
                 p.Add(name: "inFECHA_INICIO         ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.FECHA_INICIO);
                 p.Add(name: "inFECHA_TERMINO        ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.FECHA_TERMINO);
                 p.Add(name: "inMONTO_INICIAL        ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.MONTO_INICIAL);
                 p.Add(name: "inPOR_PAGAR            ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.POR_PAGAR);
                 p.Add(name: "inTOTAL_ACTIVIDADES    ", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.TOTAL_ACTIVIDADES);
                 p.Add(name: "inTOTAL_PAGAR", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.TOTAL_PAGAR);
                p.Add(name:  "inCANCELADA", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: dpto.CANCELADA);

                //try
                {
                    var resultado = connection.Query<Reserva>(@"SP_ACTUALIZARRESV"
                            , p
                            , commandType: System.Data.CommandType.StoredProcedure)
                            .FirstOrDefault();

                    return resultado;


                }
                //catch (Exception)
                {
                    

                //    return null;
                }

            }

        }

    }
}

