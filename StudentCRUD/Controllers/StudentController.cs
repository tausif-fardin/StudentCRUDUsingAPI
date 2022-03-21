using StudentCRUD.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace StudentCRUD.Controllers
{
    public class StudentController : ApiController
    {

        //add student into db
        [Route("api/student/add")]
        [HttpPost]

        public HttpResponseMessage Add(Student s)
        {
            StudentApiEntities1 db = new StudentApiEntities1();
            db.Students.Add(s);
            db.SaveChanges();
            //var data = new JavaScriptSerializer().Serialize(db);
            return Request.CreateResponse(HttpStatusCode.OK, "Student Created in DB");
        }

        //Retrieve student data by id 
        [Route("api/student/get/{id}")]
        [HttpGet]

        public HttpResponseMessage Get(int id)
        {
            StudentApiEntities1 db = new StudentApiEntities1();
            var st = db.Students.FirstOrDefault(x => x.Id == id);
            var data = new JavaScriptSerializer().Serialize(st);
            return Request.CreateResponse(HttpStatusCode.OK, st);
        }

        //Update student in db
        [Route("api/student/update")]
        [HttpPut]

        public HttpResponseMessage Update(Student s)
        {
            using (var db = new StudentApiEntities1())
            {
                var student = db.Students.FirstOrDefault(x => x.Id == s.Id);
                if(student!= null)
                {
                    student.Id = s.Id;
                    student.StudentName = s.StudentName;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Student updated in DB");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Student not found in DB");

                }
            }
        }

        //Delete student from db
        [Route("api/student/delete/{id}")]
        [HttpDelete]

        public HttpResponseMessage Delete(int id)
        {
            using(var db = new StudentApiEntities1())
            {
                var student = db.Students.FirstOrDefault(x => x.Id == id);
                db.Students.Remove(student);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Student deleted from DB");
            }
        }



    }
}
