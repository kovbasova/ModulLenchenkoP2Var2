using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//Використання моделей
using ModulLenchenkoP2Var2.Models;
using System.Data.Entity;

namespace ModulLenchenkoP2Var2.Controllers
{
    public class CRUDController : ApiController
    {
        //acces to db by model
        private DAIEntities daiDb = new DAIEntities();
        // ...../api/crud
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(daiDb.DaiTable.ToList());
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }
        public IHttpActionResult Post([FromBody] DaiTable car)
        {
            try
            {
                var newCar = daiDb.DaiTable.Add(car);
                daiDb.SaveChanges();
                return Ok(newCar);
            }
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }
        // Showing elements by chosen atribut .../api/crud?Year=2007&Num=ER 8901
        public IHttpActionResult Get([FromUri] int Year, [FromUri] string Num)
        {
            try
            {
                return Ok(daiDb.DaiTable.Where(car => (car.Yaer_of_Manufacture.Year == Year) && (car.Car_Number == Num)));
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }
        //Get elements by id /api/crud/2
        public IHttpActionResult Get(int id)
        {
            try
            {
                DaiTable carRes = daiDb.DaiTable.Find(id);
                if (carRes != null)
                    return Ok(carRes);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        //Delete element from db by id /api/crud/2
        public IHttpActionResult Delete(int id)
        {
            try
            {
                DaiTable car = daiDb.DaiTable.Find(id);
                if (car != null)
                {
                    daiDb.DaiTable.Remove(car);
                    daiDb.SaveChanges();
                    return Ok(daiDb.DaiTable.ToList());
                }
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        //Update element in db /api/crud/2
        public IHttpActionResult Patch(int id , [FromBody] DaiTable car)
        {
            try
            {
                daiDb.Entry(car).State = EntityState.Modified;
                daiDb.SaveChanges();
                return Ok(daiDb.DaiTable.ToList());
            }catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
