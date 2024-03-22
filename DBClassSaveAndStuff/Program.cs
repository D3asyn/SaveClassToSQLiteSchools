using System.Data.SQLite;

namespace DBClassSaveAndStuff
{
    internal class Program
    {
        static string connectionString = "DataSource=school.db;version=3";

        static void Main(string[] args)
        {
            bool exist = File.Exists("school.db");
            if (!exist)
            {
                Console.Write("Initiating database... ");
                using (var progress = new ProgressBar())
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        progress.Report((double)i / 100);
                        Thread.Sleep(20);
                    }
                }
                InitDB();
                Console.WriteLine("\nDone.");
            }



            bool running = true;
            while (running)
            {
                Console.WriteLine("--------------");
                Console.WriteLine("1. Add School");
                Console.WriteLine("2. Delete School");
                Console.WriteLine("3. Update School");
                Console.WriteLine("4. Get All Schools");
                Console.WriteLine("q. Exit");
                Console.WriteLine("--------------");
                Console.WriteLine(@"Hint: AddSchool es UpdateSchoolnal spammeld a azt hogy 'test' vagy 't' mert sok variable van :)");
                Console.WriteLine("--------------");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                if (choice == "q" || choice == "Q" || choice == "quit")
                {
                    CaseGitAll();
                    Console.WriteLine("Goodbye");
                    break;
                }
                else if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                else
                {
                    int choiceInt = Convert.ToInt32(choice);
                    switch (choiceInt)
                    {
                        case 1:
                            CaseAdd();
                            break;

                        case 2:
                            CaseDelete();
                            break;

                        case 3:
                            CaseUpdate();
                            break;

                        case 4:
                            CaseGitAll();
                            break;
                    }
                }
            }
        }
        //Switch Functions
        static void CaseAdd()
        {
                string[] vars = { "Name", "Address", "City", "State", "Zip", "Phone", "Email", "Website", "Type" };
                string[] schoolVarsForSchool = new string[vars.Length];

                for (int i = 0; i < vars.Length; i++)
                {
                    Console.Write($"Enter the {vars[i]}: ");
                    string? input = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        schoolVarsForSchool[i] = input;
                    }
                    else
                    {
                        Console.WriteLine($"{vars[i]} cannot be empty. Please try again.");
                        i--;
                    }
                }


                School school = new School{
                    Name = schoolVarsForSchool[0],
                    Address = schoolVarsForSchool[1],
                    City = schoolVarsForSchool[2],
                    State = schoolVarsForSchool[3],
                    Zip = schoolVarsForSchool[4],
                    Phone = schoolVarsForSchool[5],
                    Email = schoolVarsForSchool[6],
                    Website = schoolVarsForSchool[7],
                    SchoolType = schoolVarsForSchool[8]
                };

            Console.WriteLine(school);
            AddSchool(school);
        }
        static void CaseDelete()
        {
            Console.Write("Enter the ID of the school you want to delete: ");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }
            else
            {
                int id = Convert.ToInt32(input);
                DeleteSchool(id);
            }
        }
        static void CaseUpdate()
        {
            Console.Write("Enter the ID of the school you want to update: ");
            int? inputId = Convert.ToInt32(Console.ReadLine());
            if (inputId != null)
            {
                string[] vars = { "Name", "Address", "City", "State", "Zip", "Phone", "Email", "Website", "Type" };
                string[] schoolVarsForSchool = new string[vars.Length];

                for (int i = 0; i < vars.Length; i++)
                {
                    Console.Write($"Enter the {vars[i]}: ");
                    string? input = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        schoolVarsForSchool[i] = input;
                    }
                    else
                    {
                        Console.WriteLine($"{vars[i]} cannot be empty. Please try again.");
                        i--;
                    }
                }

                School school = new School{
                    Id = Convert.ToInt32(inputId),
                    Name = schoolVarsForSchool[0],
                    Address = schoolVarsForSchool[1],
                    City = schoolVarsForSchool[2],
                    State = schoolVarsForSchool[3],
                    Zip = schoolVarsForSchool[4],
                    Phone = schoolVarsForSchool[5],
                    Email = schoolVarsForSchool[6],
                    Website = schoolVarsForSchool[7],
                    SchoolType = schoolVarsForSchool[8]
                };

                UpdateSchool(school);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        static void CaseGitAll()
        {
            List<School> schl = GitAllSchool();

            foreach (School school in schl)
            {
                Console.WriteLine(school);
            }
        }

        //Functions
        static void InitDB()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS School (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT,
                Address TEXT,
                City TEXT,
                State TEXT,
                Zip TEXT,
                Phone TEXT,
                Email TEXT,
                Website TEXT,
                SchoolType TEXT
				);";
                using (var createTableCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }
            }
        }
        static void AddSchool(School school)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO School 
                (Name, Address, City, State, Zip, Phone, Email, Website, SchoolType) 
                VALUES 
                (@Name, @Address, @City, @State, @Zip, @Phone, @Email, @Website, @SchoolType)";
                using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", school.Name);
                    insertCommand.Parameters.AddWithValue("@Address", school.Address);
                    insertCommand.Parameters.AddWithValue("@City", school.City);
                    insertCommand.Parameters.AddWithValue("@State", school.State);
                    insertCommand.Parameters.AddWithValue("@Zip", school.Zip);
                    insertCommand.Parameters.AddWithValue("@Phone", school.Phone);
                    insertCommand.Parameters.AddWithValue("@Email", school.Email);
                    insertCommand.Parameters.AddWithValue("@Website", school.Website);
                    insertCommand.Parameters.AddWithValue("@SchoolType", school.SchoolType);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }
        static void DeleteSchool(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM School WHERE Id = @Id";

                using (var deleteCommand = new SQLiteCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@Id", id);
                    deleteCommand.ExecuteNonQuery();
                }

                string resetQuery = "DELETE FROM sqlite_sequence WHERE name = 'School';";
                using (var resetCommand = new SQLiteCommand(resetQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }
        }
        static void UpdateSchool(School school)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = @"UPDATE School SET 
                    Name = @Name, 
                    Address = @Address, 
                    City = @City,
                    State = @State,
                    Zip = @Zip,
                    Phone = @Phone,
                    Email = @Email,
                    Website = @Website,
                    SchoolType = @SchoolType
                    WHERE Id = @Id";

                using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Name", school.Name);
                    updateCommand.Parameters.AddWithValue("@Address", school.Address);
                    updateCommand.Parameters.AddWithValue("@City", school.City);
                    updateCommand.Parameters.AddWithValue("@State", school.State);
                    updateCommand.Parameters.AddWithValue("@Zip", school.Zip);
                    updateCommand.Parameters.AddWithValue("@Phone", school.Phone);
                    updateCommand.Parameters.AddWithValue("@Email", school.Email);
                    updateCommand.Parameters.AddWithValue("@Website", school.Website);
                    updateCommand.Parameters.AddWithValue("@SchoolType", school.SchoolType);
                    updateCommand.Parameters.AddWithValue("@Id", school.Id);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }
        static List<School> GitAllSchool()
        {
            List<School> school = new List<School>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM School";


                using (var selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);

                            string name = reader["Name"].ToString();
                            string address = reader["Address"].ToString();
                            string city = reader["City"].ToString();
                            string state = reader["State"].ToString();
                            string zip = reader["Zip"].ToString();
                            string phone = reader["Phone"].ToString();
                            string email = reader["Email"].ToString();
                            string website = reader["Website"].ToString();
                            string schoolType = reader["SchoolType"].ToString();

                            school.Add(new School{
                                    Id = id,
                                    Name = name,
                                    Address = address,
                                    City = city,
                                    State = state,
                                    Zip = zip,
                                    Phone = phone,
                                    Email = email,
                                    Website = website,
                                    SchoolType = schoolType
                            });
                        }
                    }
                }
            }
            return school;
        }
    }
}
