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

             
            
            #region 2nd question
            var film_count = _context.films_characters
                       .GroupBy(a => a.people_id)
                        .Select(i => new films_charactersDto
                        {
                            people_id = i.Key,
                            people_id_count = i.Count()
                        }).OrderByDescending(x => x.people_id_count).AsQueryable();

            var topCountPerson = film_count.Take(1).ToList();
            int peopleId = 0;
            if (topCountPerson.Any()) { peopleId = topCountPerson[0].people_id; }

            var people = _context.people.Where(x => x.id == peopleId).Select(i => new peopleDto
            {
                id = i.id,
                name = i.name
            }).ToList();
            string character = string.Empty;
            if (people.Any()) { character = people[0].name; }


            QuestionAndAnswerDto objSecondQuestion = new QuestionAndAnswerDto();
            objSecondQuestion.QuestionTitle = "What character (person) appeared in most of the Star Wars films?";
            objSecondQuestion.Answer = character;

            listQuestionAndAnswer.Add(objSecondQuestion);
            #endregion

            return listQuestionAndAnswer;
        }
    }
}