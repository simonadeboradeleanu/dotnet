﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet.Data;

#nullable disable

namespace dotnet.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("dotnet.Models.Detail", b =>
                {
                    b.Property<int>("SharkId")
                        .HasColumnType("int");

                    b.Property<int?>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("FavFood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Update")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SharkId");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("dotnet.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("admin")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("dotnet.Models.UserShark", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("SharkId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "SharkId");

                    b.HasIndex("SharkId");

                    b.ToTable("UserSharks");
                });

            modelBuilder.Entity("proiectelul.Models.Ocean", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Oceans");
                });

            modelBuilder.Entity("proiectelul.Models.Shark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OceanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OceanId");

                    b.ToTable("Sharks");
                });

            modelBuilder.Entity("dotnet.Models.Detail", b =>
                {
                    b.HasOne("proiectelul.Models.Shark", "Shark")
                        .WithOne("Detail")
                        .HasForeignKey("dotnet.Models.Detail", "SharkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shark");
                });

            modelBuilder.Entity("dotnet.Models.UserShark", b =>
                {
                    b.HasOne("proiectelul.Models.Shark", "Shark")
                        .WithMany("UserSharks")
                        .HasForeignKey("SharkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany("UserSharks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shark");

                    b.Navigation("User");
                });

            modelBuilder.Entity("proiectelul.Models.Shark", b =>
                {
                    b.HasOne("proiectelul.Models.Ocean", "Ocean")
                        .WithMany("Sharks")
                        .HasForeignKey("OceanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ocean");
                });

            modelBuilder.Entity("dotnet.Models.User", b =>
                {
                    b.Navigation("UserSharks");
                });

            modelBuilder.Entity("proiectelul.Models.Ocean", b =>
                {
                    b.Navigation("Sharks");
                });

            modelBuilder.Entity("proiectelul.Models.Shark", b =>
                {
                    b.Navigation("Detail")
                        .IsRequired();

                    b.Navigation("UserSharks");
                });
#pragma warning restore 612, 618
        }
    }
}
