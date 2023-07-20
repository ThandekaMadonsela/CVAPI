using CVAPI.Data;
using CVAPI.Interfaces;
using CVAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace CVAPI.Repository
{
    public class CVRepository : ICVInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public CVRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Student GetCV(string StudentId)
        {
            var Student = _dbContext.Student.Where(x => x.StudentId == StudentId).FirstOrDefault();

            if (Student == null)
                return null;
            
            var Qualifications = _dbContext.Qualification.Where(q => q.Student.StudentId == StudentId).ToList();

            if (Qualifications == null)
                return null;

            var WorkExperience = _dbContext.WorkExperience.Where(q => q.Student.StudentId == StudentId).ToList();

            if (WorkExperience == null)
                return null;

            var Referee = _dbContext.Referee.Where(q => q.Student.StudentId == StudentId).ToList();

            if (Referee == null)
                return null;

            Student.Qualifications = Qualifications;
            Student.WorkExperience = WorkExperience;
            Student.Referee = Referee;

            return Student;

        }

        public bool studentExists(string StudentId)
        {
            return _dbContext.Student.Any(x => x.StudentId == StudentId);
        }
    }
}
