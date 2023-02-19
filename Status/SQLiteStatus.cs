namespace fullstack_1.Status
{
    public enum QueryStatus
    {
        ERROR,
        UNSUPORTED,
        INVALID_INPUT, 
        INVALID_ID,
        NOT_FOUND,
        SUCCESS
    }

    public enum ProcessingStatus
    {
        INSUFFICIENT_DATA_FOR_PROCESSING,
        NO_PROCESSING,
        SUCCESS
    }

    public class APIStatusOutput
    {
        public string status { get; set; }  
        public APIStatusOutput(string status)
        {
            this.status = status;
        }           
    }

}
