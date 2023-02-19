namespace fullstack_1.Data
{
    public class ApplicationsData
    {
        public string year_month { get; set; }
        public int count { get; set; }  
        public ApplicationsData() {
            year_month = "";
            count = 0;
        }
        public ApplicationsData(string year_month, int count) { 
            this.year_month = year_month;
            this.count = count;
        }

    }
}
