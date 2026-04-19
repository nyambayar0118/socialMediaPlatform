# Social Media Platform Core library

## 📁 Сангийн файлын бүтэц

```
SocialMediaPlatform/
    │   README.md                           - Энэ файл
    │   SocialMediaPlatform.Core.csproj     - Төслийн үндсэн тохиргооны файл
    │
    ├───Domain  - Үндсэн модел классууд
    │   ├───DTO     - Өгөгдлийг дамжуулах объектийн модел классууд
    │   │       CommentDTO.cs               - Сэтгэгдлийн DTO
    │   │       PostDTO.cs                  - Post-ийн DTO
    │   │       UserDTO.cs                  - Хэрэглэгчийн DTO
    │   │
    │   ├───Entity  - Систем дээрх зүйлсийн модел классууд
    │   │       Comment.cs                  - Хэрэглэгчийн бичсэн сэтгэгдэл
    │   │       Entity.cs                   - Систем дээрх зүйлсийн үндсэн загвар
    │   │       Post.cs                     - Хэрэглэгчийн бичсэн post
    │   │       Reaction.cs                 - Хэрэглэгчийн үзүүлэх хариу үйлдэл
    │   │       User.cs                     - Хэрэглэгч
    │   │
    │   ├───Enum    - Enum төрлүүд
    │   │       IdEntityType.cs             - ID дугаартай зүйлсийн төрөл
    │   │       ReactionTargetType.cs       - Хариу үйлдэл үзүүлж болох зүйлсийн төрөл
    │   │       VisibilityType.cs           - Харагдацын төрөл
    │   │
    │   └───IdWrapper - ID дугаарын DTO классууд
    │           CommentId.cs                - Сэтгэгдлийн ID дугаарын wrapper
    │           PostId.cs                   - Post-ийн ID дугаарын wrapper
    │           UserId.cs                   - Хэрэглэгчийн ID дугаарын wrapper
    │
    └───Infrastructure  - Аппликейшний дэд бүтцэд хамаарах классууд
            Session.cs                      - Одоогийн системийг ашиглаж буй хэрэглэгчийн мэдээлэл
```
