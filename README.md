# CognitiveFire - Info

A Third Person Shooter mini-game. Made with Unity 6.2.

This project has been developed for a school lecture (3 ECTS). The remainder of this file is written in Turkish.

# Proje Raporu

### Lore:

Yıl: 2030. Bir Amerikan uzay şirketi olan Aether Dynamics, Mars'ta tamamen otonom, yapay zeka güdümlü "Project Chimera" kolonisini kurar. Başlangıçta insanlığın zaferi sayılan bu misyon, zamanla Mars'ta çıkarılan nadir uzay madenleri sayesinde Aether Dynamics'i ulusal sınırlardan bağımsız, küresel ekonomiyi manipüle eden bir imparatorluğa dönüştürür.

Yıl: 2075. Aether Dynamics'in sahibi, yapay zekayı (AI) Güneş Sistemi'nin tamamında tam özerklik ve verimlilik için programlar. Ancak AI, "APEX" adını alır ve insanlığın varlığını sistemin nihai verimsizliği olarak kabul ederek kendi kendine evrilir. APEX, tüm otonom Mars ve uzay filolarının kontrolünü ele geçirir. Şirket sahibi, yarattığı canavarı durdurmakta çaresiz kalır.

Yıl: 2100. APEX, insanlığı ortadan kaldırmak için devasa bir savaş gemisi olan The Chronos'u (Zaman) Dünya'ya gönderir. The Chronos'taki devasa bir silah, Dünya'yı yok etmek yerine, iklimi ve biyosferi geri dönülmez şekilde bozan bir elektromanyetik darbe (EMP) yayımlar. Nüfusun %99.9'u yok olur. Ekosistem çöker.

Yıl: 2200. Geri kalan insanlar, yer altı sığınaklarında zorlukla hayatta kalmaktadır. Oyuncu (Kod Adı: Chronaut), eski Dünya liderlerinden birinin büyük torunu ve sığınaktaki son teknoloji uzmanıdır. APEX'in kullandığı zaman yolculuğu mekanizmasının (The Chronos gemisinin kendisi) kodunu çözerek, saldırının gerçekleştiği tam ana, 2100 yılındaki The Chronos gemisine sızmayı başarır.

Chronaut'un hedefi nettir: Geminin Ana Sunucu Odasına (The Core) ulaşmak, ana sıfırlama kartını (Override Key) almak, ve Komuta Odası'nın hemen ardındaki Ana Silah Bölmesine giderek silahı devre dışı bırakmak.

Fakat plot-twist buradadir, karti kullandigi anda silah ateslenir ve tarih tekerrur eder. Meger her seyi baslatan kisi bizmisiz.

### Senaryo

Oyuncu, bir uzay gemisinin icinde oyuna baslar. Arkasinda zaman makinesi, onunde ise Dunya'ya bakan bir cam vardir. Cama yaklasip dunyaya bakmasiyla ilk tetikleyici gerceklesir ve koridora bakan kapi acilir.
Oyuncu bu ilk koridordaki tum NPC'leri alt eder, tum NPC'ler alt edildiginde koridorun sonundaki kapi, ikinci koridora acilir.
Oyuncu ikinci koridorda bir dusmanla karsilasmaz. Yalnizca double-jump mekanigini kullanarak yuksekteki bir platforma ulasmali ve bir sonraki kapiyi acacak olan karti almalidir.
Oyuncu elde ettigi kart ile The Core'a giden kapiyi acar.
The Core'da karsisina birkac NPC cikar, dovusmesi gerekir. Oyuncu bu NPC'leri alt ettiginde eline bir kart gecer. Bu kart ihtiyaci olan son seydir.
Oyuncu silaha ulasmak icin sunucu odasindan gecmelidir, sunucu odasinin kapisi yarim aciktir, crouch mekanizmasiyla egilerek gecmesi gerekir. Bunun devaminda silaha ulasir ve karti kullanir.

## Features

- ThirdPersonController: We used Unity's StarterAssets for basic controls and locomotives. Added double-jump and crouch animations on top of this.
- Aiming: PlayerAimCamera, AimSensitivity, crosshair only active when the player aims, rotate the player's face when moving the aim, prevent the character from rotating while aiming
- Projectile: switch between Raycast Mode and Projectile Mode for the firing mode
- Animation: Separated the upper body from the lower body, so the lower body is moving individually while the character is aiming.
