using MusicForge.Domain.Models;
using MusicForge.BLL.Helpers;

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
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					return chord;
				case ChordMode.MinorTriad:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					return chord;
				case ChordMode.DiminishedTriad:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 6));  // diminished 5th
					return chord;
				case ChordMode.AugmentedTriad:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 8));  // augmented 5th
					return chord;
				case ChordMode.Sus2:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 2));  // major 2nd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					return chord;
				case ChordMode.Sus4:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 5));  // perfect 4th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					return chord;
				case ChordMode.MajorSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 11)); // major 7th
					return chord;
				case ChordMode.MinorSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 10)); // minor 7th
					return chord;
				case ChordMode.DominantSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 10)); // minor 7th
					return chord;
				case ChordMode.DiminishedSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 6));  // diminished 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 9));  // diminished 7th
					return chord;
				case ChordMode.HalfDiminishedSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 6));  // diminished 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 10)); // minor 7th
					return chord;
				case ChordMode.AugmentedSeventh:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 8));  // augmented 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 10)); // minor 7th
					return chord;
				case ChordMode.MajorNinth:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 11)); // major 7th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 14)); // major 9th
					return chord;
				case ChordMode.MinorNinth:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 3));  // minor 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 10)); // minor 7th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 14)); // major 9th
					return chord;
				case ChordMode.AddNine:
					chord.Notes.Add(note);
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 4));  // major 3rd
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 7));  // perfect 5th
					chord.Notes.Add(NoteCalculatorHelper.Offset(note, 14)); // major 9th (no 7th)
					return chord;
			}
			return chord;
		}
	}
}
