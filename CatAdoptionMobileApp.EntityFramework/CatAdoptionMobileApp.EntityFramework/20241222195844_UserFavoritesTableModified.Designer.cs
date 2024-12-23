﻿// <auto-generated />
using System;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatAdoptionMobileApp.EntityFramework.CatAdoptionMobileApp.EntityFramework
{
    [DbContext(typeof(CatAdoptionDbContext))]
    [Migration("20241222195844_UserFavoritesTableModified")]
    partial class UserFavoritesTableModified
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.Cat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AdoptionStatus")
                        .HasColumnType("int");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cats");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AdoptionStatus = 0,
                            Breed = "Siamese",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Fluffy is a very friendly cat. She loves to play with kids and other cats. She is very active and loves to be around people.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/white-and-grey-kitten-on-brown-and-black-leopard-print-textile-45201/",
                            IsActive = false,
                            Name = "Fluffy",
                            Price = 250.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 2L,
                            AdoptionStatus = 0,
                            Breed = "Persian",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Whiskers is a very calm and quiet cat. He loves to sleep and relax. He is very friendly and loves to be pet",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/grey-and-white-short-fur-cat-104827/",
                            IsActive = false,
                            Name = "Whiskers",
                            Price = 150.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 3L,
                            AdoptionStatus = 0,
                            Breed = "Tabby",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Mittens is a very playful cat. She loves to run around and play with toys. She is very friendly and loves to be around people.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/selective-focus-photography-of-orange-tabby-cat-1170986/",
                            IsActive = false,
                            Name = "Mittens",
                            Price = 100.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 4L,
                            AdoptionStatus = 0,
                            Breed = "British Shorthair",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Socks is a quiet and dignified cat. She enjoys spending time with her family and has a soft, round face.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/short-coated-gray-cat-20787/",
                            IsActive = false,
                            Name = "Socks",
                            Price = 110.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 5L,
                            AdoptionStatus = 0,
                            Breed = "Bengal",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Bella is an energetic and curious cat. She loves to explore and is very intelligent, often figuring out how to open doors.",
                            Gender = 1,
                            ImageUrl = "https://www.pexels.com/photo/photo-of-orange-tabby-cat-with-red-handkerchief-1741205/",
                            IsActive = false,
                            Name = "Bella",
                            Price = 140.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 6L,
                            AdoptionStatus = 0,
                            Breed = "Orange Tabby",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Ginger is a sweet, calm cat who loves to be pampered. She enjoys napping in the sun and curling up on soft blankets.",
                            Gender = 1,
                            ImageUrl = "https://www.pexels.com/photo/cute-gray-kitten-standing-on-a-wooden-flooring-774731/",
                            IsActive = false,
                            Name = "Ginger",
                            Price = 95.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 7L,
                            AdoptionStatus = 0,
                            Breed = "Russian Blue",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Smokey is a quiet, reserved cat. He is known for his beautiful silvery coat and enjoys being petted when he’s in the mood.",
                            Gender = 1,
                            ImageUrl = "https://www.pexels.com/photo/low-angle-shot-of-a-tabby-cat-208984/",
                            IsActive = false,
                            Name = "Smokey",
                            Price = 125.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 8L,
                            AdoptionStatus = 0,
                            Breed = "Ragdoll",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Oliver is a gentle giant with a calm and loving nature. He’s very affectionate and loves to be held like a baby.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/grey-fur-kitten-127028/",
                            IsActive = false,
                            Name = "Oliver",
                            Price = 150.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 9L,
                            AdoptionStatus = 0,
                            Breed = "Abyssinian",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Luna is a playful and active cat. She loves to climb and jump, and she is always full of energy.",
                            Gender = 1,
                            ImageUrl = "https://www.pexels.com/photo/photo-of-british-shorthair-cat-sitting-on-grass-field-1521306/",
                            IsActive = false,
                            Name = "Luna",
                            Price = 105.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 10L,
                            AdoptionStatus = 0,
                            Breed = "Sphynx",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Milo is an outgoing and social cat. Despite having no fur, he is very warm and enjoys being around people.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/closeup-up-photography-of-tri-color-kitten-691583/",
                            IsActive = false,
                            Name = "Milo",
                            Price = 95.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 11L,
                            AdoptionStatus = 0,
                            Breed = "Scottish Fold",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Toby is a calm and loving cat with distinctive folded ears. He enjoys lounging around and being spoiled with treats.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/close-up-photo-of-white-cat-1485968/",
                            IsActive = false,
                            Name = "Toby",
                            Price = 115.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 12L,
                            AdoptionStatus = 0,
                            Breed = "Birman",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Chloe is an elegant cat with a luxurious coat. She loves being pampered and enjoys a good scratch behind the ears.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/white-and-brown-cat-1687831/",
                            IsActive = false,
                            Name = "Chloe",
                            Price = 130.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 13L,
                            AdoptionStatus = 0,
                            Breed = "Burmese",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Leo is an affectionate and playful cat. He loves attention and follows his owner around the house.",
                            Gender = 0,
                            ImageUrl = "https://www.pexels.com/photo/photo-of-cat-climbing-on-tree-1653357/",
                            IsActive = false,
                            Name = "Leo",
                            Price = 140.0,
                            Views = 0
                        },
                        new
                        {
                            Id = 14L,
                            AdoptionStatus = 0,
                            Breed = "Egyptian Mau",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Penny is a lively and graceful cat with a striking spotted coat. She’s very active and loves to play with interactive toys.",
                            Gender = 1,
                            ImageUrl = "https://www.pexels.com/photo/white-and-black-kitten-lying-on-tiles-479009/",
                            IsActive = false,
                            Name = "Penny",
                            Price = 120.0,
                            Views = 0
                        });
                });

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.UserAdoption", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AdoptedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CatId")
                        .HasColumnType("int");

                    b.Property<long>("CatId1")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<long>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CatId1");

                    b.HasIndex("UserId1");

                    b.ToTable("UserAdoptions");
                });

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.UserFavorites", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("CatId")
                        .HasColumnType("int");

                    b.Property<long>("CatId1")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<long>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CatId1");

                    b.HasIndex("UserId1");

                    b.ToTable("UserFavorites");
                });

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.UserAdoption", b =>
                {
                    b.HasOne("CatAdoptionMobileApp.Domain.Models.Cat", "Cat")
                        .WithMany()
                        .HasForeignKey("CatId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatAdoptionMobileApp.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatAdoptionMobileApp.Domain.Models.UserFavorites", b =>
                {
                    b.HasOne("CatAdoptionMobileApp.Domain.Models.Cat", "Cat")
                        .WithMany()
                        .HasForeignKey("CatId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatAdoptionMobileApp.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cat");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
