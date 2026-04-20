# Reddit Infrastructure Test library

## 📁 Сангийн файлын бүтэц

```
Reddit.Infrastructure.Tests/
    │   README.md                                   - Энэ файл
    │   SocialMediaPlatform.Reddit.Infrastructure.Tests.csproj  - Сангийн үндсэн тохиргооны файл
    │
    ├───Connections                                 - Санах ойд ажиллах өгөгдлийн сангийн холболт үүсгэх класс
    │       TestConnection.cs
    │
    ├───Factories
    │       TestDataFactory.cs                      - Тестийн явцад хэрэглэгдэх өгөгдлүүдийг үүсгэж өгөх үйлдвэрлэгч класс
    │
    └───Persistence
        └───Sqlite  - Sqlite өгөгдлийн сантай ажиллах адаптер болгон дээр бичсэн тестүүд
                CommentRepoSqliteTests.cs
                PostRepoSqliteTests.cs
                ReactionRepoSqliteTests.cs
                SequentialIdRepoSqliteTests.cs
                UserRepoSqliteTests.cs
```
