using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    // public ActionResult Index()
    // {
    //   return View(_db.Engineers.ToList());
    // }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
          .Include(engineer => engineer.JoinEntries)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    // public ActionResult Edit(int id)
    // {
    //   var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
    //   // ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName"); // ViewBag only transfers data from controller to view
    //   return View(thisMachine);
    // }
    
    // [HttpPost]
    // public ActionResult Edit(Machine machine) //, int EngineerId
    // {
    //   // if (EngineerId != 0)
    //   // {
    //   //   _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
    //   // }
    //   _db.Entry(machine).State=EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    public ActionResult Edit(int id)
    {      
      var thisEngineer = _db.Engineers
          .Include(engineer => engineer.JoinEntries)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State=EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = engineer.EngineerId});
    }

    //     public ActionResult EditDate(int id)
    // {
    //   var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
    //   return View(thisMachine);
    // }

    // [HttpPost]
    // public ActionResult EditDate(Machine machine)
    // {
    //   _db.Entry(machine).State=EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Details", new {id = machine.MachineId});
    // }

    // public ActionResult AddEngineer(int id)
    // {
    //   var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
    //   ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
    //   return View(thisMachine);
    // }
    
    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
      {
        var returnedJoin = _db.EngineerMachine.Any(join => join.EngineerId == engineer.EngineerId && join.MachineId == MachineId);
        if (!returnedJoin) 
        {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = engineer.EngineerId});
    }   

    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
  
    [HttpPost]
    public ActionResult DeleteMachine(int joinId, Engineer engineer)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = engineer.EngineerId});
    }

  }
}