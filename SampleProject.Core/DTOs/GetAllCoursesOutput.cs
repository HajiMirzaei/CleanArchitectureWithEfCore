using SampleProject.Core.Contracts;
using System;
using System.Collections.Generic;

namespace SampleProject.Core.DTOs
{
    public class GetAllCoursesOutput : ResponseMessage
    {
        public IEnumerable<CourseVM> Data { get; private set; }
        public GetAllCoursesOutput(bool success, IEnumerable<CourseVM> data, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
