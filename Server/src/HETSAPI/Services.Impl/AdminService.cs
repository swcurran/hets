using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HETSAPI.Models;
using Hangfire;
using HETSAPI.Import;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace HETSAPI.Services.Impl
{
    /// <summary>
    /// Admin Service
    /// </summary>
    public class AdminService : ServiceBase, IAdminService
    {
        private readonly DbAppContext _context;
        private readonly IConfiguration _configuration;
        private readonly Object _thisLock = new Object();

        /// <summary>
        /// Create a service and set the database context
        /// </summary>
        public AdminService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, DbAppContext context) : base(httpContextAccessor, context)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult AdminImportGetAsync(string path, bool realTime)
        {
            string result;

            lock (_thisLock)
            {
                try
                {                
                    string uploadPath = _configuration["UploadPath"];

                    // serialize scoring rules from config into json string
                    IConfigurationSection scoringRules = _configuration.GetSection("SeniorityScoringRules");
                    string seniorityScoringRules = GetConfigJson(scoringRules);

                    // get connection string
                    string connectionString = GetConnectionString();

                    if (realTime)
                    {
                        // not using Hangfire
                        BcBidImport.ImportJob(null, seniorityScoringRules, connectionString, uploadPath + path);
                        result = "Import complete";
                    }
                    else
                    {
                        // use Hangfire
                        result = "Created Job: ";
                        string jobId = BackgroundJob.Enqueue(() => BcBidImport.ImportJob(null, seniorityScoringRules, connectionString, uploadPath + path));
                        result += jobId;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = @"*** Import Error ***: " + e.Message;
                }
            }

            return new ObjectResult(result);
        }

        private string GetConfigJson(IConfigurationSection scoringRules)
        {
            string jsonString = RecurseConfigJson(scoringRules);

            if (jsonString.EndsWith("},"))
            {
                jsonString = jsonString.Substring(0, jsonString.Length - 1);
            }

            return jsonString;
        }

        private string RecurseConfigJson(IConfigurationSection scoringRules)
        {
            StringBuilder temp = new StringBuilder();

            temp.Append("{");

            // check for children
            foreach (IConfigurationSection section in scoringRules.GetChildren())
            {
                temp.Append(@"""" + section.Key + @"""" + ":");

                if (section.Value == null)
                {
                    temp.Append(RecurseConfigJson(section));                    
                }
                else
                {
                    temp.Append(@"""" + section.Value + @"""" + ",");
                }
            }

            string jsonString = temp.ToString();

            if (jsonString.EndsWith(","))
            {
                jsonString = jsonString.Substring(0, jsonString.Length - 1);
            }

            jsonString = jsonString + "},";
            return jsonString;
        }

        public IActionResult AdminObfuscateGetAsync(string sourcePath, string destinationPath)
        {
            string result = "Created Obfuscation Job: ";

            lock (_thisLock)
            {
                // get upload path
                string uploadPath = _configuration["UploadPath"];

                // get connection string
                string connectionString = GetConnectionString();

                ImportUtility.CreateObfuscationDestination(uploadPath + destinationPath);

                // use Hangfire
                string jobId = BackgroundJob.Enqueue(() => BcBidImport.ObfuscationJob(null, connectionString, uploadPath + sourcePath, uploadPath + destinationPath));
                result += jobId;                
            }

            return new ObjectResult(result);
        }

        public IActionResult GetSpreadsheet(string path, string filename)
        {
            // create an excel spreadsheet that will show the data       
            lock (_thisLock)
            {
                if (_configuration != null)
                {
                    string uploadPath = _configuration["UploadPath"];
                    string fullPath = Path.Combine(uploadPath + path, filename);

                    MemoryStream memory = new MemoryStream();

                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        stream.CopyToAsync(memory).Wait();
                    }

                    memory.Position = 0;

                    FileStreamResult fileStreamResult = new FileStreamResult(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = filename
                    };

                    return fileStreamResult;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Retrieve database connection string
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string connectionString;

            lock (_thisLock)
            {               
                string host = _configuration["DATABASE_SERVICE_NAME"];
                string username = _configuration["POSTGRESQL_USER"];
                string password = _configuration["POSTGRESQL_PASSWORD"];
                string database = _configuration["POSTGRESQL_DATABASE"];

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(database))
                {
                    // When things get cleaned up properly, this is the only call we'll have to make.
                    connectionString = _configuration.GetConnectionString("HETS");
                }
                else
                {
                    // Environment variables override all other settings; same behaviour as the configuration provider when things get cleaned up. 
                    connectionString = $"Host={host};Username={username};Password={password};Database={database};";
                }
            }

            return connectionString;
        }
    }
}
