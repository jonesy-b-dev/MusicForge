namespace MusicForge.Domain.Models
{
	public class NoteCollection
	{
		public NoteCollection()
		{
			Notes = [];
		}
		public List<Note> Notes { get; set; }
	}
}
