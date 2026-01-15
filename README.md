# 🎓 Erasmus Başvuru Sistemi - Test Otomasyon & Prototip Projesi

**Ders:** Yazılım Mühendisliği Mezuniyet Projesi
**Teknoloji:** .NET 8.0, PostgreSQL, Playwright, xUnit
**Durum:** Backend Prototipi Hazır & Test Süreçleri Tasarlandı

## 📄 Proje Hakkında
Bu proje, geliştirilmekte olan Erasmus Başvuru Yönetim Sistemi'nin **Backend Prototipini** ve bu sistemin kalitesini denetleyen **Test Otomasyon Altyapısını** içerir. Proje, Katmanlı Mimari (Layered Architecture) prensiplerine göre tasarlanmış olup, hem geliştirme hem de kalite güvence (QA) süreçlerini simüle eder.

## 🏗️ Proje Mimarisi (Solution Structure)
Proje, Hafta 9 Raporunda belirtilen **5 Katmanlı Yapı** üzerine kurulmuştur:

* **`ErasmusSystem.API`**: RESTful servislerin bulunduğu sunum katmanı (Swagger UI destekli).
* **`ErasmusSystem.DataAccess`**: Entity Framework Core ve PostgreSQL veritabanı bağlantı katmanı.
* **`ErasmusSystem.Business`**: İş kurallarının (Validation, Logic) işletildiği katman.
* **`ErasmusSystem.Entities`**: Veritabanı tablo modelleri.
* **`ErasmusSystem.Tests`**: **(Projenin Ana Odağı)** xUnit ve Playwright ile yazılan Test Senaryoları.

## 🧪 Test Stratejisi
Bu projede "Shift-Left Testing" yaklaşımı benimsenmiş olup, geliştirme ile eş zamanlı test yazımı hedeflenmiştir:
1.  **Birim Testler (Unit Tests):** İş mantığı kurallarının doğrulanması.
2.  **API Testleri:** Postman ve RestSharp ile servis uçlarının (Endpoints) kontrolü.
3.  **UI Otomasyonu:** Playwright kullanılarak arayüz test senaryolarının koşulması.

## 📂 Dokümantasyon
Projenin analiz, tasarım ve test planlama dokümanlarına aşağıdaki klasörden erişebilirsiniz:

* **`/docs`**: [Hafta 1-9 İlerleme Raporu ve Test Planları](./docs/Hafta%201-9%20Rapor.pdf)
* **`/database`**: ER Diyagramları ve SQL Scriptleri (DataAccess katmanında uygulanmıştır).

## 🚀 Kurulum (Nasıl Ayağa Kaldırılır?)
1.  `docker-compose up -d` komutu ile PostgreSQL veritabanını başlatın.
2.  `ErasmusSystem.sln` dosyasını Visual Studio 2022 ile açın.
3.  `Update-Database` komutu ile veritabanı tablolarını oluşturun.
4.  Projeyi çalıştırın ve `/swagger` adresinden API dokümantasyonunu inceleyin.

---
*Bu proje, T.C. Antalya Belek Üniversitesi (Temsili) Yazılım Mühendisliği Bölümü bitirme projesi kapsamında geliştirilmektedir.*
