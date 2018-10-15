﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.Migrations
{
    [DbContext(typeof(HotelContext))]
    partial class HotelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test_Project_Hotel.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientFIO");

                    b.Property<string>("ClientPassportData");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Test_Project_Hotel.Models.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoomCapacity");

                    b.Property<string>("RoomDescription");

                    b.Property<decimal>("RoomPrice");

                    b.Property<string>("RoomType");

                    b.HasKey("RoomID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Test_Project_Hotel.Models.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientID");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<DateTime>("EntryDate");

                    b.Property<int?>("RoomID");

                    b.Property<string>("ServiceDescription");

                    b.Property<string>("ServiceName");

                    b.Property<int?>("WorkerID");

                    b.HasKey("ServiceID");

                    b.HasIndex("ClientID");

                    b.HasIndex("RoomID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Test_Project_Hotel.Models.Worker", b =>
                {
                    b.Property<int>("WorkerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WorkerFIO");

                    b.Property<string>("WorkerPost");

                    b.HasKey("WorkerID");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Test_Project_Hotel.Models.Service", b =>
                {
                    b.HasOne("Test_Project_Hotel.Models.Client", "Client")
                        .WithMany("Services")
                        .HasForeignKey("ClientID");

                    b.HasOne("Test_Project_Hotel.Models.Room", "Room")
                        .WithMany("Services")
                        .HasForeignKey("RoomID");

                    b.HasOne("Test_Project_Hotel.Models.Worker", "Worker")
                        .WithMany("Services")
                        .HasForeignKey("WorkerID");
                });
#pragma warning restore 612, 618
        }
    }
}