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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the composite key for UserAdoptions
            //modelBuilder.Entity<UserFavorites>()
            //    .HasKey(uf => new { uf.UserId, uf.CatId });

            // Initial data seeding for cats
            modelBuilder.Entity<Cat>()
                .HasData(InitialCatsData());
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
                    Breed = "Siamese",
                    Description = "Fluffy is a very friendly cat. She loves to play with kids and other cats. She is very active and loves to be around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/white-and-grey-kitten-on-brown-and-black-leopard-print-textile-45201/",
                },
                new Cat
                {
                    Id = 2,
                    Name = "Whiskers",
                    Price = 150,
                    Breed = "Persian",
                    Description = "Whiskers is a very calm and quiet cat. He loves to sleep and relax. He is very friendly and loves to be pet",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/grey-and-white-short-fur-cat-104827/",
                },
                new Cat
                {
                    Id = 3,
                    Name = "Mittens",
                    Price = 100,
                    Breed = "Tabby",
                    Description = "Mittens is a very playful cat. She loves to run around and play with toys. She is very friendly and loves to be around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/selective-focus-photography-of-orange-tabby-cat-1170986/",
                },
                new Cat
                {
                    Id = 4,
                    Name = "Socks",
                    Price = 110,
                    Breed = "British Shorthair",
                    Description = "Socks is a quiet and dignified cat. She enjoys spending time with her family and has a soft, round face.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/short-coated-gray-cat-20787/",
                },
                new Cat
                {
                    Id = 5,
                    Name = "Bella",
                    Price = 140,
                    Breed = "Bengal",
                    Description = "Bella is an energetic and curious cat. She loves to explore and is very intelligent, often figuring out how to open doors.",
                    Gender = Gender.Female,
                    ImageUrl = "https://www.pexels.com/photo/photo-of-orange-tabby-cat-with-red-handkerchief-1741205/",
                },
                new Cat
                {
                    Id = 6,
                    Name = "Ginger",
                    Price = 95,
                    Breed = "Orange Tabby",
                    Description = "Ginger is a sweet, calm cat who loves to be pampered. She enjoys napping in the sun and curling up on soft blankets.",
                    Gender = Gender.Female,
                    ImageUrl = "https://www.pexels.com/photo/cute-gray-kitten-standing-on-a-wooden-flooring-774731/",
                },
                new Cat
                {
                    Id = 7,
                    Name = "Smokey",
                    Price = 125,
                    Breed = "Russian Blue",
                    Description = "Smokey is a quiet, reserved cat. He is known for his beautiful silvery coat and enjoys being petted when he’s in the mood.",
                    Gender = Gender.Female,
                    ImageUrl = "https://www.pexels.com/photo/low-angle-shot-of-a-tabby-cat-208984/",
                },
                new Cat
                {
                    Id = 8,
                    Name = "Oliver",
                    Price = 150,
                    Breed = "Ragdoll",
                    Description = "Oliver is a gentle giant with a calm and loving nature. He’s very affectionate and loves to be held like a baby.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/grey-fur-kitten-127028/",
                },
                new Cat
                {
                    Id = 9,
                    Name = "Luna",
                    Price = 105,
                    Breed = "Abyssinian",
                    Description = "Luna is a playful and active cat. She loves to climb and jump, and she is always full of energy.",
                    Gender = Gender.Female,
                    ImageUrl = "https://www.pexels.com/photo/photo-of-british-shorthair-cat-sitting-on-grass-field-1521306/",
                },
                new Cat
                {
                    Id = 10,
                    Name = "Milo",
                    Price = 95,
                    Breed = "Sphynx",
                    Description = "Milo is an outgoing and social cat. Despite having no fur, he is very warm and enjoys being around people.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/closeup-up-photography-of-tri-color-kitten-691583/",
                },
                new Cat
                {
                    Id = 11,
                    Name = "Toby",
                    Price = 115,
                    Breed = "Scottish Fold",
                    Description = "Toby is a calm and loving cat with distinctive folded ears. He enjoys lounging around and being spoiled with treats.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/close-up-photo-of-white-cat-1485968/",
                },
                new Cat
                {
                    Id = 12,
                    Name = "Chloe",
                    Price = 130,
                    Breed = "Birman",
                    Description = "Chloe is an elegant cat with a luxurious coat. She loves being pampered and enjoys a good scratch behind the ears.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/white-and-brown-cat-1687831/",
                },
                new Cat
                {
                    Id = 13,
                    Name = "Leo",
                    Price = 140,
                    Breed = "Burmese",
                    Description = "Leo is an affectionate and playful cat. He loves attention and follows his owner around the house.",
                    Gender = Gender.Male,
                    ImageUrl = "https://www.pexels.com/photo/photo-of-cat-climbing-on-tree-1653357/",
                },
                new Cat
                {
                    Id = 14,
                    Name = "Penny",
                    Price = 120,
                    Breed = "Egyptian Mau",
                    Description = "Penny is a lively and graceful cat with a striking spotted coat. She’s very active and loves to play with interactive toys.",
                    Gender = Gender.Female,
                    ImageUrl = "https://www.pexels.com/photo/white-and-black-kitten-lying-on-tiles-479009/",
                },
            };

        }
    }
}
