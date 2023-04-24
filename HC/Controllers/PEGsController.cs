using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HC.Models;

namespace HC.Controllers
{
    public class PEGsController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /PEGs/
        public async Task<ActionResult> Index()
        {
            Session.Remove("PEGID");
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            var pegs = db.PEGs.Include(p => p.HistoriasClinicas).Where(h => h.HistoriaClinicaID == id);
            return View(await pegs.ToListAsync());
        }

        // GET: /PEGs/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["PEGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEGs pegs = await db.PEGs.FindAsync(id);
            if (pegs == null)
            {
                return HttpNotFound();
            }
            return View(pegs);
        }

        // GET: /PEGs/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View();
        }

        // POST: /PEGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PEGID,HistoriaClinicaID,Fecha,Basal,MaxEsf,CfMax,CfUtil,Conclusion")] PEGs pegs)
        {
            if (ModelState.IsValid)
            {
                db.PEGs.Add(pegs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", pegs.HistoriaClinicaID);
            return View(pegs);
        }

        // GET: /PEGs/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["PEGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEGs pegs = await db.PEGs.FindAsync(id);
            if (pegs == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View(pegs);
        }

        // POST: /PEGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PEGID,HistoriaClinicaID,Fecha,Basal,MaxEsf,CfMax,CfUtil,Conclusion")] PEGs pegs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pegs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", pegs.HistoriaClinicaID);
            return View(pegs);
        }

        // GET: /PEGs/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["PEGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEGs pegs = await db.PEGs.FindAsync(id);
            if (pegs == null)
            {
                return HttpNotFound();
            }
            return View(pegs);
        }

        // POST: /PEGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PEGs pegs = await db.PEGs.FindAsync(id);
            db.PEGs.Remove(pegs);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SetPEGID(int id, char op)
        {
            Session["PEGID"] = id;

            switch (op)
            {
                case 'B':
                    return RedirectToAction("Delete");
                case 'D':
                    return RedirectToAction("Details");
                case 'E':
                    return RedirectToAction("Edit");
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}
