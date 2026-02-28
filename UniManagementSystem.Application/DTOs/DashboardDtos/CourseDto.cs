namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public int MaxDegree { get; set; } = 100;
        public int MinDegree { get; set; } = 50;
    }
}