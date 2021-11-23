using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        [HttpGet]
        public IActionResult Verify()
        {
            throw new NotImplementedException();
        }

        [HttpGet("macaddress")]
        public IActionResult MacTest()
		{
			try
			{
				var firstMacAddress = NetworkInterface
					.GetAllNetworkInterfaces()
					.Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
					.Select(nic => nic.GetPhysicalAddress().ToString())
					.FirstOrDefault();

                return Ok(firstMacAddress);
            }
			catch(Exception e)
			{
                return Unauthorized(e);
			}
        }
    }
}
