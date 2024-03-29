﻿using CriminalDatabaseBackend.Features.Cases;
using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Data;

namespace CriminalDatabaseBackend.Features.Cases
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class CasesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public CasesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddCase([FromBody] Cases cases)
        {
            var caseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(c => c.Id == cases.CaseTypeId);
            if (caseType == null) return NotFound("CaseType not found");

            var court = await databaseContext.Courts.FirstOrDefaultAsync(court => court.Id == cases.CourtId);
            if (court == null) return NotFound("Court not found");

            var act = await databaseContext.Acts.FirstOrDefaultAsync(act => act.Id == cases.ActId);
            if (act == null) return NotFound("Acts not found");

            var advocate = await databaseContext.Advocates.FirstOrDefaultAsync(advocate => advocate.Id == cases.AdvocateId);
            if (advocate == null) return NotFound("Advocate not found");

            var attorney = await databaseContext.Advocates.FirstOrDefaultAsync(attorney => attorney.Id == cases.AttorneyId);
            if (attorney == null) return NotFound("Advocate not found");

            var role = await databaseContext.Roles.FirstOrDefaultAsync(role => role.Id == cases.RoleId);
            if (attorney == null) return NotFound("Role not found");

            DateTime todaysDate = new DateTime();
            cases.DateFiled = todaysDate.ToString("d");
            cases.CaseStatus = "Pending";
            cases.TransferFromId = cases.CourtId;
            cases.TransferToId = cases.CourtId;

            await databaseContext.AddAsync(cases);
            await databaseContext.SaveChangesAsync();

            return Ok("Case Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cases = await databaseContext.Cases.Include(cases => cases.CaseType).Include(cases => cases.Court).Include(cases => cases.Act).Include(cases => cases.Advocate).Include(cases => cases.Attorney).Include(cases => cases.Role).ToListAsync();
            if (cases == null) return NotFound("Case not found");

            return Ok(cases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cases = await databaseContext.Cases.Include(cases => cases.CaseType).Include(cases => cases.Court).Include(cases => cases.Act).Include(cases => cases.Advocate).Include(cases => cases.Attorney).Include(cases => cases.Role).Include(cases => cases.TransferFrom).Include(cases => cases.TransferTo).FirstOrDefaultAsync(c => c.Id == id);
            if (cases == null) return NotFound("Case not found");

            return Ok(cases);
        }

        [HttpGet("/TotalCases")]
        public async Task<IActionResult> AllCasesCount()
        {
            var allCasesCount = await databaseContext.Cases.CountAsync();
            if (allCasesCount == 0) return NotFound("Cases not found");

            return Ok(allCasesCount);
        }

        [HttpGet("/TotalCasesCount/{RoleId}")]
        public async Task<IActionResult> AllCasesCountAccCourtCount([FromRoute] int roleId)
        {
            var casesCountAccCourtCount = await databaseContext.Cases.CountAsync(c => c.RoleId == roleId);
            if (casesCountAccCourtCount == 0) return NotFound("Case not found");

            return Ok(casesCountAccCourtCount);
        }

        [HttpGet("/TotalCasesDisplay/{RoleId}")]
        public async Task<IActionResult> AllCasesAccCourtList([FromRoute] int roleId)
        {
            var casesAccCourtList = await databaseContext.Cases.Where(c => c.RoleId == roleId).Include(casesAccCourtList => casesAccCourtList.CaseType).Include(casesAccCourtList => casesAccCourtList.Court).Include(casesAccCourtList => casesAccCourtList.Advocate).Include(casesAccCourtList => casesAccCourtList.Attorney).Include(casesAccCourtList => casesAccCourtList.Role).ToListAsync();
            if (casesAccCourtList == null) return NotFound("Case not found");
            return Ok(casesAccCourtList);
        }
        
        [HttpGet("/TotalPendingCases/{RoleId}")]
        public async Task<IActionResult> TotalPendingCases([FromRoute] int roleId)
        {
            var totalPendingCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Pending" && t.RoleId == roleId);
            if (totalPendingCases == null) return NotFound("Case not found");

            return Ok(totalPendingCases);
        }
        
        [HttpGet("/TotalCompletedCases/{RoleId}")]
        public async Task<IActionResult> TotalCompletedCases([FromRoute] int roleId)
        {
            var totalCompletedCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Completed" && t.RoleId == roleId);
            if (totalCompletedCases == null) return NotFound("Case not found");

            return Ok(totalCompletedCases);
        }

        [HttpGet("/TotalRunningCases/{RoleId}")]
        public async Task<IActionResult> TotalRunningCases([FromRoute] int roleId)
        {
            var totalRunningCases = await databaseContext.Cases.CountAsync(t => t.CaseStatus == "Running" && t.RoleId == roleId);
            if (totalRunningCases == null) return NotFound("Case not found");

            return Ok(totalRunningCases);
        }

        [HttpGet("/FetchCasesIncludingTransferedAccRole/{RoleId}")]
        public async Task<IActionResult> FetchCasesIncludingTransfered([FromRoute] int roleId)
        {
            var FetchCasesIncludingTransfered = await databaseContext.Cases.Where(f => f.RoleId == roleId).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.TransferTo).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.CaseType).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Advocate).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Attorney).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Court).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Act).ToListAsync();
            if (FetchCasesIncludingTransfered == null) return NotFound("Case not found");

            return Ok(FetchCasesIncludingTransfered);
        }
        
        [HttpGet("/FetchCasesIncludingTransferedAccCourt/{CourtId}")]
        public async Task<IActionResult> FetchCasesIncludingTransferedAccCourt([FromRoute] int courtId)
        {
            var FetchCasesIncludingTransfered = await databaseContext.Cases.Where(f => f.CourtId == courtId).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.TransferTo).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.CaseType).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Advocate).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Attorney).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Court).Include(FetchCasesIncludingTransfered => FetchCasesIncludingTransfered.Act).ToListAsync();
            if (FetchCasesIncludingTransfered == null) return NotFound("Case not found");

            return Ok(FetchCasesIncludingTransfered);
        }

        [HttpGet("/FetchCaesAccDate")]
        public async Task<IActionResult> FetchCaesAccDate()
        {
            DateTime today = DateTime.Now;
            string dateOnly = today.ToString("yyyy-MM-dd");

            var hearing = await databaseContext.Hearing.Where(hearing => hearing.HearingDate == dateOnly).ToListAsync();
            if (hearing == null) return NotFound("Hearing Not Found");

            // Case nu karvanu baki che 
            return Ok(hearing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase([FromRoute] int id, [FromBody] Cases cases)
        {
            var oldCase = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (oldCase == null) return NotFound("Case not found");

            oldCase.ActId = cases.ActId;
            oldCase.AdvocateId = cases.AdvocateId;
            oldCase.AttorneyId = cases.AttorneyId;
            oldCase.CaseStatus = cases.CaseStatus;
            oldCase.CaseTypeId = cases.CaseTypeId;
            oldCase.CnrNumber = cases.CnrNumber;
            oldCase.Comments = cases.Comments;
            oldCase.DateFiled = cases.DateFiled;
            oldCase.Defendant = cases.Defendant;
            oldCase.Description = cases.Description;
            oldCase.JudgeName = cases.JudgeName;
            oldCase.Judgment = cases.Judgment;
            oldCase.Petitioner = cases.Petitioner;
            oldCase.TransferFromId = cases.TransferFromId;
            oldCase.TransferToId = cases.TransferToId;

            await databaseContext.SaveChangesAsync();
            return Ok("Case Updated !");
        }
    }
}
