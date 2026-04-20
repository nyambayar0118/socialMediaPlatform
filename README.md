# Виндоус Программчлал хичээлийн даалгаврын сан

Энэхүү repository нь Visual Studio ашиглаж C# хэл дээр нийтийн сүлжээний үндсэн код болон түүний нэг жишээ платформ болгож хэрэгжүүлсэн Reddit-ийн зөв бүтэцтэй кодуудыг агуулна.

## ⭐ Ерөнхий мэдээлэл

- **Оюутны нэр:** О.Нямбаяр
- **Оюутны ID:** 22B1NUM5284
- **Хичээл:** Виндоус Программчлал
- **Индекс:** ICSI315

---

## 📁 Төслийн ерөнхий бүтэц

```
SocialMediaPlatform/
    ├───.gitignore                                      - Git рүү оруулахгүй файлын жагсаал
    ├───README.md                                       - Энэ файл
    ├───SocialMediaPlatform.slnx                        - Solution доторх төслүүдийн жагсаалт
    ├───ProfilePictureProcessor                         - Нүүр зураг боловсруулдаг сан
    ├───ProfilePictureProcessor.Tests                   - Нүүр зураг боловсруулдаг сангийн тест
    ├───SocialMediaPlatform.Core                        - Бизнесийн үндсэн хэрэглэгдэх кодын сан
    ├───SocialMediaPlatform.Reddit.Core                 - Reddit платформын үндсэн кодын сан
    ├───SocialMediaPlatform.Reddit.ConsoleApp           - Reddit платформын console дээрх аппликейшн
    ├───SocialMediaPlatform.Reddit.Infrastructure       - Reddit платформын адаптерийн кодын сан
    ├───SocialMediaPlatform.Reddit.Infrastructure.Tests - Reddit платформын адаптерийн кодын сангийн тест
    └───SocialMediaPlatform.Reddit.WinForms             - Reddit платформын windows forms аппликейшн
```

## 📚 Даалгаврын гүйцэтгэлийн тайлан

### 💻 Лабораторийн ажил №1 (Core Design)

**Үндсэн зорилго:** Social Media Platform-ийн кодыг өөрчлөлт хийхэд, алдаа засахад, арчилгаа хийхэд хялбар байхаар загварчлах.

**Хэрэгжүүлэх зүйлс:**

- ✅ Нийтлэг классуудыг тодорхойлж, ямар нь Abstract Class байх, аль хэсэг нь Interface болох вэ гэдгийг зөв загварчлах.
- ✅ Классын шинж чанаруудыг зөв тодорхойлж, өгөгдлийн төрлийг зөв ашиглах.
- ✅ Ерөнхий кодны зохион байгуулалтыг зөв байлгах.
- ✅ Кодоо GitHub д байршуулах.
- ✅ Энэ хийсэн суурь сангаа DLL болгоод Console төрлийн прожектд Reference хийгээд платформоо үүсгэээд, хэрэглэгч, нэмээд пост оруулаад, түгээмэл үйлдлүүдийг түүн дээрээ хийгээд (like, comment, share, etc.) ажиллуулсан байх.

**Холбогдох сангийн жагсаалт:**

- **SocialMediaPlatform.Core** - Бизнесийн түвшинд буюу нэг байгууллагын үйлдвэрлэж буй адил төстэй программ хангамжийн бүтээгдэхүүнүүд бүгд ашиглаж болох үндсэн кодын сан.
- **SocialMediaPlatform.Reddit.Core** - Reddit нэртэй нийтийн сүлжээний аппликейшний 6-н талт буюу hexagonal архитектураар бичигдсэн цөм буюу "core" хэсгийн кодын сан.
- **SocialMediaPlatform.Reddit.Infrastructure** - Цөм хэсгийн port-ийн interface-үүдийг хэрэгжүүлсэн, өгөгдлийг уншиж бичих адаптеруудын хэрэгжүүлэлтийг агуулах кодын сан.

### 💻 Лабораторийн ажил №2 (Зурагтай ажиллах)

**Үндсэн зорилго:** Хэрэглэгчийн заасан зургийг аваад тодорхой шалгуур хангуулах, өөрчлөх сангийн кодыг бичих, тестлэх.

**Хэрэгжүүлэх зүйлс:**

- ✅ Зургийн өндөр өргөнийг тодорхой харьцаатай байгааг шалгах, харьцаа буруу байвал зөв болгох.
- ✅ Library хэлбэрээр хийх.
- ✅ Documentation бүрэн байх.
- ✅ MsTest ашиглан бүх хувилбараар зураг оноож шалгах Тестийн прожект хийх.
- ✅ Тестийн line coverage нь 100% байх.

**Холбогдох сангийн жагсаалт:**

