using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using WebApplication1.Models.Activities;

namespace WebApplication1.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SenhaEmail { get; set; }

        [Required]
        public string Funcao { get; set; }

        #region Propriedades de navegação
        public virtual ICollection<AccessRemote> AccessesRemote { get; set; }
        public virtual ICollection<AccessRemoteArchive> AccessRemoteArchives { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Evidence> Evidences { get; set; }
        public virtual ICollection<EvidenceArchive> EvidenceArchives { get; set; }
        public virtual ICollection<EvidenceSolution> EvidenceSolutions { get; set; }
        public virtual ICollection<HelperCode> HelperCodes { get; set; }
        public virtual ICollection<HelperCodeArchive> HelperCodeArchives { get; set; }
        public virtual ICollection<LocalError> LocalErrors { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Problem> Problems { get; set; }
        public virtual ICollection<Requester> Requesters { get; set; }
        public virtual ICollection<RequestMeans> RequestMeanses { get; set; }
        public virtual ICollection<Screen> Screenses { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual ICollection<SolutionArchive> SolutionArchives { get; set; }
        public virtual ICollection<SolutionInformation> SolutionInformations { get; set; }
        public virtual ICollection<Status> Status { get; set; }
        #endregion
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MySql", throwIfV1Schema: false)
        {
            Configuration.ValidateOnSaveEnabled = false;
        }

        #region Declaração de propriedades
        public virtual DbSet<AccessRemote> AccessRemote { get; set; }
        public virtual DbSet<AccessRemoteArchive> AccessRemoteArchive { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Evidence> Evidence { get; set; }
        public virtual DbSet<EvidenceArchive> EvidenceArchive { get; set; }
        public virtual DbSet<EvidenceSolution> EvidenceSolution { get; set; }
        public virtual DbSet<HelperCode> HelperCode { get; set; }
        public virtual DbSet<HelperCodeArchive> HelperCodeArchive { get; set; }
        public virtual DbSet<LocalError> LocalError { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Problem> Problem { get; set; }
        public virtual DbSet<Requester> Requester { get; set; }
        public virtual DbSet<RequestMeans> RequestMeans { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<Solution> Solution { get; set; }
        public virtual DbSet<SolutionArchive> SolutionArchive { get; set; }
        public virtual DbSet<SolutionInformation> SolutionInformation { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SolutionHelperCode> SolutionHelperCode { get; set; }
        public virtual DbSet<Situation> Situation { get; set; }
        #endregion

        public static ApplicationDbContext Create()
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Configuração Usuário

            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.AccessesRemote)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.AccessRemoteArchives)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Activities)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Calendars)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Companies)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.EvidenceArchives)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Evidences)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.EvidenceSolutions)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.HelperCodeArchives)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.HelperCodes)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.LocalErrors)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Modules)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Problems)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Requesters)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.RequestMeanses)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Screenses)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.SolutionArchives)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.SolutionInformations)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Solutions)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Status)
                .WithRequired(c => c.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId)
                .WillCascadeOnDelete(false);

            #endregion

            #region Solução

            modelBuilder.Entity<Solution>()
                .HasMany(c => c.Solutions)
                .WithOptional(c => c.SolutionOld)
                .HasForeignKey(c => c.SolutionOldId);

            modelBuilder.Entity<Solution>()
                .HasMany(c => c.Solutions)
                .WithOptional(c => c.SolutionBase)
                .HasForeignKey(c => c.SolutionBaseId);

            #endregion

            #region Empresa/Requisitantes

            modelBuilder.Entity<Company>().HasMany(c => c.Requesters).WithRequired(c => c.Company).HasForeignKey(c => c.CompanyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Requester>().HasMany(c => c.Evidences).WithRequired(c => c.Requester).HasForeignKey(c => c.RequesterId).WillCascadeOnDelete(false);

            #endregion

            #region Problemas/Solução/Evidencia

            modelBuilder.Entity<Problem>().HasMany(c => c.Solutions).WithRequired(c => c.Problem).HasForeignKey(c => c.ProblemId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Problem>().HasMany(c => c.Evidences).WithRequired(c => c.Problem).HasForeignKey(c => c.ProblemId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Solution>().HasRequired(c => c.Situation).WithMany(c => c.Solutions).HasForeignKey(c => c.SituationId).WillCascadeOnDelete(false);

            #endregion

            #region Código ajuda

            modelBuilder.Entity<HelperCode>().HasMany(c => c.SolutionHelperCodes).WithRequired(c => c.HelperCode).HasForeignKey(c => c.HelperCodeId).WillCascadeOnDelete(false);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}