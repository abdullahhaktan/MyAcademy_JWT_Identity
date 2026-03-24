using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using MusicApp.API.Data.Context;
using MusicApp.API.MLModels;

namespace MusicApp.API.Services.RecommendationServices
{
    public class RecommendationService : IRecommendationService
    {
        private readonly AppDbContext _context;
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public RecommendationService(AppDbContext context)
        {
            _context = context;
            _mlContext = new MLContext();
        }

        public async Task TrainAsync()
        {
            var rawData = await _context.UserSongHistories
                .Select(h => new { h.UserId, h.SongId })
                .ToListAsync();

            if (!rawData.Any()) return;

            var historyData = rawData
                .GroupBy(h => new { h.UserId, h.SongId })
                .Select(g => new SongRatingData
                {
                    UserId = (uint)g.Key.UserId,
                    SongId = (uint)g.Key.SongId,
                    Label = (float)g.Count()
                })
                .ToList();

            Console.WriteLine($">>> Eğitim verisi sayısı: {historyData.Count}");
            Console.WriteLine($">>> Farklı kullanıcı sayısı: {historyData.Select(x => x.UserId).Distinct().Count()}");
            Console.WriteLine($">>> Farklı şarkı sayısı: {historyData.Select(x => x.SongId).Distinct().Count()}");

            var trainingData = _mlContext.Data.LoadFromEnumerable(historyData);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(SongRatingData.UserId),
                MatrixRowIndexColumnName = nameof(SongRatingData.SongId),
                LabelColumnName = nameof(SongRatingData.Label),
                NumberOfIterations = 40,
                ApproximationRank = 8,
                LearningRate = 0.01,
                Lambda = 0.025
            };

            var trainer = _mlContext.Recommendation().Trainers.MatrixFactorization(options);
            _model = trainer.Fit(trainingData);
        }

        public async Task<List<int>> GetRecommendedSongIdsAsync(int userId, int topN = 8)
        {
            var userPackageId = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.PackageId)
                .FirstOrDefaultAsync();

            if (_model == null)
                await TrainAsync();

            var listenedSongIds = await _context.UserSongHistories
                .Where(h => h.UserId == userId)
                .Select(h => h.SongId)
                .Distinct()
                .ToListAsync();

            // Soğuk başlangıç
            if (!listenedSongIds.Any())
            {
                return await _context.Songs
                    .Where(s => s.Level <= userPackageId)
                    .OrderByDescending(s => s.ClickCount)
                    .Take(topN)
                    .Select(s => s.Id)
                    .ToListAsync();
            }

            var allSongIds = await _context.Songs
                .Where(s => s.Level <= userPackageId)
                .Select(s => s.Id)
                .ToListAsync();

            var predictionEngine = _mlContext.Model
                .CreatePredictionEngine<SongRatingData, SongRatingPrediction>(_model);

            var scores = allSongIds
                .Where(songId => !listenedSongIds.Contains(songId))
                .Select(songId => new
                {
                    SongId = songId,
                    Score = predictionEngine.Predict(new SongRatingData
                    {
                        UserId = (uint)userId,
                        SongId = (uint)songId
                    }).Score
                })
                .OrderByDescending(x => x.Score)
                .ToList();

            // Skorları konsola yaz
            foreach (var s in scores.Take(10))
                Console.WriteLine($"SongId: {s.SongId}, Score: {s.Score}");

            return scores
                .Take(topN)
                .Select(x => x.SongId)
                .ToList();
        }
    }
}