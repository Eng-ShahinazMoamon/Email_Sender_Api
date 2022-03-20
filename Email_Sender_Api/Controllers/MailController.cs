using Email_Sender_Api.Data;
using Email_Sender_Api.Model;
using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email_Sender_Api.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MailController : ControllerBase
    {
        EmailSenderDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public MailController(EmailSenderDbContext dbContext, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;

        }

        // GET: api/Message
        [HttpGet]
        public IActionResult GetAllMessage()
        {
            try
            {
                return Ok(_dbContext.MailData.OrderByDescending(a => a.Id).ToList());
            }
            catch
            {
                return NoContent();
            }
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MailDataModel>> GetMessageById(int id)
        {
            try
            {
                var msg = await _dbContext.MailData.FindAsync(id);
                return msg;
            }
            catch
            {
                return id == 0 ? NotFound() : BadRequest();
            }
        }

        // POST: api/Message
        [HttpPost]
        public async Task<ActionResult<MailDataModel>> PostNewMessage(MailDataModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.MailData.Add(model);
                    await _dbContext.SaveChangesAsync();
                    return Ok("Successfully Added");
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Message/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MailDataModel>> DeleteMessage(int id)
        {
            try
            {
                var msg = await _dbContext.MailData.FindAsync(id);
                if (msg != null)
                {
                    var res = _dbContext.MailData.Remove(msg);
                    if(res != null)
                    {
                        await _dbContext.SaveChangesAsync();
                        return Ok();
                    }
                }
                return NotFound();

            }
            catch
            {
                return  BadRequest();
            }
        }

        [HttpPost("sendMail")]
        public async Task<ActionResult> SendMail(int msgId,string[] to)
        {
            try
            {
                var msg = await _dbContext.MailData.FindAsync(msgId);
                if (msg != null)
                {
                    // new message(To,Title,Body)
                    var message = new Message(to, msg.Subject,msg.Body);
                    await _emailSender.SendEmailAsync(message);
                    return Ok("Done");

                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    }
