namespace Rasta.ProjectManagment.Api.Models
{
    public class DateTimeResponseModel
    {
        public DateTime GregorianDate { get; set; }
        public string JalaliDate { get; set; }

        public int GregorianYear { get; set; }
        public int GregorianMonth { get; set; }
        public int GregorianDay { get; set;}

        public int JalaliYear { get; set; }
        public int JalaliMonth { get; set; }
        public int JalaliDay { get; set; }
    }
}
