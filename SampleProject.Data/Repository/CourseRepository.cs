﻿using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Data.Repository
{
    public class CourseRepository : Repository<CourseModel>, ICourseRepository
    {
        public StudentDbContext Context => _dbContext as StudentDbContext;
        public CourseRepository(StudentDbContext context) : base(context)
        {

        }
    }
}
