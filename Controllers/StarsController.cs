using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReactStartApi.DataAccess;
using ReactStartApi.DataAccess.Models;
using ReactStartApi.DTOs;

namespace ReactStartApi.Controllers
{
    [RoutePrefix("api/Stars")]
    [Authorize]
    public class StarsController : ApiController
    {
        AppDataContext _context = new AppDataContext();

        /// <summary>
        /// Gets a film of with max character length
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<QuestionAndAnswerDto> GetQuestionAndAnswers()
        {



            List<QuestionAndAnswerDto> listQuestionAndAnswer = new List<QuestionAndAnswerDto>();

            #region 4th question
            var planetsCount = _context.Database.SqlQuery<planets_countDto>("select count(p.[name]) planets_counts,p.id, " +
                "p.[name] from [planets] P " +
                "join[films_planets] FP on fp.Planet_id = p.id " +
                "join[films_vehicles] FV on FV.film_id = FP.film_id " +
                "join  vehicles v  on v.id = fv.vehicle_id " +
                "join[vehicles_pilots] VP on VP.vehicle_id = FV.vehicle_id " +
                "join people PP on PP.id = vp.people_id " +
                "group by p.id,p.[name] " +
                "order by 1 desc ").ToList();

            var answerForPlanetsCount = string.Empty;
            if (planetsCount.Any())
            {
                for (var i = 0; i < planetsCount.Count; i++)
                {
                    answerForPlanetsCount += planetsCount[i].name + "\r\n";
                }
            }

            QuestionAndAnswerDto objfourthQuestion = new QuestionAndAnswerDto();
            objfourthQuestion.QuestionTitle = "What planet in Star Wars universe provided largest number of vehicle pilots?";
            objfourthQuestion.Answer = answerForPlanetsCount;

            listQuestionAndAnswer.Add(objfourthQuestion);
            #endregion
            return listQuestionAndAnswer;
        }
    }
}