using Application.Features.ZM20S.Commands;
using Core.Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebApi.Controllers;
using Core.Utilities;
using Application.Features.Zs14s.Commands;
using Application.Features.MB51s.Commands;
using System.Text;
using Application.Features.MainReports.Queries;
using Application.Features.MainReports.Dtos;
using Application.Features.ZM20Histories.Commands;
using Application.Features.MB51Histories.Commands;
using Application.Features.RomaniaZm20s.Commands;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Dosya İşlemleri")]
    public class FileController : BaseController
    {
        [HttpPost]
        [Route("UploadZm20")]
        public async Task<IActionResult> UploadZm20(IFormFile _file, string reportDate)
        {
            bool result = false;
            try
            {
                DateTime _reportDate = Convert.ToDateTime(reportDate);
                if (_file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await _file.CopyToAsync(ms);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var ep = new ExcelPackage(ms);
                        var ws = ep.Workbook.Worksheets[0];

                        //Data var ise eğer Zm20 tablosunu boşaltıyoruz.
                        if (ws.Dimension.End.Row > 0)
                        {
                            DeleteZm20Command deleteCommand = new();
                            if (!await Mediator.Send(deleteCommand))
                                return Ok(result);

                            DeleteZm20HistoryCommand deleteHistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate) };
                            if (!await Mediator.Send(deleteHistoryCommand))
                                return Ok(result);


                            List<Zm20> zm20s = new List<Zm20>();
                            for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                            {
                                Zm20 zm20Row = new Zm20();
                                string materialSKUCell = ws.Cells[rw, 7].Value?.ToString();
                                if (materialSKUCell != null)
                                {
                                    int materialSKU = materialSKUCell.TryInt32Parse();

                                    if (materialSKU != 0)
                                    {

                                        zm20Row.Star = ws.Cells[rw, 1].Value?.ToString();
                                        zm20Row.UY = ws.Cells[rw, 2].Value?.ToString();
                                        zm20Row.UYCode = ws.Cells[rw, 3].Value?.ToString();
                                        zm20Row.DY = ws.Cells[rw, 4].Value?.ToString();
                                        zm20Row.ReleaseDate = ws.Cells[rw, 5].Value?.ToString().TryDatetimeParse();
                                        zm20Row.ArrivalDate = ws.Cells[rw, 6].Value?.ToString().TryDatetimeParse();
                                        zm20Row.MaterialSKU = ws.Cells[rw, 7].Value?.ToString();
                                        zm20Row.MaterialName = ws.Cells[rw, 8].Value?.ToString();
                                        zm20Row.Unit = ws.Cells[rw, 9].Value?.ToString();
                                        zm20Row.AmountDelivered = ws.Cells[rw, 10].Value?.ToString().TryInt32Parse();
                                        zm20Row.OpenAmount = ws.Cells[rw, 11].Value?.ToString().TryInt32Parse();
                                        zm20Row.RemainingStock = ws.Cells[rw, 12].Value?.ToString().TryInt32Parse();
                                        zm20Row.QualityStock = ws.Cells[rw, 13].Value?.ToString().TryInt32Parse();
                                        zm20Row.SatSasNo = ws.Cells[rw, 14].Value?.ToString();
                                        zm20Row.Item = ws.Cells[rw, 15].Value?.ToString().TryInt32Parse();
                                        zm20Row.ConfirmationDate = ws.Cells[rw, 16].Value?.ToString().TryDatetimeParse();
                                        zm20Row.Mip = ws.Cells[rw, 17].Value?.ToString().TryInt32Parse();
                                        zm20Row.TesMip = ws.Cells[rw, 18].Value?.ToString();
                                        zm20Row.SrvRef = ws.Cells[rw, 19].Value?.ToString().TryInt32Parse();
                                        zm20Row.AlanUYEmnStok = ws.Cells[rw, 20].Value?.ToString().TryInt32Parse();
                                        zm20Row.AlanUYYuvDeg = ws.Cells[rw, 21].Value?.ToString();
                                        zm20Row.Empty1 = ws.Cells[rw, 22].Value?.ToString();
                                        zm20Row.Empty2 = ws.Cells[rw, 23].Value?.ToString();
                                        zm20Row.Empty3 = ws.Cells[rw, 24].Value?.ToString();
                                        zm20Row.Empty4 = ws.Cells[rw, 25].Value?.ToString();
                                        zm20Row.TT = ws.Cells[rw, 26].Value?.ToString();
                                        zm20Row.Empty5 = ws.Cells[rw, 27].Value?.ToString();
                                        zm20Row.Seller = ws.Cells[rw, 28].Value?.ToString();
                                        zm20Row.SellerName = ws.Cells[rw, 29].Value?.ToString();
                                        zm20Row.Empty6 = ws.Cells[rw, 30].Value?.ToString();
                                        zm20Row.Empty7 = ws.Cells[rw, 31].Value?.ToString();
                                        zm20Row.Empty8 = ws.Cells[rw, 32].Value?.ToString();
                                        zm20Row.ContractNo = ws.Cells[rw, 33].Value?.ToString();
                                        zm20Row.Empty9 = ws.Cells[rw, 34].Value?.ToString();
                                        zm20Row.Item2 = ws.Cells[rw, 35].Value?.ToString();

                                        zm20s.Add(zm20Row);
                                    }
                                }
                            }


                            CreateZm20HistoryCommand zm20HistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate) };
                            zm20HistoryCommand.Zm20s = zm20s;
                            await Mediator.Send(zm20HistoryCommand);


                            CreateZm20Command zm20Command = new();
                            zm20Command.Zm20s = zm20s;
                            result = await Mediator.Send(zm20Command);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError("Api/FileController/UploadZm20/ Hata: " + ex.ToString());
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("UploadZs14")]
        public async Task<IActionResult> UploadZs14(IFormFile _file)
        {
            bool result = false;
            try
            {
                if (_file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await _file.CopyToAsync(ms);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var ep = new ExcelPackage(ms);
                        var ws = ep.Workbook.Worksheets[0];

                        //Data var ise eğer Zs14 tablosunu boşaltıyoruz.
                        if (ws.Dimension.End.Row > 0)
                        {
                            DeleteZs14Command deleteCommand = new DeleteZs14Command();
                            if (!await Mediator.Send(deleteCommand))
                                return Ok(result);


                            List<Zs14> zs14s = new List<Zs14>();
                            for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                            {
                                Zs14 zs14 = new Zs14();
                                string materialSKUCell = ws.Cells[rw, 6].Value?.ToString();
                                if (materialSKUCell != null)
                                {
                                    int materialSKU = materialSKUCell.TryInt32Parse();

                                    if (materialSKU != 0)
                                    {
                                        zs14.Star = ws.Cells[rw, 2].Value?.ToString();
                                        zs14.InstantDate = ws.Cells[rw, 3].Value?.ToString().TryDatetimeParse();
                                        zs14.TYeri = ws.Cells[rw, 5].Value?.ToString();
                                        zs14.MaterialSKU = ws.Cells[rw, 6].Value?.ToString();
                                        zs14.MipArea = ws.Cells[rw, 8].Value?.ToString();
                                        zs14.MaterialName = ws.Cells[rw, 10].Value?.ToString();
                                        zs14.Definition = ws.Cells[rw, 11].Value?.ToString();
                                        zs14.HfOrder = ws.Cells[rw, 14].Value?.ToString().TryInt32Parse();
                                        zs14.YsOrder = ws.Cells[rw, 15].Value?.ToString().TryInt32Parse();
                                        zs14.YDKIOrder = ws.Cells[rw, 16].Value?.ToString().TryInt32Parse();
                                        zs14.YDKDOrder = ws.Cells[rw, 17].Value?.ToString().TryInt32Parse();
                                        zs14.MIhrTes = ws.Cells[rw, 18].Value?.ToString().TryInt32Parse();
                                        zs14.YIIlkSip = ws.Cells[rw, 19].Value?.ToString().TryDatetimeParse();
                                        zs14.YDIlkSip = ws.Cells[rw, 20].Value?.ToString().TryDatetimeParse();
                                        zs14.Stnkrz = ws.Cells[rw, 21].Value?.ToString().TryInt32Parse();
                                        zs14.CgrSas = ws.Cells[rw, 22].Value?.ToString().TryInt32Parse();
                                        zs14.CgrSat = ws.Cells[rw, 23].Value?.ToString().TryInt32Parse();
                                        zs14.UYAmbar = ws.Cells[rw, 24].Value?.ToString().TryInt32Parse();
                                        zs14.UYDiger = ws.Cells[rw, 25].Value?.ToString().TryInt32Parse();
                                        zs14.YP = ws.Cells[rw, 26].Value?.ToString();
                                        zs14.IsSafety = ws.Cells[rw, 27].Value?.ToString().TryInt32Parse();
                                        zs14.MP = ws.Cells[rw, 28].Value?.ToString().TryInt32Parse();
                                        zs14.Mip = ws.Cells[rw, 29].Value?.ToString().TryInt32Parse();
                                        zs14.SG = ws.Cells[rw, 30].Value?.ToString();
                                        zs14.Dr = ws.Cells[rw, 31].Value?.ToString();
                                        zs14.Dr2 = ws.Cells[rw, 32].Value?.ToString();
                                        zs14.SasDelivery = ws.Cells[rw, 33].Value?.ToString().TryDatetimeParse();
                                        zs14.SasConfirm = ws.Cells[rw, 34].Value?.ToString().TryDatetimeParse();
                                        zs14.DeadlineDate = ws.Cells[rw, 35].Value?.ToString().TryDatetimeParse();
                                        zs14.Sat = ws.Cells[rw, 36].Value?.ToString().TryInt32Parse();
                                        zs14.Sas = ws.Cells[rw, 37].Value?.ToString().TryInt32Parse();
                                        zs14.Teslpln = ws.Cells[rw, 38].Value?.ToString().TryInt32Parse();
                                        zs14.ConsumptionValue = ws.Cells[rw, 39].Value?.ToString().TryInt32Parse();
                                        zs14.TransportStock = ws.Cells[rw, 40].Value?.ToString().TryInt32Parse();

                                        zs14s.Add(zs14);
                                    }
                                }
                            }

                            CreateZs14Command zs14Command = new CreateZs14Command();
                            zs14Command.Zs14s = zs14s;
                            result = await Mediator.Send(zs14Command);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError("Api/FileController/UploadZs14/ Hata: " + ex.ToString());
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("UploadMb51")]
        public async Task<IActionResult> UploadMb51(IFormFile _file, string reportDate)
        {
            bool result = false;
            try
            {
                if (_file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await _file.CopyToAsync(ms);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var ep = new ExcelPackage(ms);
                        var ws = ep.Workbook.Worksheets[0];

                        //Data var ise eğer Mb51 tablosunu boşaltıyoruz.
                        if (ws.Dimension.End.Row > 0)
                        {
                            DeleteMb51Command deleteCommand = new DeleteMb51Command();
                            if (!await Mediator.Send(deleteCommand))
                                return Ok(result);

                            DeleteMb51HistoryCommand deleteMb51HistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate) };
                            if (!await Mediator.Send(deleteMb51HistoryCommand))
                                return Ok(result);

                            List<Mb51> mb51s = new List<Mb51>();
                            for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                            {
                                Mb51 mb51 = new Mb51();
                                string materialSKUCell = ws.Cells[rw, 3].Value?.ToString();
                                if (materialSKUCell != null)
                                {
                                    int materialSKU = materialSKUCell.TryInt32Parse();

                                    if (materialSKU != 0)
                                    {
                                        mb51.MaterialSKU = ws.Cells[rw, 3].Value?.ToString();
                                        mb51.MaterialName = ws.Cells[rw, 4].Value?.ToString();
                                        mb51.Reference = ws.Cells[rw, 5].Value?.ToString();
                                        mb51.RegisterDate = ws.Cells[rw, 6].Value?.ToString().TryDatetimeParse();
                                        mb51.Amount = ws.Cells[rw, 7].Value?.ToString().TryInt32Parse();
                                        mb51.ITU = ws.Cells[rw, 8].Value?.ToString();
                                        mb51.Dpyr = ws.Cells[rw, 9].Value?.ToString().TryInt32Parse();
                                        mb51.HrkTUMtn = ws.Cells[rw, 10].Value?.ToString();
                                        mb51.MaterialInfo = ws.Cells[rw, 11].Value?.ToString();
                                        mb51.Item = ws.Cells[rw, 12].Value?.ToString().TryInt32Parse();
                                        mb51.Customer = ws.Cells[rw, 13].Value?.ToString();

                                        mb51s.Add(mb51);
                                    }
                                }
                            }

                            CreateMb51HistoryCommand mb51HistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate), Mb51s = mb51s };
                            result = await Mediator.Send(mb51HistoryCommand);

                            CreateMb51Command mb51Command = new CreateMb51Command();
                            mb51Command.Mb51s = mb51s;
                            result = await Mediator.Send(mb51Command);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError("Api/FileController/UploadMB51/ Hata: " + ex.ToString());
            }
            return Ok(result);
        }

        [HttpGet("MainReportExcel")]
        public async Task<IActionResult> GenerateExcelforMainReport([FromQuery] PageRequest pageRequest, [FromQuery] OrderByRequest orderByRequest, string? search, string plantCode)
        {
            GetMainReportQuery getMainReportQuery = new() { PageRequest = pageRequest, OrderByRequest = orderByRequest, Search = search == null ? "" : search, PlantCode = plantCode };
            var reports = await Mediator.Send(getMainReportQuery);

            StringBuilder str = new StringBuilder();
            str.Append("<meta http-equiv=\"content-type\" content=\"application/xhtml+xml; charset=UTF-8\"/>");
            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>Malzeme SKU</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Malzeme Adı</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Açık Miktar</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Kalem</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>HF</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Acil</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>İlk Sipariş Tarihi</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Firma</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>CD</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Stok</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Th Stok</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Mip</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Gönderilen Miktar</font></b></td>");
            str.Append("</tr>");
            foreach (MainReportListDto val in reports.Items.ToList())
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.MaterialSKU?.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.MaterialName?.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.OpenAmount.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Item.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.HF.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Urgent.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.FirstOrderDate.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Company?.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.CD?.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Stock.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ThStock.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Mip?.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Sent.ToString() + "</font></td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Yedek Parca Servis Ana Rapor.xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }

        [HttpPost]
        [Route("UploadRomaniaZm20")]
        public async Task<IActionResult> UploadRomaniaZm20(IFormFile _file, string reportDate)
        {
            bool result = false;
            try
            {
                if (_file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await _file.CopyToAsync(ms);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var ep = new ExcelPackage(ms);
                        var ws = ep.Workbook.Worksheets[0];

                        //Data var ise eğer Zs14 tablosunu boşaltıyoruz.
                        if (ws.Dimension.End.Row > 0)
                        {
                            DeleteRomaniaZm20Command deleteCommand = new DeleteRomaniaZm20Command();
                            if (!await Mediator.Send(deleteCommand))
                                return Ok(result);

                            DeleteRomaniaZm20HistoryCommand deleteHistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate) };
                            if (!await Mediator.Send(deleteHistoryCommand))
                                return Ok(result);

                            List<RomaniaZm20> romaniaZm20s = new List<RomaniaZm20>();
                            for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                            {
                                RomaniaZm20 romaniaZm20 = new RomaniaZm20();
                                string materialSKUCell = ws.Cells[rw, 6].Value?.ToString();
                                if (materialSKUCell != null)
                                {
                                    int materialSKU = materialSKUCell.TryInt32Parse();

                                    if (materialSKU != 0)
                                    {
                                        romaniaZm20.Item = ws.Cells[rw, 2].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.UY = ws.Cells[rw, 3].Value?.ToString();
                                        romaniaZm20.DPYR = ws.Cells[rw, 4].Value?.ToString();
                                        romaniaZm20.UY2 = ws.Cells[rw, 5].Value?.ToString();
                                        romaniaZm20.MaterialNo = ws.Cells[rw, 6].Value?.ToString();
                                        romaniaZm20.MaterialName = ws.Cells[rw, 7].Value?.ToString();
                                        romaniaZm20.ReleaseDate = ws.Cells[rw, 8].Value?.ToString().TryDatetimeParse();
                                        romaniaZm20.ArrivalDate = ws.Cells[rw, 9].Value?.ToString().TryDatetimeParse();
                                        romaniaZm20.TermQuantity = ws.Cells[rw, 10].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.Delivered = ws.Cells[rw, 11].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.Unit = ws.Cells[rw, 12].Value?.ToString();
                                        romaniaZm20.OpenQuantity = ws.Cells[rw, 13].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.MaterialSat = ws.Cells[rw, 14].Value?.ToString();
                                        romaniaZm20.TahKlm = ws.Cells[rw, 15].Value?.ToString();
                                        romaniaZm20.TrmSt = ws.Cells[rw, 16].Value?.ToString();
                                        romaniaZm20.SalesInf = ws.Cells[rw, 17].Value?.ToString();
                                        romaniaZm20.Item2 = ws.Cells[rw, 18].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.SaRequest = ws.Cells[rw, 19].Value?.ToString();
                                        romaniaZm20.Sag = ws.Cells[rw, 20].Value?.ToString();
                                        romaniaZm20.Definition = ws.Cells[rw, 21].Value?.ToString();
                                        romaniaZm20.Item3 = ws.Cells[rw, 22].Value?.ToString().TryInt32Parse();
                                        romaniaZm20.StBu = ws.Cells[rw, 23].Value?.ToString();
                                        romaniaZm20.Orderer = ws.Cells[rw, 24].Value?.ToString();
                                        romaniaZm20.ConfirmationDate = ws.Cells[rw, 25].Value?.ToString().TryDatetimeParse();
                                        romaniaZm20.Ad1 = ws.Cells[rw, 26].Value?.ToString();
                                        romaniaZm20.Suply = ws.Cells[rw, 27].Value?.ToString();
                                        romaniaZm20.Ad11 = ws.Cells[rw, 28].Value?.ToString();
                                        romaniaZm20.Mip = ws.Cells[rw, 29].Value?.ToString();
                                        romaniaZm20.Alternative = ws.Cells[rw, 30].Value?.ToString();
                                        romaniaZm20.TesMip = ws.Cells[rw, 31].Value?.ToString();
                                        romaniaZm20.SasItem = ws.Cells[rw, 32].Value?.ToString();
                                        romaniaZm20.SrvRef = ws.Cells[rw, 33].Value?.ToString();
                                        romaniaZm20.Quality = ws.Cells[rw, 34].Value?.ToString();
                                        romaniaZm20.AlanUYEm = ws.Cells[rw, 35].Value?.ToString();
                                        romaniaZm20.AlanUYY = ws.Cells[rw, 36].Value?.ToString();
                                        romaniaZm20.Star = ws.Cells[rw, 37].Value?.ToString();

                                        romaniaZm20s.Add(romaniaZm20);
                                    }
                                }
                            }
                            CreateRomaniaZm20HistoryCommand createRomaniaZm20HistoryCommand = new() { ReportDate = Convert.ToDateTime(reportDate) };
                            createRomaniaZm20HistoryCommand.RomaniaZm20s = romaniaZm20s;
                            await Mediator.Send(createRomaniaZm20HistoryCommand);

                            CreateRomaniaZm20Command createRomaniaZm20Command = new CreateRomaniaZm20Command();
                            createRomaniaZm20Command.RomaniaZm20s = romaniaZm20s;
                            result = await Mediator.Send(createRomaniaZm20Command);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError("Api/FileController/UploadRomaniaZm20/ Hata: " + ex.ToString());
            }
            return Ok(result);
        }

        [HttpGet("EstimateExcel")]
        public async Task<IActionResult> GenerateExcelforEstimate()
        {
            GetEstimationQuery getEstimationQuery = new();
            var result = await Mediator.Send(getEstimationQuery);

            StringBuilder str = new StringBuilder();
            str.Append("<meta http-equiv=\"content-type\" content=\"application/xhtml+xml; charset=UTF-8\"/>");
            str.Append(HTMLTableLibrary.CreateWithDatas(result));

            string month = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US"));
            HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=" + month + " Estimates " + DateTime.Now.ToString("dd.MM.yyyy") + ".xls");

            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }


    }
}