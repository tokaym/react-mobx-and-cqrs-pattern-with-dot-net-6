using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Core.Application.Requests;
using Core.Utilities;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Application.Features.Settings.Profiles;
using System.Text;
using Application.Features.MainReports.Queries;
using System.Net;
using Application.Features.MainReports.Dtos;
using Application.Features.Mips.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Email")]
public class EmailController : BaseController
{
    [HttpPost("Send")]
    public async Task<IActionResult> Send([FromQuery] string? date)
    {
        bool result = false;
        try
        {
            List<string> settingNames = new List<string> { "SmtpAuth", "MailSettings" };
            GetSettingsByNameQuery getSettingsByNameQuery = new() { Names = settingNames };
            var settings = await Mediator.Send(getSettingsByNameQuery);

            #region Create HTML Mail Body 
            StringBuilder htmlBody = new StringBuilder();
            htmlBody.Append("<meta http-equiv='content-type' content='application/xhtml+xml; charset=UTF-8'/>");
            htmlBody.Append("<h2>Bu mail <a href='http://arank50vt:4005/'>Spare Uygulaması</a> tarafından otomatik oluşturulmuştur. Detayları incelemek için lütfen <a href='http://arank50vt:4005/'>linke</a> tıklayınız.</h1><br/>");
            htmlBody.Append("<html>");
            htmlBody.Append(HTMLTableLibrary.GetTableStyle());
            htmlBody.Append("<head></head><body>");


            htmlBody.Append("<h3>BMİ Özet</h3>");
            //Order Rates Table Append to HTML Body
            GetOrderRatesQuery getOrderRatesQuery = new() { PlantCode = "643" };
            var orderRates = await Mediator.Send(getOrderRatesQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(orderRates));

            htmlBody.Append("<h3>Son 3 Gün Özet</h3>");
            //Today Table Append to HTML Body
            GetTodayTableQuery getTodayTableQuery = new() { PlantCode = "643" };
            var todayTable = await Mediator.Send(getTodayTableQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(todayTable));

            htmlBody.Append("<h3>1-Tedarikçi grubu bazında açık sipariş dağılımı</h3>");
            //Open Quantity by Company Table Append to HTML Body
            GetOpenAmountbyCompanyQuery getOpenAmountbyCompanyQuery = new() { PlantCode = "643" };
            var openAmountbyCompany = await Mediator.Send(getOpenAmountbyCompanyQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(openAmountbyCompany));

            htmlBody.Append("<h3>2-Malzeme grubu bazında açık sipariş dağılımı</h3>");
            //Open Quantity by MAterial Group Table Append to HTML Body
            GetOpenAmountbyMaterialGroupQuery getOpenAmountbyMaterialGroupQuery = new() { PlantCode = "643" };
            var openAmountbyMaterialGroup = await Mediator.Send(getOpenAmountbyMaterialGroupQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(openAmountbyMaterialGroup));

            htmlBody.Append("<h3>3-Mal grubu bazında Acillerin detayı– HF si olup acil olan (601S)</h3>");
            //Urgent Have HF by Material Group Table Append to HTML Body
            GetUrgentHaveHFQuery getUrgentHaveHFQuery = new() { PlantCode = "643" };
            var urgentHaveHF = await Mediator.Send(getUrgentHaveHFQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(urgentHaveHF));

            htmlBody.Append("<h3>4-Mal grubu bazında Acillerin detayı – HFsi olmayan aciller  (601S)</h3>");
            //Urgent Not Have HF Table Append to HTML Body
            GetUrgentNotHaveHFQuery getUrgentNotHaveHF = new() { PlantCode = "643" };
            var urgentNotHaveHF = await Mediator.Send(getUrgentNotHaveHF);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(urgentNotHaveHF));



            htmlBody.Append("<h3>Romanya Özet</h3>");
            //Order Rates Table Append to HTML Body
            GetOrderRatesQuery rgetOrderRatesQuery = new() { PlantCode = "909" };
            var rorderRates = await Mediator.Send(rgetOrderRatesQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(rorderRates));

            htmlBody.Append("<h3>Son 3 Gün Özet</h3>");
            //Today Table Append to HTML Body
            GetTodayTableQuery rgetTodayTableQuery = new() { PlantCode = "909" };
            var rtodayTable = await Mediator.Send(rgetTodayTableQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(rtodayTable));

            htmlBody.Append("<h3>1-Tedarikçi grubu bazında açık sipariş dağılımı</h3>");
            //Open Quantity by Company Table Append to HTML Body
            GetOpenAmountbyCompanyQuery rgetOpenAmountbyCompanyQuery = new() { PlantCode = "909" };
            var ropenAmountbyCompany = await Mediator.Send(rgetOpenAmountbyCompanyQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(ropenAmountbyCompany));

            htmlBody.Append("<h3>2-Malzeme grubu bazında açık sipariş dağılımı</h3>");
            //Open Quantity by MAterial Group Table Append to HTML Body
            GetOpenAmountbyMaterialGroupQuery rgetOpenAmountbyMaterialGroupQuery = new() { PlantCode = "909" };
            var ropenAmountbyMaterialGroup = await Mediator.Send(rgetOpenAmountbyMaterialGroupQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(ropenAmountbyMaterialGroup));

            htmlBody.Append("<h3>3-Mal grubu bazında Acillerin detayı– HF si olup acil olan (601S)</h3>");
            //Urgent Have HF by Material Group Table Append to HTML Body
            GetUrgentHaveHFQuery rgetUrgentHaveHFQuery = new() { PlantCode = "909" };
            var rurgentHaveHF = await Mediator.Send(rgetUrgentHaveHFQuery);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(rurgentHaveHF));

