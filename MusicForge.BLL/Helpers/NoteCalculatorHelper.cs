using MusicForge.Domain.Models;

namespace MusicForge.BLL.Helpers
{
	public static class NoteCalculatorHelper
	{
		public static Note Offset(Note note, int semitones)
		{
			return (Note)(((int)note + semitones) % 12);
		}
	}
}
