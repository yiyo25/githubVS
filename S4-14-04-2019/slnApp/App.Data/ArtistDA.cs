using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public class ArtistDA:BaseConnection
    {
        /// <summary>
        /// Permite obtener la cantidad de registros que existen en la tabla artista
        /// </summary>
        /// <returns>Retorna un número de registros</returns>
        public int GetCount()
        {
            var result = 0;
            var sql = "Select count(1) from Artist";
            /*1.- creando la instancia del objeto connection*/
            using (IDbConnection cn=new SqlConnection(this.ConnectionString))
            {
                /*2.- Creando el objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();//abriendo la conexion a la base de datos

                result = (int)cmd.ExecuteScalar();
                

            }


                return result;
        }
    }
}
