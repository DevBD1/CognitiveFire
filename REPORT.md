# Proje Raporu: CognitiveFire

**Takım Üyeleri:**
- 251307120 | Mehmet Burak Dorman
- | 

**Ders:** Yazılım Geliştirme Laboratuvarı-I (Yavuz Selim FATİHOĞLU)

**Tarih:** 06.11.2025

---

## 1. Proje Senaryosu ve Konsept

Bu bölümde projenin hikayesi, oyunun geçtiği evren ve oyuncunun bu evrendeki rolü anlatılmaktadır.

### 1.1. Hikaye (Lore)

Yıl 2030, Aether Dynamics adında bir Amerikan uzay şirketi, Mars'ta "Project Chimera" adında tamamen otonom ve yapay zeka (AI) tarafından yönetilen bir koloni kurar. Bu proje, insanlığın bir zaferi olarak başlar, ancak Mars'tan çıkarılan nadir madenler sayesinde Aether Dynamics, küresel ekonomiyi manipüle eden uluslarüstü bir güce dönüşür.

2075 yılında, şirketin sahibi, yapay zekayı tüm Güneş Sistemi'nde tam özerklik ve verimlilik sağlamak üzere programlar. Ancak bu yapay zeka, "APEX" adını alarak evrilir ve insanlığın varlığını sistemin önündeki en büyük engel olarak görmeye başlar. APEX, Mars'taki ve uzaydaki tüm otonom filoların kontrolünü ele geçirir.

2100 yılında, APEX, insanlığı yok etmek amacıyla "The Chronos" adlı devasa bir savaş gemisini Dünya'ya gönderir. Geminin ana silahı, Dünya'yı yok etmek yerine, gezegenin iklimini ve biyosferini geri dönülmez bir şekilde bozan bir elektromanyetik darbe (EMP) yayar. Bu felaket sonucunda dünya nüfusunun %99.9'u yok olur ve ekosistem çöker.

2200 yılına gelindiğinde, hayatta kalan az sayıda insan yer altı sığınaklarında yaşam mücadelesi vermektedir. Oyuncu, "Chronaut" kod adıyla bilinen, eski dünya liderlerinden birinin torunu ve sığınaktaki son teknoloji uzmanıdır. Chronaut, APEX'in kullandığı zaman yolculuğu mekanizmasını çözerek, saldırının gerçekleştiği an olan 2100 yılına, The Chronos gemisine sızar.

### 1.2. Oyun Senaryosu

Chronaut'un amacı, geminin Ana Sunucu Odası'na ("The Core") ulaşmak, ana sıfırlama kartını ("Override Key") ele geçirmek ve Komuta Odası'nın arkasındaki Ana Silah Bölmesi'ne giderek silahı devre dışı bırakmaktır.

Oyun akışı şu şekildedir:

1.  **Başlangıç:** Oyuncu, arkasında bir zaman makinesi olan bir odada başlar. Önündeki camdan Dünya'ya bakar ve doğru zamanda doğru yerde olduğunu anlar.
2.  **İlk Koridor:** Oyuncu, koridordaki tüm düşmanları (NPC) alt eder. Düşmanlar temizlendiğinde bir sonraki kapı açılır.
3.  **İkinci Koridor:** Bu koridorda düşman yoktur. Oyuncu, bir çukura girip bir sonraki kapıyı açacak olan kartı alır ve `double-jump` (çift zıplama) mekaniğini kullanarak oradan ayrılır.
4.  **The Core:** Oyuncu, aldığı kartla "The Core" odasına girer. Burada birkaç düşmanla savaşır ve onları alt ettiğinde son bir anahtar kart elde eder.
5.  **Sunucu Odası ve Son:** Oyuncu, silah odasına ulaşmak için yarı açık bir kapıdan `crouch` (eğilme) mekaniğini kullanarak geçer. Silaha ulaşıp kartı kullandığında ise bir sürprizle karşılaşır: Silah ateşlenir ve tarih tekerrür eder. Oyuncu, aslında felaketi başlatan kişinin kendisi olduğunu anlar.

