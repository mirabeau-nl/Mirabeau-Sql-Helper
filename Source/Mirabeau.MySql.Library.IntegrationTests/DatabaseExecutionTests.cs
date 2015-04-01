using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using AutoMapper;
using Mirabeau.Sql.Library;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.IntegrationTests
{
    [TestFixture, Explicit("Requires MYSQL database.")]
    public class DatabaseExecutionTests
    {
        // Created a free account on db4free. It is slow, and unreliable.. but allows some basic query testing.
        private const string ConnectionString = "server=db4free.net;uid=testcodelib;pwd=testcodelib;database=testcodelib;";

        [TestFixtureSetUp]
        public void Setup()
        {
            Mapper.CreateMap<IDataReader, Table>()
                .ForMember(dest => dest.Catelog, options => options.MapFrom(src => src["TABLE_CATALOG"].GetDbValueOrNullForReferenceType<string>()))
                .ForMember(dest => dest.Schema, options => options.MapFrom(src => src["TABLE_SCHEMA"].GetDbValueOrNullForReferenceType<string>()))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src["TABLE_NAME"].GetDbValueOrNullForReferenceType<string>()))
                .ForMember(dest => dest.Type, options => options.MapFrom(src => src["TABLE_TYPE"].GetDbValueOrNullForReferenceType<string>()))
                .ForMember(dest => dest.RowFormat, options => options.MapFrom(src => src["ROW_FORMAT"].GetDbValueOrNullForReferenceType<string>()))
                .ForMember(dest => dest.CreateTime, options => options.MapFrom(src => src["CREATE_TIME"].GetDbValueOrDefaultForValueType<DateTime>()))
                ;
        }

        [Test]
        public void ShouldExecuteReader()
        {
            MySqlHelper mySqlHelper = new MySqlHelper();

            using (DbDataReader dataReader = mySqlHelper.ExecuteReader(ConnectionString, CommandType.Text, "SELECT * FROM information_schema.tables"))
            {
                List<Table> rows = Mapper.Map<IDataReader, List<Table>>(dataReader);

                Assert.That(rows, Has.Count.GreaterThan(10));
            }
        }
    }
}
