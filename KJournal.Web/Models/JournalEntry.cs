namespace KJournal.Web.Models
{
    public class JournalEntry
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public JournalEntryType Type { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
