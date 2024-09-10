# Net CI Proof Projesi

### Projenin Amacı

Bu proje, GitHub Actions kullanarak otomatik olarak unit testlerin çalıştırılmasını sağlar. Unit testler hem bir pull request oluşturulduğunda hem de doğrudan master branşına birleştirme yapıldığında tetiklenir. Bu, kodun her iki durumda da sağlıklı ve hatasız olduğundan emin olunmasını sağlar.

### Proje Yapısı
Bu proje aşağıdaki katmanlardan oluşur:

* Api.Controllers: API uç noktalarını tanımlar ve HTTP isteklerini işler.
* Api.Application: İş mantığını içerir ve uygulama servislerini sağlar.
* Api.Infrastructure: Veri erişimi ve veri modeli yapılandırması ile ilgili kodları içerir.

### Kurulum
Projeyi çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. Gereksinimler:

- .NET Core SDK 7.0
- Bir veritabanı (örneğin, SQLite, SQL Server veya PostgreSQL) kurulumu

2. Proje Dosyalarını İndirin:

```
https://github.com/BerkayMehmetSert/net.CI.Proof.git
cd net.CI.Proof
```

3. Bağımlılıkları Yükleyin:

```
dotnet restore
```

4. Projeyi Çalıştırın:

```
dotnet run
```

### Kullanım

#### API Uç Noktaları

**Get All Products**

* GET /api/products
* Açıklama: Tüm ürünleri getirir.
* Yanıt: 200 OK ve ürünlerin listesi.
* Get Product By ID

**GET /api/products/{id}**

* Açıklama: Belirtilen ID'ye sahip ürünü getirir.
* Parametreler: id (ürünün ID'si)
* Yanıt: 200 OK ve ürün bilgileri. 500 Not Found eğer ürün bulunamazsa.

**Create Product**

* POST /api/products
* Açıklama: Yeni bir ürün oluşturur.
* Gövde: CreateProductRequest (Ürün adı ve fiyatı)
* Yanıt: 201 Created ve oluşturulan ürün bilgileri.

**Update Product**

* PUT /api/products/{id}
* Açıklama: Belirtilen ID'ye sahip ürünü günceller.
* Parametreler: id (ürünün ID'si)
* Gövde: UpdateProductRequest (Güncellenmiş ürün bilgileri)
* Yanıt: 204 No Content eğer başarılı bir şekilde güncellenirse. 500 Not Found eğer ürün bulunamazsa.

**Delete Product**

* DELETE /api/products/{id}
* Açıklama: Belirtilen ID'ye sahip ürünü siler.
* Parametreler: id (ürünün ID'si)
* Yanıt: 204 No Content eğer başarılı bir şekilde silinirse. 500 Not Found eğer ürün bulunamazsa.

### Katmanlar ve Sınıflar

`ProductsController`: API uç noktalarını tanımlar.

`AppDbContext`: Entity Framework Core veritabanı bağlamı ve yapılandırması.

`ProductRepository`: Veri erişim katmanı.

`ProductService`: İş mantığı ve uygulama servisleri.

### GitHub Actions

Proje, GitHub Actions kullanarak sürekli entegrasyon sağlar. Her pull request ve doğrudan master branşına yapılan birleştirme işlemleri sırasında unit testler otomatik olarak çalıştırılır. Bu, kod kalitesini ve işlevselliğini korumaya yardımcı olur.

#### GitHub Actions Yapılandırması
`actions.yml`: Testlerin otomatik olarak çalıştırılmasını sağlayan GitHub Actions iş akışını tanımlar. build ve test adımlarını içerir.