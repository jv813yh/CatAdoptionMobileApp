using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.EntityFramework.DbContexts
{
    public class CatAdoptionDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<UserAdoption> UserAdoptions { get; set; }
        public DbSet<UserFavorites> UserFavorites { get; set; }

        public CatAdoptionDbContext(DbContextOptions<CatAdoptionDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Define the composite key for UserAdoptions
        //    //modelBuilder.Entity<UserFavorites>()
        //    //    .HasKey(uf => new { uf.UserId, uf.CatId });

        //    // Initial data seeding for cats
        //    //modelBuilder.Entity<Cat>()
        //    //    .HasData(InitialCatsData());

        //    //// Define the maximum length for user properties Salt and PasswordHash
        //    //modelBuilder.Entity<User>().Property(u => 
        //    //    u.Salt).HasMaxLength(100);
        //    //modelBuilder.Entity<User>().Property(u =>
        //    //    u.PasswordHash).HasMaxLength(100);
        //}

        public void SeedCats()
        {
            if (!Cats.Any())
            {
                // Initial data seeding for cats
                Cats.AddRange(InitialCatsData());
                SaveChanges();
            }
        }

        /// <summary>
        /// Static method to seed the database with initial data
        /// </summary>
        /// <returns></returns>
        private static List<Cat> InitialCatsData()
        {
            return new List<Cat>
            {
                new Cat
                {
                    Id = 1,
                    Name = "Fluffy",
                    Price = 250,
                    DateOfBirth = new DateTime(2022, 5, 1),
                    Breed = "Siamese",
                    Description = "Fluffy is a very friendly cat. She loves to play with kids and other cats. She is very active and loves to be around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2014/11/30/14/11/cat-551554_1280.jpg",
                },
                new Cat
                {
                    Id = 2,
                    Name = "Whiskers",
                    Price = 150,
                    Breed = "Persian",
                    DateOfBirth = new DateTime(2024, 5, 1),
                    Description = "Whiskers is a very calm and quiet cat. He loves to sleep and relax. He is very friendly and loves to be pet",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/01/18/07/38/cat-6946505_1280.jpg",
                },
                new Cat
                {
                    Id = 3,
                    Name = "Mittens",
                    Price = 100,
                    Breed = "Tabby",
                    DateOfBirth = new DateTime(2023, 9, 1),
                    Description = "Mittens is a very playful cat. She loves to run around and play with toys. She is very friendly and loves to be around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/10/27/19/09/cat-6748193_1280.jpg",
                },
                new Cat
                {
                    Id = 4,
                    Name = "Socks",
                    Price = 110,
                    Breed = "British Shorthair",
                    DateOfBirth = new DateTime(2024, 8, 15),
                    Description = "Socks is a quiet and dignified cat. She enjoys spending time with her family and has a soft, round face.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/01/18/07/36/cat-6946498_1280.jpg",
                },
                new Cat
                {
                    Id = 5,
                    Name = "Bella",
                    Price = 140,
                    Breed = "Bengal",
                    DateOfBirth = new DateTime(2023, 6, 1),
                    Description = "Bella is an energetic and curious cat. She loves to explore and is very intelligent, often figuring out how to open doors.",
                    Gender = Gender.Female,
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/06/20/16/54/cat-7274205_1280.jpg"
                },
                new Cat
                {
                    Id = 6,
                    Name = "Ginger",
                    Price = 95,
                    Breed = "Orange Tabby",
                    DateOfBirth = new DateTime(2023, 12, 1),
                    Description = "Ginger is a sweet, calm cat who loves to be pampered. She enjoys napping in the sun and curling up on soft blankets.",
                    Gender = Gender.Female,
                    ImageUrl = "https://cdn.pixabay.com/photo/2023/09/21/17/05/european-shorthair-8267220_1280.jpg",
                },
                new Cat
                {
                    Id = 7,
                    Name = "Smokey",
                    Price = 125,
                    Breed = "Russian Blue",
                    DateOfBirth = new DateTime(2023, 11, 19),
                    Description = "Smokey is a quiet, reserved cat. He is known for his beautiful silvery coat and enjoys being petted when he’s in the mood.",
                    Gender = Gender.Female,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/12/01/18/17/cat-6838844_1280.jpg",
                },
                new Cat
                {
                    Id = 8,
                    Name = "Oliver",
                    Price = 150,
                    Breed = "Ragdoll",
                    DateOfBirth = new DateTime(2023, 10, 1),
                    Description = "Oliver is a gentle giant with a calm and loving nature. He’s very affectionate and loves to be held like a baby.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2023/04/07/07/14/cat-7905702_1280.jpg",
                },
                new Cat
                {
                    Id = 9,
                    Name = "Luna",
                    Price = 105,
                    Breed = "Abyssinian",
                    DateOfBirth = new DateTime(2023, 7, 1),
                    Description = "Luna is a playful and active cat. She loves to climb and jump, and she is always full of energy.",
                    Gender = Gender.Female,
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/11/11/09/26/cat-7584624_1280.jpg",
                },
                new Cat
                {
                    Id = 10,
                    Name = "Milo",
                    Price = 95,
                    Breed = "Sphynx",
                    DateOfBirth = new DateTime(2024, 4, 4),
                    Description = "Milo is an outgoing and social cat. Despite having no fur, he is very warm and enjoys being around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/11/16/22/14/cat-1046544_1280.jpg",
                },
                new Cat
                {
                    Id = 11,
                    Name = "Toby",
                    Price = 115,
                    Breed = "Scottish Fold",
                    DateOfBirth = new DateTime(2023, 5, 3),
                    Description = "Toby is a calm and loving cat with distinctive folded ears. He enjoys lounging around and being spoiled with treats.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/08/07/12/27/cat-2603300_1280.jpg",
                },
                new Cat
                {
                    Id = 12,
                    Name = "Chloe",
                    Price = 130,
                    Breed = "Birman",
                    DateOfBirth = new DateTime(2023, 8, 15),
                    Description = "Chloe is an elegant cat with a luxurious coat. She loves being pampered and enjoys a good scratch behind the ears.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2020/11/22/17/28/cat-5767334_1280.jpg",
                },
                new Cat
                {
                    Id = 13,
                    Name = "Leo",
                    Price = 140,
                    Breed = "Burmese",
                    DateOfBirth = new DateTime(2024, 9, 1),
                    Description = "Leo is an affectionate and playful cat. He loves attention and follows his owner around the house.",
                    Gender = Gender.Male,
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/07/25/15/18/cat-7344042_1280.jpg",
                },
                new Cat
                {
                    Id = 14,
                    Name = "Penny",
                    Price = 120,
                    Breed = "Egyptian Mau",
                    DateOfBirth = new DateTime(2023, 11, 1),
                    Description = "Penny is a lively and graceful cat with a striking spotted coat. She’s very active and loves to play with interactive toys.",
                    Gender = Gender.Female,
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/06/08/17/02/cat-4260536_1280.jpg",
                },
            };

        }
    }
}
