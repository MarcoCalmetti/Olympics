using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Olympics.Models;

namespace Olympics.Controllers
{
    static class  Partecipations
    {
        public static string connectionString { get { return ConfigurationManager.ConnectionStrings["Database"].ConnectionString; } }
        public static List<string> GetDistinctValueList(string value)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT " + value + " FROM athletes";
                    

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();
                        while (reader.Read())
                            values.Add((string)reader[value]);
                        return values;
                    }
                }catch(Exception)
                {
                    throw;
                }
            }
        }

        public static List<string> GetMedal()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Medal FROM athletes where Medal is not null";


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();
                        while (reader.Read())
                            values.Add((string)reader["Medal"]);
                        return values;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static List<string> GetSports(string Games)
        {
            if (String.IsNullOrEmpty(Games))
                return null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Sport FROM athletes WHERE Games = '" + Games + "';";


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();
                        while (reader.Read())
                            values.Add((string)reader["Sport"]);
                        return values;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static List<string> GetEvents(string Games, string Sport)
        {
            if (String.IsNullOrEmpty(Games) || String.IsNullOrEmpty(Sport))
                return null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT Event FROM athletes WHERE Games = '" + Games + "' and Sport = '" + Sport + "';";


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> values = new List<string>();
                        while (reader.Read())
                            values.Add((string)reader["Event"]);
                        return values;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static List<Partecipation> LoadDataGrid(int? PageNumber, int ElementsForPage, string FiltraNome, char? FiltraSesso, string Games, string Sport, string Event, string Medal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM athletes ";

                    if (!(FiltraNome is null && FiltraSesso is null && Games is null && Event is null && Medal is null))
                    {
                        command.CommandText += "WHERE";

                        if(!(FiltraNome is null))
                        {
                            command.CommandText += " Name LIKE @FiltraNome AND";
                            command.Parameters.AddWithValue("@FiltraNome",$"%{FiltraNome}%");
                        }

                        if (!(FiltraSesso is null))
                        {
                            command.CommandText += " Sex = @FiltraSesso AND";
                            command.Parameters.AddWithValue("@FiltraSesso", FiltraSesso);
                        }
                            
                        if(!(Games is null))
                        {
                            command.CommandText += " Games = @Games AND";
                            command.Parameters.AddWithValue("@Games", Games);
                        }
                            
                        if (!(Sport is null))
                        {
                            command.CommandText += " Sport = @Sport AND";
                            command.Parameters.AddWithValue("@Sport", Sport);
                        }
                            
                        if (!(Event is null))
                        {
                            command.CommandText += " Event = @Event AND";
                            command.Parameters.AddWithValue("@Event", Event);
                        }
                            
                        if(!(Medal is null))
                        {
                            command.CommandText += " Medal = @Medal AND";
                            command.Parameters.AddWithValue("@Medal", Medal);
                        }

                        command.CommandText = command.CommandText.Substring(0, command.CommandText.Length - 3);
                    }

                    command.CommandText += "ORDER BY Id OFFSET " + (PageNumber - 1) * ElementsForPage + " ROWS FETCH NEXT " + ElementsForPage + " ROWS ONLY; ";
                    //MessageBox.Show(command.CommandText);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Partecipation> partecipations = new List<Partecipation>();
                        while (reader.Read())
                        {
                            Partecipation temp = new Partecipation();
                            
                            temp.Id = (long)reader["Id"]; //da risolvere
                            temp.IdAthlete = reader["IdAthlete"].GetType() == typeof(System.DBNull) ? null : (long?)reader["IdAthlete"];
                            temp.Name = reader["Name"].GetType() == typeof(System.DBNull) ? null : (string)reader["Name"];
                            temp.Sex = ((string)reader["Sex"])[0];
                            temp.Age = reader["Age"].GetType() == typeof(System.DBNull) ? null : (int?)reader["Age"];
                            temp.Height = reader["Height"].GetType() == typeof(System.DBNull) ? null : (int?)reader["Height"];
                            temp.Weight = reader["Weight"].GetType() == typeof(System.DBNull) ? null : (int?)reader["Weight"];
                            temp.NOC = reader["NOC"].GetType() == typeof(System.DBNull) ? null : (string)reader["NOC"];
                            temp.Games = reader["Games"].GetType() == typeof(System.DBNull) ? null : (string)reader["Games"];
                            temp.Year = reader["Year"].GetType() == typeof(System.DBNull) ? null : (int?)reader["Year"];
                            temp.Season = reader["Season"].GetType() == typeof(System.DBNull) ? null : (string)reader["Season"];
                            temp.City = reader["City"].GetType() == typeof(System.DBNull) ? null : (string)reader["City"];
                            temp.Sport = reader["Sport"].GetType() == typeof(System.DBNull) ? null : (string)reader["Sport"];
                            temp.Event = reader["Event"].GetType() == typeof(System.DBNull) ? null : (string)reader["Event"];
                            temp.Medal = reader["Medal"].GetType() == typeof(System.DBNull) ? null : (string)reader["Medal"];
                            partecipations.Add(temp);
                        }
                        return partecipations;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static int GetTotalPages(int SelectedRPP)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM athletes ";
                    return (int)command.ExecuteScalar()/SelectedRPP;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static string StringLabelPagina(int? PageNumber, int ElementsForPage)
        {
            return "Pagina " + PageNumber + " di " + ElementsForPage;
        }

    }
}
