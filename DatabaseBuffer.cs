using fullstack_1.Data;
namespace fullstack_1
{


    public class Exception_404 : Exception
    {
    }
    public class Exception_400 : Exception
    {
    }

    public abstract class IMetaData
    {
        //Type DataType { get; }
        // here private... wtf
        public List<object> list { get; }
    }
    public class IMetaData<T> : IMetaData
    {
        public new List<T> list { get; }
        //public TypeId typeid;
        /*
        public T data_member;
        public Type type;
        public List<T> data;
        */
    }
    

    public class SingleBuffer<T>
    {
        public List<T> buffer;
    }

    internal class DatabaseBuffer
    {
        private static DatabaseBuffer instance = null;
        private static readonly object padlock = new object();
        public List<IMetaData> data;

        // these should throw custom exceptions and whatnot
        public void get_object_by_id<T>(int id)
        {

            for(int i = 0; i < data.Count(); ++i)
            {
                 
            }

            throw new Exception_404();
            throw new Exception_400();

            data.Add(new IMetaData<User>());
            //List[0]; 
            //return new T();
        }
        public void get_buffer<T>()
        {

        }
        public static DatabaseBuffer Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new DatabaseBuffer();
                    }
                    return instance;
                }
            }
        }
        public DatabaseBuffer()
        {

        }
    }

    // need some wrapper or something?
    // or just define lambdas and etc
    public class DatabaseBufferManager
    {

    }

    // make singleton?
    public class DatabaseBufferMain<T>
    {
        // or list of list<t>
        List<T> buffer;
        private static DatabaseBufferMain<T> instance = null;
        private static readonly object padlock = new object();

        // get data from database. 
        // update database adds and removes
        // failcheck

        public void dummy_function()
        {

        }

        // should perhaps be more database oriented or something. for example if only wants the addresses
        public static DatabaseBufferMain<T> Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new DatabaseBufferMain<T>();
                    }
                    return instance;
                }
            }
        }

    }

    // should use interfaces for the data?
    public class DatabaseBufferHelper<T>{
        private readonly int accessor;

        void add_new_object(T obj)
        {
            DatabaseBufferMain<T>.Instance.dummy_function();
        }
        void remove_object(T obj)
        {

        }
        
        void get_object()
        {
            //Type type = DatabaseBuffer.Instance.data[accessor];
            //List<T> list = DatabaseBuffer.Instance.data[accessor].list;
        }
        void get_buffer() 
        { 
            
        }

        DatabaseBufferHelper()
        {
            DatabaseBuffer.Instance.data.Add(new IMetaData<T>());
            accessor = DatabaseBuffer.Instance.data.Count()-1;
        }
    }

}
