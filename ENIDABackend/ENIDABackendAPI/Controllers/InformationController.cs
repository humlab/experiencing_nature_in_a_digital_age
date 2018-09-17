using System;
using System.Collections.Generic;
using ENIDABackendAPI.Model;
using ENIDABackendAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace ENIDABackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly InformationService informationService;
        private int defaultNumberOfPoints = 5;

        public InformationController(InformationService informationService)
        {
            this.informationService = informationService;
        }
        
        // GET: api/Information
        [HttpGet]
        public IEnumerable<Information> Get()
        {
            throw new Exception();
        }

        // GET: api/Information/5
        [HttpGet("{image}/{offset}", Name = "Get")]
        public IEnumerable<Information> Get(string image, int offset)
        {
            return informationService.GetInformationForOffset(image, offset, defaultNumberOfPoints);
        }
    }
}
