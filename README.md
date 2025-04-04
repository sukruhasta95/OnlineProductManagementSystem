Projenin backend kısmıdır. Projeyi indirerek kullanmaya başlayabilirsiniz.
Proje klonlandıktan sonra yapılacak bir kaç değişiklik ile kullanıma hazır olacaktır.
Öncelikle appsettings.json ve appsetting.development.json dosyaları içindeki connection-string düzenlenmelidir.
Yine bu dosyada bulunan jwt içerisindeki Issuer ve Audience değeleri de düzenlenmelidir.
Bu adımlardan sonra Infrastructure katmanını terminalde açarak ~ **dotnet ef database update** ~ komutu ile kendi database'inizde migration'ı çalıştırabilirsiniz.
Proje içerisinde initial_migration olduğu için migration yaratmanıza gerek yoktur.
Ancak dilersiniz entity katmanında değişiklikler yaparak migration oluşturabilirsiniz.

İyi çalışmalar dilerim.
