using Microsoft.AspNetCore.Mvc;
using Template.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Template.Controllers
{
  // [Authorize]
  public class ChildsController : Controller
  {
    private readonly TemplateContext _db;
    // private readonly UserManager<ApplicationUser> _userManager;

    public ChildsController(TemplateContext db)
    {
      // _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Childs.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.ParentId = new SelectList(_db.Parents, "ParentId", "ParentName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Child child, int ParentId)
    {
      // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // item.User = currentUser;
      _db.Childs.Add(child);
      if (ParentId != 0)
      {
        _db.ParentChild.Add(new ParentChild() { ParentId = ParentId, ChildId = child.ChildId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisChild = _db.Childs
          .Include(child => child.JoinEntries)
          .ThenInclude(join => join.Parent)
          .FirstOrDefault(child => child.ChildId == id);
      return View(thisChild);
    }

    public ActionResult Edit(int id)
    {
      var thisChild = _db.Childs.FirstOrDefault(child => child.ChildId == id);
      ViewBag.ParentId = new SelectList(_db.Parents, "ParentId", "ParentName"); // ViewBag only transfers data from controller to view
      return View(thisChild);
    }
    
    [HttpPost]
    public ActionResult Edit(Child child, int ParentId)
    {
      if (ParentId != 0)
      {
        _db.ParentChild.Add(new ParentChild() { ParentId = ParentId, ChildId = child.ChildId });
      }
      _db.Entry(child).State=EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddParent(int id)
    {
      var thisChild = _db.Childs.FirstOrDefault(childs => childs.ChildId == id);
      ViewBag.ParentId = new SelectList(_db.Parents, "ParentId", "ParentName");
      return View(thisChild);
    }
    
        [HttpPost]
    public ActionResult AddParent(Child child, int ParentId)
    {
      if (ParentId != 0)
      // Check if ParentId is valid
      {
        var returnedJoin = _db.ParentChild.Any(join => join.ChildId == child.ChildId && join.ParentId == ParentId);
        // Check if "Any" of this relationship exists, returns a bool
        if (!returnedJoin) 
        {
        // if the returnedJoin for that relationship if false, then add the relationship
          _db.ParentChild.Add(new ParentChild() { ParentId = ParentId, ChildId = child.ChildId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   


    public ActionResult Delete(int id)
    {
      var thisChild = _db.Childs.FirstOrDefault(childs => childs.ChildId == id);
      return View(thisChild);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisChild = _db.Childs.FirstOrDefault(childs => childs.ChildId == id);
      _db.Childs.Remove(thisChild);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
    [HttpPost]
    public ActionResult DeleteParent(int joinId)
    {
      var joinEntry = _db.ParentChild.FirstOrDefault(entry => entry.ParentChildId == joinId);
      _db.ParentChild.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
