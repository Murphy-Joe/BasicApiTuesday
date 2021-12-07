using BasicApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BasicApi.Controllers;

public class AgentsController : ControllerBase
{

    private readonly BasicDataContext _context;

    public AgentsController(BasicDataContext context)
    {
        _context = context;
    }

    [HttpGet("/agents")]
    public async Task<ActionResult<GetAgentsResponse>> GetAllAgents()
    {

        var response = new GetAgentsResponse();
        response.Agents = await _context.Agents!
             .Where(a => a.Retired == false)
             .Select(a => new AgentResponseItem // Map
            {
                 Id = a.Id,
                 FirstName = a.FirstName,
                 LastName = a.LastName,
                 Email = a.Email,
                 Phone = a.Phone

             })
             .ToListAsync();
        return Ok(response); 


    }
}


public class GetAgentsResponse
{
    [Required]
    public List<AgentResponseItem> Agents { get; set; } = new List<AgentResponseItem>();
}

public class AgentResponseItem
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
}
