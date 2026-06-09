using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages
{
	public class ScaleBuilderModel : PageModel
	{
		[BindProperty]
		public Note SelectedNote { get; set; }

		[BindProperty]
		public ScaleMode SelectedMode { get; set; }
		public NoteCollection scale = new();

		readonly ScaleCalculatorService _scaleCalculatorService;

		public ScaleBuilderModel(ScaleCalculatorService scaleCalulatorService)
		{
			_scaleCalculatorService = scaleCalulatorService;
		}
		public void OnGet()
		{
			SelectedNote = Note.C;
			SelectedMode = ScaleMode.Major;
			scale = _scaleCalculatorService.CalculateScale(SelectedNote, SelectedMode);
		}

		public void OnPost()
		{
			scale = _scaleCalculatorService.CalculateScale(SelectedNote, SelectedMode);
		}
	}
}
