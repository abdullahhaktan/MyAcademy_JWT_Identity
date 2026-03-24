namespace MusicApp.API.Services.RecommendationServices
{
    public interface IRecommendationService
    {
        Task TrainAsync();
        Task<List<int>> GetRecommendedSongIdsAsync(int userId, int topN = 6);
    }
}
