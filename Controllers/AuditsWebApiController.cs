﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits(DateTime? startDate = null, DateTime? endDate = null)
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

            var audits = await _auditLogService.Filter(startDate, endDate);
            return Ok(audits);
        }
    }
}
