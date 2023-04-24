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
    public class LaboratoriosController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Laboratorios/
        public async Task<ActionResult> Index()
        {
            Session.Remove("LaboratorioID");
            int? id = Convert.ToInt32(Session["HistoriaClinicaID"]);

            var laboratorios = db.Laboratorios.Include(l => l.HistoriasClinicas).Where(h => h.HistoriaClinicaID == id);
            return View(await laboratorios.ToListAsync());
        }

        // GET: /Laboratorios/Details/5
        public async Task<ActionResult> Details()
        {
            int? id = Convert.ToInt32(Session["LaboratorioID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorios laboratorios = await db.Laboratorios.FindAsync(id);
            if (laboratorios == null)
            {
                return HttpNotFound();
            }
            return View(laboratorios);
        }

        // GET: /Laboratorios/Create
        public ActionResult Create()
        {
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View();
        }

        // POST: /Laboratorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="LaboratorioID,HistoriaClinicaID,Fecha,Hto,Hb,Gb,Pq,Na,K,Glucosa,HbGlicosilada,Col,Hdl,Tg,LdlTotal,Urea,Creatinina,Otros")] Laboratorios laboratorios)
        {
            if (ModelState.IsValid)
            {
                db.Laboratorios.Add(laboratorios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", laboratorios.HistoriaClinicaID);
            return View(laboratorios);
        }

        // GET: /Laboratorios/Edit/5
        public async Task<ActionResult> Edit()
        {
            int? id = Convert.ToInt32(Session["LaboratorioID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorios laboratorios = await db.Laboratorios.FindAsync(id);
            if (laboratorios == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoriaClinicaID = Session["HistoriaClinicaID"];
            return View(laboratorios);
        }

        // POST: /Laboratorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="LaboratorioID,HistoriaClinicaID,Fecha,Hto,Hb,Gb,Pq,Na,K,Glucosa,HbGlicosilada,Col,Hdl,Tg,LdlTotal,Urea,Creatinina,Otros")] Laboratorios laboratorios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratorios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HistoriaClinicaID = new SelectList(db.HistoriasClinicas, "HistoriaClinicaID", "FactoresRiesgo", laboratorios.HistoriaClinicaID);
            return View(laboratorios);
        }

        // GET: /Laboratorios/Delete/5
        public async Task<ActionResult> Delete()
        {
            int? id = Convert.ToInt32(Session["LaboratorioID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorios laboratorios = await db.Laboratorios.FindAsync(id);
            if (laboratorios == null)
            {
                return HttpNotFound();
            }
            return View(laboratorios);
        }

        // POST: /Laboratorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Laboratorios laboratorios = await db.Laboratorios.FindAsync(id);
            db.Laboratorios.Remove(laboratorios);
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

        public ActionResult SetLaboratorioID(int id, char op)
        {
            Session["LaboratorioID"] = id;

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
