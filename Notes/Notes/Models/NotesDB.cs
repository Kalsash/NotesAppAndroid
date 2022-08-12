using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class NotesDB
    {
        readonly SQLiteAsyncConnection db;
        public NotesDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTableAsync<Note>().Wait();
        }
        public Task<List<Note>> GetNotesAsync()
        {
            return db.Table<Note>().ToListAsync();
        }
        public Task<Note> GetNoteAsync(int id)
        {
            return db.Table<Note>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                return db.UpdateAsync(note);
            }
            return db.InsertAsync(note);
        }
        public Task<int> DeleteNoteAsync(Note note)
        {

            return db.DeleteAsync(note);
        }
    }
}
