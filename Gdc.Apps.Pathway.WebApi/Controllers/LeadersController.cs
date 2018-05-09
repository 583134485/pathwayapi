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
using Gdc.Apps.Pathway.WebApi.Models;

namespace Gdc.Apps.Pathway.WebApi.Controllers
{
    public class LeadersController : ApiController
    {
        private GdcAppsPathwayWebApiContext db = new GdcAppsPathwayWebApiContext();

        // GET: api/Leaders
        //[HttpGet]
        //[ActionName("listall")]
        public IQueryable<Leader> GetLeaders()
        {
            return db.Leaders;
        }

        // GET: api/Leaders/5
        //[HttpPost]
        //[ActionName("getone")]
        [ResponseType(typeof(Leader))]
        public async Task<IHttpActionResult> GetLeader(int id)
        {
            Leader leader = await db.Leaders.FindAsync(id);
            if (leader == null)
            {
                return NotFound();
            }

            return Ok(leader);
        }

        // PUT: api/Leaders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLeader(int id, Leader leader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leader.Id)
            {
                return BadRequest();
            }

            db.Entry(leader).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaderExists(id))
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

        // POST: api/Leaders
        [ResponseType(typeof(Leader))]
        public async Task<IHttpActionResult> PostLeader(Leader leader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Leaders.Add(leader);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = leader.Id }, leader);
        }

        // DELETE: api/Leaders/5
        [ResponseType(typeof(Leader))]
        public async Task<IHttpActionResult> DeleteLeader(int id)
        {
            Leader leader = await db.Leaders.FindAsync(id);
            if (leader == null)
            {
                return NotFound();
            }

            db.Leaders.Remove(leader);
            await db.SaveChangesAsync();

            return Ok(leader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaderExists(int id)
        {
            
            return db.Leaders.Count(e => e.Id == id) > 0;
        }
    }
}