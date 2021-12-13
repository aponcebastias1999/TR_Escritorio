using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using Dapper.Oracle;


namespace Aplicacion.Database
{
    public class LoginService
    {

        internal static Usuario Login(string correo, string pass)
        {

            using (var connection = OracleProvider.GetConnection())
            {
                var p = new OracleDynamicParameters();
                p.Add(name: "out_resp", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);
                p.Add(name: "IN_CORREO", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: correo);
                p.Add(name: "IN_PASS", direction: System.Data.ParameterDirection.Input, dbType: OracleMappingType.Varchar2, value: pass);


                var resultado = connection.Query<Usuario>(@"SP_LOGIN"
                                            , p
                                            , commandType: System.Data.CommandType.StoredProcedure)
                                            .FirstOrDefault();
                return resultado;
                

            }

        }

    }
}
