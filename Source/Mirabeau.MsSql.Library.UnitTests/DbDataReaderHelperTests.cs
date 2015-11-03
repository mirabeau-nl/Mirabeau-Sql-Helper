using System;

using Mirabeau.Sql;

using NUnit.Framework;

namespace Mirabeau.MsSql.Library.UnitTests
{
    /// <summary>
    /// Test class for the database reader.
    /// </summary>
    public class DbDataReaderHelperTests
    {
        [TestFixture]
        internal class GetDbValueOrDefaultForValueTypeTests
        {
            [Test]
            public void ShouldReturnCorrectValueWhenValueIsNotDbNullAndTypeIsInt()
            {
                // Arrange
                object input = 1;

                // Act
                var value = input.GetDbValueOrDefaultForValueType<int>();
                
                // Assert
                Assert.That(value, Is.EqualTo(1).And.TypeOf<int>());
            }

            [Test]
            public void ShouldReturnDefaultValueWhenValueIsDbNullAndTypeIsInt()
            {
                // Arrange
                object input = DBNull.Value;

                // Act
                var value = input.GetDbValueOrDefaultForValueType<int>();

                // Assert
                Assert.That(value, Is.EqualTo(0).And.TypeOf<int>());
            }

            [Test]
            public void ShouldReturnCorrectValueWhenValueIsNotDbNullAndTypeIsBool()
            {
                // Arrange
                object input = true;

                // Act
                var value = input.GetDbValueOrDefaultForValueType<bool>();

                // Assert
                Assert.That(value, Is.True.And.TypeOf<bool>());
            }

            [Test]
            public void ShouldReturnDefaultValueWhenValueIsDbNullAndTypeIsBool()
            {
                // Arrange
                object input = DBNull.Value;

                // Act
                var value = input.GetDbValueOrDefaultForValueType<bool>();

                // Assert
                Assert.That(value, Is.False.And.TypeOf<bool>());
            }
        }

        [TestFixture]
        internal class GetDbValueForNullableValueType
        {
            [Test]
            public void ShouldReturnCorrectValueWhenValueIsNotDbNullAndTypeIsInt()
            {
                // Arrange
                object input = 1;

                // Act
                var value = input.GetDbValueForNullableValueType<int>();

                // Assert
                Assert.That(value, Is.EqualTo(1).And.TypeOf<int>());
            }

            [Test]
            public void ShouldReturnDefaultValueWhenValueIsDbNullAndTypeIsInt()
            {
                // Arrange
                object input = DBNull.Value;

                // Act
                var value = input.GetDbValueForNullableValueType<int>();

                // Assert
                Assert.That(value, Is.Null);
            }

            [Test]
            public void ShouldReturnCorrectValueWhenValueIsNotDbNullAndTypeIsBool()
            {
                // Arrange
                object input = true;

                // Act
                var value = input.GetDbValueForNullableValueType<bool>();

                // Assert
                Assert.That(value, Is.True.And.TypeOf<bool>());
            }

            [Test]
            public void ShouldReturnDefaultValueWhenValueIsDbNullAndTypeIsBool()
            {
                // Arrange
                object input = DBNull.Value;

                // Act
                var value = input.GetDbValueForNullableValueType<bool>();

                // Assert
                Assert.That(value, Is.Null);
            }
        }

        [TestFixture]
        internal class GetDbValueOrNullForReferenceType
        {
            [Test]
            public void ShouldReturnCorrectValueWhenValueIsNotDbNullAndTypeIsInt()
            {
                // Arrange
                object input = "test";

                // Act
                var value = input.GetDbValueOrNullForReferenceType<string>();

                // Assert
                Assert.That(value, Is.EqualTo("test").And.TypeOf<string>());
            }

            [Test]
            public void ShouldReturnDefaultValueWhenValueIsDbNullAndTypeIsInt()
            {
                // Arrange
                object input = DBNull.Value;

                // Act
                var value = input.GetDbValueOrNullForReferenceType<string>();

                // Assert
                Assert.That(value, Is.Null);
            }
        }
    }
}