- **ProfilePictureProcessor** - Хэрэглэгчийн оруулсан аватар буюу нүүр зургийг шалгуур хангаж байгаа эсэхийг шалгаж, хэрэв том бол засварлаж хадгалах кодын сан.
- **ProfilePictureProcessor.Tests** - Зурагтай ажиллах кодын сангийн тестийг агуулах кодын сан.

### 💻 Лабораторийн ажил №3 (Custom Control)

**Үндсэн зорилго:** Social networking платформ дээр түгээмэл ашиглагддаг Custom control үүсгэх.

**Хэрэгжүүлэх зүйлс:**

- ✅ Зурагт үзүүлсэн шиг эсвэл өөрсдөө сонгосон контролыг хэрэгжүүлэх.
- ✅ Graphics классын Draw... method уудыг ашигласан байх.
- ✅ Тохиргоо хийх 2 Properties-тэй байх.
- ✅ Custom Event үүсгэдэг байх. Контрол дээр байдаг стандард event биш яг тухайн контролын Event байх.
- ✅ Үүнийгээ ашиглаад Social networking platform дотроо ашигласан байх.

**Холбогдох сангийн жагсаалт:**

- **SocialMediaPlatform.Reddit.WinForms** - Reddit платформын Winforms ашигласан Desktop аппликейшний төсөл. Дотор нь custom control-ийн санг мөн агуулсан болно.

### 💻 Лабораторийн ажил №4 (Sqlite Database)

**Үндсэн зорилго:** Social Networking зориулсан өгөгдлийн санг үүсгэж Post, Comment үүсгэх, Like үйлдлүүдийг өгөгдлийн сантай холбоно.

**Хэрэгжүүлэх зүйлс:**

- ✅ Өгөгдлийн сан нь SQLite байна.
- ✅ Зөв бүтэцтэй, давхаргуудад хуваасан байдлаар repository классуудаа хэрэгжүүлнэ.
- ❌ Хийсэн Repo классуудаа Test бичиж Coverage ni 100% байлгана. **(ОДООГИЙН ГҮЙЦЭТГЭЛ: 97.8%)**

**Холбогдох сангийн жагсаалт:**

- **SocialMediaPlatform.Reddit.Infrastructure** - Цөм хэсгийн port-ийн interface-үүдийг хэрэгжүүлсэн, SQLite өгөгдлийн санд өгөгдлийг уншиж бичих адаптеруудын хэрэгжүүлэлтийг агуулах кодын сан.
- **SocialMediaPlatform.Reddit.Infrastructure.Tests** - Infrastructure доторх SQLite Repository adapter-уудын тестийг агуулах кодын сан.

### Mermaid.Live дээрх класс диаграм

