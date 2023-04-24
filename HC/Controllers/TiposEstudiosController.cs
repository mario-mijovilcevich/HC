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
    public class TiposEstudiosController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /TiposEstudios/
        public async Task<ActionResult> Index()
        {
            return View(await db.TiposEstudios.ToListAsync());
        }

        // GET: /TiposEstudios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEstudios tiposestudios = await db.TiposEstudios.FindAsync(id);
            if (tiposestudios == null)
            {
                return HttpNotFound();
            }
            return View(tiposestudios);
        }

        // GET: /TiposEstudios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TiposEstudios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="TipoEstudioID,Descripcion")] TiposEstudios tiposestudios)
        {
            if (ModelState.IsValid)
            {
                db.TiposEstudios.Add(tiposestudios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tiposestudios);
        }

        // GET: /TiposEstudios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEstudios tiposestudios = await db.TiposEstudios.FindAsync(id);
            if (tiposestudios == null)
            {
                return HttpNotFound();
            }
            return View(tiposestudios);
        }

        // POST: /TiposEstudios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="TipoEstudioID,Descripcion")] TiposEstudios tiposestudios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposestudios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tiposestudios);
        }

        // GET: /TiposEstudios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEstudios tiposestudios = await db.TiposEstudios.FindAsync(id);
            if (tiposestudios == null)
            {
                return HttpNotFound();
            }
            return View(tiposestudios);
        }

        // POST: /TiposEstudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TiposEstudios tiposestudios = await db.TiposEstudios.FindAsync(id);
            db.TiposEstudios.Remove(tiposestudios);
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
