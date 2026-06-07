using MusicForge.Domain.Models;

namespace MusicForge.Domain.Models
{
	public class NoteCollection
	{
		public NoteCollection()
		{
			notes = new();
		}
		public List<Note> notes;
	}
}
