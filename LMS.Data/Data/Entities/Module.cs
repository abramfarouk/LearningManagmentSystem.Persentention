namespace LMS.Data.Data.Entities
{
    public class Module : IBaseEntity //Organize Course 
    {
        //Example
        // Module: "Introduction to Programming"
        //  Lesson 1: "What is Programming?"
        //   Lesson 2: "Understanding Programming Languages"
        //    Lesson 3: "Your First Program"
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Lesson>? Lessons { get; set; } = new HashSet<Lesson>();

        public int? CourseId { get; set; }
        public virtual Course? Course { get; set; } = null!;

    }
}
