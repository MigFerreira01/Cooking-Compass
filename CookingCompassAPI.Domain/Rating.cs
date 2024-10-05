namespace CookingCompassAPI.Domain
{
    public class Rating
    {

        public int Id { get; set; }

        public int RecepiId { get; set; }

        public int Rate { get; set; }

        public DateTime SubmitDate { get; set; }

    }
}
