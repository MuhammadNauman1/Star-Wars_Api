using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ReactStartApi.DataAccess;
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

            #region 1st question
            var fileTitleLength = _context.Films
                .Select(i => new FileTitleLengthDto
                {
                    id = i.id,
                    title = i.title,
                    opening_crawl = i.opening_crawl,
                    opening_crawl_length = i.opening_crawl.Length
                }).OrderByDescending(x => x.opening_crawl_length).Take(1)
                .AsQueryable();
            var fileTitleLengthResult = fileTitleLength.First(m => m.opening_crawl_length == (fileTitleLength.Max(e => e.opening_crawl_length)));
            string filmTitle = fileTitleLengthResult.title;

            QuestionAndAnswerDto objFirstQuestion = new QuestionAndAnswerDto();
            objFirstQuestion.QuestionTitle = "Which of all Star Wars movies has the longest opening crawl (counted by number  of characters)?";
            objFirstQuestion.Answer = filmTitle;

            listQuestionAndAnswer.Add(objFirstQuestion);
            #endregion

                   return listQuestionAndAnswer;
        }
    }
}