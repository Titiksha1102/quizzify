using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class fetch_data
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\bganapathy.CEIIND\Source\Repos\UIProject\Test\Final.accdb";
        public QuestionDTO FetchQuestion(string qid)
        {
            QuestionDTO returnValue = new QuestionDTO();

            string randomRowQuery = $"SELECT SQID, QText, OT1, OT2, OT3, OT4, CrctAns, Image, Audio FROM DATA WHERE (QID=@qid) AND (FLAG=0) ORDER BY RND(SQID) * 100 DESC";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(randomRowQuery, connection))
                {
                    command.Parameters.AddWithValue("@qid", Convert.ToInt32(qid));

                    connection.Open();

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            returnValue.sqid = "" + reader.GetValue(reader.GetOrdinal("SQID"));
                            returnValue.question = "" + reader.GetValue(reader.GetOrdinal("QText"));
                            returnValue.option1 = ("" + reader.GetValue(reader.GetOrdinal("OT1"))).Trim();
                            returnValue.option2= ("" + reader.GetValue(reader.GetOrdinal("OT2"))).Trim();
                            returnValue.option3= ("" + reader.GetValue(reader.GetOrdinal("OT3"))).Trim();
                            returnValue.option4= ("" + reader.GetValue(reader.GetOrdinal("OT4"))).Trim();
                            returnValue.correctAnswer = ("" + reader.GetValue(reader.GetOrdinal("CrctAns"))).Trim();
                            returnValue.image = "" + reader.GetValue(reader.GetOrdinal("Image"));
                            returnValue.audio = "" + reader.GetValue(reader.GetOrdinal("Audio"));
                        }
                        else
                        {
                            Console.WriteLine("No rows found in the table.");
                        }
                    }
                }
            }

            string updateQuery = $"UPDATE DATA SET Flag = 1 WHERE qid = @qid AND sqid = @sqid";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@qid", Convert.ToInt32(qid));
                    command.Parameters.AddWithValue("@sqid", Convert.ToInt32(returnValue.sqid));

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Update successful. {rowsAffected} row(s) affected.");
                    }
                    else
                    {
                        Console.WriteLine("No rows updated. The specified ID may not exist.");
                    }
                }
            }

            return returnValue;

        }
    }
}

