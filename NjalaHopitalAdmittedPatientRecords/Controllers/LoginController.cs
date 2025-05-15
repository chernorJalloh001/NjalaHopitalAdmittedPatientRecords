using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NjalaHopitalAdmittedPatientRecords.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly NjalaHopitalContext _context;

    public LoginController(NjalaHopitalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Login>>> GetAll()
    {
        return await _context.Login.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Login>> Create(Login login)
    {
        _context.Login.Add(login);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { userName = login.UserName }, login);
    }
}
