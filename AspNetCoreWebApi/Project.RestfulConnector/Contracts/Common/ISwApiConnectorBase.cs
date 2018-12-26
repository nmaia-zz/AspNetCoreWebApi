namespace Project.RestfulConnector.Contracts
{
    public interface ISwApiConnectorBase
    {
        dynamic GetAllMovieApparitionsByPlanet(string planetName);
    }
}