            htmlBody.Append("<h3>4-Mal grubu bazında Acillerin detayı – HFsi olmayan aciller  (601S)</h3>");
            //Urgent Not Have HF Table Append to HTML Body
            GetUrgentNotHaveHFQuery rgetUrgentNotHaveHF = new() { PlantCode = "909" };
            var rurgentNotHaveHF = await Mediator.Send(rgetUrgentNotHaveHF);
            htmlBody.Append(HTMLTableLibrary.CreateWithDatas(rurgentNotHaveHF));

            htmlBody.Append("</body></html>");
            #endregion
            // #region Create Excel for Attachment
            // GetMainReportQuery getMainReportQuery = new() { PageRequest = new PageRequest { Page = 0, PageSize = 10000 }, OrderByRequest = new OrderByRequest { ColumnName = "MaterialSKU", Type = "desc" }, Search = "", PlantCode = "643" };
            // var reports = await Mediator.Send(getMainReportQuery);

            // StringBuilder str = new StringBuilder();
            // str.Append("<table border=`" + "1px" + "`b>");
            // str.Append("<tr>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Malzeme SKU</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Malzeme Adı</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Açık Miktar</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Kalem</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>HF</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Acil</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>İlk Sipariş Tarihi</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Firma</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>CD</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Stok</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Th Stok</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Mip</font></b></td>");
            // str.Append("<td><b><font face=Arial Narrow size=3>Gönderilen Miktar</font></b></td>");
            // str.Append("</tr>");
            // foreach (MainReportListDto val in reports.Items.ToList())
            // {
            //     str.Append("<tr>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.MaterialSKU?.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.MaterialName?.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.OpenAmount.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Item.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.HF.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Urgent.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.FirstOrderDate.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Company?.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.CD?.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Stock.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ThStock.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Mip?.ToString() + "</font></td>");
            //     str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Sent.ToString() + "</font></td>");
            //     str.Append("</tr>");
            // }
            // str.Append("</table>");
            // byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            // Stream st = new MemoryStream(temp);
            // #endregion

            var mailSettings = settings.FirstOrDefault(a => a.Name == "MailSettings")?.Value.Split("|");
            var email = new MimeMessage();
            List<string> toList = mailSettings[0].Split(';').ToList();
            List<string> ccList = mailSettings[1].Split(';').ToList();
            for (int i = 0; i < toList.Count(); i++)
            {
                if (!String.IsNullOrEmpty(toList[i]))
                    email.To.Add(MailboxAddress.Parse(toList[i]));
            }
            for (int i = 0; i < ccList.Count(); i++)
            {
                if (!String.IsNullOrEmpty(ccList[i]))
                    email.Cc.Add(MailboxAddress.Parse(ccList[i]));
            }
            email.From.Add(MailboxAddress.Parse(mailSettings[2]));
            email.Subject = "Header";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody.ToString() };

            // email.Attachments.Append(new MimePart("application/vnd.ms-excel")
            // {
            //     Content = new MimeContent(st),
            //     ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //     ContentTransferEncoding = ContentEncoding.Base64,
            //     FileName = "Servis İhtiyaçları"
            // });

            var smtpSettings = settings.FirstOrDefault(a => a.Name == "SmtpAuth")?.Value.Split("|");
            using var smtp = new SmtpClient();
            smtp.Connect("");
            // smtp.Authenticate(smtpSettings[0], smtpSettings[1]);

            string x = smtp.Send(email);
            smtp.Disconnect(true);
            result = true;
        }
        catch (System.Exception ex)
        {
            Logger.LogError("Api/EmailController/Send \r\n Hata: " + ex.ToString());
            result = false;
        }

        return Ok(result);
    }

    [HttpGet("Settings")]
    public async Task<IActionResult> Settings()
    {
        List<string> settingNames = new List<string> { "MailSettings" };
        GetSettingsByNameQuery getSettingsByNameQuery = new() { Names = settingNames };
        var settings = await Mediator.Send(getSettingsByNameQuery);
        var values = settings[0].Value.Split('|');
        MailSettingsDto result = new MailSettingsDto
        {
            Id = settings[0].Id.ToString(),
            Name = settings[0].Name,
            Description = settings[0].Description,
            To = values[0],
            Cc = values[1],
            From = values[2]
        };

        return Ok(result);
    }

    [HttpPut("SettingsUpdate")]
    public async Task<IActionResult> SettingsUpdate([FromBody] MailSettingsDto model)
    {
        UpdateMailSettingsCommand updateMailSettingsCommand = new() { mailSettingsDto = model };
        var result = await Mediator.Send(updateMailSettingsCommand);
        return Ok(result);
    }
}