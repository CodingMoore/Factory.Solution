using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  // [Authorize]
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    // private readonly UserManager<ApplicationUser> _userManager;

    public MachinesController(FactoryContext db)
    {
      // _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Machines.ToList());
    }

    public ActionResult Create()
    {
      // ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      _db.Machines.Add(machine);
      // if (EngineerId != 0)
      // {
      //   _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
      // }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
          .Include(machine => machine.JoinEntries)
          .ThenInclude(join => join.Engineer)
          .FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      // ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName"); // ViewBag only transfers data from controller to view
      return View(thisMachine);
    }
    
    [HttpPost]
    public ActionResult Edit(Machine machine) //, int EngineerId
    {
      // if (EngineerId != 0)
      // {
      //   _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
      // }
      _db.Entry(machine).State=EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }
    
        [HttpPost]
    public ActionResult AddEngineer(Machine machine, int EngineerId)
    {
      if (EngineerId != 0)
      // Check if EngineerId is valid
      {
        var returnedJoin = _db.EngineerMachine.Any(join => join.MachineId == machine.MachineId && join.EngineerId == EngineerId);
        // Check if "Any" of this relationship exists, returns a bool
        if (!returnedJoin) 
        {
        // if the returnedJoin for that relationship if false, then add the relationship
          _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   


    public ActionResult Delete(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
    [HttpPost]
    public ActionResult DeleteEngineer(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
