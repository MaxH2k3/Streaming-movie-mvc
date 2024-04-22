namespace SMovie.Domain.Constants
{
    public class TaskConstant
    {
        public static class TransferMovie
        {
            public const string JobKey = "MovieRecordViewer";
            public const string Description = "Calculate sum of all movie a weakly. Transfer to another database and then reset the current database.";
        }
    }
}
