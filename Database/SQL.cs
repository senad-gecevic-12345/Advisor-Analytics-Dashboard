using Microsoft.Data.SqlClient;
using fullstack_1.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using fullstack_1.Status;

namespace fullstack_1.Database
{
    public static class DB
    {
        public static string string_null_check(in SQLiteDataReader reader, int number)
        {
            if (!reader.IsDBNull(number))
                return reader.GetString(number);
            return string.Empty;
        }
        // crash. probably db not updated or wrong reader thing. string for int or something/int_n
        public static int int_null_check(in SQLiteDataReader reader, int number)
        {
            if (!reader.IsDBNull(number))
                return reader.GetInt32(number);
            return -1;
        }       
    }
    
    public class SQLiteDB
    {
        private static readonly string sql_folder_location = "C:\\Users\\meme_\\source\\repos\\fullstack_1\\fullstack_1\\SQLite\\";
        private static readonly string connection_string = @"URI=file:C:\Users\meme_\source\repos\fullstack_1\fullstack_1\SQLite\DB.db";
        static class Query<T> where T : new()
        {
            internal static QueryStatus post_query(string query, Action<SQLiteParameterCollection> parameters)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);
                try
                {
                    command.ExecuteNonQuery();
                    status = QueryStatus.SUCCESS;
                }
                catch (SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();
                return status;
            }
            internal static Tuple<QueryStatus, List<T>>read_query(string query, Func<SQLiteDataReader, T> func)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                List<T> list = new List<T>();
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(func(reader));    
                    }
                    status = QueryStatus.SUCCESS;
                }
                catch(SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return Tuple.Create(status, list);
            }
            internal static Tuple<QueryStatus, List<T>>read_query(string query, Action<SQLiteParameterCollection> parameters,Func<SQLiteDataReader, T> func)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);

                List<T> list = new List<T>();
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(func(reader));    
                    }
                    status = QueryStatus.SUCCESS;
                }
                catch(SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return Tuple.Create(status, list);
            }
            internal static QueryStatus read_populate_query(string query, Action<SQLiteParameterCollection> parameters, Func<SQLiteDataReader, T> func, ref List<T> list)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(func(reader));    
                    }
                    status = QueryStatus.SUCCESS;
                }
                catch(SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return status;
            }
            internal static QueryStatus read_populate_query(string query, Func<SQLiteDataReader, T> func, ref List<T> list)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(func(reader));    
                    }
                    status = QueryStatus.SUCCESS;
                }
                catch(SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return status;
            }
            internal static Tuple<QueryStatus, T> lookup_query(string query, Action<SQLiteParameterCollection> parameters, Func<SQLiteDataReader, T> func) 
            {
                QueryStatus status;
                T value;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);
                using var reader = command.ExecuteReader(); 
                try {
                    if (reader.Read())
                    {
                        value = func(reader);
                        status = QueryStatus.SUCCESS;
                    }
                    else
                    {
                        status = QueryStatus.NOT_FOUND;
                        value = new T();
                        //value = default(T);
                    }
                }
                catch(SQLiteException) {
                    status = QueryStatus.ERROR;
                    //value = default(T);
                    value = new T();
                }
                connection.Close();
                return Tuple.Create(status, value);
            }
            internal static QueryStatus delete_query(string query, Action<SQLiteParameterCollection> parameters)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);
                try
                {
                    command.ExecuteNonQuery();
                    status = QueryStatus.SUCCESS;
                }
                catch (SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return status;
            }
            internal static QueryStatus has_value_query(string query, Action<SQLiteParameterCollection> parameters)
            {
                QueryStatus status;
                using var connection = Instance.CreateConnection();
                using var command = new SQLiteCommand(query, connection);
                parameters(command.Parameters);
                try
                {
                    // els£
                    int rows = Convert.ToInt32(command.ExecuteScalar());
                    if (rows > 0) status = QueryStatus.SUCCESS;
                    else status = QueryStatus.NOT_FOUND;
                }
                catch (SQLiteException)
                {
                    status = QueryStatus.ERROR;
                }
                connection.Close();

                return status;
            }
            class Parse<Item> where Item : new()
            {
                internal static QueryStatus parse_post(string query, Func<Item, Item> lambda, Action<Item> post, List<Item> list)
                {
                    foreach(Item item in list)
                    {
                        post(lambda(item)); 
                    }

                    return QueryStatus.ERROR;
                }
            }
            
            /*
            internal static SQLiteStatus update_query(string query, Action<SQLiteParameterCollection> parameters)
            {

            }
            */
        }
        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection db_connection = new SQLiteConnection(connection_string);
            try
            {
                db_connection.Open();
            }
            catch (Exception)
            {

            }
            return db_connection;
        }
        private static SQLiteDB instance = null;
        private static readonly object padlock = new object();

        // probably need to parse this too
        // use dto or something
        internal class SQLiteDBContactNumber
        {
            internal static QueryStatus parse_post(string id, List<ContactNumber>? list)
            {
                if (list == null)
                    return QueryStatus.SUCCESS;
                foreach(var item in list)
                {
                    QueryStatus status = Query<ContactNumber>.post_query("INSERT INTO CONTACTNUMBER(Id, Type, Value) " +
                        "VALUES(@Id, @Type, @Value);",
                        p =>
                        {
                            p.AddWithValue("@Id", id);
                            p.AddWithValue("@Type", item.type);
                            p.AddWithValue("@Value", item.value);
                        }
                    );
                    if (status == QueryStatus.ERROR)
                        return QueryStatus.ERROR;
                }
                return QueryStatus.SUCCESS;
            }
            internal static Tuple<QueryStatus, List<ContactNumber>> get(string id)
            {
                return Query<ContactNumber>.read_query("SELECT Id, Type, Value FROM CONTACTNUMBER WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new ContactNumber(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1)
                            );
                    });
            }
            internal static QueryStatus populate(string id, ref List<ContactNumber> target)
            {
                return Query<ContactNumber>.read_populate_query("SELECT Id, Type, Value FROM CONTACTNUMBER WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new ContactNumber(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1)
                            );
                    }, 
                    ref target
                    );
            }
        }
        // extension_number is not unique, so cannot be foreign key
        // instead should let extension have a unique primary key, as well as a foreign key. 
        // what about duplicates?
        internal class SQLiteDBExtension
        {
            internal static QueryStatus create_extension_if_not_created(string id, string extension_number)
            {
                return Query<UserExtensions>.post_query("INSERT OR REPLACE INTO USEREXTENSIONS(Id, ExtensionNumber) WHERE ExtensionNumber = @ExtensionNumber " +
                                                        "VALUES(@Id, @ExtensionNumber);",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                        p.AddWithValue("@ExtensionNumber", extension_number);
                    }
                    );
            }
        }
        // check that user id is not already created. or for dids, if they are shared, then should not add if already is there
        internal class SQLiteDBDID
        {
            private static QueryStatus post(Did did)
            {
                return Query<Did>.post_query("INSERT INTO DID(ExtensionNumber, CustomTag, PhoneNumber) " +
                                             "VALUES(@ExtensionNumber, @CustomTag, @PhoneNumber);",
                    p =>
                    {
                         p.AddWithValue("@ExtensionNumber", did.extension);
                         p.AddWithValue("@CustomTag", did.custom_tag);
                         p.AddWithValue("@PhoneNumber", did.phone_number);
                    }
                );
            }
            public static QueryStatus populate(string extension_number, ref List<Did> target)
            {
                return Query<Did>.read_populate_query("SELECT ExtensionNumber, CustomTag, PhoneNumber FROM DID WHERE ExtensionNumber = @ExtensionNumber",
                    p =>
                    {
                        p.AddWithValue("@ExtensionNumber", extension_number);
                    },
                    reader =>
                    {
                        return new Did(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2)
                            );
                    }, 
                    ref target
                );
            }

            public static Tuple<QueryStatus, List<Did>> get(string extension_number)
            {
                return Query<Did>.read_query("SELECT ExtensionNumber, CustomTag, PhoneNumber FROM DID WHERE ExtensionNumber = @ExtensionNumber",
                    p =>
                    {
                        p.AddWithValue("@ExtensionNumber", extension_number);
                    },
                    reader =>
                    {
                        return new Did(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2)
                            );
                    }
                );
            }
        }
        internal class SQLiteDBUserExtension
        {
            private static readonly string post_query;

            // populate too probably
            // or no ?
            internal static QueryStatus parse_post(string id, List<UserExtensions>? extensions)
            {
                if (extensions == null)
                    return QueryStatus.SUCCESS;
                // step 1. for each extension lookup extension_number. if not created, create it
                // step 2. post a userextension for each extension
                // handle exception where duplicated, if that is relevant
                // step 3. post dids associated with extension number
                foreach (var extension in extensions)
                {
                    QueryStatus extension_status = SQLiteDBExtension.create_extension_if_not_created(id, extension.extension_number);
                    if (extension_status != QueryStatus.SUCCESS)
                    {

                        return QueryStatus.ERROR;
                    }

                    if (extension.dids == null) continue;
                    foreach (var did in extension.dids)
                    {
                        QueryStatus did_status = Query<Did>.post_query("INSERT INTO DID(ExtensionNumber, CustomTag, PhoneNumber) " +
                                                                      "VALUES(@ExtensionNumber, @CustomTag, @PhoneNumber);",
                                p =>
                                {
                                    p.AddWithValue("@ExtensionNumber", extension.extension_number);
                                    p.AddWithValue("@CustomTag", did.custom_tag);
                                    p.AddWithValue("@PhoneNumber", did.phone_number);
                                }
                        );
                        if (did_status != QueryStatus.SUCCESS)
                        {
                            return QueryStatus.ERROR;
                        }

                    }
                }
                return QueryStatus.SUCCESS;
            }

            // have both user extension and also extension
            public static QueryStatus populate(string id, ref List<UserExtensions> target)
            {
                    return Query<UserExtensions>.read_populate_query("SELECT Id, ExtensionNumber FROM USEREXTENSION WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        var item = new UserExtensions(
                            DB.string_null_check(in reader, 0)); 
                        var did_query = SQLiteDBDID.get(item.extension_number);
                        if (did_query.Item1 == QueryStatus.SUCCESS)
                            item.dids = did_query.Item2;
                        return item;
                    },
                    ref target
                    );
 
            }
            public static Tuple<QueryStatus, List<UserExtensions>> get(string id)
            {
                    return Query<UserExtensions>.read_query("SELECT Id, ExtensionNumber FROM USEREXTENSION WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        var item = new UserExtensions(
                            DB.string_null_check(in reader, 0)); 
                        var did_query = SQLiteDBDID.get(item.extension_number);
                        if (did_query.Item1 == QueryStatus.SUCCESS)
                            item.dids = did_query.Item2;
                        return item;
                    }
                    );
 
            }
        }

        internal class SQLiteDBPersonId
        {
            internal static Tuple<QueryStatus, PersonId> get(string id)
            {
                return Query<PersonId>.lookup_query("SELECT Id, TableName FROM PERSONID WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new PersonId(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1));
                    }
                    );
            }
            internal static QueryStatus create_user(string id, string table)
            {
                // need to check that the values are valid
                // check here, and not at the post_query
                return Query<PersonId>.post_query("INSERT INTO PERSONID(Id, TableName) " +
                                                  "VALUES(@Id, @TableName);",
                                                  p =>
                                                  {
                                                      p.AddWithValue("@Id", id);
                                                      p.AddWithValue("@TableName", table);
                                                  });

                
            }
            internal static QueryStatus delete_user_cascade(string id)
            {
                return Query<PersonId>.delete_query("DELETE FROM PERSONID WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    });
            }

            internal static QueryStatus delete_user_addresses(string id)
            {
                return Query<Address>.delete_query("DELETE FROM ADDRESS WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    });
                
            }

        }
        public class SQLiteDBUser
        {
            public static Tuple<QueryStatus, Table, User> post(UserDTOPost dto)
            {
                // create user id first
                string id = Guid.NewGuid().ToString();
                User user = new User(id, dto);
                string query;
                Table table;
                switch (user.type.type)
                {
                    case "Advisor":
                        {
                            table = Table.ADVISOR;
                            query = "INSERT INTO ADVISOR(Id, FirstName, LastName, LoginName, Level, Type, Email) " +
                                     "VALUES(@Id, @FirstName, @LastName, @LoginName, @Level, @Type, @Email);";
                            break;
                        }
                    case "Founder":
                        {
                            query = "INSERT INTO FOUNDER(Id, FirstName, LastName, LoginName, Level, Type, Email) " +
                                    "VALUES(@Id, @FirstName, @LastName, @LoginName, @Level, @Type, @Email);";
                            table = Table.FOUNDER;
                            break;
                        }
                    case "User":
                        {
                            query = "INSERT INTO USER(Id, FirstName, LastName, LoginName, Level, Type, Email) " +
                                    "VALUES(@Id, @FirstName, @LastName, @LoginName, @Level, @Type, @Email);";
                            table = Table.USER;
                            break;
                        }
                    default:
                        {
                            return new Tuple<QueryStatus, Table, User>(QueryStatus.UNSUPORTED, Table.ERROR, user);
                        }
                }
                SQLiteDBPersonId.create_user(user.id, user.type.type.ToUpper());
                QueryStatus status;
                var has_type = Query<User>.has_value_query("SELECT count(*) FROM USERTYPE WHERE Type = @Type",
                    p =>
                    {
                        p.AddWithValue("@Type", user.type.type);
                    }
                    );
                var has_level = Query<User>.has_value_query("SELECT count(*) FROM USERLEVEL WHERE Level = @Level",
                    p =>
                    {
                        p.AddWithValue("@Level", user.level.level);
                    }
                    );
                if(has_type != QueryStatus.SUCCESS || has_level != QueryStatus.SUCCESS)
                {
                    return Tuple.Create(QueryStatus.INVALID_INPUT, Table.ERROR, user);
                }
                
                

                var post_status = Query<User>.post_query(query,
                                                   p =>
                                                   {
                                                       p.AddWithValue("@Id", user.id);
                                                       p.AddWithValue("@FirstName", user.first_name);
                                                       p.AddWithValue("@LastName", user.last_name);
                                                       p.AddWithValue("@LoginName", user.login_name);
                                                       p.AddWithValue("@Level", user.level.level);
                                                       p.AddWithValue("@Type", user.type.type);
                                                       p.AddWithValue("@Email", user.email);
                                                   });
                QueryStatus contact_number_post_status = SQLiteDBContactNumber.parse_post(user.id, user.contact_number);
                QueryStatus address_post_status = SQLiteDBAddress.parse_post(user.id, user.addresses);
                QueryStatus user_extensions_post_status = SQLiteDBUserExtension.parse_post(user.id, user.extensions);
                
                return new Tuple<QueryStatus, Table, User>(post_status, table, user);
            }
            public static QueryStatus delete(string id)
            {
                return Query<User>.delete_query("DELETE FROM PERSONID WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                });
            }












            public static Tuple<QueryStatus, List<User>> get()
            {
                // also address
                // and other stuff
                List<User> list = new List<User>();
                Query<User>.read_populate_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM USER",
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                          User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses, 
                            contact_numbers, 
                            user_extension
                            );

                        var addresses_status = SQLiteDBAddress.populate(usr.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(usr.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(usr.id, ref user_extension);
                        
                        return usr;                      

                    },
                    ref list
                    );
                Query<User>.read_populate_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR",
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                          User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses, 
                            contact_numbers, 
                            user_extension
                            );

                        var addresses_status = SQLiteDBAddress.populate(usr.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(usr.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(usr.id, ref user_extension);
                        
                        return usr;                      
  
                    },
                    ref list
                    );
                Query<User>.read_populate_query(
                    "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER",
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                          User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses, 
                            contact_numbers, 
                            user_extension
                            );

                        var addresses_status = SQLiteDBAddress.populate(usr.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(usr.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(usr.id, ref user_extension);
                        
                        return usr;                      
                    },
                    ref list
                    );
                return Tuple.Create(QueryStatus.SUCCESS, list);

            }
            public static Tuple<QueryStatus, User> lookup(string id)
            {
                string query_string;
                var person_id_query = SQLiteDBPersonId.get(id);
                if (person_id_query.Item1 != QueryStatus.SUCCESS)
                    return Tuple.Create(person_id_query.Item1, new User());
                switch (person_id_query.Item2.table_name)
                {
                    case "ADVISOR":
                        query_string = "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR WHERE Id = @Id;";
                        break;
                    case "FOUNDER":
                        query_string = "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER WHERE Id = @Id;";
                        break;
                    case "USER":
                        query_string = "SELECT Id, FirstName, LastName, LoginName, Level, Type FROM USER WHERE Id = @Id;";
                        break;
                    default:
                        return Tuple.Create(QueryStatus.UNSUPORTED, new User());
                        break;
                }

                // still need to populate it
                return Query<User>.lookup_query(query_string,
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                        User usr = new User(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses, 
                            contact_numbers,
                            user_extension
                            );
                        var addresses_status = SQLiteDBAddress.populate(usr.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(usr.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(usr.id, ref user_extension);
                        return usr;
                    }
                    );
            }
        }
        internal class SQLiteDBAddress
        {
            static public Tuple<QueryStatus, List<Address>> get()
            {
                return Query<Address>.read_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS",
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    }
                    );
            }
            static internal QueryStatus populate(string id, ref List<Address> target)
            {
                return Query<Address>.read_populate_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    },
                    ref target
                    );
            }
            static internal Tuple<QueryStatus, List<Address>> get_by_address_id(string id)
            {
                return Query<Address>.read_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS WHERE AddressId = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    }
                    );
            }
            static internal Tuple<QueryStatus, List<Address>> get_by_user_id(string id)
            {
                return Query<Address>.read_query("SELECT Street, Number, City, Country, PostCode FROM ADDRESS WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Address(
                            DB.string_null_check(in reader, 0),
                            DB.int_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4));
                    }
                    );
            }

            // what about with parameters?
            // now need a dto
            public static QueryStatus post(AddressDTOPOST dto)
            {
                string id = dto.id;
                Address address = new Address(dto);
                string address_id = Guid.NewGuid().ToString();

                return Query<Address>.post_query("INSERT INTO ADDRESS(Id, Street, Number, City, Country, PostCode) " +
                                                 "VALUES(@Id, @Street, @Number, @City, @Country, @PostCode);",
                                                 p =>
                                                 {
                                                     p.AddWithValue("@Id", id);
                                                     p.AddWithValue("@Street", address.street);

                                                     p.AddWithValue("@Number", address.number);
                                                     p.AddWithValue("@City", address.city);
                                                     p.AddWithValue("@Country", address.country);
                                                     p.AddWithValue("@PostCode", address.postcode);
                                                 }
                                                 );
                
            }
            // remove id from address
            internal static QueryStatus parse_post(string id, List<Address>? list)
            {
                if (list == null)
                    return QueryStatus.SUCCESS;
                foreach(var address in list)
                {
                    QueryStatus status = Query<ContactNumber>.post_query(
                                                 "INSERT INTO ADDRESS(Id, Street, Number, City, Country, PostCode) " +
                                                 "VALUES(@Id, @Street, @Number, @City, @Country, @PostCode);",

                        p =>
                        {
                            p.AddWithValue("@Id", id);
                            p.AddWithValue("@Street", address.street);
                            p.AddWithValue("@Number", address.number);
                            p.AddWithValue("@City", address.city);
                            p.AddWithValue("@Country", address.country);
                            p.AddWithValue("@PostCode", address.postcode);

                        }
                    );
                    if (status == QueryStatus.ERROR)
                        return QueryStatus.ERROR;
                }
                return QueryStatus.SUCCESS;





            }
        }
        public class SQLiteDBAdvisor
        {
            public static Tuple<QueryStatus, List<Advisor>> get()
            {
                var tuple = Query<Advisor>.read_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR;",  
                    reader => {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();
                        Advisor advisor = new Advisor(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses, 
                            contact_numbers, 
                            user_extension
                            );
                        var addresses_status = SQLiteDBAddress.populate(advisor.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(advisor.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(advisor.id, ref user_extension);

                        return advisor;
                    });
                return tuple;
            }
            static public QueryStatus delete(string id)
            {
                return SQLiteDBPersonId.delete_user_cascade(id);
            }
            static public Tuple<QueryStatus, Advisor> lookup(string id)
            {
                return Query<Advisor>.lookup_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM ADVISOR WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                        var advisor = new Advisor(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses,
                            contact_numbers, 
                            user_extension
                            );
                        var addresses_status = SQLiteDBAddress.populate(advisor.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(advisor.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(advisor.id, ref user_extension);
                        return advisor;
                    }
                    );


            }

            static public Tuple<QueryStatus, List<Address>> get_addresses(string id)
            {
                return SQLiteDBAddress.get_by_user_id(id);
            }
        }

        public class SQLiteDBFounder
        {
            static public Tuple<QueryStatus, List<Founder>> get()
            {
                // also return the other stuff, companies, addresses, etc, etc, 
                var tuple = Query<Founder>.read_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER;",  
                    reader => {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                        Founder founder = new Founder(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses,
                            contact_numbers,
                            user_extension
                            );
                        var addresses_status = SQLiteDBAddress.populate(founder.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(founder.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(founder.id, ref user_extension);
                        return founder;

                    });
                return tuple;
            }
            static public Tuple<QueryStatus, Founder> lookup(string id)
            {
                return Query<Founder>.lookup_query("SELECT Id, FirstName, LastName, LoginName, Level, Type FROM FOUNDER WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        List<Address> addresses             = new();
                        List<ContactNumber> contact_numbers = new();
                        List<UserExtensions> user_extension = new();

                        Founder founder = new Founder(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            new Level(DB.string_null_check(in reader, 4)),
                            new UserType(DB.string_null_check(in reader, 5)),
                            addresses,
                            contact_numbers,
                            user_extension
                            );
                        var addresses_status = SQLiteDBAddress.populate(founder.id, ref addresses);
                        var contact_numbers_status = SQLiteDBContactNumber.populate(founder.id, ref contact_numbers);
                        var user_extension_status = SQLiteDBUserExtension.populate(founder.id, ref user_extension);
                        return founder;
                    }
                    );

            }
            static public QueryStatus delete(string id)
            {
                return SQLiteDBPersonId.delete_user_cascade(id);
            }
            static public Tuple<QueryStatus, List<Address>> get_addresses(string id)
            {
                return SQLiteDBAddress.get_by_user_id(id);
            }
        }
        internal class SQLiteDBCompany
        {
            public static Tuple<QueryStatus, List<Company>> get_companies()
            {
                return Query<Company>.read_query("SELECT Id, Name, Industry, Location FROM COMPANY",
                    reader =>
                    {
                        return new Company(
                        DB.string_null_check(reader, 0),
                        DB.string_null_check(reader, 1),
                        DB.string_null_check(reader, 2),
                        DB.string_null_check(reader, 3));
                    });
            }
            internal static Tuple<QueryStatus, List<Company>> get_companies_for_founder(string id)
            {
                QueryStatus status;
                List<Company> list = new List<Company>();
                var (query_status, founder_companies) = Query<int>.read_query("SELECT CompanyId FROM FOUNDERCOMPANIES WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return DB.int_null_check(in reader, 0); 
                    });
                if(query_status != QueryStatus.SUCCESS)
                {
                    return Tuple.Create(QueryStatus.ERROR, list);
                }
                status = QueryStatus.SUCCESS;
                foreach(int x in founder_companies)
                {
                    var tuple = Query<Company>.lookup_query("SELECT Id, Name, Industry, Location FROM COMPANY WHERE Id = @Id",
                        p =>
                        {
                            p.AddWithValue("@Id", x);
                        },
                        reader =>
                        {
                            return new Company(
                                DB.string_null_check(in reader, 0),
                                DB.string_null_check(in reader, 1),
                                DB.string_null_check(in reader, 2),
                                DB.string_null_check(in reader, 3)
                                );
                        }
                        );
                    if(tuple.Item1 != QueryStatus.SUCCESS)
                    {
                        status = QueryStatus.ERROR;
                        break;
                    }

                    list.Add(tuple.Item2);
                }
                return Tuple.Create(status, list);
            }

            public static QueryStatus post(Company company)
            {
                return Query<Company>.post_query(
                 "INSERT INTO COMPANY(Id, Name, Industry, Location) " +
                 "VALUES(@Id, @Name, @Industry, @Location);", 
                 p =>
                 {
                     p.AddWithValue("@Id", company.id);
                     p.AddWithValue("@Name", company.name);
                     p.AddWithValue("@Industry", company.industry);
                     p.AddWithValue("@Location", company.location);
                 });
            }
            public static QueryStatus delete(string id)
            {
                return Query<Company>.delete_query("DELETE FROM COMPANY WHERE Id = @Id",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    }
                    );
            }

        }
        internal class SQLiteDBEvent{

            public static Tuple<QueryStatus, List<Event>> get_events_for_calendar_id(string calendar_id)
            {
                // 
                // let reader be a delegate probably
                return Query<Event>.read_query(
                    "SELECT(EventId, CalendarId, EventDate, StartTime, EndTime, Title, CreatedAt, UpdatedAt, Status) " +
                    "FROM EVENT WHERE CalendarId = @CalendarId;",
                    p =>
                    {
                        p.AddWithValue("@CalendarId", calendar_id);
                    },
                    reader =>
                    {
                        return new Event(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.int_null_check(in reader, 6),
                            DB.int_null_check(in reader, 7),
                            DB.string_null_check(in reader, 8)
                            );
                    });
            }
            private static QueryStatus status_validity_query(string status)
            {
                QueryStatus has_value = Query<Event>.has_value_query("SELECT count(*) FROM EVENTSTATUS WHERE Status = @Status;",
                    p =>
                    {
                        p.AddWithValue("@Status", status);
                    }
                    );
                if(has_value != QueryStatus.SUCCESS)
                {
                    return QueryStatus.ERROR;
                }
                else
                {
                    return QueryStatus.SUCCESS;
                }
            }
            public static QueryStatus post_event(Event evt)
            {
                if(status_validity_query(evt.detail.status) != QueryStatus.SUCCESS)
                {
                    return QueryStatus.ERROR;
                }
                return Query<Event>.post_query("INSERT INTO EVENT(EventId, CalendarId, EventDate, StartTime, EndTime, Title, CreatedAt, UpdatedAt, Status) " +
                                               "VALUES(@EventId, @CalendarId, @EventDate, @StartTime, @EndTime, @Title, @CreatedAt, @UpdatedAt, @Status);",
                                               p =>
                                               {
                                                   p.AddWithValue("@EventId", evt.id);
                                                   p.AddWithValue("@CalendarId", evt.calendar_id);
                                                   p.AddWithValue("@EventDate", evt.detail.date);
                                                   p.AddWithValue("@StartTime", evt.detail.start_time);
                                                   p.AddWithValue("@EndTime", evt.detail.end_time);
                                                   p.AddWithValue("@Title", evt.detail.title);
                                                   p.AddWithValue("@CreatedAt", evt.detail.created_at);
                                                   p.AddWithValue("@UpdatedAt", evt.detail.updated_at);
                                                   p.AddWithValue("@Status", evt.detail.status);
                                               });
            }
            public static QueryStatus delete_event(string id)
            {
                return Query<Event>.delete_query("DELETE FROM Event WHERE EventId = @EventId",
                    p =>
                    {
                        p.AddWithValue("@EventId", id);
                    });
            }
            // may need DTO for event
            public static QueryStatus update_event(string event_id, string calendar_id, EventRequest evt)
            {
                // check if has value or not. don't create if there is none
                QueryStatus has_value = Query<Event>.has_value_query("SELECT count(*) FROM EVENT WHERE EventId = @EventId AND CalendarId = @CalendarId;",
                    p =>
                    {
                        p.AddWithValue("@EventId", event_id);
                        p.AddWithValue("@CalendarId", calendar_id);
                    }
                    );
                if(has_value != QueryStatus.SUCCESS)
                {
                    return QueryStatus.ERROR;
                }
                if(status_validity_query(evt.status) != QueryStatus.SUCCESS)
                {
                    return QueryStatus.ERROR;
                }
                return Query<Event>.post_query(
                    "INSERT OR REPLACE INTO EVENT(EventDate, StartTime, EndTime, CreatedAt, UpdatedAt, Status) WHERE EventId = @EventId " + 
                    "VALUES(@EventDate, @StartTime, @EndTime, @CreatedAt, @UpdatedAt, @Status); ",
                    p =>
                    {
                        p.AddWithValue("@EventDate", evt.date);
                        p.AddWithValue("@StartTime", evt.start_time);
                        p.AddWithValue("@EndTime", evt.end_time);
                        p.AddWithValue("@Title", evt.title);
                        p.AddWithValue("@CreatedAt", evt.created_at);
                        p.AddWithValue("@UpdatedAt", evt.updated_at);
                        p.AddWithValue("@Status", evt.status);
                    }
                    );
            }
        }

        public class SQLiteDBCalendar
        {

            public static Tuple<QueryStatus, Calendar> get(string id)
            {
                return Query<Calendar>.lookup_query("SELECT Id, Name, EventCount FROM CALENDAR WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader => {
                    return new Calendar( 
                        DB.string_null_check(in reader, 0),
                        DB.string_null_check(in reader, 1),
                        DB.int_null_check(in reader, 2)
                    );
                });
            }
            public static Tuple<QueryStatus, List<Calendar>> get_calendars()
            {
                return Query<Calendar>.read_query("SELECT ID, Name, EventCount FROM CALENDAR;",  reader => {
                    return (new Calendar(
                                DB.string_null_check(in reader, 0),
                                DB.string_null_check(in reader, 1),
                                DB.int_null_check(in reader, 2)
                            ));
                });
            } 
            public static QueryStatus post(Calendar calendar)
            {

                return Query<Calendar>.post_query(
                    "INSERT INTO CALENDAR(Id, Name, EventCount) " +
                    "VALUES(@Id, @Name, @EventCount);", p =>
                    {
                        p.AddWithValue("@Id", calendar.id);
                        p.AddWithValue("@Name", calendar.calendar_name);
                        p.AddWithValue("@EventCount", calendar.event_count);
                    });
            }
            public static QueryStatus delete(string id)
            {
                return Query<Calendar>.delete_query("DELETE FROM CALENDAR WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    });
            }
            public static Tuple<QueryStatus, Calendar> lookup(string id)
            {
                return Query<Calendar>.lookup_query("SELECT Id, Name, EventCount FROM CALENDAR WHERE Id = @Id;",
                    p =>
                    {
                        p.AddWithValue("@Id", id);
                    },
                    reader =>
                    {
                        return new Calendar(DB.string_null_check(in reader, 0),
                                            DB.string_null_check(in reader, 1),
                                            DB.int_null_check(in reader, 2)
                                            );
                    }
                    );
            }


       }

        public class SQLiteDBGraph
        {
            public static Tuple<QueryStatus, List<TrafficData>> get_traffic()
            {
                return Query<TrafficData>.read_query("SELECT YearMonth, VisitCount FROM SITETRAFFIC;",  reader => {
                    return (new TrafficData(
                                DB.string_null_check(in reader, 0),
                                DB.int_null_check(in reader, 1)
                            ));
                });
            }
            public static Tuple<QueryStatus, List<ApplicationsData>> get_applications()
            {
                return Query<ApplicationsData>.read_query("SELECT YearMonth, ApplicationsCount FROM APPLICATIONS;",  reader => {
                    return (new ApplicationsData(
                                DB.string_null_check(in reader, 0),
                                DB.int_null_check(in reader, 1)
                            ));
                });
            }
        }

        public class SQLiteDBMeetings
        {
            // store date 'yyyy-mm-dd hh:mm:ss'
            public static Tuple<QueryStatus, List<Meeting>> get_meetings_asc_by_date()
            {
                return Query<Meeting>.read_query("SELECT MEETING.MeetingId, ADVISOR.Id, ADVISOR.FirstName, ADVISOR.LastName, Meeting.FounderId, MEETING.MeetingDate, MEETING.Status FROM MEETING INNER JOIN ADVISOR ON Meeting.AdvisorId = Advisor.Id ORDER BY MeetingDate ASC",
                    reader =>
                    {
                        return new Meeting(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.string_null_check(in reader, 6)
                        );
                    }
                    );
            }
            public static Tuple<QueryStatus, List<Meeting>> get_latest_meetings_limit_3()
            {
                return Query<Meeting>.read_query("SELECT MEETING.MeetingId, ADVISOR.Id, ADVISOR.FirstName, ADVISOR.LastName, Meeting.FounderId, MEETING.MeetingDate, MEETING.Status FROM MEETING INNER JOIN ADVISOR ON Meeting.AdvisorId = Advisor.Id ORDER BY MeetingDate DESC LIMIT 3",
                    reader =>
                    {
                        return new Meeting(
                            DB.string_null_check(in reader, 0),
                            DB.string_null_check(in reader, 1),
                            DB.string_null_check(in reader, 2),
                            DB.string_null_check(in reader, 3),
                            DB.string_null_check(in reader, 4),
                            DB.string_null_check(in reader, 5),
                            DB.string_null_check(in reader, 6)
                        );
                    }
                    );
            }
        }

        public static SQLiteDB Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new SQLiteDB();
                    }
                    return instance;
                }
            }
        }
    }
}