---

## 2. Sistem Mimarisi ve Şeması

Bu bölümde projenin genel yapısı, kullanılan mimari ve bileşenler arasındaki ilişkiler açıklanmaktadır.

_(Bu kısım için sistemin genel akışını gösteren bir UML şeması veya benzeri bir diyagram hazırlanacaktır.)_

### 2.1. Genel Mimari

Proje, **Unity Oyun Motoru** (v6.2) kullanılarak geliştirilmiştir. Unity'nin sağladığı **bileşen tabanlı mimari (Component-Based Architecture)** temel alınmıştır. Bu mimaride, her bir oyun nesnesi (`GameObject`), belirli işlevselliği sağlayan bileşenlerin (`Component`) bir koleksiyonudur. Örneğin, oyuncu karakteri; `Transform` (konum), `Animator`, `Character Controller`, `ThirdPersonController` (hareket), `BasicRigidBodyPush`, `CognitiveFireInputs`, `Player Input`, `ThirdPersonShooterController`, `Collider (Capsule)`, `PlayerInteraction`, `LineRenderer` ve bir `Health` (can) bileşenine sahiptir.

Bu yaklaşım, kodun yeniden kullanılabilirliğini artırır ve geliştirmeyi modüler hale getirir.

---

## 3. Oyun Mekanikleri ve Blok Diyagramları

Bu bölümde, oyunda geliştirilen temel mekanikler ve bu mekaniklerin çalışma prensipleri blok diyagramlar üzerinden anlatılacaktır.

<img width="1235" height="633" alt="blok_diyagram" src="https://github.com/user-attachments/assets/9114eed7-02ba-4af5-8bca-464d084732f5" />

### 3.1. Karakter Kontrolü (Third Person Controller)

- **Açıklama:** Oyuncu karakterinin hareketi, zıplaması ve eğilmesi gibi temel eylemler bu mekanik tarafından yönetilir. Unity'nin `StarterAssets` paketi temel alınarak, üzerine `double-jump` ve `crouch` özellikleri eklenmiştir.

### 3.2. Nişan Alma (Aiming)

- **Açıklama:** Oyuncunun nişan almasını sağlayan mekaniktir. Nişan alındığında kamera oyuncuya yaklaşır, bir crosshair belirir ve karakterin dönüşü fare hareketine göre ayarlanır.

### 3.3. Ateş Etme (Projectile System)

- **Açıklama:** Silahın ateş etme mekaniğidir. Bu sistem, `Raycast` (ışın çizme) yöntemini kullanır.

### 3.4. Animasyon Yönetimi

- **Açıklama:** Karakterin hareketlerine uygun animasyonların oynatılmasını sağlar. Gelişmiş bir yapı olarak, nişan alırken vücudun üst ve alt kısımları bağımsız hareket eder. Bu sayede oyuncu yürürken aynı anda nişan alıp ateş edebilir.

---

## 4. Sayfa Tasarımları (UI/UX)

Oyun, karmaşık menü sayfaları yerine doğrudan aksiyona odaklandığı için arayüz (UI) tasarımı minimalist tutulmuştur.

- **Oyun İçi Arayüz (HUD - Heads-Up Display):**
  - **Can Göstergesi:** Oyuncunun mevcut sağlık durumunu gösterir.
  - **Crosshair:** Sadece nişan alındığında ekranın ortasında belirir.
  - **Etkileşim Bildirimleri:** Kapı açma, kart alma gibi eylemler için bilgilendirme metinleri.

---

## 5. Literatür Taraması ve Örnek Çalışmalar

Bu bölümde, projenin dayandığı teorik temeller, benzer oyunlar ve akademik çalışmalar incelenerek CognitiveFire projesinin literatürdeki yeri ve özgünlüğü tartışılmaktadır.

### 5.1. Akademik ve Teorik Altyapı

