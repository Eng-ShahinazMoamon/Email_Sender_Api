using Email_Sender_Api.Data;
using Email_Sender_Api.Model;
using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult<ResultModel> GetAllMessage()
        {
            try
            {

                return new ResultModel
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = _dbContext.MailData.OrderByDescending(a => a.Id).ToList(),
                    Message = ""
                };
            }
            catch
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = "",
                    Message = "Internal Server Error"
                };
            }
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel>> GetMessageById(int id)
        {
            try
            {
                var msg = await _dbContext.MailData.FindAsync(id);
                return new ResultModel
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = msg,
                    Message = ""
                };
            }
            catch
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = "",
                    Message = "Internal Server Error"
                };
            }
        }

        // POST: api/Message
        [HttpPost]
        public async Task<ActionResult<ResultModel>> PostNewMessage(MailDataModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var res=_dbContext.MailData.Add(model);
                    if (res != null)
                    {
                        await _dbContext.SaveChangesAsync();

                        return new ResultModel
                        {
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK,
                            Data = "",
                            Message = ""
                        };
                    }
                    return new ResultModel
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotImplemented,
                        Data = "",
                        Message = ""
                    };
                }
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = "",
                    Message = ""
                };
            }
            catch
            {
                 return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = "",
                    Message = ""
                };
            }
        }

        // GET: api/Message/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteMessage(int id)
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
                        return new ResultModel
                        {
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK,
                            Data = "",
                            Message = ""
                        };
                    }
                }
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = "",
                    Message = ""
                };

            }
            catch
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = "",
                    Message = ""
                };
            }
        }

        [HttpPost("sendMail")]
        public async Task<ActionResult<ResultModel>> SendMail(int msgId,string[] to)
        {
            try
            {
                var msg = await _dbContext.MailData.FindAsync(msgId);
                if (msg != null)
                {
                    // new message(To,Title,Body)
                    var message = new Message(to, msg.Subject,msg.Body);
                    await _emailSender.SendEmailAsync(message);
                    return new ResultModel
                    {
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
                        Data = "",
                        Message = "Sended Successfully"
                    };
                }
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = "",
                    Message = ""
                };
            }
            catch
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = "",
                    Message = ""
                };
            }
        }
    }
    }
