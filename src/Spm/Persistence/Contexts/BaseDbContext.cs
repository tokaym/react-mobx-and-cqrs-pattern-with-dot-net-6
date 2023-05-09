using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;

        }

        public BaseDbContext()
        {
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Zs14> Zs14s { get; set; }
        public DbSet<Zm20> Zm20s { get; set; }
        public DbSet<Zm20History> Zm20Histories { get; set; }
        public DbSet<Mb51> Mb51s { get; set; }
        public DbSet<Mb51History> Mb51Histories { get; set; }
        public DbSet<MainReport> MainReports { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<MaterialGroup> MaterialGroups { get; set; }
        public DbSet<Mip> Mips { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<RomaniaZm20> RomaniaZm20s { get; set; }
        public DbSet<RomaniaZm20History> RomaniaZm20Histories { get; set; }

        public async Task<int> ExecuteSqlQueryAsync(string query)
        {
            return await this.Database.ExecuteSqlRawAsync(query);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<User>(c =>
            {
                c.ToTable("Users").HasKey(k => k.Id);
                c.Property(p => p.Name).HasColumnName("Name");
                c.Property(p => p.Mail).HasColumnName("Mail");
                c.Property(p => p.EmployeeNo).HasColumnName("EmployeeNo");
                c.Property(p => p.Status).HasColumnName("Status").HasDefaultValue(true);
            });

            modelBuilder.Entity<Mb51>(c =>
            {
                c.ToTable("Mb51s").HasKey(k => k.Id);
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.Reference).HasColumnName("Reference");
                c.Property(p => p.RegisterDate).HasColumnName("RegisterDate");
                c.Property(p => p.Amount).HasColumnName("Amount");
                c.Property(p => p.ITU).HasColumnName("ITU");
                c.Property(p => p.Dpyr).HasColumnName("Dpyr");
                c.Property(p => p.HrkTUMtn).HasColumnName("HrkTUMtn");
                c.Property(p => p.MaterialInfo).HasColumnName("MaterialInfo");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.Customer).HasColumnName("Customer");
                c.Property(p => p.SatSasNo).HasColumnName("SatSasNo");
            });

            modelBuilder.Entity<Mb51History>(c =>
            {
                c.ToTable("Mb51Histories").HasKey(k => k.Id);
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.Reference).HasColumnName("Reference");
                c.Property(p => p.RegisterDate).HasColumnName("RegisterDate");
                c.Property(p => p.Amount).HasColumnName("Amount");
                c.Property(p => p.ITU).HasColumnName("ITU");
                c.Property(p => p.Dpyr).HasColumnName("Dpyr");
                c.Property(p => p.HrkTUMtn).HasColumnName("HrkTUMtn");
                c.Property(p => p.MaterialInfo).HasColumnName("MaterialInfo");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.Customer).HasColumnName("Customer");
                c.Property(p => p.CreatedTime).HasColumnName("CreatedTime");
                c.Property(p => p.ReportDate).HasColumnName("ReportDate");
                c.Property(p => p.SatSasNo).HasColumnName("SatSasNo");

                c.HasOne(h => h.User);
            });

            modelBuilder.Entity<Zs14>(c =>
            {
                c.ToTable("Zs14s").HasKey(k => k.Id);
                c.Property(p => p.CgrSas).HasColumnName("CgrSas");
                c.Property(p => p.CgrSat).HasColumnName("CgrSat");
                c.Property(p => p.ConsumptionValue).HasColumnName("ConsumptionValue");
                c.Property(p => p.DeadlineDate).HasColumnName("DeadlineDate");
                c.Property(p => p.Definition).HasColumnName("Definition");
                c.Property(p => p.Dr).HasColumnName("Dr");
                c.Property(p => p.Dr2).HasColumnName("Dr2");
                c.Property(p => p.Empty1).HasColumnName("Empty1");
                c.Property(p => p.HfOrder).HasColumnName("HfOrder");
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.InstantDate).HasColumnName("InstantDate");
                c.Property(p => p.IsSafety).HasColumnName("IsSafety");
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.MIhrTes).HasColumnName("MIhrTes");
                c.Property(p => p.Mip).HasColumnName("Mip");
                c.Property(p => p.MipArea).HasColumnName("MipArea");
                c.Property(p => p.MP).HasColumnName("MP");
                c.Property(p => p.Sas).HasColumnName("Sas");
                c.Property(p => p.SasConfirm).HasColumnName("SasConfirm");
                c.Property(p => p.SasDelivery).HasColumnName("SasDelivery");
                c.Property(p => p.Sat).HasColumnName("Sat");
                c.Property(p => p.SG).HasColumnName("SG");
                c.Property(p => p.Star).HasColumnName("Star");
                c.Property(p => p.Stnkrz).HasColumnName("Stnkrz");
                c.Property(p => p.Teslpln).HasColumnName("Teslpln");
                c.Property(p => p.TransportStock).HasColumnName("TransportStock");
                c.Property(p => p.TYeri).HasColumnName("TYeri");
                c.Property(p => p.UYAmbar).HasColumnName("UYAmbar");
                c.Property(p => p.UYDiger).HasColumnName("UYDiger");
                c.Property(p => p.YDIlkSip).HasColumnName("YDIlkSip");
                c.Property(p => p.YDKDOrder).HasColumnName("YDKDOrder");
                c.Property(p => p.YDKIOrder).HasColumnName("YDKIOrder");
                c.Property(p => p.YIIlkSip).HasColumnName("YIIlkSip");
                c.Property(p => p.YP).HasColumnName("YP");
                c.Property(p => p.YsOrder).HasColumnName("YsOrder");
            });

            modelBuilder.Entity<Zm20>(c =>
            {
                c.ToTable("Zm20s").HasKey(k => k.Id);
                c.Property(p => p.MaterialSat).HasColumnName("MaterialSat");
                c.Property(p => p.Name).HasColumnName("Name");
                c.Property(p => p.Star).HasColumnName("Star");
                c.Property(p => p.UY).HasColumnName("UY");
                c.Property(p => p.UYCode).HasColumnName("UYCode");
                c.Property(p => p.DY).HasColumnName("DY");
                c.Property(p => p.ReleaseDate).HasColumnName("ReleaseDate");
                c.Property(p => p.ArrivalDate).HasColumnName("ArrivalDate");
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.Unit).HasColumnName("Unit");
                c.Property(p => p.AmountDelivered).HasColumnName("AmountDelivered");
                c.Property(p => p.OpenAmount).HasColumnName("OpenAmount");
                c.Property(p => p.RemainingStock).HasColumnName("RemainingStock");
                c.Property(p => p.QualityStock).HasColumnName("QualityStock");
                c.Property(p => p.SatSasNo).HasColumnName("SatSasNo");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.ConfirmationDate).HasColumnName("ConfirmationDate");
                c.Property(p => p.Mip).HasColumnName("Mip");
                c.Property(p => p.TesMip).HasColumnName("TesMip");
                c.Property(p => p.SrvRef).HasColumnName("SrvRef");
                c.Property(p => p.AlanUYEmnStok).HasColumnName("AlanUYEmnStok");
                c.Property(p => p.AlanUYYuvDeg).HasColumnName("AlanUYYuvDeg");
                c.Property(p => p.Empty1).HasColumnName("Empty1");
                c.Property(p => p.Empty2).HasColumnName("Empty2");
                c.Property(p => p.Empty3).HasColumnName("Empty3");
                c.Property(p => p.Empty4).HasColumnName("Empty4");
                c.Property(p => p.TT).HasColumnName("TT");
                c.Property(p => p.Empty5).HasColumnName("Empty5");
                c.Property(p => p.Seller).HasColumnName("Seller");
                c.Property(p => p.SellerName).HasColumnName("SellerName");
                c.Property(p => p.Empty6).HasColumnName("Empty6");
                c.Property(p => p.Empty7).HasColumnName("Empty7");
                c.Property(p => p.Empty8).HasColumnName("Empty8");
                c.Property(p => p.ContractNo).HasColumnName("ContractNo");
                c.Property(p => p.Empty9).HasColumnName("Empty9");
                c.Property(p => p.Item2).HasColumnName("Item2");
            });

            modelBuilder.Entity<Zm20History>(c =>
           {
               c.ToTable("Zm20Histories").HasKey(k => k.Id);
               c.Property(p => p.MaterialSat).HasColumnName("MaterialSat");
               c.Property(p => p.Name).HasColumnName("Name");
               c.Property(p => p.Star).HasColumnName("Star");
               c.Property(p => p.UY).HasColumnName("UY");
               c.Property(p => p.UYCode).HasColumnName("UYCode");
               c.Property(p => p.DY).HasColumnName("DY");
               c.Property(p => p.ReleaseDate).HasColumnName("ReleaseDate");
               c.Property(p => p.ArrivalDate).HasColumnName("ArrivalDate");
               c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
               c.Property(p => p.MaterialName).HasColumnName("MaterialName");
               c.Property(p => p.Unit).HasColumnName("Unit");
               c.Property(p => p.AmountDelivered).HasColumnName("AmountDelivered");
               c.Property(p => p.OpenAmount).HasColumnName("OpenAmount");
               c.Property(p => p.RemainingStock).HasColumnName("RemainingStock");
               c.Property(p => p.QualityStock).HasColumnName("QualityStock");
               c.Property(p => p.SatSasNo).HasColumnName("SatSasNo");
               c.Property(p => p.Item).HasColumnName("Item");
               c.Property(p => p.ConfirmationDate).HasColumnName("ConfirmationDate");
               c.Property(p => p.Mip).HasColumnName("Mip");
               c.Property(p => p.TesMip).HasColumnName("TesMip");
               c.Property(p => p.SrvRef).HasColumnName("SrvRef");
               c.Property(p => p.AlanUYEmnStok).HasColumnName("AlanUYEmnStok");
               c.Property(p => p.AlanUYYuvDeg).HasColumnName("AlanUYYuvDeg");
               c.Property(p => p.Empty1).HasColumnName("Empty1");
               c.Property(p => p.Empty2).HasColumnName("Empty2");
               c.Property(p => p.Empty3).HasColumnName("Empty3");
               c.Property(p => p.Empty4).HasColumnName("Empty4");
               c.Property(p => p.TT).HasColumnName("TT");
               c.Property(p => p.Empty5).HasColumnName("Empty5");
               c.Property(p => p.Seller).HasColumnName("Seller");
               c.Property(p => p.SellerName).HasColumnName("SellerName");
               c.Property(p => p.Empty6).HasColumnName("Empty6");
               c.Property(p => p.Empty7).HasColumnName("Empty7");
               c.Property(p => p.Empty8).HasColumnName("Empty8");
               c.Property(p => p.ContractNo).HasColumnName("ContractNo");
               c.Property(p => p.Empty9).HasColumnName("Empty9");
               c.Property(p => p.Item2).HasColumnName("Item2");
               c.Property(p => p.CreatedTime).HasColumnName("CreatedTime");
               c.Property(p => p.ReportDate).HasColumnName("ReportDate");

               c.HasOne(h => h.User);
           });

            modelBuilder.Entity<MainReport>(c =>
            {
                c.ToTable("MainReports").HasKey(k => k.Id);
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.OpenAmount).HasColumnName("OpenAmount");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.HF).HasColumnName("HF");
                c.Property(p => p.Urgent).HasColumnName("Urgent");
                c.Property(p => p.FirstOrderDate).HasColumnName("FirstOrderDate");
                c.Property(p => p.Company).HasColumnName("Company");
                c.Property(p => p.ProductClass).HasColumnName("ProductClass");
                c.Property(p => p.CD).HasColumnName("CD");
                c.Property(p => p.Stock).HasColumnName("Stock");
                c.Property(p => p.SasCloses).HasColumnName("SasCloses");
                c.Property(p => p.UrgentCloses).HasColumnName("UrgentCloses");
                c.Property(p => p.HfCloses).HasColumnName("HfCloses");
                c.Property(p => p.ThStock).HasColumnName("ThStock");
                c.Property(p => p.Mip).HasColumnName("Mip");
                c.Property(p => p.MipLiable).HasColumnName("MipLiable");
                c.Property(p => p.Sent).HasColumnName("Sent");
                c.Property(p => p.TT).HasColumnName("TT");
                c.Property(p => p.ReportDate).HasColumnName("ReportDate");

                c.HasOne(h => h.User);
                c.HasOne(h => h.Plant);
            });

            modelBuilder.Entity<MaterialGroup>(c =>
            {
                c.ToTable("MaterialGroups").HasKey(k => k.Id);
                c.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                c.Property(p => p.GroupName).HasColumnName("GroupName");
            });

            modelBuilder.Entity<Mip>(c =>
            {
                c.ToTable("Mips").HasKey(k => k.Id);
                c.Property(p => p.Code).HasColumnName("Code");
                c.Property(p => p.CD).HasColumnName("CD");
            });

            modelBuilder.Entity<Estimate>(e =>
            {
                e.ToTable("Estimates").HasKey(k => k.Id);
                e.Property(p => p.MaterialSKU).HasColumnName("MaterialSKU");
                e.Property(p => p.Month).HasColumnName("Month");
                e.Property(p => p.Year).HasColumnName("Year");
                e.Property(p => p.Quantity).HasColumnName("Quantity");
                e.Property(p => p.CreatedTime).HasColumnName("CreatedTime");
            });

            modelBuilder.Entity<Setting>(c =>
            {
                c.ToTable("Settings").HasKey(k => k.Id);
                c.Property(p => p.Name).HasColumnName("Name");
                c.Property(p => p.Value).HasColumnName("Value");
                c.Property(p => p.Description).HasColumnName("Description");
            });

            modelBuilder.Entity<Plant>(c =>
            {
                c.ToTable("Plants").HasKey(k => k.Id);
                c.Property(p => p.Code).HasColumnName("Code");
                c.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<RomaniaZm20>(c =>
            {
                c.ToTable("RomaniaZm20s").HasKey(k => k.Id);
                c.Property(p => p.MaterialSat).HasColumnName("MaterialSat");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.TrmSt).HasColumnName("TrmSt");
                c.Property(p => p.StBu).HasColumnName("StBu");
                c.Property(p => p.SalesInf).HasColumnName("SalesInf");
                c.Property(p => p.Item2).HasColumnName("Item2");
                c.Property(p => p.SaRequest).HasColumnName("SaRequest");
                c.Property(p => p.Item3).HasColumnName("Item3");
                c.Property(p => p.Orderer).HasColumnName("Orderer");
                c.Property(p => p.Ad1).HasColumnName("Ad1");
                c.Property(p => p.UY).HasColumnName("UY");
                c.Property(p => p.DPYR).HasColumnName("DPYR");
                c.Property(p => p.UY2).HasColumnName("UY2");
                c.Property(p => p.Sag).HasColumnName("Sag");
                c.Property(p => p.Definition).HasColumnName("Definition");
                c.Property(p => p.MaterialNo).HasColumnName("MaterialNo");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.ReleaseDate).HasColumnName("ReleaseDate");
                c.Property(p => p.ArrivalDate).HasColumnName("ArrivalDate");
                c.Property(p => p.ConfirmationDate).HasColumnName("ConfirmationDate");
                c.Property(p => p.Delivered).HasColumnName("Delivered");
                c.Property(p => p.Unit).HasColumnName("Unit");
                c.Property(p => p.OpenQuantity).HasColumnName("OpenQuantity");
                c.Property(p => p.Suply).HasColumnName("Suply");
                c.Property(p => p.Mip).HasColumnName("Mip");
                c.Property(p => p.TesMip).HasColumnName("TesMip");
                c.Property(p => p.Cd).HasColumnName("Cd");
                c.Property(p => p.SrvRef).HasColumnName("SrvRef");
                c.Property(p => p.Quality).HasColumnName("Quality");
                c.Property(p => p.AlanUYEm).HasColumnName("AlanUYEm");
                c.Property(p => p.AlanUYY).HasColumnName("AlanUYY");
                c.Property(p => p.Star).HasColumnName("Star");
                c.Property(p => p.TahKlm).HasColumnName("TahKlm");
                c.Property(p => p.Seller).HasColumnName("Seller");
                c.Property(p => p.Ad11).HasColumnName("Ad11");
                c.Property(p => p.Alternative).HasColumnName("Alternative");
                c.Property(p => p.SasItem).HasColumnName("SasItem");
            });

            modelBuilder.Entity<RomaniaZm20History>(c =>
            {
                c.ToTable("RomaniaZm20Histories").HasKey(k => k.Id);
                c.Property(p => p.MaterialSat).HasColumnName("MaterialSat");
                c.Property(p => p.Item).HasColumnName("Item");
                c.Property(p => p.TrmSt).HasColumnName("TrmSt");
                c.Property(p => p.StBu).HasColumnName("StBu");
                c.Property(p => p.SalesInf).HasColumnName("SalesInf");
                c.Property(p => p.Item2).HasColumnName("Item2");
                c.Property(p => p.SaRequest).HasColumnName("SaRequest");
                c.Property(p => p.Item3).HasColumnName("Item3");
                c.Property(p => p.Orderer).HasColumnName("Orderer");
                c.Property(p => p.Ad1).HasColumnName("Ad1");
                c.Property(p => p.UY).HasColumnName("UY");
                c.Property(p => p.DPYR).HasColumnName("DPYR");
                c.Property(p => p.UY2).HasColumnName("UY2");
                c.Property(p => p.Sag).HasColumnName("Sag");
                c.Property(p => p.Definition).HasColumnName("Definition");
                c.Property(p => p.MaterialNo).HasColumnName("MaterialNo");
                c.Property(p => p.MaterialName).HasColumnName("MaterialName");
                c.Property(p => p.ReleaseDate).HasColumnName("ReleaseDate");
                c.Property(p => p.ArrivalDate).HasColumnName("ArrivalDate");
                c.Property(p => p.ConfirmationDate).HasColumnName("ConfirmationDate");
                c.Property(p => p.Delivered).HasColumnName("Delivered");
                c.Property(p => p.Unit).HasColumnName("Unit");
                c.Property(p => p.OpenQuantity).HasColumnName("OpenQuantity");
                c.Property(p => p.Suply).HasColumnName("Suply");
                c.Property(p => p.Mip).HasColumnName("Mip");
                c.Property(p => p.TesMip).HasColumnName("TesMip");
                c.Property(p => p.Cd).HasColumnName("Cd");
                c.Property(p => p.SrvRef).HasColumnName("SrvRef");
                c.Property(p => p.Quality).HasColumnName("Quality");
                c.Property(p => p.AlanUYEm).HasColumnName("AlanUYEm");
                c.Property(p => p.AlanUYY).HasColumnName("AlanUYY");
                c.Property(p => p.Star).HasColumnName("Star");
                c.Property(p => p.TahKlm).HasColumnName("TahKlm");
                c.Property(p => p.Seller).HasColumnName("Seller");
                c.Property(p => p.Ad11).HasColumnName("Ad11");
                c.Property(p => p.Alternative).HasColumnName("Alternative");
                c.Property(p => p.SasItem).HasColumnName("SasItem");
                c.Property(p => p.CreatedTime).HasColumnName("CreatedTime");
                c.Property(p => p.ReportDate).HasColumnName("ReportDate");

                c.HasOne(h => h.User);
            });
        }
    }
}