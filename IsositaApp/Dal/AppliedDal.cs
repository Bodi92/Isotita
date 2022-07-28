using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfacaes;
using Logic.Dtos;
using MySql.Data.MySqlClient;

namespace Dal
{
    public class AppliedDal : IAppliedDal, IJobAppliedDal
    {
        private AppConfiguration _config;
        private MySqlConnection conn;
        public AppliedDal()
        {
            _config = new AppConfiguration();
            conn = new MySqlConnection(_config.SqlConnectionString);
        }
        public void ApplyOnJob(List<AppliedDTO> appliedDTOs)
        {
            string applyOnJobQuery = "INSERT INTO applied (`job_id`, `email`, `cv`, `motivation_letter`) " +
               "VALUES (@jobId, @email, @cv, @motivationLetter)";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(applyOnJobQuery, conn);
            foreach(AppliedDTO appliedDTO in appliedDTOs)
            {
                cmd.Parameters.AddWithValue("@jobId", appliedDTO.Job_DTO.Id);
                cmd.Parameters.AddWithValue("@email", appliedDTO.Email);
                cmd.Parameters.AddWithValue("@cv", appliedDTO.CV);
                cmd.Parameters.AddWithValue("@motivationLetter", appliedDTO.MotivationLetter);
            }
            cmd.ExecuteReader();
            conn.Close();
        }

        public bool CheckIfAppliedOnJob(int jobId, string email)
        {
            bool exist = false;
            string checkIfExistQuery = "SELECT id FROM applied WHERE job_id = @jobId AND email = @email";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(checkIfExistQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobId);
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                exist = true;
            }
            conn.Close();
            return exist;
        }
        public void ChangeStatus(AppliedDTO appliedDTO)
        {
            string updateStatusQuery = "UPDATE applied SET status= @status, treated = @treated WHERE job_id = @jobId AND email = @email";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(updateStatusQuery, conn);
            cmd.Parameters.AddWithValue("@status", appliedDTO.Status);
            cmd.Parameters.AddWithValue("@treated", true);
            cmd.Parameters.AddWithValue("@jobId", appliedDTO.Job_DTO.Id);
            cmd.Parameters.AddWithValue("@email", appliedDTO.Email);
            cmd.ExecuteReader();
            conn.Close();
        }

        public List<AppliedDTO> GetAppliedsPerJob(int jobId)
        {
            List<AppliedDTO> appliedDTOs = new List<AppliedDTO>();
            string GetAppliedsPerJobQuery = "SELECT  `email`, `cv`, `motivation_letter`, `status`, `treated` FROM `applied` WHERE job_id = @jobId";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(GetAppliedsPerJobQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    appliedDTOs.Add(new AppliedDTO()
                    {
                        Email = dataReader.GetString("email"),
                        CV = dataReader.GetString("cv"),
                        MotivationLetter = dataReader.GetString("motivation_letter"),
                        Status = dataReader.GetBoolean("status"),
                        Treated = dataReader.GetBoolean("treated"),
                    });
                }
            }
            return appliedDTOs;
        }

        public void ChangeApplicantAppliedStatus(AppliedDTO appliedDTO)
        {
            string updateStatusQuery = "UPDATE applied SET status= @status, treated = @treated WHERE job_id = @jobId AND email = @email";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(updateStatusQuery, conn);
            cmd.Parameters.AddWithValue("@status", appliedDTO.Status);
            cmd.Parameters.AddWithValue("@treated", true);
            cmd.Parameters.AddWithValue("@jobId", appliedDTO.Job_DTO.Id);
            cmd.Parameters.AddWithValue("@email", appliedDTO.Email);
            cmd.ExecuteReader();
            conn.Close();
        }

        public AppliedDTO GetAppliedByJobIdAndEmail(int jobId, string email)
        {
            AppliedDTO appliedDTO = new AppliedDTO();
            string GetAppliedOnJobIdAndEmailQuery = "SELECT `cv`, `motivation_letter`, `status`, `treated` FROM `applied` WHERE job_id = @jobId AND email= @email";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(GetAppliedOnJobIdAndEmailQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobId);
            cmd.Parameters.AddWithValue("@email", email);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    appliedDTO.Job_DTO = new JobDTO()
                    {
                        Id = jobId
                    };
                    appliedDTO.Email = email;
                    appliedDTO.CV = dataReader.GetString("cv");
                    appliedDTO.MotivationLetter = dataReader.GetString("motivation_letter");
                    appliedDTO.Status = dataReader.GetBoolean("status");
                    appliedDTO.Treated = dataReader.GetBoolean("treated");
                }
            }
            conn.Close();
            return appliedDTO;
        }
    }
}
