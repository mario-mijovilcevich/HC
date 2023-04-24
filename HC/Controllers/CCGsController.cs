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
    public class CCGsController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /CCGs/
        public async Task<ActionResult> Index()
        {
            Session.Remove("CCGID");
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            var ccgs = db.CCGs.Include(c => c.HistoriasClinicas).Where(h => h.HistoriaClinicaID == id);
            return View(await ccgs.ToListAsync());
        }

        // GET: /CCGs/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["CCGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCGs ccgs = await db.CCGs.FindAsync(id);
            if (ccgs == null)
            {
                return HttpNotFound();
            }
            return View(ccgs);
        }

        // GET: /CCGs/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View();
        }

        // POST: /CCGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CCGID,HistoriaClinicaID,Fecha,Tci,Da,Cx,Cd,Conclusion")] CCGs ccgs)
        {
            if (ModelState.IsValid)
            {
                db.CCGs.Add(ccgs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", ccgs.HistoriaClinicaID);
            return View(ccgs);
        }

        // GET: /CCGs/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["CCGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCGs ccgs = await db.CCGs.FindAsync(id);
            if (ccgs == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View(ccgs);
        }

        // POST: /CCGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CCGID,HistoriaClinicaID,Fecha,Tci,Da,Cx,Cd,Conclusion")] CCGs ccgs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ccgs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", ccgs.HistoriaClinicaID);
            return View(ccgs);
        }

        // GET: /CCGs/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["CCGID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CCGs ccgs = await db.CCGs.FindAsync(id);
            if (ccgs == null)
            {
                return HttpNotFound();
            }
            return View(ccgs);
        }

        // POST: /CCGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CCGs ccgs = await db.CCGs.FindAsync(id);
            db.CCGs.Remove(ccgs);
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

        public ActionResult SetCCGID(int id, char op)
        {
            Session["CCGID"] = id;

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
