using QueryRoomAPI.Models;
using QueryRoomAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QueryRoom.Controllers.API_controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserStatsAPIController : ApiController
    {
        QueryRoomDBEntities q = new QueryRoomDBEntities();

        // GET api/<controller>
        [HttpGet]
        [Route("userstats/stats/{username}")]
        public userstats totalquestions(string username)                                                               
        {
            var user = from m in q.TBL_SIGNUP
                         where m.USERNAME == username
                         select m;
            int count_questions = q.TBL_QUESTIONS.ToList().Count(x => x.USERNAME == user.First().USERNAME);
            
            int count_answers = q.TBL_ANSWERS.ToList().Count(x => x.USERNAME == user.First().USERNAME);

            int validated_a = q.TBL_ANSWERS.ToList().Count(x => x.USERNAME == user.First().USERNAME && x.ISVALIDATED == true);


            double p = validated_a;
            double y = count_answers;
            
            int c = y==0?0:(int)((p / y) * 100);
            userstats us = new userstats()
            {
                questions = count_questions,
                answers = count_answers,
                validated_answers = validated_a,
                credibility = c
            };

            return us;
        }


        // admin part


        [HttpGet]
        [Route("adminstats/stats")]
        public userstats admin()                                                               
        {

            int count_questions = q.TBL_QUESTIONS.ToList().Count();

            int count_answers = q.TBL_ANSWERS.ToList().Count();

            int count_answered_questions = q.TBL_ANSWERS.ToList().GroupBy(x => x.QID).Count();

            int validated_a = q.TBL_ANSWERS.ToList().Count(x => x.ISVALIDATED == true);


            


            double p = count_answered_questions;
            double y = count_questions;

            int poaq = y == 0 ? 0 : (int)((p / y) * 100);

            p = validated_a;
            y = count_answers;

            int pova = y == 0 ? 0 : (int)((p / y) * 100);

            userstats us = new userstats()
            {
                questions = count_questions,
                answers = count_answers,
                validated_answers = validated_a,
                credibility = poaq,
                for_admin = pova
            };

            return us;
        }
    }
}