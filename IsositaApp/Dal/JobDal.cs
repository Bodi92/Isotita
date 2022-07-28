using Logic;
using Logic.Dtos;
using Logic.Interfacaes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class JobDal : IJobDal, ICompanyJobDal, IJobContainerDal
    {
        private AppConfiguration _config;
        private MySqlConnection conn;
        public JobDal()
        {
            _config = new AppConfiguration();
            conn = new MySqlConnection(_config.SqlConnectionString);
        }
        public void AddJob(List<JobDTO> jobDTOs)
        {
            
            string addJobQuery = "INSERT INTO job (`company_id`, `title`, `description`, `requirements`, `experience`)" +
                " VALUES(@companyId, @title, @description, @requierments, @experience)";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(addJobQuery, conn);
            foreach(JobDTO jobDTO in jobDTOs)
            {
                cmd.Parameters.AddWithValue("@companyId", jobDTO.Company_DTO.Id);
                cmd.Parameters.AddWithValue("@title", jobDTO.Title);
                cmd.Parameters.AddWithValue("@description", jobDTO.Description);
                cmd.Parameters.AddWithValue("@requierments", jobDTO.Requirements);
                cmd.Parameters.AddWithValue("@experience", jobDTO.Experience);
            }
            cmd.ExecuteReader();
            conn.Close();
        }

        public void Delete(int jobId)
        {
            string deleteJobQuery = "DELETE FROM `job` WHERE id = @jobId";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(deleteJobQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobId);
            cmd.ExecuteReader();
            conn.Close();
        }

        public void Edit(JobDTO jobDTO)
        {
            string updateJobQuery = "UPDATE `job` SET `title`=@title,`description`= @description,`requirements`= @requirements,`experience`=@experience WHERE id = @jobId";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(updateJobQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobDTO.Id);
            cmd.Parameters.AddWithValue("@title", jobDTO.Title);
            cmd.Parameters.AddWithValue("@description", jobDTO.Description);
            cmd.Parameters.AddWithValue("@requirements", jobDTO.Requirements);
            cmd.Parameters.AddWithValue("@experience", jobDTO.Experience);
            cmd.ExecuteReader();
            conn.Close();
        }

        public List<JobDTO> GetAllJobs()
        {
            List<JobDTO> jobDTOs = new List<JobDTO>();
            string getJobsPerCompanyQuery = "SELECT job.id, company_id, title, description, company.name AS companyName FROM job " +
                " INNER JOIN company ON company_id = company.id";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getJobsPerCompanyQuery, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    jobDTOs.Add(
                        new JobDTO()
                        {
                            Company_DTO = new CompanyDTO
                            {
                                Id = dataReader.GetInt32("company_id"),
                                Name = dataReader.GetString("companyName")
                            },
                            Id = dataReader.GetInt32("id"),
                            Title = dataReader.GetString("title"),
                            Description = dataReader.GetString("description")
                        });
                }
            }
            conn.Close();
            return jobDTOs;
        }

        public JobDTO GetJobById(int jobId)
        {
            JobDTO jobDTO = null;
            string getJobByIdQuery = "SELECT `company_id`, `title`, `description`, `requirements`, `experience`, company.name AS companyName FROM job " +
                "INNER JOIN company ON company_id = company.id WHERE job.id = @jobId ORDER BY job.Id ASC";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getJobByIdQuery, conn);
            cmd.Parameters.AddWithValue("@jobId", jobId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if(dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    jobDTO = new JobDTO()
                    {
                        Company_DTO = new CompanyDTO
                        {
                            Id = dataReader.GetInt32("company_id"),
                            Name = dataReader.GetString("companyName"),
                        },
                        Id = jobId,
                        Title = dataReader.GetString("title"),
                        Description = dataReader.GetString("description"),
                        Requirements = dataReader.GetString("requirements"),
                        Experience = dataReader.GetString("experience")
                    };
                }
            }
            conn.Close();
            return jobDTO;
        }

        public List<JobDTO> GetJobs(int companyId)
        {
            CompanyDTO companyDTO = new CompanyDTO();
            companyDTO.jobs = new List<JobDTO>();
            string getJobsPerCompanyQuery = "SELECT id, title, description, requirements, experience FROM" +
                " job WHERE company_id = @companyId ORDER BY id ASC";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(getJobsPerCompanyQuery, conn);
            cmd.Parameters.AddWithValue("@companyId", companyId);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    companyDTO.jobs.Add(
                        new JobDTO()
                        {
                            Id = dataReader.GetInt32("id"),
                            Title = dataReader.GetString("title"),
                            Description = dataReader.GetString("description"),
                            Requirements = dataReader.GetString("requirements"),
                            Experience = dataReader.GetString("experience"),
                        });
                }
            }
            conn.Close();
            return companyDTO.jobs;
        }
    }
}
