﻿using Chapter08.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chapter08.Interfaces
{
    public interface IStarWarsApi
    {
        [Get("/planets/?page={page}")]
        Task<ApiResult<List<Planet>>> GetPlanetsAsync(int page = 1);

        [Get("/planets/{id}/")]
        Task<Planet> GetPlanetAsync(int id);

        /*
        [Get("/people/")]
        Task<Response<List<Person>>> GetPeopleAsync();

        [Get("/people/{id}/")]
        Task<Person> GetPersonAsync(int id);

        //[Get("/planets/")]
        //Task<Response<List<Chapter08.Models.Planet>>> GetPlanetsAsync();

        [Get("/films/")]
        Task<Response<List<Film>>> GetFilmsAsync();


        [Get("/films/{id}/")]
        Task<Planet> GetFilmAsync(int id);
        */
    }

    public static class StarWarsApiExtensions
    {
        public static async Task<IEnumerable<Planet>> GetAllPlanetsAsync(this IStarWarsApi api)
        {
            List<Planet> results = new List<Planet>();

            int page = 0;
            do
            {
                page++;
                ApiResult<List<Planet>> response = await api.GetPlanetsAsync(page);
                results.AddRange(response.Data);
                if (response.Next == null) break;
            } while (true);

            return results;
        }
    }
}