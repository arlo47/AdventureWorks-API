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
    public class SalesPersonQuotaHistoriesController : ApiController
    {
        private AdventureWorks2017Entities db = new AdventureWorks2017Entities();

        // GET: api/SalesPersonQuotaHistories
        public IQueryable<SalesPersonQuotaHistory> GetSalesPersonQuotaHistories()
        {
            return db.SalesPersonQuotaHistories;
        }

        // GET: api/SalesPerson/5/SalesPersonQuotaHistory
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        [Route("api/SalesPerson/{id}/SalesPersonQuotaHistory")]     // define routes by attribute
        public async Task<IHttpActionResult> GetSalesPersonQuotaHistory(int id)
        {
            // gets quota history by BusinessEntityID (Sales Person PK)
            List<SalesPersonQuotaHistory> quotaHistories = await db.SalesPersonQuotaHistories
                                        .Where(q => q.BusinessEntityID == id)
                                        .ToListAsync();
            if (quotaHistories == null)
            {
                return NotFound();
            }

            return Ok(quotaHistories);
        }

        // PUT: api/SalesPersonQuotaHistories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesPersonQuotaHistory(int id, SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesPersonQuotaHistory.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(salesPersonQuotaHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonQuotaHistoryExists(id))
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

        // POST: api/SalesPersonQuotaHistories
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        public async Task<IHttpActionResult> PostSalesPersonQuotaHistory(SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesPersonQuotaHistories.Add(salesPersonQuotaHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonQuotaHistoryExists(salesPersonQuotaHistory.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesPersonQuotaHistory.BusinessEntityID }, salesPersonQuotaHistory);
        }

        // DELETE: api/SalesPersonQuotaHistories/5
        [ResponseType(typeof(SalesPersonQuotaHistory))]
        public async Task<IHttpActionResult> DeleteSalesPersonQuotaHistory(int id)
        {
            SalesPersonQuotaHistory salesPersonQuotaHistory = await db.SalesPersonQuotaHistories.FindAsync(id);
            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }

            db.SalesPersonQuotaHistories.Remove(salesPersonQuotaHistory);
            await db.SaveChangesAsync();

            return Ok(salesPersonQuotaHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesPersonQuotaHistoryExists(int id)
        {
            return db.SalesPersonQuotaHistories.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}