using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NanoTest2.Controllers
{
    [Produces("application/json")]
    [Route("api/Hello")]
    public class HelloController : Controller
    {
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "Hello", "Hello" };
		}
	}
}