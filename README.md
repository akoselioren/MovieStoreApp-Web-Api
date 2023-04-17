<h1 align="center">🌟 MovieStoreApp( Web API ) 🌟</h1>
</br>
.NET Core ile MovieStore Web API'si geliştirdim. Veritabanı olarak In Memory kullandım ve XUnit test ile testlerini yazdım.
Detaylı bir şekilde aşşağıda görseller ile destekleyerek projenin açıklaması bulunmaktadır.
</br>
</br>
Patika Academy'deki <a href="https://academy.patika.dev/courses/net-core">link</a> kurstan faydalanarak oradaki Book Store projesinin farklı bir versiyonu olan Movie Store'yi yaparak
kendimi geliştirdim.

<h2 align="left">- Kullanıdığım araçlar ve yöntemler</h1>
<p>• Entity Framework </p>
<p>• Linq ile CRUD işlemleri</p>
<p>• Relational database</p>
<p>• Entity, Model ve Mapper Kullanımı</p>
<p>• Fluent Validation Kullanarak Modellerin Doğrulanması</p>
<p>• Custom Exception Middleware</p>
<p>• Projeye DI Container Kullanarak Logger Servis Eklemek</p>
<p>• TDD (Test Driven Development)</p>
<p>• Token Bazlı Kimlik Doğrulama ve Access Token Kullanımı</p>
<p>• Refresh Token Kullanımı</p>

<h1 align="center">🟠 Proje Görselleri ve Açıklamaları 🟠</h1>
</br>
<h2>🔶 Linq komutları ile CRUD işlemleri </h2>
<p>• Aktör(Actor) Controller'de : Tüm aktörleri Listeleme, ID ile listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>
<p>• Müşteri(Customer) Controller'de : Tüm müşterileri Listeleme, ID'ye göre listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>
<p>• Yönetmen(Director) Controller'de : Tüm yönetmenleri Listeleme, ID'ye göre listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>
<p>• Film türü(Genre) Controller'de : Tüm film türleri Listeleme, ID'ye göre listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>
<p>• Film(Movie) Controller'de : Tüm filmleri Listeleme, ID'ye göre listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>
<p>• Sipariş(Order) Controller'de : Tüm siparişleri Listeleme, ID'ye göre listeleme, Ekleme, Silme ve Güncelleme işlemleri</p>

