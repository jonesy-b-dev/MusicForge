using MusicForge.Domain.Models;
using MusicForge.BLL.Helpers;

namespace MusicForge.BLL.Services
{
	public class ScaleCalculatorService
	{
		public NoteCollection CalculateScale(Note note, ScaleMode scaleMode)
		{
			NoteCollection scale = new();
			int[] intervals = scaleMode switch
			{
				ScaleMode.Major => [0, 2, 4, 5, 7, 9, 11],
				ScaleMode.Minor => [0, 2, 3, 5, 7, 8, 10],
				ScaleMode.Dorian => [0, 2, 3, 5, 7, 9, 10],
				ScaleMode.Phrygian => [0, 1, 3, 5, 7, 8, 10],
				ScaleMode.Lydian => [0, 2, 4, 6, 7, 9, 11],
				ScaleMode.Mixolydian => [0, 2, 4, 5, 7, 9, 10],
				ScaleMode.Locrian => [0, 1, 3, 5, 6, 8, 10],
				ScaleMode.MajorPentatonic => [0, 2, 4, 7, 9],
				ScaleMode.MinorPentatonic => [0, 3, 5, 7, 10],
				ScaleMode.Blues => [0, 3, 5, 6, 7, 10],
				ScaleMode.HarmonicMinor => [0, 2, 3, 5, 7, 8, 11],
				ScaleMode.MelodicMinor => [0, 2, 3, 5, 7, 9, 11],
				ScaleMode.WholeTone => [0, 2, 4, 6, 8, 10],
				ScaleMode.Diminished => [0, 2, 3, 5, 6, 8, 9, 11],
				_ => []
			};

			foreach (int interval in intervals)
				scale.notes.Add(NoteCalculatorHelper.Offset(note, interval));

			return scale;
		}
	}
}
