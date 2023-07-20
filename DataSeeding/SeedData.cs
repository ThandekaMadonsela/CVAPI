using Bogus;
using CVAPI.Data;
using CVAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVAPI.DataSeeding
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

                await SeedStudentsAsync(dbContext);
                await SeedQualificationsAsync(dbContext);
                await SeedWorkExperienceAsync(dbContext);
                await SeedRefereesAsync(dbContext);

                // Perform other data seeding if needed
            }
        }

        private static async Task SeedStudentsAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Student.AnyAsync())
            {
                var studentFaker = new Faker<Student>()
                    .RuleFor(s => s.StudentId, f => Guid.NewGuid().ToString())
                    .RuleFor(s => s.IDNumber, f => f.Random.Replace("#####"))
                    .RuleFor(s => s.CareerObjective, f => f.Random.Words())
                    .RuleFor(s => s.Skills, f => f.Random.Words())
                    .RuleFor(s => s.Achievements, f => f.Random.Words())
                    .RuleFor(s => s.Interests, f => f.Random.Words())
                    .RuleFor(s => s.Address, f => f.Address.FullAddress());

                var students = studentFaker.Generate(3);

                dbContext.Student.AddRange(students);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedQualificationsAsync(ApplicationDbContext dbContext)
        {
            var students = await dbContext.Student.ToListAsync();

            var qualificationFaker = new Faker<Qualification>()
                .RuleFor(q => q.QualificationId, f => Guid.NewGuid().ToString())
                .RuleFor(q => q.Institution, f => f.Company.CompanyName())
                .RuleFor(q => q.Date, f => f.Date.Past().ToString("yyyy-MM-dd"))
                .RuleFor(q => q.StuQualification, f => f.Random.Word())
                .RuleFor(q => q.Subjects, f => f.Random.Words())
                .RuleFor(q => q.Majors, f => f.Random.Words())
                .RuleFor(q => q.Submajors, f => f.Random.Words())
                .RuleFor(q => q.Research, f => f.Random.Words());

            foreach (var student in students)
            {
                var qualifications = qualificationFaker.Generate(2);

                foreach (var qualification in qualifications)
                {
                    qualification.Student = student;
                }

                student.Qualifications = qualifications; // Add qualifications to the student's collection

                await dbContext.Qualification.AddRangeAsync(qualifications);
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedWorkExperienceAsync(ApplicationDbContext dbContext)
        {
            var students = await dbContext.Student.ToListAsync();

            var workExperienceFaker = new Faker<WorkExperience>()
                .RuleFor(w => w.WorkExperienceId, f => Guid.NewGuid().ToString())
                .RuleFor(w => w.Employer, f => f.Company.CompanyName())
                .RuleFor(w => w.Date, f => f.Date.Past().ToString("yyyy-MM-dd"))
                .RuleFor(w => w.JobTitle, f => f.Name.JobTitle())
                .RuleFor(w => w.TaskandResponsibilities, f => f.Lorem.Sentence());

            foreach (var student in students)
            {
                var workExperiences = workExperienceFaker.Generate(2);

                foreach (var workExperience in workExperiences)
                {
                    workExperience.Student = student;
                }

                student.WorkExperience = workExperiences; // Add work experiences to the student's collection

                await dbContext.WorkExperience.AddRangeAsync(workExperiences);
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedRefereesAsync(ApplicationDbContext dbContext)
        {
            var students = await dbContext.Student.ToListAsync();

            var refereeFaker = new Faker<Referee>()
                .RuleFor(r => r.RefereeId, f => Guid.NewGuid().ToString())
                .RuleFor(r => r.FullName, f => f.Name.FullName())
                .RuleFor(r => r.JobTitle, f => f.Name.JobTitle())
                .RuleFor(r => r.Institution, f => f.Company.CompanyName())
                .RuleFor(r => r.Cellphone, f => f.Phone.PhoneNumber("##########"))
                .RuleFor(r => r.Email, (f, r) => f.Internet.Email(r.FullName));

            foreach (var student in students)
            {
                var referees = refereeFaker.Generate(3);

                foreach (var referee in referees)
                {
                    referee.Student = student;
                }

                student.Referee = referees; // Add referees to the student's collection

                await dbContext.Referee.AddRangeAsync(referees);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
