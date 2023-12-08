using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTime.Services;

namespace TestTime.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AuditController : Controller
    {
        private readonly AuditLogService _auditService;

        public AuditController(AuditLogService auditService)
        {
            _auditService = auditService;
        }

        public async Task<ViewResult> Index()
        {
            var auditLogs = await _auditService.GetAuditLogs();
            return View("Audit", auditLogs);
        }

        [HttpPost]
        public async Task<ViewResult> FilterAuditLog(DateTime? startDate, DateTime? endDate)
        {
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;

            var filteredAuditLogs = await _auditService.Filter(startDate, endDate);

            return View("Audit", filteredAuditLogs);
        }
    }
}
