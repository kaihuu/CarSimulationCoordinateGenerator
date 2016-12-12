using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace SegmentInserter
{
    class DatabaseAccessor
    {
        
        public static DataTable LinkTableGetter2(int id)
        {

            string cn = @"Data Source=ECOLOGDB;Initial Catalog=ECOLOGDBver2;Integrated Security=True";//接続DB

            DataTable dt = new DataTable();


            string query = "WITH Corres AS (select l1.NUM,MIN(ABS(l2.NUM - l1.NUM)) as diff from LINKS as l1,LINKS as l2,SEMANTIC_LINKS";
            query += " where l1.NUM != l2.NUM and l1.LINK_ID = l2.LINK_ID and l1.LINK_ID = SEMANTIC_LINKS.LINK_ID and SEMANTIC_LINK_ID in ("+ id + ")";
            query += " group by l1.NUM) select l1.LINK_ID as LINK_ID , l1.NUM, l1.LATITUDE as START_LAT, l1.LONGITUDE as START_LONG,";
            query += " l2.LATITUDE as END_LAT, l2.LONGITUDE as END_LONG ,[dbo].[Hubeny](l1.LATITUDE,l1.LONGITUDE,l2.LATITUDE,l2.LONGITUDE) as DISTANCE";
            query += " from LINKS as l1,LINKS as l2,Corres where l1.NUM = Corres.NUM and l1.LINK_ID = l2.LINK_ID and ABS(l2.NUM-l1.NUM) =  Corres.diff";

            using (SqlConnection SQLConn = new SqlConnection(cn))
            {
                SQLConn.FireInfoMessageEventOnUserErrors = false;

                SqlDataAdapter da = new SqlDataAdapter(query, cn);

                //DBからデータを取得しDataTableへ格納
                try
                {
                    SQLConn.Open();
                    SqlCommand cmd = new SqlCommand(query, SQLConn);
                    cmd.CommandTimeout = 600;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    SQLConn.Close();
                }
            }

            return dt;
        }
        public static void InsertSegment(List<SegmentData> result)
        {
            string cn = @"Data Source=ECOLOGDB;Initial Catalog=ECOLOGDBver2;Integrated Security=True";//接続DB

            String insertString;
            for (int i = 0; i < result.Count; i++) {
                insertString = makeQuerySegmentData(result[i]);

                using (SqlConnection sqlConnection = new SqlConnection(cn))
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;
                    try
                    {
                        sqlCommand.CommandText = insertString;
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        sqlTransaction.Rollback();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }
        public static void InsertLinkList(List<LinkListData> result)
        {
            string cn = @"Data Source=ECOLOGDB;Initial Catalog=ECOLOGDBver2;Integrated Security=True";//接続DB

            String insertString;
            for (int i = 0; i < result.Count; i++)
            {
                insertString = makeQueryLinkList(result[i]);

                using (SqlConnection sqlConnection = new SqlConnection(cn))
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;
                    try
                    {
                        sqlCommand.CommandText = insertString;
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        sqlTransaction.Rollback();
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }
        public static string makeQuerySegmentData(SegmentData data)
        {
            string query = "INSERT INTO [100M_SEGMENT] VALUES ('" + data.SEGMENT_ID + "','" + data.SEMANTIC_LINK_ID + "','" + data.START_LINK_ID + "','";
            query += data.START_NUM + "','" + data.START_POINT_OFFSET + "')";

            return query;
        }
        public static string makeQueryLinkList(LinkListData data)
        {
            string query = "INSERT INTO LINK_LIST VALUES ('" + data.SEGMENT_ID + "','" + data.SEMANTIC_LINK_ID + "','" + data.LINK_NUMBER + "','";
            query += data.LINK_ID + "')";

            return query;
        }
    }
}
