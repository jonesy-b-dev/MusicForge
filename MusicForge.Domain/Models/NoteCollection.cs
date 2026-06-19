namespace MusicForge.Domain.Models
{
	public class NoteCollection
	{
		public NoteCollection(List<Note> notes)
		{
			Notes = notes;
		}
		public NoteCollection()
		{
			Notes = [];
		}
		public List<Note> Notes { get; set; }
	}
}
