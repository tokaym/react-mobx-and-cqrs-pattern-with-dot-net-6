using Application.Features.MainReports.Commands;
using Application.Features.MainReports.Queries;
using Application.Features.ZM20Histories.Queries;
using Core.Application.Requests;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainReportController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, [FromQuery] OrderByRequest orderByRequest, string? search, string? date, string plantCode)
    {
        GetMainReportQuery getMainReportQuery = new() { PageRequest = pageRequest, OrderByRequest = orderByRequest, Search = search == null ? "" : search, Date = date, PlantCode = plantCode };
        var reports = await Mediator.Send(getMainReportQuery);
        return Ok(reports);
    }

    [HttpPost]
    [Authorize(Roles = "Rapor Oluştur")]
    public async Task<IActionResult> CreateReport(string reportDate, string plantCode)
    {
        if (plantCode == "643")
        {
            ReceiveReportQuery receiveReportQuery = new() { ReportDate = reportDate.TryDatetimeParse(), PlantCode = plantCode };
            var result = await Mediator.Send(receiveReportQuery);

            return Ok(result);
        }
        else
        {
            ReceiveRomaniaReportQuery receiveRomaniaReportQuery = new() { ReportDate = reportDate.TryDatetimeParse(), PlantCode = plantCode };
            var result = await Mediator.Send(receiveRomaniaReportQuery);

            return Ok(result);
        }
    }

    [HttpGet]
    [Route("GetReportDates")]
    public async Task<IActionResult> GetReportDates(string plantCode)
    {
        GetMainReportDatesQuery mainReportDatesQuery = new() { PlantCode = plantCode };
        var result = await Mediator.Send(mainReportDatesQuery);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetZm20s")]
    public async Task<IActionResult> GetZm20s(string materialSKU)
    {
        GetZm20sWithSKUQuery zm20SWithSKUQuery = new GetZm20sWithSKUQuery();
        zm20SWithSKUQuery.MaterialSKU = materialSKU;
        var zm20s = await Mediator.Send(zm20SWithSKUQuery);
        return Ok(zm20s);
    }


    [HttpGet]
    [Route("GetSumLast3Day")]
    public async Task<IActionResult> GetSumLast3Day(string type)
    {
        GetSumLast3DayQueries getSumLast3DayQueries = new() { Type = type };
        var list = await Mediator.Send(getSumLast3DayQueries);
        return Ok(list);
    }

    [HttpGet]
    [Route("GetOrderFulfillment")]
    public async Task<IActionResult> GetFulfillment()
    {
        GetOrderFulfillmentQuery getOrderFulfillmentQuery = new();
        var list = await Mediator.Send(getOrderFulfillmentQuery);
        return Ok(list);
    }
}