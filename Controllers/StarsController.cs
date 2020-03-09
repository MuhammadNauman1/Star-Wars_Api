using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReactStartApi.DataAccess;
using ReactStartApi.DataAccess.Models;
using ReactStartApi.DTOs;

namespace ReactStartApi.Controllers
{
    [RoutePrefix("api/Stars")]
    [Authorize]
    public class StarsController : ApiController
    {
        AppDataContext _context = new AppDataContext();

     
    }
}