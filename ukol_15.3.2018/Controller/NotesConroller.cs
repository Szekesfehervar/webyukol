using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ukol_15_3_2018.Model;

namespace ukol_15._3._2018.NotesController
{
    public class NotesConroller : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Note Note { get; set; }
        protected Note _owner;
        private int numofnotes;
        public int NumberOfNotes { get; set; }
        public NotesConroller(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public string Author { get => _owner.OwnerId; }
        
        public void NumOfNotes()
        {
            int numofnotes = _db.Notes.Count();  
        }



        public void Authoros()
        {
            for (int i = 0; i < numofnotes; i++)
            {
                NumberOfNotes++;
            }
        }






        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Note = new Note();
            if (id == null)
            {
                //create
                return View(Note);
            }
            Note = _db.Notes.FirstOrDefault(u => u.Id == id);
            if (Note == null)
            {
                return NotFound();
            }
            return View(Note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Note.Id == 0)
                {
                    //create
                    _db.Notes.Add(Note);
                }
                else
                {
                    _db.Notes.Update(Note);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Note);
        }

    }
}
