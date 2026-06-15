# UnitTesting

Bu repository, .NET ekosisteminde unit test yazımını temelden gerçek hayat senaryolarına kadar adım adım inceleyen bir projedir. Proje; basit hesap makinesi testlerinden başlayıp assertion teknikleri, dependency isolation, mocking, validation, logging ve ASP.NET Core Web API servislerinin test edilmesine kadar genişleyen dört ayrı bölümden oluşur.

Amaç yalnızca test yazmak değil; test edilebilir kod tasarımı, bağımlılıkların soyutlanması, servis katmanı davranışlarının doğrulanması ve gerçek bir API senaryosunda hata/başarı akışlarının güvence altına alınmasıdır.

## Proje Yapısı

```text
UnitTesting/
├── Fundamentals/
│   ├── src/CalculatorLibrary
│   └── test/CalculatorLibraryTests
├── Concepts/
│   ├── src/UnderstandingDependencies.Api
│   └── test/UnderstandingDependencies.Api.Tests.Unit
├── Techniques/
│   ├── src/TestingTechniques
│   └── test/TestingTechniques.Test.Unit
├── RealWorld/
│   ├── src/Users.Api
│   └── test/Users.Api.Tests.Unit
├── UnitTesting.sln
└── UnitTesting.slnx
```

## Kullanılan Teknolojiler

| Teknoloji / Kütüphane | Kullanım Amacı |
| --- | --- |
| .NET 10 | Tüm class library, Web API ve test projelerinin hedef framework'ü |
| C# | Uygulama, API ve test kodlarının geliştirilmesi |
| ASP.NET Core Web API | `Concepts` ve `RealWorld` bölümlerindeki HTTP API projeleri |
| Entity Framework Core | Repository katmanında SQL Server erişimi ve migration yönetimi |
| SQL Server LocalDB | Lokal geliştirme veritabanı senaryosu |
| xUnit | Unit test framework'ü |
| FluentAssertions | Okunabilir ve expressive assertion yazımı |
| NSubstitute | Mock, substitute ve davranış doğrulama |
| Moq | Alternatif mocking yaklaşımını göstermek için konsept seviyesinde kullanım |
| FluentValidation | DTO doğrulama kurallarının modellenmesi |
| Coverlet | Test coverage toplama altyapısı |
| Microsoft.NET.Test.Sdk | .NET test runner entegrasyonu |

## Bölümler

### Fundamentals

`CalculatorLibrary` projesi unit testin temel yapı taşlarını gösterir. Dört temel işlem üzerinden `Theory`, `InlineData`, expected/actual karşılaştırması, test fixture yaşam döngüsü ve xUnit output kullanımı örneklenir.

Kapsanan başlıklar:

- `Fact` ve `Theory` yaklaşımı
- Parametrik testler
- FluentAssertions ile okunabilir doğrulamalar
- `IAsyncLifetime` ile test setup/teardown akışı
- Skip edilen test senaryosu

### Techniques

`TestingTechniques` bölümü, farklı veri tipleri ve davranışlar üzerinde assertion pratiği yapmak için hazırlanmıştır. String, sayı, tarih, nesne, koleksiyon, exception, event ve internal member testleri içerir.

Kapsanan başlıklar:

- String assertion'ları
- Numeric assertion'lar
- `DateOnly` doğrulamaları
- Nesne karşılaştırmaları ve `BeEquivalentTo`
- Koleksiyon doğrulamaları
- Exception testleri
- Event raise doğrulama
- `InternalsVisibleTo` ile internal üyelerin test edilmesi

### Concepts

`UnderstandingDependencies.Api`, bağımlılıkları anlamaya odaklanan bir ASP.NET Core Web API örneğidir. Repository ve service katmanı üzerinden veri çekme senaryosu bulunur. Test projesinde repository bağımlılığı mock'lanarak servis davranışı izole şekilde doğrulanır.

Kapsanan başlıklar:

- Dependency abstraction
- Repository interface kullanımı
- Service unit testleri
- NSubstitute ile bağımlılıkları izole etme
- Moq kullanımına dair alternatif örnekler
- EF Core ve SQL Server bağlantı konsepti

### RealWorld

`Users.Api`, projedeki en kapsamlı gerçek hayat senaryosudur. Kullanıcı yönetimi için servis, repository, controller, validation ve logging katmanlarını içerir. Bu bölümde amaç, bir API'nin iş kurallarını database veya framework detaylarına bağımlı kalmadan test etmektir.

API davranışları:

- Kullanıcıları listeleme
- Id ile kullanıcı getirme
- Yeni kullanıcı oluşturma
- Kullanıcı silme
- Aynı isimle kullanıcı oluşturmayı engelleme
- Geçersiz DTO verisini reddetme
- Operasyonları loglama
- Exception durumlarında loglama ve hatayı yukarı taşıma

Test edilen senaryolar:

- Boş ve dolu liste sonuçları
- Kullanıcı bulunması / bulunmaması
- Validation hataları
- Duplicate name kontrolü
- Başarılı create/delete akışları
- Repository exception durumları
- Logger çağrılarının doğrulanması
- Controller action'larının HTTP 200 sonucu dönmesi

## Mimari Yaklaşım

Proje özellikle test edilebilirlik prensiplerini görünür hale getirir:

- Controller katmanı HTTP request/response sorumluluğunda tutulur.
- Service katmanı iş kurallarını, validation'ı, logging'i ve hata akışlarını yönetir.
- Repository katmanı veri erişimini soyutlar.
- Interface'ler sayesinde servisler testlerde gerçek veritabanına ihtiyaç duymadan doğrulanır.
- Logging, `ILoggerAdapter` ile soyutlanarak testlerde doğrulanabilir hale getirilir.
- Validation kuralları `FluentValidation` ile merkezi ve okunabilir şekilde tanımlanır.

## Test Kapsamı

Bu repository toplam 47 test senaryosu içerir:

- 46 başarılı test
- 1 bilinçli olarak atlanan test

Son doğrulama komutu:

```bash
dotnet test UnitTesting.sln --no-restore
```

Test sonucu:

```text
Failed: 0, Passed: 46, Skipped: 1, Total: 47
```


## Öne Çıkan Kazanımlar

- Unit test yazımının yalnızca assertion seviyesinde değil, kod tasarımı seviyesinde ele alınması
- Bağımlılıkların interface üzerinden soyutlanması
- Mock/substitute kullanarak hızlı ve izole testler yazılması
- Service ve controller davranışlarının ayrı ayrı doğrulanması
- Validation, exception handling ve logging gibi gerçek dünya ihtiyaçlarının test kapsamına alınması
- Test isimlendirmelerinde `Method_ShouldExpectedBehavior_WhenCondition` formatının kullanılması