![Crud1](https://user-images.githubusercontent.com/112801816/232340442-67888c2b-50db-4938-87bb-ed64b393aeeb.png)

</br>
<h2>🔶 Oluşan veri modellerinin şeması </h2>
</br>

![Veri modellerininschemes](https://user-images.githubusercontent.com/112801816/232341274-16cf2196-aaff-4729-9d11-cc778b70b9f5.png)

</br>
<h2>🔶 Signin işlemi </h2>
</br>
<p>◉ Postman üzerinden Post methodu ile JSON veri tipinde yeni bir kullanıcı oluşturuyoruz.</p>
<p>◉ İşlem başarılı olduğu için bize 200 OK Status Kodu dönüyor.</p>

![signin](https://user-images.githubusercontent.com/112801816/232341456-ec2723a3-95b2-400c-8e9c-52d29672b359.png)

</br>
<h2>🔶 Validation işlemleri </h2>
</br>
<p>◉ Burada tekrar Send'e tıklayarak tekrar oluşturmaya çalıştığımda yazmış olduğum validasyonlara takılarak hata veriyor.</p>
<p>◉ İşlem başarısız olduğu için bize 500 Server Error Status Kodu dönüyor.</p>

![customerror](https://user-images.githubusercontent.com/112801816/232341977-7fbd6a2b-0cde-4e47-ab96-3a19d4ece866.png)


<p>◉ Burada ise bilerek şifreyi yanlış giriyorum ve tekrar validasyonlara takılıyor.</p>
<p>◉ İşlem başarısız olduğu için bize 500 Server Error Status Kodu dönüyor.</p>

![customerror2](https://user-images.githubusercontent.com/112801816/232342096-af3cbdde-619c-49a2-8641-40bf97591660.png)


</br>
<h2>🔶 Token işlemleri </h2>
</br>

<h3>🟢 Token oluşturma </h3>
<p>◉ Yukarıda oluşturduğum kullanıcı ile giriş isteğinde bulunuyorum ve bana token oluşturuyor</p>
<p>◉ İşlem başarılı olduğu için bize 200 OK Status Kodu dönüyor.</p>
<p>- Access Token (Erişim Belirteci): Bir kaynağa ulaşmak için verilmiş belirteçtir. </p>
<p>- Expiration (Erişim Süresi): Bu süre Access Token'in erişim süresidir, Yazılımcının belirlediği erişim süresi sona erdiğinde Refresh Token ile bu süre tekrar yenilenir. </p>
<p>- Refresh Token (Yenileme Belirteci): Bir erişim belirtecinin geçersiz olduğu durumlarda kullanılmak üzere oluşturulmuş olan ve bu geçersiz belirtecin güncellenmesini/yenilenmesini sağlayan belirteçtir. </p>

![token oluştu](https://user-images.githubusercontent.com/112801816/232351906-1f085ebf-fd34-49c8-8292-f3a987663266.png)

</br>
<h3>🟢 Authorize işlemi </h3>
<p>◉ Microsoft.AspNetCore.Authorization kütüphanesini kullanarak [Authorize] Attribute ile erişim kısıtı getireceğimiz Controller'ları belirliyoruz ve böylece sisteme Authenticate olmadan ulaşamıyor.</p>
<p>◉ Bize oluşturulan Token ile istekte bulunmadığım için bana 401 Unauthorized hatası verdi.</p>

![Anauth](https://user-images.githubusercontent.com/112801816/232353150-a28f153d-eecb-4e96-bf14-d62aef5e7584.png)

</br>
<h3>🟢 Başarılı Token işlemi </h3>
<p>◉ Access Token ile Postmandeki Authorization tab'i içinde Bearer Token tipinde Token'imi yazarak istekte bulunuyorum. </p>
<p>◉ Token'imin süresi dolmamış ve doğru token ise bana izin vererek işlemi gerçekleştirmemi sağlıyor.</p>
<p>◉ İşlem başarılı olduğu için bize 200 OK Status Kodu dönüyor.</p>

![authantice](https://user-images.githubusercontent.com/112801816/232353812-aa823632-4e24-4038-84b0-bf6cc8632b7d.png)

</br>
<h3>🟢 Refresh Token Kullanımı </h3>
<p>◉ Access Token'ımızın süresi dolduğunda Kullanıcı Tekrardan Login işlemine geçmeden, Kullanıcı Logout olmadan bir kullanım sunmamızı sağlamaktadır.</p>
<p>◉ Refresh Token ile bir istekte bulunarak yeni bir Access Token ürettik ve süremizde yenilenmiş oldu.</p>
<p>◉ İşlem başarılı olduğu için bize 200 OK Status Kodu dönüyor.</p>

![newrefresh](https://user-images.githubusercontent.com/112801816/232355555-c8875000-a4a6-430a-94b6-de92b339b8a6.png)

</br>
<h2>🔶 Custom Console Log işlemi </h2>
</br>
<p>◉ Custom Exception Middleware ile tüm Request ve Response'lar Console'da istediğimiz formatta loglanıyor. </p>
<p>◉ Exception durumlarında yazmış olduğumuz Validasyonlar ile hata fırlattığında Console Loglanmaktadır.</p>
<a href="https://github.com/akoselioren/MovieStoreApp/blob/master/WebApi/Middlewares/CustomExceptionMiddleware.cs" target="_blank">[Kodlara gitmek için tıklayın.]</a>

![Console Logging2](https://user-images.githubusercontent.com/112801816/232364291-bf027100-574b-4a93-b1ab-71ab4eeb4e8a.png)

</br>
<h2>🔶 TDD xUnit Test ile birim testi </h2>
</br>
<p>◉ Birim testi, yazılım programlamasında bir tasarım ve geliştirme yöntemidir. </p>
<p>◉ Bu yöntemde yazılımcı yazılım kodunu oluşturan birimlerin kullanıma hazır olduğuna iknâ olur. </p>
<p>◉ Birim, bir yazılım uygulamasında test edilebilecek en küçük bölüme denir. </p>
<p>◉ Başlangıç olarak test işlemlerinin aşamalarını görerek bu konu hakkında tecrübe edinmiş oldum.</p>

![Test](https://user-images.githubusercontent.com/112801816/232364588-cd10bc76-7130-44a2-9924-7ad8f0ce0982.png)

</br>
<h2 align="center">🔶 Proje Kurulumu Hakkında Bilgiler </h2>
</br>

<p>Proje'yi direk indirip çalıştırabilirsiniz. Veri tabanı olarak InMemory yapısını kullandığım için veri tabanı entegre etmenize ihtiyaç yoktur.</p>
<p>Projeyi .NET CORE 5 İle geliştirdim.</p>
<p>Elimden geldiğince okunabilirliği yüksek olması için yalın bir şekilde SOLID Prensiplerine bağlı kalarak geliştirmeye özen gösterdim.

<h3>🟢 Kullanılan kütüphaneler </h3>
<p>◉ AutoMapper </p>
<p>◉ AutoMapper.Extensions.Microsoft.DependencyInjection </p>
<p>◉ FluentAssertions </p>
<p>◉ FluentValidation </p>
<p>◉ Microsoft.AspNetCore.Authentication.JwtBearer </p>
<p>◉ Microsoft.EntityFrameworkCore </p>
<p>◉ Microsoft.EntityFrameworkCore.InMemory </p>
<p>◉ Microsoft.NET.Test.Sdk </p>
<p>◉ Moq </p>
<p>◉ Newtonsoft.Json </p>
<p>◉ Swashbuckle.AspNetCore </p>
<p>◉ xunit </p>
</br>

<h2 align="center">🌟Proje'yi Yıldızlayıp(⭐) bana destekte bulunabilirsiniz..🌟</h5>


