# Reddit Core library

## 📁 Сангийн файлын бүтэц

```
Reddit.Core/
    │   README.md                                   - Энэ файл
    │   SocialMediaPlatform.Reddit.Core.csproj      - Сангийн үндсэн тохиргооны файл
    │
    ├───Domain  - Үндсэн модел классууд
    │   ├───Comments- Сэтгэгдлийн модел
    │   │       MainComment.cs                      - Post доорх хэрэглэгчийн бичсэн сэтгэгдэл
    │   │
    │   ├───DTOs    - Өгөгдлийг дамжуулах объектийн модел классууд
    │   │       TimelinePostDTO.cs                  - Нийтэд харагдах post-ийн DTO
    │   │
    │   ├───Enum    - Enum төрлүүд
    │   │       ReactionType.cs                     - Хариу үйлдлийн төрлүүд
    │   │       UserType.cs                         - Хэрэглэгчийн төрлүүд
    │   │
    │   ├───Posts   - Систем дээрх post-ийн модел
    │   │       TimelinePost.cs                     - Нийтэд харагдах post
    │   │
    │   ├───Reactions   - Хэрэглэгчийн дарах хариу үйлдлийн модел
    │   │       Downvote.cs                         - Таалагдаагүй/санал нийлэхгүй байна гэсэн хариу үйлдэл
    │   │       Upvote.cs                           - Таалагдсан/санал нийлж байна гэсэн хариу үйлдэл
    │   │
    │   └───Users   - Системийн хэрэглэгчийн модел
    │           NormalUser.cs                       - Систем дээрх энгийн хэрэглэгч
    │
    ├───Factories   - Бусад классын объект үйлдвэрлэгч классууд
    │       PostFactory.cs                          - Post үйлдвэрлэгч класс
    │       UserFactory.cs                          - Хэрэглэгч үйлдвэрлэгч класс
    │
    ├───Port    - Цөмтэй хандах хэсгийн портууд
    │   ├───Input   - Цөм рүү дотогш хандах хэсгийн порт
    │   │       ICommentServicePort.cs              - Сэтгэгдлийн үйлчилгээнд хандах порт
    │   │       IPostServicePort.cs                 - Post-ийн үйлчилгээнд хандах порт
    │   │       IReactionServicePort.cs             - Хариу үйлдлийн үйлчилгээнд хандах порт
    │   │       IUserServicePort.cs                 - Хэрэглэгчийн үйлчилгээнд хандах порт
    │   │
    │   └───Output  - Цөмөөс гадагш хандах хэсгийн порт
    │           ICommentRepoPort.cs                 - Сэтгэгдлийг хадгалах repository-той хандах порт
    │           IIdGeneratorPort.cs                 - ID дугаар үүсгэгчтэй хандах порт
    │           IPostRepoPort.cs                    - Post-ийг хадгалах repository-той хандах порт
    │           IReactionRepoPort.cs                - Хариу үйлдлийг хадгалах repository-той хандах порт
    │           ISequentialIdRepoPort.cs            - Дараалласан ID дугаарыг хадгалах repository-той хандах порт
    │           IUserRepoPort.cs                    - Хэрэглэгчийг хадгалах repository-той хандах порт
    │
    └───Service - Үндсэн бизнес үйл ажиллагааг хийх үйлчилгээний классууд
            CommentService.cs                       - Сэтгэгдлийн үйлчилгээний класс
            PostService.cs                          - Post-ийн үйлчилгээний класс
            ReactionService.cs                      - Хариу үйлдлийн үйлчилгээний класс
            UserService.cs                          - Хэрэглэгчийн үйлчилгээний класс
```
