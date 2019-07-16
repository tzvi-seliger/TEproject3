using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        public int surveyId { get; set; }

        [Required]
        public string parkCode { get; set; }

        [Required]
        [EmailAddress]
        public string emailAddress { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string activityLevel { get; set; }
    }
}
