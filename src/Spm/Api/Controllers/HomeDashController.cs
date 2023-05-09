using Application.Features.MainReports.Commands;
using Application.Features.MainReports.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeDashController : BaseController
{

    [HttpGet]
    [Route("GetOrderRates")]
    public async Task<IActionResult> GetOrderRates(string plantCode)
    {
        GetOrderRatesQuery getOrderRatesQuery = new () {PlantCode = plantCode};
        var chart = await Mediator.Send(getOrderRatesQuery);
        return Ok(chart);
    }

    [HttpGet]
    [Route("GetOpenAmountByCompany")]
    public async Task<IActionResult> GetOpenAmountByCompany(string plantCode)
    {
        GetOpenAmountbyCompanyQuery getOpenAmountbyCompanyQuery = new () {PlantCode = plantCode};
        var chart = await Mediator.Send(getOpenAmountbyCompanyQuery);
        return Ok(chart);
    }

    [HttpGet]
    [Route("GetOpenAmountByMaterialGroup")]
    public async Task<IActionResult> GetOpenAmountByMaterialGroup(string plantCode)
    {
        GetOpenAmountbyMaterialGroupQuery getOpenAmountbyMaterialGroupQuery = new () {PlantCode = plantCode};
        var chart = await Mediator.Send(getOpenAmountbyMaterialGroupQuery);
        return Ok(chart);
    }

    [HttpGet]
    [Route("GetUrgentHaveHF")]
    public async Task<IActionResult> GetUrgentHaveHF(string plantCode)
    {
        GetUrgentHaveHFQuery getUrgentHaveHFQuery = new () {PlantCode = plantCode};
        var chart = await Mediator.Send(getUrgentHaveHFQuery);
        return Ok(chart);
    }

    [HttpGet]
    [Route("GetUrgentNotHaveHF")]
    public async Task<IActionResult> GetUrgentNotHaveHF(string plantCode)
    {
        GetUrgentNotHaveHFQuery getUrgentNotHaveHFQuery = new () {PlantCode = plantCode};
        var chart = await Mediator.Send(getUrgentNotHaveHFQuery);
        return Ok(chart);
    }

    [HttpGet]
    [Route("GetTodayTable")]
    public async Task<IActionResult> GetTodayTable(string plantCode)
    {
        GetTodayTableQuery getTodayTableQuery = new () {PlantCode = plantCode};
        var table = await Mediator.Send(getTodayTableQuery);
        return Ok(table);
    }
}