namespace Eventmaster.DAL.Entity
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public status? status { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ImagePath { get; set; }
        //public string Password { get; internal set; }
        public bool IsOrganizer { get; set; }
        public bool IsApproved { get; set; } = false;


        public User() 
        {
        }
        public User(string name, string bio, status? status, bool isOrganizer, bool isApproved, string? imagePath)
        {
            Name = name;
            Bio = bio;
            this.status = status;
            IsOrganizer = isOrganizer;
            IsApproved = isApproved;
            ImagePath = imagePath;
        }

    }
}
