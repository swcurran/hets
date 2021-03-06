﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using HETSCommon;
using System;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    /// <summary>
    /// Version Controller
    /// </summary>
    [Route("version")]
    [Route("hets/version")]
    public class VersionController : Controller
    {        
        private readonly Uri _apiServerUri;
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _configuration;

        /// <summary>
        /// Version Controller Constructor
        /// </summary>
        /// <param name="apiServerOptions"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        public VersionController(IOptions<ApiProxyServerOptions> apiServerOptions, ILoggerFactory loggerFactory, IConfigurationRoot configuration)
        {
            _apiServerUri = apiServerOptions.Value.ToUri();
            _logger = loggerFactory.CreateLogger(typeof(VersionController));
            _configuration = configuration;
        }

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetVersion()
        {
            _logger.LogInformation("[GetVersion] Get version");

            ProductVersionInfo version = GetVersion<ProductVersionInfo>(_configuration.GetSection("Constants").GetSection("VersionUrl").Value);
            version.ApplicationVersions.Add(GetApplicationVersionInfo());

            if (version.ApplicationVersions.Count > 0)
                _logger.LogInformation("[GetVersion] Version: " + version.ApplicationVersions[0].Version);

            return Ok(version);
        }

        private T GetVersion<T>(string path)
        {
            HttpClient httpClient = new HttpClient {BaseAddress = _apiServerUri};
            string content = httpClient.GetStringAsync(path).Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        private ApplicationVersionInfo GetApplicationVersionInfo()
        {
            Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            return assembly.GetApplicationVersionInfo();
        }
    }
}
