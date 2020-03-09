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
        public List<QuestionAndAnswerDto> GetQuestionAndAnswers ()
        {

           

            List<QuestionAndAnswerDto> listQuestionAndAnswer = new List<QuestionAndAnswerDto>();

            #region 1st question
            var fileTitleLength = _context.Films
                .Select(i => new FileTitleLengthDto
                {
                    id=i.id,
                    title =  i.title,
                    opening_crawl = i.opening_crawl,
                    opening_crawl_length =  i.opening_crawl.Length
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

            #region 3rd question
            var tempspecies = _context.films_species
                      .GroupBy(a => a.species_id)
                       .Select(i => new films_speciesDto
                       {
                           species_id = i.Key,
                           species_id_count = i.Count()
                       }).OrderByDescending(x => x.species_id_count).AsQueryable();

            var filmSpeciesCount = _context.species.
                Join(tempspecies, u => u.id, uir => uir.species_id, (u, uir) => new { u, uir }).
                Join(_context.species_people, r => r.uir.species_id, ro => ro.species_id, (r, ro) => new { r, ro })
                .Select(m => new FilmSpeciesCountDto
                {
                    name = m.r.u.name,
                    filmcount = m.r.uir.species_id_count
                }).Take(1).ToList();
            var answerForFilmCount = string.Empty;
            for (var i = 0; i < filmSpeciesCount.Count; i++)
            {
                answerForFilmCount += filmSpeciesCount[i].name + " (" + filmSpeciesCount[i].filmcount + ")";
            }

            QuestionAndAnswerDto objThirdQuestion = new QuestionAndAnswerDto();
            objThirdQuestion.QuestionTitle = "What species (i.e. characters that belong to certain species) appeared in the most  number of Star Wars films?";
            objThirdQuestion.Answer = answerForFilmCount;

            listQuestionAndAnswer.Add(objThirdQuestion);
            #endregion
            #region 4th question
            var planetsCount = _context.Database.SqlQuery<planets_countDto>("select count(p.[name]) planets_counts,p.id, " +
                "p.[name] from [planets] P " +
                "join[films_planets] FP on fp.Planet_id = p.id " +
                "join[films_vehicles] FV on FV.film_id = FP.film_id " +
                "join  vehicles v  on v.id = fv.vehicle_id " +
                "join[vehicles_pilots] VP on VP.vehicle_id = FV.vehicle_id " +
                "join people PP on PP.id = vp.people_id " +
                "group by p.id,p.[name] " +
                "order by 1 desc ").Take(1).ToList();
           
            var answerForPlanetsCount = string.Empty;
            if (planetsCount.Any())
            {
                answerForFilmCount = planetsCount[0].name;
            }

            QuestionAndAnswerDto objfourthQuestion = new QuestionAndAnswerDto();
            objfourthQuestion.QuestionTitle = "What planet in Star Wars universe provided largest number of vehicle pilots?";
            objfourthQuestion.Answer = answerForFilmCount;

            listQuestionAndAnswer.Add(objfourthQuestion);
            #endregion
            return listQuestionAndAnswer;
        }
    }
}