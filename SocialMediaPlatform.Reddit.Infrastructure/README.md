# Reddit Infrastructure library

## 📁 Сангийн файлын бүтэц

```
Reddit.Core/
    │   AppConfig.cs                                - Аппликейшний үндсэн хэсгүүдийг initialize хийж өгөх класс
    │   README.md                                   - Энэ файл
    │   SocialMediaPlatform.Reddit.Infrastructure.csproj    - Сангийн үндсэн тохиргооны файл
    │
    ├───IdGenerator
    │       SequentialIdGenerator.cs                - Дараалсан ID дугаар үүсгэгч адаптер
    │
    └───Persistence
        ├───File    - Текст файл руу I/O үйлдэл хийх адаптерууд
        │       CommentRepoFile.cs
        │       PostRepoFile.cs
        │       ReactionRepoFile.cs
        │       SequentialIdRepoFile.cs
        │       UserRepoFile.cs
        │
        └───Sqlite  - Sqlite өгөгдлийн сантай ажиллах хэсэг
            │   DatabaseConnection.cs   - Өгөгдлийнс сангийн холболтыг үүсгэх класс
            │
            ├───Mappers - Resultset-ээс домайн классын объект болгох туслагч классууд
            │       CommentMapper.cs
            │       PostMapper.cs
            │       ReactionMapper.cs
            │       UserMapper.cs
            │
            └───Repositories    - Sqlite өгөгдлийн сан руу I/O үйлдэл хийх адаптерууд
                    CommentRepoSqlite.cs
                    PostRepoSqlite.cs
                    ReactionRepoSqlite.cs
                    SequentialIdRepoSqlite.cs
                    UserRepoSqlite.cs
```
