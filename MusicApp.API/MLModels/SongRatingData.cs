using Microsoft.ML.Data;

namespace MusicApp.API.MLModels
{
    public class SongRatingData
    {
        [KeyType(count: 100)]  // Max SongId ve UserId'den büyük olmalı
        public uint UserId { get; set; }

        [KeyType(count: 50)]  // 59'dan büyük
        public uint SongId { get; set; }

        public float Label { get; set; }
    }

    public class SongRatingPrediction
    {
        public float Label { get; set; }
        public float Score { get; set; }
    }
}