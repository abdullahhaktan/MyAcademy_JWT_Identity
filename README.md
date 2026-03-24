# 🎵 Bepop — JWT Project

> Full-Stack .NET kapsamında geliştirilen, JWT tabanlı kimlik doğrulama, ML.NET öneri sistemi ve Google Cloud Storage entegrasyonu içeren müzik streaming platformu.

---

## 📸 Ekran Görüntüleri

<!-- Ekran görüntülerini buraya ekleyebilirsiniz -->

| Anasayfa | Chart Sayfası | Sanatçılar |
|----------|--------------|------------|
| ![home](screenshots/home.png) | ![chart](screenshots/chart.png) | ![artist](screenshots/artist.png) |

| Player | Öneriler | Blog |
|--------|----------|------|
| ![player](screenshots/player.png) | ![recommend](screenshots/recommend.png) | ![blog](screenshots/blog.png) |

---

## 🚀 Özellikler

- 🔐 **JWT Authentication** — Token tabanlı kimlik doğrulama ve yetkilendirme
- 📦 **Paket Sistemi** — Kullanıcı abonelik seviyesine göre içerik erişim kontrolü
- 🤖 **ML.NET Öneri Motoru** — Matrix Factorization ile kişiselleştirilmiş şarkı önerileri
- 🎧 **Kesintisiz Müzik Deneyimi** — Sayfa geçişlerinde müzik kesilmez (SPA-like navigasyon)
- ☁️ **Google Cloud Storage** — Tüm görseller ve MP3 dosyaları bulutta saklanır
- 🎛️ **Gelişmiş Filtreleme** — Ülke, tür ve popülerlik bazlı şarkı filtreleme
- 👤 **Kullanıcı Yönetimi** — Kayıt, giriş, session yönetimi
- 📝 **Blog & Yorum** — Blog detay sayfası ve yorum sistemi

---

## 🏗️ Mimari

```
MyAcademy_JWT_Identity/
├── MusicApp.API/
│   ├── Controllers/
│   │   ├── SongsController.cs
│   │   ├── ArtistsController.cs
│   │   ├── BlogsController.cs
│   │   ├── CommentsController.cs
│   │   ├── LoginController.cs
│   │   └── RegisterController.cs
│   ├── Data/
│   │   ├── Context/
│   │   └── Entities/
│   ├── DTOs/
│   ├── Services/
│   │   ├── TokenServices/
│   │   └── RecommendationServices/
│   └── MLModels/
│
└── MusicApp.UI/
    ├── Controllers/
    │   ├── DiscoverController.cs
    │   ├── ChartController.cs
    │   ├── ArtistController.cs
    │   ├── GenreController.cs
    │   ├── BlogController.cs
    │   ├── LoginController.cs
    │   └── RegisterController.cs
    ├── Views/
    ├── Components/
    └── Services/
```

---

## 🔐 JWT & Kimlik Doğrulama

Kullanıcı giriş yaptığında üretilen JWT token içerisine `PackageId` claim'i embed edilir. Her API isteğinde bu claim okunarak kullanıcının erişebileceği içerikler sunucu tarafında filtrelenir.

```csharp
var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
    new Claim("PackageId", user.PackageId.ToString())
};
```

---

## 🤖 ML.NET Öneri Sistemi

Kullanıcının dinleme geçmişine göre **Matrix Factorization** algoritmasıyla şarkı önerileri üretilir. Soğuk başlangıç durumunda (yeni kullanıcı) popülerlik bazlı fallback devreye girer.

```csharp
var options = new MatrixFactorizationTrainer.Options
{
    MatrixColumnIndexColumnName = nameof(SongRatingData.UserId),
    MatrixRowIndexColumnName = nameof(SongRatingData.SongId),
    LabelColumnName = nameof(SongRatingData.Label),
    NumberOfIterations = 40,
    ApproximationRank = 8,
};
```

---

## 🎧 SPA-like Navigasyon

Sayfa geçişlerinde `loadPage()` fonksiyonu ile yalnızca `#content` bölgesi AJAX ile güncellenir. Bottom player DOM'da sabit kalır, müzik kesilmez.

```javascript
function loadPage(url) {
    $.ajax({
        url: url,
        type: 'GET',
        headers: { "X-Requested-With": "XMLHttpRequest" },
        success: function(result) {
            $('#content').html(result);
            window.history.pushState({}, '', url);
        }
    });
}
```

---

## ☁️ Google Cloud Storage

Tüm şarkı görselleri ve MP3 dosyaları Google Cloud Storage bucket'larında saklanır. Uygulama dosyalara doğrudan bucket URL'leri üzerinden erişir.

---

## 🛠️ Kullanılan Teknolojiler

| Katman | Teknoloji |
|--------|-----------|
| Backend API | ASP.NET Core Web API |
| UI | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Veritabanı | SQL Server |
| Kimlik Doğrulama | JWT, ASP.NET Identity |
| Öneri Sistemi | ML.NET, Matrix Factorization |
| Nesne Mapleme | Mapster |
| Medya Depolama | Google Cloud Storage |

---

## ⚙️ Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server
- Google Cloud Storage hesabı

### Adımlar

```bash
# Repoyu klonla
git clone https://github.com/abdullahhaktan/MyAcademy_JWT_Identity.git
cd MyAcademy_JWT_Identity
```

**API — `appsettings.json` ayarları:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=MusicApp;..."
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpireInMinutes": "60"
  }
}
```

```bash
# Migration uygula
cd MusicApp.API
dotnet ef database update

# API'yi başlat
dotnet run

# UI'yi başlat (yeni terminal)
cd ../MusicApp.UI
dotnet run
```

---

## 👨‍💻 Geliştirici

**Abdullah Haktan**
GitHub → [abdullahhaktan](https://github.com/abdullahhaktan)

---

## 🙏 Teşekkür

Rehberim olan **Erhan Gündüz** ve **Murat Yücedağ** hocalarıma teşekkür ederim.

---

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