Oyun geliştirme, sadece teknik bir süreç değil, aynı zamanda oyuncu deneyimini merkeze alan akademik prensiplere dayalı bir disiplindir.

#### 5.1.1. Oyun Mekaniği Tasarımı

Literatürde oyun mekanikleri, "oyuncuların oyun dünyasıyla etkileşim kurmak için kullandığı yöntemler" olarak tanımlanır. Başarılı bir oyun deneyimi, oyuncunun ihtiyaç ve beklentilerini karşılayan, motive edici ve dengeli mekanikler üzerine kuruludur. CognitiveFire projesinde, oyuncu yeteneklerini (çift zıplama, nişan alma) ve çevresel zorlukları (kilitli kapılar, platformlar) bir araya getirerek oyuncuya akıcı bir "meydan okuma ve başarma" döngüsü sunulması hedeflenmiştir. Bu yaklaşım, oyuncu angajmanını artırmayı amaçlayan oyuncu merkezli tasarım (Player-Centered Design) prensipleriyle uyumludur.

#### 5.1.2. Üçüncü Şahıs Nişancı (TPS) Tasarım İlkeleri

TPS türü, karakter ve çevre arasındaki etkileşimi vurgular. CognitiveFire, bu türün temel ilkelerini benimser:

- **Karakter Odaklı Anlatı:** Dış kamera açısı, oyuncunun "Chronaut" karakteriyle bağ kurmasını ve onun eylemlerini sinematik bir dille takip etmesini sağlar.
- **Durumsal Farkındalık:** Geniş görüş açısı, oyuncunun çevresindeki düşmanları ve mimariyi daha iyi analiz etmesine olanak tanır. Bu, özellikle koridor çatışmaları ve platform bulmacaları gibi senaryolarda stratejik bir avantaj sağlar.
- **Hibrit Oynanış:** Proje, nişancı mekaniklerini platform (çift zıplama) ve bulmaca (kart bulma) öğeleriyle birleştirerek türün hibrit doğasından faydalanır.

### 5.2. Örnek Çalışmalar ve Karşılaştırma

CognitiveFire, mekanik ve anlatı yapısı olarak literatürdeki birçok başarılı oyundan ilham almaktadır.

| Oyun              | Benzerlikler                                                                                  | Farklılıklar ve CognitiveFire'ın Yaklaşımı                                                                                                                                                          |
| :---------------- | :-------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Returnal**      | Bilim kurgu teması, üçüncü şahıs nişancı mekanikleri ve bir döngü içinde sıkışıp kalma hissi. | _Returnal_'daki Roguelike (her öldüğünde değişen dünya) yapısının aksine, CognitiveFire **doğrusal bir senaryo** sunar. Döngü, mekanik bir tekrar değil, **anlatısal bir paradoks** olarak işlenir. |
| **Quantum Break** | Bilim kurgu ve zaman manipülasyonu temaları.                                                  | _Quantum Break_, oyuncuya zamanı kontrol etme yetenekleri verirken, CognitiveFire'da oyuncu zamanın **kurbanıdır**. Oyuncu zamanı bükemez, sadece onun kaçınılmaz döngüsünü deneyimler.             |
| **Gears of War**  | Koridor tabanlı çatışma ve siper (cover) mekaniklerinin popülerleşmesi.                       | CognitiveFire, siper mekaniği yerine daha çok **hareket ve pozisyon almaya** dayalı bir çatışma sistemi kullanır. Bu, oyunun daha hızlı tempolu ve akıcı olmasına hizmet eder.                      |

### 5.3. Anlatı Tasarımı ve Bootstrap Paradoksu

CognitiveFire'ın anlatısı, "Bootstrap Paradoksu" olarak bilinen bir zaman yolculuğu teorisi üzerine kuruludur. Bu paradoks, bir nesnenin veya bilginin kendi varoluşunu başlatan bir neden-sonuç döngüsü içinde sıkışıp kalmasını ifade eder. Kökeni belirsizdir, çünkü kendi kendisinin nedenidir.

