# README #

### What is this repository for? ###

This is a helper assembly to make accessing the database easier. It helps you creating SqlParameters, executing queries and stored procedures and reading from the DataReader object.
This assembly has proven itself by being used for several clients and is based on the Microsoft Data Access Application Block for .NET

### Examples ###
#### Sql parameters ####
            // This extention is available for the common value types and the DateTime object
            int value = 3;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("SqlParameterName");

            string stringValue = "some value";
            SqlParameter sqlParameter2 = stringValue.CreateSqlParameter("ParameterName");
            
            // Sets the parameter value to DBNull.Value
            int? nullableValue = null;
            SqlParameter sqlParameter3 = nullableValue.CreateSqlParameter("ParameterName");
            
            // If you have a parameter that is not supported out of the box, there is a generic method for you:
            ulong otherValue = 123;
            SqlParameterExtensions.CreateSqlParameter<ulong>(otherValue, "ParameterName");


#### Executing queries ####
            string connectionString = "my database connection string";

            var parameters = new List<SqlParameter> { 1234.CreateSqlParameter("Parameter1"), "parmeter2value".CreateSqlParameter("Parameter2") };

            using (IDataReader dataReader = DatabaseHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, "MyStoredProcedure", parameters))
            {
                while (dataReader.Read()) 
                { 
                    // Datareader helper
                    // For not-nullable columns:
                    int column1 = dataReader["databaseColumn1"].GetDbValueOrDefaultForValueType<int>();
                    
                    // For nullable columns:
                    int? column2 = dataReader["databaseColumn2"].GetDbValueOrNullForReferenceType<int>();
                }
            }
            
            // Transactions
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    DatabaseHelper.ExecuteScalar(sqlTransaction, CommandType.StoredProcedure, "StoredProcedureName1");
                    DatabaseHelper.ExecuteScalar(sqlTransaction, CommandType.StoredProcedure, "StoredProcedureName2");
                    sqlTransaction.Commit();
                }
            }

### How do I get set up? ###

Build the project, or get the nuget package:
```sh
Install-Package Mirabeau.Sql.Library
```

If you have long running queries and need to change the connection timeout you can set the config value SqlCommandTimeout in te appsettings (in seconds)
<add key="SqlCommandTimeout" value="45"/>

### Contribution guidelines ###
* Comments, methods and variables in english.
* Create unittests where possible.
* Try to stick to the existing coding style.
* Give a short description in the pull request what you're doing and why.

