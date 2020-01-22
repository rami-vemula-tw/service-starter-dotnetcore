﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentApi.Models;

namespace PaymentApi.Migrations.BankInfo
{
    [DbContext(typeof(BankInfoContext))]
    partial class BankInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PaymentApi.Models.BankInfo", b =>
                {
                    b.Property<string>("BankCode")
                        .HasColumnType("text");

                    b.Property<string>("AccountAPIUrl")
                        .HasColumnType("text");

                    b.Property<string>("PaymentAPIURL")
                        .HasColumnType("text");

                    b.HasKey("BankCode");

                    b.ToTable("BankInfo");
                });
#pragma warning restore 612, 618
        }
    }
}