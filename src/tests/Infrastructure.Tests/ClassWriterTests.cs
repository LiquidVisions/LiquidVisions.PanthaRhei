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
        private readonly ClassWriter _writer;
        private readonly Mock<IFile> _mockedFileService = new();
        private readonly Mock<ILogger> _mockedLogger = new();
        private readonly string _fakePath = "C://Some/Fake/Path.cs";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWriterTests"/> class.
        /// </summary>
        public ClassWriterTests()
        {
            _mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClass());
            _writer = new ClassWriter(_mockedFileService.Object, _mockedLogger.Object);
            _writer.Load(_fakePath);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.Load(string)"/>.
        /// </summary>
        [Fact]
        public void Load_ShouldLoad()
        {
            // arrange
            // act
            // assert
            Assert.NotEmpty(_writer.Lines);
            _mockedLogger.Verify(x => x.Trace(It.IsAny<string>()), Times.Once);
            _mockedFileService.Verify(x => x.ReadAllLines(_fakePath), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.AddNameSpace(string)"/>.
        /// </summary>
        [Fact]
        public void AddNameSpace_ShouldAdd()
        {
            // arrange
            string nameSpace = "Just.Some.NameSpace";

            // act
            _writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", _writer.Lines);
            _mockedLogger.Verify(x => x.Trace($"Adding namespace {nameSpace} to the file."), Times.Once);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.AddNameSpace(string)"/>.
        /// Adding an existing namespace should be skipped.
        /// </summary>
        [Fact]
        public void AddNameSpace_ShouldSkipExistingNameSpace()
        {
            // arrange
            string nameSpace = "System.Collections.Generic";

            // act
            _writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", _writer.Lines);
            Assert.Equal(1, _writer.Lines.Count(x => x == $"using {nameSpace};"));
            _mockedLogger.Verify(x => x.Trace($"Adding namespace using {nameSpace}; to the file."), Times.Never);
        }

        /// <summary>
        /// Test for <see cref="ClassWriter.Save(string)"/>.
        /// </summary>
        [Fact]
        public void Save_ShouldSucceed()
        {
            // arrange
            List<string> lines = _writer.Lines;

            // act
            _writer.Save(_fakePath);

            // assert
            Assert.Null(_writer.Lines);
            _mockedFileService.Verify(x => x.WriteAllLines(_fakePath, lines));
            _mockedLogger.Verify(x => x.Trace($"Saving file {_fakePath}."));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// </summary>
        [Fact]
        public void WriteAt_ShouldSucceed()
        {
            // arrange
            string text = "JustATest";
            string match = "class Class1";

            // act
            int index = _writer.WriteAt(match, text);

            // assert
            _mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Once);
            Assert.Equal($"   {text}", _writer.Lines.First(x => x.Contains(text)));
            Assert.Equal(index, _writer.IndexOf(text));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Should not write when match is not found.
        /// </summary>
        [Fact]
        public void WriteAt_MatchNotFound_ShouldNotWrite()
        {
            // arrange
            string text = "JustATest";
            string match = "Match";

            // act
            int index = _writer.WriteAt(match, text);

            // assert
            Assert.Equal(-1, index);
            _mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Never);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Empty row should be added.
        /// </summary>
        [Fact]
        public void WriteAt_EmptyRowShouldBeAdded()
        {
            // arrange
            string text = string.Empty;
            int totalLines = _writer.Lines.Count;

            // act
            _writer.WriteAt(1, text);

            // assert
            Assert.Equal(totalLines + 1, _writer.Lines.Count);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.WriteAt(string, string)"/>.
        /// Empty row should not be added when index is on an empty row.
        /// </summary>
        [Fact]
        public void WriteAt_EmptyRowShouldNotBeAddedWhenIndexIsOnAnEmptyRow()
        {
            // arrange

            string text = string.Empty;
            int totalLines = _writer.Lines.Count;

            // act
            _writer.WriteAt(6, text);

            // assert
            Assert.Equal(totalLines, _writer.Lines.Count);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.RemoveLinesUntil(string, string)"/>.
        /// </summary>
        [Fact]
        public void RemoveLinesUntil_ShouldSucceed()
        {
            // arrange

            int totalLines = _writer.Lines.Count;
            string match = "public class Class1";
            string matchUntil = "}";

            // act
            _writer.RemoveLinesUntil(match, matchUntil);

            // assert
            Assert.NotEqual(totalLines, _writer.Lines.Count);
            Assert.Equal(totalLines - 3, _writer.Lines.Count);
            Assert.Equal(-1, _writer.IndexOf(match));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.Replace(string, string)"/>.
        /// </summary>
        [Fact]
        public void Replace_ShouldSucceed()
        {
            // arrange

            string match = "public class Class1";
            string replaceValue = "SomeReplaceValue";

            // act
            _writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, _writer.IndexOf(match));
            Assert.Equal(8, _writer.IndexOf(replaceValue));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.RemoveLinesUntil(string, string)"/>.
        /// ReplaceValue is already in file, should not replace.
        /// </summary>
        [Fact]
        public void Replace_ReplaceValueAlreadyInFile_ShouldNotReplace()
        {
            // arrange

            string replaceValue = "public class Class1";
            string match = "SomeReplaceValue";

            // act
            _writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, _writer.IndexOf(match));
            Assert.Equal(8, _writer.IndexOf(replaceValue));
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// </summary>
        [Fact]
        public void LastIndexOf_ShouldSucceed()
        {
            // arrange
            string match = "}";


            // act
            int index = _writer.LastIndexOf(match);

            // assert
            Assert.Equal(11, index);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// Should return -1 when match is not found.
        /// </summary>
        [Fact]
        public void LastIndexOf_UnmatchedCharacter_ShouldReturnMinusOne()
        {
            // arrange
            string match = "@";


            // act
            int index = _writer.LastIndexOf(match);

            // assert
            Assert.Equal(-1, index);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.LastIndexOf(string)"/>."/>
        /// Should add text at first empty row after the match.
        /// </summary>
        [Fact]
        public void WriteAtEmptyRow_ShouldAddTextAtFirstEmptyRowAfterTheMatch()
        {
            // arrange

            string text = "JustATest";

            // act
            _writer.WriteAtEmptyRow("using", text);
            int index = _writer.LastIndexOf(text);

            // arrange
            Assert.Equal(5, index);
            Assert.Equal(string.Empty, _writer.Lines[index + 1]);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AppendMethodToClass(string)"/>."/>
        /// </summary>
        [Fact]
        public void Append_ShouldSkipEmptyString()
        {
            // arrange

            string expectedResult = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidVisions.Jafar.Tests.Domain
{
   public class Class1
   {

   }
}";

            // act
            _writer.AppendMethodToClass(string.Empty);
            string result = string.Join(Environment.NewLine, _writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddOrReplaceMethod(string)"/>."/>
        /// </summary>
        [Fact]
        public void Replace_ShouldSkipEmptyString()
        {
            // arrange
            _mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());

            string replaceString = @"       public void Test()
       {
            // this is a test
       }";
            string expectedResult = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidVisions.Jafar.Tests.Domain
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
            _writer.AddOrReplaceMethod(replaceString);
            string result = string.Join(Environment.NewLine, _writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Test for <seealso cref="ClassWriter.AddOrReplaceMethod(string)"/>."/>
        /// <seealso cref="IndexOutOfRangeException"/> should be thrown when empty string is passed.
        /// </summary>
        [Fact]
        public void Replace_EmptyString_ShouldThrowException()
        {
            // arrange
            _mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());


            // act
            // arrange
            Assert.Throws<IndexOutOfRangeException>(() => _writer.AddOrReplaceMethod(string.Empty));
        }
    }
}
