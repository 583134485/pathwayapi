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
    public class InvestmentsController : ApiController
    {
        private GdcAppsPathwayWebApiContext db = new GdcAppsPathwayWebApiContext();

        // GET: api/Investments
        public IQueryable<Investment> GetInvestments()
        {
            return db.Investments;
        }

        //manger id
        public List<Investment> GetInvestmentsByManager(int id)
        {
            List<Investment> investments = db.Investments.Where(x => x.ManagerId == id).ToList();
            return investments;
        }

        // GET: api/Investments/5
        [ResponseType(typeof(Investment))]
        public async Task<IHttpActionResult> GetInvestment(int id)
        {
            Investment investment = await db.Investments.FindAsync(id);
            if (investment == null)
            {
                return NotFound();
            }

            return Ok(investment);
        }

        // PUT: api/Investments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvestment(int id, Investment investment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != investment.Id)
            {
                return BadRequest();
            }

            db.Entry(investment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestmentExists(id))
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

        // POST: api/Investments
        [ResponseType(typeof(Investment))]
        public async Task<IHttpActionResult> PostInvestment(Investment investment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Manager manager = db.Managers.FirstOrDefault(x=>x.Name==investment.Manager);
            if (manager == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            investment.ManagerId = manager.Id;
            db.Investments.Add(investment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = investment.Id }, investment);
        }

        // DELETE: api/Investments/5
        [HttpPost]
        [ActionName("DeleteInvestment")]
        [ResponseType(typeof(Investment))]
        public async Task<IHttpActionResult> DeleteInvestment(Investment investmentpara)
        {
            if (investmentpara.Id == null||string.IsNullOrEmpty(investmentpara.Id.ToString()))
            {
                return NotFound();
            }
            Investment investment = await db.Investments.FindAsync(investmentpara.Id);
            if (investment == null)
            {
                return NotFound();
            }

            db.Investments.Remove(investment);
            await db.SaveChangesAsync();

            return Ok(investment);
        }

        [HttpPost]
        [ActionName("CheckInvestment")]
        public bool CheckInvestment(Investment investment)
        {
         
            int samename = db.Investments.Count(e => e.Name == investment.Name);

            int sameManagerAndLeader = db.Investments.Where(data => data.Manager == investment.Manager).Count(s => s.Leader == investment.Leader);

            if ((samename + sameManagerAndLeader) == 0)
            {
                return true;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvestmentExists(int id)
        {
            return db.Investments.Count(e => e.Id == id) > 0;
        }
    }
}