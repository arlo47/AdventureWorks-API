using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Aventureworks_Final_2.Models;

namespace Aventureworks_Final_2.Controllers
{
    public class SalesTerritoriesController : ApiController
    {
        private AdventureWorks2017Entities db = new AdventureWorks2017Entities();

        // GET: api/SalesTerritories
        public IQueryable<SalesTerritory> GetSalesTerritories()
        {
            return db.SalesTerritories;
        }

        // GET: api/SalesTerritories/5
        [ResponseType(typeof(SalesTerritory))]
        public async Task<IHttpActionResult> GetSalesTerritory(int id)
        {
            SalesTerritory salesTerritory = await db.SalesTerritories.FindAsync(id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            return Ok(salesTerritory);
        }

        // PUT: api/SalesTerritories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesTerritory(int id, SalesTerritory salesTerritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesTerritory.TerritoryID)
            {
                return BadRequest();
            }

            db.Entry(salesTerritory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesTerritoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SalesTerritories
        [ResponseType(typeof(SalesTerritory))]
        public async Task<IHttpActionResult> PostSalesTerritory(SalesTerritory salesTerritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesTerritories.Add(salesTerritory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = salesTerritory.TerritoryID }, salesTerritory);
        }

        // DELETE: api/SalesTerritories/5
        [ResponseType(typeof(SalesTerritory))]
        public async Task<IHttpActionResult> DeleteSalesTerritory(int id)
        {
            SalesTerritory salesTerritory = await db.SalesTerritories.FindAsync(id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            db.SalesTerritories.Remove(salesTerritory);
            await db.SaveChangesAsync();

            return Ok(salesTerritory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesTerritoryExists(int id)
        {
            return db.SalesTerritories.Count(e => e.TerritoryID == id) > 0;
        }
    }
}