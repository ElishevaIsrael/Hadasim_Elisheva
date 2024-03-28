using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DbAccess;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationAndCovidController : ControllerBase
    {
        private readonly VaccinationAndCovidDB _VacDB;

        public VaccinationAndCovidController()
        {
            _VacDB = new VaccinationAndCovidDB();
        }

        [HttpGet("vaccination/{id}")]
        public IActionResult GetVaccination(string id)
        {
            return Ok(_VacDB.GetListOfVaccinationById(id));
        }

        [HttpGet("covidstatus/{id}")]
        public IActionResult GetCovidStatus(string id)
        {
            return Ok(_VacDB.GetCovidStatus(id));
        }


        [HttpPost]
        public ActionResult AddVaccination(Vaccination vaccination)
        {
            if (_VacDB.GetVaccinaById(vaccination.IdNumber, vaccination.CoronaVaccine) != null)
            {
                return BadRequest("The vaccine  with ID number '" + vaccination.IdNumber + "' already exists.");
            }
            _VacDB.AddVaccination(vaccination);
            // Return the newly created member with status code
            return Ok(vaccination);
        }


        [HttpPost("{CovidStatus}")]
        public ActionResult SetCovidStatus(CovidStatus CovidStatus)
        {  
            if(CovidStatus.RecoveryDate< CovidStatus.PositiveTestDate)
            {
                return BadRequest("The dates aren't correct");
            }
            if (_VacDB.GetCovidStatus(CovidStatus.IdNumber) != null)
            {
                return BadRequest("The CovidStatus  with ID number '" + CovidStatus.IdNumber + "' already exists.");
            }
            _VacDB.SetCovidStatus(CovidStatus);
            // Return the newly created member with status code
            return Ok(CovidStatus);
        }


    }
}