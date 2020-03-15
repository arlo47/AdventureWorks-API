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
    public class SalesPersonsController : ApiController
    {
        private AdventureWorks2017Entities db = new AdventureWorks2017Entities();

        // GET: api/SalesPersons
        public IQueryable<SalesPerson> GetSalesPersons()
        {
            return db.SalesPersons;
        }

        // GET: api/SalesPersons/5
        [ResponseType(typeof(SalesPerson))]
        public async Task<IHttpActionResult> GetSalesPerson(int id)
        {
            SalesPerson salesPerson = await db.SalesPersons.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return Ok(salesPerson);
        }

        // GET: api/SalesTerritory/5/SalesPersons
        [ResponseType(typeof(SalesPerson))]
        [Route("api/SalesTerritory/{id}/SalesPersons")]
        public async Task<IHttpActionResult> GetSalesPersonByTerritory(int id)
        {
            List<SalesPerson> persons = await db.SalesPersons
                                        .Where(p => p.TerritoryID == id)
                                        .ToListAsync();
            if (persons == null)
            {
                return NotFound();
            }

            return Ok(persons);
        }

        // PUT: api/SalesPersons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesPerson(int id, SalesPerson salesPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesPerson.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(salesPerson).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonExists(id))
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

        // POST: api/SalesPersons
        [ResponseType(typeof(SalesPerson))]
        public async Task<IHttpActionResult> PostSalesPerson(SalesPerson salesPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesPersons.Add(salesPerson);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonExists(salesPerson.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesPerson.BusinessEntityID }, salesPerson);
        }

        // DELETE: api/SalesPersons/5
        [ResponseType(typeof(SalesPerson))]
        public async Task<IHttpActionResult> DeleteSalesPerson(int id)
        {
            SalesPerson salesPerson = await db.SalesPersons.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            db.SalesPersons.Remove(salesPerson);
            await db.SaveChangesAsync();

            return Ok(salesPerson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesPersonExists(int id)
        {
            return db.SalesPersons.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}