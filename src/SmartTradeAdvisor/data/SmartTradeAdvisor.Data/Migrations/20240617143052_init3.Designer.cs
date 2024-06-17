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
    [Migration("20240617143052_init3")]
    partial class init3
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

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Macd");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.MacdSignal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("MacdSignal");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Mfi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("NegativeDis");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.PositiveDi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("PositiveDis");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.Rsi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Wallet.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Seal")
                        .HasColumnType("boolean");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Wallet.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MarketIndexId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Strategy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MarketIndexId");

                    b.ToTable("Wallets");
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

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Indexes.MacdSignal", b =>
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

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Wallet.Transaction", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.Wallet.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("SmartTradeAdvisor.Data.Entities.MarketIndex", "MarketIndex")
                        .WithMany("Wallets")
                        .HasForeignKey("MarketIndexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketIndex");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.MarketIndex", b =>
                {
                    b.Navigation("MarketIndexValues");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("SmartTradeAdvisor.Data.Entities.Wallet.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
