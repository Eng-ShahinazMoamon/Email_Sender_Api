using Email_Sender_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Email_Sender_Api.Data
{
    public class EmailSenderDbContext:DbContext
    {
        public EmailSenderDbContext(DbContextOptions<EmailSenderDbContext> options) : base(options)
        {

        }

        public DbSet<MailDataModel> MailData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
