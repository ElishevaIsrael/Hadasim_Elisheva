using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbAccess;
using WebApplication1.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberDB _MemberDB;
        private readonly VaccinationAndCovidDB _vacDB;

        public MemberController()
        {
            _MemberDB = new MemberDB();
            _vacDB = new VaccinationAndCovidDB();
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            return Ok(_MemberDB.GetMembers());
        }

        [HttpGet("{id}")]
        public ActionResult<Members> GetMemberById(string id)
        {
            var member = _MemberDB.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpPost("AddPerson/{member}")]
        public ActionResult<Members> AddPerson(Members member)
        {
            if (!ModelState.IsValid) // holds validation errors
            {
                return BadRequest(ModelState); // Return bad request with validation errors
            }
            if (_MemberDB.GetMemberById(member.IdNumber) != null)
            {
                return BadRequest("Member with ID number '" + member.IdNumber + "' already exists.");
            }
             _MemberDB.AddMember(member);
            // Return the newly created member with status code
            return CreatedAtRoute("GetMember", new { id = member.IdNumber }, member);//return ok
        }

        [HttpDelete("MemberFromDb/{id}")]
        public IActionResult DeleteMemberFromDb(string id)
        {
            if (_MemberDB.DeleteMember(id) > 0 && _vacDB.DeleteVaccinefromMember(id)>=0 && _vacDB.DeleteCovidStatus(id)>=0)
            {
                return NoContent();  // Member deleted successfully
            }
            else
            {
                return NotFound("Member with ID number '" + id + "' not found.");
            }
        }

        [HttpPut]
        public IActionResult UpdateMember([FromBody] Members member)
        {
            if (member == null || string.IsNullOrEmpty(member.IdNumber))
            {
                return BadRequest("Member data or ID number is missing.");
            }
            if (!ModelState.IsValid) // holds validation errors
            {
                return BadRequest(ModelState); // Return bad request with validation errors
            }
            if (_MemberDB.GetMemberById(member.IdNumber) == null)
            {
                return NotFound("Member with ID number '" + member.IdNumber + "' not found.");
            }

            if (_MemberDB.UpdateMember(member) > 0)
            {
                return Ok(member); // Return the updated member data
            }
            else
            {
                return NotFound("Member with ID number '" + member.IdNumber + "' not found.");
            }
        }

    }
}
