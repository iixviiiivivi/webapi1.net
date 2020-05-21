using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using webapi1.DAO.Member;
using webapi1.Exceptions;
using webapi1.Filters;
using webapi1.Models;

namespace webapi1.Controllers
{
    public class MemberController : ApiController
    {
        private static readonly MemberDAO memberDAO = new MemberDAO();

        public HttpResponseMessage GetAll()
        {
            List<member> members = memberDAO.FindAll();
            if (members.Count > 0)
                return Request.CreateResponse(HttpStatusCode.OK, members);

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No members found");
        }

        // Print messages before and after executing action
        [ActionInfo]
        public HttpResponseMessage Get(int? id)
        {
            member member = memberDAO.FindOne(id);
            if (member != null)
                return Request.CreateResponse(HttpStatusCode.OK, member);
            
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Member id {id} not found");
        }

        public HttpResponseMessage Post(member m)
        {
            if (ModelState.IsValid)
            {
                member member = memberDAO.Save(m);
                if (member != null)
                    return Request.CreateResponse(HttpStatusCode.OK, member);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Saving member failed");
            }

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);
        }
        
        public HttpResponseMessage Put(int? id, member m)
        {
            if (!ModelState.IsValid) 
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            member member = memberDAO.Update(id, m);
            if (member is null) 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Updating member id {id} failed");

            return Request.CreateResponse(HttpStatusCode.OK, member);
        }

        public HttpResponseMessage Delete(int? id)
        {
            bool flag = memberDAO.Delete(id);
            if (flag) 
                return Request.CreateResponse(HttpStatusCode.OK, $"Member ID {id} deleted");

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Deleting member id {id} failed");
        }

        // Exception test at action scope
        [ExceptionHandler]
        [Route("api/exception")]
        public HttpResponseMessage GetExceptionTest()
        {
            throw new Exception("GetExceptionTest throws an exception");
        }

        // Exception test at global scope
        [Route("api/globalexception")]
        public HttpResponseMessage GetGlobalExceptionTest()
        {
            throw new Exception("GetGlobalExceptionTest throws an exception");
        }
    }
}
