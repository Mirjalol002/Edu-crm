using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCRM.Application.Models
{
    public class UpdateGroupModel
    {
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan LessonStartTime { get; set; }
        public TimeSpan LessonEndTime { get; set; }
    }
}
