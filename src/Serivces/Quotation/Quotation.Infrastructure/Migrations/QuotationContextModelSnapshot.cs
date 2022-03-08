﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Quotation.Infrastructure;

#nullable disable

namespace Quotation.Infrastructure.Migrations
{
    [DbContext(typeof(QuotationContext))]
    partial class QuotationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("quotationprofitseq", "quotation")
                .IncrementsBy(10);

            modelBuilder.HasSequence("quotationseq", "quotation")
                .IncrementsBy(10);

            modelBuilder.Entity("Quotation.Domain.AggregatesModel.QuotationAggregate.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "quotationseq", "quotation");

                    b.Property<string>("FIGI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Quotation", "quotation");
                });

            modelBuilder.Entity("Quotation.Domain.AggregatesModel.QuotationProfitAggregate.QuotationProfit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "quotationprofitseq", "quotation");

                    b.Property<int>("CountBuyQuotationPossible")
                        .HasColumnType("integer");

                    b.Property<string>("FIGI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("InvestedAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayoutAvg")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PayoutsYieldAvg")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PossibleProfitSpeculation")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PriceAvg")
                        .HasColumnType("numeric");

                    b.Property<decimal>("QuantityPaymentsAvg")
                        .HasColumnType("numeric");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("QuotationProfit", "quotation");
                });
#pragma warning restore 612, 618
        }
    }
}