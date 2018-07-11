using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/Package")]
    public class PackageController : Controller
    {
        private readonly IMapper _autoMapper;
        public PackageController(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }
    }
}