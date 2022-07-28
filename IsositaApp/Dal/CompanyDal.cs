using Logic;
using Logic.Interfacaes;
using Logic.Dtos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class CompanyDal : ICompanyDal
    {
        private AppConfiguration _config;
        private MySqlConnection conn;
        public CompanyDal()
        {
            _config = new AppConfiguration();
            conn = new MySqlConnection(_config.SqlConnectionString);
        }

        public void AddCompany(List<CompanyDTO> companyDTOs)
        {
            string addCompanyQuery = "INSERT INTO `company`(`name`, `email`, `password`, `mobile_number`, `address`, `city`, `zipcode`)" +
                " VALUES (@name, @email, @password, @mobileNumber, @address, @city, @zipcode)";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(addCompanyQuery, conn);
            foreach(CompanyDTO companyDTO in companyDTOs)
            {
                cmd.Parameters.AddWithValue("@name", companyDTO.Name);
                cmd.Parameters.AddWithValue("@email", companyDTO.Email);
                cmd.Parameters.AddWithValue("@password", companyDTO.Password);
                cmd.Parameters.AddWithValue("@mobileNumber", companyDTO.MobileNumber);
                cmd.Parameters.AddWithValue("@address", companyDTO.Address);
                cmd.Parameters.AddWithValue("@city", companyDTO.City);
                cmd.Parameters.AddWithValue("@zipcode", companyDTO.Zipcode);
            }
            cmd.ExecuteReader();
            conn.Close();
        }

        public bool CheckIfCompanyExist(string email)
        {
            bool exist = false;
            string CheckIfCompanyExistQuery = "SELECT id FROM company where email = @email";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(CheckIfCompanyExistQuery, conn);
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                exist = true;
            }
            conn.Close();
            return exist;
        }

        public CompanyDTO GetCompanyById(int companyId)
        {
            CompanyDTO companyDTO = new CompanyDTO();
            string getCompanyById = "SELECT name, email, mobile_number, address, city, zipcode FROM company WHERE id = @companyId";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getCompanyById, conn);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                companyDTO.Id = companyId;
                companyDTO.Name = dataReader.GetString("name");
                companyDTO.Email = dataReader.GetString("email");
                companyDTO.MobileNumber = dataReader.GetString("mobile_number");
                companyDTO.Address = dataReader.GetString("address");
                companyDTO.City = dataReader.GetString("city");
                companyDTO.Zipcode = dataReader.GetString("zipcode");
            }
            conn.Close();
            return companyDTO;
        }

        public int GetCompanyId(string email, string password)
        {
            string getCompanyId = "SELECT id FROM company WHERE email = @email AND password = @password";
            CompanyDTO companyDTO = new CompanyDTO();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getCompanyId, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            while(dataReader.Read())
            {
                companyDTO.Id = dataReader.GetInt32("id");
            }
            conn.Close();
            return companyDTO.Id;
        }
    }
}
