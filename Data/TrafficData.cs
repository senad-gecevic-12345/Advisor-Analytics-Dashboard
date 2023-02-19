namespace fullstack_1.Data
{
    public class TrafficData
    {
        public string year_month { get; set; }
        public int count { get; set; }
        public TrafficData()
        {
            year_month = "";
            count = 0;
        }
        public TrafficData(string year_month, int count)
        {
            this.year_month = year_month;
            this.count = count;
        }
    }
}
