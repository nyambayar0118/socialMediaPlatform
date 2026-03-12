# Виндоус Программчлал - Даалгавар №1

Энэхүү repository нь Visual Studio ашиглаж C# хэл дээр нийтийн сүлжээний үндсэн код болон түүний нэг жишээ платформ болгож хэрэгжүүлсэн Reddit-ийн зөв бүтэцтэй кодуудыг агуулна.

## ⭐ Ерөнхий мэдээлэл

- **Core:** SocialMediaPlatform.Core
- **Platform Core:** SocialMediaPlatform.Reddit.Core
- **Platform ConsoleApp:** SocialMediaPlatform.Reddit.ConsoleApp
- **Student Name:** О.Нямбаяр
- **Student ID:** 22B1NUM5284
- **Course:** Виндоус Программчлал

---

## 📁 Project Structure

```
C:.
│   .gitignore
│   README.md       # Энэ файл
│   SocialMediaPlatform.slnx    # Solution-ийн тохиргооны файл
│
├───SocialMediaPlatform.Core    # Платформ хоорондын хуваалцах library
│   │   SocialMediaPlatform.Core.csproj
│   │
│   ├───Domain      # Domain классууд
│   │   ├───DTO     # Data Transfer Object record төрлүүд
│   │   │       CommentDTO.cs
│   │   │       PostDTO.cs
│   │   │       UserDTO.cs
│   │   │
│   │   ├───Entity  # Abstract domain entity классууд
│   │   │       Comment.cs
│   │   │       Entity.cs
│   │   │       Post.cs
│   │   │       Reaction.cs
│   │   │       User.cs
│   │   │
│   │   ├───Enum    # Enum төрлүүд
│   │   │       IdEntityType.cs
│   │   │       ReactionTargetType.cs
│   │   │       VisibilityType.cs
│   │   │
│   │   └───IdWrapper   # ID wrapper классууд
│   │           CommentId.cs
│   │           PostId.cs
│   │           UserId.cs
│   │
│   └───Infrastructure  # Дэд бүтцийн классууд
│           Session.cs  # Нэвтэрсэн хэрэглэгчийн singleton session
│
├───SocialMediaPlatform.Reddit.ConsoleApp   # Console апп — програмын оролт
│   │   Program.cs      # Програмын эхлэл, console UI
│   │   SocialMediaPlatform.Reddit.ConsoleApp.csproj
│   │
│   └───bin
│       └───Debug
│           └───net10.0
│               └───data    # Програм ажиллах үед үүсгэгдэх өгөгдлийн файлууд
│                       comments.txt
│                       ids.txt
│                       posts.txt
│                       reactions.txt
│                       users.txt
│
└───SocialMediaPlatform.Reddit.Core     # Reddit платформын үндсэн код
    │   SocialMediaPlatform.Reddit.Core.csproj
    │
    ├───Adapter     # Port-уудыг хэрэгжүүлсэн адаптерууд
    │       CommentRepoFile.cs      # Сэтгэгдлийн файл репозитори
    │       PostRepoFile.cs         # Постын файл репозитори
    │       ReactionRepoFile.cs     # Reaction-ий файл репозитори
    │       SequentialIdGenerator.cs    # Дараалсан ID үүсгэгч
    │       SequentialIdRepoFile.cs     # ID-ийн файл репозитори
    │       UserRepoFile.cs         # Хэрэглэгчийн файл репозитори
    │
    ├───Domain      # Reddit платформын тодорхой domain классууд
    │   ├───Comments
    │   │       MainComment.cs      # Постод бичигдэх үндсэн сэтгэгдэл
    │   │
    │   ├───DTOs
    │   │       TimelinePostDTO.cs  # TimelinePost-ийн өгөгдлийн дамжуулах объект
    │   │
    │   ├───Enum    # Reddit платформын enum төрлүүд
    │   │       ReactionType.cs     # Upvote, Downvote
    │   │       UserType.cs         # Normal, Admin
    │   │
    │   ├───Posts
    │   │       TimelinePost.cs     # Гарчиг, агуулгатай үндсэн пост
    │   │
    │   ├───Reactions
    │   │       Downvote.cs         # Сөрөг reaction
    │   │       Upvote.cs           # Эерэг reaction
    │   │
    │   └───Users
    │           NormalUser.cs       # Энгийн хэрэглэгч
    │
    ├───Factory     # Domain объект үүсгэх factory классууд
    │       PostFactory.cs          # TimelinePost үүсгэх
    │       UserFactory.cs          # NormalUser үүсгэх
    │
    ├───Infrastructure  # Дэд бүтцийн классууд
    │       AppConfig.cs    # Dependency-үүдийг угсрах, тохиргооны файл уншдаг
    │       Controller.cs   # Service-үүд рүү хандах үндсэн controller
    │
    ├───Port        # Hexagonal архитектурын портууд
    │   ├───Input   # Core луу хандах service-ийн портууд
    │   │       ICommentServicePort.cs
    │   │       IPostServicePort.cs
    │   │       IReactionServicePort.cs
    │   │       IUserServicePort.cs
    │   │
    │   └───Output  # Core-оос гадагш хандах репозиторийн портууд
    │           ICommentRepoPort.cs
    │           IIdGeneratorPort.cs
    │           IPostRepoPort.cs
    │           IReactionRepoPort.cs
    │           ISequentialIdRepoPort.cs
    │           IUserRepoPort.cs
    │
    └───Service     # Input Port-уудыг хэрэгжүүлсэн service классууд
            CommentService.cs
            PostService.cs
            ReactionService.cs
            UserService.cs
```

