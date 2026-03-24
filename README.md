# 🎵 Bepop — JWT Project

> Full-Stack .NET kapsamında geliştirilen, JWT tabanlı kimlik doğrulama, ML.NET öneri sistemi ve Google Cloud Storage entegrasyonu içeren müzik streaming platformu.

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


## 📸 Ekran Görüntüleri


<img width="959" height="470" alt="Ekran görüntüsü 2026-03-24 085901" src="https://github.com/user-attachments/assets/7528789c-e779-4150-a135-6f985e926654" />

---

<img width="313" height="322" alt="Ekran görüntüsü 2026-03-24 090026" src="https://github.com/user-attachments/assets/b8971509-42ea-4fbd-91da-8e6baedc6100" />

---

<img width="959" height="470" alt="Ekran görüntüsü 2026-03-24 090300" src="https://github.com/user-attachments/assets/f04166d0-8b97-4031-a297-4d7ad45aaad7" />

---

<img width="959" height="64" alt="Ekran görüntüsü 2026-03-24 090422" src="https://github.com/user-attachments/assets/fedd16ea-72be-435f-be28-472dc2202eee" />

---

<img width="959" height="473" alt="Ekran görüntüsü 2026-03-24 090542" src="https://github.com/user-attachments/assets/79ce3025-b6f6-4c5e-87dd-c843f9564bd1" />

---

<img width="959" height="472" alt="Ekran görüntüsü 2026-03-24 090808" src="https://github.com/user-attachments/assets/2f1391f7-123c-4d60-a84d-9127d4acc4c6" />

---

<img width="959" height="467" alt="Ekran görüntüsü 2026-03-24 092917" src="https://github.com/user-attachments/assets/f5bee399-a4f1-4ab6-ae23-dc06f9eb055e" />

---

<img width="959" height="470" alt="Ekran görüntüsü 2026-03-24 091008" src="https://github.com/user-attachments/assets/d6792c1d-b00f-4977-ade4-e9bab8c4c5f8" />

---

<img width="959" height="471" alt="Ekran görüntüsü 2026-03-24 090951" src="https://github.com/user-attachments/assets/ba68bbfa-ec7f-464b-a1e3-d10f61fb0606" />

---

<img width="959" height="467" alt="Ekran görüntüsü 2026-03-24 093250" src="https://github.com/user-attachments/assets/a7ec180a-3b85-462c-9f6d-a38045425733" />

---

<img width="959" height="471" alt="Ekran görüntüsü 2026-03-24 093157" src="https://github.com/user-attachments/assets/a55caeac-96ae-4fe5-b256-c81f4875b790" />

---

<img width="959" height="473" alt="Ekran görüntüsü 2026-03-24 092955" src="https://github.com/user-attachments/assets/314cd00f-ddd2-4983-86bd-5762b0f676ff" />

---

<img width="959" height="472" alt="Ekran görüntüsü 2026-03-24 093415" src="https://github.com/user-attachments/assets/6063d356-3900-4917-b50e-19b2943906c2" />
<img width="959" height="470" alt="Ekran görüntüsü 2026-03-24 093310" src="https://github.com/user-attachments/assets/13c057cd-b32f-4621-9e07-072aaa77b225" />

---

<img width="959" height="472" alt="Ekran görüntüsü 2026-03-24 093501" src="https://github.com/user-attachments/assets/e1cb4b6a-92d5-43ae-8e63-635575e01fbf" />

---

<img width="959" height="467" alt="Ekran görüntüsü 2026-03-24 100426" src="https://github.com/user-attachments/assets/5c4d8603-ba7b-4d3e-828f-d362de8e99ed" />

---

<img width="959" height="470" alt="Ekran görüntüsü 2026-03-24 101621" src="https://github.com/user-attachments/assets/c7f22bbd-8f28-471b-88a9-34af6003b14d" />

---

<img width="958" height="475" alt="Ekran görüntüsü 2026-03-24 101601" src="https://github.com/user-attachments/assets/04d30ae3-a281-4396-b281-3b2e536cdaf0" />

---

<img width="3524" height="4624" alt="localhost_7067_" src="https://github.com/user-attachments/assets/4a315e12-cb2c-4d36-b9bb-f5eb2ba1a3c2" />

---

<img width="959" height="471" alt="Ekran görüntüsü 2026-03-24 102057" src="https://github.com/user-attachments/assets/7d9e392a-26d3-4b6f-8f4c-35faccf8f1ec" />

---

<img width="959" height="475" alt="Ekran görüntüsü 2026-03-24 101711" src="https://github.com/user-attachments/assets/e51fbcbc-3259-4798-8606-80a3cfad1dd3" />

---

<img width="3524" height="2102" alt="localhost_7067_Login_Index (1)" src="https://github.com/user-attachments/assets/e55a93f6-46c5-4c3c-9a8a-0e66131100c7" />

---

<img width="3524" height="4654" alt="localhost_7155_swagger_index html" src="https://github.com/user-attachments/assets/d6265b4c-9cdf-45ed-b50e-7450b9b65656" />

---

<img width="3524" height="2102" alt="localhost_7067_Register_Index (1)" src="https://github.com/user-attachments/assets/866b7f80-58f8-426a-b8eb-039c3e951ee3" />
