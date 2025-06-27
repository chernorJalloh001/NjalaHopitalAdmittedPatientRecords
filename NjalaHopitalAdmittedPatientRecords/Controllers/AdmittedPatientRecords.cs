using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AdmittedPatientRecordsController : ControllerBase
{
    private readonly NjalaHopitalContext _context;

    public AdmittedPatientRecordsController(NjalaHopitalContext context)
    {
        _context = context;
    }

    // 🔐 Require login to access all records
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdmittedPatientRecord>>> GetAll()
    {
        return await _context.AdmittedPatientRecords.ToListAsync();
    }

    // 🔐 Require login to access a specific record
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AdmittedPatientRecord>> GetById(string id)
    {
        var record = await _context.AdmittedPatientRecords.FindAsync(id);
        if (record == null)
            return NotFound();
        return record;
    }


    // 🔐 Require login to access all records
    [Authorize]
    // 🔓 Allow anyone to create — remove [Authorize] if registration is open
    [HttpPost]
    public async Task<ActionResult<AdmittedPatientRecord>> Create(AdmittedPatientRecord record)
    {
        _context.AdmittedPatientRecords.Add(record);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = record.ID }, record);
    }

    // 🔐 Require login to update
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, AdmittedPatientRecord record)
    {
        if (id != record.ID)
            return BadRequest();

        _context.Entry(record).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.AdmittedPatientRecords.AnyAsync(r => r.ID == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // 🔐 Require login to delete
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var record = await _context.AdmittedPatientRecords.FindAsync(id);
        if (record == null)
            return NotFound();

        _context.AdmittedPatientRecords.Remove(record);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
