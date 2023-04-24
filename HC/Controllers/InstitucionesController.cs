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
    public class InstitucionesController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /Instituciones/
        public async Task<ActionResult> Index()
        {
            return View(await db.Instituciones.ToListAsync());
        }

        // GET: /Instituciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = await db.Instituciones.FindAsync(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }
            return View(instituciones);
        }

        // GET: /Instituciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Instituciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="InstitucionID,Nombre")] Instituciones instituciones)
        {
            if (ModelState.IsValid)
            {
                db.Instituciones.Add(instituciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(instituciones);
        }

        // GET: /Instituciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = await db.Instituciones.FindAsync(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }
            return View(instituciones);
        }

        // POST: /Instituciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="InstitucionID,Nombre")] Instituciones instituciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instituciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(instituciones);
        }

        // GET: /Instituciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instituciones instituciones = await db.Instituciones.FindAsync(id);
            if (instituciones == null)
            {
                return HttpNotFound();
            }
            return View(instituciones);
        }

        // POST: /Instituciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Instituciones instituciones = await db.Instituciones.FindAsync(id);
            db.Instituciones.Remove(instituciones);
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
