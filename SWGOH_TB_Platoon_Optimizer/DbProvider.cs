using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SWGOH_TB_Platoon_Optimizer
{
    public abstract class DbProvider
    {
        public static void FillDataset(string sConnectionString, string _commandText, DataSet _dataSet)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);

            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(_commandText, connection);
                da.Fill(_dataSet);

                da.Dispose();
                da = null;

                connection.Close();

                connection.Dispose();
                connection = null;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "DB2 - FillDataset(" + _commandText + ", DataSet dataSet)", ex.ToString());
                //ErrorBox.Show();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Queryies database for specified data and returns it in a DataTable.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="query">Query string</param>
        /// <returns>DataTable with query data in it.</returns>
        public static DataTable FillDataTable(string sConnectionString, string query)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                da.SelectCommand.CommandTimeout = 480;
                da.Fill(dt);

                da.Dispose();
                da = null;

                connection.Close();
                connection.Dispose();
                connection = null;

                return dt;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "DB2 - FillDataTable(" + query + ")", ex.ToString());
                //ErrorBox.Show();
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Inserts data into a table.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="table">The table in which the data will be inserted.</param>
        /// <param name="columnName">The ordering of the names of the columns in the table.</param>
        /// <param name="columnData">The columnData in of a new row to be inserted</param>
        /// <param name="needID">True - Returns Scope_Identity() as int; False - returns void</param>
        /// <returns>True if the insert was successful, otherwise false.</returns>
        public static int Insert(string sConnectionString, string table, string[] columnName, bool needID, params object[] columnData)
        {
            try
            {
                if (columnName.Length != columnData.Length)
                    throw new ArgumentException("The number of columnData does not match the size of the columnName.");

                string query = "INSERT INTO " + table + " (";
                for (int i = 0; i < columnName.Length; i++)
                {
                    query += columnName[i];
                    if (i != columnName.Length - 1)
                        query += ",";
                }
                query += ") VALUES (";
                for (int i = 0; i < columnData.Length; i++)
                {
                    query += Formatted(columnData[i]);
                    if (i != columnData.Length - 1)
                        query += ",";
                }
                query += "); select Scope_Identity()";

                if (needID)
                {
                    return DBQuery(sConnectionString, query, "returnID");
                }
                else
                {
                    return DBQuery(sConnectionString, query, "insert");
                }

            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "", ex.ToString());
                //ErrorBox.Show();
                return 0;
            }
        }

        /// <summary>
        /// Changes data within a certain table.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="table">The table whose data is to be changed.</param>
        /// <param name="columnName">The names of the columns in the table to be changed.</param>
        /// <param name="whereKeys">The indexes of the whereKeys within the columnName array that will be used by WHERE</param>
        /// <param name="columnData">The new values of the row(s) to be updated.</param>
        /// <returns>True if at least one row was changed, otherwise false.</returns>
        public static int Update(string sConnectionString, string table, string[] columnName, int[] whereKeys, params object[] columnData)
        {
            try
            {
                if (columnName.Length != columnData.Length)
                    throw new ArgumentException("The number of columnData does not match the size of the columnName.");
                string query = "UPDATE " + table + " SET ";
                for (int i = 0; i < columnData.Length; i++)
                {
                    bool is_key = false;
                    for (int j = 0; j < whereKeys.Length; j++)
                        if (whereKeys[j] == i)
                            is_key = true;
                    if (!is_key)
                    {
                        query += columnName[i] + " = " + Formatted(columnData[i]);
                        if (i != columnData.Length - 1)
                            query += ",";
                    }
                }
                query += " WHERE ";
                for (int i = 0; i < whereKeys.Length; i++)
                {
                    query += columnName[whereKeys[i]] + " = " + Formatted(columnData[whereKeys[i]]);
                    if (i != whereKeys.Length - 1)
                        query += " AND ";
                }

                return DBQuery(sConnectionString, query, "update");
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "", ex.ToString());
                //ErrorBox.Show();
                return 0;
            }
        }

        /// <summary>
        /// Deletes data from a certain table.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="table">The table whose data is to be manipulated</param>
        /// <param name="whereColumn">The columns of the whereColumn associated with the deletion</param>
        /// <param name="columnData">The values of the whereColumn for the row to be deleted.</param>
        /// <returns>True if at least one row was deleted, otherwise false.</returns>
        public static int Delete(string sConnectionString, string table, string[] whereColumn, params object[] columnData)
        {
            try
            {
                if (whereColumn.Length != columnData.Length)
                    throw new ArgumentException("The number of columnData does not match the number of whereColumn.");
                string query = "DELETE FROM " + table + " WHERE ";
                for (int i = 0; i < columnData.Length; i++)
                {
                    query += whereColumn[i] + " = " + Formatted(columnData[i]);
                    if (i != columnData.Length - 1)
                        query += " AND ";
                }

                return DBQuery(sConnectionString, query, "delete");
                //return 0;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "", ex.ToString());
                //ErrorBox.Show();
                return 0;
            }
        }

        /// <summary>
        /// Executes supplied stored query
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="query">The query to be executed.</param>
        /// <param name="querytype">Type of query</param>
        /// <returns></returns>
        private static int DBQuery(string sConnectionString, string query, string querytype)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlCommand command = new SqlCommand();
            int ID = 0;

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Connection = connection;

                if (querytype == "returnID")
                    ID = Int32.Parse(command.ExecuteScalar().ToString());

                if (querytype == "insert")
                    ID = command.ExecuteNonQuery();

                if (querytype == "update")
                    ID = command.ExecuteNonQuery();

                if (querytype == "delete")
                    ID = command.ExecuteNonQuery();

                connection.Close();

                command.Dispose();
                command = null;

                connection.Dispose();
                connection = null;

                return ID;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "DB2 / DBQuery(" + query + ")", query + "\n" + ex.ToString());
                //ErrorBox.Show();
                return 0;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Executes supplied stored query
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="query">The query to be executed.</param>
        /// <returns></returns>
        public static int DBQuery(string sConnectionString, string query)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlCommand command = new SqlCommand();
            int ID = 0;

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Connection = connection;

                ID = command.ExecuteNonQuery();

                connection.Close();

                command.Dispose();
                command = null;

                connection.Dispose();
                connection = null;

                return ID;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "DB2 / DBQuery(" + query + ")", query + "\n" + ex.ToString());
                //ErrorBox.Show();
                return 0;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Executes supplied query
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="StoredProcedure">Name of the stored procedure to execute.</param>        
        /// <returns></returns>
        public static bool EXEC(string sConnectionString, string StoredProcedure)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 4000;
            command.Connection = connection;
            command.CommandText = StoredProcedure;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                command.Dispose();
                command = null;

                connection.Dispose();
                connection = null;

                return true;
            }
            catch (Exception ex)
            {
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.Message.ToString(), "DB2 / EXEC(" + StoredProcedure + ")", StoredProcedure + "\n" + ex.ToString());
                //ErrorBox.Show();
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Formats query objects to the correct SQL formatt
        /// </summary>
        /// <param name="o">NULL, bool, int, string, or DateTime</param>
        /// <returns></returns>
        private static string Formatted(object o)
        {
            if (o is System.DBNull)
                return "NULL";
            if (o is bool)
                return (bool)o ? "1" : "0";
            if (o is int)
                return ((int)o).ToString();
            if (o is string)
                return "'" + SafeSqlLiteral(o.ToString()) + "'";
            if (o is DateTime)
                return "'" + o.ToString() + "'";
            return o.ToString();
        }

        /// <summary>
        /// Replaces a single quote with two single quotes.
        /// </summary>
        /// <param name="inputSQL">String to preform replace on.</param>
        /// <returns>Safe SQL string</returns>
        public static string SafeSqlLiteral(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }

        /// <summary>
        /// Used to insert bulk records.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param> 
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="commitBatchSize"></param>
        /// <param name ="tableLock">True for table lock, false for row lock</param>
        /// <returns>True if success</returns>
        public static Boolean SqlBulkWrite(string sConnectionString, DataTable dataTable, string tableName, int commitBatchSize, bool tableLock)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 480;

            try
            {
                SqlBulkCopy bulkCopy;

                if (tableLock)
                {
                    bulkCopy =
                            new SqlBulkCopy
                            (
                            connection,
                            SqlBulkCopyOptions.TableLock |
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.UseInternalTransaction,
                            null
                            );
                }
                else
                {
                    bulkCopy =
                            new SqlBulkCopy
                            (
                            connection,
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.UseInternalTransaction,
                            null
                            );
                }

                bulkCopy.BatchSize = commitBatchSize;
                bulkCopy.DestinationTableName = tableName;
                connection.Open();

                bulkCopy.BulkCopyTimeout = 180;

                bulkCopy.WriteToServer(dataTable);

                command.Dispose();
                command = null;

                connection.Dispose();
                connection = null;
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.ToString(), "DB2 / WriteToDatabase", ex.ToString());
                //ErrorBox.Show();
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        /// <summary>
        /// Used to insert bulk records.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param> 
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="commitBatchSize"></param>
        /// <param name ="tableLock">True for table lock, false for row lock</param>
        /// <param name ="mapColumns">True if you are not supplying as many columns as needed.</param>
        /// <returns>True if success</returns>
        public static Boolean SqlBulkWrite(string sConnectionString, DataTable dataTable, string tableName, int commitBatchSize, bool tableLock, bool mapColumns)
        {
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlCommand command = new SqlCommand();

            try
            {
                SqlBulkCopy bulkCopy;

                if (tableLock)
                {
                    bulkCopy =
                            new SqlBulkCopy
                            (
                            connection,
                            SqlBulkCopyOptions.TableLock |
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.UseInternalTransaction,
                            null
                            );
                }
                else
                {
                    bulkCopy =
                            new SqlBulkCopy
                            (
                            connection,
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.UseInternalTransaction,
                            null
                            );
                }

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dataTable.Columns[i].ColumnName, dataTable.Columns[i].ColumnName));
                }

                bulkCopy.BatchSize = commitBatchSize;
                bulkCopy.DestinationTableName = tableName;
                connection.Open();

                bulkCopy.WriteToServer(dataTable);

                command.Dispose();
                command = null;

                connection.Dispose();
                connection = null;
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //ErrorBox ErrorBox;
                //ErrorBox = new ErrorBox(ex.ToString(), "DB2 / WriteToDatabase", ex.ToString());
                //ErrorBox.Show();
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }


        /// <summary>
        /// Changes data within a certain table.
        /// </summary>
        /// <param name="sConnectionString">Connection string to use.</param>
        /// <param name="table">The table whose data is to be changed.</param>
        /// <param name="columnName">The names of the columns in the table to be changed.</param>
        /// <param name="whereKeys">The indexes of the whereKeys within the columnName array that will be used by WHERE</param>
        /// <param name="columnData">The new values of the row(s) to be updated.</param>
        /// <returns>True if at least one row was changed, otherwise false.</returns>
        public static int Update(string sConnectionString, string tableName, DataTable dataTable)
        {
            using (SqlConnection conn = new SqlConnection(sConnectionString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        dataTable.Columns.Remove("TransactionLegacyKey");
                        dataTable.Columns.Remove("PointExpirationDateLpTz");
                        dataTable.Columns.Remove("PointValue");

                        conn.Open();

                        //Creating temp table on database
                        command.CommandText = "CREATE TABLE #TmpTable(Id bigint, RemainingPointValue decimal(18, 2))";
                        command.ExecuteNonQuery();

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 660;
                            bulkcopy.DestinationTableName = "#TmpTable";
                            bulkcopy.WriteToServer(dataTable);
                            bulkcopy.Close();
                        }

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 300;
                        command.CommandText = "UPDATE T SET T.RemainingPointValue = Temp.RemainingPointValue FROM dbo.Transactions as T INNER JOIN #TmpTable Temp ON Temp.Id = T.Id; DROP TABLE #TmpTable;";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return 0;
                        // Handle exception properly
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return 1;
                }
            }
            //try
            //{
            //    using (var con = new SqlConnection(sConnectionString))
            //    using (var adapter = new SqlDataAdapter("SELECT * FROM " + tableName + " WHERE AccountId = '" + accountId + "'", sConnectionString))
            //    using (new SqlCommandBuilder(adapter))
            //    {
            //        //
            //        // Fill the DataAdapter with the values in the DataTable.
            //        //
            //        adapter.Fill(dataTable);
            //        //
            //        // Open the connection to the SQL database.
            //        //
            //        con.Open();
            //        //
            //        // Insert the data table into the SQL database.
            //        //
            //        return adapter.Update(dataTable);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //ErrorBox ErrorBox;
            //    //ErrorBox = new ErrorBox(ex.Message.ToString(), "", ex.ToString());
            //    //ErrorBox.Show();
            //    return 0;
            //}
        }
    }
}