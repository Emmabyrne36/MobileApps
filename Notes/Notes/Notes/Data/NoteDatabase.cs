using Notes.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class NoteDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public NoteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync() =>
            _database.Table<Note>().ToListAsync();


        public Task<Note> GetNoteAsync(int id) =>
            _database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)
                return _database.UpdateAsync(note);

            return _database.InsertAsync(note);
        }

        public Task<int> DeleteNoteAsync(Note note) =>
            _database.DeleteAsync(note);
    }
}
