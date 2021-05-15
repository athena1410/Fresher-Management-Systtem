using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Candidate> Candidates { get; }
        public DbSet<Class> Classes { get; }
        public DbSet<Location> Locations { get; }
        public DbSet<Channel> Channels { get; }
        public DbSet<EntryTest> EntryTests { get; }
        public DbSet<Faculty> Faculties { get; }
        public DbSet<Interview> Interviews { get; }
        public DbSet<Offer> Offers { get; }
        public DbSet<Trainee> Trainees { get; }
        public DbSet<University> Universities { get; }
        public DbSet<TraineeCandidateProfile> TraineeCandidateProfiles { get; }
    }
}
