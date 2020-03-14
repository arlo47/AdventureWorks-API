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
    public class SalesTaxRatesController : ApiController
    {
        private AdventureWorks2017Entities db = new AdventureWorks2017Entities();

        // GET: api/SalesTaxRates
        public IQueryable<SalesTaxRate> GetSalesTaxRates()
        {
            return db.SalesTaxRates;
        }

        // GET: api/SalesTaxRates/5
        [ResponseType(typeof(SalesTaxRate))]
        public async Task<IHttpActionResult> GetSalesTaxRate(int id)
        {
            SalesTaxRate salesTaxRate = await db.SalesTaxRates.FindAsync(id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }

            return Ok(salesTaxRate);
        }

        // PUT: api/SalesTaxRates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesTaxRate(int id, SalesTaxRate salesTaxRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesTaxRate.SalesTaxRateID)
            {
                return BadRequest();
            }

            db.Entry(salesTaxRate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesTaxRateExists(id))
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

        // POST: api/SalesTaxRates
        [ResponseType(typeof(SalesTaxRate))]
        public async Task<IHttpActionResult> PostSalesTaxRate(SalesTaxRate salesTaxRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesTaxRates.Add(salesTaxRate);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = salesTaxRate.SalesTaxRateID }, salesTaxRate);
        }

        // DELETE: api/SalesTaxRates/5
        [ResponseType(typeof(SalesTaxRate))]
        public async Task<IHttpActionResult> DeleteSalesTaxRate(int id)
        {
            SalesTaxRate salesTaxRate = await db.SalesTaxRates.FindAsync(id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }

            db.SalesTaxRates.Remove(salesTaxRate);
            await db.SaveChangesAsync();

            return Ok(salesTaxRate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesTaxRateExists(int id)
        {
            return db.SalesTaxRates.Count(e => e.SalesTaxRateID == id) > 0;
        }
    }
}