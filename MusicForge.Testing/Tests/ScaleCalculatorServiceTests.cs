using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Testing.Tests
{
	public class ScaleCalculatorServiceTests
	{
		// CalculateScale
		[Fact]
		public void CalculateScale_GetCMajorScale()
		{
			// Arrange
			NoteCollection expectedResult = new([Note.C, Note.D, Note.E, Note.F, Note.G, Note.A, Note.B]);
			ScaleCalculatorService _scaleCalculatorService = new();

			//Act
			NoteCollection result = _scaleCalculatorService.CalculateScale(Note.C, ScaleMode.Major);

			// Assert
			Assert.Equal(expectedResult.Notes, result.Notes);
		}
	}
}
