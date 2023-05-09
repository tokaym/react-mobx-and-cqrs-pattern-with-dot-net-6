using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebApi.Controllers;
using Core.Utilities;
using Application.Features.MainReports.Queries;
using Application.Features.Estimates.Commands;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimateController : BaseController
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(IFormFile _file)
        {
            bool result = false;
            DateTime createdTime = DateTime.Now;
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

                        List<Estimate> estimates = new List<Estimate>();

                        List<DateTime> dates = new List<DateTime>();
                        for (int i = 0; i < 12; i++)
                        {
                            dates.Add(Convert.ToDateTime(ws.Cells[1, 7 + i].Value));
                        }

                        for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                        {
                            string materialSKUCell = ws.Cells[rw, 1].Value?.ToString();
                            if (materialSKUCell != null)
                            {
                                int materialSKU = materialSKUCell.TryInt32Parse();

                                if (materialSKU != 0)
                                {
                                    int counter = 7;
                                    foreach (DateTime item in dates)
                                    {

                                        Estimate estimate = new Estimate();
                                        estimate.MaterialSKU = materialSKUCell;
                                        estimate.Month = item.Month;
                                        estimate.Year = item.Year;
                                        estimate.Quantity = ws.Cells[rw, counter].Value?.ToString().TryInt32Parse();
                                        estimate.CreatedTime = createdTime;
                                        estimates.Add(estimate);
                                        counter++;
                                    }
                                }
                            }
                        }

                        CreateEstimateCommand estimateCreateCommand = new CreateEstimateCommand();
                        estimateCreateCommand.Estimates = estimates;
                        result = await Mediator.Send(estimateCreateCommand);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError("Api/EstimateController/Upload Exception Message: " + ex.ToString());
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetReport()
        {
            GetEstimationQuery getEstimationQuery = new();
            var result = await Mediator.Send(getEstimationQuery);
            return Ok(result);
        }
    }
}
