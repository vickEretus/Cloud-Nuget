using Microsoft.SqlServer.Management.Smo;
using System;

namespace Database
{
    public class UserDB : AbstractDatabase
    {
        public UserDB() : base("revmetrix-u")
        {
            
        }
        
        public void DropIfExists(string value)
        {
            var table = Database.Tables[value];
            table?.DropIfExists();
        }

        public void CreateTables()
        {
            // User Table
            {
                // Drop old
                DropIfExists("User");

                // Create new
                var UserTable = new Table(Database, "User");

                // Username
                var username = new Column(UserTable, "username", DataType.VarChar(255))
                {
                    Nullable = false
                };
                UserTable.Columns.Add(username);

                // Salt
                var salt = new Column(UserTable, "salt", DataType.VarBinary(16))
                {
                    Nullable = false
                };
                UserTable.Columns.Add(salt);

                // Roles
                var roles = new Column(UserTable, "roles", DataType.VarChar(255))
                {
                    Nullable = false
                };
                UserTable.Columns.Add(roles);

                // Password
                var password = new Column(UserTable, "password", DataType.VarBinaryMax)
                {
                    Nullable = false
                };
                UserTable.Columns.Add(password);

                // Email
                var email = new Column(UserTable, "email", DataType.VarChar(255));
                UserTable.Columns.Add(email);

                // Phone
                var phone = new Column(UserTable, "phone", DataType.VarChar(255));
                UserTable.Columns.Add(phone);

                // ID
                var id = new Column(UserTable, "id", DataType.BigInt)
                {
                    IdentityIncrement = 1,
                    Nullable = false,
                    IdentitySeed = 1,
                    Identity = true
                };
                UserTable.Columns.Add(id);

                UserTable.Create();

                // Create the primary key constraint using SQL
                var sql = "ALTER TABLE [User] ADD CONSTRAINT PK_User PRIMARY KEY (id);";
                Database.ExecuteNonQuery(sql);
            }

            // Token Table
            {
                // Drop old
                DropIfExists("Token");

                // Create new
                var TokenTable = new Table(Database, "Token");

                /*
                // User ID
                var userIdKey = new ForeignKey(Database.Tables["User"], "FK_Token_User"); // Choose a descriptive name for the foreign key constraint
                var userId = new ForeignKeyColumn(userIdKey, "userid", "id");
                userIdKey.Columns.Add(userId);
                userIdKey.ReferencedTable = "User";
                userIdKey.ReferencedTableSchema = "id"; // Specify the referenced column in the "User" table
                userIdKey.Create();
                */

                // Expiration
                var expiration = new Column(TokenTable, "expiration", DataType.DateTime)
                {
                    Nullable = false
                };
                TokenTable.Columns.Add(expiration);

                // User ID
                var userId = new Column(TokenTable, "userid", DataType.BigInt); // Assuming "userid" is the name of the column in "TokenTable" that references "id" in "User" table
                userId.Nullable = false;
                TokenTable.Columns.Add(userId);

                // Token
                var token = new Column(TokenTable, "token", DataType.VarBinary(32))
                {
                    Nullable = false
                };
                TokenTable.Columns.Add(token);

                TokenTable.Create();

                // Create the foreign key after the "TokenTable" has been created
                {
                    TokenTable = Database.Tables["Token"]; // Retrieve the existing "TokenTable"

                    // User ID
                    var userIdKey = new ForeignKey(TokenTable, "FK_Token_User");
                    var userIdKeyCol = new ForeignKeyColumn(userIdKey, "userid");
                    userIdKeyCol.ReferencedColumn = "id";
                    userIdKey.Columns.Add(userIdKeyCol);
                    userIdKey.ReferencedTable = "User";

                    userIdKey.Create();
                }
            }
        }

    }
    /*
    public class A
    {
        public static void Main()
        {
            Server svr = new Server();
            Database db = new Database(svr, "TESTDB");
            db.Create();

            // PK Table  
            Table tab1 = new Table(db, "Table1");

            // Define Columns and add them to the table  
            Column col1 = new Column(tab1, "Col1", DataType.Int);

            col1.Nullable = false;
            tab1.Columns.Add(col1);
            Column col2 = new Column(tab1, "Col2", DataType.NVarChar(50));
            tab1.Columns.Add(col2);
            Column col3 = new Column(tab1, "Col3", DataType.DateTime);
            tab1.Columns.Add(col3);

            // Create the ftable  
            tab1.Create();

            // Define Index object on the table by supplying the Table1 as the parent table and the primary key name in the constructor.  
            Index pk = new Index(tab1, "Table1_PK");
            pk.IndexKeyType = IndexKeyType.DriPrimaryKey;

            // Add Col1 as the Index Column  
            IndexedColumn idxCol1 = new IndexedColumn(pk, "Col1");
            pk.IndexedColumns.Add(idxCol1);

            // Create the Primary Key  
            pk.Create();

            // Create Unique Index on the table  
            Index unique = new Index(tab1, "Table1_Unique");
            unique.IndexKeyType = IndexKeyType.DriUniqueKey;

            // Add Col1 as the Unique Index Column  
            IndexedColumn idxCol2 = new IndexedColumn(unique, "Col2");
            unique.IndexedColumns.Add(idxCol2);

            // Create the Unique Index  
            unique.Create();

            // Create Table2                    
            Table tab2 = new Table(db, "Table2");
            Column col21 = new Column(tab2, "Col21", DataType.NChar(20));
            tab2.Columns.Add(col21);
            Column col22 = new Column(tab2, "Col22", DataType.Int);
            tab2.Columns.Add(col22);
            tab2.Create();

            // Define a Foreign Key object variable by supplying the Table2 as the parent table and the foreign key name in the constructor.   
            ForeignKey fk = new ForeignKey(tab2, "Table2_FK");

            // Add Col22 as the foreign key column.   
            ForeignKeyColumn fkc = new ForeignKeyColumn(fk, "Col22", "Col1");
            fk.Columns.Add(fkc);
            fk.ReferencedTable = "Table1";

            // Create the foreign key on the instance of SQL Server.   
            fk.Create();

            // Get list of Foreign Keys on Table2  
            foreach (ForeignKey f in tab2.ForeignKeys)
            {
                Console.WriteLine(f.Name + " " + f.ReferencedTable + " " + f.ReferencedKey);
            }

            // Get list of Foreign Keys referencing table1  
            foreach (Table tab in db.Tables)
            {
                if (tab == tab1)
                    continue;
                foreach (ForeignKey f in tab.ForeignKeys)
                {
                    if (f.ReferencedTable.Equals(tab1.Name))
                        Console.WriteLine(f.Name + " " + f.Parent.Name);
                }
            }
        }
    }*/
}
