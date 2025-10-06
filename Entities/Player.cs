namespace ChessRatingListApi.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Rating { get; set; }
        public string Federation { get; set; }

        public Player
        (
            int id,
            string name,
            string surname,
            int age,
            int rating,
            string federation
        )
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Rating = rating;
            Federation = federation;
        }

        public void AddRating(int rating) => Rating += rating;
        public void SubtractRating(int rating) => Rating -= rating;
    }
}
