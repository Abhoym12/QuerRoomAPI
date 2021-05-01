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
    public class QuestionsAPIController : ApiController
    {
        QueryRoomDBEntities dbObj = new QueryRoomDBEntities();
        //get all questions
        public IEnumerable<Questions> GetQuestions()                                            
        {
            return dbObj.TBL_QUESTIONS.ToList().Select(Mapper.Map<TBL_QUESTIONS,Questions>);       
        }

        //posting  a question
        public IHttpActionResult PostQuestion(Questions tquestion)                                  
        {
            TBL_QUESTIONS tbObj = new TBL_QUESTIONS();
            if (ModelState.IsValid)
            {
                tbObj.QID = Guid.NewGuid();
                tbObj.QUESTION = tquestion.QUESTION;
                tbObj.USERNAME = tquestion.USERNAME;
                tbObj.TIMESTAMP = DateTime.Now;
                dbObj.TBL_QUESTIONS.Add(tbObj);
                var user = dbObj.TBL_SIGNUP.ToList().Find(x=>x.USERNAME == tquestion.USERNAME);
                user.POINTS += 2;
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
        }

        //Deleting a Question for User's Question Only
        [Route("Questions/Delete/{guid}")]
        public IHttpActionResult DeleteQuestion(string guid)                                        
        {
            var data = dbObj.TBL_QUESTIONS.ToList().Find(x => x.QID == Guid.Parse(guid));
            if (ModelState.IsValid)
            {
                dbObj.TBL_QUESTIONS.Remove(data);
                dbObj.SaveChanges();
                return Ok();
            }
            ModelState.Clear();
            return BadRequest();
        }
        

        //Get Questions of the Current user
        [Route("User/Questions/{userid}")]                                                                
        public IEnumerable<Questions> GetQuestionsOfUser(string userid)
        {
            var questions = dbObj.TBL_QUESTIONS.Where(x => x.USERNAME == userid);
            return questions.ToList().Select(Mapper.Map<TBL_QUESTIONS,Questions>);
        }

        //Trending Questions 
        [HttpGet]
        [Route("trendingquestions")]
        public object TrendingQuestions()                                                                 
        {
            var RecentlyPosted = dbObj.TBL_QUESTIONS.ToList().OrderByDescending(ques => ques.TIMESTAMP).Take(5);
            return new { RecentQuestions = RecentlyPosted.Select(Mapper.Map<TBL_QUESTIONS,Questions>) };
        }
    }
}
