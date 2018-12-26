using Project.RestfulConnector.Contracts;
using RestSharp;
using System;

namespace Project.RestfulConnector.Concrete.Common
{
    public class SwApiConnectorBase : ISwApiConnectorBase
    {
        public dynamic GetAllMovieApparitionsByPlanet(string planetName)
        {
            //Url to be consumed: https://swapi.co/api/planets/?search=Dagobah

            try
            {
                var client = new RestClient
                {
                    BaseUrl = new Uri("https://swapi.co/api/planets")
                };

                var request = new RestRequest
                {
                    Resource = "?search=" + planetName
                };

                IRestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
