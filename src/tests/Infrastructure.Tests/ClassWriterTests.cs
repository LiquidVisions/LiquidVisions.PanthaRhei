using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    /// <summary>
    /// Tests for <see cref="ClassWriter"/>.
    /// </summary>
    public class ClassWriterTests
    {
        private readonly ClassWriter writer;
        private readonly Mock<IFile> mockedFileService = new();
        private readonly Mock<ILogger> mockedLogger = new();
        private readonly string fakePath = "C://Some/Fake/Path.cs";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWriterTests"/> class.
        /// </summary>
        public ClassWriterTests()
        {
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClass());
            writer = new ClassWriter(mockedFileService.Object, mockedLogger.Object);
            writer.Load(fakePath);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.Load(string)"/>.
        /// </summary>
        [Fact]
        public void LoadShouldLoad()
        {
            // arrange
            // act
            // assert
            Assert.NotEmpty(writer.Lines);
            mockedLogger.Verify(x => x.Trace(It.IsAny<string>()), Times.Once);
            mockedFileService.Verify(x => x.ReadAllLines(fakePath), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.AddNameSpace(string)"/>.
        /// </summary>
        [Fact]
        public void AddNameSpaceShouldAdd()
        {
            // arrange
            string nameSpace = "Just.Some.NameSpace";

            // act
            writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", writer.Lines);
            mockedLogger.Verify(x => x.Trace($"Adding namespace {nameSpace} to the file."), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.AddNameSpace(string)"/>.
        /// Adding an existing namespace should be skipped.
        /// </summary>
        [Fact]
        public void AddNameSpaceShouldSkipExistingNameSpace()
        {
            // arrange
            string nameSpace = "System.Collections.Generic";

            // act
            writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", writer.Lines);
            Assert.Equal(1, writer.Lines.Count(x => x == $"using {nameSpace};"));
            mockedLogger.Verify(x => x.Trace($"Adding namespace using {nameSpace}; to the file."), Times.Never);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.Save(string)"/>.
        /// </summary>
        [Fact]
        public void SaveShouldSucceed()
        {
            // arrange
            List<string> lines = writer.Lines;

            // act
            writer.Save(fakePath);

            // assert
            Assert.Null(writer.Lines);
            mockedFileService.Verify(x => x.WriteAllLines(fakePath, lines));
            mockedLogger.Verify(x => x.Trace($"Saving file {fakePath}."));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// </summary>
        [Fact]
        public void WriteAtShouldSucceed()
        {
            // arrange
            string text = "JustATest";
            string match = "class Class1";

            // act
            int index = writer.WriteAt(match, text);

            // assert
            mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Once);
            Assert.Equal($"   {text}", writer.Lines.First(x => x.Contains(text, StringComparison.InvariantCulture)));
            Assert.Equal(index, writer.IndexOf(text));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Should not write when match is not found.
        /// </summary>
        [Fact]
        public void WriteAtMatchNotFoundShouldNotWrite()
        {
            // arrange
            string text = "JustATest";
            string match = "Match";

            // act
            int index = writer.WriteAt(match, text);

            // assert
            Assert.Equal(-1, index);
            mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Empty row should be added.
        /// </summary>
        [Fact]
        public void WriteAtEmptyRowShouldBeAdded()
        {
            // arrange
            string text = string.Empty;
            int totalLines = writer.Lines.Count;

            // act
            writer.WriteAt(1, text);

            // assert
            Assert.Equal(totalLines + 1, writer.Lines.Count);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Empty row should not be added when index is on an empty row.
        /// </summary>
        [Fact]
        public void WriteAtEmptyRowShouldNotBeAddedWhenIndexIsOnAnEmptyRow()
        {
            // arrange

            string text = string.Empty;
            int totalLines = writer.Lines.Count;

            // act
            writer.WriteAt(6, text);

            // assert
            Assert.Equal(totalLines, writer.Lines.Count);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.RemoveLinesUntil(string, string)"/>.
        /// </summary>
        [Fact]
        public void RemoveLinesUntilShouldSucceed()
        {
            // arrange

            int totalLines = writer.Lines.Count;
            string match = "public class Class1";
            string matchUntil = "}";

            // act
            writer.RemoveLinesUntil(match, matchUntil);

            // assert
            Assert.NotEqual(totalLines, writer.Lines.Count);
            Assert.Equal(totalLines - 3, writer.Lines.Count);
            Assert.Equal(-1, writer.IndexOf(match));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.Replace(string, string)"/>.
        /// </summary>
        [Fact]
        public void ReplaceShouldSucceed()
        {
            // arrange

            string match = "public class Class1";
            string replaceValue = "SomeReplaceValue";

            // act
            writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, writer.IndexOf(match));
            Assert.Equal(8, writer.IndexOf(replaceValue));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.RemoveLinesUntil(string, string)"/>.
        /// ReplaceValue is already in file, should not replace.
        /// </summary>
        [Fact]
        public void ReplaceReplaceValueAlreadyInFileShouldNotReplace()
        {
            // arrange

            string replaceValue = "public class Class1";
            string match = "SomeReplaceValue";

            // act
            writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, writer.IndexOf(match));
            Assert.Equal(8, writer.IndexOf(replaceValue));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// </summary>
        [Fact]
        public void LastIndexOfShouldSucceed()
        {
            // arrange
            string match = "}";


            // act
            int index = writer.LastIndexOf(match);

            // assert
            Assert.Equal(11, index);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// Should return -1 when match is not found.
        /// </summary>
        [Fact]
        public void LastIndexOfUnmatchedCharacterShouldReturnMinusOne()
        {
            // arrange
            string match = "@";


            // act
            int index = writer.LastIndexOf(match);

            // assert
            Assert.Equal(-1, index);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// Should add text at first empty row after the match.
        /// </summary>
        [Fact]
        public void WriteAtEmptyRowShouldAddTextAtFirstEmptyRowAfterTheMatch()
        {
            // arrange

            string text = "JustATest";

            // act
            writer.WriteAtEmptyRow("using", text);
            int index = writer.LastIndexOf(text);

            // arrange
            Assert.Equal(5, index);
            Assert.Equal(string.Empty, writer.Lines[index + 1]);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AppendMethodToClass(string)"/>."/>
        /// </summary>
        [Fact]
        public void AppendShouldSkipEmptyString()
        {
            // arrange

            string expectedResult = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Tests.Domain
{
   public class Class1
   {

   }
}";

            // act
            writer.AppendMethodToClass(string.Empty);
            string result = string.Join(Environment.NewLine, writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddOrReplaceMethod(string)"/>."/>
        /// </summary>
        [Fact]
        public void ReplaceShouldSkipEmptyString()
        {
            // arrange
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());

            string replaceString = @"       public void Test()
       {
            // this is a test
       }";
            string expectedResult = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidVisions.PanthaRhei.Tests.Domain
{
   public class Class1
   {
       public void Test()
       {
            // this is a test
       }
   }
}"
    ;

            // act
            writer.AddOrReplaceMethod(replaceString);
            string result = string.Join(Environment.NewLine, writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddOrReplaceMethod(string)"/>."/>
        /// <seealso cref="IndexOutOfRangeException"/> should be thrown when empty string is passed.
        /// </summary>
        [Fact]
        public void ReplaceEmptyStringShouldThrowException()
        {
            // arrange
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());


            // act
            // arrange
            Assert.Throws<IndexOutOfRangeException>(() => writer.AddOrReplaceMethod(string.Empty));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddBetween(string, string, string)"/>."/>
        /// </summary>
        [Fact]
        public void AddBetweenShouldAddBetweenBeginMatchAndEndMatch()
        {
            // arrange
            string beginMatch = "public class Class1";
            string endMatch = "}";
            string replaceValue = "Replacement";

            // act
            writer.AddBetween(beginMatch, endMatch, replaceValue);

            // assert
            Assert.Contains("   Replacement", writer.Lines);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddBetween(string, string, string)"/>."/>
        /// </summary>
        [Fact]
        public void AddOrReplaceMethodCoversIfCondition()
        {
            // Arrange
            string text = "SomeText";

            // Act
            writer.AddOrReplaceMethod(text);

            // Assert
            // Check if index is greater than 0
            Assert.True(writer.IndexOf(text) > 0);

            // Check if lines are removed correctly
            Assert.Contains("SomeText", writer.Lines);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddOrReplaceMethod(string)"/>."/>
        /// </summary>
        [Fact]
        public void AppendToMethodShouldInsertStatementInMethod()
        {
            // Arrange
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());
            ClassWriter writer = new(mockedFileService.Object, mockedLogger.Object);
            writer.Load(fakePath);


            string method = "public void Test()";
            string statement = "Console.WriteLine(\"Hello, World!\");";

            // Act
            writer.AppendToMethod(method, statement);

            // Assert
            Assert.Contains(statement, writer.Lines);
        }
    }
}
