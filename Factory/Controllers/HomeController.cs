using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Factory.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db; // Defining the Database as Factory
    public HomeController(FactoryContext db) //constructor for the controller 
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Machine> machineList = _db.Machines.ToList();
      List<Engineer> engineerList = _db.Engineers.ToList();
      ViewBag.MachineList = machineList;
      ViewBag.EngineerList = engineerList;
      ViewBag.Machines = _db.Machines;
      ViewBag.Engineers = _db.Engineers;
      return View();
    }

  }
}