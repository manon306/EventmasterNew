namespace Eventmaster.DAL.DataBase
{
    // Make User public to match the accessibility of Context and its base class
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
            public DbSet<Event> Events { get; set; }
            public DbSet<Registrations> Registrations { get; set; }
            public DbSet<Attachments> Attachments { get; set; }
            public DbSet<SavedEvents> SavedEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ضبط علاقة الـ Registrations لمنع الـ Cycle والـ Shadow Properties
            builder.Entity<Registrations>(entity =>
            {
                entity.HasKey(r => r.RegId);

                // نربط الـ Property "Event" بالـ Foreign Key "EventId" بشكل صريح
                entity.HasOne(r => r.Event)
                    .WithMany()
                    .HasForeignKey(r => r.EventId)
                    .OnDelete(DeleteBehavior.NoAction);

                // نربط الـ Property "Participant" بالـ Foreign Key "ParticipantId" بشكل صريح
                entity.HasOne(r => r.Participant)
                    .WithMany()
                    .HasForeignKey(r => r.ParticipantId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<SavedEvents>(entity =>
            {
                entity.HasKey(s => s.SaveId);

                // هنا لازم تضيف Navigation Properties في كلاس SavedEvents لو مش موجودة 
                // أو تربطها بنفس الطريقة دي لو موجودة:
                entity.HasOne(s => s.Participant)
                    .WithMany()
                    .HasForeignKey(s => s.ParticipantId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(s => s.Event)
                    .WithMany()
                    .HasForeignKey(s => s.EventId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // تأكد من ضبط الـ Attachment
            builder.Entity<Attachments>().HasKey(a => a.FileId);
        }
    }
}
