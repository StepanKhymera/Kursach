using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace kursach.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ClaimPayment> ClaimPayments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyInsuranceObject> PolicyInsuranceObjects { get; set; }
        public virtual DbSet<PolicyPayment> PolicyPayments { get; set; }
        public virtual DbSet<PositionType> PositionTypes { get; set; }
        public virtual DbSet<PropertyParameter> PropertyParameters { get; set; }
        public virtual DbSet<StatusesOfClaim> StatusesOfClaims { get; set; }
        public virtual DbSet<Types_Of_Insurance> Types_Of_Insurances { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4IJ8AA6;Database=InsuranceBroker;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.Property(e => e.ClaimId).ValueGeneratedNever();

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_Policies");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claims_Statuses_Of_Claims");
            });

            modelBuilder.Entity<ClaimPayment>(entity =>
            {
                entity.Property(e => e.ClaimPaymentId).ValueGeneratedNever();

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.ClaimPayments)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Claim_Payments_Claims");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_Addresses");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.OfficeId).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Employees_Addresses");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employees_Offices");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employees_Position_Types");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.Property(e => e.OfficeId).ValueGeneratedNever();

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offices_Addresses");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.PolicyId).ValueGeneratedNever();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policies_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policies_Employees");

                entity.HasOne(d => d.InsuranceTypeNavigation)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.InsuranceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policies_Types_Of_Insurance");
            });

            modelBuilder.Entity<PolicyInsuranceObject>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.PolicyInsuranceObjects)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policy_InsuranceObject_Property_Parameters");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyInsuranceObjects)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policy_InsuranceObject_Policies");
            });

            modelBuilder.Entity<PolicyPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK_Payments");

                entity.Property(e => e.PaymentId).ValueGeneratedNever();

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyPayments)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Policies");
            });

            modelBuilder.Entity<PositionType>(entity =>
            {
                entity.Property(e => e.PositionTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<PropertyParameter>(entity =>
            {
                entity.Property(e => e.ParamaterId).ValueGeneratedNever();

                entity.HasOne(d => d.TypeOfInsuranceNavigation)
                    .WithMany(p => p.PropertyParameters)
                    .HasForeignKey(d => d.TypeOfInsurance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_Parameters_Types_Of_Insurance");
            });

            modelBuilder.Entity<StatusesOfClaim>(entity =>
            {
                entity.Property(e => e.StatusCode).ValueGeneratedNever();

                entity.Property(e => e.StatusDescription).IsUnicode(false);
            });

            modelBuilder.Entity<Types_Of_Insurance>(entity =>
            {
                entity.Property(e => e.InsuranceTypeCode).ValueGeneratedNever();

                entity.Property(e => e.TypeDescription).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
