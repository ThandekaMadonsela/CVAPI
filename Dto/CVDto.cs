using CVAPI.Models;

namespace CVAPI.Dto
{
    public class CVDto
    {
        public string StudentId { get; set; }

        public string IDNumber { get; set; }

        public string? CareerObjective { get; set; }

        public string? Skills { get; set; }

        public string? Achievements { get; set; }

        public string? Interests { get; set; }

        public string? Address { get; set; }

        public ICollection<Qualification> Qualifications { get; set; }
        public ICollection<WorkExperience> WorkExperience { get; set; }
        public ICollection<Referee> Referee { get; set; }
    }
}
