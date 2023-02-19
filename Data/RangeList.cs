namespace fullstack_1.Data
{

    public class RangeListData
    {
        int index { get; set; }
        int count { get; set; }
        int pos { get; set; }
        int offset { get; set; }
        public RangeListData() { }
    }

    public class RangeList<T>
    {
        private static List<T> list         = new List<T>();
        private static List<RangeListData> list_data = new List<RangeListData>();
        int list_index;
        
        public List<T> get_list()
        {
            //return list.GetRange(index, count);
            return new List<T>();
        }

        public void add(T data)
        {

        }
        public void remove_at(int index)
        {
            
        }

        public RangeList()
        {
            list_index = list_data.Count;
        }

    }
}
