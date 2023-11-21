using CriminalDatabaseBackend.Features.Cases;
using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace CriminalDatabaseBackend.Features.Cases
{
    [Route("/[controller]")]
    [ApiController]
    public class CasesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public CasesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{caseTypeId}/{courtId}/{actId}/{advocateId}/{attorneyId}/{roleId}")]
        public async Task<IActionResult> AddCase([FromRoute] int caseTypeId, [FromRoute] int courtId, [FromRoute] int actId, [FromRoute] int advocateId, [FromRoute] int attorneyId, [FromRoute] int roleId, [FromBody] Cases cases)
        {
            var caseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(c => c.Id == caseTypeId);
            if (caseType == null) return NotFound();

            var court = await databaseContext.Courts.FirstOrDefaultAsync(court => court.Id == courtId);
            if (court == null) return NotFound();

            var act = await databaseContext.Acts.FirstOrDefaultAsync(act => act.Id == actId);
            if (act == null) return NotFound();

            var advocate = await databaseContext.Advocates.FirstOrDefaultAsync(advocate => advocate.Id == advocateId);
            if (advocate == null) return NotFound();

            var attorney = await databaseContext.Advocates.FirstOrDefaultAsync(attorney => attorney.Id == attorneyId);
            if (attorney == null) return NotFound();

            var role = await databaseContext.Roles.FirstOrDefaultAsync(role => role.Id == roleId);
            if (attorney == null) return NotFound();

            cases.CaseStatus = "Pending";
            cases.CaseTypeId = caseTypeId;
            cases.CourtId = courtId;
            cases.ActId = actId;
            cases.AdvocateId = advocateId;
            cases.AttorneyId = attorneyId;
            cases.RoleId = roleId;
            cases.TransferFromId = roleId;
            cases.TransferToId = roleId;

            await databaseContext.AddAsync(cases);
            await databaseContext.SaveChangesAsync();

            return Ok("Case Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cases = await databaseContext.Cases.Include(cases => cases.CaseType).Include(cases => cases.Court).Include(cases => cases.Act).Include(cases => cases.Advocate).Include(cases => cases.Attorney).Include(cases => cases.Role).ToListAsync();
            if (cases == null) return NotFound();

            return Ok(cases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cases = await databaseContext.Cases.Include(cases => cases.CaseType).Include(cases => cases.Court).Include(cases => cases.Act).Include(cases => cases.Advocate).Include(cases => cases.Attorney).Include(cases => cases.Role).FirstOrDefaultAsync(c => c.Id == id);
            if (cases == null) return NotFound();

            return Ok(cases);
        }

        [HttpGet("/TotalCases")]
        public async Task<IActionResult> AllCasesCount()
        {
            var allCasesCount = await databaseContext.Cases.CountAsync();
            if (allCasesCount == 0) return NotFound();

            return Ok(allCasesCount);
        }

        [HttpGet("/TotalCasesCount/{RoleId}")]
        public async Task<IActionResult> AllCasesCountAccCourtCount([FromRoute] int roleId)
        {
            var casesCountAccCourtCount = await databaseContext.Cases.CountAsync(c => c.RoleId == roleId);
            if (casesCountAccCourtCount == 0) return NotFound();

            return Ok(casesCountAccCourtCount);
        }

        [HttpGet("/TotalCasesDisplay/{RoleId}")]
        public async Task<IActionResult> AllCasesAccCourtList([FromRoute] int roleId)
        {
            var casesAccCourtList = await databaseContext.Cases.Where(c => c.RoleId == roleId).Include(casesAccCourtList => casesAccCourtList.CaseType).Include(casesAccCourtList => casesAccCourtList.Court).Include(casesAccCourtList => casesAccCourtList.Advocate).Include(casesAccCourtList => casesAccCourtList.Attorney).Include(casesAccCourtList => casesAccCourtList.Role).ToListAsync();
            if (casesAccCourtList == null) return NotFound();
            return Ok(casesAccCourtList);
        }
        
        [HttpGet("/TotalPendingCases/{RoleId}")]
        public async Task<IActionResult> TotalPendingCases([FromRoute] int roleId)
        {
            var totalPendingCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Pending" && t.RoleId == roleId);
            if (totalPendingCases == null) return NotFound();

            return Ok(totalPendingCases);
        }
        
        [HttpGet("/TotalCompletedCases/{RoleId}")]
        public async Task<IActionResult> TotalCompletedCases([FromRoute] int roleId)
        {
            var totalCompletedCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Completed" && t.RoleId == roleId);
            if (totalCompletedCases == null) return NotFound();

            return Ok(totalCompletedCases);
        }

        [HttpGet("/TotalRunningCases/{RoleId}")]
        public async Task<IActionResult> TotalRunningCases([FromRoute] int roleId)
        {
            var totalRunningCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Running" && t.RoleId == roleId);
            if (totalRunningCases == null) return NotFound();

            return Ok(totalRunningCases);
        }

        [HttpGet("/FetchCasesIncludingTransfered/{RoleId}")]
        public async Task<IActionResult> FetchCasesIncludingTransfered([FromRoute] int roleId)
        {
            var FetchCasesIncludingTransfered = await databaseContext.Cases.Where(f => f.TransferToId == roleId || f.TransferFromId == roleId || f.RoleId == roleId).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.TransferTo).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.CaseType).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Advocate).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Attorney).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Court).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Act).ToListAsync();
            if (FetchCasesIncludingTransfered == null) return NotFound();

            return Ok(FetchCasesIncludingTransfered);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase([FromRoute] int id, [FromBody] Cases cases)
        {
            var oldCase = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (oldCase == null) return NotFound();

            oldCase.DateFiled = cases.DateFiled;
            oldCase.CnrNumber = cases.CnrNumber;
            oldCase.Petitioner = cases.Petitioner;
            oldCase.Defendant = cases.Defendant;
            oldCase.JudgeName = cases.JudgeName;
            oldCase.Description = cases.Description;
            oldCase.CaseStatus = cases.CaseStatus;
            oldCase.Judgment = cases.Judgment;
            oldCase.Comments = cases.Comments;
            oldCase.ActId = cases.ActId;
            oldCase.CourtId = cases.CourtId;
            oldCase.CaseTypeId = cases.CaseTypeId;
            oldCase.AdvocateId = cases.AdvocateId;
            oldCase.AttorneyId = cases.AttorneyId;
            oldCase.RoleId = cases.RoleId;
            oldCase.TransferFromId = cases.TransferFromId;
            oldCase.TransferToId = cases.TransferToId;

            await databaseContext.SaveChangesAsync();
            return Ok("Case Updated !");
        }
    }
}
