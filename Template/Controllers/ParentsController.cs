using Microsoft.AspNetCore.Mvc;
using Template.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Template.Controllers
{
  public class ParentsController : Controller // allows ParentsController to operate as a Controller
  {
    private readonly TemplateContext _db; // Defining the Database as Template
    public ParentsController(TemplateContext db) //constructor for the controller 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Parent> model = _db.Parents.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Parent parent)
    {
      _db.Parents.Add(parent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisParent = _db.Parents //return Parent name and id 
          .Include(parent => parent.JoinEntries) //find childs(JoinEntries) related to the parent
          .ThenInclude(join => join.Child) //With all join entries add the related child 
          .FirstOrDefault(parent => parent.ParentId == id); // find the Parent that matches the ID
      return View(thisParent);
    }

    public ActionResult Edit(int id)
    {
      var thisParent = _db.Parents.FirstOrDefault(parent => parent.ParentId == id); // finds the first match and assigns it to "thisParent".
      return  View(thisParent);
    }

    [HttpPost]
    public ActionResult Edit(Parent parent) //parent is an object that contains all properties, not just the ID
    {
      _db.Entry(parent).State = EntityState.Modified; // holding the information in a bucket
      _db.SaveChanges();// pour the bucket into the database
      return RedirectToAction("Index"); //returning to index page in parents
    }

    public ActionResult Delete(int id)
    {
      var thisParent = _db.Parents.FirstOrDefault(parent => parent.ParentId == id);
      return View(thisParent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisParent = _db.Parents.FirstOrDefault(parent => parent.ParentId == id);
      _db.Parents.Remove(thisParent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}