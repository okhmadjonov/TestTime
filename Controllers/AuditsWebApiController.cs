using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestTime.Models;
using TestTime.Services;

namespace TestTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsWebApiController : ControllerBase
    {

        private readonly AuditLogService _auditLogService;

        public AuditsWebApiController(AuditLogService auditLogService) => _auditLogService = auditLogService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits([FromQuery(Name = "startDate")]
                               [SwaggerParameter("Start date for filtering audits")]
                               DateTime? startDate = null,

                               [FromQuery(Name = "endDate")]
                               [SwaggerParameter("End date for filtering audits")]
                               DateTime? endDate = null)
        {
            if (!startDate.HasValue && !endDate.HasValue)
            {
                return Ok(await _auditLogService.GetAuditLogs());
            }
            if (startDate.HasValue && !endDate.HasValue)
            {
                endDate = DateTime.MaxValue;
            }
            if (!startDate.HasValue && endDate.HasValue)
            {
                startDate = DateTime.MinValue;
            }
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                // Swap the values if the start date is greater than the end date
                var temp = startDate;
                startDate = endDate;
                endDate = temp;
            }


            var audits = await _auditLogService.Filter(startDate, endDate);
            return Ok(audits);
        }
    }
}
