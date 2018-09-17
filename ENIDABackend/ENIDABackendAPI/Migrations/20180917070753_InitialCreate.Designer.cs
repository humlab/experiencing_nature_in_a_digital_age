﻿// <auto-generated />
using ENIDABackendAPI.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ENIDABackendAPI.Migrations
{
    [DbContext(typeof(ENIDADbContext))]
    [Migration("20180917070753_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065");

            modelBuilder.Entity("ENIDABackendAPI.Model.Image", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MaxYOffset");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ENIDABackendAPI.Model.Information", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("ImageId");

                    b.Property<int>("Type");

                    b.Property<int>("YOffset");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Information");
                });

            modelBuilder.Entity("ENIDABackendAPI.Model.Information", b =>
                {
                    b.HasOne("ENIDABackendAPI.Model.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });
#pragma warning restore 612, 618
        }
    }
}
