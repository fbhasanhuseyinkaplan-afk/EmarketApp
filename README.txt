EMARKETAPP PROFESYONEL PAKET

Kurulum:
1) SSMS aç.
2) SQL/EmarketDB_Kurulum.sql dosyasını çalıştır.
   Bu script EmarketDB veritabanını sıfırdan kurar.
3) EmarketApp.sln dosyasını Visual Studio 2013 ile aç.
4) EmarketApp/DAL/Db.cs içindeki connection string'i kendi bilgisayarına göre düzelt.

Varsayılan giriş:
Kullanıcı: admin
Şifre: 1234

Önemli:
- Formlar kod ile oluşturuldu. Designer dosyası yok.
- Bu yüzden "designer uçtu" hatası yaşamazsın.
- Eğer SQL Server adın farklıysa sadece Db.cs değişecek.

Proje yapısı:
- DAL: Veritabanı işlemleri
- Entities: Tablo classları
- Forms: Login, Ana Panel, Ürün Yönetimi, Satış Ekranı
