using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Testing.Tests
{
	public class ChordCalculatorServiceTests
	{
		// CalculateChord
		[Fact]
		public void CalculateChord_GetCMajorTriadChord()
		{
			// Arrange
			NoteCollection expectedResult = new([Note.C, Note.E, Note.G]);
			ChordCalculatorService _chordCalculatorService = new();

			// Act
			NoteCollection result = _chordCalculatorService.CalculateChord(Note.C, ChordMode.MajorTriad);

			// Assert
			Assert.Equal(expectedResult.Notes, result.Notes);
		}
	}
}
