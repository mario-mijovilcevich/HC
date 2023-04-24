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
    public class TiposDocumentosController : Controller
    {
        private HCContext db = new HCContext();

        // GET: /TiposDocumentos/
        public async Task<ActionResult> Index()
        {
            return View(await db.TiposDocumentos.ToListAsync());
        }

        // GET: /TiposDocumentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDocumentos tiposdocumentos = await db.TiposDocumentos.FindAsync(id);
            if (tiposdocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposdocumentos);
        }

        // GET: /TiposDocumentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TiposDocumentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="TipoDocumentoID,Description")] TiposDocumentos tiposdocumentos)
        {
            if (ModelState.IsValid)
            {
                db.TiposDocumentos.Add(tiposdocumentos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tiposdocumentos);
        }

        // GET: /TiposDocumentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDocumentos tiposdocumentos = await db.TiposDocumentos.FindAsync(id);
            if (tiposdocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposdocumentos);
        }

        // POST: /TiposDocumentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="TipoDocumentoID,Description")] TiposDocumentos tiposdocumentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposdocumentos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tiposdocumentos);
        }

        // GET: /TiposDocumentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposDocumentos tiposdocumentos = await db.TiposDocumentos.FindAsync(id);
            if (tiposdocumentos == null)
            {
                return HttpNotFound();
            }
            return View(tiposdocumentos);
        }

        // POST: /TiposDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TiposDocumentos tiposdocumentos = await db.TiposDocumentos.FindAsync(id);
            db.TiposDocumentos.Remove(tiposdocumentos);
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
