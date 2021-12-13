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
    public class EliminarDptoService
    {
       
        internal static Departamento Eliminar (Departamento dpto )
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                 p.Add(name: "outRESP", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                 p.Add(name: "inID", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value:dpto.ID_DEPTO);


                try
                {
                    var resultado = connection.Query<Departamento>(@"SP_DELETEDPTO"
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

