1. Создаём проект веб апи асп нет кор
2. Устанавливаем EF CORE
   ![[Pasted image 20250505140707.png]]
3. По-умолчанию asp.net предлагает использовать MVC. Ну в целом для курсовой нормально. Тогда нам нужна папка Models, в которой сложим описание всех таблиц
   book
   
4. Создадим контекст для работы с базой данных, создав файл класса ApplicationContext.cs в папке Models. Пример класса приведён ниже.

   ```csharp
using Microsoft.EntityFrameworkCore;

namespace LibrariumAPI.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Books> Books { get; set; }
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=45.82.153.59;Database=librariumdb;User ID=aaaa;Password=123456; Trusted_Connection=True;");
        }
    }
}


```
5. Проверить подключение можно через утилиту sqlcmd 
      ```cmd
   sqlcmd -S 45.82.153.59 -U aaaa -P 123456 -d librariumdb
src
	
6. Создадим таблицы для базы данных, описывая  их с помощью классов С#. Для каждой таблицы создадим отдельный класс, который размещён в отдельном файле. Вот так будет выглядеть описание таблицы Books. 
```csharp
namespace LibrariumAPI.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishYear { get; set; }
    }
}

```

8. После создания таблиц опишем связи между ними. В данном случае связи будут один ко многим. Для примера опишем связь один ко многим к карточке выдачи и книге добавив к классу Book следующие строки. 
   ```csharp
public ICollection<BookPlace> BookPlaces { get; set; } = new List<BookPlace>();
public ICollection<IssueCards> Issues { get; set; }= new List<IssueCards>();
```

Класс Book должен выглядеть вот так:
```csharp
namespace LibrariumAPI.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishYear { get; set; }

        public ICollection<BookPlace> BookPlaces { get; set; } = new   List<BookPlace>();
        public ICollection<IssueCards> Issues { get; set; }= new List<IssueCards>();
    }
}

```

9.  После описания связей необходимо добавить поля,  которые будут в себе содержать внешние ключи.  Для примера добавим внешний ключ от таблицы Book в таблицу IssueCards добавив следующую строку:
   ```csharp
        [ForeignKey("Book")]
        public int BookId { get; set; }
```

После добавления всех внешних ключей класс IssueCards будет выглядеть так:
```csharp
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrariumAPI.Models
{
    public class IssueCards
    {

        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }

        [ForeignKey("Librarian")]
        public int LibrarianId { get; set; }

    }
}

```

Данное действие необходимо проделать со всеми связанными таблицами . 

10. Добавим в контекст созданные нами таблиц. Контекст примет вид:
```csharp
using LibrariumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Models
{
    public class LibraryContext : DbContext
    {

        public DbSet<Books> Books => Set<Books>();
        public DbSet<Readers> Readers => Set<Readers>();
        public DbSet<Librarians> Librarians => Set<Librarians>();
        public DbSet<IssueCards> IssueCards => Set<IssueCards>();
        public DbSet<BookPlace> BookPlaces => Set<BookPlace>();

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
```

10. Для успешного создания миграций необходимо создать класс LibraryContextFactory.cs со следущим содержанием
```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibraryApi.Models
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // чтобы найти appsettings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}

```
10. После этого необходимо добавить в appsettgins.json строку для подключения, с игнорированием сервера сертефиката, списка разрешённых хостов и авторизацией через логин и пароль
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=45.82.153.59;Database=librariumdb;User ID=aaaa; Password=123456; Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=false"
  },
```
10. Установим пакет  Microsoft.EntityFrameworkCore.Tools для создания миграций
11. Создаём миграцию ```dotnet ef migrations add InitialCreate```

12. Запишем миграцию в базу данных, таким образом создав в ней необходимые таблицы ```
```
dotnet ef database update
```

13. Проверяем созданные таблицы в базе данных:

	
    
14. 