# C# ve Azure ile Gerçek Zamanlı Speech-to-Text Projesi

Bu proje, Nesne Tabanlı Programlama dersi ödevi kapsamında geliştirilmiş bir C# Windows Forms uygulamasıdır. Kullanıcının varsayılan mikrofonundan aldığı **Türkçe** (`tr-TR`) konuşmayı, Microsoft Azure Bilişsel Hizmetleri'ni (Cognitive Services) kullanarak gerçek zamanlı olarak metne dönüştürür.

Proje, gizli API anahtarlarını kodun dışında tutmak için `.gitignore` ve `App.config` yapılandırma dosyalarını kullanır.

## 🛠 Kullanılan Teknolojiler

* C# (.NET Framework)
* Windows Forms
* Visual Studio 2022
* **Azure Bilişsel Hizmetler - Konuşma SDK'sı**
* `App.config` ile API Anahtarı Yönetimi
* `.gitignore`

---

## 🚀 Projeyi Çalıştırma (Kurulum Adımları)

Bu projeyi kendi bilgisayarınızda çalıştırmak için bu adımları sırayla takip etmelisiniz.

### 1. Adım: Projeyi Klonlama

Projeyi bilgisayarınıza klonlayın (veya ZIP olarak indirin).

```bash
git clone (https://github.com/Nananisnana/Speech-to-Text.git)
```

### 2. Adım: Azure API Anahtarı Alma

Bu proje, çalışmak için bir Azure Konuşma Hizmeti API Anahtarı gerektirir.

1.  [Azure Portal](https://portal.azure.com/) adresine gidin. (Ücretsiz bir hesap oluşturabilirsiniz).
2.  Portalda, "Kaynak oluştur" diyerek bir **"Konuşma hizmetleri" (Speech services)** kaynağı oluşturun.
3.  Fiyatlandırma katmanı olarak **"Free F0" (Ücretsiz F0)** seçeneğini seçin.
4.  Kaynak oluşturulduktan sonra, kaynağın **"Anahtarlar ve Uç Nokta" (Keys and Endpoint)** bölümüne gidin.
5.  Buradaki **`ANAHTAR 1` (KEY 1)** ve **`BÖLGE` (LOCATION / REGION)** (örn: `eastus`, `westeurope`) bilgilerini kopyalayın.

### 3. Adım: API Anahtarlarını Yapılandırma

Gizli anahtarlarınızı asla doğrudan koda yazmamalısınız. Bu proje, anahtarları `App.config` dosyasından okur.

1.  Proje klasöründe **`App.config.example`** adında bir şablon dosya göreceksiniz.
2.  Bu dosyanın bir kopyasını oluşturun ve adını **`App.config`** olarak değiştirin.
3.  Yeni oluşturduğunuz `App.config` dosyasını bir metin düzenleyici ile açın.
4.  `<appSettings>` bölümünü bulun ve 2. Adımda aldığınız kendi anahtar ve bölge bilgilerinizle doldurun:

    ```xml
    <appSettings>
        <add key="AzureSpeechKey" value="SENİN_ANAHTARIN_BURAYA_GELECEK" />
        <add key="AzureSpeechRegion" value="SENİN_BÖLGE_ADIN_BURAYA_GELECEK" />
    </appSettings>
    ```

### 4. Adım: Visual Studio'da Projeyi Açma

1.  Projeyi (`.sln` uzantılı dosyayı) Visual Studio 2022 ile açın.
2.  Visual Studio, eksik olan NuGet paketlerini (özellikle `Microsoft.CognitiveServices.Speech`) otomatik olarak geri yükleyecektir.
    * *Eğer yüklemezse:* Solution Explorer'da proje adına sağ tıklayın ve **"Restore NuGet Packages" (NuGet Paketlerini Geri Yükle)** seçeneğine tıklayın.
3.  `System.Configuration` referansının eklendiğinden emin olun (Kodun `App.config`'i okuması için gereklidir).

### 5. Adım: Platform Hedefini Ayarlama (Çok Önemli!)

Azure Speech SDK'sı yerel (native) C++ kütüphaneleri kullandığından, projenin `x64` (veya `x86`) olarak derlenmesi gerekir. Varsayılan **"Any CPU"** ayarı, `System.TypeInitializationException` hatasına neden olur.

1.  Visual Studio'da "Başlat" (Start ▷) butonunun solundaki "Any CPU" yazan açılır menüye tıklayın.
2.  **"Configuration Manager..." (Yapılandırma Yörüneticisi)** seçeneğine tıklayın.
3.  "Active solution platform" (Etkin çözüm platformu) açılır menüsünden **`<New...>` (Yeni...)** seçeneğini seçin.
4.  Yeni platform olarak **`x64`**'ü seçin ve "OK" deyin.
5.  Pencereyi kapatın.

### 6. Adım: Çalıştırma

Platform hedefinin **"x64"** olarak ayarlandığından emin olun ve yeşil **"Başlat" (Start ▷)** butonuna basarak projeyi çalıştırın.
