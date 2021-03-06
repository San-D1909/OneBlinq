// <auto-generated />
using System;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Backend.Models.CompanyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BTWNumber")
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<string>("KVKNumber")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Backend.Models.DeviceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("varchar(767)");

                    b.Property<int>("LicenseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "LicenseId", "DeviceToken" }, "IX_UNIQUE_DEVICE")
                        .IsUnique();

                    b.ToTable("Device");
                });

            modelBuilder.Entity("Backend.Models.LicenseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LicenseKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text");

                    b.Property<int>("TimesActivated")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("VariantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VariantId");

                    b.ToTable("License");
                });

            modelBuilder.Entity("Backend.Models.PluginBundleImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageData")
                        .IsRequired()
                        .HasColumnType("LONGTEXT")
                        .HasColumnName("imagedata");

                    b.Property<int?>("PluginBundleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PluginBundleId");

                    b.ToTable("PluginBundleImage");
                });

            modelBuilder.Entity("Backend.Models.PluginBundleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BundleDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BundleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StripeProductId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PluginBundle");
                });

            modelBuilder.Entity("Backend.Models.PluginBundleVariantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSubscription")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxActivations")
                        .HasColumnType("int");

                    b.Property<int>("PluginBundleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("StripePriceId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PluginBundleId");

                    b.ToTable("PluginBundleVariant");
                });

            modelBuilder.Entity("Backend.Models.PluginBundlesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PluginBundleId")
                        .HasColumnType("int");

                    b.Property<int>("PluginId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.HasIndex(new[] { "PluginBundleId", "PluginId" }, "IX_UNIQUE_PLUGINBUNDLE")
                        .IsUnique();

                    b.ToTable("PluginBundles");
                });

            modelBuilder.Entity("Backend.Models.PluginImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageData")
                        .IsRequired()
                        .HasColumnType("LONGTEXT")
                        .HasColumnName("imagedata");

                    b.Property<int?>("PluginId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.ToTable("PluginImage");
                });

            modelBuilder.Entity("Backend.Models.PluginLicenseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LicenseId")
                        .HasColumnType("int");

                    b.Property<int?>("PluginBundleId")
                        .HasColumnType("int");

                    b.Property<int?>("PluginId")
                        .HasColumnType("int");

                    b.Property<int>("TimesActivated")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PluginBundleId");

                    b.HasIndex("PluginId");

                    b.HasIndex(new[] { "LicenseId", "PluginId", "PluginBundleId" }, "IX_UNIQUE_PLUGINLICENSE")
                        .IsUnique();

                    b.ToTable("PluginLicense");
                });

            modelBuilder.Entity("Backend.Models.PluginModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PluginDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PluginName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StripeProductId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Plugin");
                });

            modelBuilder.Entity("Backend.Models.PluginVariantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSubscription")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxActivations")
                        .HasColumnType("int");

                    b.Property<int>("PluginId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("StripePriceId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.ToTable("PluginVariant");
                });

            modelBuilder.Entity("Backend.Models.ResetTokenModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResetToken");
                });

            modelBuilder.Entity("Backend.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(4000)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Backend.Models.LicenseModel", b =>
                {
                    b.HasOne("Backend.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PluginVariantModel", "Variant")
                        .WithMany()
                        .HasForeignKey("VariantId");

                    b.Navigation("User");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("Backend.Models.PluginBundleImageModel", b =>
                {
                    b.HasOne("Backend.Models.PluginBundleModel", "PluginBundle")
                        .WithMany()
                        .HasForeignKey("PluginBundleId");

                    b.Navigation("PluginBundle");
                });

            modelBuilder.Entity("Backend.Models.PluginBundleVariantModel", b =>
                {
                    b.HasOne("Backend.Models.PluginBundleModel", "PluginBundle")
                        .WithMany()
                        .HasForeignKey("PluginBundleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PluginBundle");
                });

            modelBuilder.Entity("Backend.Models.PluginBundlesModel", b =>
                {
                    b.HasOne("Backend.Models.PluginBundleModel", "PluginBundle")
                        .WithMany()
                        .HasForeignKey("PluginBundleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PluginModel", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plugin");

                    b.Navigation("PluginBundle");
                });

            modelBuilder.Entity("Backend.Models.PluginImageModel", b =>
                {
                    b.HasOne("Backend.Models.PluginModel", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId");

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("Backend.Models.PluginLicenseModel", b =>
                {
                    b.HasOne("Backend.Models.LicenseModel", "License")
                        .WithMany()
                        .HasForeignKey("LicenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PluginBundleModel", "PluginBundle")
                        .WithMany()
                        .HasForeignKey("PluginBundleId");

                    b.HasOne("Backend.Models.PluginModel", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId");

                    b.Navigation("License");

                    b.Navigation("Plugin");

                    b.Navigation("PluginBundle");
                });

            modelBuilder.Entity("Backend.Models.PluginVariantModel", b =>
                {
                    b.HasOne("Backend.Models.PluginModel", "Plugin")
                        .WithMany()
                        .HasForeignKey("PluginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("Backend.Models.UserModel", b =>
                {
                    b.HasOne("Backend.Models.CompanyModel", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
