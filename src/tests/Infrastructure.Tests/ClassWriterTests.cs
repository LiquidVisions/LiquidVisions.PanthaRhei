using System;
using System.Collections.Generic;
using System.Linq;
using LiquidVisions.PanthaRhei.Domain.IO;
using LiquidVisions.PanthaRhei.Domain.Logging;
using Moq;
using Xunit;

namespace LiquidVisions.PanthaRhei.Infrastructure.Tests
{
    public class ClassWriterTests
    {
        private readonly ClassWriter writer;
        private readonly Mock<IFile> mockedFileService = new();
        private readonly Mock<ILogger> mockedLogger = new();
        private readonly string fakePath = "C://Some/Fake/Path.cs";

        public ClassWriterTests()
        {
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClass());
            writer = new ClassWriter(mockedFileService.Object, mockedLogger.Object);
        }

        [Fact]
        public void Load_ShouldLoad()
        {
            // arrange
            // act
            writer.Load(fakePath);

            // assert
            Assert.NotEmpty(writer.Lines);
            mockedLogger.Verify(x => x.Trace(It.IsAny<string>()), Times.Once);
            mockedFileService.Verify(x => x.ReadAllLines(fakePath), Times.Once);
        }

        [Fact]
        public void AddNameSpace_ShouldAdd()
        {
            // arrange
            string nameSpace = "Just.Some.NameSpace";

            // act
            writer.Load(fakePath);
            writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", writer.Lines);
            mockedLogger.Verify(x => x.Trace($"Adding namespace {nameSpace} to the file."), Times.Once);
        }

        [Fact]
        public void AddNameSpace_ShouldSkipExistingNameSpace()
        {
            // arrange
            string nameSpace = "System.Collections.Generic";
            writer.Load(fakePath);

            // act
            writer.AddNameSpace(nameSpace);

            // assert
            Assert.Contains($"using {nameSpace};", writer.Lines);
            Assert.Equal(1, writer.Lines.Count(x => x == $"using {nameSpace};"));
            mockedLogger.Verify(x => x.Trace($"Adding namespace using {nameSpace}; to the file."), Times.Never);
        }

        [Fact]
        public void Save_ShouldSucceed()
        {
            // arrange
            writer.Load(fakePath);
            List<string> lines = writer.Lines;

            // act
            writer.Save(fakePath);

            // assert
            Assert.Null(writer.Lines);
            mockedFileService.Verify(x => x.WriteAllLines(fakePath, lines));
            mockedLogger.Verify(x => x.Trace($"Saving file {fakePath}."));
        }

        [Fact]
        public void WriteAt_ShouldSucceed()
        {
            // arrange
            writer.Load(fakePath);
            string text = "JustATest";
            string match = "class Class1";

            // act
            int index = writer.WriteAt(match, text);

            // assert
            mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Once);
            Assert.Equal($"   {text}", writer.Lines.First(x => x.Contains(text)));
            Assert.Equal(index, writer.IndexOf(text));
        }

        [Fact]
        public void WriteAt_MatchNotFound_ShouldNotWrite()
        {
            // arrange
            writer.Load(fakePath);
            string text = "JustATest";
            string match = "Match";

            // act
            int index = writer.WriteAt(match, text);

            // assert
            Assert.Equal(-1, index);
            mockedLogger.Verify(x => x.Trace($"Writing {text} to file."), Times.Never);
        }

        [Fact]
        public void WriteAt_EmptyRowShouldBeAdded()
        {
            // arrange
            writer.Load(fakePath);
            string text = string.Empty;
            int totalLines = writer.Lines.Count;

            // act
            writer.WriteAt(1, text);

            // assert
            Assert.Equal(totalLines + 1, writer.Lines.Count);
        }

        [Fact]
        public void WriteAt_EmptyRowShouldNotBeAddedWhenIndexIsOnAnEmptyRow()
        {
            // arrange
            writer.Load(fakePath);
            string text = string.Empty;
            int totalLines = writer.Lines.Count;

            // act
            writer.WriteAt(6, text);

            // assert
            Assert.Equal(totalLines, writer.Lines.Count);
        }

        [Fact]
        public void RemoveLinesUntil_ShouldSucceed()
        {
            // arrange
            writer.Load(fakePath);
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

        [Fact]
        public void Replace_ShouldSucceed()
        {
            // arrange
            writer.Load(fakePath);
            string match = "public class Class1";
            string replaceValue = "SomeReplaceValue";

            // act
            writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, writer.IndexOf(match));
            Assert.Equal(8, writer.IndexOf(replaceValue));
        }

        [Fact]
        public void Replace_ReplaceValueAlreadyInFile_ShouldNotReplace()
        {
            // arrange
            writer.Load(fakePath);
            string replaceValue = "public class Class1";
            string match = "SomeReplaceValue";

            // act
            writer.Replace(match, replaceValue);

            // assert
            Assert.Equal(-1, writer.IndexOf(match));
            Assert.Equal(8, writer.IndexOf(replaceValue));
        }

        [Fact]
        public void LastIndexOf_ShouldSucceed()
        {
            // arrange
            string match = "}";
            writer.Load(fakePath);

            // act
            int index = writer.LastIndexOf(match);

            // assert
            Assert.Equal(11, index);
        }

        [Fact]
        public void LastIndexOf_UnmatchedCharacter_ShouldReturnMinusOne()
        {
            // arrange
            string match = "@";
            writer.Load(fakePath);

            // act
            int index = writer.LastIndexOf(match);

            // assert
            Assert.Equal(-1, index);
        }

        [Fact]
        public void WriteAtEmptyRow_ShouldAddTextAtFirstEmptyRowAfterTheMatch()
        {
            // arrange
            writer.Load(fakePath);
            string text = "JustATest";

            // act
            writer.WriteAtEmptyRow("using", text);
            int index = writer.LastIndexOf(text);

            // arrange
            Assert.Equal(5, index);
            Assert.Equal(string.Empty, writer.Lines[index + 1]);
        }

        [Fact]
        public void Append_ShouldSkipEmptyString()
        {
            // arrange
            writer.Load(fakePath);
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
            writer.AppendMethodToClass(string.Empty);
            string result = string.Join(Environment.NewLine, writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Replace_ShouldSkipEmptyString()
        {
            // arrange
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());
            writer.Load(fakePath);
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
            writer.AddOrReplaceMethod(replaceString);
            string result = string.Join(Environment.NewLine, writer.Lines);

            // arrange
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Replace_EmptyString_ShouldThrowException()
        {
            // arrange
            mockedFileService.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(InfrastructureFakes.GetEmptyClassWithEmptyMethod());
            writer.Load(fakePath);

            // act
            // arrange
            Assert.Throws<IndexOutOfRangeException>(() => writer.AddOrReplaceMethod(string.Empty));
        }
    }
}
