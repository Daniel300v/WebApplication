using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class survay
    {
        public int Id { get; set; }
        public int PayRateing { get; set; }
        public int SatisfactionRating { get; set; }
        public string feedback { get; set; }

        public survay()
        {

        }

    }
}