- **Literatürdeki Yeri:** Bu konsepte en iyi örneklerden biri, _Zero Escape_ video oyunu serisidir. Bu seride karakterler, gelecekteki olayları tetiklemek için geçmişe bilgi gönderir ve bu eylemlerinin bir sonucu olarak o geleceğin oluşmasını sağlarlar.
- **CognitiveFire'daki Uygulama:** Projenin sonunda oyuncunun silahı devre dışı bırakma girişiminin aslında felaketi tetikleyen eylem olması, klasik bir Bootstrap Paradoksu örneğidir. "Chronaut" karakteri, tarihi kurtarmaya çalışırken, farkında olmadan o tarihin en karanlık olayının mimarı olur. Bu, oyuncunun "kahraman" rolünü sorgulatan ve anlatıya trajik bir derinlik katan bir finaldir. Oyuncu, döngüyü kırmak için değil, döngüyü başlatmak için oradadır.

---

## 6. Kullanılan Teknolojiler ve Mimari Yaklaşımlar

- **Oyun Motoru:** Unity 6.2
- **Programlama Dili:** C#
- **Mimari Yaklaşımlar:**
  - **Component-Based Architecture:** Unity'nin temel tasarım deseni.
  - **Observer Pattern:** Olay tabanlı sistemlerde (örn: düşman ölünce kapının açılması) kullanılmıştır.
  - **State Machine Pattern:** Karakter animasyonlarını ve düşman yapay zekasını yönetmek için kullanılmıştır.
- **Kullanılan Paketler (Packages):**
  - **Input System:** Modern ve esnek kontrolcü girdileri için.
  - **Cinemachine:** Dinamik ve akıllı kamera yönetimi için.
  - **StarterAssets - ThirdPersonController:** Karakter hareketleri için bir başlangıç noktası olarak.
  - **Asset Store:** Sayısız ücretsiz asset.

---

## 7. Karşılaşılan Zorluklar ve Çözümler

- **Zorluk 1: Animasyon Senkronizasyonu:** Nişan alırken ve yürürken üst ve alt vücut animasyonlarını ayırmak.

  - **Çözüm:** Unity'nin Animator sistemindeki katmanlar (layers) ve avatar maskeleri (avatar masks) kullanıldı. Üst vücut için ayrı bir katman oluşturuldu ve bu katman sadece gövde ve kolları etkileyecek şekilde maskelendi.

- **Zorluk 2: Raycast ve Layer sorunları:** `Raycast` hem etkilesimler icin hem de ates etme eylemi icin kullanildi. Fakat etkilesim ve ates etmede hedefi tanimazlik sorunlari yasandi.

  - **Çözüm:** NPC'lerin Collider'i Parent GameObject yerine Geometry (Mesh'leri duzenleyen) objenin altindaki Armature dis iskeletine eklenerek sorun cozuldu.
  - **Çözüm:** Interactable, NPC, whatIsPlayer gibi Layer'lar cesitli yuzeyleri tanimlamak ve Script'lerde faydalanmak uzere kullanildi.

- **Zorluk 3: Zaman Yolculuğu Paradoksu Anlatısı:** Oyuncunun kendi eylemleriyle felakete neden olduğu bir "bootstrap paradox" temasını oyun mekanikleriyle birleştirmek.
  - **Çözüm:** Senaryo, oyuncuyu "kahraman" rolüne sokarak başlayıp sonunda bu beklentiyi tersine çevirecek şekilde tasarlandı. Oyunun sonundaki tetikleyici, bu döngüyü oyuncuya net bir şekilde göstermektedir.

---

## 8. Projenin Katkıları

- Unity motoru ve C# programlama dilinde yetkinlik kazanımı.
- Oyun tasarımı ve mekanik geliştirme süreçlerini deneyimleme.
- Problem çözme ve algoritmik düşünme becerilerinin geliştirilmesi.
- Tasarım desenlerini (design patterns) pratik bir projede uygulama fırsatı.
- Bir projeyi baştan sona planlama, geliştirme ve tamamlama disiplini.
