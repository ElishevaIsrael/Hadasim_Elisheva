using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DbAccess
{
    public class VaccinationAndCovidDB
    {
        private readonly string connectionString;

        public VaccinationAndCovidDB()
        {
            connectionString = "Server=DESKTOP-V6KSTL4;Database=HMO;Integrated Security=SSPI;MultipleActiveResultSets=true";
        }

        public List<Vaccination> GetListOfVaccinationById(string id)
        {
            List<Vaccination> vacList = new List<Vaccination>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(@"select * from Vaccinations where Vaccinations.IdNumber=@Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Vaccination v = new Vaccination
                        {
                            IdNumber = (string)reader["IdNumber"],
                            CoronaVaccine = (int)reader["CoronaVaccine"],
                            VaccineDate = (DateTime)reader["VaccineDate"],
                            VaccineManufacturer = (string)reader["VaccineManufacturer"]
                        };
                        vacList.Add(v);
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error " + ex.Message, ex);
                }

                finally
                {
                    connection.Close();
                }
            }

    return vacList;
}

        public Vaccination GetVaccinaById(string IdNumber,int CoronaVaccine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Vaccination vaccination = null;
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Vaccinations WHERE IdNumber = @IdNumber and CoronaVaccine=@CoronaVaccine";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", IdNumber);
                    command.Parameters.AddWithValue("@CoronaVaccine", CoronaVaccine);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string id = (string)reader["IdNumber"];
                        int CV = (int)reader["CoronaVaccine"];
                        DateTime VD = (DateTime)reader["VaccineDate"];
                        string mf = (string)reader["VaccineManufacturer"];
                        vaccination = new Vaccination
                        {
                            IdNumber = id,
                            CoronaVaccine = CV,
                            VaccineManufacturer = mf,
                            VaccineDate = VD
                        };
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL error
                    throw new Exception("Error: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }

                return vaccination;
            }
        }
 
        public void AddVaccination(Vaccination Vaccination)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Vaccinations (IdNumber,CoronaVaccine,VaccineDate,VaccineManufacturer) VALUES (@IdNumber, @CoronaVaccine,@VaccineDate,@VaccineManufacturer)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", Vaccination.IdNumber);
                    command.Parameters.AddWithValue("@CoronaVaccine", Vaccination.CoronaVaccine);
                    command.Parameters.AddWithValue("@VaccineDate", Vaccination.VaccineDate);
                    command.Parameters.AddWithValue("@VaccineManufacturer", Vaccination.VaccineManufacturer);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 1)
                    {
                        throw new Exception("Unexpected number of rows affected: " + rowsAffected);
                    }
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors gracefully
                    throw new Exception("Error adding vaccine: " + ex.Message, ex);
                }
                finally
                {
                connection.Close();
                }
            }
        }

        public void SetCovidStatus(CovidStatus CovidStatus)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO CovidStatus (IdNumber,PositiveTestDate,RecoveryDate) VALUES (@IdNumber, @PositiveTestDate,@RecoveryDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", CovidStatus.IdNumber);
                    command.Parameters.AddWithValue("@PositiveTestDate", CovidStatus.PositiveTestDate);
                    command.Parameters.AddWithValue("@RecoveryDate", CovidStatus.RecoveryDate);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 1)
                    {
                        // Handle unexpected outcome
                        throw new Exception("Unexpected number of rows affected: " + rowsAffected);
                    }
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error adding CovidStatus: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public CovidStatus GetCovidStatus(string IdNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                CovidStatus CovidStatus = null;
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM CovidStatus WHERE IdNumber = @IdNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", IdNumber);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string id = (string)reader["IdNumber"];
                        DateTime PT = (DateTime)reader["PositiveTestDate"];
                        DateTime RD = (DateTime)reader["RecoveryDate"];
                        CovidStatus = new CovidStatus
                        {
                            IdNumber = id,
                            PositiveTestDate=PT,
                            RecoveryDate=RD
                        };
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }

                return CovidStatus;
            }
        }
        public int DeleteVaccinefromMember(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Vaccinations WHERE IdNumber = @IdNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;  // Return the number of rows deleted
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error deleting vaccine: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int DeleteCovidStatus(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM CovidStatus WHERE IdNumber = @IdNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;  // Return the number of rows deleted
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error deleting CovidStatus: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


    }
}
