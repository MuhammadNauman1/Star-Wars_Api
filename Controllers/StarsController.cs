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
                }).ToList();
            var answerForFilmCount = string.Empty;
            for (var i = 0; i < filmSpeciesCount.Count; i++)
            {
                answerForFilmCount += filmSpeciesCount[i].name + " (" + filmSpeciesCount[i].filmcount + ")\r\n";
            }

            QuestionAndAnswerDto objThirdQuestion = new QuestionAndAnswerDto();
            objThirdQuestion.QuestionTitle = "What species (i.e. characters that belong to certain species) appeared in the most  number of Star Wars films?";
            objThirdQuestion.Answer = answerForFilmCount;

            listQuestionAndAnswer.Add(objThirdQuestion);
            #endregion
            
            return listQuestionAndAnswer;
        }
    }
}