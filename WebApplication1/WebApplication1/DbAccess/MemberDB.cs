using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DbAccess
{
    public class MemberDB
    {
        private readonly string connectionString;

        public MemberDB()
        {
            connectionString = "Server=DESKTOP-V6KSTL4;Database=HMO;Integrated Security=SSPI;MultipleActiveResultSets=true";
        }
        public List<Members> GetMembers()
        {
            List<Members> members = new List<Members>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Members", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string idNumber = (string)reader["IdNumber"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    string city = (string)reader["City"];
                    string street = (string)reader["Street"];
                    string number = (string)reader["Number"];
                    DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                    string phone = (string)reader["Phone"];
                    string mobilePhone = (string)reader["MobilePhone"];
                    Members member = new Members
                    {
                        IdNumber = idNumber,
                        FirstName = firstName,
                        LastName = lastName,
                        City = city,
                        DateOfBirth = dateOfBirth,
                        Phone = phone,
                        MobilePhone = mobilePhone,
                        Number = number,
                        Street = street,
                    };
                    members.Add(member);
                }

                reader.Close();
                connection.Close();
            }

            return members;
        }

        public Members GetMemberById(string id)
        {
            Members member = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Members WHERE IdNumber = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string idNumber = (string)reader["IdNumber"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        string city = (string)reader["City"];
                        string street = (string)reader["Street"];
                        string number = (string)reader["Number"];
                        DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                        string phone = (string)reader["Phone"];
                        string mobilePhone = (string)reader["MobilePhone"];

                        member = new Members
                        {
                            IdNumber = idNumber,
                            FirstName = firstName,
                            LastName = lastName,
                            City = city,
                            DateOfBirth = dateOfBirth,
                            Phone = phone,
                            MobilePhone = mobilePhone,
                            Number = number,
                            Street = street,
                        };
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors gracefully
                    throw new Exception("Error " + ex.Message, ex);
                }

                finally
                {
                    connection.Close();
                }
            }

            return member;
        }

        public void AddMember(Members member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Members(IdNumber, FirstName, LastName, City, Street, Number, DateOfBirth, Phone, MobilePhone)VALUES (@IdNumber, @FirstName, @LastName, @City, @Street, @Number, @DateOfBirth,@Phone,@MobilePhone)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", member.IdNumber);
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@City", member.City);
                    command.Parameters.AddWithValue("@Street", member.Street);
                    command.Parameters.AddWithValue("@Number", member.Number);
                    command.Parameters.AddWithValue("@DateOfBirth", member.DateOfBirth);
                    command.Parameters.AddWithValue("@Phone", member.Phone);
                    command.Parameters.AddWithValue("@MobilePhone", member.MobilePhone);
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
                    throw new Exception("Error adding member: " + ex.Message, ex);
                }

                finally
                {
                connection.Close();
                }


            }
        }

        public int DeleteMember(string idNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Members WHERE IdNumber = @IdNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdNumber", idNumber);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;  // Return the number of rows deleted
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL error
                    throw new Exception("Error deleting member: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int UpdateMember(Members member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName, Street = @Street, Number = @Number,Phone=@Phone,MobilePhone=@MobilePhone,DateOfBirth=@DateOfBirth WHERE IdNumber = @IdNumber";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Adding parameters for each member property
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@City", member.City);
                    command.Parameters.AddWithValue("@Street", member.Street);
                    command.Parameters.AddWithValue("@Number", member.Number);
                    command.Parameters.AddWithValue("@Phone", member.Phone);
                    command.Parameters.AddWithValue("@MobilePhone", member.MobilePhone);
                    command.Parameters.AddWithValue("@DateOfBirth", member.DateOfBirth);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected; // Return the number of rows updated
                }
                catch (SqlException ex)
                {
                    // Handle any potential SQL errors
                    throw new Exception("Error updating member: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
