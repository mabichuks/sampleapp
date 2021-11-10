using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Data.Repository
{
    public class EnrollmentRepository : Repository<EnrollmentModel>, IEnrollmentRepository
    {
        public StudentDbContext Context => _dbContext as StudentDbContext;
        public EnrollmentRepository(StudentDbContext context) : base(context)
        {

        }
    }
}
