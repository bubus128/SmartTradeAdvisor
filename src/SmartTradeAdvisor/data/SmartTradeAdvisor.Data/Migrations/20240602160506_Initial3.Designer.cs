﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartTradeAdvisor.Data.DbContexts;

#nullable disable

namespace SmartTradeAdvisor.Data.Migrations
{
    [DbContext(typeof(IndexDbContext))]
    [Migration("20240602160506_Initial3")]
    partial class Initial3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Adx", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Adx");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Cmo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Cmo");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Macd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Macd");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Macd");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Mfi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Mfi");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.NegativeDi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("NegativeDi");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.PositiveDi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("PositiveDi");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Rsi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Rsi");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Ult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Ult");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.MarketIndex", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("MarketIndexes");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.MarketIndexValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("ClosingValue")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("HighValue")
                        .HasColumnType("double precision");

                    b.Property<double>("LowValue")
                        .HasColumnType("double precision");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Volume")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("MarketIndexValues");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.MacdSignal", b =>
                {
                    b.HasBaseType("SmartTradeAdvisor.Data.Entities.Indexes.Macd");

                    b.HasDiscriminator().HasValue("MacdSignal");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Adx", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Cmo", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Macd", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Mfi", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.NegativeDi", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.PositiveDi", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Rsi", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Ult", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany()
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.MarketIndexValue", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany("MarketIndexValues")
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.MarketIndex", b =>
                {
                    b.Navigation("MarketIndexValues");
                });
#pragma warning restore 612, 618
        }
    }
}