https://mermaid.live/edit#pako:eNrlW-lu4zgSfhVCwAA2IgedqzsRGgHcttNjIIfhYwa7yB9GYhxiZUpDSen2dCeYR9gfsy84TzIkdfGybPex284GAeyIVWRVseorskr55PhRgBzP8UOYJH0M5xQubsktAeznp5_AX__598v4rTTq3VyNutf_2O_djAcAtJIHSFEA-peX4K8__gQkAnEI0_uILjpJjHx8j32QLmOUtF-iTWqd_vwj_wXvupNB_eeu_JaqCDcGA5LidAk-5c_4z9u38C5JKfTT8_P66V6PIpiioJt6oM--TPEC5aNPVtv0b666w2uws3aZJYhuYJVh4AnSYSA95A8IXCAPMA5M5tLQYAFxaHk-Ymt-iGigDj2pMo2iJN1QJk6qyNTN0oeIWsX9BSf4DofMDTxQf5-ySLZK0YsWC0Q2FaSg3lSWXG6L_L2IpGyeRvuMEZMAR2QD0aaQzpFYKcMkNQa48l41X_1srRKVREVcvf3c6QgC8ylX0Xxa2MscKIWxR9v0Jtl1JOJWYnrIm_fNwms9eNURZpPhW4bTVgIV7mCT6btG1pdA_rAPfh13R6PBeAJ2y-2GAfjEkOIDhXGM6Pk520QYZigHB66t6iFbkFebtIbDYs7B9exqAnY5olXvFxZAJFsw9UfZXYh9F4wofmSepdrMhF2FlW2AWyUhhXEY5ICps_Atdk1Gmwtfj2ZTMLoZT390Fy5Ev0Is4IMEZAkCEQmX4A6yb-IUDiAJ8sSQROz7sjqtAz-iCPiwNgBexCESZkkf0AJ8wGzSLAX-AyRzBgkgwXMC04yiZF_d4SE37QTRR-yjUUS1QwHzbUTvoY-U1DtGc5ywgVam4bgLkAzfLoi1Q1HbK5OENN1lxES0zLUpM1O0xcYeIyyD4nuUcmoxbw2n1jkGAbbRumCNftbJ-ihEKbIvXYuoJYkhd-0tdyHHds7YglrScAELolAS21czRNsr86RmBTFbrOSZL5orN4JlNus-2enMWRnpBUIB3-xL5oDPBcWz3aQFTmxp1W4QFIyGHUwrm6aos71hjXJav0wo0gHAapZiNDFNI7Svl1phgBKFt45uxtZKleM1c4LGU3UeKrJpaEkjOCr7aFrOCP0Gq1mtV7L1ooxsuwCbsI_FM0iXz6Xjc85naZGfYSJYmUd-tfx3URQ2ncxuZtMdyGuaAw6D94ggCtOIbup81-hjmhul1bYcgflwHgWttuUgzIermGq1jRP2kyX3jVEcbSrdBD4igeq5aKbfXWASvFuyta1ZxyAs70FG8jMZZnHA0L5x8Rxmts06W-vP0Si3fYP-VjTXCLthqED5s6lu01qFug35ZUVG2FrjArErd2rQewW4KzUBicOe-WR4fzY1XptAViWCrRUvQbwGsZXboCOgnixNRgHM75Y5LH4XgB58ZIZM8jDrkqBYSc9UW69swerC0BP0W8a2A8NwGGxjbJauLqFANVRdfzzlMtQ2ylx8g9YwueCxvqPaPURJMpPBZDK82Z2Sr2r9CUoSo3SYMMdg_hkR2d4dTJIUEp9ZpmCSxvyMUmbQWQW0xhl0WHBz8DL4pdtMxW76ftOtpVcL0LLeMIYJY5-jYEhapje-4GbWeNDvD6dlL4vdg7NFfSPm_awAxYiwqzRzArnx9f_SzMobNjwpEJ8yTAYCFzD6X9SV1dC84Hul9z72pvK9cstGgZhR62NoJNfMLWAod4E0gm6wwETvEu31IMlTWpeI9CxHWEnwDgq-pCEXzOLHiG2BfeV-9IFYh4WNRLOgMlk-UKpajylHCqGFGKu11kcqdfOBqtWSNziEuLaRUtYX27XgRfEQE2TrHHyZi5YzCftp07_QQjH3K71om7uimzveigKxXucVbujWAWI_qFx0e9Ob8XCwCzYz7XTBdI_oUkUdUcxrpeIMVxrTBXjzYuTKeunKNlmDHLix7LW2IKiB16rT5viXYW-wI35vbmNRVpPN16Hs0O-pdQX52FnXQzyjOiLR3ecb48neonVPi8VblgW51zSs4wLb9O2VXtKgplw--Bo1JWfU2p2amvKCm6spTd9uatE2aKpVDrZXdk9do2Wfd41O7cZ3JhrE1wsAerW35m6t4NCXNtpGbz_v78thUZDpfQ1BJj0syCy1ekGpPi-IbXVtQa0NlLLKwdrpnJtRpRNY4tcgse60HC-cynBsncASQgaJdSXNZQWh1UlthNYZdTcSlKbjWIC82--Opj_6KwMriwZ1xaYyii2EbKUduRpjm6fVwGuP5dLxLnDIz0Wde_Y5gulDmVvNNxk2p5bcYzMGefM349AVbeIqglnvEYhAtlozZ7BvmHDW1VtkL8wZS5UiSxCnEsvbIwGcSiTvigpvKp22Hxq4qbT6Vqx49eFi3J1Mx7PedDb-IV9r1dMuSWkUhuoFvJPVUOsZeUaii2ug9IxEIxf0FAT0bMlGCXcFBj1rupHok7wAaKsEftVrEmbBcNN3JDYtNUrvD-zU2wINrwCs6-D_WP36r-i2p-pI3iD4Jr31_34vXa_KxTEDhns8V0DBr7DCk3BD3vWSje0X_5DTTVvfp5I_bxGbs3WYGkExXSxPZNUl_7RrM6IR_ycTlgT3riAmZRjWZBII8iSmoIg2ZsdCncgKhDrRKgjU6VZiX71LnE63Yak1H6soHdeZUxw4Xkoz5DoLRBkIsj8dsc-3Dn9xDd06HvsaQPqvW-eWPDGeGJJ_RtGiZKNRNn9wvHsYJuyvTLSJi__kqUgQCRAVvup4hwenZ2ISx_vkfHS8zuHh2f7R67PjN6-PDk6PTs6OTl1nyZ4fHe4fvz49Pj549ers5PDg-PT4yXV-Fysf7J8cnb06OXp99ObV8cGbkzeugxj8RfSq-Hci_vH0NxWeQcI
