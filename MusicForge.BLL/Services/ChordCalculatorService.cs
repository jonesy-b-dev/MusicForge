using MusicForge.Domain.Models;

namespace MusicForge.BLL.Services
{
	public class ChordCalculatorService
	{
		public NoteCollection CalculateChord(Note note, ChordMode chordMode)
		{
			NoteCollection chord = new();

			switch (chordMode)
			{
				case ChordMode.MajorTriad:
					chord.notes.Add(note);
					chord.notes.Add(Offset(note, 4)); // major 3rd
					chord.notes.Add(Offset(note, 7)); // perfect 5th
					return chord;
			}

			return chord;
		}
		private Note Offset(Note note, int semitones)
		{
			return (Note)(((int)note + semitones) % 12);
		}
	}
}
