using App.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace App.Data
{
    public class ArtistTXDistribuidasDA:BaseConnection
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


        /// <summary>
        /// Permite obtener la lista de artistas
        /// </summary>
        /// <returns>Lista de artistas</returns>
        public List<Artist> GetAll(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "select * from Artist where Name LIKE @paramFilterByName";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();

                filterByName = $"%{filterByName}%";

                /*Confifurando los parametros*/
                cmd.Parameters.Add(
                    new SqlParameter("@paramFilterByName", filterByName)
                    );

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    artist.ArtistId = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("Name");
                    artist.Name = reader.GetString(indice);
                    result.Add(artist);
                }

               
            }
                return result;
        }

        public Artist Get(int id)
        {
            var result = new Artist();
            var sql = "select * from Artist where ArtistId = @paramID";
            using (IDbConnection cn= new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();

                /*Confifurando los parametros*/
                cmd.Parameters.Add(
                    new SqlParameter("@paramID", id)
                    
                    );

                var reader = cmd.ExecuteReader();
                var indice = 0;
                while (reader.Read())
                {
                    indice = reader.GetOrdinal("ArtistId");
                    result.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    result.Name = reader.GetString(indice);

                }
            }

                return result;
        }

        public List<Artist> GetAllSP(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "ups_GetAll";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                /*indicar que es un procedimiento almacenado*/
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cn.Open();

                filterByName = $"%{filterByName}%";

                /*Confifurando los parametros*/
                cmd.Parameters.Add(
                    new SqlParameter("@filterByName", filterByName)
                    );

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    artist.ArtistId = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("Name");
                    artist.Name = reader.GetString(indice);
                    result.Add(artist);
                }


            }
            return result;
        }


        public int Insert(Artist entity)
        {
            var result = 0;

            using (var trx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        cn.Open();

                        IDbCommand cmd = new SqlCommand("usp_InsertArtist");
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(
                            new SqlParameter("@pName", entity.Name)
                            );

                        result = Convert.ToInt32(cmd.ExecuteScalar());
                        
                    }

                    //confirma la transaccion
                    trx.Complete();

                }
                catch (Exception)
                {

                    
                }

            }

            return result;
        }

        public int Update(Artist entity)
        {
            var result = 0;

            using (var trx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        cn.Open();

                        IDbCommand cmd = new SqlCommand("usp_UpdateArtist");
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(
                            new SqlParameter("@pName", entity.Name)
                            );
                        cmd.Parameters.Add(
                            new SqlParameter("@pId", entity.ArtistId)
                            );

                        result = cmd.ExecuteNonQuery();
                        
                    }
                    trx.Complete();
                }
                catch (Exception)
                {
                    
                }
            }
       
            return result;
        }

        public int DeleteSp(int id)
        {
            var result = 0;

            using (var trx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        cn.Open();

                        IDbCommand cmd = new SqlCommand("usp_DeleteArtist");
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(
                                new SqlParameter("@pId", id
                                )
                            );

                        result = cmd.ExecuteNonQuery();
                    }
                    trx.Complete();
                }
                catch (Exception)
                {

                }

            }

           
                return result;
        }
    }
}
