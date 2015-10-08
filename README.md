# The SqlHelper #
### What is this repository for? ###

This is a helper assembly to make accessing the database easier. It helps you creating SqlParameters, executing queries and stored procedures and reading from the DataReader object.
This assembly has proven itself by being used for several clients and is based on the Microsoft Data Access Application Block for .NET.

Currently it supports both MsSql and MySql databases.
The base sourcecode was taken from the Microsoft .NET Data Access Application Block v2.0 and improved to support the latest .net Frameworks and has build in support for asynchronous programming (async/await)
There is also a now-static version (MsSqlHelper : IMsSqlHelper & MySqlHelper : IMySqlHelper)

The is also an option to generate Sql-staments for queries and stored procedure with parameter declaration to debug your queries in Sql Server Managerment studio (Sql-server only).

Build status| Coverage Status| NuGet downloads (MSSql) | NuGet downloads (MYSQL)
----------- | -------------- | --------------- | ---------------
[![Build status](https://ci.appveyor.com/api/projects/status/ip703gxi2cy8i6bh?svg=true)](https://ci.appveyor.com/project/jeroenpot/sqlhelper)|[![Coverage Status](https://coveralls.io/repos/jeroenpot/SqlHelper/badge.svg?branch=&service=github)](https://coveralls.io/github/jeroenpot/SqlHelper?branch=)|[![NuGet downloads MsSqlHelper](https://img.shields.io/nuget/dt/MsSqlHelper.svg)](https://www.nuget.org/packages/MsSqlHelper)|[![NuGet downloads MySqlHelper](https://img.shields.io/nuget/dt/MySqlHelper.svg)](https://www.nuget.org/packages/MySqlHelper)

### Examples ###
#### Sql parameters ####
```C#
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
```

#### Executing queries ####
```C#
            string connectionString = "my database connection string";

            var parameters = new List<SqlParameter>
            {
                1234.CreateSqlParameter("Parameter1"),
                "parmeter2value".CreateSqlParameter("Parameter2")
            };

            using (
                IDataReader dataReader = DatabaseHelper.ExecuteReader(connectionString, CommandType.StoredProcedure,
                    "MyStoredProcedure", parameters))
            {
                while (dataReader.Read())
                {
                    // Datareader helper
                    // For not-nullable columns:
                    int column1 = dataReader["databaseColumn1"].GetDbValueOrDefaultForValueType<int>();

                    // For nullable columns:
                    int? column2 = dataReader["databaseColumn2"].GetDbValueForNullableValueType<int>();
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
```
#### Generate Executable Sql Statements (sql-server only) ####
```C#
float? nullable = null;
decimal decimalValue = 123.456m;
string sql = "sp_test";

IList<SqlParameter> parameters = new List<SqlParameter>();
parameters.Add(0.CreateSqlParameter("value1"));
parameters.Add("hello world".CreateSqlParameter("value2"));
parameters.Add(nullable.CreateSqlParameter("value3"));
parameters.Add(decimalValue.CreateSqlParameter("value4"));
            
string executableSql = SqlDebugHelper.CreateExecutableSqlStatement(sql, parameters);
// Results in
//EXEC sp_test @value1 = 0, @value2 = N'hello world', @value3 = null, @value4 = 123.456"));
```

### Extend and override ###
Do you want to log all exceptions, or want to override any action in the SqlHelper?
See this test how to override and interact with every executed action

[ActionExecuterTests.cs](https://github.com/jeroenpot/SqlHelper/blob/master/Source/Mirabeau.MsSql.Library.UnitTests/ActionExecuterTests.cs)


### How do I get set up? ###

Build the project, or get the nuget package:
```sh
Install-Package MsSqlHelper
```

```sh
Install-Package MySqlHelper
```

If you have long running queries and need to change the connection timeout you can set the config value SqlCommandTimeout in te appsettings (in seconds)
```sh
<add key="SqlCommandTimeout" value="45"/>
```

### Contribution guidelines ###
* Pull request should be made to develop branch.
* Comments, methods and variables in english.
* Create unittests where possible.
* Try to stick to the existing coding style.
* Give a short description in the pull request what you're doing and why.

