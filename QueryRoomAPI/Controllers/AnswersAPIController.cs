using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using QueryRoomAPI.Models;

namespace QueryRoom.Controllers.API_controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AnswersAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();

        //Posting the answer of a question
        public IHttpActionResult PostAnswer(Answers tanswer)                                            
        {
            TBL_ANSWERS tbObj = new TBL_ANSWERS();
            if(ModelState.IsValid)
            {
                tbObj.AID = Guid.NewGuid();
                tbObj.ANSWER = tanswer.ANSWER;
                tbObj.QID = tanswer.QID;
                tbObj.USERNAME = tanswer.USERNAME;
                tbObj.LIKES = 0;
                tbObj.DISLIKES = 0;
                tbObj.ISVALIDATED = false;
                tbObj.DATE = DateTime.Now;

                dbObj.TBL_ANSWERS.Add(tbObj);
                var user = dbObj.TBL_SIGNUP.ToList().Find(x=>x.USERNAME == tanswer.USERNAME);
                user.POINTS += 5;
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
        }

        [HttpGet]
        //View Answers API
        public IHttpActionResult GetAnswers(Guid qid)                   
        {
            var ans = dbObj.TBL_ANSWERS.Where(x=>x.QID == qid);
            if (ans==null)
            {
                return NotFound();
            }
            return Ok(ans.ToList().Select(Mapper.Map<TBL_ANSWERS,Answers>));
        }

        //API for liking or disliking an answer.
        //Future scope adding the restriction to like or dislike only once by a user.
        [HttpGet]
        public IHttpActionResult InsertLikeOrDislike(Guid aid, int func)          
        {
            var answer = dbObj.TBL_ANSWERS.ToList().Find(x=>x.AID == aid);
            var question = dbObj.TBL_QUESTIONS.ToList().Find(x=>x.QID == answer.QID);           
            var answerPostedBy = dbObj.TBL_SIGNUP.ToList().Find(x=>x.USERNAME == answer.USERNAME);
            if (answer==null)
            {
                return NotFound();
            }
            if (func == 1 )
            {
                answer.LIKES +=1;
                answerPostedBy.POINTS +=3;
            }
            else if(func ==0)
            {
                answer.DISLIKES +=1;
                answerPostedBy.POINTS -=3;
            }
            else
            {
                return BadRequest();
            }
            dbObj.SaveChanges();
            return Ok(Mapper.Map<TBL_QUESTIONS,Questions>(question));
        }

        //Validate answer by the Admin
        [HttpGet]
        public IHttpActionResult ValidateAnswer(Guid aid)                                                             
        {
            var res = dbObj.TBL_ANSWERS.ToList().Find(x => x.AID == aid);
            if (res==null)
            {
                return NotFound();
            }
            var question = dbObj.TBL_QUESTIONS.ToList().Find(x => x.QID == res.QID);
            
            res.ISVALIDATED = true;

            var user = dbObj.TBL_SIGNUP.ToList().Find(x => x.USERNAME == res.USERNAME);
            user.POINTS += 5;
            dbObj.SaveChanges();
            return Ok(Mapper.Map<TBL_QUESTIONS,Questions>(question));
        }
    }

}
