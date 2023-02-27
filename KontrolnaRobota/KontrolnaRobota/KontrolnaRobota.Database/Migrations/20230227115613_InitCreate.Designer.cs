﻿// <auto-generated />
using System;
using KontrolnaRobota.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KontrolnaRobota.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230227115613_InitCreate")]
    partial class InitCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.BuyerEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buyer", (string)null);
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.CheckEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BuyerFK")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfBuying")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuyerFK");

                    b.ToTable("Checks", (string)null);
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.ProductEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CheckFK")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CheckFK");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.CheckEntity", b =>
                {
                    b.HasOne("KontrolnaRobota.Database.Entities.BuyerEntity", "Buyer")
                        .WithMany("Checks")
                        .HasForeignKey("BuyerFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.ProductEntity", b =>
                {
                    b.HasOne("KontrolnaRobota.Database.Entities.CheckEntity", "Check")
                        .WithMany("Products")
                        .HasForeignKey("CheckFK")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Check");
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.BuyerEntity", b =>
                {
                    b.Navigation("Checks");
                });

            modelBuilder.Entity("KontrolnaRobota.Database.Entities.CheckEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