## Mermaid.live ашиглаж зурсан гол класс диаграм

https://mermaid.live/edit#pako:eNrlW-lu4zgSfhVCwAA2IgedqzsRGgHcttNjIIfhYwa7yB9GYhxiZUpDSen2dCeYR9gfsy84TzIkdfGybPex284GAeyIVWRVseorskr55PhRgBzP8UOYJH0M5xQubsktAeznp5_AX__598v4rTTq3VyNutf_2O_djAcAtJIHSFEA-peX4K8__gQkAnEI0_uILjpJjHx8j32QLmOUtF-iTWqd_vwj_wXvupNB_eeu_JaqCDcGA5LidAk-5c_4z9u38C5JKfTT8_P66V6PIpiioJt6oM--TPEC5aNPVtv0b666w2uws3aZJYhuYJVh4AnSYSA95A8IXCAPMA5M5tLQYAFxaHk-Ymt-iGigDj2pMo2iJN1QJk6qyNTN0oeIWsX9BSf4DofMDTxQf5-ySLZK0YsWC0Q2FaSg3lSWXG6L_L2IpGyeRvuMEZMAR2QD0aaQzpFYKcMkNQa48l41X_1srRKVREVcvf3c6QgC8ylX0Xxa2MscKIWxR9v0Jtl1JOJWYnrIm_fNwms9eNURZpPhW4bTVgIV7mCT6btG1pdA_rAPfh13R6PBeAJ2y-2GAfjEkOIDhXGM6Pk520QYZigHB66t6iFbkFebtIbDYs7B9exqAnY5olXvFxZAJFsw9UfZXYh9F4wofmSepdrMhF2FlW2AWyUhhXEY5ICps_Atdk1Gmwtfj2ZTMLoZT390Fy5Ev0Is4IMEZAkCEQmX4A6yb-IUDiAJ8sSQROz7sjqtAz-iCPiwNgBexCESZkkf0AJ8wGzSLAX-AyRzBgkgwXMC04yiZF_d4SE37QTRR-yjUUS1QwHzbUTvoY-U1DtGc5ywgVam4bgLkAzfLoi1Q1HbK5OENN1lxES0zLUpM1O0xcYeIyyD4nuUcmoxbw2n1jkGAbbRumCNftbJ-ihEKbIvXYuoJYkhd-0tdyHHds7YglrScAELolAS21czRNsr86RmBTFbrOSZL5orN4JlNus-2enMWRnpBUIB3-xL5oDPBcWz3aQFTmxp1W4QFIyGHUwrm6aos71hjXJav0wo0gHAapZiNDFNI7Svl1phgBKFt45uxtZKleM1c4LGU3UeKrJpaEkjOCr7aFrOCP0Gq1mtV7L1ooxsuwCbsI_FM0iXz6Xjc85naZGfYSJYmUd-tfx3URQ2ncxuZtMdyGuaAw6D94ggCtOIbup81-hjmhul1bYcgflwHgWttuUgzIermGq1jRP2kyX3jVEcbSrdBD4igeq5aKbfXWASvFuyta1ZxyAs70FG8jMZZnHA0L5x8Rxmts06W-vP0Si3fYP-VjTXCLthqED5s6lu01qFug35ZUVG2FrjArErd2rQewW4KzUBicOe-WR4fzY1XptAViWCrRUvQbwGsZXboCOgnixNRgHM75Y5LH4XgB58ZIZM8jDrkqBYSc9UW69swerC0BP0W8a2A8NwGGxjbJauLqFANVRdfzzlMtQ2ylx8g9YwueCxvqPaPURJMpPBZDK82Z2Sr2r9CUoSo3SYMMdg_hkR2d4dTJIUEp9ZpmCSxvyMUmbQWQW0xhl0WHBz8DL4pdtMxW76ftOtpVcL0LLeMIYJY5-jYEhapje-4GbWeNDvD6dlL4vdg7NFfSPm_awAxYiwqzRzArnx9f_SzMobNjwpEJ8yTAYCFzD6X9SV1dC84Hul9z72pvK9cstGgZhR62NoJNfMLWAod4E0gm6wwETvEu31IMlTWpeI9CxHWEnwDgq-pCEXzOLHiG2BfeV-9IFYh4WNRLOgMlk-UKpajylHCqGFGKu11kcqdfOBqtWSNziEuLaRUtYX27XgRfEQE2TrHHyZi5YzCftp07_QQjH3K71om7uimzveigKxXucVbujWAWI_qFx0e9Ob8XCwCzYz7XTBdI_oUkUdUcxrpeIMVxrTBXjzYuTKeunKNlmDHLix7LW2IKiB16rT5viXYW-wI35vbmNRVpPN16Hs0O-pdQX52FnXQzyjOiLR3ecb48neonVPi8VblgW51zSs4wLb9O2VXtKgplw--Bo1JWfU2p2amvKCm6spTd9uatE2aKpVDrZXdk9do2Wfd41O7cZ3JhrE1wsAerW35m6t4NCXNtpGbz_v78thUZDpfQ1BJj0syCy1ekGpPi-IbXVtQa0NlLLKwdrpnJtRpRNY4tcgse60HC-cynBsncASQgaJdSXNZQWh1UlthNYZdTcSlKbjWIC82--Opj_6KwMriwZ1xaYyii2EbKUduRpjm6fVwGuP5dLxLnDIz0Wde_Y5gulDmVvNNxk2p5bcYzMGefM349AVbeIqglnvEYhAtlozZ7BvmHDW1VtkL8wZS5UiSxCnEsvbIwGcSiTvigpvKp22Hxq4qbT6Vqx49eFi3J1Mx7PedDb-IV9r1dMuSWkUhuoFvJPVUOsZeUaii2ug9IxEIxf0FAT0bMlGCXcFBj1rupHok7wAaKsEftVrEmbBcNN3JDYtNUrvD-zU2wINrwCs6-D_WP36r-i2p-pI3iD4Jr31_34vXa_KxTEDhns8V0DBr7DCk3BD3vWSje0X_5DTTVvfp5I_bxGbs3WYGkExXSxPZNUl_7RrM6IR_ycTlgT3riAmZRjWZBII8iSmoIg2ZsdCncgKhDrRKgjU6VZiX71LnE63Yak1H6soHdeZUxw4Xkoz5DoLRBkIsj8dsc-3Dn9xDd06HvsaQPqvW-eWPDGeGJJ_RtGiZKNRNn9wvHsYJuyvTLSJi__kqUgQCRAVvup4hwenZ2ISx_vkfHS8zuHh2f7R67PjN6-PDk6PTs6OTl1nyZ4fHe4fvz49Pj549ers5PDg-PT4yXV-Fysf7J8cnb06OXp99ObV8cGbkzeugxj8RfSq-Hci_vH0NxWeQcI