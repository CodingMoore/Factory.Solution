using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
  public class EngineersController : Controller // allows EngineersController to operate as a Controller
  {
    private readonly FactoryContext _db; // Defining the Database as Factory
    public EngineersController(FactoryContext db) //constructor for the controller 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.ToList();
      return View(model);
    }

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
      var thisEngineer = _db.Engineers //return Engineer name and id 
          .Include(engineer => engineer.JoinEntries) //find machines(JoinEntries) related to the engineer
          .ThenInclude(join => join.Machine) //With all join entries add the related machine 
          .FirstOrDefault(engineer => engineer.EngineerId == id); // find the Engineer that matches the ID
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id); // finds the first match and assigns it to "thisEngineer".
      return  View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer) //engineer is an object that contains all properties, not just the ID
    {
      _db.Entry(engineer).State = EntityState.Modified; // holding the information in a bucket
      _db.SaveChanges();// pour the bucket into the database
      return RedirectToAction("Index"); //returning to index page in engineers
    }

    public ActionResult Delete(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}