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
    public class ObrasSocialesController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /ObrasSociales/
        public async Task<ActionResult> Index()
        {
            return View(await db.ObrasSociales.ToListAsync());
        }

        // GET: /ObrasSociales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObrasSociales obrassociales = await db.ObrasSociales.FindAsync(id);
            if (obrassociales == null)
            {
                return HttpNotFound();
            }
            return View(obrassociales);
        }

        // GET: /ObrasSociales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ObrasSociales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ObraSocialID,Nombre")] ObrasSociales obrassociales)
        {
            if (ModelState.IsValid)
            {
                db.ObrasSociales.Add(obrassociales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(obrassociales);
        }

        // GET: /ObrasSociales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObrasSociales obrassociales = await db.ObrasSociales.FindAsync(id);
            if (obrassociales == null)
            {
                return HttpNotFound();
            }
            return View(obrassociales);
        }

        // POST: /ObrasSociales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ObraSocialID,Nombre")] ObrasSociales obrassociales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obrassociales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obrassociales);
        }

        // GET: /ObrasSociales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObrasSociales obrassociales = await db.ObrasSociales.FindAsync(id);
            if (obrassociales == null)
            {
                return HttpNotFound();
            }
            return View(obrassociales);
        }

        // POST: /ObrasSociales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ObrasSociales obrassociales = await db.ObrasSociales.FindAsync(id);
            db.ObrasSociales.Remove(obrassociales);
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
    }
}
