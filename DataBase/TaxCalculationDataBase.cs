using PIN_izračun.Models;
using System.Data.SqlClient;
using System.Data;

namespace PIN_izračun.DataBase
{
    public class TaxCalculationDataBase
    {
        public string SQLConectionstring = "Server=(localdb)\\MSSQLLocalDB;Database=TaxCalculation;Trusted_Connection=True";
        public SqlConnection connection = null;

        public TaxCalculationDataBase()
        {
            this.connection = new SqlConnection(this.SQLConectionstring);
        }

        public bool CreateUser(User user)
        {
            this.connection.Open();
            string sql = "INSERT INTO UserLogin(Password,Name,Email) VALUES(@param1,@param2,@param3)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, this.connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 200).Value = user.Password;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 200).Value = user.Name;
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 200).Value = user.Email;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            this.connection.Close();

            return true;
        }

        public bool CheckLogIn(string username, string password)
        {
            bool isUserValid = false;
            try
            {
                this.connection.Open();
                string qry = "SELECT * FROM UserLogin WHERE Name = @param1 AND Password = @param2";
                using (SqlCommand cmd = new SqlCommand(qry, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 200).Value = username;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 200).Value = password;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // we have user valid
                        isUserValid = true;
                    }
                }

                this.connection.Close();

                return isUserValid;
            }
            catch (Exception ex)
            {
                this.connection.Close();
                return false;
            }
        }

        public bool UpdateTaxSelected(User user, int idUser)
        {
            try
            {
                this.connection.Open();
                string qry = @"UPDATE UserLogin
                                SET SalaryBruto =  @param1,
                                City =  @param2,
                                TaxReduction =  @param3,
                                Stup =  @param4,
                                SalaryNeto = @param6
                                WHERE id = @param5
                                ";
                using (SqlCommand cmd = new SqlCommand(qry, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Decimal).Value = user.SalaryBruto;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 200).Value = user.City;
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = user.TaxReduction;
                    cmd.Parameters.Add("@param4", SqlDbType.Bit).Value = user.Stup;
                    cmd.Parameters.Add("@param5", SqlDbType.Int).Value = idUser;
                    cmd.Parameters.Add("@param6", SqlDbType.Decimal).Value = user.IznosNeto;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }

                this.connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.connection.Close();
                return false;
            }
        }

        public bool UpdateTaxNeto(User user, int idUser)
        {
            try
            {
                this.connection.Open();
                string qry = @"UPDATE UserLogin
                                SET SalaryNeto = @param1
                                WHERE id = @param2
                                ";
                using (SqlCommand cmd = new SqlCommand(qry, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Decimal).Value = user.IznosNeto;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).Value = idUser;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                }

                this.connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                this.connection.Close();
                return false;
            }
        }

        public User GetUser(int idUser)
        {
            User user = new User();
            try
            {
                this.connection.Open();
                string qry = "SELECT * FROM UserLogin WHERE Id = @param1";
                using (SqlCommand cmd = new SqlCommand(qry, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = idUser;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        user.Name = reader["Name"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.SalaryBruto = reader.GetDecimal("SalaryBruto");
                        user.City = reader.GetString("City");
                        user.TaxReduction = reader.GetString("TaxReduction");
                        user.Stup = reader.GetBoolean("Stup");
                    }
                }

                this.connection.Close();

                return user;
            }
            catch (Exception ex)
            {
                this.connection.Close();
                return null;
            }
        }

    }
}
