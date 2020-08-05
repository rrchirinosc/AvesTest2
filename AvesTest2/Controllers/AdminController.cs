using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using AvesTest2.Database.DTO;
using AvesTest2.Database.Repositories;
using AvesTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using ClosedXML.Excel;
using MimeKit;
using AvesTest2.Infrastructure.Mail;
using AvesTest2.Infrastructure.Options;
using DocumentFormat.OpenXml.Wordprocessing;
using MailKit.Net.Smtp;



//TODO: Implement caching

namespace AvesTest2.Controllers
{
    public class AdminController : BaseController
    {
        private AdminViewModel model;
        private MailOptions _mailOptions;

        public AdminController(IOptions<ApplicationOptions> appOptions,
            IOptions<MailOptions> mailOptions):base(appOptions, mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }

        public async Task<IActionResult> Admin()
        {
#if DEBUG
            model = await AdminViewModel.Load(Connection);
            ViewData["Title"] = "Admin";
            return View(model);
#else
            return RedirectToAction("Index", "Home");
#endif
        }

        private string GetDateFromImage(int BirdId, string FileName)
        {
            string File = string.Format("{0}/{1}/{2}/{3}.jpg", Directory.GetCurrentDirectory(),"wwwroot/Images/Birds", BirdId, FileName);
            Image image = Image.FromFile(File);

            return Encoding.UTF8.GetString(image.GetPropertyItem(0x0132).Value);

        }

        [HttpGet]
        public JsonResult GetBirdTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<BirdDTO> model = repo.Birds.ToList();
            
            return Json(model);
        }

        [HttpGet]
        public JsonResult GetFamilyTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<FamilyDTO> model = repo.GetFamilyTable.ToList();
            return Json(model);
        }

        [HttpGet]
        public JsonResult GetImageTable()
        {
            BirdsRepository repo = new BirdsRepository(Connection);
            List<ImageDTO> model = repo.GetImageTable.ToList();

            return Json(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetStats()
        {
            StatsViewModel model = await StatsViewModel.Load(Connection);

            return Json(model.Stats);
        }

        [HttpPost]
        public int AddBird(string Name, string SciName, int FamilyId)
        {
            BirdDTO bird = new BirdDTO();
            int result = 0;

            // This check is not really necessary since I validate on the client but...
            if (Name == null || SciName == null || FamilyId == 0)
                return result;
            
            bird.Name = Name;
            bird.SciName = SciName;
            bird.FamilyId = FamilyId;

            BirdsRepository repo = new BirdsRepository(Connection);
            result = repo.AddBird(bird);

            return result;
        }

        [HttpPost]
        public int AddImage(int BirdId, string FileName, string Location,
            DateTime Date, int Country, string Coordinate, string Comment, bool KeyImage = false)
        {
            ImageDTO image = new ImageDTO();
            int result = 0;

            // This check is not really necessary since I validate on the client but...
            if (BirdId == 0 || FileName == null ||
                Location == null || Date.Year == 1 || Country == 0)
                return result;

            image.BirdId = BirdId;
            image.FileName = FileName;
            image.Location = Location;
            image.Date = Date.ToShortDateString();
            image.Country = Country;
            image.Coordinate = (Coordinate == null) ? "" : Coordinate;
            image.KeyImage = KeyImage;
            image.Comment = (Comment == null) ? "" : Comment;

            BirdsRepository repo = new BirdsRepository(Connection);

            // check whether or not the bird has already an image set as key and
            // if so remove the flag for that entry
            if (image.KeyImage == true)
            {
                repo.ResetKeyImage(BirdId);
            }

            result = repo.AddImage(image);

            return result;
        }

        [HttpPost]
        public int UpdateImage(int ImageId, string FileName, string Location,
            DateTime Date, int Country, string Coordinate, string Comment)
        {
            ImageDTO image = new ImageDTO();
            int result = 0;

            // Double-check that params are valid
            if (ImageId == 0 || (FileName == null &&
                Location == null && Date.Year == 1 && Country == 0 &&
                Coordinate == null && Comment == null))
                return result;

            image.Id = ImageId;
            image.FileName = FileName;
            image.Location = Location;
            image.Date = Date.Year == 1 ? null : Date.ToShortDateString();
            image.Country = Country;
            image.Coordinate = (Coordinate == null) ? "" : Coordinate;
            image.Comment = (Comment == null) ? "" : Comment;

            BirdsRepository repo = new BirdsRepository(Connection);
            result = repo.UpdateImage(image);

            return result;
        }

        [HttpPost]
        public int RemoveImage(int ImageId)
        {
            int result = 0;

            // This check is not really necessary since I validate on the client but...
            if (ImageId == 0)
                return result;

            BirdsRepository repo = new BirdsRepository(Connection);
            result = repo.RemoveImage(ImageId);

            return result;
        }

        [HttpGet]
        public  int MailBirdList(string Receiver, string Password)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Sender = new MailboxAddress("OnlyAves", _mailOptions.Sender);
                message.Receiver = new MailboxAddress("Raul", Receiver);
                message.Subject = "Current birds list";

                // setup list
                BirdsRepository repo = new BirdsRepository(Connection);
                List<BirdDTO> birds = repo.Birds.ToList();
                string Content = "Current Bird List\n\n";

                foreach (var bird in birds)
                {
                    Content += bird.Name + " (" + bird.Id.ToString() + ")";
                    Content += "\n";
                }

                message.Content = Content;
                var mimeMessage = CreateMimeMessage(message);
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Connect(_mailOptions.SmtpServer, _mailOptions.Port);  //using SSL
                    smtpClient.Authenticate(_mailOptions.Sender, Password); //check gmail allowances on failure
                    smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            return 1;
        }



        public IActionResult CreateBirdsXLSList()
        {
            // get the bird data
            BirdsRepository repo = new BirdsRepository(Connection);
            List<BirdDTO> birds = repo.Birds.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Bird");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "SciName";
                foreach (var bird in birds)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = bird.Name;
                    worksheet.Cell(currentRow, 2).Value = bird.SciName;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "birds.xlsx");
                }      
            }
        }

        private MimeMessage CreateMimeMessage(MailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Receiver);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                                    { Text = message.Content };

            return mimeMessage;
        }
    }    
 }