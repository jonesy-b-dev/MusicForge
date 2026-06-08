using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages.Tools
{
	public class ChordBuilderModel : PageModel
	{
		[BindProperty]
		public Note SelectedNote { get; set; }

		[BindProperty]
		public ChordMode SelectedMode { get; set; }
		public string result = "";
		public NoteCollection chord = new();

		readonly ChordCalculatorService _chordCalulatorService;
		public ChordBuilderModel(ChordCalculatorService chordCalulatorService)
		{
			_chordCalulatorService = chordCalulatorService;
		}
		public void OnGet()
		{
			SelectedNote = Note.C;
			SelectedMode = ChordMode.MajorTriad;
			chord = _chordCalulatorService.CalculateChord(SelectedNote, SelectedMode);
		}

		public void OnPost()
		{
			chord = _chordCalulatorService.CalculateChord(SelectedNote, SelectedMode);
		}

	}
}
