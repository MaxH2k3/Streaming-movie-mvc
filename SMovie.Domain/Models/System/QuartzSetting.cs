namespace SMovie.Domain.Models
{
    public class QuartzSetting
    {
        public string SchedulerId { get; set; } = null!;
        public string SchedulerName { get; set; } = null!;
        public int MaxConcurrency { get; set; }
        public int MaxBatchSize { get; set; }
        public int MisfireThreshold { get; set; }
    }
}
